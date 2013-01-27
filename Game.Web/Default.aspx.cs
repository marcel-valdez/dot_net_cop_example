using System;
using Game.Presentation;

namespace Game
{
  public partial class _Default : BasePage
  {

    ILoginPresentation _Login;

    private ILoginPresentation LoginPresenter
    {
      get
      {
        return LazyLoad(() => this._Login);
      }
    }

    private void Page_Init(object sender, EventArgs e)
    {
      if (!LoginPresenter.IsAuthenticated)
      {
        this.lkBCreate.Click +=
            (s, ea) =>
            {
              Response.Redirect("Create.aspx");
            };
        this.btnLogin.Click +=
            (s, ea) =>
            {
              LoginPresenter.Username = tbUser.Text;
              LoginPresenter.Password = tbPass.Text;
              LoginPresenter.LoginClicked();
              if (LoginPresenter.IsAuthenticated)
              {
                Response.Redirect("Profile.aspx");
              }
              else
              {
                lbMsg.Visible = LoginPresenter.MessageVisible;
                lbMsg.Text = LoginPresenter.Message;
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
      btnLogin.Text = "Ingresar";
      lbCreate.Text = "Crear nueva cuenta";
      if (LoginPresenter.MessageVisible)
      {
        lbMsg.Text = LoginPresenter.Message;
      }
    }
  }
}
