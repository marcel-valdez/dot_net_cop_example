using System;
using System.Web.UI;

namespace Game
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lbPTitle.Text = "GameNET";
        }
    }
}
