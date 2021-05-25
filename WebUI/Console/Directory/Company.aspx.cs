using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSI.Library.Objects.Companies;
using CSI.Library.Objects.Users;

namespace CSI.WebUI.Console.Directory
{
    public partial class Company : BasePage
    {
        private CompanyMine _Company;
        private String _Location;
        public String Location
        { get { return _Location.Replace(',', '.'); } }
        private Boolean _CanManage;

        protected void Page_Init(object sender, EventArgs e)
        {
            _CanManage = (I is UserOperatorMeManager);
            BindControls();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData();
            SetMenu();
            if (!IsPostBack)
            {
                LoadTexts();
                //Users
                if (_CanManage)
                    LoadUsers();
            }
        }

        #region Private Methods

        private void BindControls()
        {
            if (_CanManage)
            {
                divUsers.Style.Add("visibility", "block");
                rptUsers.ItemDataBound += new RepeaterItemEventHandler(rptUsers_ItemDataBound);
                rptUsers.ItemCommand += new RepeaterCommandEventHandler(rptUsers_ItemCommand);
            }
            else
                divUsers.Style.Add("visibility", "none");
        }
        private void LoadTexts()
        {
            lblSiteProperties.Text = Resources.Data.Properties;
            lblUsers.Text = Resources.Data.Users;
            lblLocation.Text = Resources.Data.Location;

            lblName.Text = Resources.Data.Name;
            lblUrl.Text = Resources.Data.Url;
            lblEmail.Text = Resources.Data.Email;
            lblTelephone.Text = Resources.Data.Telephone;
            lblAddress.Text = Resources.Data.Address;
            lblFacebook.Text = Resources.Data.Facebook;
            lblTwitter.Text = Resources.Data.Twitter;
            
            imgLogo.Alt = Resources.Data.Logo;

            lblHeaderFullname.Text = Resources.Data.Fullname;
            lblHeaderEmail.Text = Resources.Data.Email;
            lblHeaderIsManager.Text = Resources.Data.IsCompanyManager;
        }
        private void LoadData()
        {
            _Company = (CompanyMine)I.Company;
              
            CSI.Library.Objects.Auxiliaries.Geographic.Contact _contact = _Company.Contact;
            CSI.Library.Objects.Auxiliaries.Geographic.Position _position;

            if (_contact.Location != null)
            {
                _position = _contact.Location.Position;
                _Location = _position.Coordenates;
                lblAddressValue.Text = _contact.Location.Address;
            }
            else
            {
                _Location = "";
            }

            imgLogo.Src = WebUI.Common.GetPath(WebUI.Common.eFolders.Common, Request) + "ImageViewer.aspx?IdFile=" + (_Company.Logo != null ? _Company.Logo.IdFile.ToString() : "-1");
            Pair _size;
            if (_Company.Logo != null)
                _size = WebUI.Common.GetImageSize(_Company.Logo.Stream, 150);
            else
                _size = WebUI.Common.GetDefaultImageSize(150);
            imgLogo.Style.Add("height", _size.Second.ToString() + "px");
            imgLogo.Style.Add("width", _size.First.ToString() + "px");

            lblNameValue.Text = _Company.Name;
            
            lblUrlValue.Text = _contact.Website;
            lblEmailValue.Text = _contact.Email;
            lblFacebookValue.Text = _contact.Facebook;
            lblTwitterValue.Text = _contact.Twitter;
            lblTelephoneValue.Text = _contact.Telephone;
            
            
        }
        private void LoadUsers()
        {
            rptUsers.DataSource = _Company.Operators.Values;
            rptUsers.DataBind();
        }
        private void SetMenu()
        {
            if (I is UserOperatorMeManager)
            {
                lnkEditCompany.Visible = true;
                lnkEditCompany.ToolTip = Resources.Data.CompanyModify;
            }
            else 
            {
                lnkEditCompany.Visible = false;
            }
        }

        #endregion

        #region Page Events

        void rptUsers_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                
                CSI.Library.Objects.Users.UserOperatorCoworker _operator = (CSI.Library.Objects.Users.UserOperatorCoworker)e.Item.DataItem;

                String _idOperator = _operator.IdOperator.ToString();
                
                Button btn = ((Button)e.Item.FindControl("btnUserView"));
                btn.CommandArgument = _idOperator;
                btn.ToolTip = Resources.Data.View;
                
                btn = ((Button)e.Item.FindControl("btnUserEdit"));
                btn.CommandArgument = _idOperator;
                btn.ToolTip = Resources.Data.Edit;

                btn = ((Button)e.Item.FindControl("btnUserDelete"));
                
                if (_operator.IdUser != I.IdUser)
                {
                    btn.Visible = true;
                    btn.CommandArgument = _idOperator;
                    btn.ToolTip = Resources.Data.Delete;

                    AddConfirmRequest((WebControl)btn, Resources.Messages.ConfirmDeleteTitle, Resources.Messages.ConfirmDeleteMessage);
                }
                else
                {
                    btn.Visible = false;
                }
                
                Label _lbl = ((Label)e.Item.FindControl("lblFullname"));
                _lbl.Text = _operator.Firstname;

                _lbl = ((Label)e.Item.FindControl("lblEmail"));
                _lbl.Text = _operator.Email;

                _lbl = ((Label)e.Item.FindControl("lblIsManager"));
                _lbl.Text = WebUI.Common.GetBooleanTranslation(_operator.IsManager);
            } 
        }
        void rptUsers_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "View":
                    Response.Redirect(WebUI.Common.GetPath(WebUI.Common.eFolders.Directory, Request) + "User.aspx?Operator=" + e.CommandArgument, false);
                    Context.ApplicationInstance.CompleteRequest();
                    break;
                case "Edit":
                    Response.Redirect(WebUI.Common.GetPath(WebUI.Common.eFolders.Directory, Request) + "UserEdit.aspx?Operator=" + e.CommandArgument, false);
                    Context.ApplicationInstance.CompleteRequest();
                    break;
                case "Delete":
                    
                    try
                    {
                        ((UserOperatorMeManager)I).RemoveOperator((UserOperatorCoworker)I.GetOperator(Convert.ToInt64(e.CommandArgument)));
                    }
                    catch (Exception exception)
                    {
                        String _error = Resources.Messages.StandardError;
                        _error = _error.Replace("[error]", exception.Message);
                        _error = _error.Replace("[mail]", Resources.Data.HelpDeskMailAddress);

                        ((Main)Page.Master).ErrorHandler.SetMessage(Resources.Data.Error, _error);
                    }

                    LoadUsers();
                    break;
                default:
                    break;
            }
           
        }


        #endregion
    }
}