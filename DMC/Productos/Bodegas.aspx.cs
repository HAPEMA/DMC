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
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) { 
                LlenarGridView();
            }
        }

        private void LlenarGridView()
        {
            string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection sqlConectar = new SqlConnection(conectar))
            {
                try
                {
                    sqlConectar.Open();
                    DataTable GV = new DataTable();

                    // Define las columnas del DataTable
                    GV.Columns.AddRange(new DataColumn[]{
                new DataColumn("id_bodega", typeof(string)),
                new DataColumn("codigo_bodega", typeof(string)),
                new DataColumn("Nombre_Bodega", typeof(string)),
                new DataColumn("activo", typeof(string))
            });

                    // Utiliza el procedimiento almacenado adecuado
                    using (SqlCommand cmd = new SqlCommand("SP_ListarBodega", sqlConectar))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        // Ejecuta el comando y llena el DataTable
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    GV.Rows.Add(
                                        dr["id_bodega"].ToString(),
                                        dr["codigo_bodega"].ToString(),
                                        dr["Nombre_Bodega"].ToString(),
                                        dr["activo"].ToString()
                                    );
                                }
                            }
                        }
                    }

                    // Asigna el DataTable al GridView
                    GridViewBodega.DataSource = GV;
                    GridViewBodega.DataBind();
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

        protected void MostrarModal(object sender, EventArgs e)
        {
            ModalPanel.Visible = true;
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

            if (rowIndex >= 0 && rowIndex < GridViewBodega.Rows.Count)
            {
                int IDBodega = Convert.ToInt32(GridViewBodega.DataKeys[rowIndex]["id_bodega"]);

                if (activar)
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
                    try
                    {
                        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                        {
                            using (SqlCommand sqlCommand = new SqlCommand("SP_Activar_DesactivarBodega", sqlConnection))
                            {
                                sqlCommand.CommandType = CommandType.StoredProcedure;
                                sqlCommand.Parameters.AddWithValue("@ID", IDBodega);
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
            GridViewRow selectedRow = GridViewBodega.Rows[e.NewEditIndex];

            // Asigna los valores de las celdas de la fila a diferentes controles en la página
            TextID.Text = HttpUtility.HtmlDecode(selectedRow.Cells[0].Text);
            TextBodega.Text = HttpUtility.HtmlDecode(selectedRow.Cells[1].Text);
            TxtNombreBo.Text = HttpUtility.HtmlDecode(selectedRow.Cells[2].Text);


            // Muestra el formulario de edición y oculta otros controles de la interfaz de usuario
            ModalPanel.Visible = true;
            actualizar.Visible = true;
            volvercasa.Visible= true;
        }
        protected void OculatarActualizar(object sender, EventArgs e)
        {
            ModalPanel.Visible = false;
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            int IDBodega = Convert.ToInt32(TextID.Text);
            string CodigoBodega = TextBodega.Text;
            string NombreBodega = TxtNombreBo.Text;
          
            // Llama al procedimiento almacenado para actualizar el servicio
            ActualizarServicio(IDBodega, CodigoBodega, NombreBodega);

            // Vuelve a cargar los servicios para actualizar la GridView
            LlenarGridView();

            // Limpia los controles
            Session.Remove("SelectedRowID");
        }
        private void ActualizarServicio(int IDBodega, string CodigoBodega, string NombreBodega)
        {
            // Configura la conexión a la base de datos
            string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection sqlConectar = new SqlConnection(conectar))
            {
                // Configura y ejecuta el comando para llamar al procedimiento almacenado
                SqlCommand cmd = new SqlCommand("SP_ActualizarBodega", sqlConectar);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IDBodega", IDBodega);
                cmd.Parameters.AddWithValue("@CodigoBo", CodigoBodega);
                cmd.Parameters.AddWithValue("@NombreBo", NombreBodega);
              

                sqlConectar.Open();
                cmd.ExecuteNonQuery();
            }
            ModalPanel.Visible = false;
        }

    }
}