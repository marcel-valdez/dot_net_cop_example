using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Presentation;

namespace Game
{
    public partial class Battle : BasePage, ICallbackEventHandler
    {

        IBattlePresentation _Presenter;
        string json = "";

        public IBattlePresentation Presenter
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
          if (str[0] == "moving")
          {
            int i = Convert.ToInt16(str[1]) - 1;
            Presenter.MyCards.ElementAt(i).Selected = true;
            Presenter.ConfirmButtonPressed();
            json = "info = {\"state\": \"waitingM\"}";
          }
          if (str[0] == "waitingM")
          {
            if (Presenter.HasBattleEnded)
            {
              json = "info = {\"state\": \"finish\", \"MiddleMsgTitle\": \"" + Presenter.MiddleMsgTitle +
                      "\", \"MiddleMsgFooter\": \"" + Presenter.MiddleMsgFooter + "\"}";
            }
            else
            {
              if (!Presenter.IsWaitingForOpponent)
              {
                json = "info = {\"state\": \"dispose\", \"HarmInflictedTxt\": \"" + Presenter.HarmInflictedTxt +
                        "\", \"HarmInflictedPts\": \"" + Presenter.HarmInflictedPts +
                        "\", \"HarmReceivedTxt\": \"" + Presenter.HarmReceivedTxt +
                        "\", \"HarmReceivedPts\": \"" + Presenter.HarmReceivedPts +
                        "\", \"MiddleMsgTitle\": \"" + Presenter.MiddleMsgTitle +
                        "\", \"MiddleMsgFooter\": \"" + Presenter.MiddleMsgFooter +
                        "\", \"hpPlayer\": \"" + Presenter.MyInfo.LifePoints +
                        "\", \"hpOpponent\": \"" + Presenter.OppInfo.LifePoints + "\"}";
              }
              else
              {
                json = "info = {\"state\": \"waitingM\"}";
              }
            }
          }

          if (str[0] == "waitingD")
          {
            if (Presenter.HasBattleEnded)
            {
              json = "info = {\"state\": \"finish\", \"MiddleMsgTitle\": \"" + Presenter.MiddleMsgTitle +
                      "\", \"MiddleMsgFooter\": \"" + Presenter.MiddleMsgFooter + "\"}";
            }
            else
            {
              if (!Presenter.IsWaitingForOpponent)
              {
                json = "info = {\"state\": \"moving\", ";
                json += "\"cardP1\": \"" + Presenter.MyCards.ElementAt(0).ImageUrl + "\",";
                json += "\"cardP2\": \"" + Presenter.MyCards.ElementAt(1).ImageUrl + "\",";
                json += "\"cardP3\": \"" + Presenter.MyCards.ElementAt(2).ImageUrl + "\",";
                json += "\"cardP4\": \"" + Presenter.MyCards.ElementAt(3).ImageUrl + "\",";
                json += "\"cardO1\": \"" + Presenter.OppCards.ElementAt(0).ImageUrl + "\",";
                json += "\"cardO2\": \"" + Presenter.OppCards.ElementAt(1).ImageUrl + "\",";
                json += "\"cardO3\": \"" + Presenter.OppCards.ElementAt(2).ImageUrl + "\",";
                json += "\"cardO4\": \"" + Presenter.OppCards.ElementAt(3).ImageUrl + "\"}";
              }
              else
              {
                json = "info = {\"state\": \"waitingD\"}";
              }
            }
          }
          if (str[0] == "dispose")
          {
            int[] s = new int[4];
            s[0] = Convert.ToInt16(str[1]);
            s[1] = Convert.ToInt16(str[2]);
            s[2] = Convert.ToInt16(str[3]);
            s[3] = Convert.ToInt16(str[4]);
            for (int i = 0; i < 4; i++)
            {
              Presenter.MyCards.ElementAt(i).Selected = false;
              if (s[i] == 1)
              {
                Presenter.MyCards.ElementAt(i).Selected = true;
              }
            }
            Presenter.ConfirmButtonPressed();
            json = "info = {\"state\": \"waitingD\"}";
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
          btReturn.Click +=
              (s, ea) =>
              {
                Response.Redirect("Room.aspx");
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
          lbOpponent.Text = Presenter.OppInfo.Username; //bppo.Username;
          hpTxtO.Text = Presenter.OppInfo.LifePointsTxt; //bppo.LifePointsTxt;
          hpPointsO.Text = Convert.ToString(Presenter.OppInfo.LifePoints); //bppo.LifePoints);
          Image[] fieldO = new Image[4] { card1O, card2O, card3O, card4O };
          Image[] fieldP = new Image[4] { card1P, card2P, card3P, card4P };
          int i = 0;
          foreach (IBattleCardPresentation ibcp in Presenter.OppCards)
          {
            fieldO[i].ImageUrl = ibcp.ImageUrl;
            fieldO[i].Height = 110;
            fieldO[i].Width = 90;
            i++;
          }
          i = 0;
          foreach (IBattleCardPresentation ibcp in Presenter.MyCards)
          {
            fieldP[i].ImageUrl = ibcp.ImageUrl;
            fieldP[i].Height = 110;
            fieldP[i].Width = 90;
            i++;
          }
          msg.Text = Presenter.MiddleMsgTitle; //"Welcome to Death!";
          lbPlayer.Text = Presenter.MyInfo.Username; //bppp.Username;
          hpTxtP.Text = Presenter.MyInfo.LifePointsTxt; //bppp.LifePointsTxt;
          hpPointsP.Text = Convert.ToString(Presenter.MyInfo.LifePoints); //bppp.LifePoints);
          //confirm.Text = "Confirmar";
          //confirm.Enabled = bp.ConfirmBtnEnabled;
          btReturn.Text = "Salir";
          btReturn.Attributes.CssStyle.Add("visibility", "hidden");
          btLogout.Text = Presenter.LogoutButton.ButtonText;
        }
    }
}