using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Threading;
using System.Globalization;

namespace CSI.Library
{
    public class Operation
    {
        public Operation(String email, String password, String idLanguage, String ipAddress)
        {
            _Credential = new Security.Credential(email, password, idLanguage, ipAddress);

            CultureInfo _cultureInfo = new CultureInfo(idLanguage);

            Thread.CurrentThread.CurrentCulture = _cultureInfo;
            Thread.CurrentThread.CurrentUICulture = _cultureInfo;
        }
        
        #region Private Fields

        private Security.Credential _Credential;
        private Objects.Users.UserOperatorMe _CurrentUser;

        #endregion

        #region Public Properties

        public Objects.Users.UserOperatorMe CurrentUser
        {
            get
            {
                if (_CurrentUser == null)
                    _CurrentUser = ((Objects.Users.UserOperatorMe)_Credential.CurrentUser);
                return _CurrentUser;
            }
        }
        public Objects.Auxiliaries.Globalization.Language CurrentLanguage
        {
            get { return _Credential.CurrentLanguage; }
            set { _Credential.CurrentLanguage = value; }
        }
        public Objects.Auxiliaries.Globalization.Language DefaultLanguage
        { get { return _Credential.DefaultLanguage; } }

       

        public static Dictionary<String, Objects.Auxiliaries.Globalization.Language> GetLanguagesAvailable()
        { return Handlers.Languages.Options(); }

        #endregion

        #region Public Methods

        #region Registration Methods

        public static void AccountRegister(String name, String location, Objects.Auxiliaries.Geographic.Position position, String email, String password, String firstname, String lastname, String idLanguage)
        {
            //Establezco el idioma actual
            CultureInfo _cultureInfo = new CultureInfo(idLanguage);

            Thread.CurrentThread.CurrentCulture = _cultureInfo;
            Thread.CurrentThread.CurrentUICulture = _cultureInfo;

            Objects.Companies.Company _company = null;

            //Check name and email
            Security.Authority.CheckCompanyData(name, email);

            using (TransactionScope _transactionScope = new TransactionScope())
            {
                Security.Credential _credential = new Security.Credential(idLanguage);

                _company = new Handlers.Companies().Register(name, location, position, "", "", "", "", "", 0, false, _credential);
                new Handlers.Operators().Add(_company.IdCompany, email, firstname, lastname, Security.Cryptography.Hash(password), 0, false, true, idLanguage, _credential);

                //Send mail
                Objects.Notifications.Mailer _mailer = new Objects.Notifications.Mailer();
                String _body = Resources.Messages.AccountRegistrationMailBody;
                _body = _body.Replace("[token]", System.Web.HttpUtility.UrlEncode(Security.Cryptography.Encrypt(_company.IdCompany.ToString())));
                _body = _body.Replace("[language]", idLanguage);
                _body += Resources.Messages.MailFooter;
                _mailer.Send(email, Resources.Messages.AccountRegistrationMailSubject, _body);
                
                //Send administration mail
                Handlers.Mail _config = new Handlers.Mail();

                _body = Resources.Messages.AccountRegistrationAdministrationMailBody;
                _body = _body.Replace("[company]", _company.Name);
                _body += Resources.Messages.MailFooter;
                _mailer.Send(_config.Configuration().Receiver, Resources.Messages.AccountRegistrationMailSubject, _body);

                _transactionScope.Complete();

            }           
            
        }
        public static void AccountRegister(String name, String email, String password, String firstname, String lastname, String idLanguage)
        {
            //Establezco el idioma actual
            CultureInfo _cultureInfo = new CultureInfo(idLanguage);

            Thread.CurrentThread.CurrentCulture = _cultureInfo;
            Thread.CurrentThread.CurrentUICulture = _cultureInfo;

            Objects.Companies.Company _company = null;

            //Check name and email
            Security.Authority.CheckCompanyData(name, email);

            using (TransactionScope _transactionScope = new TransactionScope())
            {
                Security.Credential _credential = new Security.Credential(idLanguage);

                _company = new Handlers.Companies().Register(name, "", "", "", "", "", 0, false, _credential);
                new Handlers.Operators().Add(_company.IdCompany, email, firstname, lastname, Security.Cryptography.Hash(password), 0, false, true, idLanguage, _credential);

                //Send mail
                Objects.Notifications.Mailer _mailer = new Objects.Notifications.Mailer();
                String _body = Resources.Messages.AccountRegistrationMailBody;
                _body = _body.Replace("[token]", System.Web.HttpUtility.UrlEncode(Security.Cryptography.Encrypt(_company.IdCompany.ToString())));
                _body = _body.Replace("[language]", idLanguage);
                _body += Resources.Messages.MailFooter;
                _mailer.Send(email, Resources.Messages.AccountRegistrationMailSubject, _body);

                //Send administration mail
                Handlers.Mail _config = new Handlers.Mail();

                _body = Resources.Messages.AccountRegistrationAdministrationMailBody;
                _body = _body.Replace("[company]", _company.Name);
                _body += Resources.Messages.MailFooter;
                _mailer.Send(_config.Configuration().Receiver, Resources.Messages.AccountRegistrationMailSubject, _body);

                _transactionScope.Complete();

            }

        }
        public static void AccountVerify(String token, String idLanguage)
        {
            //Establezco el idioma actual
            CultureInfo _cultureInfo = new CultureInfo(idLanguage);

            Thread.CurrentThread.CurrentCulture = _cultureInfo;
            Thread.CurrentThread.CurrentUICulture = _cultureInfo;

            Int64 _idCompany = Convert.ToInt64(Security.Cryptography.Decrypt(token));

            Security.Credential _credential = new Security.Credential(idLanguage);
            Objects.Companies.Company _company = new Handlers.Companies().Item(_idCompany, _credential);
            if (_company == null)
                throw new Security.RegistrationException(Resources.Messages.AccountActivationBadTokenError);
                        
            new Handlers.Companies().ChangeActiveStatus(_idCompany, true);

            //Send administration mail
            Objects.Notifications.Mailer _mailer = new Objects.Notifications.Mailer();
            Handlers.Mail _config = new Handlers.Mail();

            String _body = Resources.Messages.AccountVerificationAdministrationMailBody;
            _body = _body.Replace("[company]", _company.Name);
            _body += Resources.Messages.MailFooter;
            _mailer.Send(_config.Configuration().Receiver, Resources.Messages.AccountRegistrationMailSubject, _body);

            
        }
        public static void AccountRecover(String email, String idLanguage)
        {
            //Establezco el idioma actual
            CultureInfo _cultureInfo = new CultureInfo(idLanguage);

            Thread.CurrentThread.CurrentCulture = _cultureInfo;
            Thread.CurrentThread.CurrentUICulture = _cultureInfo;

            String _idUser = new Handlers.Operators().Item(email, new Security.Credential(idLanguage)).IdUser.ToString();
            
            //Send mail
            Objects.Notifications.Mailer _mailer = new Objects.Notifications.Mailer();
            String _body = Resources.Messages.AccountRecoveryMailBody;
            _body = _body.Replace("[token]", Security.Cryptography.Encrypt(_idUser));
            _body = _body.Replace("[language]", idLanguage);
            _body += Resources.Messages.MailFooter;
            _mailer.Send(email, Resources.Messages.AccountRecoveryMailSubject, _body);

        }
        public static void AccountReset(String token, String newPassword, String idLanguage)
        {
            //Establezco el idioma actual
            CultureInfo _cultureInfo = new CultureInfo(idLanguage);

            Thread.CurrentThread.CurrentCulture = _cultureInfo;
            Thread.CurrentThread.CurrentUICulture = _cultureInfo;

            Int64 _idUser = Convert.ToInt64(Security.Cryptography.Decrypt(token));
            new Handlers.Users().ResetPassword(_idUser, Security.Cryptography.Hash(newPassword));

        }
        public static void Contact(String from, String name, String company, String message)
        {
            //Send mail
            Objects.Notifications.Mailer _mailer = new Objects.Notifications.Mailer();
            String _body = message + name + company;
            _mailer.Send(from, Resources.Messages.ContactTitle, _body);
        }

        #endregion

        #region Utility Methods

        public Objects.Auxiliaries.Files.File File(Int64 idFile)
        {
            return new Handlers.Files().Item(idFile);
        }

        #endregion

        #region Log Methods

        public void LogAuthenticatedError(Exception exception)
        {
            new Handlers.LogManager().LogError(_CurrentUser.IdOperator, BuildErrorFromException(exception));
        }
        public static void LogUnauthenticatedError(Exception exception)
        {
            new Handlers.LogManager().LogError(0, BuildErrorFromException(exception));
        }
        private static String BuildErrorFromException(Exception ex)
        {
            return ex.ToString();
        }

        #endregion

        #endregion
    }
}
