using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace DMC.DMC.Login
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login1_LoggedIn(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            string Usuario = txtUsuario.Text.Trim();
            string Contrasena = txtContrasena.Text.Trim();

            // Verificar si se ingresaron ambos campos
            if (string.IsNullOrEmpty(Usuario) || string.IsNullOrEmpty(Contrasena))
            {
                lblMensaje.Text = "Por favor, complete ambos campos.";
                return; // Detener el proceso si falta información
            }

            bool UsuarioValido = ValidateCredentials(Usuario, Contrasena);
            if (!UsuarioValido)
            {
                // Muestra un mensaje de error si las credenciales son incorrectas
                lblMensaje.Text = "Nombre de usuario o contraseña incorrectos";
            }
        }

        private bool ValidateCredentials(string Usuario, string Contrasena)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_VerificarUsuario", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Usuario", Usuario);
                    cmd.Parameters.AddWithValue("@Contrasena", Contrasena);

                    connection.Open();

                    // Definir variables para almacenar la información del usuario
                    string Nombre = "";
                    string APPAT = "";
                    string PERMISOS = "";
                    string CORREO = "";
                    //int IDUsuario = 0;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Leer los valores del resultado del procedimiento almacenado
                            Nombre = reader.GetString(reader.GetOrdinal("NOMBRE"));
                            APPAT = reader.GetString(reader.GetOrdinal("APPAT"));
                            PERMISOS = reader.GetString(reader.GetOrdinal("PERMISOS"));
                            CORREO = reader.GetString(reader.GetOrdinal("CORREO"));
                            //IDUsuario = reader.GetString(reader.GetOrdinal("ID_USUARIO"));
                        }
                    }

                    connection.Close();

                    // Verificar si se recuperó información del usuario
                    if (!string.IsNullOrEmpty(Nombre))
                    {
                        // Almacenar la información del usuario en la sesión
                        Session["NOMBRE"] = Nombre;
                        Session["APPAT"] = APPAT;
                        Session["PERMISOS"] = PERMISOS;
                        Session["CORREO"] = CORREO;
                        //Session["ID_USUARIO"] = IDUsuario;

                        // Redirigir al usuario según su rol
                        RedirectAuthenticatedUser(Nombre, APPAT, PERMISOS, CORREO);

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }


        private void RedirectAuthenticatedUser(string Nombre, string APPAT, string PERMISOS, string CORREO)
        {
            Response.Redirect("~/DMC/Menus/MenuDespuesLogin.aspx");
        }

    }
}