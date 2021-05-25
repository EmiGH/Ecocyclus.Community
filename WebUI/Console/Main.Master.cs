using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Globalization;
using System.Threading;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace CSI.WebUI.Console
{
    public partial class Main : System.Web.UI.MasterPage
    {
        #region Properties

        private Library.Operation _Library
        {
            get
            {
                if (Session["Operation"] != null)
                    return (Library.Operation)Session["Operation"];

                try
                {
                    HttpCookie _authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                    if (_authCookie != null)
                    {
                        FormsAuthenticationTicket _ticket = FormsAuthentication.Decrypt(_authCookie.Value);
                        if (_ticket != null)
                        {
                            String[] _data = _ticket.UserData.Split('|');
                            String _user = _data[0].ToString();
                            String _password = _data[1].ToString();
                            String _language = _data[2].ToString();

                            CSI.Library.Operation _operation = new CSI.Library.Operation(_user, _password, _language, Request.ServerVariables["LOCAL_ADDR"]);

                            //Set session variables
                            Session["Operation"] = _operation;

                            return (Library.Operation)Session["Operation"];
                        }
                    }
                }
                catch (Exception)
                {
                    FormsAuthentication.RedirectToLoginPage("?Url=" + HttpUtility.UrlEncode(Request.Url.AbsolutePath));
                }

                return null;
            }
        }
        public Controls.ucErrorHandler ErrorHandler
        {
            get { return (Controls.ucErrorHandler)ehHandler; }
        }
        public Controls.ucMenuNavigation MenuNavigation
        {
            get { return (Controls.ucMenuNavigation)((Controls.ucMenuGlobal)mnGlobal).MenuNavigation; }
        }
        public Controls.ucMenuGlobal MenuGlobal
        {
            get { return (Controls.ucMenuGlobal)mnGlobal; }
        }
        public Controls.ucGuide Guide
        {
            get { return (Controls.ucGuide)gGuide; }
        }

        #endregion

        #region Lifecycle

        protected override void OnLoad(EventArgs e) { base.OnLoad(e); Page.Header.DataBind(); }

        protected void Page_Init(object sender, EventArgs e)
        {
            BindControls();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = Resources.Data.ProductTitle;
            SetMenu(); 
            if (!IsPostBack)
            {
                LoadTexts();
                
            }
        }

        #endregion

        #region Private Methods

        private void BindControls()
        {
            lnkSignOut.Click += new EventHandler(btnSignOut_Click);
        }
        private void SetMenu()
        {
            
            //lnkSites.ToolTip = lblSites.Text = Resources.Data.Sites;
            //if (lnkDashboard.PostBackUrl == "~" + Request.Url.AbsolutePath)
            //    lnkDashboard.CssClass = "active";
            //if (lnkSites.PostBackUrl == "~" + Request.Url.AbsolutePath)
            //    lnkSites.CssClass = "active";

            //Managers Options
            //if (_Library.CurrentUser is Library.Objects.Users.UserOperatorMeManager)
            //{
            //    lnkDashboard.ToolTip = lblDashboard.Text = Resources.Data.Dashboard;
            //    lnkDashboard.Visible = true;

            //    lnkAddSite.ToolTip = Resources.Data.SitesAdd;
            //    lblAddSite.Text = Resources.Data.SitesAdd;
            //    if (lnkAddSite.PostBackUrl == "~" + Request.Url.AbsolutePath)
            //        lnkAddSite.CssClass = "active";
                
            //    lnkCompanyUserAdd.Visible = true;
            //    lnkCompanyUserAdd.ToolTip = lnkCompanyUserAdd.Text = Resources.Data.UsersAdd;
            //}
            //else
            //{
            //    liDashboard.Visible = false;
            //    liAddSite.Visible = false;
            //    lnkDashboard.Visible = false;
            //    lnkAddSite.Visible = false;
            //    lnkCompanyUserAdd.Visible = false;

            //    lnkCompanyUserAdd.ToolTip = lnkCompanyUserAdd.Text = "";
            //}

            if (_Library.CurrentUser is Library.Objects.Users.UserOperatorMeManager)
            {
                lnkCompanyUserAdd.Visible = true;
                lnkCompanyUserAdd.ToolTip = lnkCompanyUserAdd.Text = Resources.Data.UsersAdd;
            }
            else
            {
                lnkCompanyUserAdd.Visible = false;
                lnkCompanyUserAdd.ToolTip = lnkCompanyUserAdd.Text = "";
            }
            MenuGlobal.Initialize(Console.Controls.ucMenuGlobal.MenuItem.None, Console.Controls.ucMenuGlobal.MenuItem.None, WebUI.Common.GetPermissionFromContext(_Library.CurrentUser, null));

        }
        private void LoadTexts()
        {
            Library.Objects.Users.UserOperatorMe _i = _Library.CurrentUser;
            CSI.Library.Objects.Companies.Company _company = _i.Company;

            lnkUser.Text = _i.Firstname;
            lnkUserChangePassword.Text = Resources.Data.ChangePassword;
            lnkUserProfile.Text = Resources.Data.Profile;
            lblUser.Text = _i.Fullname;
            lblUserEmail.Text = _i.Email;

            lnkCompany.Text = _company.Name;
            lnkCompanyProfile.Text = Resources.Data.Profile;
            lnkCompanyUserAdd.Text = Resources.Data.UsersAdd;
            lnkCompanyUrl.Text = lnkCompanyUrl.PostBackUrl = _company.Contact.Website;

            if (_i.Picture != null)
            {
                ibtUser.ImageUrl = WebUI.Common.GetPath(WebUI.Common.eFolders.Common, Request) + "ImageViewer.aspx?IdFile=" + _i.Picture.IdFile.ToString();

                Pair _size = WebUI.Common.GetImageSize(_i.Picture.Stream, 80);
                ibtUser.Style.Add("height", _size.Second.ToString() + "px");
                ibtUser.Style.Add("width", _size.First.ToString() + "px");
            }
            else
            {
                ibtUser.ImageUrl = WebUI.Common.GetPath(WebUI.Common.eFolders.Images, Request) + "img-users.png";

                ibtUser.Style.Add("height", "37px");
                ibtUser.Style.Add("width", "44px");
            }

            hplHelp.Text = hplHelp.ToolTip = Resources.Data.Help;
            lnkSignOut.Text = lnkSignOut.ToolTip = Resources.Data.SignOff;

            lblFooter.Text = Resources.Data.Footer;
            
        }

        #endregion

        #region Page Events

        void btnSignOut_Click(object sender, EventArgs e)
        {
            HttpCookie _authCookie = Request.Cookies["Credential"];
            if (_authCookie != null)
            {
                FormsAuthenticationTicket _ticket = FormsAuthentication.Decrypt(_authCookie.Value);
                if (_ticket != null)
                {
                    try
                    {
                        String[] _data = _ticket.UserData.Split('|');
                        String _user = _data[0].ToString();
                        String _password = _data[1].ToString();
                        String _language = _data[2].ToString();
                        Boolean _RememberMe = Boolean.Parse(_data[3].ToString());

                        HttpContext.Current.Response.Cookies.Remove(FormsAuthentication.FormsCookieName);

                        String _ticketValues = _user + "|" + _password + "|" + _language + "|" + _RememberMe + "|" + Boolean.FalseString;

                        HttpCookie _cookie = GenerateAuthenticationCookie(0, _user, _ticketValues);
                        HttpContext.Current.Response.Cookies.Add(_cookie);
                    }
                    catch (Exception) { }
                }
            }

            Session.Abandon();
            Response.Redirect(WebUI.Common.GetPath(WebUI.Common.eFolders.Root, Request) + "Default.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
        private HttpCookie GenerateAuthenticationCookie(int expiryInMinutes, String username, String userData)
        {
            DateTime _cookieExpiration = (expiryInMinutes > 0 ? DateTime.Now.AddMinutes(expiryInMinutes) : DateTime.MaxValue);

            var authenticationTicket =
                new FormsAuthenticationTicket(
                    1,
                    username,
                    DateTime.Now,
                    _cookieExpiration,
                    true,
                    userData,
                    FormsAuthentication.FormsCookiePath);

            // ticket must be encrypted
            string _encryptedTicket = FormsAuthentication.Encrypt(authenticationTicket);

            // create cookie to contain encrypted auth ticket
            var _authCookie = new HttpCookie("Credential", _encryptedTicket);
            _authCookie.Expires = authenticationTicket.Expiration;
            _authCookie.Path = FormsAuthentication.FormsCookiePath;
            _authCookie.HttpOnly = true;

            return _authCookie;
        }

        #endregion
    }
}