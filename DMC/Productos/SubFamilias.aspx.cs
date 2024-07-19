using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DMC.DMC.Productos
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LlenarGridView();
                LlarmarFamilia();
            }
        }

        private void LlenarGridView()
        {
            string textoBusqueda = txtTextoBusqueda.Text.Trim(); // Obtén el valor del filtro de texto

            string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection sqlConectar = new SqlConnection(conectar))
            {
                try
                {
                    sqlConectar.Open();
                    DataTable GV = new DataTable();

                    // Define las columnas del DataTable
                    GV.Columns.AddRange(new DataColumn[]{
                new DataColumn("id_SubFamilia", typeof(string)),
                new DataColumn("codigo_SubFamilia", typeof(string)),
                new DataColumn("descripcion", typeof(string)),
                new DataColumn("activo", typeof(string)),
                new DataColumn("descripcionFa", typeof(string))
            });

                    // Utiliza el procedimiento almacenado adecuado
                    using (SqlCommand cmd = new SqlCommand("SP_ListarSubFamilias", sqlConectar))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TextoBusqueda", textoBusqueda); // Nuevo parámetro para el filtro de texto

                        // Ejecuta el comando y llena el DataTable
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    GV.Rows.Add(
                                        dr["id_SubFamilia"].ToString(),
                                        dr["codigo_SubFamilia"].ToString(),
                                        dr["descripcion"].ToString(),
                                        dr["activo"].ToString(),
                                        dr["descripcionFa"].ToString()
                                    );
                                }
                            }
                        }
                    }

                    // Asigna el DataTable al GridView
                    GridViewSubFamilia.DataSource = GV;
                    GridViewSubFamilia.DataBind();
                }
                catch (Exception ex)
                {
                    // Manejo de errores
                    Response.Write("Error: " + ex.Message);
                }
                finally
                {
                    sqlConectar.Close();
                }
            }
        }

        protected void GridViewServicios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex2 = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Activar")
            {
                DesactivarActivarServicios(rowIndex2, true);
            }
        }

        private void DesactivarActivarServicios(int rowIndex, bool activar)
        {
            if (rowIndex >= 0 && rowIndex < GridViewSubFamilia.Rows.Count)
            {
                int ID = Convert.ToInt32(GridViewSubFamilia.DataKeys[rowIndex]["id_SubFamilia"]);
                if (activar)
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
                    try
                    {
                        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                        {
                            using (SqlCommand sqlCommand = new SqlCommand("SP_Activar_DesactivarSubFamilia", sqlConnection))
                            {
                                sqlCommand.CommandType = CommandType.StoredProcedure;
                                sqlCommand.Parameters.AddWithValue("@ID", ID);
                                sqlConnection.Open();
                                sqlCommand.ExecuteNonQuery();
                            }
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        Console.WriteLine("Error de SQL al Activar: " + sqlEx.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al Activar : " + ex.Message);
                    }
                }
                LlenarGridView();
            }
        }

        protected void GridViewBodega_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Obtiene la fila que se está editando
            GridViewRow selectedRow = GridViewSubFamilia.Rows[e.NewEditIndex];
            // Asigna los valores de las celdas de la fila a diferentes controles en la página
            TextID.Text = HttpUtility.HtmlDecode(selectedRow.Cells[0].Text);
            TextSubFamilia.Text = HttpUtility.HtmlDecode(selectedRow.Cells[1].Text);
            TxtDescrip.Text = HttpUtility.HtmlDecode(selectedRow.Cells[2].Text);

            // Muestra el formulario de edición y oculta otros controles de la interfaz de usuario
            PanelActualizar.Visible = true;
            actualizar.Visible = true;
        }

        protected void OculatarActualizar(object sender, EventArgs e)
        {
            PanelActualizar.Visible = false;
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            int IDSubFamilia = Convert.ToInt32(TextID.Text);
            string CodigoSubFamilia = TextSubFamilia.Text;
            string DescripSubFamilia = TxtDescrip.Text;

            // Llama al procedimiento almacenado para actualizar el servicio
            ActualizarServicio(IDSubFamilia, CodigoSubFamilia, DescripSubFamilia);

            // Vuelve a cargar los servicios para actualizar la GridView
            LlenarGridView();

            // Limpia los controles
            Session.Remove("SelectedRowID");

        }

        private void ActualizarServicio(int IDSubFamilia, string CodigoSubFamilia, string DescripSubFamilia)
        {
            // Configura la conexión a la base de datos
            string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection sqlConectar = new SqlConnection(conectar))
            {
                // Configura y ejecuta el comando para llamar al procedimiento almacenado
                SqlCommand cmd = new SqlCommand("SP_ActualizarSubFamilia", sqlConectar);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", IDSubFamilia);
                cmd.Parameters.AddWithValue("@CodigoSub", CodigoSubFamilia);
                cmd.Parameters.AddWithValue("@DescripSub", DescripSubFamilia);


                sqlConectar.Open();
                cmd.ExecuteNonQuery();
            }
            PanelActualizar.Visible = false;
            LlenarGridView();

        }


        protected void Guardar_Click(object sender, EventArgs e)
        {
            if (
                string.IsNullOrWhiteSpace(AgreCodigoSubFamilia.Text) ||
                string.IsNullOrWhiteSpace(AgregarDescripSubFamilia.Text)
            )
            {
                return;
            }
            //conexion a la BD
            string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection sqlConectar = new SqlConnection(conectar))
            using (SqlCommand sql = new SqlCommand("SP_AgregarSubFamilia", sqlConectar))
            {
                sqlConectar.Open();
                sql.CommandType = CommandType.StoredProcedure;

                string CodigoSubFa = AgreCodigoSubFamilia.Text;
                string DescripSubFa = AgregarDescripSubFamilia.Text;
                string Familia = ddlFamilia.SelectedValue;

                // Parámetros del procedimiento almacenado
                sql.Parameters.Add("@CodigoSubFa", SqlDbType.VarChar, 250).Value = CodigoSubFa;
                sql.Parameters.Add("@DescripSubFa", SqlDbType.VarChar, 250).Value = DescripSubFa;
                sql.Parameters.Add("@IDFamiliaSub", SqlDbType.VarChar, 250).Value = Familia;

                // Ejecutar el procedimiento almacenado
                sql.ExecuteNonQuery();
            }

            // Limpiar los campos después de guardar
            AgreCodigoSubFamilia.Text = "";
            AgregarDescripSubFamilia.Text = "";

            // Recarga los datos en el GridView después de la operación
            PanelAgrearFa.Visible = false;
            LlenarGridView();

        }

        private void LlarmarFamilia()
        {
            string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection sqlConectar = new SqlConnection(conectar))
            {
                sqlConectar.Open();
                using (SqlCommand sql = new SqlCommand("select codigo_familia, id_familia from familia where activo = 1", sqlConectar))
                {
                    sql.CommandType = CommandType.Text;

                    ddlFamilia.DataSource = sql.ExecuteReader();
                    ddlFamilia.DataTextField = "codigo_familia";
                    ddlFamilia.DataValueField = "id_familia";
                    ddlFamilia.DataBind();

                    //// Insertar "Todos" al inicio
                    //System.Web.UI.WebControls.ListItem itemTodos = new System.Web.UI.WebControls.ListItem("Todos", "Todos");
                    //ddlProducto.Items.Insert(0, itemTodos);

                    // Insertar "Selecciona Tienda" después de "Todos"
                    ddlFamilia.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecciona una Famila", ""));
                }
            }
        }
        protected void OcultarAgregar(object sender, EventArgs e)
        {
            PanelAgrearFa.Visible = false;
            PanelActualizar.Visible = false;
        }
        protected void MostrarAgregar(object o, EventArgs e)
        {
            PanelAgrearFa.Visible = true;
            PanelActualizar.Visible = false;

        }
        protected void OcultarEditar(object sender, EventArgs e)
        {
            PanelAgrearFa.Visible = false;
            PanelActualizar.Visible = false;

        }
        protected void MostrarEditar(object o, EventArgs e)
        {
            PanelAgrearFa.Visible = false;
            PanelActualizar.Visible = true;

        }
        protected void ButtonMostrar_Click(object sender, EventArgs e)
        {
            LlenarGridView();
        }
    }
}