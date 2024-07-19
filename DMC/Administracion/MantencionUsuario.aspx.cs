using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DMC.DMC.Administracion
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LlenarGridView();
                PanelAgrearFa.Visible = false;
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
                new DataColumn("ID_Usuario", typeof(string)),
                new DataColumn("NOMBRE", typeof(string)),
                new DataColumn("APPAT", typeof(string)),
                new DataColumn("CLAVE", typeof(string)),
                new DataColumn("CORREO", typeof(string)),
                new DataColumn("Activo", typeof(string))
                    });

                    // Utiliza el procedimiento almacenado adecuado
                    using (SqlCommand cmd = new SqlCommand("[SP_ListarUsuarios]", sqlConectar))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TextBuscador", textoBusqueda); // Nuevo parámetro para el filtro de texto
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    GV.Rows.Add(
                                        dr["ID_Usuario"].ToString(),
                                        dr["NOMBRE"].ToString(),
                                        dr["APPAT"].ToString(),
                                        dr["CLAVE"].ToString(),
                                        dr["CORREO"].ToString(),
                                        dr["Activo"].ToString()
                                    );
                                }
                            }
                        }
                    }

                    // Asigna el DataTable al GridView
                    GridViewUsuario.DataSource = GV;
                    GridViewUsuario.DataBind();
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
        protected void MostrarAgregar(object o, EventArgs e)
        {
            PanelAgrearFa.Visible = true;

        }

        protected void ButtonMostrar_Click(object sender, EventArgs e)
        {
            LlenarGridView();

        }
        protected void GridViewServicios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex2 = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Activar_Desactivar")
            {
                DesactivarActivarServicios(rowIndex2, true);
            }
        }
        private void DesactivarActivarServicios(int rowIndex, bool activar)
        {

            if (rowIndex >= 0 && rowIndex < GridViewUsuario.Rows.Count)
            {
                int ID = Convert.ToInt32(GridViewUsuario.DataKeys[rowIndex]["ID_USUARIO"]);

                if (activar)
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
                    try
                    {
                        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                        {
                            using (SqlCommand sqlCommand = new SqlCommand("SP_ActualizarEstadoUsuario", sqlConnection))
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
                PanelAgrearFa.Visible = false;
            }
        }

        protected void Guardar_Click(object sender, EventArgs e)
        {
            if (
                string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(TextPaterno.Text)||
                string.IsNullOrWhiteSpace(TextClave.Text)||
                string.IsNullOrWhiteSpace(TextCorreo.Text)
            )
            {
                return;
            }
            //conexion a la BD
            string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection sqlConectar = new SqlConnection(conectar))
            using (SqlCommand sql = new SqlCommand("SP_AgregarUsuario", sqlConectar))
            {
                sqlConectar.Open();
                sql.CommandType = CommandType.StoredProcedure;

                string Nombre = txtNombre.Text;
                string ApellidoPaterno = TextPaterno.Text;
                string ApellidoMaterno = TextMaterno.Text;
                string Clave = TextClave.Text;
                string Correo = TextCorreo.Text;

                // Parámetros del procedimiento almacenado
                sql.Parameters.Add("@Nombre", SqlDbType.VarChar, 250).Value = Nombre;
                sql.Parameters.Add("@APPT", SqlDbType.VarChar, 250).Value = ApellidoPaterno;
                sql.Parameters.Add("@APMT", SqlDbType.VarChar, 250).Value = ApellidoMaterno;
                sql.Parameters.Add("@Clave", SqlDbType.VarChar, 250).Value = Clave;
                sql.Parameters.Add("@Correo", SqlDbType.VarChar, 250).Value = Correo;

                // Ejecutar el procedimiento almacenado
                sql.ExecuteNonQuery();
            }

            // Limpiar los campos después de guardar
            txtNombre.Text = "";
            TextPaterno.Text = "";
            TextMaterno.Text = "";
            TextClave.Text = "";
            TextCorreo.Text = "";

            // Recarga los datos en el GridView después de la operación
            LlenarGridView();

        }
        protected void GridViewServicios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GridViewUsuario.PageIndex = e.NewPageIndex;
                LlenarGridView();
            }
            catch (Exception ex)
            {
                // Manejo de errores en la paginación
                Response.Write("Error: " + ex.Message);
                // Opcional: Loguear el error en un archivo de log o sistema de registro
            }
        }

    }
}