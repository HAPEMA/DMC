using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace DMC.DMC.Documentos
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        private string FOLIO_DOC;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LlenarGridView();
                ObtenerRazonSocialCliente();
                ObtenerRutCliente();
                ObtenerDireccionCliente();
                ObtenerVendedorCliente();
                ObtenerFolioCliente();
                ObtenerDescripcionCliente();
                ObtenerSubTotalCliente();
                ObtenerIVACliente();
                ObtenerTotalCliente();
                LlenarMes();
                LlenarAños();

                // Obtener el mes y año actual
                int mesActual = DateTime.Now.Month;
                int añoActual = DateTime.Now.Year;

                // Seleccionar automáticamente el mes actual
                System.Web.UI.WebControls.ListItem mesSeleccionado = DropMes.Items.FindByValue(mesActual.ToString());
                if (mesSeleccionado != null)
                {
                    mesSeleccionado.Selected = true;
                }

                // Seleccionar automáticamente el año actual
                System.Web.UI.WebControls.ListItem añoSeleccionado = DropAño.Items.FindByValue(añoActual.ToString());
                if (añoSeleccionado != null)
                {
                    añoSeleccionado.Selected = true;
                }

            }

        }

        private void LlenarGridView()
        {
            string textoBusqueda = txtTextoBusqueda.Text.Trim(); // Obtén el valor del filtro de texto
            string ObtenerAño = DropAño.SelectedValue;// Obtén el valor del filtro de texto
            string ObtenerMes = DropMes.SelectedValue; // Obtén el valor del filtro de texto

            string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection sqlConectar = new SqlConnection(conectar))
            {
                try
                {
                    sqlConectar.Open();
                    DataTable GV = new DataTable();

                    // Define las columnas del DataTable
                    GV.Columns.AddRange(new DataColumn[]{
                        new DataColumn("ID_DOCADMINISTRATIVO", typeof(string)),
                        new DataColumn("FOLIO_DOC", typeof(string)),
                        new DataColumn("FECHA_DOCUMENTO", typeof(string)),
                        new DataColumn("RZN_SOCIAL", typeof(string)),
                        new DataColumn("PROYECTO", typeof(string)),
                        new DataColumn("TOTAL_DOCUMENTO", typeof(string)),
                        new DataColumn("ID_ESTADOCUMENTO", typeof(string)),
                        new DataColumn("NOMBRE", typeof(string))
                    });

                    // Utiliza el procedimiento almacenado adecuado
                    using (SqlCommand cmd = new SqlCommand("SP_ListarDocumentos", sqlConectar))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TextoBusqueda", textoBusqueda); 
                        cmd.Parameters.AddWithValue("@Año", ObtenerAño); 
                        cmd.Parameters.AddWithValue("@Mes", ObtenerMes); 

                        // Ejecuta el comando y llena el DataTable
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    GV.Rows.Add(
                                        dr["ID_DOCADMINISTRATIVO"].ToString(),
                                        dr["FOLIO_DOC"].ToString(),
                                        dr["FECHA_DOCUMENTO"].ToString(),
                                        dr["RZN_SOCIAL"].ToString(),
                                        dr["PROYECTO"].ToString(),
                                        dr["TOTAL_DOCUMENTO"].ToString(),
                                        dr["ID_ESTADOCUMENTO"].ToString(),
                                        dr["NOMBRE"].ToString()
                                    );
                                }
                            }
                        }
                    }

                    // Asigna el DataTable al GridView
                    GridViewCotizacion.DataSource = GV;
                    GridViewCotizacion.DataBind();
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

        protected void GridViewServicios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GridViewCotizacion.PageIndex = e.NewPageIndex;
                LlenarGridView();
            }
            catch (Exception ex)
            {
                // Manejo de errores en la paginación
                Response.Write("Error: " + ex.Message);
                // Opcional: Loguear el error en un archivo de log o sistema de registro
            }
        }

        protected void ButtonMostrar_Click(object sender, EventArgs e)
        {
            LlenarGridView();
            ModalPanel.Visible = false;

        }

        private void llamaModal()
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
                        new DataColumn("LINEA", typeof(string)),
                        new DataColumn("CODIGO_MAESTRO", typeof(string)),
                        new DataColumn("DESCRIPCION_PRODUCTO", typeof(string)),
                        new DataColumn("CANTIDAD", typeof(string)),
                        new DataColumn("VALOR_UNITARIO", typeof(string)),
                        new DataColumn("DESCUENTOS", typeof(string)),
                        new DataColumn("TOTAL_LINEA", typeof(string))
                    });

                    // Utiliza el procedimiento almacenado adecuado
                    using (SqlCommand cmd = new SqlCommand("SP_ListarModalDocumento", sqlConectar))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", FOLIO_DOC); // Nuevo parámetro para el filtro de texto

                        // Ejecuta el comando y llena el DataTable
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    GV.Rows.Add(
                                        dr["LINEA"].ToString(),
                                        dr["CODIGO_MAESTRO"].ToString(),
                                        dr["DESCRIPCION_PRODUCTO"].ToString(),
                                        dr["CANTIDAD"].ToString(),
                                        dr["VALOR_UNITARIO"].ToString(),
                                        dr["DESCUENTOS"].ToString(),
                                        dr["TOTAL_LINEA"].ToString()
                                    );
                                }
                            }
                        }
                    }

                    // Asigna el DataTable al GridView
                    GridViewModal.DataSource = GV;
                    GridViewModal.DataBind();
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

        protected void GridViewCotizacion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex2 = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "VerRow")
            {
                // Obtener el índice de la fila seleccionada
                int index = Convert.ToInt32(e.CommandArgument);

                // Obtener la fila seleccionada
                GridViewRow selectedRow = GridViewCotizacion.Rows[index];

                // Obtener el valor de FOLIO_DOC de la fila seleccionada
                string folioDoc = selectedRow.Cells[1].Text; // Asegúrate de que el índice corresponde a la columna de FOLIO_DOC

                // Asigna el valor de FOLIO_DOC a una variable de instancia para que pueda ser usada en el método ObtenerDireccionCliente
                FOLIO_DOC = folioDoc;

                // Llamar al método ObtenerDireccionCliente
                ObtenerDireccionCliente();
                ObtenerRutCliente();
                ObtenerRazonSocialCliente();
                ObtenerVendedorCliente();
                ObtenerFolioCliente();
                llamaModal();
                ObtenerDescripcionCliente();
                ObtenerSubTotalCliente();
                ObtenerIVACliente();
                ObtenerTotalCliente();
            }
            if (e.CommandName == "Desactivar")
            {
                DesactivarActivarServicios(rowIndex2, true);
            }
        }

        private void DesactivarActivarServicios(int rowIndex, bool Desactivar)
        {

            if (rowIndex >= 0 && rowIndex < GridViewCotizacion.Rows.Count)
            {
                int ID = Convert.ToInt32(GridViewCotizacion.DataKeys[rowIndex]["ID_DOCADMINISTRATIVO"]);

                if (Desactivar)
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
                    try
                    {
                        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                        {
                            using (SqlCommand sqlCommand = new SqlCommand("SP_EliminarDocumentacion", sqlConnection))
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
                        Console.WriteLine("Error de SQL al Eliminar: " + sqlEx.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al ELiminar : " + ex.Message);
                    }
                }
                LlenarGridView();
            }
        }

        protected void ObtenerRazonSocialCliente()
        {
            if (int.TryParse(FOLIO_DOC, out int IDFolio))
            {
                string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
                using (SqlConnection sqlConectar = new SqlConnection(conectar))
                {
                    sqlConectar.Open();
                    using (SqlCommand sql = new SqlCommand("select RZNSOC_RECEPTOR from DOC_ADMINISTRATIVO where FOLIO_DOC =" + IDFolio, sqlConectar))
                    {
                        sql.CommandType = CommandType.Text;
                        using (SqlDataReader reader = sql.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                RazonSocialCliente.Text = reader["RZNSOC_RECEPTOR"].ToString();
                            }
                        }
                    }
                }
            }
        }

        protected void ObtenerRutCliente()
        {
            if (int.TryParse(FOLIO_DOC, out int IDFolio))
            {
                string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
                using (SqlConnection sqlConectar = new SqlConnection(conectar))
                {
                    sqlConectar.Open();
                    using (SqlCommand sql = new SqlCommand("select RUT_RECEPTOR from DOC_ADMINISTRATIVO where FOLIO_DOC =" + IDFolio, sqlConectar))
                    {
                        sql.CommandType = CommandType.Text;
                        using (SqlDataReader reader = sql.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                RutCliente.Text = reader["RUT_RECEPTOR"].ToString();
                            }
                        }
                    }
                }
            }
        }

        protected void ObtenerDireccionCliente()
        {
            if (int.TryParse(FOLIO_DOC, out int IDFolio))
            {
                string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
                using (SqlConnection sqlConectar = new SqlConnection(conectar))
                {
                    sqlConectar.Open();
                    using (SqlCommand sql = new SqlCommand("select DIRECCION_RECEPTOR from DOC_ADMINISTRATIVO where FOLIO_DOC =" + IDFolio, sqlConectar))
                    {
                        sql.CommandType = CommandType.Text;
                        using (SqlDataReader reader = sql.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                DireccionCliente.Text = reader["DIRECCION_RECEPTOR"].ToString();
                            }
                        }
                    }
                }
            }
        }

        protected void ObtenerVendedorCliente()
        {
            if (int.TryParse(FOLIO_DOC, out int IDFolio))
            {
                string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
                using (SqlConnection sqlConectar = new SqlConnection(conectar))
                {
                    sqlConectar.Open();
                    using (SqlCommand sql = new SqlCommand("select Concat(Us.NOMBRE, ' ' ,Us.APPAT) as NombreCompleto from DOC_ADMINISTRATIVO as Doc left join TBUSUARIO as Us on US.id_usuario = Doc.ID_USUARIO where FOLIO_DOC =" + IDFolio, sqlConectar))
                    {
                        sql.CommandType = CommandType.Text;
                        using (SqlDataReader reader = sql.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                lblEjecutivo.Text = reader["NombreCompleto"].ToString();
                            }
                        }
                    }
                }
            }
        }

        protected void ObtenerFolioCliente()
        {
            if (int.TryParse(FOLIO_DOC, out int IDFolio))
            {
                string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
                using (SqlConnection sqlConectar = new SqlConnection(conectar))
                {
                    sqlConectar.Open();
                    using (SqlCommand sql = new SqlCommand("select FOLIO_DOC from DOC_ADMINISTRATIVO where FOLIO_DOC =" + IDFolio, sqlConectar))
                    {
                        sql.CommandType = CommandType.Text;
                        using (SqlDataReader reader = sql.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                lblFolio.Text = reader["FOLIO_DOC"].ToString();
                            }
                        }
                    }
                }
            }
        }

        protected void ObtenerDescripcionCliente()
        {
            if (int.TryParse(FOLIO_DOC, out int IDFolio))
            {
                string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
                using (SqlConnection sqlConectar = new SqlConnection(conectar))
                {
                    sqlConectar.Open();
                    using (SqlCommand sql = new SqlCommand("select OBSERVACIONES from DOC_ADMINISTRATIVO where FOLIO_DOC =" + IDFolio, sqlConectar))
                    {
                        sql.CommandType = CommandType.Text;
                        using (SqlDataReader reader = sql.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                ObserCliente.Text = reader["OBSERVACIONES"].ToString();
                            }
                        }
                    }
                }
            }
        }

        protected void ObtenerSubTotalCliente()
        {
            if (int.TryParse(FOLIO_DOC, out int IDFolio))
            {
                string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
                using (SqlConnection sqlConectar = new SqlConnection(conectar))
                {
                    sqlConectar.Open();
                    using (SqlCommand sql = new SqlCommand("select Format(TOTAL_DOCUMENTO_NETO, '$#,###') as TOTAL_DOCUMENTO_NETO from DOC_ADMINISTRATIVO where FOLIO_DOC =" + IDFolio, sqlConectar))
                    {
                        sql.CommandType = CommandType.Text;
                        using (SqlDataReader reader = sql.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                ValorSubTotal.Text = reader["TOTAL_DOCUMENTO_NETO"].ToString();
                            }
                        }
                    }
                }
            }
        }

        protected void ObtenerIVACliente()
        {
            if (int.TryParse(FOLIO_DOC, out int IDFolio))
            {
                string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
                using (SqlConnection sqlConectar = new SqlConnection(conectar))
                {
                    sqlConectar.Open();
                    using (SqlCommand sql = new SqlCommand("select Format(IVA, '$#,###') as IVA from DOC_ADMINISTRATIVO where FOLIO_DOC =" + IDFolio, sqlConectar))
                    {
                        sql.CommandType = CommandType.Text;
                        using (SqlDataReader reader = sql.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                ValorIVA.Text = reader["IVA"].ToString();
                            }
                        }
                    }
                }
            }
        }

        protected void ObtenerTotalCliente()
        {
            if (int.TryParse(FOLIO_DOC, out int IDFolio))
            {
                string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
                using (SqlConnection sqlConectar = new SqlConnection(conectar))
                {
                    sqlConectar.Open();
                    using (SqlCommand sql = new SqlCommand("select Format(TOTAL_DOCUMENTO, '$#,###') as TOTAL_DOCUMENTO from DOC_ADMINISTRATIVO where FOLIO_DOC =" + IDFolio, sqlConectar))
                    {
                        sql.CommandType = CommandType.Text;
                        using (SqlDataReader reader = sql.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                ValorTotal.Text = reader["TOTAL_DOCUMENTO"].ToString();
                            }
                        }
                    }
                }
            }
        }

        protected void MostrarModal(object sender, EventArgs e)
        {
            ModalPanel.Visible = true;
        }

        protected void LlenarMes()
        {
            string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection sqlConectar = new SqlConnection(conectar))
            {
                sqlConectar.Open();
                using (SqlCommand sql = new SqlCommand("SELECT DISTINCT MONTH(FECHA_INSERT) AS Mes, CASE MONTH(FECHA_INSERT) WHEN 1 THEN 'Enero' WHEN 2 THEN 'Febrero'WHEN 3 THEN 'Marzo'  WHEN 4 THEN 'Abril'  WHEN 5 THEN 'Mayo'  WHEN 6 THEN 'Junio'    WHEN 7 THEN 'Julio'   WHEN 8 THEN 'Agosto'   WHEN 9 THEN 'Septiembre'    WHEN 10 THEN 'Octubre'     WHEN 11 THEN 'Noviembre'   WHEN 12 THEN 'Diciembre' END AS NombreMes  FROM DOC_ADMINISTRATIVO  ORDER BY Mes ASC;", sqlConectar))
                {
                    sql.CommandType = CommandType.Text;
                    DropMes.DataSource = sql.ExecuteReader();
                    DropMes.DataTextField = "NombreMes";
                    DropMes.DataValueField = "Mes";
                    DropMes.DataBind();
                    DropMes.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecciona un Mes", ""));
                }
            }
        }

        protected void LlenarAños()
        {
            string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection sqlConectar = new SqlConnection(conectar))
            {
                sqlConectar.Open();
                using (SqlCommand sql = new SqlCommand("SELECT DISTINCT YEAR(FECHA_INSERT) AS Año FROM DOC_ADMINISTRATIVO ORDER BY Año DESC;", sqlConectar))
                {
                    sql.CommandType = CommandType.Text;
                    DropAño.DataSource = sql.ExecuteReader();
                    DropAño.DataTextField = "Año";
                    DropAño.DataValueField = "Año";
                    DropAño.DataBind();
                    DropAño.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecciona un Año", ""));
                }
            }
        }
    }
}