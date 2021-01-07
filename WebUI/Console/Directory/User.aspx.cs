using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSI.Library.Objects.Users;

namespace CSI.WebUI.Console.Directory
{
    public partial class User : BasePage
    {
        private UserOperatorCoworker _Operator;

        protected void Page_Init(object sender, EventArgs e)
        {
            BindControls();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            _Operator = (UserOperatorCoworker)I.GetOperator(Convert.ToInt64(Request.QueryString["Operator"].ToString()));
            SetMenu();
            if (!IsPostBack)
            {
                LoadTexts();
                LoadData();
            }
        }

        #region Private Methods

        private void BindControls()
        {
            rptPermissions.ItemDataBound += new RepeaterItemEventHandler(rptPermissions_ItemDataBound);
        }
        private void LoadTexts()
        {
            lblSiteProperties.Text = Resources.Data.Properties;
            lblPermissions.Text = Resources.Data.Permissions;

            lblIsActive.Text = Resources.Data.IsActive;
            lblDate.Text = Resources.Data.Date;
            lblName.Text = Resources.Data.Fullname;
            lblEmail.Text = Resources.Data.Email;
            lblIsManager.Text = Resources.Data.IsCompanyManager;

            lblPermissionsSite.Text = Resources.Data.Site;
            lblPermissionsDate.Text = Resources.Data.Date;
            lblPermissionsManage.Text = Resources.Data.Manage;
            lblPermissionsLoad.Text = Resources.Data.Operate;
            lblPermissionsView.Text = Resources.Data.View;

        }
        private void LoadData()
        {            
            Library.Objects.Auxiliaries.Files.File _picture = _Operator.Picture;

            imgPicture.Src = WebUI.Common.GetPath(WebUI.Common.eFolders.Common, Request) + "ImageViewer.aspx?IdFile=" + (_picture != null ? _picture.IdFile.ToString() : "-1");
            Pair _size;
            if (_picture != null)
                _size = WebUI.Common.GetImageSize(_picture.Stream, 150);
            else
                _size = WebUI.Common.GetDefaultImageSize(150);
            imgPicture.Style.Add("height", _size.Second.ToString() + "px");
            imgPicture.Style.Add("width", _size.First.ToString() + "px");

            lblNameValue.Text = _Operator.Fullname;
            lblEmailValue.Text = _Operator.Email;
            lblDateValue.Text = _Operator.Timestamp.ToShortDateString();
            lblIsManagerValue.Text = WebUI.Common.GetBooleanTranslation(_Operator.IsManager);
            lblIsActiveValue.Text = WebUI.Common.GetBooleanTranslation(_Operator.IsActive);
            
            //Permissions
            if (I is UserOperatorMeManager)
            {
                divPermissions.Style.Add("display", "block");
                LoadPermissions();
            }
        }
        private void LoadPermissions()
        {
            rptPermissions.DataSource = _Operator.GetPermissionsGranted().Values;
            rptPermissions.DataBind();
        }
        private void SetMenu()
        {
            if (I is UserOperatorMeManager)
            {
                String _idOperator = _Operator.IdOperator.ToString();

                lnkEditUser.Visible = true;
                lnkEditUser.ToolTip = Resources.Data.UserModify;
                lnkEditUser.PostBackUrl = WebUI.Common.GetPath(WebUI.Common.eFolders.Directory, Request) + "UserEdit.aspx?Operator=" + _idOperator;

                if (!_Operator.IsManager)
                {
                    lnkPermissions.Visible = true;
                    lnkPermissions.ToolTip = Resources.Data.Permissions;
                    lnkPermissions.PostBackUrl = WebUI.Common.GetPath(WebUI.Common.eFolders.Permissions, Request) + "UserPermissions.aspx?Operator=" + _idOperator;
                }
            }
            else
            {
                lnkEditUser.Visible = false;
                lnkPermissions.Visible = false;
            }
        }

        #endregion

        #region Page Events

        void rptPermissions_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CSI.Library.Objects.Sites.Permission _permission = (CSI.Library.Objects.Sites.Permission)e.Item.DataItem;

                Label _lbl = ((Label)e.Item.FindControl("lblPermissionSite"));
                _lbl.Text = _permission.Site.Title;

                _lbl = ((Label)e.Item.FindControl("lblPermissionDate"));
                _lbl.Text = _permission.Date.ToShortDateString();

                _lbl = ((Label)e.Item.FindControl("lblPermissionManage"));
                _lbl.Text = WebUI.Common.GetBooleanTranslation(_permission.Manage);

                _lbl = ((Label)e.Item.FindControl("lblPermissionLoad"));
                _lbl.Text = WebUI.Common.GetBooleanTranslation(_permission.Load);

                _lbl = ((Label)e.Item.FindControl("lblPermissionView"));
                _lbl.Text = WebUI.Common.GetBooleanTranslation(!_permission.Manage && !_permission.Load); ;

            }
        }


        #endregion
    }
}