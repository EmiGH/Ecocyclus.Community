using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSI.Library.Objects.Users;

namespace CSI.WebUI.Console.Dashboard.Permissions
{
    public partial class UserPermissions : BasePage
    {
        private UserOperatorCoworker _Operator;
        
        protected void Page_Init(object sender, EventArgs e)
        {
            BindControls();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //Permissions
            if (!(I is UserOperatorMeManager))
            {
                throw new ApplicationException(Resources.Messages.AccessDenied);
            }

            _Operator = (UserOperatorCoworker)I.GetOperator(Convert.ToInt64(Request.QueryString["Operator"]));
            if (!IsPostBack)
            {
                LoadUser();
                LoadTexts();
                LoadPermissions();
                LoadAvailableUsers();
            }                
        }


        #region Private Methods

        private void BindControls()
        {
            rptPermissionsGranted.ItemDataBound += new RepeaterItemEventHandler(rptPermissionsGranted_ItemDataBound);
            rptPermissionsAvailable.ItemDataBound += new RepeaterItemEventHandler(rptPermissionsAvailable_ItemDataBound);

            btnSave.Click += new EventHandler(btnSave_Click);
        }
        private void LoadTexts()
        {
            lblSiteProperties.Text = Resources.Data.Properties;
            lblPermissionsAvailable.Text = Resources.Data.PermissionsAvailable;

            lblPermissionsGrantedHeaderSite.Text = Resources.Data.Site;
            lblPermissionsGrantedHeaderIsManager.Text = Resources.Data.IsManager;
            lblPermissionsGrantedHeaderIsOperator.Text = Resources.Data.IsOperator;
            lblPermissionsGrantedHeaderIsReader.Text = Resources.Data.IsReader;
            lblPermissionsGrantedHeaderNoAccess.Text = Resources.Data.NoAccess;

            lblPermissionsGranted.Text = Resources.Data.PermissionsGranted;

            lblPermissionsAvailableHeaderSite.Text = Resources.Data.Site;
            lblPermissionsAvailableHeaderIsManager.Text = Resources.Data.IsManager;
            lblPermissionsAvailableHeaderIsOperator.Text = Resources.Data.IsOperator;
            lblPermissionsAvailableHeaderIsReader.Text = Resources.Data.IsReader;
            lblPermissionsAvailableHeaderNoAccess.Text = Resources.Data.NoAccess;

            btnSave.Text = btnSave.ToolTip = Resources.Data.Save;

        }
        private void LoadUser()
        {
            Library.Objects.Auxiliaries.Files.File _picture = _Operator.Picture;

            imgLogo.Src = WebUI.Common.GetPath(WebUI.Common.eFolders.Common, Request) + "ImageViewer.aspx?IdFile=" + (_picture != null ? _picture.IdFile.ToString() : "-1");
            Pair _size;
            if (_picture != null)
                _size = WebUI.Common.GetImageSize(_picture.Stream, 150);
            else
                _size = WebUI.Common.GetDefaultImageSize(150);
            imgLogo.Style.Add("height", _size.Second.ToString() + "px");
            imgLogo.Style.Add("width", _size.First.ToString() + "px");

            lblDate.Text = Resources.Data.Date;
            lblName.Text = Resources.Data.Fullname;
            lblEmail.Text = Resources.Data.Email;
            lblIsManager.Text = Resources.Data.IsCompanyManager;
            
            
            lblNameValue.Text = _Operator.Fullname;
            lblEmailValue.Text = _Operator.Email;
            lblDateValue.Text = _Operator.Timestamp.ToShortDateString();
            lblIsManagerValue.Text = WebUI.Common.GetBooleanTranslation(_Operator.IsManager);
           
            
            if (_Operator.IsManager)
            {
                btnSave.Visible = false;
                btnSave.Enabled = false;
            }
            else
            {
                btnSave.Visible = true;
                btnSave.Enabled = true;
            }
        }
        private void LoadPermissions()
        {
            rptPermissionsGranted.DataSource = _Operator.GetPermissionsGranted().Values;
            rptPermissionsGranted.DataBind();
        }
        private void LoadAvailableUsers()
        {
            rptPermissionsAvailable.DataSource = _Operator.GetPermissionsNotGranted().Values;
            rptPermissionsAvailable.DataBind();
        }

        #endregion

        #region Events

        void rptPermissionsGranted_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {            
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Library.Objects.Sites.Permission _permission = (Library.Objects.Sites.Permission)e.Item.DataItem;
                Library.Objects.Sites.SiteMine _site = (Library.Objects.Sites.SiteMine)_permission.Site;

                HiddenField _hdn = ((HiddenField)e.Item.FindControl("hdnPermissionsGrantedIdSite"));
                _hdn.Value = _site.IdSite.ToString();
                
                Label _lbl = ((Label)e.Item.FindControl("lblPermissionsGrantedSite"));
                _lbl.Text = _site.Title;

                RadioButton _rdb = ((RadioButton)e.Item.FindControl("rdPermissionsGrantedIsManager"));
                _rdb.GroupName = _site.IdSite.ToString();
                _rdb.Checked = _permission.Manage;
                _rdb.Enabled = !_Operator.IsManager;
                _rdb = ((RadioButton)e.Item.FindControl("rdPermissionsGrantedIsOperator"));
                _rdb.GroupName = _site.IdSite.ToString();
                _rdb.Checked = _permission.Load;
                _rdb.Enabled = !_Operator.IsManager;
                _rdb = ((RadioButton)e.Item.FindControl("rdPermissionsGrantedIsReader"));
                _rdb.GroupName = _site.IdSite.ToString();
                _rdb.Checked = !_permission.Manage && !_permission.Load;
                _rdb.Enabled = !_Operator.IsManager;
                _rdb = ((RadioButton)e.Item.FindControl("rdPermissionsGrantedNoAccess"));
                _rdb.GroupName = _site.IdSite.ToString();
                _rdb.Checked = false;
                _rdb.Enabled = !_Operator.IsManager;

            }
        }
        void rptPermissionsAvailable_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Library.Objects.Sites.SiteMine _site = (Library.Objects.Sites.SiteMine)e.Item.DataItem;

                HiddenField _hdn = ((HiddenField)e.Item.FindControl("hdnPermissionsAvailableIdSite"));
                _hdn.Value = _site.IdSite.ToString();

                Label _lbl = ((Label)e.Item.FindControl("lblPermissionsAvailableSite"));
                _lbl.Text = _site.Title;

                RadioButton _rdb = ((RadioButton)e.Item.FindControl("rdPermissionsAvailableIsManager"));
                _rdb.GroupName = _site.IdSite.ToString();
                _rdb.Checked = false;
                _rdb.Enabled = !_Operator.IsManager;
                _rdb = ((RadioButton)e.Item.FindControl("rdPermissionsAvailableIsOperator"));
                _rdb.GroupName = _site.IdSite.ToString();
                _rdb.Checked = false;
                _rdb.Enabled = !_Operator.IsManager;
                _rdb = ((RadioButton)e.Item.FindControl("rdPermissionsAvailableIsReader"));
                _rdb.GroupName = _site.IdSite.ToString();
                _rdb.Checked = false;
                _rdb.Enabled = !_Operator.IsManager;
                _rdb = ((RadioButton)e.Item.FindControl("rdPermissionsAvailableIsReader"));
                _rdb.GroupName = _site.IdSite.ToString();
                _rdb.Checked = false;
                _rdb.Enabled = !_Operator.IsManager;
                _rdb = ((RadioButton)e.Item.FindControl("rdPermissionsAvailableNoAccess"));
                _rdb.GroupName = _site.IdSite.ToString();
                _rdb.Checked = true;
                _rdb.Enabled = !_Operator.IsManager;

            }
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            if (!_Operator.IsManager)
            {
                Library.Objects.Sites.Permission.PermissionsStructure _structure = Library.Objects.Sites.Permission.GetStructure();

                //Iterate through existing permissions
                foreach (RepeaterItem _item in rptPermissionsGranted.Items)
                {
                    if (_item.ItemType == ListItemType.AlternatingItem || _item.ItemType == ListItemType.Item)
                    {
                        RadioButton _rdb = ((RadioButton)_item.FindControl("rdPermissionsGrantedNoAccess"));
                        if (!_rdb.Checked)
                        {
                            HiddenField _hdnSite = ((HiddenField)_item.FindControl("hdnPermissionsGrantedIdSite"));
                            Library.Objects.Sites.SiteMine _site = (Library.Objects.Sites.SiteMine)I.GetSite(Convert.ToInt64(_hdnSite.Value));
                            
                            if (((RadioButton)_item.FindControl("rdPermissionsGrantedIsManager")).Checked)
                            {
                                _structure.AddPermission(_site.IdSite, true, false);
                            }
                            else
                            {
                                if (((RadioButton)_item.FindControl("rdPermissionsGrantedIsOperator")).Checked)
                                {
                                    _structure.AddPermission(_site.IdSite, false, true);
                                }
                                else
                                    _structure.AddPermission(_site.IdSite, false, false);
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
                            HiddenField _hdnSite = ((HiddenField)_item.FindControl("hdnPermissionsAvailableIdSite"));
                            Library.Objects.Sites.SiteMine _site = (Library.Objects.Sites.SiteMine)I.GetSite(Convert.ToInt64(_hdnSite.Value));
                            
                            if (((RadioButton)_item.FindControl("rdPermissionsAvailableIsManager")).Checked)
                            {
                                _structure.AddPermission(_site.IdSite, true, false);
                            }
                            else
                            {
                                if (((RadioButton)_item.FindControl("rdPermissionsAvailableIsOperator")).Checked)
                                {
                                    _structure.AddPermission(_site.IdSite, false, true);
                                }
                                else
                                    _structure.AddPermission(_site.IdSite, false, false);
                            }
                        }
                    }
                }

                try
                {
                    ((UserOperatorMeManager)I).AddPermissions(_Operator, _structure);

                    Response.Redirect(WebUI.Common.GetPath(WebUI.Common.eFolders.Directory, Request) + "User.aspx?Operator=" + _Operator.IdOperator.ToString(), false);
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
        }



        #endregion
    }
}