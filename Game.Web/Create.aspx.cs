using System;
using Game.Presentation;

namespace Game
{
    public partial class Create : BasePage
    {

        ICreateAccountPresentation _Presenter;//new CreateAccountPresentation();
        ILoginPresentation _LoginPresenter;

        public ILoginPresentation LoginPresenter
        {
          get
          {
            return LazyLoad(() => this._LoginPresenter);
          }
        }

        public ICreateAccountPresentation Presenter
        {
          get
          {
            return LazyLoad(() => this._Presenter);
          }
        }

        private void Page_Init(object sender, EventArgs e)
        {
          if (!LoginPresenter.IsAuthenticated)
          {
            btCreate.Click +=
                (s, ea) =>
                {
                  Presenter.Username = tbUser.Text;
                  Presenter.Password = tbPass.Text;
                  Presenter.PasswordConfirmation = tbPassCon.Text;
                  Presenter.CreateAccount();
                  if (Presenter.CreationSuccess)
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
                  message.Text = Presenter.ResultMessage;
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

          if (Presenter.UsernameVisible)
          {
            lbUser.Text = "Nuevo usuario";
          }
          else
          {
            lbUser.Visible = false;
            tbUser.Visible = false;
          }
          if (Presenter.PasswordVisible)
          {
            lbPass.Text = "Nueva contraseña";
          }
          else
          {
            lbPass.Visible = false;
            tbPass.Visible = false;
          }
          if (Presenter.PasswordConfirmationVisible)
          {
            lbPassCon.Text = "Confirmar contraseña";
          }
          else
          {
            lbPassCon.Visible = false;
            tbPassCon.Visible = false;
          }
          if (Presenter.CreateButtonVisible)
          {
            btCreate.Text = "Crear cuenta";
          }
          else
          {
            btCreate.Visible = false;
          }
          if (Presenter.RedirectButtonVisible)
          {
            btRed.Text = "Iniciar sesion";
          }
          else
          {
            btRed.Visible = false;
          }
          if (Presenter.ResultMessageVisible)
          {
            message.Visible = true;
            message.Text = Presenter.ResultMessage;
          }
        }
    }
}