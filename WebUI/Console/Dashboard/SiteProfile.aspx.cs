using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSI.Library.Objects.Users;

namespace CSI.WebUI.Console.Dashboard
{
    public partial class SiteProfile : BasePage
    {
        private Library.Objects.Sites.SiteMine _Site;
        public String Location
        {
            set
            {
                ViewState["Location"] = value.Replace(',', '.');
            }
            get
            {
                return ViewState["Location"].ToString();
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            BindControls();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            _Site = (Library.Objects.Sites.SiteMine)I.GetSite(Convert.ToInt64(Request.QueryString["Site"]));
            
            if (!IsPostBack)
            {
                SetMenu();
                LoadTexts();
                LoadSite();
            }

        }

        #region Private Methods

        private void BindControls()
        {
            lnkModifySite.Click += new EventHandler(lnkModifySite_Click);
            lnkToggleSite.Click += new EventHandler(lnkToggleSite_Click);

            AddConfirmRequest(lnkRemoveSite, Resources.Messages.ConfirmDeleteTitle, Resources.Messages.ConfirmDeleteMessage);
            lnkRemoveSite.Click += new EventHandler(lnkRemoveSite_Click);
        }

        private void SetMenu()
        {
            ((Main)Page.Master).MenuNavigation.Initialize(_Site.IdSite, _Site.Title, Console.Controls.ucMenuNavigation.MenuItem.Profile, WebUI.Common.GetPermissionFromContext(I,_Site));

            if (I is UserOperatorMeManager)
            {
                lnkToggleSite.Visible = true;
                if(_Site is Library.Objects.Sites.SiteMineOpen)
                    lnkToggleSite.ToolTip = Resources.Data.SiteClose;
                else
                    lnkToggleSite.ToolTip = Resources.Data.SiteOpen;
                lnkModifySite.Visible = true;
                lnkModifySite.ToolTip = Resources.Data.SiteModify;
                lnkModifySite.PostBackUrl = "SiteEdit.aspx?Site=" + _Site.IdSite.ToString();
                lnkRemoveSite.Visible = true;
                lnkRemoveSite.ToolTip = Resources.Data.SiteRemove;
            }
        }
        private void LoadTexts()
        {
            //Titles
            lblSiteProperties.Text = Resources.Data.Properties;
            lblSiteClient.Text = Resources.Data.Client;
            lblMap.Text = Resources.Data.Map;

            lblTitle.Text = Resources.Data.Title;
            lblType.Text = Resources.Data.SiteType;
            lblLoadStatus.Text = Resources.Data.SiteLoadStatus;
            lblLiveStatus.Text = Resources.Data.SiteLiveStatus;
            lblLocation.Text = Resources.Data.Location;
            lblStart.Text = Resources.Data.Start;
            lblWeeks.Text = Resources.Data.Weeks;
            lblNumber.Text = Resources.Data.Number;
            lblValue.Text = Resources.Data.Value;
            lblFloorSpace.Text = Resources.Data.FloorSpace;
            lblUnits.Text = Resources.Data.Units;
            lblClient.Text = Resources.Data.Client;
            lblAgent.Text = Resources.Data.Agent;
            lblContractor.Text = Resources.Data.Contractor;
            lblResponsible.Text = Resources.Data.Responsible;
            lblManager.Text = Resources.Data.Manager;
            lblTelephone.Text = Resources.Data.Telephone;
            lblEmail.Text = Resources.Data.Email;
            lblUrl.Text = Resources.Data.Url;
            lblFacebook.Text = Resources.Data.Facebook;
            lblTwitter.Text = Resources.Data.Twitter;
            lblDescription.Text = Resources.Data.Description;
            
        }
        private void LoadSite()
        {
            Library.Objects.Auxiliaries.Geographic.Contact _contact = _Site.Contact;
            Location = _contact.Location.Position.Coordenates;

            imgImage.Src = WebUI.Common.GetPath(WebUI.Common.eFolders.Common, Request) + "ImageViewer.aspx?IdFile=" + (_Site.Image != null ? _Site.Image.IdFile.ToString() : "-1");
            Pair _size;
            if (_Site.Image != null)
                _size = WebUI.Common.GetImageSize(_Site.Image.Stream, 150);
            else
                _size = WebUI.Common.GetDefaultImageSize(150);
            imgImage.Style.Add("height", _size.Second.ToString() + "px");
            imgImage.Style.Add("width", _size.First.ToString() + "px");

            lblTitleValue.Text = _Site.Title;
            lblTypeValue.Text = _Site.Type.Name;
            lblLocationValue.Text = _contact.Location.Address;
            lblLoadStatusValue.Text = (_Site is Library.Objects.Sites.SiteMineOpen ? Resources.Data.SiteOpened : Resources.Data.SiteClosed);
            lblLiveStatusValue.Text = (_Site.IsFinished ? Resources.Data.SiteFinished : Resources.Data.SiteLive);
            lblStartValue.Text = _Site.Start.ToShortDateString();
            lblWeeksValue.Text = _Site.Weeks.ToString();
            lblNumberValue.Text = _Site.Number;
            lblValueValue.Text = _Site.Currency.Symbol + " " + _Site.Value.ToString() + " [" + _Site.Currency.Name + "]";
            lblFloorSpaceValue.Text = _Site.FloorSpace.ToString();
            lblUnitsValue.Text = _Site.Units.ToString();
            lblClientValue.Text = _Site.Client;
            lblAgentValue.Text = _Site.Agent;
            lblContractorValue.Text = _Site.Contractor;
            lblResponsibleValue.Text = _Site.Responsible;
            lblManagerValue.Text = _Site.Manager;
            lblTelephoneValue.Text = _contact.Telephone;
            lblEmailValue.Text = _contact.Email;
            lblUrlValue.Text = _contact.Website;
            lblFacebookValue.Text = _contact.Facebook;
            lblTwitterValue.Text = _contact.Twitter;
            lblDescriptionValue.Text = _Site.Description;
            
        }
        
        #endregion

        #region Events
        
        void lnkModifySite_Click(object sender, EventArgs e)
        {
            Response.Redirect(WebUI.Common.GetPath(WebUI.Common.eFolders.Dashboard,Request) + "SiteEdit.aspx?Site=" + _Site.IdSite.ToString(), false);
            Context.ApplicationInstance.CompleteRequest();
        }
        void lnkToggleSite_Click(object sender, EventArgs e)
        {
            ((UserOperatorMeManager)I).ToggleSite(_Site);
        }
        void lnkRemoveSite_Click(object sender, EventArgs e)
        {
            try
            {
                ((UserOperatorMeManager)I).RemoveSite(_Site);
                Response.Redirect(WebUI.Common.GetPath(WebUI.Common.eFolders.Dashboard, Request) + "Sites.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exception)
            {
                String _error = Resources.Messages.StandardError;
                _error = _error.Replace("[error]", exception.Message);
                _error = _error.Replace("[mail]", Resources.Data.HelpDeskMailAddress);

                ((Main)Page.Master).ErrorHandler.SetMessage(Resources.Data.Error, _error);
            }
        }

        
        #endregion
    }
}