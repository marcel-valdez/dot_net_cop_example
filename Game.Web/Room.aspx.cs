using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using DependencyLocation;
using Game.Presentation;

namespace Game
{
  public partial class Room : BasePage, ICallbackEventHandler
  {

    IRoomPresentation _Presenter;
    string json = "";

    public IRoomPresentation Presenter
    {
      get
      {
        return LazyLoad(() => this._Presenter);
      }
    }

    public void RaiseCallbackEvent(String arg)
    {
      char[] delimeter = { ',' };
      string[] str = arg.Split(delimeter);
      if (str[0] == "select")
      {
        Presenter.SelectedUserIndex = Convert.ToInt16(str[1]);
        IStatisticsPresentation isp = Presenter.SelectedUserStatistics;
        json = "info = { \"response\": \"select\", \"Username\": \"" +
                      isp.Username + "\", \"lbPlayed\": \"" +
                      isp.PlayedGamesLabelText + "\", \"lbPCount\": \"" +
                      Convert.ToString(isp.PlayedGamesCount) + "\", \"lbWon\": \"" +
                      isp.WonGamesLabelText + "\", \"lbWCount\": \"" +
                      Convert.ToString(isp.WonGamesCount) + "\", \"lbLost\": \"" +
                      isp.LostGamesLabelText + "\", \"lbLCount\": \"" +
                      Convert.ToString(isp.LostGamesCount) + "\"}";
      }

      if (str[0] == "initChallenge")
      {
        Presenter.SelectedUserIndex = ltbUsers.SelectedIndex;
        Presenter.ChallengeButtonClicked();
        while (!Presenter.DialogVisible)
        {
        }
        IUserDialogPresentation iudp = Presenter.CurrentDialogPresentation;
        json = "info = { \"response\" : \"waitFirst\", \"title\": \"" + iudp.Title +
                      "\", \"subtitle\": \"" + iudp.Subtitle +
                      "\", \"dialogMessage\": \"" + iudp.DialogMessage +
                      "\", \"buttons\": [";
        int limit = iudp.ButtonsText.Count<string>();
        int i = 0;
        foreach (string bt in iudp.ButtonsText)
        {
          json += "\"" + bt + "\"";
          i++;
          if (i != limit)
          {
            json += ",";
          }
        }
        json += "]}";
      }

      if (str[0] == "findBattle")
      {
        Presenter.FindBattleButtonClicked();
        while (!Presenter.DialogVisible)
        {
        }
        IUserDialogPresentation iudp = Presenter.CurrentDialogPresentation;
        json = "info = { \"response\" : \"waitFirst\", \"title\": \"" + iudp.Title +
                      "\", \"subtitle\": \"" + iudp.Subtitle +
                      "\", \"dialogMessage\": \"" + iudp.DialogMessage +
                      "\", \"buttons\": [";
        int limit = iudp.ButtonsText.Count<string>();
        int i = 0;
        foreach (string bt in iudp.ButtonsText)
        {
          json += "\"" + bt + "\"";
          i++;
          if (i != limit)
          {
            json += ",";
          }
        }
        json += "]}";
      }

      if (str[0] == "listen")
      {
        if (Presenter.ReadyForBattle)
        {
          Response.Redirect("Battle.aspx");
        }

        if (Presenter.DialogVisible)
        {
          IUserDialogPresentation iudp = Presenter.CurrentDialogPresentation;
          json = "info = { \"response\" : \"challenge\", \"title\": \"" + iudp.Title +
                        "\", \"subtitle\": \"" + iudp.Subtitle +
                        "\", \"dialogMessage\": \"" + iudp.DialogMessage +
                        "\", \"buttons\": [";
          int limit = iudp.ButtonsText.Count<string>();
          int i = 0;
          foreach (string bt in iudp.ButtonsText)
          {
            json += "\"" + bt + "\"";
            i++;
            if (i != limit)
            {
              json += ",";
            }
          }
          json += "]}";
        }
        else
        {
          json = "info = { \"response\": \"listen\" }";
        }
      }

      if (str[0] == "waiting")
      {
        if (Presenter.ReadyForBattle)
        {
          Response.Redirect("Battle.aspx");
        }
        if (!Presenter.DialogVisible)
        {
          json = "info = { \"response\": \"cancel\" }";
        }
      }

      if (str[0] == "finding")
      {
        if (Presenter.ReadyForBattle)
        {
          Response.Redirect("Battle.aspx");
        }
      }

      if (str[0] == "pressed")
      {
        IUserDialogPresentation giudp = Dependency.Locator.GetSingleton<IUserDialogPresentation>();
        giudp.PressedButtonIndex = Convert.ToInt16(str[1]);
        giudp.ButtonClicked();
        json = "info = { \"response\": \"listen\" }";
      }
    }

    public string GetCallbackResult()
    {
      return json;
    }

    private void Page_Init(object sender, EventArgs e)
    {

      string callbackRef = Page.ClientScript.GetCallbackEventReference(this, "arg", "PlayersViewHandler", "context");
      string callbackScript = "function CallServer(arg, context) {" + callbackRef + ";}";
      Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "CallServer", callbackScript, true);
      btExit.Click +=
          (s, ea) =>
          {
            Presenter.HomeButtonClicked();
            Response.Redirect("Profile.aspx");
          };
      btLogout.Click +=
          (s, ea) =>
          {
            Presenter.LogoutButton.LogoutButtonClicked();
            Response.Redirect("Default.aspx");
          };
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      Title = "Title";
      lbTitle.Text = "Sala ";
      for (int i = 0; i < Presenter.Usuarios.Count<IRoomUserDTO>(); i++)
      {
        ListItem lt = new ListItem(Presenter.Usuarios.ElementAt(i).Username + " (" + Presenter.Usuarios.ElementAt(i).RoomUserState + ")", Convert.ToString(i));
        if (Presenter.Usuarios.ElementAt(i).RoomUserState == "Esperando")
        {
          lt.Attributes.CssStyle.Add("background-color", "Green");
        }
        if (Presenter.Usuarios.ElementAt(i).RoomUserState == "Jugando")
        {
          lt.Attributes.CssStyle.Add("background-color", "Red");
        }
        ltbUsers.Items.Add(lt);
      }
      btExit.Text = "Salir";
      btLogout.Text = Presenter.LogoutButton.ButtonText;
    }
  }
}