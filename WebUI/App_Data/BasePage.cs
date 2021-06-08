using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Threading;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Resources;
using System.Reflection;
using CSI.Library.Objects.Users;

namespace CSI.WebUI
{
    public class BasePage : System.Web.UI.Page
    {
        internal BasePage() { }

        protected override void OnPreInit(EventArgs e)
        {
            TestOperation();

            CultureInfo ci = CurrentCultureInfo;
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            base.OnPreInit(e);
        }

        #region Menu Navigation Stuff

        
        #endregion

        #region Library Access Stuff

        internal void TestOperation()
        {
            if(System.Web.HttpContext.Current!=null)
                if(System.Web.HttpContext.Current.Session!=null)
                    if (MyLibrary != null)
                        return;

            FormsAuthentication.RedirectToLoginPage();
        }
        internal Library.Operation MyLibrary
        {
            get
            {
                if (Session["Operation"] != null)
                    return (Library.Operation)Session["Operation"];

                HttpCookie _authCookie = Request.Cookies["Credential"];
                if (_authCookie != null)
                {
                    FormsAuthenticationTicket _ticket = FormsAuthentication.Decrypt(_authCookie.Value);
                    if (_ticket != null)
                    {

                        String[] _data = _ticket.UserData.Split('|');
                        if (_data[0].ToString() != "")
                        {
                            if (Boolean.Parse(_data[4].ToString()))
                            {
                                //Keep me logged in
                                String _user = _data[0].ToString();
                                String _password = _data[1].ToString();
                                String _language = _data[2].ToString();

                                CSI.Library.Operation _operation = new CSI.Library.Operation(_user, _password, _language, Request.ServerVariables["LOCAL_ADDR"]);
                                Session["Operation"] = _operation;

                                return (Library.Operation)Session["Operation"];
                            }
                        }

                    }
                }
                
                return null;
            }
        }
        internal Library.Objects.Users.UserOperatorMe I
        { get { return MyLibrary != null ? MyLibrary.CurrentUser : null; } }
        internal CultureInfo CurrentCultureInfo
        {
            get
            {
                if (Session["CurrentLanguage"] == null)
                {
                    HttpCookie userCultureCookie = Request.Cookies["UserSelectedCulture"];
                    if (userCultureCookie != null)
                    {
                        Session["CurrentLanguage"] = userCultureCookie.Value;
                    }
                    else
                    {
                        Session["CurrentLanguage"] = MyLibrary.CurrentUser.Language.IdLanguage;
                    }
                }
                    
                return new CultureInfo(Convert.ToString(Session["CurrentLanguage"]));
            }
        }

        #endregion

        #region Confirmation Popup Script

        protected void AddConfirmRequest(WebControl control, string title, string message)
        {
            string postBackReference = Page.ClientScript.GetPostBackEventReference(control, String.Empty);
            string _click = (control is LinkButton ? "href" : "onClick");

            string function = String.Format("javascript:showConfirmRequest(function() {{ {0} }}, '{1}', '{2}'); return false;",
                                             postBackReference,
                                             title,
                                             message);
            control.Attributes.Add(_click, function);
        }

        #endregion
         
        #region Web Methods

        [WebMethod]
        public static String Helper(string control, string lang, string title)
        {
            CultureInfo ci = new CultureInfo(lang);
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            Assembly _assembly = global::System.Reflection.Assembly.Load("App_GlobalResources");
            ResourceManager _rmData = new ResourceManager("Resources.Data", _assembly);

            try
            {
                ResourceManager _rmHelper = new ResourceManager("Resources.Helper", _assembly);

                String _key = GetCurrentPageName().Replace('.','_') + "_" + control;
                String _helpContent = _rmHelper.GetString(_key); //new ResourceManager("Resources.Helper." + lang, _assembly).GetString(_key);
                
                if (_helpContent != null)
                {
                    if (_helpContent == "" && title.Contains(_rmData.GetString("HelpPostfix")))
                        return _rmData.GetString("HelpEmpty");
                }
                else
                {
                    if (title.Contains(_rmData.GetString("HelpPostfix")))
                        return _rmData.GetString("HelpEmpty");

                    _helpContent = "";
                }

                return _helpContent;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public static string GetCurrentPageName()
        {
            string sPath = HttpContext.Current.Request.Url.AbsolutePath;
            string[] strarry = sPath.Split('/');

            for (int i = 0; i < strarry.Length; i++)
            {
                if (strarry[i].Contains(".aspx"))
                {
                    return strarry[i];
                }
            }
            return "";
        }

        [WebMethod]
        public static List<string> GetAutoCompleteClients(string clientPattern)
        {
            List<string> result = new List<string>();
            UserOperatorMeManager i = (UserOperatorMeManager)((Library.Operation)HttpContext.Current.Session["Operation"]).CurrentUser;
            foreach (String client in i.SearchClients(clientPattern))
            {
                result.Add(client);
            }
            return result;

        }
        [WebMethod]
        public static List<string> GetAutoCompleteAgents(string agentPattern)
        {
            List<string> result = new List<string>();
            UserOperatorMeManager i = (UserOperatorMeManager)((Library.Operation)HttpContext.Current.Session["Operation"]).CurrentUser;
            foreach (String client in i.SearchAgents(agentPattern))
            {
                result.Add(client);
            }
            return result;

        }
        [WebMethod]
        public static List<string> GetAutoCompleteContractors(string contractorPattern)
        {
            List<string> result = new List<string>();
            UserOperatorMeManager i = (UserOperatorMeManager)((Library.Operation)HttpContext.Current.Session["Operation"]).CurrentUser;
            foreach (String client in i.SearchContractors(contractorPattern))
            {
                result.Add(client);
            }
            return result;

        }
        [WebMethod]
        public static List<string> GetAutoCompleteResponsible(string responsiblePattern)
        {
            List<string> result = new List<string>();
            UserOperatorMeManager i = (UserOperatorMeManager)((Library.Operation)HttpContext.Current.Session["Operation"]).CurrentUser;
            foreach (String client in i.SearchResponsible(responsiblePattern))
            {
                result.Add(client);
            }
            return result;

        }
        [WebMethod]
        public static List<string> GetAutoCompleteManagers(string managerPattern)
        {
            List<string> result = new List<string>();
            UserOperatorMeManager i = (UserOperatorMeManager)((Library.Operation)HttpContext.Current.Session["Operation"]).CurrentUser;
            foreach (String client in i.SearchManagers(managerPattern))
            {
                result.Add(client);
            }
            return result;

        }

        #endregion
    }
}