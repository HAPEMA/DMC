using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Linq;

namespace DMC.DMC.Documentos
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Inicializa la sesión y las filas si es necesario
                if (Session["ValoresFilas"] == null)
                {
                    Session["ValoresFilas"] = new List<string[]>();
                }

                if (Session["FilaIDs"] == null)
                {
                    Session["FilaIDs"] = new List<int>();
                }

                // Genera las filas iniciales
                GenerarFilas();

                txtfecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
                DropClientes();
                DropTienda();
                ObtenerRut();
                ObtenerTelefonoComercial();
                ObtenerRegion();
                ObtenerEmail();
                ObtenerDireccion();
                ObtenerRazonSocial();
                ObtenerTelefonoSocial();
                //------------------  Tienda ----------------------//
                ObtenerDirecciontienda();
                ObtenerCasaTienda();
                ObtenerEmailTienda();
                ObtenerTelefonoTienda();
                ObtenerRegionTienda();
                ObtenerComunaTienda();
                ObtenerEncargadoTienda();


                List<string[]> valoresFilas = Session["ValoresFilas"] as List<string[]> ?? new List<string[]>();
                List<int> filaIDs = Session["FilaIDs"] as List<int> ?? new List<int>();

                if (valoresFilas.Count == 0)
                {
                    valoresFilas.Add(new string[8]); // Añadir una nueva fila vacía
                    filaIDs.Add(1); // Añadir un nuevo ID de fila, comenzando en 1
                }

                Session["ValoresFilas"] = valoresFilas;
                Session["FilaIDs"] = filaIDs;

                GenerarFilas();
            }
            else
            {
                // Asegurarse de regenerar las filas en cada postback para mantener el estado de los controles dinámicos
                GenerarFilas();
            }
        }
        protected void DropClientes()
        {
            string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection sqlConectar = new SqlConnection(conectar))
            {
                sqlConectar.Open();
                using (SqlCommand sql = new SqlCommand("select * from CLIENTE order by ltrim(RZN_SOCIAL) asc", sqlConectar))
                {
                    sql.CommandType = CommandType.Text;
                    ddlCliente.DataSource = sql.ExecuteReader();
                    ddlCliente.DataTextField = "RZN_SOCIAL";
                    ddlCliente.DataValueField = "ID_CLIENTE";
                    ddlCliente.DataBind();
                    ddlCliente.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecciona un Cliente", "0"));
                }
            }
        }
        protected void DropTienda()
        {
            // Verificar si el valor seleccionado es numérico
            if (int.TryParse(ddlCliente.SelectedValue, out int IdCliente))
            {
                string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
                using (SqlConnection sqlConectar = new SqlConnection(conectar))
                {
                    sqlConectar.Open();
                    using (SqlCommand sql = new SqlCommand("select NOMBRE_SUCURSAL,ID_TIENDA from SUCURSAL where ID_CLIENTE =" + IdCliente, sqlConectar))
                    {
                        sql.CommandType = CommandType.Text;
                        sql.Parameters.AddWithValue("@IdCliente", IdCliente);

                        // Obtener los nombres de las tiendas de la base de datos
                        ddlTienda.DataSource = sql.ExecuteReader();
                        ddlTienda.DataTextField = "NOMBRE_SUCURSAL";
                        ddlTienda.DataValueField = "ID_TIENDA";
                        ddlTienda.DataBind();


                    }
                }
            }
        }
        protected void ObtenerRut()
        {
            if (int.TryParse(ddlCliente.SelectedValue, out int IdCliente))
            {
                string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
                using (SqlConnection sqlConectar = new SqlConnection(conectar))
                {
                    sqlConectar.Open();
                    using (SqlCommand sql = new SqlCommand("SELECT RUT_CLIENTE FROM CLIENTE  where ID_CLIENTE =" + IdCliente, sqlConectar))
                    {
                        sql.CommandType = CommandType.Text;
                        using (SqlDataReader reader = sql.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IdRut.Text = reader["RUT_CLIENTE"].ToString();
                            }
                        }
                    }
                }
            }
        }
        protected void ObtenerRazonSocial()
        {
            if (int.TryParse(ddlCliente.SelectedValue, out int IdCliente))
            {
                string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
                using (SqlConnection sqlConectar = new SqlConnection(conectar))
                {
                    sqlConectar.Open();
                    using (SqlCommand sql = new SqlCommand("SELECT RZN_SOCIAL FROM CLIENTE  where ID_CLIENTE =" + IdCliente, sqlConectar))
                    {
                        sql.CommandType = CommandType.Text;
                        using (SqlDataReader reader = sql.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                idRazonSocial.Text = reader["RZN_SOCIAL"].ToString();
                            }
                        }
                    }
                }
            }
        }
        protected void ObtenerDireccion()
        {
            if (int.TryParse(ddlCliente.SelectedValue, out int IdCliente))
            {
                string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
                using (SqlConnection sqlConectar = new SqlConnection(conectar))
                {
                    sqlConectar.Open();
                    using (SqlCommand sql = new SqlCommand("SELECT DIRECCION FROM CLIENTE  where ID_CLIENTE =" + IdCliente, sqlConectar))
                    {
                        sql.CommandType = CommandType.Text;
                        using (SqlDataReader reader = sql.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                idDireccion.Text = reader["DIRECCION"].ToString();
                            }
                        }
                    }
                }
            }
        }
        protected void ObtenerEmail()
        {
            if (int.TryParse(ddlCliente.SelectedValue, out int IdCliente))
            {
                string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
                using (SqlConnection sqlConectar = new SqlConnection(conectar))
                {
                    sqlConectar.Open();
                    using (SqlCommand sql = new SqlCommand("SELECT EMAIL FROM CLIENTE  where ID_CLIENTE =" + IdCliente, sqlConectar))
                    {
                        sql.CommandType = CommandType.Text;
                        using (SqlDataReader reader = sql.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                idImail.Text = reader["EMAIL"].ToString();
                            }
                        }
                    }
                }
            }
        }
        protected void ObtenerRegion()
        {
            if (int.TryParse(ddlCliente.SelectedValue, out int IdCliente))
            {
                string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
                using (SqlConnection sqlConectar = new SqlConnection(conectar))
                {
                    sqlConectar.Open();
                    using (SqlCommand sql = new SqlCommand("SELECT REGION FROM CLIENTE  where ID_CLIENTE =" + IdCliente, sqlConectar))
                    {
                        sql.CommandType = CommandType.Text;
                        using (SqlDataReader reader = sql.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                idRegion.Text = reader["REGION"].ToString();
                            }
                        }
                    }
                }
            }
        }
        protected void ObtenerTelefonoComercial()
        {
            if (int.TryParse(ddlCliente.SelectedValue, out int IdCliente))
            {
                string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
                using (SqlConnection sqlConectar = new SqlConnection(conectar))
                {
                    sqlConectar.Open();
                    using (SqlCommand sql = new SqlCommand("SELECT TELEFONO_COMERCIAL FROM CLIENTE  where ID_CLIENTE =" + IdCliente, sqlConectar))
                    {
                        sql.CommandType = CommandType.Text;
                        using (SqlDataReader reader = sql.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                idTelefonoComercial.Text = reader["TELEFONO_COMERCIAL"].ToString();
                            }
                        }
                    }
                }
            }
        }
        protected void ObtenerTelefonoSocial()
        {
            if (int.TryParse(ddlCliente.SelectedValue, out int IdCliente))
            {
                string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
                using (SqlConnection sqlConectar = new SqlConnection(conectar))
                {
                    sqlConectar.Open();
                    using (SqlCommand sql = new SqlCommand("SELECT TELEFONO_MOVIL FROM CLIENTE  where ID_CLIENTE =" + IdCliente, sqlConectar))
                    {
                        sql.CommandType = CommandType.Text;
                        using (SqlDataReader reader = sql.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                idTelefonoSocial.Text = reader["TELEFONO_MOVIL"].ToString();
                            }
                        }
                    }
                }
            }
        }

        //**********************************************************************************          Sucursal            *******************************************************************************//

        protected void ObtenerDirecciontienda()
        {
            if (int.TryParse(ddlTienda.SelectedValue, out int IdTienda))
            {
                string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
                using (SqlConnection sqlConectar = new SqlConnection(conectar))
                {
                    sqlConectar.Open();
                    using (SqlCommand sql = new SqlCommand("select DIRECCION from SUCURSAL where ID_TIENDA =" + IdTienda, sqlConectar))
                    {
                        sql.CommandType = CommandType.Text;
                        using (SqlDataReader reader = sql.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                idDirecciontienda.Text = reader["DIRECCION"].ToString();
                            }
                        }
                    }
                }
            }
        }
        protected void ObtenerRegionTienda()
        {
            if (int.TryParse(ddlTienda.SelectedValue, out int IdTienda))
            {
                string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
                using (SqlConnection sqlConectar = new SqlConnection(conectar))
                {
                    sqlConectar.Open();
                    using (SqlCommand sql = new SqlCommand("select REGION from SUCURSAL where ID_TIENDA =" + IdTienda, sqlConectar))
                    {
                        sql.CommandType = CommandType.Text;
                        using (SqlDataReader reader = sql.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                idRegiontienda.Text = reader["REGION"].ToString();
                            }
                        }
                    }
                }
            }
        }
        protected void ObtenerComunaTienda()
        {
            if (int.TryParse(ddlTienda.SelectedValue, out int IdTienda))
            {
                string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
                using (SqlConnection sqlConectar = new SqlConnection(conectar))
                {
                    sqlConectar.Open();
                    using (SqlCommand sql = new SqlCommand("select COMUNA from SUCURSAL where ID_TIENDA =" + IdTienda, sqlConectar))
                    {
                        sql.CommandType = CommandType.Text;
                        using (SqlDataReader reader = sql.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                idComunatienda.Text = reader["COMUNA"].ToString();
                            }
                        }
                    }
                }
            }
        }
        protected void ObtenerEncargadoTienda()
        {
            if (int.TryParse(ddlTienda.SelectedValue, out int IdTienda))
            {
                string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
                using (SqlConnection sqlConectar = new SqlConnection(conectar))
                {
                    sqlConectar.Open();
                    using (SqlCommand sql = new SqlCommand("select ENCARGADO_LOCAL from SUCURSAL where ID_TIENDA =" + IdTienda, sqlConectar))
                    {
                        sql.CommandType = CommandType.Text;
                        using (SqlDataReader reader = sql.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                idEncargadoienda.Text = reader["ENCARGADO_LOCAL"].ToString();
                            }
                        }
                    }
                }
            }
        }
        protected void ObtenerTelefonoTienda()
        {
            if (int.TryParse(ddlTienda.SelectedValue, out int IdTienda))
            {
                string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
                using (SqlConnection sqlConectar = new SqlConnection(conectar))
                {
                    sqlConectar.Open();
                    using (SqlCommand sql = new SqlCommand("select TELEFONO_ENCARGADO from SUCURSAL where ID_TIENDA =" + IdTienda, sqlConectar))
                    {
                        sql.CommandType = CommandType.Text;
                        using (SqlDataReader reader = sql.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                idTelefonotienda.Text = reader["TELEFONO_ENCARGADO"].ToString();
                            }
                        }
                    }
                }
            }
        }
        protected void ObtenerEmailTienda()
        {
            if (int.TryParse(ddlTienda.SelectedValue, out int IdTienda))
            {
                string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
                using (SqlConnection sqlConectar = new SqlConnection(conectar))
                {
                    sqlConectar.Open();
                    using (SqlCommand sql = new SqlCommand("select EMAIL_ENCARGADO from SUCURSAL where ID_TIENDA =" + IdTienda, sqlConectar))
                    {
                        sql.CommandType = CommandType.Text;
                        using (SqlDataReader reader = sql.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                idEmailtienda.Text = reader["EMAIL_ENCARGADO"].ToString();
                            }
                        }
                    }
                }
            }
        }
        protected void ObtenerCasaTienda()
        {
            if (int.TryParse(ddlTienda.SelectedValue, out int IdTienda))
            {
                string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
                using (SqlConnection sqlConectar = new SqlConnection(conectar))
                {
                    sqlConectar.Open();
                    using (SqlCommand sql = new SqlCommand("select CASA_MATRIZ from SUCURSAL where ID_TIENDA =" + IdTienda, sqlConectar))
                    {
                        sql.CommandType = CommandType.Text;
                        using (SqlDataReader reader = sql.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                idCasaMatriztienda.Text = reader["CASA_MATRIZ"].ToString();
                            }
                        }
                    }
                }
            }
        }


        //----------------------------------------------  Producto ----------------------------------------------------//


        //protected void DropProducto()
        //{
        //    string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        //    using (SqlConnection sqlConectar = new SqlConnection(conectar))
        //    {
        //        sqlConectar.Open();
        //        using (SqlCommand sql = new SqlCommand("select codigo_maestro, id_maestro from maestro order by ltrim(codigo_maestro) asc", sqlConectar))
        //        {
        //            sql.CommandType = CommandType.Text;
        //            SqlDataAdapter da = new SqlDataAdapter(sql);
        //            DataTable dt = new DataTable();
        //            da.Fill(dt);
        //            IDProductopropio.DataSource = dt;
        //            IDProductopropio.DataTextField = "codigo_maestro";
        //            IDProductopropio.DataValueField = "id_maestro";
        //            IDProductopropio.DataBind();
        //            IDProductopropio.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecciona", "0"));
        //        }
        //    }
        //}

        //protected void DropDescripcion()
        //{
        //    int IdCliente = Convert.ToInt32(IDProductopropio.SelectedValue);

        //    string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        //    using (SqlConnection sqlConectar = new SqlConnection(conectar))
        //    {
        //        sqlConectar.Open();
        //        using (SqlCommand sql = new SqlCommand("SELECT DISTINCT descripcion, Id_maestro FROM maestro where Id_maestro =" + IdCliente, sqlConectar))
        //        {
        //            sql.CommandType = CommandType.Text;
        //            Iddproducto.DataSource = sql.ExecuteReader();
        //            Iddproducto.DataTextField = "descripcion";
        //            Iddproducto.DataValueField = "Id_maestro";
        //            Iddproducto.DataBind();
        //        }
        //    }
        //}

        //protected void ObtenerPrecioBase()
        //{
        //    int IdProducto = Convert.ToInt32(IDProductopropio.SelectedValue);
        //    {
        //        string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        //        using (SqlConnection sqlConectar = new SqlConnection(conectar))
        //        {
        //            sqlConectar.Open();
        //            using (SqlCommand sql = new SqlCommand("select top 1 PRECIO_VENTA from LISTA_PRECIOS where ID_MAESTRO =" + IdProducto, sqlConectar))
        //            {
        //                sql.CommandType = CommandType.Text;
        //                using (SqlDataReader reader = sql.ExecuteReader())
        //                {
        //                    if (reader.Read())
        //                    {
        //                        IDPrecioBase.Text = reader["PRECIO_VENTA"].ToString();
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        //protected void DdlCliente_SelectedIndexChangedIDProducto(object sender, EventArgs e)
        //{
        //    DropDescripcion();
        //    ObtenerPrecioBase();
        //}

        /////**************************************************************************************************************************************************************************************//


        //protected void DropProducto2()
        //{
        //    string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        //    using (SqlConnection sqlConectar = new SqlConnection(conectar))
        //    {
        //        sqlConectar.Open();
        //        using (SqlCommand sql = new SqlCommand("select codigo_maestro, id_maestro from maestro order by ltrim(codigo_maestro) asc", sqlConectar))
        //        {
        //            sql.CommandType = CommandType.Text;
        //            SqlDataAdapter da = new SqlDataAdapter(sql);
        //            DataTable dt = new DataTable();
        //            da.Fill(dt);
        //            IDProductopropio2.DataSource = dt;
        //            IDProductopropio2.DataTextField = "codigo_maestro";
        //            IDProductopropio2.DataValueField = "id_maestro";
        //            IDProductopropio2.DataBind();
        //            IDProductopropio2.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecciona", "0"));
        //        }
        //    }
        //}

        //protected void DropDescripcion2()
        //{
        //    int IdCliente = Convert.ToInt32(IDProductopropio2.SelectedValue);

        //    string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        //    using (SqlConnection sqlConectar = new SqlConnection(conectar))
        //    {
        //        sqlConectar.Open();
        //        using (SqlCommand sql = new SqlCommand("SELECT DISTINCT descripcion, Id_maestro FROM maestro where Id_maestro =" + IdCliente, sqlConectar))
        //        {
        //            sql.CommandType = CommandType.Text;
        //            Iddproducto2.DataSource = sql.ExecuteReader();
        //            Iddproducto2.DataTextField = "descripcion";
        //            Iddproducto2.DataValueField = "Id_maestro";
        //            Iddproducto2.DataBind();
        //        }
        //    }
        //}

        //protected void ObtenerPrecioBase2()
        //{
        //    int IdProducto = Convert.ToInt32(IDProductopropio2.SelectedValue);
        //    {
        //        string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        //        using (SqlConnection sqlConectar = new SqlConnection(conectar))
        //        {
        //            sqlConectar.Open();
        //            using (SqlCommand sql = new SqlCommand("select top 1 PRECIO_VENTA from LISTA_PRECIOS where ID_MAESTRO =" + IdProducto, sqlConectar))
        //            {
        //                sql.CommandType = CommandType.Text;
        //                using (SqlDataReader reader = sql.ExecuteReader())
        //                {
        //                    if (reader.Read())
        //                    {
        //                        IDPrecioBase2.Text = reader["PRECIO_VENTA"].ToString();
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        //protected void DdlCliente_SelectedIndexChangedIDProducto2(object sender, EventArgs e)
        //{
        //    DropDescripcion2();
        //    ObtenerPrecioBase2();
        //}

        /////**************************************************************************************************************************************************************************************//        ///**************************************************************************************************************************************************************************************//


        //protected void DropProducto3()
        //{
        //    string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        //    using (SqlConnection sqlConectar = new SqlConnection(conectar))
        //    {
        //        sqlConectar.Open();
        //        using (SqlCommand sql = new SqlCommand("select codigo_maestro, id_maestro from maestro order by ltrim(codigo_maestro) asc", sqlConectar))
        //        {
        //            sql.CommandType = CommandType.Text;
        //            SqlDataAdapter da = new SqlDataAdapter(sql);
        //            DataTable dt = new DataTable();
        //            da.Fill(dt);
        //            IDProductopropio3.DataSource = dt;
        //            IDProductopropio3.DataTextField = "codigo_maestro";
        //            IDProductopropio3.DataValueField = "id_maestro";
        //            IDProductopropio3.DataBind();
        //            IDProductopropio3.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecciona", "0"));
        //        }
        //    }
        //}

        //protected void DropDescripcion3()
        //{
        //    int IdCliente = Convert.ToInt32(IDProductopropio3.SelectedValue);

        //    string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        //    using (SqlConnection sqlConectar = new SqlConnection(conectar))
        //    {
        //        sqlConectar.Open();
        //        using (SqlCommand sql = new SqlCommand("SELECT DISTINCT descripcion, Id_maestro FROM maestro where Id_maestro =" + IdCliente, sqlConectar))
        //        {
        //            sql.CommandType = CommandType.Text;
        //            Iddproducto3.DataSource = sql.ExecuteReader();
        //            Iddproducto3.DataTextField = "descripcion";
        //            Iddproducto3.DataValueField = "Id_maestro";
        //            Iddproducto3.DataBind();
        //        }
        //    }
        //}

        //protected void ObtenerPrecioBase3()
        //{
        //    int IdProducto = Convert.ToInt32(IDProductopropio3.SelectedValue);
        //    {
        //        string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        //        using (SqlConnection sqlConectar = new SqlConnection(conectar))
        //        {
        //            sqlConectar.Open();
        //            using (SqlCommand sql = new SqlCommand("select top 1 PRECIO_VENTA from LISTA_PRECIOS where ID_MAESTRO =" + IdProducto, sqlConectar))
        //            {
        //                sql.CommandType = CommandType.Text;
        //                using (SqlDataReader reader = sql.ExecuteReader())
        //                {
        //                    if (reader.Read())
        //                    {
        //                        IDPrecioBase3.Text = reader["PRECIO_VENTA"].ToString();
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        //protected void DdlCliente_SelectedIndexChangedIDProducto3(object sender, EventArgs e)
        //{
        //    DropDescripcion3();
        //    ObtenerPrecioBase3();
        //}

        /////**************************************************************************************************************************************************************************************//        ///**************************************************************************************************************************************************************************************//


        //protected void DropProducto4()
        //{
        //    string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        //    using (SqlConnection sqlConectar = new SqlConnection(conectar))
        //    {
        //        sqlConectar.Open();
        //        using (SqlCommand sql = new SqlCommand("select codigo_maestro, id_maestro from maestro order by ltrim(codigo_maestro) asc", sqlConectar))
        //        {
        //            sql.CommandType = CommandType.Text;
        //            SqlDataAdapter da = new SqlDataAdapter(sql);
        //            DataTable dt = new DataTable();
        //            da.Fill(dt);
        //            IDProductopropio4.DataSource = dt;
        //            IDProductopropio4.DataTextField = "codigo_maestro";
        //            IDProductopropio4.DataValueField = "id_maestro";
        //            IDProductopropio4.DataBind();
        //            IDProductopropio4.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecciona", "0"));
        //        }
        //    }
        //}

        //protected void DropDescripcion4()
        //{
        //    int IdCliente = Convert.ToInt32(IDProductopropio4.SelectedValue);

        //    string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        //    using (SqlConnection sqlConectar = new SqlConnection(conectar))
        //    {
        //        sqlConectar.Open();
        //        using (SqlCommand sql = new SqlCommand("SELECT DISTINCT descripcion, Id_maestro FROM maestro where Id_maestro =" + IdCliente, sqlConectar))
        //        {
        //            sql.CommandType = CommandType.Text;
        //            Iddproducto4.DataSource = sql.ExecuteReader();
        //            Iddproducto4.DataTextField = "descripcion";
        //            Iddproducto4.DataValueField = "Id_maestro";
        //            Iddproducto4.DataBind();
        //        }
        //    }
        //}

        //protected void ObtenerPrecioBase4()
        //{
        //    int IdProducto = Convert.ToInt32(IDProductopropio4.SelectedValue);
        //    {
        //        string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        //        using (SqlConnection sqlConectar = new SqlConnection(conectar))
        //        {
        //            sqlConectar.Open();
        //            using (SqlCommand sql = new SqlCommand("select top 1 PRECIO_VENTA from LISTA_PRECIOS where ID_MAESTRO =" + IdProducto, sqlConectar))
        //            {
        //                sql.CommandType = CommandType.Text;
        //                using (SqlDataReader reader = sql.ExecuteReader())
        //                {
        //                    if (reader.Read())
        //                    {
        //                        IDPrecioBase4.Text = reader["PRECIO_VENTA"].ToString();
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        //protected void DdlCliente_SelectedIndexChangedIDProducto4(object sender, EventArgs e)
        //{
        //    DropDescripcion4();
        //    ObtenerPrecioBase4();
        //}

        /////**************************************************************************************************************************************************************************************//        ///**************************************************************************************************************************************************************************************//


        //protected void DropProducto5()
        //{
        //    string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        //    using (SqlConnection sqlConectar = new SqlConnection(conectar))
        //    {
        //        sqlConectar.Open();
        //        using (SqlCommand sql = new SqlCommand("select codigo_maestro, id_maestro from maestro order by ltrim(codigo_maestro) asc", sqlConectar))
        //        {
        //            sql.CommandType = CommandType.Text;
        //            SqlDataAdapter da = new SqlDataAdapter(sql);
        //            DataTable dt = new DataTable();
        //            da.Fill(dt);
        //            IDProductopropio5.DataSource = dt;
        //            IDProductopropio5.DataTextField = "codigo_maestro";
        //            IDProductopropio5.DataValueField = "id_maestro";
        //            IDProductopropio5.DataBind();
        //            IDProductopropio5.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecciona", "0"));
        //        }
        //    }
        //}

        //protected void DropDescripcion5()
        //{
        //    int IdCliente = Convert.ToInt32(IDProductopropio5.SelectedValue);

        //    string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        //    using (SqlConnection sqlConectar = new SqlConnection(conectar))
        //    {
        //        sqlConectar.Open();
        //        using (SqlCommand sql = new SqlCommand("SELECT DISTINCT descripcion, Id_maestro FROM maestro where Id_maestro =" + IdCliente, sqlConectar))
        //        {
        //            sql.CommandType = CommandType.Text;
        //            Iddproducto5.DataSource = sql.ExecuteReader();
        //            Iddproducto5.DataTextField = "descripcion";
        //            Iddproducto5.DataValueField = "Id_maestro";
        //            Iddproducto5.DataBind();
        //        }
        //    }
        //}

        //protected void ObtenerPrecioBase5()
        //{
        //    int IdProducto = Convert.ToInt32(IDProductopropio5.SelectedValue);
        //    {
        //        string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        //        using (SqlConnection sqlConectar = new SqlConnection(conectar))
        //        {
        //            sqlConectar.Open();
        //            using (SqlCommand sql = new SqlCommand("select top 1 PRECIO_VENTA from LISTA_PRECIOS where ID_MAESTRO =" + IdProducto, sqlConectar))
        //            {
        //                sql.CommandType = CommandType.Text;
        //                using (SqlDataReader reader = sql.ExecuteReader())
        //                {
        //                    if (reader.Read())
        //                    {
        //                        IDPrecioBase5.Text = reader["PRECIO_VENTA"].ToString();
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        //protected void DdlCliente_SelectedIndexChangedIDProducto5(object sender, EventArgs e)
        //{
        //    DropDescripcion5();
        //    ObtenerPrecioBase5();
        //}

        /////**************************************************************************************************************************************************************************************//        ///**************************************************************************************************************************************************************************************//


        //protected void DropProducto6()
        //{
        //    string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        //    using (SqlConnection sqlConectar = new SqlConnection(conectar))
        //    {
        //        sqlConectar.Open();
        //        using (SqlCommand sql = new SqlCommand("select codigo_maestro, id_maestro from maestro order by ltrim(codigo_maestro) asc", sqlConectar))
        //        {
        //            sql.CommandType = CommandType.Text;
        //            SqlDataAdapter da = new SqlDataAdapter(sql);
        //            DataTable dt = new DataTable();
        //            da.Fill(dt);
        //            IDProductopropio6.DataSource = dt;
        //            IDProductopropio6.DataTextField = "codigo_maestro";
        //            IDProductopropio6.DataValueField = "id_maestro";
        //            IDProductopropio6.DataBind();
        //            IDProductopropio6.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecciona", "0"));
        //        }
        //    }
        //}

        //protected void DropDescripcion6()
        //{
        //    int IdCliente = Convert.ToInt32(IDProductopropio6.SelectedValue);

        //    string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        //    using (SqlConnection sqlConectar = new SqlConnection(conectar))
        //    {
        //        sqlConectar.Open();
        //        using (SqlCommand sql = new SqlCommand("SELECT DISTINCT descripcion, Id_maestro FROM maestro where Id_maestro =" + IdCliente, sqlConectar))
        //        {
        //            sql.CommandType = CommandType.Text;
        //            Iddproducto6.DataSource = sql.ExecuteReader();
        //            Iddproducto6.DataTextField = "descripcion";
        //            Iddproducto6.DataValueField = "Id_maestro";
        //            Iddproducto6.DataBind();
        //        }
        //    }
        //}

        //protected void ObtenerPrecioBase6()
        //{
        //    int IdProducto = Convert.ToInt32(IDProductopropio6.SelectedValue);
        //    {
        //        string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        //        using (SqlConnection sqlConectar = new SqlConnection(conectar))
        //        {
        //            sqlConectar.Open();
        //            using (SqlCommand sql = new SqlCommand("select top 1 PRECIO_VENTA from LISTA_PRECIOS where ID_MAESTRO =" + IdProducto, sqlConectar))
        //            {
        //                sql.CommandType = CommandType.Text;
        //                using (SqlDataReader reader = sql.ExecuteReader())
        //                {
        //                    if (reader.Read())
        //                    {
        //                        IDPrecioBase6.Text = reader["PRECIO_VENTA"].ToString();
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        //protected void DdlCliente_SelectedIndexChangedIDProducto6(object sender, EventArgs e)
        //{
        //    DropDescripcion6();
        //    ObtenerPrecioBase6();
        //}

        ///**************************************************************************************************************************************************************************************//        ///**************************************************************************************************************************************************************************************//
        protected void GenerarFilas()
        {
            GuardarValoresFilas();

            HtmlTable tablaProductos = (HtmlTable)FindControlRecursive(this, "tablaProductos");

            if (tablaProductos != null)
            {
                // Limpiar las filas existentes excepto la fila de encabezado
                for (int i = tablaProductos.Rows.Count - 1; i > 0; i--)
                {
                    tablaProductos.Rows.RemoveAt(i);
                }

                List<string[]> valoresFilas = Session["ValoresFilas"] as List<string[]> ?? new List<string[]>();
                List<int> filaIDs = Session["FilaIDs"] as List<int> ?? new List<int>();

                for (int i = 0; i < valoresFilas.Count; i++)
                {
                    HtmlTableRow row = new HtmlTableRow();

                    // ID de la fila
                    int filaID = filaIDs[i];

                    // Columna 9 - Botón Eliminar
                    HtmlTableCell cell9 = new HtmlTableCell();
                    Button btnEliminar = new Button();
                    btnEliminar.ID = "btnEliminar" + filaID;
                    btnEliminar.Text = "Eliminar";
                    btnEliminar.CssClass = "btn btn-danger";
                    btnEliminar.Attributes["data-fila-id"] = filaID.ToString();
                    btnEliminar.Click += new EventHandler(btnEliminar_Click);
                    cell9.Controls.Add(btnEliminar);
                    row.Cells.Add(cell9);

                    // Columna 1
                    HtmlTableCell cell1 = new HtmlTableCell();
                    TextBox txtLinea = new TextBox();
                    txtLinea.ID = "Linea" + filaID;
                    txtLinea.Text = (i + 1).ToString();
                    txtLinea.ReadOnly = true;
                    cell1.Controls.Add(txtLinea);
                    row.Cells.Add(cell1);

                    // Columna 2
                    HtmlTableCell cell2 = new HtmlTableCell();
                    DropDownList ddlProductoPropio = new DropDownList();
                    ddlProductoPropio.ID = "IDProductopropio" + filaID;
                    ddlProductoPropio.AutoPostBack = true;
                    ddlProductoPropio.SelectedIndexChanged += new EventHandler(ddlProductoPropio_SelectedIndexChanged);
                    cell2.Controls.Add(ddlProductoPropio);
                    row.Cells.Add(cell2);

                    // Columna 3
                    HtmlTableCell cell3 = new HtmlTableCell();
                    DropDownList ddlProducto = new DropDownList();
                    ddlProducto.ID = "Iddproducto" + filaID;
                    cell3.Controls.Add(ddlProducto);
                    row.Cells.Add(cell3);

                    // Columna 4
                    HtmlTableCell cell4 = new HtmlTableCell();
                    TextBox txtCantidad = new TextBox();
                    txtCantidad.ID = "IdCantidad" + filaID;
                    txtCantidad.Attributes["placeholder"] = "Cantidad";
                    txtCantidad.TextMode = TextBoxMode.Number;
                    txtCantidad.Attributes.Add("onkeyup", $"calculateTotal({filaID})");
                    txtCantidad.Attributes.Add("onchange", $"calculateTotal({filaID})");
                    cell4.Controls.Add(txtCantidad);
                    row.Cells.Add(cell4);

                    // Columna 5
                    HtmlTableCell cell5 = new HtmlTableCell();
                    TextBox txtPrecioBase = new TextBox();
                    txtPrecioBase.ID = "IDPrecioBase" + filaID;
                    txtPrecioBase.Attributes["placeholder"] = "Precio";
                    txtPrecioBase.TextMode = TextBoxMode.Number;
                    txtPrecioBase.Attributes.Add("onkeyup", $"calculateTotal({filaID})");
                    txtPrecioBase.Attributes.Add("onchange", $"calculateTotal({filaID})");
                    cell5.Controls.Add(txtPrecioBase);
                    row.Cells.Add(cell5);

                    // Columna 6
                    HtmlTableCell cell6 = new HtmlTableCell();
                    TextBox txtDescuento = new TextBox();
                    txtDescuento.ID = "IdDescuentoProducto" + filaID;
                    txtDescuento.Attributes["placeholder"] = "Descuento";
                    txtDescuento.TextMode = TextBoxMode.Number;
                    txtDescuento.Attributes.Add("onkeyup", $"calculateTotal({filaID})");
                    txtDescuento.Attributes.Add("onchange", $"calculateTotal({filaID})");
                    cell6.Controls.Add(txtDescuento);
                    row.Cells.Add(cell6);

                    // Columna 7
                    HtmlTableCell cell7 = new HtmlTableCell();
                    TextBox txtPrecioUnitario = new TextBox();
                    txtPrecioUnitario.ID = "IdPrecioUnitario" + filaID;
                    txtPrecioUnitario.Attributes["placeholder"] = "Monto";
                    txtPrecioUnitario.TextMode = TextBoxMode.Number;
                    txtPrecioUnitario.ReadOnly = false;
                    cell7.Controls.Add(txtPrecioUnitario);
                    row.Cells.Add(cell7);

                    // Columna 8
                    HtmlTableCell cell8 = new HtmlTableCell();
                    TextBox txtPrecioTotal = new TextBox();
                    txtPrecioTotal.ID = "IdPrecioTotal" + filaID;
                    txtPrecioTotal.Attributes["placeholder"] = "Total";
                    txtPrecioTotal.TextMode = TextBoxMode.Number;
                    txtPrecioTotal.ReadOnly = false;
                    cell8.Controls.Add(txtPrecioTotal);
                    row.Cells.Add(cell8);

                    // Agregar la fila a la tabla
                    tablaProductos.Rows.Add(row);

                    // Llenar el dropdown de productos propio en cada postback
                    DropProducto(ddlProductoPropio);

                    // Restaurar los valores de la fila
                    if (valoresFilas.Count > i)
                    {
                        string[] valoresFila = valoresFilas[i];
                        ddlProductoPropio.SelectedValue = valoresFila[1];
                        if (!string.IsNullOrEmpty(valoresFila[1]))
                        {
                            DropDescripcion(ddlProducto, int.Parse(valoresFila[1]));

                            if (!string.IsNullOrEmpty(valoresFila[2]))
                            {
                                ddlProducto.SelectedValue = valoresFila[2];
                            }
                        }

                        txtCantidad.Text = valoresFila[3];
                        txtPrecioBase.Text = valoresFila[4];
                        txtDescuento.Text = valoresFila[5];
                        txtPrecioUnitario.Text = valoresFila[6];
                        txtPrecioTotal.Text = valoresFila[7];
                    }
                }

                // Adjuntar manejadores de eventos nuevamente
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "attachHandlers", "attachCalculateTotalHandlers();", true);
            }
            else
            {
                throw new NullReferenceException("tablaProductos fue null.");
            }
        }

        private void GuardarValoresFilas()
        {
            List<string[]> valoresFilas = new List<string[]>();

            HtmlTable tablaProductos = (HtmlTable)FindControlRecursive(this, "tablaProductos");
            List<int> filaIDs = Session["FilaIDs"] as List<int> ?? new List<int>();

            for (int i = 0; i < filaIDs.Count; i++)
            {
                int filaID = filaIDs[i];
                string[] valoresFila = new string[8];
                valoresFila[0] = ((TextBox)tablaProductos.FindControl("Linea" + filaID))?.Text;
                valoresFila[1] = ((DropDownList)tablaProductos.FindControl("IDProductopropio" + filaID))?.SelectedValue;
                valoresFila[2] = ((DropDownList)tablaProductos.FindControl("Iddproducto" + filaID))?.SelectedValue;
                valoresFila[3] = ((TextBox)tablaProductos.FindControl("IdCantidad" + filaID))?.Text;
                valoresFila[4] = ((TextBox)tablaProductos.FindControl("IDPrecioBase" + filaID))?.Text;
                valoresFila[5] = ((TextBox)tablaProductos.FindControl("IdDescuentoProducto" + filaID))?.Text;
                valoresFila[6] = ((TextBox)tablaProductos.FindControl("IdPrecioUnitario" + filaID))?.Text;
                valoresFila[7] = ((TextBox)tablaProductos.FindControl("IdPrecioTotal" + filaID))?.Text;
                valoresFilas.Add(valoresFila);
            }

            Session["ValoresFilas"] = valoresFilas;
        }

        protected void ButtonAgregar_Click(object sender, EventArgs e)
        {
            List<string[]> valoresFilas = Session["ValoresFilas"] as List<string[]> ?? new List<string[]>();
            List<int> filaIDs = Session["FilaIDs"] as List<int> ?? new List<int>();

            valoresFilas.Add(new string[8]); // Añadir una nueva fila vacía
            filaIDs.Add(valoresFilas.Count); // Añadir un nuevo ID de fila

            Session["ValoresFilas"] = valoresFilas;
            Session["FilaIDs"] = filaIDs;

            GenerarFilas();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Button btnEliminar = (Button)sender;
            int filaID = int.Parse(btnEliminar.Attributes["data-fila-id"]);

            List<string[]> valoresFilas = Session["ValoresFilas"] as List<string[]> ?? new List<string[]>();
            List<int> filaIDs = Session["FilaIDs"] as List<int> ?? new List<int>();

            int index = filaIDs.IndexOf(filaID);
            if (index != -1)
            {
                valoresFilas.RemoveAt(index);
                filaIDs.RemoveAt(index);
            }

            Session["ValoresFilas"] = valoresFilas;
            Session["FilaIDs"] = filaIDs;

            GenerarFilas();
        }

        private Control FindControlRecursive(Control root, string id)
        {
            if (root.ID == id)
            {
                return root;
            }
            foreach (Control control in root.Controls)
            {
                Control foundControl = FindControlRecursive(control, id);
                if (foundControl != null)
                {
                    return foundControl;
                }
            }
            return null;
        }

        protected void ddlProductoPropio_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            int rowIndex = int.Parse(ddl.ID.Substring("IDProductopropio".Length));
            DropDownList ddlProducto = (DropDownList)FindControlRecursive(this, "Iddproducto" + rowIndex);

            if (ddlProducto != null)
            {
                DropDescripcion(ddlProducto, int.Parse(ddl.SelectedValue));
            }
        }

        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            int rowIndex = int.Parse(textBox.ID.Substring(textBox.ID.Length - 1));
            TextBox txtCantidad = (TextBox)FindControlRecursive(this, "IdCantidad" + rowIndex);
            TextBox txtPrecioBase = (TextBox)FindControlRecursive(this, "IDPrecioBase" + rowIndex);
            TextBox txtPrecioTotal = (TextBox)FindControlRecursive(this, "IdPrecioTotal" + rowIndex);

            if (txtCantidad != null && txtPrecioBase != null && txtPrecioTotal != null)
            {
                decimal cantidad = string.IsNullOrEmpty(txtCantidad.Text) ? 0 : Convert.ToDecimal(txtCantidad.Text);
                decimal precioBase = string.IsNullOrEmpty(txtPrecioBase.Text) ? 0 : Convert.ToDecimal(txtPrecioBase.Text);
                txtPrecioTotal.Text = (cantidad * precioBase).ToString("0.00");
            }

            // Forzar el UpdatePanel a refrescar
            UpdatePanel1.Update();
        }

        protected void DropProducto(DropDownList ddl)
        {
            string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection sqlConectar = new SqlConnection(conectar))
            {
                sqlConectar.Open();
                using (SqlCommand sql = new SqlCommand("SELECT codigo_maestro, id_maestro FROM maestro ORDER BY LTRIM(codigo_maestro) ASC", sqlConectar))
                {
                    SqlDataAdapter da = new SqlDataAdapter(sql);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    ddl.DataSource = dt;
                    ddl.DataTextField = "codigo_maestro";
                    ddl.DataValueField = "id_maestro";
                    ddl.DataBind();
                    ddl.Items.Insert(0, new ListItem("Selecciona", "0"));
                }
            }
        }

        protected void DropDescripcion(DropDownList ddl, int IDMaestro)
        {
            string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection sqlConectar = new SqlConnection(conectar))
            {
                sqlConectar.Open();
                using (SqlCommand sql = new SqlCommand("SELECT DISTINCT descripcion, Id_maestro FROM maestro WHERE Id_maestro = @IDMaestro", sqlConectar))
                {
                    sql.Parameters.AddWithValue("@IDMaestro", IDMaestro);
                    SqlDataAdapter da = new SqlDataAdapter(sql);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    ddl.DataSource = dt;
                    ddl.DataTextField = "descripcion";
                    ddl.DataValueField = "Id_maestro";
                    ddl.DataBind();
                }
            }
        }

        protected void ObtenerPrecio(DropDownList ddl, TextBox txtPrecioBase, int IDMaestro)
        {
            if (int.TryParse(ddlCliente.SelectedValue, out int IDCliente) && int.TryParse(ddl.SelectedValue, out int IDMaestro2))
            {
                string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
                using (SqlConnection sqlConectar = new SqlConnection(conectar))
                {
                    sqlConectar.Open();
                    using (SqlCommand sql = new SqlCommand("[SP_ListarPrecios]", sqlConectar))
                    {
                        sql.CommandType = CommandType.StoredProcedure;
                        sql.Parameters.AddWithValue("@IDMaestro", IDMaestro); // Convertir a string porque el parámetro es Varchar
                        sql.Parameters.AddWithValue("@IDCliente", IDCliente);

                        using (SqlDataReader reader = sql.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtPrecioBase.Text = reader["PRECIO_VENTA"].ToString();
                            }
                        }
                    }
                }
            }
        }

        protected void BotonEnviar_Click(object sender, EventArgs e)
        {
            // Primera parte: Obtener datos del formulario
            string Proyecto = NombreProyecto.Text;
            string IDSucursal = ddlTienda.SelectedValue;
            string IDUCliente = ddlCliente.SelectedValue;
            string Total = PrecioTotalConIva2.Text;
            string Ivaa = Iva.Text;
            string Neto = PrecioNeto2.Text;
            string Observacion = txtdescripcion.Text;
            string Telefono = idTelefonoComercial.Text;
            string Direccion = idDireccion.Text;
            string RazonSocial = idRazonSocial.Text;
            string RutCliente = IdRut.Text;
            DateTime Fecha = DateTime.Parse(txtfecha.Text);

            // Segunda parte: Obtener datos de detalle del formulario
            HtmlTable tablaProductos = (HtmlTable)FindControlRecursive(this, "tablaProductos");

            if (tablaProductos != null)
            {
                for (int i = 1; i < tablaProductos.Rows.Count; i++) // Empezar en 1 para saltar la fila de encabezado
                {
                    HtmlTableRow fila = tablaProductos.Rows[i];
                    int filaID = i; // Asignar el índice de la fila o algún identificador único

                    TextBox txtLinea = fila.FindControl("Linea" + filaID) as TextBox;
                    TextBox txtCantidad = fila.FindControl("IdCantidad" + filaID) as TextBox;
                    TextBox txtValorUnitario = fila.FindControl("IdPrecioUnitario" + filaID) as TextBox;
                    TextBox txtTotalLinea = fila.FindControl("IdPrecioTotal" + filaID) as TextBox;
                    TextBox txtDescuento = fila.FindControl("IdDescuentoProducto" + filaID) as TextBox;
                    DropDownList ddlIDMaestro = fila.FindControl("IDProductopropio" + filaID) as DropDownList;
                    DropDownList ddlCodigoMaestro = fila.FindControl("Iddproducto" + filaID) as DropDownList;

                    if (txtLinea != null && txtCantidad != null && txtValorUnitario != null && txtTotalLinea != null && txtDescuento != null && ddlIDMaestro != null && ddlCodigoMaestro != null)
                    {
                        string Linea = txtLinea.Text;
                        string Cantidad = txtCantidad.Text;
                        string ValorUnitario = txtValorUnitario.Text;
                        string TotalLinea = txtTotalLinea.Text;
                        string Descuento = txtDescuento.Text;
                        string IDMaestro = ddlIDMaestro.SelectedValue;
                        string CodigoMaestro = ddlCodigoMaestro.SelectedValue;

                        // Llamar al método para agregar datos de cotización
                        AgregarDatosServicioCompleto(RutCliente, RazonSocial, Direccion, Telefono, Observacion, Fecha, Neto, Ivaa, Total, IDUCliente, IDSucursal, Proyecto, Descuento, TotalLinea, ValorUnitario, Cantidad, Linea, IDMaestro, CodigoMaestro);
                    }
                }
            }
            else
            {
                throw new NullReferenceException("tablaProductos fue null.");
            }

            LimpiarCampos();
        }


        protected void AgregarDatosServicioCompleto(string RutCliente, string RazonSocial, string Direccion, string Telefono, string Observacion, DateTime Fecha, string Neto, string Ivaa, string Total, string IDUCliente, string IDSuculsal, string Proyecto, string Descuento, string TotalLinea, string ValorUnitario, string Cantidad, string Linea, string IDMaestro, string CodigoMaestro)
        {
            string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conectar))
            {
                using (SqlCommand cmd = new SqlCommand("SP_AgregarCotizacion", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetros del procedimiento almacenado

                    cmd.Parameters.AddWithValue("@Proyecto", Proyecto);
                    cmd.Parameters.AddWithValue("@IDSucursal", IDSuculsal);
                    cmd.Parameters.AddWithValue("@IDCliente", IDUCliente);
                    cmd.Parameters.AddWithValue("@Total", Total);
                    cmd.Parameters.AddWithValue("@Iva", Ivaa);
                    cmd.Parameters.AddWithValue("@Neto", Neto);
                    cmd.Parameters.AddWithValue("@Observacion", Observacion);
                    cmd.Parameters.AddWithValue("@Telefono", Telefono);
                    cmd.Parameters.AddWithValue("@Dirreccion", Direccion);
                    cmd.Parameters.AddWithValue("@RazonSocial", RazonSocial);
                    cmd.Parameters.AddWithValue("@RutCliente", RutCliente);
                    cmd.Parameters.AddWithValue("@Fecha", Fecha);
                    cmd.Parameters.AddWithValue("@Linea", Linea);
                    cmd.Parameters.AddWithValue("@IDMaestro", IDMaestro);
                    cmd.Parameters.AddWithValue("@CodigoMaestro", CodigoMaestro);
                    cmd.Parameters.AddWithValue("@Cantidad", Cantidad);
                    cmd.Parameters.AddWithValue("@ValorUnitario", ValorUnitario);
                    cmd.Parameters.AddWithValue("@TotalLinea", TotalLinea);
                    cmd.Parameters.AddWithValue("@Descuento", Descuento);
                    cmd.Parameters.AddWithValue("@IDItemPrecio", null);
                    cmd.Parameters.AddWithValue("@Descripcion", Observacion);

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
            //Idbodega.SelectedIndex = 0;
            //DropDocumento.SelectedIndex = 0;
            //IDdescripcion.Text = "";
            //IDEntrada.Text = "";
            //IdSalida.Text = "";
            //Idfecha.Text = DateTime.Now.Date.ToShortDateString();
        }

        //--------------------------------------------------------Funciones ------------------------------------------------------//

        protected void PanelOcualtaMostrar(object sender, EventArgs e)
        {
            InformacionCliente.Visible = false;
            Producto.Visible = true;
        }

        protected void DdlCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropTienda();
            ObtenerRut();
            ObtenerTelefonoComercial();
            ObtenerRegion();
            ObtenerEmail();
            ObtenerDireccion();
            ObtenerRazonSocial();
            ObtenerTelefonoSocial();
            //----------------------------Tienda --------------------------//
            ObtenerDirecciontienda();
            ObtenerCasaTienda();
            ObtenerTelefonoTienda();
            ObtenerRegionTienda();
            ObtenerComunaTienda();
            ObtenerEncargadoTienda();
            ObtenerEmailTienda();

        }


    }
}