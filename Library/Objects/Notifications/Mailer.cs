using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace CSI.Library.Objects.Notifications
{
    internal class Mailer
    {
        private SmtpClient _Client;
        private String _Sender;

        internal Mailer()
        {
            Configuration _configuration = new Handlers.Notifications().Configuration();
            _Sender = _configuration.Sender;

            //if (_configuration.Port > 0)
            //    _Client = new SmtpClient(_configuration.Host, _configuration.Port);
            //else
            //    _Client = new SmtpClient(_configuration.Host);

            //_Client.Credentials = new System.Net.NetworkCredential(_configuration.Username, _configuration.Password);
            //_Client.EnableSsl = true; 
            //_Client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //_Client.UseDefaultCredentials = false;
            _Client = new SmtpClient();
           
        }

        internal void Send(String to, String subject, String body)
        {
            MailMessage _mail = new MailMessage(_Sender, to);
            System.Net.Mime.ContentType _mimeType;
            AlternateView _alternate;

            _mimeType = new System.Net.Mime.ContentType("text/plain");
            _alternate = AlternateView.CreateAlternateViewFromString(ReplaceSpecialCharacters(body, "\n"), _mimeType);
            _mail.AlternateViews.Add(_alternate);

            _mimeType = new System.Net.Mime.ContentType("text/html");
            _alternate = AlternateView.CreateAlternateViewFromString(ReplaceSpecialCharacters(BuildHTML(body), "<br />"), _mimeType);
            _mail.AlternateViews.Add(_alternate);
            _mail.Subject = subject;

            try
            {
                _Client.Send(_mail);
            }
            catch (Exception ex) 
            { 
            
            }
        }
        internal void Send(Notification notification)
        {
            MailMessage _mail = new MailMessage(_Sender, notification.User.Email);
            System.Net.Mime.ContentType _mimeType;
            AlternateView _alternate;

            _mimeType = new System.Net.Mime.ContentType("text/plain");
            _alternate = AlternateView.CreateAlternateViewFromString(ReplaceSpecialCharacters(notification.Title, "\n"), _mimeType);
            _mail.AlternateViews.Add(_alternate);

            _mimeType = new System.Net.Mime.ContentType("text/html");
            _alternate = AlternateView.CreateAlternateViewFromString(ReplaceSpecialCharacters(BuildHTML(notification.Title), "<br />"), _mimeType);
            _mail.AlternateViews.Add(_alternate);

            _mail.Subject = notification.Message;

            try
            {
                _Client.Send(_mail);
            }
            catch(Exception ex) {
            }
        }

        private String BuildHTML(String body)
        {
            StringBuilder _sb = new StringBuilder("<!DOCTYPE html><html><head><meta charset='utf-8' /></head><body><form>");
            _sb.Append(body);
            _sb.Append("</form></body></html>");

            return _sb.ToString();
        }

        internal static String ReplaceSpecialCharacters(String message, String newCRLF)
        {
            String _msg = message;

            _msg = ReplaceCRLF(_msg, newCRLF);
            _msg = ReplaceHTMLAccents(_msg);
            _msg = ReplaceLinks(_msg);

            return _msg;
        }
        internal static String ReplaceCRLF(String message, String newCRLF)
        {
            return message.Replace("[n]", newCRLF);
        }
        internal static String ReplaceHTMLAccents(String message)
        {
            String _newMessage = message;

            _newMessage = _newMessage.Replace("á", "&aacute;");
            _newMessage = _newMessage.Replace("é", "&eacute;");
            _newMessage = _newMessage.Replace("í", "&iacute;");
            _newMessage = _newMessage.Replace("ó", "&oacute;");
            _newMessage = _newMessage.Replace("ú", "&uacute;");
            _newMessage = _newMessage.Replace("Á", "&Aacute;");
            _newMessage = _newMessage.Replace("É", "&Eacute;");
            _newMessage = _newMessage.Replace("Í", "&Iacute;");
            _newMessage = _newMessage.Replace("Ó", "&Oacute;");
            _newMessage = _newMessage.Replace("Ú", "&Uacute;");
            _newMessage = _newMessage.Replace("ñ", "&ntilde;");
            _newMessage = _newMessage.Replace("Ñ", "&Ntilde;");
            _newMessage = _newMessage.Replace("ä", "&auml;");
            _newMessage = _newMessage.Replace("ö", "&ouml;");
            _newMessage = _newMessage.Replace("ü", "&uuml;");
            _newMessage = _newMessage.Replace("Ä", "&Äuml;");
            _newMessage = _newMessage.Replace("Ö", "&Öuml;");
            _newMessage = _newMessage.Replace("Ü", "&Uuml;");
            _newMessage = _newMessage.Replace("ß", "&szlig;");

            return _newMessage;

        }
        internal static String ReplaceLinks(String message)
        {
            message = message.Replace("[<a]", "<a href='");
            message = message.Replace("[a>]", "'>");
            message = message.Replace("[/a]", "</a>");

            return message;
        }
    }
}
