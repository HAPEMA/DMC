using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DMC.DMC.Productos
{
    public partial class WebForm1 : System.Web.UI.Page
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
                        new DataColumn("codigo_familia", typeof(string)),
                        new DataColumn("codigo_maestro", typeof(string)),
                        new DataColumn("descripcion", typeof(string)),
                        new DataColumn("Saldo", typeof(string)),
                        new DataColumn("Estado_Item", typeof(string))
                    });

                    // Utiliza el procedimiento almacenado adecuado
                    using (SqlCommand cmd = new SqlCommand("SP_ListarInventario", sqlConectar))
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
                                        dr["codigo_familia"].ToString(),
                                        dr["codigo_maestro"].ToString(),
                                        dr["descripcion"].ToString(),
                                        dr["Saldo"].ToString(),
                                        dr["Estado_Item"].ToString()
                                    );
                                }
                            }
                        }
                    }

                    // Asigna el DataTable al GridView
                    GridViewInventario.DataSource = GV;
                    GridViewInventario.DataBind();
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


        public override void VerifyRenderingInServerForm(Control control)
        {
            // Confirms that an HtmlForm control is rendered for the specified ASP.NET server control at run time.
            // No code required here.
        }

        protected void BtnDescargar_Click_Excel(object sender, EventArgs e)
        {
            // Deshabilitar la paginación temporalmente
            GridViewInventario.AllowPaging = false;
            LlenarGridView(); // Volver a llenar el GridView para cargar todos los datos

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Inventario.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";

            // Configurar el estilo
            Response.Write("<style> .header { background-color: #6a1b9a; color: #fff; font-weight: bold; } .even-row { background-color: #f2f2f2; } .odd-row { background-color: #ffffff; } </style>");

            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new HtmlTextWriter(sw);

            // Agregar estilos a las celdas de encabezado
            GridViewInventario.HeaderRow.Attributes["class"] = "header";

            // Agregar estilos a las celdas de datos
            for (int i = 0; i < GridViewInventario.Rows.Count; i++)
            {
                GridViewRow row = GridViewInventario.Rows[i];

                // Aplicar estilos alternados a las filas
                if (i % 2 == 0)
                {
                    row.Attributes["class"] = "even-row";
                }
                else
                {
                    row.Attributes["class"] = "odd-row";
                }
            }

            // Renderizar el GridView
            GridViewInventario.RenderControl(hw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            // Volver a habilitar la paginación
            GridViewInventario.AllowPaging = true;
            LlenarGridView(); // Volver a llenar el GridView para cargar los datos con paginación
        }

        [Obsolete]

        protected void ButtonMostrar_Click(object sender, EventArgs e)
        {
            LlenarGridView();
        }

        protected void GridViewServicios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GridViewInventario.PageIndex = e.NewPageIndex;
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
