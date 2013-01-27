using System;
using System.Web.UI.WebControls;
using Game.Logic;
using Game.Presentation;
using DependencyLocation;
using Game.Core;

namespace Game
{
  public partial class Profile : BasePage
  {

    IHomePresentation _HomePresenter;
    ILoginPresentation _LoginPresenter;

    public IHomePresentation HomePresenter
    {
      get
      {
        return LazyLoad(() => this._HomePresenter);
      }
    }

    public ILoginPresentation LoginPresenter
    {
      get
      {
        return LazyLoad(() => this._LoginPresenter);
      }
    }

    private void Page_Init(object sender, EventArgs e)
    {
      try
      {
        if (LoginPresenter.IsAuthenticated)
        {
          btnEnter.Click += OnIngresarClicked;
          btnLogout.Click += OnLogoutClicked;
          RoomsListbox_Init();
        }
        else
        {
          Response.Redirect("Default.aspx");
        }
      }
      catch (SecurityException)
      {
        Response.Redirect("Default.aspx");
        return;
      }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      lbTitle.Text = HomePresenter.Title;
      lbHello.Text = HomePresenter.WelcomeMessage;
      lbUser.Text = HomePresenter.UserStats.Username;
      btnEnter.Text = "Ingresar";
      btnLogout.Text = HomePresenter.LogoutButton.ButtonText;
      lbxGames.Text = HomePresenter.UserStats.PlayedGamesLabelText;
      lblGCount.Text = Convert.ToString(HomePresenter.UserStats.PlayedGamesCount);
      lblWon.Text = HomePresenter.UserStats.WonGamesLabelText;
      lblWCount.Text = Convert.ToString(HomePresenter.UserStats.WonGamesCount);
      lblLost.Text = HomePresenter.UserStats.LostGamesLabelText;
      lblLCount.Text = Convert.ToString(HomePresenter.UserStats.LostGamesCount);
      lblMessage.Text = HomePresenter.Message;
      lblMessage.Visible = HomePresenter.MessageVisible;
    }

    private void OnLogoutClicked(object _sender, EventArgs _evtArgs)
    {
      HomePresenter.LogoutButton.LogoutButtonClicked();
      Response.Redirect("Default.aspx");
    }

    private void OnIngresarClicked(object sender, EventArgs evtArgs)
    {
      Log("Attempting to set room index {0}.", lbxRooms.SelectedIndex);
      Log("Page cycle stage: {0}", this.CycleState);

      HomePresenter.SelectedRoomIndex = lbxRooms.SelectedIndex;
      HomePresenter.IngresarClicked();
      // HACK: Commented due to being too hacky (forever loop)

      Response.Redirect("Room.aspx");
    }

    private void RoomsListbox_Init()
    {
      lbxRooms.Rows = 10;
      foreach (string str in HomePresenter.RoomNames)
      {
        lbxRooms.Items.Add(new ListItem(str));
      }
    }
  }
}