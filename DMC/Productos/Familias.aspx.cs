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
    public partial class Familiasaspx : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LlenarGridView();
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
                new DataColumn("id_familia", typeof(string)),
                new DataColumn("codigo_familia", typeof(string)),
                new DataColumn("descripcion", typeof(string)),
                new DataColumn("activo", typeof(string))
            });

                    // Utiliza el procedimiento almacenado adecuado
                    using (SqlCommand cmd = new SqlCommand("SP_ListarFamilias", sqlConectar))
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
                                        dr["id_familia"].ToString(),
                                        dr["codigo_familia"].ToString(),
                                        dr["descripcion"].ToString(),
                                        dr["activo"].ToString()
                                    );
                                }
                            }
                        }
                    }

                    // Asigna el DataTable al GridView
                    GridViewFamilia.DataSource = GV;
                    GridViewFamilia.DataBind();
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

        protected void ButtonMostrar_Click(object sender, EventArgs e)
        {
            LlenarGridView();
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
            if (rowIndex >= 0 && rowIndex < GridViewFamilia.Rows.Count)
            {
                int ID = Convert.ToInt32(GridViewFamilia.DataKeys[rowIndex]["id_familia"]);
                if (activar)
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
                    try
                    {
                        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                        {
                            using (SqlCommand sqlCommand = new SqlCommand("SP_Activar_DesactivarFamilia", sqlConnection))
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
            GridViewRow selectedRow = GridViewFamilia.Rows[e.NewEditIndex];
            // Asigna los valores de las celdas de la fila a diferentes controles en la página
            TextID.Text = HttpUtility.HtmlDecode(selectedRow.Cells[0].Text);
            TextFamilia.Text = HttpUtility.HtmlDecode(selectedRow.Cells[1].Text);
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
            int IDFamilia = Convert.ToInt32(TextID.Text);
            string CodigoFamilia = TextFamilia.Text;
            string DescripFamilia = TxtDescrip.Text;

            // Llama al procedimiento almacenado para actualizar el servicio
            ActualizarServicio(IDFamilia, CodigoFamilia, DescripFamilia);

            // Vuelve a cargar los servicios para actualizar la GridView
            LlenarGridView();

            // Limpia los controles
            Session.Remove("SelectedRowID");

        }

        private void ActualizarServicio(int IDFamilia, string CodigoFamilia, string DescripFamilia)
        {
            // Configura la conexión a la base de datos
            string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection sqlConectar = new SqlConnection(conectar))
            {
                // Configura y ejecuta el comando para llamar al procedimiento almacenado
                SqlCommand cmd = new SqlCommand("SP_ActualizarFamilia", sqlConectar);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IDFamilia", IDFamilia);
                cmd.Parameters.AddWithValue("@CodigoFa", CodigoFamilia);
                cmd.Parameters.AddWithValue("@DescripcionFa", DescripFamilia);


                sqlConectar.Open();
                cmd.ExecuteNonQuery();
            }
            PanelActualizar.Visible = false;
            LlenarGridView();

        }




        protected void Guardar_Click(object sender, EventArgs e)
        {
            if (
                string.IsNullOrWhiteSpace(AgreCodigoFamilia.Text) ||
                string.IsNullOrWhiteSpace(AgregarDescripFamilia.Text)
            )
            {
                return;
            }
            //conexion a la BD
            string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection sqlConectar = new SqlConnection(conectar))
            using (SqlCommand sql = new SqlCommand("SP_AgregarFamilia", sqlConectar))
            {
                sqlConectar.Open();
                sql.CommandType = CommandType.StoredProcedure;

                string CodigoFa = AgreCodigoFamilia.Text;
                string DescripFa = AgregarDescripFamilia.Text;

                // Parámetros del procedimiento almacenado
                sql.Parameters.Add("@CodigoFa", SqlDbType.VarChar, 250).Value = CodigoFa;
                sql.Parameters.Add("@DescripFa", SqlDbType.VarChar, 250).Value = DescripFa;

                // Ejecutar el procedimiento almacenado
                sql.ExecuteNonQuery();
            }

            // Limpiar los campos después de guardar
            AgreCodigoFamilia.Text = "";
            AgregarDescripFamilia.Text = "";

            // Recarga los datos en el GridView después de la operación
            PanelAgrearFa.Visible = false;
            LlenarGridView();

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

    }
}