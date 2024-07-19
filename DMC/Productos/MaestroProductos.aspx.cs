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
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LlenarGridView();
                CargarSubFamilia();
                CargarFamilia();
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
                        new DataColumn("id_maestro", typeof(string)),
                        new DataColumn("codigo_maestro", typeof(string)),
                        new DataColumn("descripcion", typeof(string)),
                        new DataColumn("unidades", typeof(string)),
                        new DataColumn("formato", typeof(string)),
                        new DataColumn("descripcionn", typeof(string)),
                        new DataColumn("codigo_subfamilia", typeof(string)),
                        new DataColumn("Estado_Item", typeof(string)),
                        new DataColumn("activo", typeof(string))
                    });

                    // Utiliza el procedimiento almacenado adecuado
                    using (SqlCommand cmd = new SqlCommand("SP_ListarMaestroProductos", sqlConectar))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TextoBusqueda", textoBusqueda); // Nuevo parámetro para el filtro de texto
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    GV.Rows.Add(
                                        dr["id_maestro"].ToString(),
                                        dr["codigo_maestro"].ToString(),
                                        dr["descripcion"].ToString(),
                                        dr["unidades"].ToString(),
                                        dr["formato"].ToString(),
                                        dr["descripcionn"].ToString(),
                                        dr["codigo_subfamilia"].ToString(),
                                        dr["Estado_Item"].ToString(),
                                        dr["activo"].ToString()
                                    );
                                }
                            }
                        }
                    }

                    // Asigna el DataTable al GridView
                    GridViewMaestro.DataSource = GV;
                    GridViewMaestro.DataBind();
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

        protected void GridViewServicios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GridViewMaestro.PageIndex = e.NewPageIndex;
                LlenarGridView();
            }
            catch (Exception ex)
            {
                // Manejo de errores en la paginación
                Response.Write("Error: " + ex.Message);
                // Opcional: Loguear el error en un archivo de log o sistema de registro
            }
        }


          protected void GridViewServicios_RowCommand(object sender, GridViewCommandEventArgs e)
          {
            int rowIndex2 = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Ver")
            {
                VerBodega(rowIndex2, true);
                ModalPanelBodega.Visible = true;

            }
          }

        private void VerBodega(int rowIndex, bool Ver)
        {
            ModalPanel.Visible = false;

            if (rowIndex >= 0 && rowIndex < GridViewMaestro.Rows.Count)
            {
                int ID = Convert.ToInt32(GridViewMaestro.DataKeys[rowIndex]["id_maestro"]);
                if (Ver)
                {

                    string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand("SP_VerBodegaMaestroProducto", conn))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@ID", ID);

                                conn.Open();
                                SqlDataAdapter da = new SqlDataAdapter(cmd);
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                GridViewSaldos.DataSource = dt;
                                GridViewSaldos.DataBind();
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





        private void CargarFamilia()
        {
            string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection sqlConectar = new SqlConnection(conectar))
            {
                sqlConectar.Open();
                using (SqlCommand sql = new SqlCommand("SELECT * FROM familia WHERE activo = 1", sqlConectar))
                {
                    sql.CommandType = CommandType.Text;
                    using (SqlDataReader reader = sql.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            ddlfamilia.DataSource = reader;
                            ddlfamilia.DataTextField = "codigo_familia";
                            ddlfamilia.DataValueField = "id_familia";
                            ddlfamilia.DataBind();
                        }
                    }
                }
            }
            ddlfamilia.Items.Insert(0, new ListItem("Seleccionar Familia", "0"));
        }

        private void CargarSubFamilia()
        {
            string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection sqlConectar = new SqlConnection(conectar))
            {
                sqlConectar.Open();
                using (SqlCommand sql = new SqlCommand("SELECT * FROM SubFamilia WHERE activo = 1", sqlConectar))
                {
                    sql.CommandType = CommandType.Text;
                    using (SqlDataReader reader = sql.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            ddlsubfamilia.DataSource = reader;
                            ddlsubfamilia.DataTextField = "codigo_SubFamilia";
                            ddlsubfamilia.DataValueField = "id_SubFamilia";
                            ddlsubfamilia.DataBind();
                        }
                    }
                }
            }
            ddlsubfamilia.Items.Insert(0, new ListItem("Seleccionar SubFamilia", "0"));
        }





        protected void GridViewBodega_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Obtiene la fila que se está editando
            GridViewRow selectedRow = GridViewMaestro.Rows[e.NewEditIndex];
            // Asigna los valores de las celdas de la fila a diferentes controles en la página
            TextID.Text = HttpUtility.HtmlDecode(selectedRow.Cells[0].Text);
            TextProducto.Text = HttpUtility.HtmlDecode(selectedRow.Cells[1].Text);
            TxtDescrip.Text = HttpUtility.HtmlDecode(selectedRow.Cells[2].Text);
            TextCantidadPro.Text = HttpUtility.HtmlDecode(selectedRow.Cells[3].Text);
            TextFormatoMaestro.Text = HttpUtility.HtmlDecode(selectedRow.Cells[4].Text);

            ////Hacer el recolectar Familia y SubFamilia

            //ddlfamilia.SelectedValue = HttpUtility.HtmlDecode(selectedRow.Cells[5].Text);
            //ddlsubfamilia.SelectedValue = HttpUtility.HtmlDecode(selectedRow.Cells[6].Text);
            DropEstados.SelectedValue = HttpUtility.HtmlDecode(selectedRow.Cells[7].Text);
            DropActivo.SelectedValue = HttpUtility.HtmlDecode(selectedRow.Cells[8].Text);

            // Muestra el formulario de edición y oculta otros controles de la interfaz de usuario
            Actualizar.Visible = true;
            ModalPanel.Visible = true;
            ModalPanelBodega.Visible = false;

        }




        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(TextID.Text);
            string Codigo = TextProducto.Text;
            string Descripcion = TxtDescrip.Text;
            string Cantidad = TextCantidadPro.Text;
            string Formato = TextFormatoMaestro.Text;

            string Familia = ddlfamilia.SelectedValue;
            //string FamiliaCodigo = null;

            string SubFamilia = ddlsubfamilia.SelectedValue;
            //string SubFamiliaCodigo = null;

            string Estado = DropEstados.SelectedValue;
            string Activo = DropActivo.SelectedValue;
            //string IMG = TxtDescrip.Text;

            // Llama al procedimiento almacenado para actualizar el servicio
            ActualizarServicio(ID, Codigo, Descripcion, Cantidad, Formato, Familia, SubFamilia, Estado, Activo);

            // Vuelve a cargar los servicios para actualizar la GridView
            LlenarGridView();

            // Limpia los controles
            Session.Remove("SelectedRowID");

        }

        private void ActualizarServicio(int IDFamilia, string Codigo, string Descripcion, string Cantidad, string Formato, string Familia, string SubFamilia, string Estado, string Activo)
        {
            // Configura la conexión a la base de datos
            string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection sqlConectar = new SqlConnection(conectar))
            {
                // Configura y ejecuta el comando para llamar al procedimiento almacenado
                SqlCommand cmd = new SqlCommand("SP_ActualizarProductoMaestro", sqlConectar);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", IDFamilia);
                cmd.Parameters.AddWithValue("@Codigo", Codigo);
                cmd.Parameters.AddWithValue("@Descripcion", Descripcion);
                cmd.Parameters.AddWithValue("@Cantidad", Cantidad);
                cmd.Parameters.AddWithValue("@Formato", Formato);
                cmd.Parameters.AddWithValue("@Familia", Familia);
                cmd.Parameters.AddWithValue("@SubFamilia", SubFamilia);
                cmd.Parameters.AddWithValue("@Estado", Estado);
                cmd.Parameters.AddWithValue("@Activo", Activo);

                sqlConectar.Open();
                cmd.ExecuteNonQuery();
            }
            ModalPanel.Visible = false;
            LlenarGridView();

        }

    }
}