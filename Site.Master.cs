using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DMC
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["NOMBRE"] != null)
            {
                string Nombre = Session["NOMBRE"].ToString();
            }
            if (Session["APPAT"] != null)
            {
                string APPAT = Session["APPAT"].ToString();
            } 

            
            if (Session["ID_USUARIO"] != null)
            {
                string IDUsuario = Session["ID_USUARIO"].ToString();
            }


            string PrimerNombre = Session["NOMBRE"] as string;
            string Apellido = Session["APPAT"] as string;

            NombreyApellido.Text = PrimerNombre + " " + Apellido;
        }
        protected void BtnLogout_Click(object sender, EventArgs e)
        {
            // Limpiar la sesión
            Session.Clear();
            Session.Abandon();

            // Desautenticar al usuario utilizando FormsAuthentication
            FormsAuthentication.SignOut();

            // Redirigir a la página de inicio de sesión
            Response.Redirect("~/DMC/Login/Login.aspx");
        }
        protected void ddlProductosFx(object sender, EventArgs e)
        {

            if (ddlProductos.SelectedValue == "Inventario")
            {
                Response.Redirect("~/DMC/Productos/Inventario.aspx");
            }
            else if (ddlProductos.SelectedValue == "Bodega")
            {
                Response.Redirect("~/DMC/Productos/Bodegas.aspx");
            }
            else if (ddlProductos.SelectedValue == "IngresarMovimiento")
            {
                Response.Redirect("~/DMC/Productos/IngresarMovimiento.aspx");
            }
            else if (ddlProductos.SelectedValue == "Documentos")
            {
                Response.Redirect("~/DMC/Productos/Documentos.aspx");
            }
            else if (ddlProductos.SelectedValue == "MovimientoProductos")
            {
                Response.Redirect("~/DMC/Productos/MovimientoPorProducto.aspx");
            }
            else if (ddlProductos.SelectedValue == "MaestroProductos")
            {
                Response.Redirect("~/DMC/Productos/MaestroProductos.aspx");
            }
            else if (ddlProductos.SelectedValue == "AgregarProducto")
            {
                Response.Redirect("~/DMC/Productos/AgregarProducto.aspx");
            }
            else if (ddlProductos.SelectedValue == "Familias")
            {
                Response.Redirect("~/DMC/Productos/Familias.aspx");
            }
            else if (ddlProductos.SelectedValue == "SubFamilias")
            {
                Response.Redirect("~/DMC/Productos/SubFamilias.aspx");
            }

        }
        protected void ddlComercialFx(object sender, EventArgs e)
        {
            if (ddlComercial.SelectedValue == "MaestroCliente")
            {
                Response.Redirect("~/DMC/Comercio/MaestroCliente.aspx");
            }
            else if (ddlComercial.SelectedValue == "ListaPrecios")
            {
                Response.Redirect("~/DMC/Comercio/ListarPrecios.aspx");
            }

        }
        protected void ddlDocumentoFx(object sender, EventArgs e)
        {
            if (ddlDocumento.SelectedValue == "EmitirDocumento")
            {
                Response.Redirect("~/DMC/Documentos/Cotizacion.aspx");
            }
            else if (ddlDocumento.SelectedValue == "ListadoDocumentos")
            {
                Response.Redirect("");
            }
        }
        protected void ddlAdministrativoFx(object sender, EventArgs e)
        {
            if (ddlAdministrativo.SelectedValue == "Usuario")
            {
                Response.Redirect("~/DMC/Administracion/MantencionUsuario.aspx");
            }
        }
        protected void ddlSoporteFx(object sender, EventArgs e)
        {
            if (ddlProductos.SelectedValue == "18")
            {
                Response.Redirect("~/DMC/Documentos/DesCotizacion.aspx");
            }
            else if (ddlAdministrativo.SelectedValue == "19")
            {
                Response.Redirect("~/DMC/Documentos/DesCotizacion.aspx");
            }
            else if (ddlAdministrativo.SelectedValue == "20")
            {
                Response.Redirect("~/DMC/Documentos/DesCotizacion.aspx");
            }

        }

        protected void MenuInicio(object sender, EventArgs e)
        {
            Response.Redirect("~/DMC/Menus/MenuDespuesLogin.aspx");
        }
    }
}