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
    public partial class Battle : System.Web.UI.Page, System.Web.UI.ICallbackEventHandler
    {

        IBattlePresentation bp = Dependency.Locator.Create<IBattlePresentation>(HttpContext.Current.Session.SessionID);
        string json = "";

        public void RaiseCallbackEvent(String arg)
        {
            char[] delimeter = { ',' };
            string[] str = arg.Split(delimeter);
            if (str[0] == "moving")
            {
                int i = Convert.ToInt16(str[1]) - 1;
                bp.MyCards.ElementAt(i).Selected = true;
                bp.ConfirmButtonPressed();
                json = "info = {\"state\": \"waitingM\"}";
            }
            if (str[0] == "waitingM")
            {
                if (bp.HasBattleEnded)
                {
                    json = "info = {\"state\": \"finish\", \"MiddleMsgTitle\": \"" + bp.MiddleMsgTitle +
                            "\", \"MiddleMsgFooter\": \"" + bp.MiddleMsgFooter + "\"}";
                }
                else
                {
                    if (!bp.IsWaitingForOpponent)
                    {
                        json = "info = {\"state\": \"dispose\", \"HarmInflictedTxt\": \"" + bp.HarmInflictedTxt +
                                "\", \"HarmInflictedPts\": \"" + bp.HarmInflictedPts +
                                "\", \"HarmReceivedTxt\": \"" + bp.HarmReceivedTxt +
                                "\", \"HarmReceivedPts\": \"" + bp.HarmReceivedPts +
                                "\", \"MiddleMsgTitle\": \"" + bp.MiddleMsgTitle +
                                "\", \"MiddleMsgFooter\": \"" + bp.MiddleMsgFooter +
                                "\", \"hpPlayer\": \"" + bp.MyInfo.LifePoints +
                                "\", \"hpOpponent\": \"" + bp.OppInfo.LifePoints + "\"}";
                    }
                    else
                    {
                        json = "info = {\"state\": \"waitingM\"}";
                    }
                }
            }
            if (str[0] == "waitingD")
            {
                if (bp.HasBattleEnded)
                {
                    json = "info = {\"state\": \"finish\", \"MiddleMsgTitle\": \"" + bp.MiddleMsgTitle +
                            "\", \"MiddleMsgFooter\": \"" + bp.MiddleMsgFooter + "\"}";
                }
                else
                {
                    if (!bp.IsWaitingForOpponent)
                    {
                        json = "info = {\"state\": \"moving\", ";
                        json += "\"cardP1\": \"" + bp.MyCards.ElementAt(0).ImageUrl + "\",";
                        json += "\"cardP2\": \"" + bp.MyCards.ElementAt(1).ImageUrl + "\",";
                        json += "\"cardP3\": \"" + bp.MyCards.ElementAt(2).ImageUrl + "\",";
                        json += "\"cardP4\": \"" + bp.MyCards.ElementAt(3).ImageUrl + "\",";
                        json += "\"cardO1\": \"" + bp.OppCards.ElementAt(0).ImageUrl + "\",";
                        json += "\"cardO2\": \"" + bp.OppCards.ElementAt(1).ImageUrl + "\",";
                        json += "\"cardO3\": \"" + bp.OppCards.ElementAt(2).ImageUrl + "\",";
                        json += "\"cardO4\": \"" + bp.OppCards.ElementAt(3).ImageUrl + "\"}";
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
                    bp.MyCards.ElementAt(i).Selected = false;
                    if (s[i] == 1)
                    {
                        bp.MyCards.ElementAt(i).Selected = true;
                    }
                }
                bp.ConfirmButtonPressed();
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
                    bp.LogoutButton.LogoutButtonClicked();
                    Response.Redirect("Default.aspx");
                };
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lbOpponent.Text = bp.OppInfo.Username; //bppo.Username;
            hpTxtO.Text = bp.OppInfo.LifePointsTxt; //bppo.LifePointsTxt;
            hpPointsO.Text = Convert.ToString(bp.OppInfo.LifePoints); //bppo.LifePoints);
            Image[] fieldO = new Image[4]{card1O, card2O, card3O, card4O};
            Image[] fieldP = new Image[4]{card1P, card2P, card3P, card4P};
            int i = 0;
            foreach (IBattleCardPresentation ibcp in bp.OppCards)
            {
                fieldO[i].ImageUrl = ibcp.ImageUrl;
                fieldO[i].Height = 110;
                fieldO[i].Width = 90;
                i++;
            }
            i = 0;
            foreach (IBattleCardPresentation ibcp in bp.MyCards)
            {
                fieldP[i].ImageUrl = ibcp.ImageUrl;
                fieldP[i].Height = 110;
                fieldP[i].Width = 90;
                i++;
            }
            msg.Text = bp.MiddleMsgTitle; //"Welcome to Death!";
            lbPlayer.Text = bp.MyInfo.Username; //bppp.Username;
            hpTxtP.Text = bp.MyInfo.LifePointsTxt; //bppp.LifePointsTxt;
            hpPointsP.Text = Convert.ToString(bp.MyInfo.LifePoints); //bppp.LifePoints);
            //confirm.Text = "Confirmar";
            //confirm.Enabled = bp.ConfirmBtnEnabled;
            btReturn.Text = "Salir";
            btReturn.Attributes.CssStyle.Add("visibility", "hidden");
            btLogout.Text = bp.LogoutButton.ButtonText;
        }
    }
}