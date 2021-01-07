using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Security
{
    public class Credential
    {
        
        internal Credential()
        {
        }
        internal Credential(String currentLanguage)
        { 
            //Para usuarios anonimos
            _selectedLanguage = currentLanguage;
        }
        internal Credential(String email, String password, String currentLanguage, String ipAddress)
        {
            //Chequeo la validez del usuario
            Authority.Authenticate(email, Cryptography.Hash(password), ipAddress);

            //Guardo el nombre de usuario
            _Email = email;

            //Cargo el lenguaje actual seleccionado por el usuario en el login
            _selectedLanguage = currentLanguage;
        }

        #region Private Fields

        private String _Email;
        private String _selectedLanguage;

        //private Objects.Users.User _CurrentUser;
        private Objects.Auxiliaries.Globalization.Language _CurrentLanguage;
        private Objects.Auxiliaries.Globalization.Language _DefaultLanguage;
        
        #endregion

        #region Public Properties

        internal Objects.Auxiliaries.Globalization.Language CurrentLanguage
        {
            get
            {
                if (_CurrentLanguage == null)
                    if (_selectedLanguage == "")
                        _CurrentLanguage = CurrentUser.Language;
                    else
                        _CurrentLanguage = new Handlers.Languages().Item(_selectedLanguage);

                return _CurrentLanguage;
            }
            set
            {
                _CurrentLanguage = value;
            }
        }
        internal Objects.Auxiliaries.Globalization.Language DefaultLanguage
        {
            get
            {
                if (_DefaultLanguage == null)
                    _DefaultLanguage = new Handlers.Languages().ItemDefault();
                return _DefaultLanguage;
            }
        }
        internal Objects.Users.User CurrentUser
        {
            get
            {
                if (string.IsNullOrEmpty(_Email))
                    return new Objects.Users.UserAnonymous(this);

                return new Handlers.Users().ItemForMe(_Email, this);
            }
        }

        #endregion
    }
}