using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Presentation;
using DependencyLocation;
using Game.Core;
namespace SoftArch
{
    public partial class Profile : System.Web.UI.Page
    {

        IHomePresentation hp;
        //StatisticsPresentation sp = new StatisticsPresentation("admin",100,70);
        ILoginPresentation ilp;

        private void Page_Init(object sender, EventArgs e)
        {
            try
            {
                hp = Dependency.Locator.Create<IHomePresentation>(Session.SessionID);
                ilp = Dependency.Locator.Create<ILoginPresentation>(Session.SessionID);
            }
            catch (Game.Logic.SecurityException)
            {
                Response.Redirect("Default.aspx");
                return;
            }


            if (ilp.IsAuthenticated)
            {
                btEnter.Click +=
                    (s, ea) =>
                    {
                        hp.SelectedRoomIndex = ltbRooms.SelectedIndex;
                        hp.IngresarClicked();
                        while (!hp.HasJoinedRoom) { }
                        Response.Redirect("Room.aspx");
                    };
                btLogout.Click +=
                    (s, ea) =>
                    {
                        hp.LogoutButton.LogoutButtonClicked();
                        Response.Redirect("Default.aspx");
                    };
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lbTitle.Text = hp.Title;
            lbHello.Text = hp.WelcomeMessage;
            lbUser.Text = hp.UserStats.Username; //sp.Username;
            //ltbRooms.Items.Add(new ListItem("hola"));
            ltbRooms.Rows = 10;
            foreach (string str in hp.RoomNames)
            {
                ltbRooms.Items.Add(new ListItem(str));
            }
            btEnter.Text = "Ingresar";
            btLogout.Text = hp.LogoutButton.ButtonText;
            lbGames.Text = hp.UserStats.PlayedGamesLabelText; //sp.PlayedGamesLabelText;
            lbGCount.Text = Convert.ToString(hp.UserStats.PlayedGamesCount); //sp.PlayedGamesCount);
            lbWon.Text = hp.UserStats.WonGamesLabelText; //sp.WonGamesLabelText;
            lbWCount.Text = Convert.ToString(hp.UserStats.WonGamesCount); //sp.WonGamesCount);
            lbLost.Text = hp.UserStats.LostGamesLabelText; //sp.LostGamesLabelText;
            lbLCount.Text = Convert.ToString(hp.UserStats.LostGamesCount); //sp.LostGamesCount);
            lbMessage.Text = hp.Message;
            lbMessage.Visible = hp.MessageVisible;
        }
    }
}