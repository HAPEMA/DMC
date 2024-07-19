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
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int idCliente;
                if (int.TryParse(Request.QueryString["ID_CLIENTE"], out idCliente))
                {
                    // Almacenar el ID en un control oculto
                    HiddenFieldIDCliente.Text = idCliente.ToString();
                    // Llamar al método para llenar el GridView
                    LlenarGridView(idCliente);
                }
                else
                {
                    // Manejar el caso en que el ID_CLIENTE no esté presente o no sea válido
                    Response.Write("ID_CLIENTE no válido o no presente.");
                }


            }
        }
        private void LlenarGridView(int idCliente)
        {
            //string textoBusqueda = txtTextoBusqueda.Text.Trim(); // Obtén el valor del filtro de texto

            string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection sqlConectar = new SqlConnection(conectar))
            {
                try
                {
                    sqlConectar.Open();
                    DataTable GV = new DataTable();

                    // Define las columnas del DataTable
                    GV.Columns.AddRange(new DataColumn[]{
                new DataColumn("ID_TIENDA", typeof(string)),
                new DataColumn("NOMBRE_SUCURSAL", typeof(string)),
                new DataColumn("DIRECCION", typeof(string)),
                new DataColumn("COMUNA", typeof(string)),
                new DataColumn("REGION", typeof(string)),
                new DataColumn("ENCARGADO_LOCAL", typeof(string)),
                new DataColumn("TELEFONO_ENCARGADO", typeof(string)),
                new DataColumn("EMAIL_ENCARGADO", typeof(string)),
                new DataColumn("CASA_MATRIZ", typeof(string))
            });

                    // Utiliza el procedimiento almacenado adecuado
                    using (SqlCommand cmd = new SqlCommand("SP_ListarSucursal", sqlConectar))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.AddWithValue("@TXTBuscardor", textoBusqueda); // Nuevo parámetro para el filtro de texto
                        cmd.Parameters.AddWithValue("@ID", idCliente); // Nuevo parámetro para el ID del cliente

                        // Ejecuta el comando y llena el DataTable
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    GV.Rows.Add(
                                        dr["ID_TIENDA"].ToString(),
                                        dr["NOMBRE_SUCURSAL"].ToString(),
                                        dr["DIRECCION"].ToString(),
                                        dr["COMUNA"].ToString(),
                                        dr["REGION"].ToString(),
                                        dr["ENCARGADO_LOCAL"].ToString(),
                                        dr["TELEFONO_ENCARGADO"].ToString(),
                                        dr["EMAIL_ENCARGADO"].ToString(),
                                        dr["CASA_MATRIZ"].ToString()
                                    );
                                }
                            }
                        }
                    }

                    // Asigna el DataTable al GridView
                    GridViewClientes.DataSource = GV;
                    GridViewClientes.DataBind();
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

        protected void btnMostrarPanelAgregar(object sender, EventArgs e)
        {
            PanelCliente.Visible = true;
            Panelbottoagregar.Visible = true;
            PanelTituloEditar.Visible = false;
            LimpiarFormulario();
        }


        //protected void btnEnviar_Click(object sender, EventArgs e)
        //{
        //    int ID = Convert.ToInt32(IDCliente.Text);
        //    string Rut = txtrut1.Text;
        //    string RazonSocial = txtrazonsocial.Text;
        //    string ContactoCliente = txtcontactocliente.Text;
        //    string TelefonoCliente = txttelefonos.Text;
        //    string CorreoCliente = txtcorreo.Text;
        //    string Direccion = txtdireccion.Text;
        //    string Comuna = txtcomuna.Text;
        //    string Region = ddlregiones.SelectedValue;
        //    string Ejecutivo = ddlejecutivodmc.SelectedValue;
        //    string ListaPrecio = ddlListarPrecio.SelectedValue;

        //    // Llama al procedimiento almacenado para actualizar el servicio
        //    ActualizarServicio(ID, Rut, RazonSocial, Direccion, ContactoCliente, TelefonoCliente, CorreoCliente, Comuna, Region, Ejecutivo, ListaPrecio);

        //    // Vuelve a cargar los servicios para actualizar la GridView
        //    LlenarGridView();

        //    // Limpia los controles
        //    Session.Remove("SelectedRowID");

        //}

        //private void ActualizarServicio(int ID, string Rut, string RazonSocial, string Direccion, string ContactoCliente, string TelefonoCliente, string CorreoCliente, string Comuna, string Region, string Ejecutivo, string ListaPrecio)
        //{
        //    // Configura la conexión a la base de datos
        //    string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        //    using (SqlConnection sqlConectar = new SqlConnection(conectar))
        //    {
        //        // Configura y ejecuta el comando para llamar al procedimiento almacenado
        //        SqlCommand cmd = new SqlCommand("SP_ActualizarCliente", sqlConectar);
        //        cmd.CommandType = CommandType.StoredProcedure;


        //        cmd.Parameters.AddWithValue("", ID);
        //        cmd.Parameters.AddWithValue("", Rut);
        //        cmd.Parameters.AddWithValue("", RazonSocial);
        //        cmd.Parameters.AddWithValue("", Direccion);
        //        cmd.Parameters.AddWithValue("", ContactoCliente);
        //        cmd.Parameters.AddWithValue("", TelefonoCliente);
        //        cmd.Parameters.AddWithValue("", CorreoCliente);
        //        cmd.Parameters.AddWithValue("", Comuna);
        //        cmd.Parameters.AddWithValue("", Region);
        //        cmd.Parameters.AddWithValue("", Ejecutivo);
        //        cmd.Parameters.AddWithValue("", ListaPrecio);

        //        try
        //        {
        //            sqlConectar.Open();
        //            cmd.ExecuteNonQuery();
        //            // Mensaje de éxito
        //            Response.Write("<script>alert('Cliente Actualizado exitosamente');</script>");
        //            // Aquí puedes agregar código para actualizar la lista de clientes
        //            LimpiarFormulario();
        //            PanelCliente.Visible = false;
        //            LlenarGridView();
        //        }
        //        catch (SqlException ex)
        //        {
        //            // Manejo de errores
        //            Response.Write("<script>alert('Error al agregar cliente: " + ex.Message + "');</script>");
        //        }

        //    }
        //}


        protected void Guardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtdireccion.Text) ||
                string.IsNullOrWhiteSpace(txtcomuna.Text) ||
                string.IsNullOrWhiteSpace(ddlregiones.Text) ||
                string.IsNullOrWhiteSpace(TextEncargado.Text) ||
                string.IsNullOrWhiteSpace(TextTelefonoEncargado.Text) ||
                string.IsNullOrWhiteSpace(DropCasaMaestroEstado.Text) ||
                string.IsNullOrWhiteSpace(TextEmailEncargado.Text))
            {
                // Muestra un mensaje de error o realiza alguna acción
                Response.Write("Todos los campos son obligatorios.");
                return;
            }

            // Conexión a la BD
            string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            try
            {
                using (SqlConnection sqlConectar = new SqlConnection(conectar))
                using (SqlCommand sql = new SqlCommand("SP_AgregarSucursal", sqlConectar))
                {
                    sqlConectar.Open();
                    sql.CommandType = CommandType.StoredProcedure;

                    string Nombre = txtNombre.Text;
                    string Direccion = txtdireccion.Text;
                    string Comuna = txtcomuna.Text;
                    string Region = ddlregiones.SelectedValue;
                    string Encargado = TextEncargado.Text;
                    string Telefono = TextTelefonoEncargado.Text;
                    string Email = TextEmailEncargado.Text;
                    string CasaMaestro = DropCasaMaestroEstado.SelectedValue;
                    string IDCliente = HiddenFieldIDCliente.Text;

                    // Parámetros del procedimiento almacenado
                    sql.Parameters.Add("@NOMBRE", SqlDbType.VarChar, 50).Value = Nombre;
                    sql.Parameters.Add("@DIRECCION", SqlDbType.VarChar, 50).Value = Direccion;
                    sql.Parameters.Add("@COMUNA", SqlDbType.VarChar, 50).Value = Comuna;
                    sql.Parameters.Add("@REGION", SqlDbType.VarChar, 100).Value = Region;
                    sql.Parameters.Add("@ENCARGADOlOCAL", SqlDbType.VarChar, 50).Value = Encargado;
                    sql.Parameters.Add("@TELEFONOENCARGADO", SqlDbType.VarChar, 50).Value = Telefono;
                    sql.Parameters.Add("@EMAILENCARGADO", SqlDbType.VarChar, 100).Value = Email;
                    sql.Parameters.Add("@CasaMatrix", SqlDbType.VarChar, 50).Value = CasaMaestro;
                    sql.Parameters.Add("@IDCLIENTE", SqlDbType.VarChar, 50).Value = IDCliente;

                    // Ejecutar el procedimiento almacenado
                    sql.ExecuteNonQuery();
                }

                // Limpiar los campos después de guardar
                LimpiarFormulario();

                // Recarga los datos en el GridView después de la operación
                //PanelAgrearPrecio.Visible = false;
                int idCliente = int.Parse(HiddenFieldIDCliente.Text); // Obtén el ID del cliente del campo oculto
                LlenarGridView(idCliente);
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

        protected void GridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Obtiene la fila que se está editando
            GridViewRow selectedRow = GridViewClientes.Rows[e.NewEditIndex];

            // Asigna los valores de las celdas de la fila a diferentes controles en la página
            IDSucursal.Text = HttpUtility.HtmlDecode(selectedRow.Cells[0].Text);
            txtNombre.Text = HttpUtility.HtmlDecode(selectedRow.Cells[1].Text);
            txtdireccion.Text = HttpUtility.HtmlDecode(selectedRow.Cells[2].Text);
            txtcomuna.Text = HttpUtility.HtmlDecode(selectedRow.Cells[3].Text);
            ddlregiones.Text = HttpUtility.HtmlDecode(selectedRow.Cells[4].Text);
            TextEncargado.Text = HttpUtility.HtmlDecode(selectedRow.Cells[5].Text);
            TextTelefonoEncargado.Text = HttpUtility.HtmlDecode(selectedRow.Cells[6].Text);
            TextEmailEncargado.Text = HttpUtility.HtmlDecode(selectedRow.Cells[7].Text);
            DropCasaMaestroEstado.Text = HttpUtility.HtmlDecode(selectedRow.Cells[8].Text);

            // Muestra el formulario de edición y oculta otros controles de la interfaz de usuario
            PanelCliente.Visible = true;
            PanelTituloNormal.Visible = false;
            PanelTituloEditar.Visible = true;
            Panelbottoagregar.Visible = false;
            PanelbottoActualizar.Visible = true;
            // Llama a la función con el índice de la fila


            int idCliente = int.Parse(HiddenFieldIDCliente.Text); // Obtén el ID del cliente del campo oculto
            LlenarGridView(idCliente);
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(IDSucursal.Text);
            string Nombre = txtNombre.Text;
            string Direccion = txtdireccion.Text;
            string Comuna = txtcomuna.Text;
            string Region = ddlregiones.SelectedValue;
            string EncargadoLocar = TextEncargado.Text;
            string Telefono = TextTelefonoEncargado.Text;
            string Email = TextEmailEncargado.Text;
            string CasaMatrix = DropCasaMaestroEstado.SelectedValue;

            // Llama al procedimiento almacenado para actualizar el servicio
            ActualizarServicio(ID, Nombre, Direccion, Comuna, Region, EncargadoLocar, Telefono, Email, CasaMatrix);

            // Vuelve a cargar los servicios para actualizar la GridView
            int idCliente = int.Parse(HiddenFieldIDCliente.Text); // Obtén el ID del cliente del campo oculto
            LlenarGridView(idCliente);
            // Limpia los controles
            Session.Remove("SelectedRowID");

        }

        private void ActualizarServicio(int ID, string Nombre, string Direccion, string Comuna, string Region, string EncargadoLocar, string Telefono, string Email, string CasaMatrix)
        {
            // Configura la conexión a la base de datos
            string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection sqlConectar = new SqlConnection(conectar))
            {
                // Configura y ejecuta el comando para llamar al procedimiento almacenado
                SqlCommand cmd = new SqlCommand("SP_ActualizarSucursal", sqlConectar);
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@NOMBRE", Nombre);
                cmd.Parameters.AddWithValue("@DIRECCION", Direccion);
                cmd.Parameters.AddWithValue("@COMUNA", Comuna);
                cmd.Parameters.AddWithValue("@REGION", Region);
                cmd.Parameters.AddWithValue("@ENCARGADO_LOCAL", EncargadoLocar);
                cmd.Parameters.AddWithValue("@TELEFONOENCARGADO", Telefono);
                cmd.Parameters.AddWithValue("@EMAILENCARGADO", Email);
                cmd.Parameters.AddWithValue("@CASAMATRIX", CasaMatrix);

                try
                {
                    sqlConectar.Open();
                    cmd.ExecuteNonQuery();
                    // Mensaje de éxito
                    Response.Write("<script>alert('Actualizado exitosamente');</script>");
                    // Aquí puedes agregar código para actualizar la lista de clientes
                    LimpiarFormulario();
                    PanelCliente.Visible = false;


                    int idCliente = int.Parse(HiddenFieldIDCliente.Text); // Obtén el ID del cliente del campo oculto
                    LlenarGridView(idCliente);
                }
                catch (SqlException ex)
                {
                    // Manejo de errores
                    Response.Write("<script>alert('Error al Actualizar: " + ex.Message + "');</script>");
                }
            }
        }

        private void LimpiarFormulario()
        {
            //Limpiar controles del formulario luego de que se hayan guardado en la BD
            IDSucursal.Text = "";
            txtNombre.Text = "";
            txtdireccion.Text = "";
            txtcomuna.Text = "";
            TextEncargado.Text = "";
            TextTelefonoEncargado.Text = "";
            TextEmailEncargado.Text = "";
            ddlregiones.SelectedIndex = 0;
        }

    }
}