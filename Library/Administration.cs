using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library
{
    public class Administration
    {
        public Administration(String email, String password, String language, String ipAddress)
        {
            _Credential = new Security.Credential(email, password, language, ipAddress);

        }

        #region Private Fields

        private Security.Credential _Credential;
        private Objects.Users.UserAdministratorMe _CurrentUser;

        #endregion

        #region Public Properties

        public Objects.Users.UserAdministratorMe CurrentUser
        {
            get
            {
                if (_CurrentUser == null)
                    _CurrentUser = ((Objects.Users.UserAdministratorMe)_Credential.CurrentUser);
                return _CurrentUser;
            }
        }
        public Objects.Auxiliaries.Globalization.Language CurrentLanguage
        { get { return _Credential.CurrentLanguage; } }
        public Objects.Auxiliaries.Globalization.Language DefaultLanguage
        { get { return _Credential.DefaultLanguage; } }

        #endregion

        #region Public Methods


        #endregion
    }
}
