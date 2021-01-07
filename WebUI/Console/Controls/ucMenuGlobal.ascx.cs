using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CSI.WebUI.Console.Controls
{
    public partial class ucMenuGlobal : System.Web.UI.UserControl
    {
        #region Control Properties

        public enum MenuItem
        { Dashboard, Sites, AddSite, None }
        
        private String _BlinkClassName="blink";
        private String _SelectedClassName = "selected";
        
        public Controls.ucMenuNavigation MenuNavigation
        {
            get { return (Controls.ucMenuNavigation)mnMenuNavigation; }
        }

        #endregion

        public void Initialize(MenuItem selected, MenuItem blink, WebUI.Common.PermissionType permission)
        {
            SetMenu(permission);
            EvaluateSelected(selected);
            EvaluateBlink(blink);

        }
        public void Blink(MenuItem menu)
        {
            EvaluateBlink(menu);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void SetMenu(WebUI.Common.PermissionType permission)
        {
            lnkMenuGlobalSites.ToolTip = lblMenuGlobalSites.Text = Resources.Data.Sites;
            if (lnkMenuGlobalDashboard.PostBackUrl == "~" + Request.Url.AbsolutePath)
                lnkMenuGlobalDashboard.CssClass = "active";
            if (lnkMenuGlobalSites.PostBackUrl == "~" + Request.Url.AbsolutePath)
                lnkMenuGlobalSites.CssClass = "active";

            //Managers Options
            if (permission == WebUI.Common.PermissionType.CompanyManager)
            {
                lnkMenuGlobalDashboard.ToolTip = lblMenuGlobalDashboard.Text = Resources.Data.Dashboard;
                lnkMenuGlobalDashboard.Visible = true;

                lnkMenuGlobalAddSite.ToolTip = Resources.Data.SitesAdd;
                lblMenuGlobalAddSite.Text = Resources.Data.SitesAdd;
                if (lnkMenuGlobalAddSite.PostBackUrl == "~" + Request.Url.AbsolutePath)
                    lnkMenuGlobalAddSite.CssClass = "active";

            }
            else
            {
                liMenuGlobalDashboard.Visible = false;
                liMenuGlobalAddSite.Visible = false;
                lnkMenuGlobalDashboard.Visible = false;
                lnkMenuGlobalAddSite.Visible = false;

            }
        }       
        private void EvaluateBlink(MenuItem blink)
        {
            switch (blink)
            {
                case MenuItem.Dashboard:
                    liMenuGlobalDashboard.Attributes.Add("class", _BlinkClassName);
                    break;
                case MenuItem.Sites:
                    liMenuGlobalSites.Attributes.Add("class", _BlinkClassName);
                    break;
                case MenuItem.AddSite:
                    liMenuGlobalAddSite.Attributes.Add("class", _BlinkClassName);
                    break;
                case MenuItem.None:
                    break;
                default:
                    break;
            }
        }
        private void EvaluateSelected(MenuItem selected)
        {
            switch (selected)
            {
                case MenuItem.Dashboard:
                    liMenuGlobalDashboard.Style.Add("class", _SelectedClassName);
                    break;
                case MenuItem.Sites:
                    liMenuGlobalSites.Style.Add("class", _SelectedClassName);
                    break;
                case MenuItem.AddSite:
                    liMenuGlobalAddSite.Style.Add("class", _SelectedClassName);
                    break;
                case MenuItem.None:
                    break;
                default:
                    break;
            }
        }


    }
}