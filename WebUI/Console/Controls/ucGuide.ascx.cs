using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CSI.WebUI.Console.Controls
{
    public partial class ucGuide : System.Web.UI.UserControl
    {
        public void SetMessage(String title, String message, Double top, Double left)
        {
            divGuide.Style.Add("top", top.ToString());
            divGuide.Style.Add("left", left.ToString());

            hdnMessageGuideTitle.Value = title;
            hdnMessageGuide.Value = message;
        }
    }
}