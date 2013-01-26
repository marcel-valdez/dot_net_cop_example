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
    public partial class Create : System.Web.UI.Page
    {

        ICreateAccountPresentation cap;//new CreateAccountPresentation();
        ILoginPresentation ilp;

        private void Page_Init(object sender, EventArgs e)
        {
            cap = Dependency.Locator.Create<ICreateAccountPresentation>(this.Session.SessionID);
            ilp = Dependency.Locator.Create<ILoginPresentation>(this.Session.SessionID);
            if (!ilp.IsAuthenticated)
            {
                btCreate.Click +=
                    (s, ea) =>
                    {
                        cap.Username = tbUser.Text;
                        cap.Password = tbPass.Text;
                        cap.PasswordConfirmation = tbPassCon.Text;
                        cap.CreateAccount();
                        if (cap.CreationSuccess)
                        {
                            lbUser.Visible = false;
                            tbUser.Visible = false;
                            lbPass.Visible = false;
                            tbPass.Visible = false;
                            lbPassCon.Visible = false;
                            tbPassCon.Visible = false;
                            btCreate.Visible = false;
                            btRed.Text = "Iniciar sesion";
                            btRed.Visible = true;
                        }
                        message.Visible = true;
                        message.Text = cap.ResultMessage;
                    };
                btRed.Click +=
                    (s, ea) =>
                    {
                        Response.Redirect("Default.aspx");
                    };
            }
            else
            {
                Response.Redirect("Profile.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lbTitle.Text = "Nueva cuenta";

            if (cap.UsernameVisible)
            {
                lbUser.Text = "Nuevo usuario";
            }
            else
            {
                lbUser.Visible = false;
                tbUser.Visible = false;
            }
            if (cap.PasswordVisible)
            {
                lbPass.Text = "Nueva contraseña";
            }
            else
            {
                lbPass.Visible = false;
                tbPass.Visible = false;
            }
            if (cap.PasswordConfirmationVisible)
            {
                lbPassCon.Text = "Confirmar contraseña";
            }
            else
            {
                lbPassCon.Visible = false;
                tbPassCon.Visible = false;
            }
            if (cap.CreateButtonVisible)
            {
                btCreate.Text = "Crear cuenta";
            }
            else
            {
                btCreate.Visible = false;
            }
            if (cap.RedirectButtonVisible)
            {
                btRed.Text = "Iniciar sesion";
            }
            else
            {
                btRed.Visible = false;
            }
            if (cap.ResultMessageVisible)
            {
                message.Visible = true;
                message.Text = cap.ResultMessage;
            }
        }
    }
}