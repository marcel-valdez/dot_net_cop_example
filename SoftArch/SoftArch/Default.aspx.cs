using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Presentation;
using DependencyLocation;

namespace SoftArch
{
    public partial class _Default : System.Web.UI.Page
    {

        ILoginPresentation lp = Dependency.Locator.Create<ILoginPresentation>(HttpContext.Current.Session.SessionID);//new LoginPresentation();

        private void Page_Init(object sender, EventArgs e)
        {

            lp = Dependency.Locator.Create<ILoginPresentation>(this.Session.SessionID);

            //lp 
            if (!lp.IsAuthenticated)
            {
                this.lkBCreate.Click +=
                    (s, ea) =>
                    {
                        Response.Redirect("Create.aspx");
                    };
                this.btLogin.Click +=
                    (s, ea) =>
                    {
                        lp.Username = tbUser.Text;
                        lp.Password = tbPass.Text;
                        lp.LoginClicked();
                        if (lp.IsAuthenticated)
                        {
                            Response.Redirect("Profile.aspx");
                        }
                        else
                        {
                            lbMsg.Visible = lp.MessageVisible;
                            lbMsg.Text = lp.Message;
                        }
                    };
            }
            else
            {
                Response.Redirect("Profile.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lbTitle.Text = "Ingresar al juego";
            lbUser.Text = "Usuario";
            lbPass.Text = "Contraseña";
            btLogin.Text = "Ingresar";
            lbCreate.Text = "Crear nueva cuenta";
            if (lp.MessageVisible)
            {
                lbMsg.Text = lp.Message;
            }
        }
    }
}
