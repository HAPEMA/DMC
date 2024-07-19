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
    public partial class WebForm7 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DropBodega();
                DropProducto();
                Idfecha.Text = DateTime.Now.ToString("yyyy-MM-dd");

                if (Session["ID_USUARIO"] != null)
                {
                    string IDUsuario = Session["ID_USUARIO"].ToString();
                }
                string IDUser = Session["NOMBRE"] as string;
                IDUsu.Text = IDUser;

            }
        }

        protected void DropBodega()
        {
            string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection sqlConectar = new SqlConnection(conectar))
            {
                sqlConectar.Open();
                using (SqlCommand sql = new SqlCommand("select * from Bodega where activo = 1", sqlConectar))
                {
                    sql.CommandType = CommandType.Text;
                    Idbodega.DataSource = sql.ExecuteReader();
                    Idbodega.DataTextField = "Nombre_Bodega";
                    Idbodega.DataValueField = "Id_bodega";
                    Idbodega.DataBind();
                    Idbodega.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecciona una bodega", "0"));
                }
            }
        }

        protected void DropProducto()
        {
            string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection sqlConectar = new SqlConnection(conectar))
            {
                sqlConectar.Open();
                using (SqlCommand sql = new SqlCommand("SELECT descripcion, Id_maestro FROM maestro where activo = 1", sqlConectar))
                {
                    sql.CommandType = CommandType.Text;
                    IDProducto.DataSource = sql.ExecuteReader();
                    IDProducto.DataTextField = "descripcion";
                    IDProducto.DataValueField = "Id_maestro";
                    IDProducto.DataBind();
                    IDProducto.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecciona", "0"));
                }
            }
        }

        protected void botonEnviar_Click(object sender, EventArgs e)
        {
            // Validar que todos los campos estén llenos
            if (
                string.IsNullOrWhiteSpace(Idfecha.Text) ||
                string.IsNullOrWhiteSpace(DropDocumento.Text) ||
                string.IsNullOrWhiteSpace(Idbodega.Text) ||
                string.IsNullOrWhiteSpace(IDProducto.Text) ||
                string.IsNullOrWhiteSpace(IDSalida.Text) ||
                string.IsNullOrWhiteSpace(IDEntrada.Text) ||
                string.IsNullOrWhiteSpace(IDObservacion.Text)
              )
            {
                // Mostrar mensaje de validación en la parte superior del formulario
                divMensajeValidacion.InnerText = "Te hace falta un campo clave para poder guardar.";
                return;
            }
            divMensajeValidacion.InnerText = "";

            DateTime fecha = DateTime.Parse(Idfecha.Text);
            int IDBodega = int.Parse(Idbodega.SelectedItem.Value);
            int IDMaestro = int.Parse(IDProducto.SelectedValue);
            string IDDocumento = DropDocumento.Text;
            string NDocumento = IDNumDocumento.Text;
            int IDEntradaN;
            if (!int.TryParse(IDEntrada.Text, out IDEntradaN))
            {
                IDEntradaN = 0;
            }
            int IDSalidaN;
            if (!int.TryParse(IDSalida.Text, out IDSalidaN))
            {
                IDSalidaN = 0; 
            }
            string IDDecripcion = IDObservacion.Text;
            string Usuario = IDUsu.Text;

            AgregarDatosServicioCompleto(fecha, IDBodega, IDMaestro, IDDocumento, NDocumento, IDEntradaN, IDSalidaN, IDDecripcion);

            LimpiarCampos();
        }

        protected void AgregarDatosServicioCompleto(DateTime fecha, int IDBodega, int IDMaestro, string IDDocumento, string NDocumento, int IDEntradaN, int IDSalidaN, string IDDecripcion)
        {
            string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conectar))
            {
                using (SqlCommand cmd = new SqlCommand("[SP_ModificarInventario]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Parámetros del procedimiento almacenado
                    cmd.Parameters.AddWithValue("@Fecha", fecha);
                    cmd.Parameters.AddWithValue("@IDBodega", IDBodega);
                    cmd.Parameters.AddWithValue("@IDMaestro", IDMaestro);
                    cmd.Parameters.AddWithValue("@IDocumento", IDDocumento);
                    cmd.Parameters.AddWithValue("@NumDocumento", NDocumento);
                    cmd.Parameters.AddWithValue("@Salida", IDEntradaN);
                    cmd.Parameters.AddWithValue("@Entrada", IDSalidaN);
                    cmd.Parameters.AddWithValue("@Observacion", IDDecripcion);
                    //cmd.Parameters.AddWithValue("@IDUsuario", Usuario);
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException sqlEx)
                    {
                        for (int i = 0; i < sqlEx.Errors.Count; i++)
                        {
                            Console.WriteLine($"Index #{i}: " +
                                $"Error Number: {sqlEx.Errors[i].Number}, " +
                                $"Message: {sqlEx.Errors[i].Message}");
                        }
                    }
                }
            }
        }

        private void LimpiarCampos()
        {
            Idbodega.SelectedIndex = 0;
            DropDocumento.SelectedIndex = 0;
            IDProducto.SelectedIndex = 0;
            DropDocumento.Text = "";
            IDEntrada.Text = "";
            txtBusqueda.Text = "";
            IDNumDocumento.Text = "";
            IDSalida.Text = "";
            IDObservacion.Text = "";
            Idfecha.Text = DateTime.Now.Date.ToShortDateString();
        }

    }
}