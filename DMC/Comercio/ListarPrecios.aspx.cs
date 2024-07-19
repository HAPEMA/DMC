using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DMC.DMC.Comercio
{
    public partial class WebForm2 : System.Web.UI.Page
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
            string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection sqlConectar = new SqlConnection(conectar))
            {
                try
                {
                    sqlConectar.Open();
                    DataTable GV = new DataTable();

                    // Define las columnas del DataTable
                    GV.Columns.AddRange(new DataColumn[]{
                        new DataColumn("ID_LISTAPRECIO", typeof(string)),
                        new DataColumn("DESCRIPCION", typeof(string)),
                        new DataColumn("ACTIVO", typeof(string))
                    });

                    // Utiliza el procedimiento almacenado adecuado
                    using (SqlCommand cmd = new SqlCommand("SP_ListarPrecioCliente", sqlConectar))
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
                                        dr["ID_LISTAPRECIO"].ToString(),
                                        dr["DESCRIPCION"].ToString(),
                                        dr["ACTIVO"].ToString()
                                    );
                                }
                            }
                        }
                    }

                    // Asigna el DataTable al GridView
                    GridViewPrecio.DataSource = GV;
                    GridViewPrecio.DataBind();
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
        protected void Guardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DescipPrecio.Text))
            {
                return;
            }
            // Conexión a la BD
            string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            try
            {
                using (SqlConnection sqlConectar = new SqlConnection(conectar))
                using (SqlCommand sql = new SqlCommand("SP_AgregarPrecioCliente", sqlConectar))
                {
                    sqlConectar.Open();
                    sql.CommandType = CommandType.StoredProcedure;

                    string Descripcion = DescipPrecio.Text;

                    // Parámetros del procedimiento almacenado
                    sql.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value = Descripcion;

                    // Ejecutar el procedimiento almacenado
                    sql.ExecuteNonQuery();
                }

                // Limpiar los campos después de guardar
                DescipPrecio.Text = "";

                // Recarga los datos en el GridView después de la operación
                PanelAgrearPrecio.Visible = false;
                LlenarGridView();
            }
            catch (SqlException ex)
            {
                // Mostrar el mensaje de error de la base de datos
                string errorMessage = "Error en la operación de la base de datos: " + ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += " Detalle: " + ex.InnerException.Message;
                }
                // Aquí puedes mostrar el mensaje de error en una etiqueta o algún otro control en tu página
                lblMensaje.Text = errorMessage; // Asumiendo que tienes un Label llamado lblErrorMessage
                lblMensaje.Visible = true;
            }
            catch (Exception ex)
            {
                // Mostrar cualquier otro tipo de error
                string errorMessage = "Error: " + ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += " Detalle: " + ex.InnerException.Message;
                }
                // Aquí puedes mostrar el mensaje de error en una etiqueta o algún otro control en tu página
                lblMensaje.Text = errorMessage; // Asumiendo que tienes un Label llamado lblErrorMessage
                lblMensaje.Visible = true;
            }
        }
        protected void AgregarListarPrecio(object sender, EventArgs e)
        {
            PanelAgrearPrecio.Visible = true;
            PanelActualizarPrecio.Visible= false;
        }



        protected void GridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Obtiene la fila que se está editando
            GridViewRow selectedRow = GridViewPrecio.Rows[e.NewEditIndex];
            // Asigna los valores de las celdas de la fila a diferentes controles en la página
            TextID.Text = HttpUtility.HtmlDecode(selectedRow.Cells[0].Text);
            txtDescrip.Text = HttpUtility.HtmlDecode(selectedRow.Cells[1].Text);
            ddlestado.Text = HttpUtility.HtmlDecode(selectedRow.Cells[2].Text);

            // Muestra el formulario de edición y oculta otros controles de la interfaz de usuario
            PanelActualizarPrecio.Visible = true;
            actualizar.Visible = true;
            PanelAgrearPrecio.Visible = false;
        }
        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(TextID.Text);
            string Descripcion = txtDescrip.Text;
            string Activo = ddlestado.Text;

            // Llama al procedimiento almacenado para actualizar el servicio
            ActualizarServicio(ID, Descripcion, Activo);

            // Vuelve a cargar los servicios para actualizar la GridView
            LlenarGridView();

            // Limpia los controles
            Session.Remove("SelectedRowID");

        }
        private void ActualizarServicio(int ID, string Descripcion, string Activo)
        {
            // Configura la conexión a la base de datos
            string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            try
            {
                using (SqlConnection sqlConectar = new SqlConnection(conectar))
                {
                    // Configura y ejecuta el comando para llamar al procedimiento almacenado
                    SqlCommand cmd = new SqlCommand("SP_ActualizarPrecioCliente", sqlConectar);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@DESCRIPCION", Descripcion);
                    cmd.Parameters.AddWithValue("@ACTIVO", Activo);

                    sqlConectar.Open();
                    cmd.ExecuteNonQuery();
                }

                PanelActualizarPrecio.Visible = false;
                LlenarGridView();
            }
            catch (SqlException ex)
            {
                // Mostrar el mensaje de error de la base de datos
                string errorMessage = "Error en la operación de la base de datos: " + ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += " Detalle: " + ex.InnerException.Message;
                }
                // Aquí puedes mostrar el mensaje de error en una etiqueta o algún otro control en tu página
                lblMensaje.Text = errorMessage; // Asumiendo que tienes un Label llamado lblErrorMessage
                lblMensaje.Visible = true;
            }
            catch (Exception ex)
            {
                // Mostrar cualquier otro tipo de error
                string errorMessage = "Error: " + ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += " Detalle: " + ex.InnerException.Message;
                }
                // Aquí puedes mostrar el mensaje de error en una etiqueta o algún otro control en tu página
                lblMensaje.Text = errorMessage; // Asumiendo que tienes un Label llamado lblErrorMessage
                lblMensaje.Visible = true;
            }
        }



        //protected void GridViewServicios_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    int rowIndex2 = Convert.ToInt32(e.CommandArgument);

        //    if (e.CommandName == "Detalle")
        //    {
        //        DesactivarActivarServicios(rowIndex2, true);
        //    }
        //}

        //private void DesactivarActivarServicios(int rowIndex, bool activar)
        //{
        //    if (rowIndex >= 0 && rowIndex < GridViewPrecio.Rows.Count)
        //    {
        //        int ID = Convert.ToInt32(GridViewPrecio.DataKeys[rowIndex]["ID_LISTAPRECIO"]);
        //        if (activar)
        //        {
        //            string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        //            try
        //            {
        //                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        //                {
        //                    using (SqlCommand sqlCommand = new SqlCommand("", sqlConnection))
        //                    {
        //                        sqlCommand.CommandType = CommandType.StoredProcedure;
        //                        sqlCommand.Parameters.AddWithValue("", ID);
        //                        sqlConnection.Open();
        //                        sqlCommand.ExecuteNonQuery();
        //                    }
        //                }
        //            }
        //            catch (SqlException sqlEx)
        //            {
        //                Console.WriteLine("Error de SQL al Activar: " + sqlEx.Message);
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine("Error al Activar : " + ex.Message);
        //            }
        //        }
        //        LlenarGridView();
        //    }
        //}



    }
}