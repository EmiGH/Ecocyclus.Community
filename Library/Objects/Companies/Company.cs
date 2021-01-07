using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Companies
{
    public class Company
    {
        internal Company(Int64 idCompany, String name, DateTime date, Auxiliaries.Geographic.Contact contact, Int64 idLogo, Security.Credential credential)
        {
            _Credential = credential;
            _Name = name;
            _IdCompany = idCompany;
            _Date = date;
            _Contact = contact;
            _IdLogo = idLogo;
            
        }

        #region Private Fields

        internal protected Security.Credential _Credential;

        private Int64 _IdCompany;
        private String _Name;
        private DateTime _Date;
        private Auxiliaries.Geographic.Contact _Contact;
        private Int64 _IdLogo;

        #endregion

        #region Public Properties

        public Int64 IdCompany
        { get { return _IdCompany; } }
        public String Name
        { get { return _Name; } }
        public DateTime Date
        { get { return _Date; } }
        public Auxiliaries.Geographic.Contact Contact
        { get { return _Contact; } }
        public Auxiliaries.Files.File Logo
        { get { return new Handlers.Files().Item(_IdLogo); } }
        
        #endregion

        #region Public Methods

        #endregion
    }
}
