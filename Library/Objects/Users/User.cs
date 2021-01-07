using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Users
{
    public class User
    {
        internal User(Int64 idUser, DateTime timestamp, String email, String firstname, String lastname, Int64 idPicture, String idLanguage, Boolean isActive, Security.Credential credential) 
        {
            _Credential = credential;

            _IdUser = idUser;
            _Timestamp = timestamp;
            _Email = email;
            _Firstname = firstname;
            _Lastname = lastname;
            _IdPicture = idPicture;
            _IdLanguage = idLanguage;
            _IsActive = isActive;

        }

        #region Private Fields

        protected Security.Credential _Credential;

        private Int64 _IdUser;
        private DateTime _Timestamp;
        private String _Email;
        private String _Firstname;
        private String _Lastname;
        private Int64 _IdPicture;
        private String _IdLanguage;
        private Boolean _IsActive;

        #endregion

        #region Public Properties

        internal Security.Credential Credential
        { get { return _Credential; } }

        public Int64 IdUser
        { get { return _IdUser; } }
        public DateTime Timestamp
        { get { return _Timestamp; } }
        public String Email
        { get { return _Email; } }
        public String Firstname
        { get { return _Firstname; } }
        public String Lastname
        { get { return _Lastname; } }
        public Boolean IsActive
        { get { return _IsActive; } }
        public String Fullname
        { get { return Lastname + ", " + Firstname; } }


        public Auxiliaries.Files.File Picture
        { get { return new Handlers.Files().Item(_IdPicture); } }
        public Auxiliaries.Globalization.Language Language
        { get { return new Handlers.Languages().Item(_IdLanguage); } }

        #endregion

        #region Public Methods
               
        
        #endregion

        #region Private Methods


        #endregion
    }
}
