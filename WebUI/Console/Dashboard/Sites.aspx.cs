using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using CSI.Library.Objects.Users;

namespace CSI.WebUI.Console.Dashboard
{
    public partial class Sites : BasePage
    {
        public String Locations
        {
            set
            {
                ViewState["Locations"] = value.Replace(',', '.');
            }
            get
            {
                if(ViewState["Locations"]!=null)
                    return ViewState["Locations"].ToString();
                return "";
            }
        }
        private Boolean _CanManage;
   
        protected void Page_Init(object sender, EventArgs e)
        {
            _CanManage = (I is UserOperatorMeManager);

            //If no sites then show guide
            if (I.GetSites().Count == 0)
            {
                ((Main)Page.Master).Guide.SetMessage(Resources.Messages.FirstTimeTitle.Replace("[user]", I.Firstname), Resources.Messages.FirstTimeGuideApp, 100, 100);
                ((Main)Page.Master).MenuGlobal.Blink(Console.Controls.ucMenuGlobal.MenuItem.AddSite);
            }

            BindControls();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                SetMenu();
                LoadTexts();
                LoadSites();
                
            }
        }

        #region Private Methods

        private void SetMenu()
        {
        }
        private void BindControls()
        {
            rptSites.ItemDataBound += new RepeaterItemEventHandler(rptSites_ItemDataBound);
            rptSites.ItemCommand += new RepeaterCommandEventHandler(rptSites_ItemCommand);
            
        }
        private void LoadTexts()
        {
            lblSites.Text = Resources.Data.Sites;
            lblMap.Text = Resources.Data.Map;

            lblSitesHeaderStatus.Text = Resources.Data.Status;
            lblSitesHeaderAddress.Text = Resources.Data.Address;
            lblSitesHeaderTitle.Text = Resources.Data.Title;
            lblSiteHeaderType.Text = Resources.Data.SiteType;

        }
        private void LoadSites()
        {
            rptSites.DataSource = I.GetSites().Values;
            rptSites.DataBind();
        }

        #endregion
        
        #region Events

        void rptSites_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //Data Items
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                CSI.Library.Objects.Sites.SiteMine _site = (CSI.Library.Objects.Sites.SiteMine)e.Item.DataItem;

                String _IdSite = _site.IdSite.ToString();
                
                Button btn = ((Button)e.Item.FindControl("btnSitesView"));
                btn.CommandArgument = _IdSite;
                btn.ToolTip = Resources.Data.View;

                Button btnEdit = ((Button)e.Item.FindControl("btnSitesEdit"));
                Button btnDelete = ((Button)e.Item.FindControl("btnSitesDelete"));
                Image _img = ((Image)e.Item.FindControl("imgSitesLoadStatus"));
                if (_CanManage)
                {
                    if (_site is CSI.Library.Objects.Sites.SiteMineOpen)
                    {
                        btnEdit.CommandArgument = _IdSite;
                        btnEdit.ToolTip = Resources.Data.Edit;

                        btnDelete.CommandArgument = _IdSite;
                        btnDelete.ToolTip = Resources.Data.Delete;
                        AddConfirmRequest((WebControl)btnDelete, Resources.Messages.ConfirmDeleteTitle, Resources.Messages.ConfirmDeleteMessage);

                        _img.ImageUrl = WebUI.Common.GetPath(WebUI.Common.eFolders.Images, Request) + "SiteOpen.png";
                        _img.ToolTip = Resources.Data.SiteOpened;
                    }
                    else
                    {
                        btnEdit.Visible = btnDelete.Visible = false;
                        _img.ImageUrl = WebUI.Common.GetPath(WebUI.Common.eFolders.Images, Request) + "SiteClosed.png";
                        _img.ToolTip = Resources.Data.SiteClosed;
                    }

                }
                else
                {
                    btnEdit.Visible = btnDelete.Visible = false;
                    _img.ImageUrl = WebUI.Common.GetPath(WebUI.Common.eFolders.Images, Request) + "SiteClosed.png";
                    _img.ToolTip = Resources.Data.SiteClosed;
                }

                _img = ((Image)e.Item.FindControl("imgSitesLiveStatus"));
                _img.ToolTip = (_site.IsFinished ? Resources.Data.SiteFinished : Resources.Data.SiteLive);
                _img.ImageUrl = (_site.IsFinished ? WebUI.Common.GetPath(WebUI.Common.eFolders.Images, Request) + "SiteFinished.png" : WebUI.Common.GetPath(WebUI.Common.eFolders.Images, Request) + "SiteLive.png");

                Label _lbl = ((Label)e.Item.FindControl("lblSitesTitle"));
                _lbl.Text = _site.Title;

                _lbl = ((Label)e.Item.FindControl("lblSitesType"));
                _lbl.Text = _site.Type.Name;

                _lbl = ((Label)e.Item.FindControl("lblSitesAddress"));
                _lbl.Text = _site.Contact.Location.Address;
                
                //Armo el String para el map
                if(!String.IsNullOrEmpty(Locations))
                    Locations += "|";

                CSI.Library.Objects.Auxiliaries.Geographic.Location _location = _site.Contact.Location;
                Locations += _location.Position.Coordenates + ";<b>" + _site.Title + "</b><br/>" + _site.Description + "<br/><br/>" + _location.Address;

            }

        }
        void rptSites_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "View":
                    Response.Redirect(WebUI.Common.GetPath(WebUI.Common.eFolders.Dashboard, Request) + "Site.aspx?Site=" + e.CommandArgument);
                    break;
                case "Edit":
                    Response.Redirect(WebUI.Common.GetPath(WebUI.Common.eFolders.Dashboard, Request) + "SiteEdit.aspx?Site=" + e.CommandArgument);
                    break;
                case "Delete":
                    try
                    {
                        ((UserOperatorMeManager)I).RemoveSite(((Library.Objects.Sites.SiteMine)I.GetSite(Convert.ToInt64(e.CommandArgument))));
                        LoadSites();
                    }
                    catch (Exception ex)
                    { 
                        String _error = Resources.Messages.StandardError;
                        _error = _error.Replace("[error]", ex.Message);
                        _error = _error.Replace("[mail]", Resources.Data.HelpDeskMailAddress);

                        ((Main)Page.Master).ErrorHandler.SetMessage(Resources.Data.Error, _error);
                    
                    }
                    break;

                default:
                    break;
            }

        }
        
        #endregion
    }
}