using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Game.Presentation;
using DependencyLocation;

namespace SoftArch
{
    public partial class Room : System.Web.UI.Page, System.Web.UI.ICallbackEventHandler
    {

        IRoomPresentation rp = Dependency.Locator.Create<IRoomPresentation>(HttpContext.Current.Session.SessionID); //new RoomPresentation();
        string json = "";

        public void RaiseCallbackEvent(String arg)
        {
            char[] delimeter = { ',' };
            string[] str = arg.Split(delimeter);
            if (str[0] == "select")
            {
                rp.SelectedUserIndex = Convert.ToInt16(str[1]);
                IStatisticsPresentation isp = rp.SelectedUserStatistics;
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
                rp.SelectedUserIndex = ltbUsers.SelectedIndex;
                rp.ChallengeButtonClicked();
                while (!rp.DialogVisible) { }
                IUserDialogPresentation iudp = rp.CurrentDialogPresentation;
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
                rp.FindBattleButtonClicked();
                while (!rp.DialogVisible) { }
                IUserDialogPresentation iudp = rp.CurrentDialogPresentation;
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
                if (rp.ReadyForBattle)
                {
                    Response.Redirect("Battle.aspx");
                }

                if (rp.DialogVisible)
                {
                    IUserDialogPresentation iudp = rp.CurrentDialogPresentation;
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
                if (rp.ReadyForBattle)
                {
                    Response.Redirect("Battle.aspx");
                }
                if (!rp.DialogVisible)
                {
                    json = "info = { \"response\": \"cancel\" }";
                }
            }

            if (str[0] == "finding")
            {
                if (rp.ReadyForBattle)
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
                    rp.HomeButtonClicked();
                    Response.Redirect("Profile.aspx");
                };
            btLogout.Click +=
                (s, ea) =>
                {
                    rp.LogoutButton.LogoutButtonClicked();
                    Response.Redirect("Default.aspx");
                };
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Title";
            lbTitle.Text = "Sala ";
            for (int i = 0; i < rp.Usuarios.Count<IRoomUserDTO>(); i++) 
            {
                ListItem lt = new ListItem(rp.Usuarios.ElementAt(i).Username + " (" + rp.Usuarios.ElementAt(i).RoomUserState + ")", Convert.ToString(i));          
                if (rp.Usuarios.ElementAt(i).RoomUserState == "Esperando")
                {
                    lt.Attributes.CssStyle.Add("background-color", "Green");
                }
                if (rp.Usuarios.ElementAt(i).RoomUserState == "Jugando")
                {
                    lt.Attributes.CssStyle.Add("background-color", "Red");
                }
                ltbUsers.Items.Add(lt);
            }
            btExit.Text = "Salir";
            btLogout.Text = rp.LogoutButton.ButtonText;
        }
    }
}