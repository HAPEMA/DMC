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
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LlenarGridView();
                PrecioCliente();
                LlenarEjecutivo();
            }
        }


        private void LlenarEjecutivo()
        {
            string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection sqlConectar = new SqlConnection(conectar))
            {
                sqlConectar.Open();
                using (SqlCommand sql = new SqlCommand("Select ID_USUARIO, (NOMBRE + ' ' + APPAT) as Ejecutivo from TBUSUARIO where ACTIVO = 1", sqlConectar))
                {
                    sql.CommandType = CommandType.Text;
                    using (SqlDataReader reader = sql.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            ddlejecutivodmc.DataSource = reader;
                            ddlejecutivodmc.DataTextField = "Ejecutivo";
                            ddlejecutivodmc.DataValueField = "ID_USUARIO";
                            ddlejecutivodmc.DataBind();
                        }
                    }
                }
            }
            ddlejecutivodmc.Items.Insert(0, new ListItem("Seleccionar Ejecutivo", "0"));
        }

        protected void GridViewServicios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex2 = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "sucursal")
            {
                VerSucursalCliente(rowIndex2, true);
                PanelCliente.Visible = false;
            }
        }

        private void VerSucursalCliente(int rowIndex, bool Sucursal)
        {
            if (rowIndex >= 0 && rowIndex < GridViewClientes.Rows.Count)
            {
                int ID = Convert.ToInt32(GridViewClientes.DataKeys[rowIndex]["ID_CLIENTE"]);
                if (Sucursal)
                {
                    // Aquí puedes redirigir a la página que desees, pasando el ID en la URL
                    Response.Redirect($"~/DMC/Comercio/Sucursar.aspx?ID_CLIENTE={ID}");
                }
            }
        }


        protected void GridViewClientes_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Obtiene la fila que se está editando
            GridViewRow selectedRow = GridViewClientes.Rows[e.NewEditIndex];

            // Asigna los valores de las celdas de la fila a diferentes controles en la página
            IDCliente.Text = HttpUtility.HtmlDecode(selectedRow.Cells[0].Text);
            txtrut1.Text = HttpUtility.HtmlDecode(selectedRow.Cells[1].Text);
            txtrazonsocial.Text = HttpUtility.HtmlDecode(selectedRow.Cells[2].Text);
            txtdireccion.Text = HttpUtility.HtmlDecode(selectedRow.Cells[3].Text);

            // Muestra el formulario de edición y oculta otros controles de la interfaz de usuario
            PanelCliente.Visible = true;
            PanelTituloNormal.Visible = false;
            PanelTituloEditar.Visible = true;
            Panelbottoagregar.Visible = false;
            PanelbottoActualizar.Visible = true;
            // Llama a la función con el índice de la fila
            ObtenerNombreContactoCliente();
            ObtenerTelefonoCliente();
            ObtenerEMAilCliente();
            ObtenerComuncaCliente();
        }

        //protected void OculatarActualizar(object sender, EventArgs e)
        //{
        //    PanelActualizar.Visible = false;
        //}

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(IDCliente.Text);
            string Rut = txtrut1.Text;
            string RazonSocial = txtrazonsocial.Text;
            string ContactoCliente = txtcontactocliente.Text;
            string TelefonoCliente = txttelefonos.Text;
            string CorreoCliente = txtcorreo.Text;
            string Direccion = txtdireccion.Text;
            string Comuna = txtcomuna.Text;
            string Region = ddlregiones.SelectedValue;
            string Ejecutivo = ddlejecutivodmc.SelectedValue;
            string ListaPrecio = ddlListarPrecio.SelectedValue;

            // Llama al procedimiento almacenado para actualizar el servicio
            ActualizarServicio(ID, Rut, RazonSocial, Direccion, ContactoCliente, TelefonoCliente, CorreoCliente, Comuna, Region, Ejecutivo, ListaPrecio);

            // Vuelve a cargar los servicios para actualizar la GridView
            LlenarGridView();

            // Limpia los controles
            Session.Remove("SelectedRowID");

        }

        private void ActualizarServicio(int ID, string Rut, string RazonSocial, string Direccion, string ContactoCliente, string TelefonoCliente, string CorreoCliente, string Comuna, string Region, string Ejecutivo, string ListaPrecio)
        {
            // Configura la conexión a la base de datos
            string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection sqlConectar = new SqlConnection(conectar))
            {
                // Configura y ejecuta el comando para llamar al procedimiento almacenado
                SqlCommand cmd = new SqlCommand("SP_ActualizarCliente", sqlConectar);
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@Rut", Rut);
                cmd.Parameters.AddWithValue("@RazonSocial", RazonSocial);
                cmd.Parameters.AddWithValue("@Direccion", Direccion);
                cmd.Parameters.AddWithValue("@ContactoCliente", ContactoCliente);
                cmd.Parameters.AddWithValue("@Telefono", TelefonoCliente);
                cmd.Parameters.AddWithValue("@Email", CorreoCliente);
                cmd.Parameters.AddWithValue("@Comuna", Comuna);
                cmd.Parameters.AddWithValue("@Region", Region);
                cmd.Parameters.AddWithValue("@Ejecutivo", Ejecutivo);
                cmd.Parameters.AddWithValue("@ListarPrecio", ListaPrecio);

                try
                {
                    sqlConectar.Open();
                    cmd.ExecuteNonQuery();
                    // Mensaje de éxito
                    Response.Write("<script>alert('Cliente Actualizado exitosamente');</script>");
                    // Aquí puedes agregar código para actualizar la lista de clientes
                    LimpiarFormulario();
                    PanelCliente.Visible = false;
                    LlenarGridView();
                }
                catch (SqlException ex)
                {
                    // Manejo de errores
                    Response.Write("<script>alert('Error al agregar cliente: " + ex.Message + "');</script>");
                }

            }
        }

        protected void ButtonMostrar_Click(object sender, EventArgs e)
        {
            LlenarGridView();
            PanelCliente.Visible=false;
        }

        protected void btnAgregarcliente(object sender, EventArgs e)
        {
            PanelCliente.Visible=true;
            Panelbottoagregar.Visible=true;
            LimpiarFormulario();
        }

        public class ClienteDAL
        {
            private string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

            public DataTable ObtenerClientes(string textoBusqueda)
            {
                DataTable clientes = new DataTable();

                using (SqlConnection sqlConectar = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_ListarClientes", sqlConectar))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TextoBusqueda", textoBusqueda);

                        sqlConectar.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                clientes.Load(dr);
                            }
                        }
                    }
                }

                return clientes;
            }
        }
        private void LlenarGridView()
        {
            try
            {
                string textoBusqueda = txtTextoBusqueda.Text.Trim();
                ClienteDAL clienteDAL = new ClienteDAL();
                DataTable clientes = clienteDAL.ObtenerClientes(textoBusqueda);

                GridViewClientes.DataSource = clientes;
                GridViewClientes.DataBind();
            }
            catch (Exception ex)
            {
                // Manejo de errores
                MostrarError("Error al llenar el GridView: " + ex.Message);
            }
        }
        private void MostrarError(string mensaje)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = "mensaje-error";
        }


        private void PrecioCliente()
        {
            string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection sqlConectar = new SqlConnection(conectar))
            {
                sqlConectar.Open();
                using (SqlCommand sql = new SqlCommand("Select ID_LISTAPRECIO,DESCRIPCION from LISTAPRECIO_CLIENTE where ACTIVO = 1 ", sqlConectar))
                {
                    sql.CommandType = CommandType.Text;
                    using (SqlDataReader reader = sql.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            ddlListarPrecio.DataSource = reader;
                            ddlListarPrecio.DataTextField = "DESCRIPCION";
                            ddlListarPrecio.DataValueField = "ID_LISTAPRECIO";
                            ddlListarPrecio.DataBind();
                        }
                    }
                }
            }
            ddlListarPrecio.Items.Insert(0, new ListItem("Seleccionar Precio", "0"));
        }
        protected void GridViewClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewClientes.PageIndex = e.NewPageIndex;
            LlenarGridView();
            PanelCliente.Visible = false;
        }

        protected void btnagregar_Click(object sender, EventArgs e)
        {
            string rutCliente = txtrut1.Text;
            string razonSocial = txtrazonsocial.Text;
            string telefonoComercial = txttelefonos.Text;
            string email = txtcorreo.Text;
            string direccion = txtdireccion.Text;
            string comuna = txtcomuna.Text;
            string region = ddlregiones.SelectedValue;
            int idVendedor = int.Parse(ddlejecutivodmc.SelectedValue);
            int ID_ListarPrecio = int.Parse(ddlListarPrecio.SelectedValue);
            string encargadoLocal = txtcontactocliente.Text;
            string nombreSucursal = "CASA MATRIZ";

            string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_AgregarCliente", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RUT_CLIENTE", rutCliente);
                    command.Parameters.AddWithValue("@RZN_SOCIAL", razonSocial);
                    command.Parameters.AddWithValue("@TELEFONO_COMERCIAL", telefonoComercial);
                    command.Parameters.AddWithValue("@EMAIL", email);
                    command.Parameters.AddWithValue("@DIRECCION", direccion);
                    command.Parameters.AddWithValue("@COMUNA", comuna);
                    command.Parameters.AddWithValue("@REGION", region);
                    command.Parameters.AddWithValue("@ID_VENDEDOR", idVendedor);
                    command.Parameters.AddWithValue("@ID_LISTAPRECIO", ID_ListarPrecio);
                    command.Parameters.AddWithValue("@ENCARGADO_LOCAL", encargadoLocal);
                    command.Parameters.AddWithValue("@NOMBRE_SUCURSAL", nombreSucursal);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        // Mensaje de éxito
                        Response.Write("<script>alert('Cliente agregado exitosamente');</script>");
                        // Aquí puedes agregar código para actualizar la lista de clientes
                        LimpiarFormulario();
                    }
                    catch (SqlException ex)
                    {
                        // Manejo de errores
                        Response.Write("<script>alert('Error al agregar cliente: " + ex.Message + "');</script>");
                    }
                }
            }
        }

        //protected void Guardar_Click(object sender, EventArgs e)
        //{
        //    if (
        //        string.IsNullOrWhiteSpace(AgreCodigoFamilia.Text) ||
        //        string.IsNullOrWhiteSpace(AgregarDescripFamilia.Text)
        //    )
        //    {
        //        return;
        //    }
        //    //conexion a la BD
        //    string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        //    using (SqlConnection sqlConectar = new SqlConnection(conectar))
        //    using (SqlCommand sql = new SqlCommand("SP_AgregarFamilia", sqlConectar))
        //    {
        //        sqlConectar.Open();
        //        sql.CommandType = CommandType.StoredProcedure;

        //        string CodigoFa = AgreCodigoFamilia.Text;
        //        string DescripFa = AgregarDescripFamilia.Text;

        //        // Parámetros del procedimiento almacenado
        //        sql.Parameters.Add("@CodigoFa", SqlDbType.VarChar, 250).Value = CodigoFa;
        //        sql.Parameters.Add("@DescripFa", SqlDbType.VarChar, 250).Value = DescripFa;

        //        // Ejecutar el procedimiento almacenado
        //        sql.ExecuteNonQuery();
        //    }

        //    // Limpiar los campos después de guardar
        //    AgreCodigoFamilia.Text = "";
        //    AgregarDescripFamilia.Text = "";

        //    // Recarga los datos en el GridView después de la operación
        //    PanelAgrearFa.Visible = false;
        //    LlenarGridView();

        //}

        //private void llamarContactocliente(int rowIndex)
        //{
        //    try
        //    {
        //        int ID = Convert.ToInt32(GridViewClientes.DataKeys[rowIndex]["id_SubFamilia"]);
        //        string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

        //        using (SqlConnection sqlConectar = new SqlConnection(conectar))
        //        {
        //            sqlConectar.Open();
        //            using (SqlCommand sql = new SqlCommand("SELECT ENCARGADO_LOCAL FROM SUCURSAL WHERE ID_CLIENTE = @ID", sqlConectar))
        //            {
        //                sql.CommandType = CommandType.Text;
        //                sql.Parameters.AddWithValue("@ID", ID);

        //                using (SqlDataReader reader = sql.ExecuteReader())
        //                {
        //                    if (reader.HasRows)
        //                    {
        //                        ddlejecutivodmc.DataSource = reader;
        //                        ddlejecutivodmc.DataTextField = "ENCARGADO_LOCAL"; // Aquí debe ir el campo que quieres mostrar
        //                        ddlejecutivodmc.DataValueField = "ENCARGADO_LOCAL"; // Aquí debe ir el campo del valor
        //                        ddlejecutivodmc.DataBind();
        //                    }
        //                }
        //            }
        //        }

        //        ddlejecutivodmc.Items.Insert(0, new ListItem("Seleccionar Ejecutivo", "0"));
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejo de excepciones adecuado (puedes loguear el error o mostrar un mensaje)
        //        Console.WriteLine("Ocurrió un error: " + ex.Message);
        //    }
        //}

        protected void ObtenerNombreContactoCliente()
        {
            if (int.TryParse(IDCliente.Text, out int ID))
            {
                string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
                using (SqlConnection sqlConectar = new SqlConnection(conectar))
                {
                    sqlConectar.Open();
                    using (SqlCommand sql = new SqlCommand("SELECT ENCARGADO_LOCAL FROM SUCURSAL WHERE ID_CLIENTE =" + ID, sqlConectar))
                    {
                        sql.CommandType = CommandType.Text;
                        using (SqlDataReader reader = sql.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtcontactocliente.Text = reader["ENCARGADO_LOCAL"].ToString();
                            }
                        }
                    }
                }
            }
        }
        protected void ObtenerTelefonoCliente()
        {
            if (int.TryParse(IDCliente.Text, out int ID))
            {
                string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
                using (SqlConnection sqlConectar = new SqlConnection(conectar))
                {
                    sqlConectar.Open();
                    using (SqlCommand sql = new SqlCommand("SELECT TELEFONO_ENCARGADO FROM SUCURSAL WHERE ID_CLIENTE =" + ID, sqlConectar))
                    {
                        sql.CommandType = CommandType.Text;
                        using (SqlDataReader reader = sql.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txttelefonos.Text = reader["TELEFONO_ENCARGADO"].ToString();
                            }
                        }
                    }
                }
            }
        }
        protected void ObtenerEMAilCliente()
        {
            if (int.TryParse(IDCliente.Text, out int ID))
            {
                string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
                using (SqlConnection sqlConectar = new SqlConnection(conectar))
                {
                    sqlConectar.Open();
                    using (SqlCommand sql = new SqlCommand("SELECT EMAIL_ENCARGADO FROM SUCURSAL WHERE ID_CLIENTE =" + ID, sqlConectar))
                    {
                        sql.CommandType = CommandType.Text;
                        using (SqlDataReader reader = sql.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtcorreo.Text = reader["EMAIL_ENCARGADO"].ToString();
                            }
                        }
                    }
                }
            }
        }
        protected void ObtenerComuncaCliente()
        {
            if (int.TryParse(IDCliente.Text, out int ID))
            {
                string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
                using (SqlConnection sqlConectar = new SqlConnection(conectar))
                {
                    sqlConectar.Open();
                    using (SqlCommand sql = new SqlCommand("SELECT COMUNA FROM SUCURSAL WHERE ID_CLIENTE =" + ID, sqlConectar))
                    {
                        sql.CommandType = CommandType.Text;
                        using (SqlDataReader reader = sql.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtcomuna.Text = reader["COMUNA"].ToString();
                            }
                        }
                    }
                }
            }
        }
        
        
        //protected void Obtener()
        //{
        //    if (int.TryParse(IDCliente.Text, out int ID))
        //    {
        //        string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        //        using (SqlConnection sqlConectar = new SqlConnection(conectar))
        //        {
        //            sqlConectar.Open();
        //            using (SqlCommand sql = new SqlCommand("SELECT COMUNA FROM SUCURSAL WHERE ID_CLIENTE =" + ID, sqlConectar))
        //            {
        //                sql.CommandType = CommandType.Text;
        //                using (SqlDataReader reader = sql.ExecuteReader())
        //                {
        //                    if (reader.Read())
        //                    {
        //                        txtcomuna.Text = reader["COMUNA"].ToString();
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}


        //private void LlenarEjecutivo()
        //{
        //    string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        //    using (SqlConnection sqlConectar = new SqlConnection(conectar))
        //    {
        //        sqlConectar.Open();
        //        using (SqlCommand sql = new SqlCommand("Select ID_USUARIO, (NOMBRE + ' ' + APPAT) as Ejecutivo from TBUSUARIO where ACTIVO = 1", sqlConectar))
        //        {
        //            sql.CommandType = CommandType.Text;
        //            using (SqlDataReader reader = sql.ExecuteReader())
        //            {
        //                if (reader.HasRows)
        //                {
        //                    ddlejecutivodmc.DataSource = reader;
        //                    ddlejecutivodmc.DataTextField = "Ejecutivo";
        //                    ddlejecutivodmc.DataValueField = "ID_USUARIO";
        //                    ddlejecutivodmc.DataBind();
        //                }
        //            }
        //        }
        //    }
        //    ddlejecutivodmc.Items.Insert(0, new ListItem("Seleccionar Ejecutivo", "0"));
        //}



        //private void LlenarEjecutivo()
        //{
        //    string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        //    using (SqlConnection sqlConectar = new SqlConnection(conectar))
        //    {
        //        sqlConectar.Open();
        //        using (SqlCommand sql = new SqlCommand("Select ID_USUARIO, (NOMBRE + ' ' + APPAT) as Ejecutivo from TBUSUARIO where ACTIVO = 1", sqlConectar))
        //        {
        //            sql.CommandType = CommandType.Text;
        //            using (SqlDataReader reader = sql.ExecuteReader())
        //            {
        //                if (reader.HasRows)
        //                {
        //                    ddlejecutivodmc.DataSource = reader;
        //                    ddlejecutivodmc.DataTextField = "Ejecutivo";
        //                    ddlejecutivodmc.DataValueField = "ID_USUARIO";
        //                    ddlejecutivodmc.DataBind();
        //                }
        //            }
        //        }
        //    }
        //    ddlejecutivodmc.Items.Insert(0, new ListItem("Seleccionar Ejecutivo", "0"));
        //}




        //private void LlenarEjecutivo()
        //{
        //    string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        //    using (SqlConnection sqlConectar = new SqlConnection(conectar))
        //    {
        //        sqlConectar.Open();
        //        using (SqlCommand sql = new SqlCommand("Select ID_USUARIO, (NOMBRE + ' ' + APPAT) as Ejecutivo from TBUSUARIO where ACTIVO = 1", sqlConectar))
        //        {
        //            sql.CommandType = CommandType.Text;
        //            using (SqlDataReader reader = sql.ExecuteReader())
        //            {
        //                if (reader.HasRows)
        //                {
        //                    ddlejecutivodmc.DataSource = reader;
        //                    ddlejecutivodmc.DataTextField = "Ejecutivo";
        //                    ddlejecutivodmc.DataValueField = "ID_USUARIO";
        //                    ddlejecutivodmc.DataBind();
        //                }
        //            }
        //        }
        //    }
        //    ddlejecutivodmc.Items.Insert(0, new ListItem("Seleccionar Ejecutivo", "0"));
        //}

        private void LimpiarFormulario()
        {
            //Limpiar controles del formulario luego de que se hayan guardado en la BD
            txtrut1.Text = "";
            txtrazonsocial.Text = "";
            txtcontactocliente.Text = "";
            txttelefonos.Text = "";
            txtcorreo.Text = "";
            txtdireccion.Text = "";
            txtcomuna.Text = "";
            ddlregiones.SelectedIndex = 0;
            ddlejecutivodmc.SelectedIndex = 0;
        }

    }
}