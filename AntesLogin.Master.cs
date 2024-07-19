using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DMC
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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
    }
}