using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;        
using System.Net.Mail;
using System.Resources;
using System.Reflection;

namespace WebCorporative
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadTexts();
        }

        private void LoadTexts()
        {
            wmEmail.WatermarkText = Resources.Data.Email;
            wmName.WatermarkText = Resources.Data.Fullname;
            wmOrganization.WatermarkText = Resources.Data.Organization;
            wmPhone.WatermarkText = Resources.Data.Telephone;
            wmMessage.WatermarkText = Resources.Data.Comments;

            btnSubmit.Text = Resources.Data.Submit;
        }

        [WebMethod]
        public static String Contact(String from, String name, String organisation, String telephone, String message)
        {
            try
            {
                Assembly _assembly = global::System.Reflection.Assembly.Load("App_GlobalResources");

                SmtpClient _client = new SmtpClient();

                String _body = "Name: " + name + "[n]";
                _body += "Email: " + from + "[n]";
                _body += "Company: " + organisation + "[n]";
                _body += "Telephone: " + telephone + "[n]";
                _body += "Message: " + message + "[n]";

                MailMessage _mail = new MailMessage(from, "info@siteimpacts.com");
                System.Net.Mime.ContentType _mimeType;
                AlternateView _alternate;

                _mimeType = new System.Net.Mime.ContentType("text/plain");
                _alternate = AlternateView.CreateAlternateViewFromString(_body.Replace("[n]", "\n"), _mimeType);
                _mail.AlternateViews.Add(_alternate);

                _mimeType = new System.Net.Mime.ContentType("text/html");
                _alternate = AlternateView.CreateAlternateViewFromString(_body.Replace("[n]", "<br />"), _mimeType);
                _mail.AlternateViews.Add(_alternate);

                _mail.Subject = "CSI :: Contact Request";

                _client.Send(_mail);
                
                return new ResourceManager("Resources.Data", _assembly).GetString("ContactEmailSent");

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}