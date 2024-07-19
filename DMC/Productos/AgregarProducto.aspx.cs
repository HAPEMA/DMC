using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DMC.DMC.Productos
{
    public partial class WebForm6 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarFamilia();
                CargarSubFamilia();
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

        protected void Guardar_Click(object sender, EventArgs e)
        {

            string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection sqlConectar = new SqlConnection(conectar))
            using (SqlCommand sql = new SqlCommand("SP_AgregarMaestroProducto", sqlConectar))
            {
                try
                {
                    sqlConectar.Open();
                    sql.CommandType = CommandType.StoredProcedure;

                    // Asignar los valores de los controles al procedimiento almacenado
                    sql.Parameters.AddWithValue("@codigo_maestro", txtcodigo.Text);
                    sql.Parameters.AddWithValue("@descripcion", txtdescripcion.Text);
                    sql.Parameters.AddWithValue("@unidades", int.Parse(txtunidades.Text));
                    sql.Parameters.AddWithValue("@formato", txtformato.Text);
                    sql.Parameters.AddWithValue("@id_familia", int.Parse(ddlfamilia.SelectedValue));
                    sql.Parameters.AddWithValue("@codigo_familia", ddlfamilia.SelectedItem.Text); // Suponiendo que el texto es el código

                    // Verificar si se ha seleccionado una subfamilia
                    if (ddlsubfamilia.SelectedValue == "0")
                    {
                        sql.Parameters.AddWithValue("@id_subfamilia", DBNull.Value);
                        sql.Parameters.AddWithValue("@codigo_subfamilia", DBNull.Value);
                    }
                    else
                    {
                        sql.Parameters.AddWithValue("@id_subfamilia", int.Parse(ddlsubfamilia.SelectedValue));
                        sql.Parameters.AddWithValue("@codigo_subfamilia", ddlsubfamilia.SelectedItem.Text); // Suponiendo que el texto es el código
                    }

                    sql.Parameters.AddWithValue("@Estado_Item", ddlestado.SelectedValue);
                    sql.Parameters.AddWithValue("@activo", lblHiddenS.Text); // O cualquier lógica que determine el valor de 'activo'

                    // Convertir la imagen a byte[]
                    byte[] imagenBytes;
                    if (fileUploadImagen.HasFile)
                    {
                        using (BinaryReader br = new BinaryReader(fileUploadImagen.PostedFile.InputStream))
                        {
                            imagenBytes = br.ReadBytes(fileUploadImagen.PostedFile.ContentLength);
                        }
                    }
                    else
                    {
                        imagenBytes = null;
                    }
                    sql.Parameters.Add("@imagen", SqlDbType.VarBinary).Value = (object)imagenBytes ?? DBNull.Value;

                    // Ejecutar el procedimiento almacenado
                    sql.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Manejo de errores
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    if (sqlConectar.State == ConnectionState.Open)
                    {
                        sqlConectar.Close();
                    }
                }
            }

            LimpiarFormulario();
        }

        protected void Limpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            txtcodigo.Text = string.Empty;
            txtdescripcion.Text = string.Empty;
            txtunidades.Text = string.Empty;
            txtformato.Text = string.Empty;
            ddlfamilia.SelectedIndex = 0;
            ddlsubfamilia.SelectedIndex = 0;
            ddlestado.SelectedIndex = 0;
            lblHiddenS.Text = string.Empty;
        }
    }
}