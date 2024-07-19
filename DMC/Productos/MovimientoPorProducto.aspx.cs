using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace DMC.DMC.Productos
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                LlenarGridView();
                llamarBodega();
                llamarProducto();
            }
        }

        protected void ButtonMostrar_Click(object sender, EventArgs e)
        {
            LlenarGridView();
        }

        private void LlenarGridView()
        {
            string Bodega = (ddlBodega.SelectedValue != "0") ? ddlBodega.SelectedValue : null;
            string Producto = (ddlProducto.SelectedValue != "0") ? ddlProducto.SelectedValue : null;

            string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection sqlConectar = new SqlConnection(conectar))
            {
                try
                {
                    sqlConectar.Open();
                    DataTable GV = new DataTable();

                    // Define las columnas del DataTable
                    GV.Columns.AddRange(new DataColumn[]{
                new DataColumn("numero_documento", typeof(string)),
                new DataColumn("fecha_documento", typeof(string)),
                new DataColumn("entradas", typeof(string)),
                new DataColumn("salidas", typeof(string)),
                new DataColumn("observacion", typeof(string)),
                new DataColumn("codigo_maestro", typeof(string)),
                new DataColumn("Nombre_Bodega", typeof(string)),
                new DataColumn("nombre_usuario", typeof(string))
            });

                    // Utiliza el procedimiento almacenado adecuado
                    using (SqlCommand cmd = new SqlCommand("SP_ListarMovimientoPorProducto", sqlConectar))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Bodega", Bodega); 
                        cmd.Parameters.AddWithValue("@Codigo", Producto); 

                        // Ejecuta el comando y llena el DataTable
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    GV.Rows.Add(
                                        dr["numero_documento"].ToString(),
                                        dr["fecha_documento"].ToString(),
                                        dr["entradas"].ToString(),
                                        dr["salidas"].ToString(),
                                        dr["observacion"].ToString(),
                                        dr["codigo_maestro"].ToString(),
                                        dr["Nombre_Bodega"].ToString(),
                                        dr["nombre_usuario"].ToString()
                                    );
                                }
                            }
                        }
                    }

                    // Asigna el DataTable al GridView
                    GridViewMovimiento.DataSource = GV;
                    GridViewMovimiento.DataBind();
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

        private void llamarBodega()
        {
            string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection sqlConectar = new SqlConnection(conectar))
            {
                sqlConectar.Open();
                using (SqlCommand sql = new SqlCommand("select id_bodega,Nombre_Bodega from bodega where activo = 1", sqlConectar))
                {
                    sql.CommandType = CommandType.Text;

                    ddlBodega.DataSource = sql.ExecuteReader();
                    ddlBodega.DataTextField = "Nombre_Bodega";
                    ddlBodega.DataValueField = "id_bodega";
                    ddlBodega.DataBind();

                    //// Insertar "Todos" al inicio
                    //System.Web.UI.WebControls.ListItem itemTodos = new System.Web.UI.WebControls.ListItem("Todos", "Todos");
                    //ddlBodega.Items.Insert(0, itemTodos);

                    // Insertar "Selecciona Tienda" después de "Todos"
                    ddlBodega.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecciona Bodega", ""));
                }
            }
        }
        
        private void llamarProducto()
        {
            string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection sqlConectar = new SqlConnection(conectar))
            {
                sqlConectar.Open();
                using (SqlCommand sql = new SqlCommand("Select id_maestro, descripcion from maestro where activo = 1", sqlConectar))
                {
                    sql.CommandType = CommandType.Text;

                    ddlProducto.DataSource = sql.ExecuteReader();
                    ddlProducto.DataTextField = "descripcion";
                    ddlProducto.DataValueField = "id_maestro";
                    ddlProducto.DataBind();

                    //// Insertar "Todos" al inicio
                    //System.Web.UI.WebControls.ListItem itemTodos = new System.Web.UI.WebControls.ListItem("Todos", "Todos");
                    //ddlProducto.Items.Insert(0, itemTodos);

                    // Insertar "Selecciona Tienda" después de "Todos"
                    ddlProducto.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecciona un Producto", ""));
                }
            }
        }





    }
}