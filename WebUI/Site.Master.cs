using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Threading;
using System.Web.Security;

namespace CSI.WebUI
{
    public partial class Site : System.Web.UI.MasterPage
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                LoadLanguages();
                BindEvents();
                Page.Title = Resources.Data.ProductTitle;

                if (!IsPostBack)
                {
                    LoadTexts();
                    LoadCreadentialCookie();
                    LoadLanguagesRegister();
                }
               
            }
            catch (Exception ex)
            {
                pnlLoginError.CssClass = "LoginErrorDiv";
                lblLoginError.CssClass = "LoginErrorLbl";
                Page.SetFocus(txtLoginPassword);

                String _error = Resources.Messages.StandardError;
                _error = _error.Replace("[error]", ex.Message);
                _error = _error.Replace("[mail]", Resources.Data.HelpDeskMailAddress);

                hdnMessageTitle.Value = Resources.Data.Error;
                hdnMessage.Value = _error;        
            }            
        }

        #region Private Methods

        private void BindEvents()
        {
            btnRegister.Click += new EventHandler(btnRegister_Click);
            btnLogin.Click += new EventHandler(btnLogin_Click);
        }
        private void LoadLanguages()
        {
            String _idLanguage = String.Empty;
            LinkButton _lnkLanguage = null;
            Label _lblSeparator = null;
            int x = 0;

            //Clear language list
            pnlLanguages.Controls.Clear();

            foreach (CSI.Library.Objects.Auxiliaries.Globalization.Language _language in Library.Operation.GetLanguagesAvailable().Values)
            {
                //Links for Languages
                _lblSeparator = new Label();
                _lblSeparator.Text = " - ";
                if (x > 0)
                    pnlLanguages.Controls.Add(_lblSeparator);

                _lnkLanguage = new LinkButton();
                _lnkLanguage.Text = _language.Name;
                _lnkLanguage.CommandArgument = _language.IdLanguage;
                _lnkLanguage.Click += new EventHandler(lnkLanguage_Click);
                _lnkLanguage.ID = "lnk" + _language.IdLanguage;

                //Check current language
                if (_language.IsDefault)
                {
                    if (Session["CurrentLanguage"] == null)
                    {
                        //Recuperamos la Cookie
                        HttpCookie userCultureCookie = Request.Cookies["UserSelectedCulture"];
                        if (userCultureCookie != null)
                        {
                            _idLanguage = userCultureCookie.Value;
                        }
                        else
                        {
                            _idLanguage = _language.IdLanguage;
                        }
                    }
                    else
                    {
                        _idLanguage = Session["CurrentLanguage"].ToString();
                    }
                    SetCultureLogin(_idLanguage);
                }

                pnlLanguages.Controls.Add(_lnkLanguage);
                x++;
            }

        }
        private void LoadLanguagesRegister()
        {
            foreach (CSI.Library.Objects.Auxiliaries.Globalization.Language _language in Library.Operation.GetLanguagesAvailable().Values)
            {
                ListItem _item = new ListItem(_language.Name, _language.IdLanguage);
                if(_language.IsDefault) _item.Selected = true;
                ddlRegisterLanguage.Items.Add(_item);
            }
        }
        private void LoadTexts()
        {
            //Login
            wmLoginEmail.WatermarkText = Resources.Data.Email;
            wmLoginPassword.WatermarkText = txtLoginPassword.ToolTip = Resources.Data.Password;
            btnLogin.Text = Resources.Data.Login;
            lblLoginKeepMeLoggedIn.Text = Resources.Data.LoginKeepMeLoggedIn;
            lblLoginRememberMe.Text = Resources.Data.RememberMe;
            hplForgotPassword.Text = Resources.Data.LoginForgotPassword;
                        
            //Registration
            lblMapLocationSelected.Text = Resources.Data.RegistrationLocation;
            lblRegistrationAgree.Text = Resources.Data.RegistrationAgree;

            //Cookie Policy
            lblCookiePolicyWarning.Text = Resources.Messages.CookieUseWarning;
            lnkCookieWarningGotoPage.Text = Resources.Data.CookieGotoPage;
            lnkCookieWarningHide.Text = Resources.Data.Agree;

            //Validators
            rfvRegisterCompany.Text = cuvRegisterLocation.Text = 
            rfvRegisterFirstname.Text = rfvRegisterLastname.Text  = 
            cvRegisterLanguage.Text = rfvRegisterEmail.Text = 
            rfvRegisterEmail2.Text = rfvRegisterPassword.Text = 
            revRegisterPassword.Text = rfvRegisterPassword2.Text = 
            cvRegisterPassword.Text = revRegisterEmail.Text = 
            cvRegisterEmail.Text = Resources.Messages.SummaryErrorCharacter;
            
            //Watermarks
            wmRegisterCompany.WatermarkText = txtRegisterCompany.ToolTip = Resources.Data.RegistrationCompanyName;
            wmRegisterFirstname.WatermarkText = txtRegisterFirstname.ToolTip = Resources.Data.Firstname;
            wmRegisterLastname.WatermarkText = txtRegisterLastname.ToolTip = Resources.Data.Lastname;
            wmRegisterPassword.WatermarkText = txtRegisterPassword.ToolTip = Resources.Data.Password;
            wmRegisterPassword2.WatermarkText = txtRegisterPassword2.ToolTip = Resources.Data.Password;
            wmRegisterEmail.WatermarkText = txtRegisterEmail.ToolTip = Resources.Data.RegistrationEmail;
            wmRegisterEmail2.WatermarkText = txtRegisterEmail2.ToolTip = Resources.Data.RegistrationEmailRetype;
                     
            //Button   
            btnRegister.Text = Resources.Data.Register;

            //Summary
            vsRegister.HeaderText = Resources.Messages.SummaryCheck;
            
            //Map
            lblMapLocator.Text = Resources.Data.Locator;
            btnMapConfirm.Text = Resources.Data.Ok;
            btnMapCancel.Text = Resources.Data.Cancel;

            //Footer

            lnkFacebook.PostBackUrl = Resources.Data.lnkFacebook;
            lnkTwitter.PostBackUrl = Resources.Data.lnkTwitter;
            linkedin.PostBackUrl = Resources.Data.linkedin;
            lnkEmail.PostBackUrl = Resources.Data.lnkEmail;

            lblCopyright.Text = Resources.Data.Copyright;
            
        }

        private void SetCultureLogin(String strCulture)
        {
            //Seteo la cultura 
            CultureInfo _culture = new CultureInfo(strCulture);
            //Seta la cultura Seleccionada
            Thread.CurrentThread.CurrentCulture = _culture;
            Thread.CurrentThread.CurrentUICulture = _culture;

            HttpCookie userCultureCookie = Request.Cookies["UserSelectedCulture"];
            if (userCultureCookie != null)
            {
                Request.Cookies.Remove("UserSelectedCulture");
            }
            // Creamos elemento HttpCookie con su nombre y su valor
            HttpCookie addCookie = new HttpCookie("UserSelectedCulture", strCulture);
            addCookie.Expires = DateTime.Now.AddMonths(1);
            // Y finalmente añadimos la cookie a nuestro usuario
            Response.Cookies.Add(addCookie);

            Session["CurrentLanguage"] = _culture;
        }

        #endregion

        #region Events

        void btnRegister_Click(object sender, EventArgs e)
        {

            if (Page.IsValid)
            {
                try
                {
                    if (hdnMapLocationPoint.Value == "")
                        CSI.Library.Operation.AccountRegister(txtRegisterCompany.Text, txtRegisterEmail.Text, txtRegisterPassword.Text, txtRegisterFirstname.Text, txtRegisterLastname.Text, ddlRegisterLanguage.SelectedValue);
                    else
                        CSI.Library.Operation.AccountRegister(txtRegisterCompany.Text, hdnMapLocationAddress.Value, new Library.Objects.Auxiliaries.Geographic.Position(hdnMapLocationPoint.Value), txtRegisterEmail.Text, txtRegisterPassword.Text, txtRegisterFirstname.Text, txtRegisterLastname.Text, ddlRegisterLanguage.SelectedValue);

                    hdnMessageTitle.Value = Resources.Data.Success;
                    hdnMessage.Value = Resources.Messages.AccountRegistrationSuccessful;

                }
                catch (Exception exception)
                {
                    String _error = Resources.Messages.AccountRegistrationError;
                    _error = _error.Replace("[error]", exception.Message);
                    _error = _error.Replace("[mail]", Resources.Data.HelpDeskMailAddress);

                    hdnMessageTitle.Value = Resources.Data.Error;
                    hdnMessage.Value = _error;
                    
                }
            }
            else
            {
                hdnMessageTitle.Value = Resources.Data.Information;
                hdnMessage.Value = Resources.Messages.SummaryCheck;

            }
        }
        void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                //Authenticate
                CSI.Library.Operation _operation = new CSI.Library.Operation(txtLoginEmail.Text, txtLoginPassword.Text, Convert.ToString(Session["CurrentLanguage"]), Request.ServerVariables["LOCAL_ADDR"]);
                //Set session variables
                Session["Operation"] = _operation;
                //Set Authentication Cookie
                FormsAuthentication.SetAuthCookie(txtLoginEmail.Text, true);
                
                //Set Credential Cookie
                //Clear out existing cookie for good measure (probably overkill) then add
                HttpContext.Current.Response.Cookies.Remove("Credential");
                if (chkLoginKeepMeLoggedIn.Checked || chkLoginRememberMe.Checked)
                {
                    String _ticketValues = txtLoginEmail.Text + "|" + txtLoginPassword.Text + "|" + Convert.ToString(Session["CurrentLanguage"]) + "|" + chkLoginRememberMe.Checked.ToString() + "|" + chkLoginKeepMeLoggedIn.Checked.ToString();

                    HttpCookie _cookie = GenerateAuthenticationCookie(0, txtLoginEmail.Text, _ticketValues);
                    HttpContext.Current.Response.Cookies.Add(_cookie);
                }

                RedirectToPage();                

            }
            catch (Exception ex)
            {
                pnlLoginError.CssClass = "LoginErrorDiv";
                lblLoginError.CssClass = "LoginErrorLbl";
                Page.SetFocus(txtLoginPassword);

                if (ex.Message == "AuthenticationFailed")
                {
                    hdnMessageTitle.Value = Resources.Data.Error;
                    hdnMessage.Value = Resources.Messages.LoginAuthenticationFail;
                }
                else
                {
                    hdnMessageTitle.Value = Resources.Data.Error;
                    hdnMessage.Value = Resources.Messages.LoginAuthenticationError;
                }
            }            
        }
        void lnkLanguage_Click(object sender, EventArgs e)
        {
            //Update Style
            foreach (CSI.Library.Objects.Auxiliaries.Globalization.Language _language in Library.Operation.GetLanguagesAvailable().Values)
            {
                LinkButton _lnk = (LinkButton)Page.FindControl("ctl00$lnk" + _language.IdLanguage);
                _lnk.CssClass = "";
            }
            ((LinkButton)sender).CssClass = "selected";

            //Set Culture
            String _cultureName = ((LinkButton)sender).CommandArgument;
            SetCultureLogin(_cultureName);

            //Reload Texts
            LoadTexts();
            

        }
        
        #endregion

        #region Cookie Features

        private void LoadCreadentialCookie()
        {
            HttpCookie _authCookie = Request.Cookies["Credential"];
            if (_authCookie != null)
            {
                FormsAuthenticationTicket _ticket = FormsAuthentication.Decrypt(_authCookie.Value);
                if (_ticket != null)
                {
                    String[] _data = _ticket.UserData.Split('|');
                    if (_data[0].ToString() != "")
                    {
                        if (Boolean.Parse(_data[3].ToString()))
                            LoadRememberMe(_data[0].ToString(),_data[1].ToString(),Boolean.Parse(_data[3].ToString()),Boolean.Parse(_data[4].ToString()));
                        
                        SetCultureLogin(_data[2].ToString());

                        if (Boolean.Parse(_data[4].ToString()))
                            TryLogin(_data[0].ToString(), _data[1].ToString(), _data[2].ToString());
                    }
                }
            }

        }
        private void LoadRememberMe(String username, String password, Boolean rememberMe , Boolean keepMeLoggedIn)
        {
            txtLoginEmail.Text = username;

            txtLoginPassword.Attributes.Add("value", password); 
            wmLoginPassword.Enabled = false;

            chkLoginRememberMe.Checked = rememberMe;
            chkLoginKeepMeLoggedIn.Checked = keepMeLoggedIn;
                            
        }
        private void TryLogin(String username, String password, String language)
        {
            CSI.Library.Operation _operation = new CSI.Library.Operation(username, password, language, Request.ServerVariables["LOCAL_ADDR"]);
            //Set session variables
            Session["Operation"] = _operation; 
            //Set Authentication Cookie
            FormsAuthentication.SetAuthCookie(username, true);

            RedirectToPage();

        }
        private HttpCookie GenerateAuthenticationCookie(int expiryInMinutes, String username, String userData)
        {
            DateTime _cookieExpiration = (expiryInMinutes>0?DateTime.Now.AddMinutes(expiryInMinutes):DateTime.MaxValue);
                        
            var authenticationTicket = new FormsAuthenticationTicket(1,username, DateTime.Now,_cookieExpiration, true, userData, FormsAuthentication.FormsCookiePath);
            
            // ticket must be encrypted
            string _encryptedTicket = FormsAuthentication.Encrypt(authenticationTicket);

            // create cookie to contain encrypted auth ticket
            var _authCookie = new HttpCookie("Credential", _encryptedTicket);
            _authCookie.Expires = authenticationTicket.Expiration;
            _authCookie.HttpOnly = true;

            return _authCookie;                         
        }
        private void RedirectToPage()
        {
          
            String _returnURL = "";

            if (Request.QueryString["ReturnURL"] != null)
                _returnURL = HttpUtility.UrlDecode(Request.QueryString["ReturnURL"].ToString());

            if (_returnURL != "" && _returnURL != "/")
                Response.Redirect(Request.QueryString["ReturnURL"].ToString());

            CSI.Library.Operation _operation = (CSI.Library.Operation)Session["Operation"];
            if (_operation != null)
            {
                if (_operation.CurrentUser is Library.Objects.Users.UserOperatorMeManager)
                {
                    Response.Redirect(WebUI.Common.GetPath(Common.eFolders.Dashboard, Request) + "Dashboard.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    Response.Redirect(WebUI.Common.GetPath(Common.eFolders.Dashboard, Request) + "Sites.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
        }

        #endregion
        
    }
}