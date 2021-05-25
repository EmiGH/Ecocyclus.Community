using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSI.Library.Objects.Users;

namespace CSI.WebUI.Console.Dashboard.Permissions
{
    public partial class SitePermissions : BasePage
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
                if (ViewState["Location"] != null)
                    return ViewState["Location"].ToString();
                return "";
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            //Permissions
            if (!(I is UserOperatorMeManager) && !(_Site.CurrentPermission() == Library.Security.Authority.PermissionTypes.SiteManager))
            {
                throw new ApplicationException(Resources.Messages.AccessDenied);
            } 
            
            _Site = (Library.Objects.Sites.SiteMine)I.GetSite(Convert.ToInt64(Request.QueryString["Site"]));
            BindControls();
        }
        protected void Page_Load(object sender, EventArgs e)
        {           

            if (!IsPostBack)
            {
                SetMenu();
                LoadTexts();
                LoadSite();
                LoadManagers();
                LoadPermissions();
                LoadAvailableUsers();
            }
        }


        #region Private Methods

        private void BindControls()
        {
            rptPermissionsManagers.ItemDataBound += new RepeaterItemEventHandler(rptPermissionsManagers_ItemDataBound);
            rptPermissionsGranted.ItemDataBound += new RepeaterItemEventHandler(rptPermissionsGranted_ItemDataBound);
            rptPermissionsAvailable.ItemDataBound += new RepeaterItemEventHandler(rptPermissionsAvailable_ItemDataBound);
            
            AddConfirmRequest((WebControl)btnSave, Resources.Messages.ConfirmModifyTitle, Resources.Messages.ConfirmModifyMessage);
            btnSave.Click += new EventHandler(btnSave_Click);
        }
        private void SetMenu()
        {
            ((Main)Page.Master).MenuNavigation.Initialize(_Site.IdSite, _Site.Title, Console.Controls.ucMenuNavigation.MenuItem.Permissions, WebUI.Common.GetPermissionFromContext(I,_Site));
        }
        private void LoadTexts()
        {
            lblSiteProperties.Text = Resources.Data.Properties;

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

            lblPermissionsAvailable.Text = Resources.Data.PermissionsAvailable;
            lblPermissionsGranted.Text = Resources.Data.PermissionsGranted;
            lblPermissionsManagers.Text = Resources.Data.Managers;

            lblPermissionsManagersHeaderUser.Text = Resources.Data.Name;
            lblPermissionsManagersHeaderEmail.Text = Resources.Data.Email;
            lblPermissionsManagersHeaderIsManager.Text = Resources.Data.IsManager;
            lblPermissionsManagersHeaderIsOperator.Text = Resources.Data.IsOperator;
            lblPermissionsManagersHeaderIsReader.Text = Resources.Data.IsReader;
            lblPermissionsManagersHeaderNoAccess.Text = Resources.Data.NoAccess;

            lblPermissionsGrantedHeaderUser.Text = Resources.Data.Name;
            lblPermissionsGrantedHeaderEmail.Text = Resources.Data.Email;
            lblPermissionsGrantedHeaderIsManager.Text = Resources.Data.IsManager;
            lblPermissionsGrantedHeaderIsOperator.Text = Resources.Data.IsOperator;
            lblPermissionsGrantedHeaderIsReader.Text = Resources.Data.IsReader;
            lblPermissionsGrantedHeaderNoAccess.Text = Resources.Data.NoAccess;
            
            lblPermissionsAvailableHeaderUser.Text = Resources.Data.Name;
            lblPermissionsAvailableHeaderEmail.Text = Resources.Data.Email;
            lblPermissionsAvailableHeaderIsManager.Text = Resources.Data.IsManager;
            lblPermissionsAvailableHeaderIsOperator.Text = Resources.Data.IsOperator;
            lblPermissionsAvailableHeaderIsReader.Text = Resources.Data.IsReader;
            lblPermissionsAvailableHeaderNoAccess.Text = Resources.Data.NoAccess;

            btnSave.Text = btnSave.ToolTip = Resources.Data.Save;
          
        }
        private void LoadSite()
        {
            Library.Objects.Auxiliaries.Geographic.Contact _contact = _Site.Contact;
            Location = _contact.Location.Position.Coordenates;

            lblTitleValue.Text = _Site.Title;
            lblLoadStatusValue.Text = (_Site is Library.Objects.Sites.SiteMineOpen ? Resources.Data.SiteOpened : Resources.Data.SiteClosed);
            lblLiveStatusValue.Text = (_Site.IsFinished ? Resources.Data.SiteFinished : Resources.Data.SiteLive);
            lblLocationValue.Text = _contact.Location.Address;
            lblStartValue.Text = _Site.Start.ToShortDateString();
            lblWeeksValue.Text = _Site.Weeks.ToString();
            lblNumberValue.Text = _Site.Number;
            lblValueValue.Text = _Site.Currency.Symbol + " " + _Site.Value.ToString() + " [" + _Site.Currency.Name + "]";
            lblFloorSpaceValue.Text = _Site.FloorSpace.ToString();
            lblUnitsValue.Text = _Site.Units.ToString();

        }
        private void LoadPermissions()
        {
            rptPermissionsGranted.DataSource = _Site.GetPermissionsGranted().Values;
            rptPermissionsGranted.DataBind();
        }
        private void LoadAvailableUsers()
        {
            rptPermissionsAvailable.DataSource = _Site.GetPermissionsNotGranted().Values;
            rptPermissionsAvailable.DataBind();
        }
        private void LoadManagers()
        {
            rptPermissionsManagers.DataSource = _Site.GetManagers().Values;
            rptPermissionsManagers.DataBind();
        }

        #endregion

        #region Events
        
        void rptPermissionsManagers_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Library.Objects.Users.UserOperatorCoworker _operator = (Library.Objects.Users.UserOperatorCoworker)e.Item.DataItem;

                Label _lbl = ((Label)e.Item.FindControl("lblPermissionsManagersUser"));
                _lbl.Text = _operator.Fullname;
                _lbl = ((Label)e.Item.FindControl("lblPermissionsManagersEmail"));
                _lbl.Text = _operator.Email;

                RadioButton _rdb = ((RadioButton)e.Item.FindControl("rdPermissionsManagersIsManager"));
                _rdb.GroupName = _operator.IdOperator.ToString();
                _rdb.Checked = true;
                _rdb.Enabled = false;

                _rdb = ((RadioButton)e.Item.FindControl("rdPermissionsManagersIsOperator"));
                _rdb.GroupName = _operator.IdOperator.ToString();
                _rdb.Checked = false; 
                _rdb.Enabled = false;

                _rdb = ((RadioButton)e.Item.FindControl("rdPermissionsManagersIsReader"));
                _rdb.GroupName = _operator.IdOperator.ToString();
                _rdb.Checked = false; 
                _rdb.Enabled = false;

                _rdb = ((RadioButton)e.Item.FindControl("rdPermissionsManagersNoAccess"));
                _rdb.GroupName = _operator.IdOperator.ToString();
                _rdb.Checked = false;
                _rdb.Enabled = false;

            }
        }
        void rptPermissionsGranted_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Library.Objects.Sites.Permission _permission = (Library.Objects.Sites.Permission)e.Item.DataItem;
                Library.Objects.Users.UserOperatorCoworker _operator = (Library.Objects.Users.UserOperatorCoworker)_permission.Operator;

                HiddenField _hdn = ((HiddenField)e.Item.FindControl("hdnPermissionsGrantedIdOperator"));
                _hdn.Value = _operator.IdOperator.ToString();
                Label _lbl = ((Label)e.Item.FindControl("lblPermissionsGrantedUser"));
                _lbl.Text = _operator.Fullname;
                _lbl = ((Label)e.Item.FindControl("lblPermissionsGrantedEmail"));
                _lbl.Text = _operator.Email;

                RadioButton _rdb = ((RadioButton)e.Item.FindControl("rdPermissionsGrantedIsManager"));
                _rdb.GroupName = _operator.IdOperator.ToString();
                _rdb.Checked = _permission.Manage;
                _rdb.Enabled = !_operator.IsManager;

                _rdb = ((RadioButton)e.Item.FindControl("rdPermissionsGrantedIsOperator"));
                _rdb.GroupName = _operator.IdOperator.ToString();
                _rdb.Checked = _permission.Load;
                _rdb.Enabled = !_operator.IsManager;

                _rdb = ((RadioButton)e.Item.FindControl("rdPermissionsGrantedIsReader"));
                _rdb.GroupName = _operator.IdOperator.ToString();
                _rdb.Checked = !_permission.Manage && !_permission.Load;
                _rdb.Enabled = !_operator.IsManager;

                _rdb = ((RadioButton)e.Item.FindControl("rdPermissionsGrantedNoAccess"));
                _rdb.GroupName = _operator.IdOperator.ToString();
                _rdb.Checked = false;
                _rdb.Enabled = !_operator.IsManager;

            }
        }
        void rptPermissionsAvailable_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Library.Objects.Users.UserOperatorCoworker _operator = (Library.Objects.Users.UserOperatorCoworker)e.Item.DataItem;

                HiddenField _hdn = ((HiddenField)e.Item.FindControl("hdnPermissionsAvailableIdOperator"));
                _hdn.Value = _operator.IdOperator.ToString();
                Label _lbl = ((Label)e.Item.FindControl("lblPermissionsAvailableUser"));
                _lbl.Text = _operator.Fullname;
                _lbl = ((Label)e.Item.FindControl("lblPermissionsAvailableEmail"));
                _lbl.Text = _operator.Email;

                RadioButton _rdb = ((RadioButton)e.Item.FindControl("rdPermissionsAvailableIsManager"));
                _rdb.GroupName = _operator.IdOperator.ToString();
                _rdb.Checked = false;
                _rdb.Enabled = !_operator.IsManager;

                _rdb = ((RadioButton)e.Item.FindControl("rdPermissionsAvailableIsOperator"));
                _rdb.GroupName = _operator.IdOperator.ToString();
                _rdb.Checked = false;
                _rdb.Enabled = !_operator.IsManager;

                _rdb = ((RadioButton)e.Item.FindControl("rdPermissionsAvailableIsReader"));
                _rdb.GroupName = _operator.IdOperator.ToString();
                _rdb.Checked = false;
                _rdb.Enabled = !_operator.IsManager;

                _rdb = ((RadioButton)e.Item.FindControl("rdPermissionsAvailableIsReader"));
                _rdb.GroupName = _operator.IdOperator.ToString();
                _rdb.Checked = true;
                _rdb.Enabled = !_operator.IsManager;

            }
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            Library.Objects.Sites.Permission.PermissionsStructure _structure = Library.Objects.Sites.Permission.GetStructure();

            //Iterate through existing permissions
            foreach (RepeaterItem _item in rptPermissionsGranted.Items)
            {
                if (_item.ItemType == ListItemType.AlternatingItem || _item.ItemType == ListItemType.Item)
                {
                    Int64 _idOperator = Convert.ToInt64(((HiddenField)_item.FindControl("hdnPermissionsGrantedIdOperator")).Value);
                    Library.Objects.Users.UserOperatorCoworker _coworker = (Library.Objects.Users.UserOperatorCoworker)I.GetOperator(_idOperator);

                    RadioButton _rdb = ((RadioButton)_item.FindControl("rdPermissionsGrantedNoAccess"));
                    if (!_rdb.Checked)
                    {
                        if (((RadioButton)_item.FindControl("rdPermissionsGrantedIsManager")).Checked)
                        {
                            _structure.AddPermission(_coworker.IdOperator, true, false);
                        }
                        else
                        {
                            if (((RadioButton)_item.FindControl("rdPermissionsGrantedIsOperator")).Checked)
                            {
                                _structure.AddPermission(_coworker.IdOperator, false, true);
                            }
                            else
                                _structure.AddPermission(_coworker.IdOperator, false, false);
                        }
                    }
                }
            }

            //Iterate through new permissions
            foreach (RepeaterItem _item in rptPermissionsAvailable.Items)
            {
                if (_item.ItemType == ListItemType.AlternatingItem || _item.ItemType == ListItemType.Item)
                {
                    RadioButton _rdb = ((RadioButton)_item.FindControl("rdPermissionsAvailableNoAccess"));
                    if (!_rdb.Checked)
                    {
                        Int64 _idOperator = Convert.ToInt64(((HiddenField)_item.FindControl("hdnPermissionsAvailableIdOperator")).Value);
                        Library.Objects.Users.UserOperatorCoworker _coworker = (Library.Objects.Users.UserOperatorCoworker)I.GetOperator(_idOperator);

                        if (((RadioButton)_item.FindControl("rdPermissionsAvailableIsManager")).Checked)
                        {
                            _structure.AddPermission(_coworker.IdOperator, true, false);
                        }
                        else
                        {
                            if (((RadioButton)_item.FindControl("rdPermissionsAvailableIsOperator")).Checked)
                            {
                                _structure.AddPermission(_coworker.IdOperator, false, true);
                            }
                            else
                                _structure.AddPermission(_coworker.IdOperator, false, false);
                        }
                    }
                }
            }

            try
            {
                ((UserOperatorMeManager)I).AddPermissions(_Site, _structure);

                Response.Redirect(WebUI.Common.GetPath(WebUI.Common.eFolders.Dashboard, Request) + "Site.aspx?Site=" + _Site.IdSite.ToString(), false);
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