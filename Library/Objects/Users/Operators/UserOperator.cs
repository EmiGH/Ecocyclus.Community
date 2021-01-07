using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Users
{
    public class UserOperator : User
    {
        internal UserOperator(Int64 idOperator, Int64 idUser, Int64 idCompany, DateTime timestamp, String email, String firstname, String lastname, Int64 idPicture, String idlanguage, Boolean isActive, Security.Credential credential)
            : base(idUser, timestamp, email, firstname, lastname, idPicture, idlanguage, isActive, credential)
        {
            _IdOperator = idOperator;
            _IdCompany = idCompany;
        }

        #region Private Fields

        private Int64 _IdOperator;
        private Int64 _IdCompany;

        #endregion

        #region Public Properties

        protected Int64 idCompany
        { get { return _IdCompany; } }
        public Int64 IdOperator
        { get { return _IdOperator; } }
        public virtual Companies.Company Company
        { get { return new Handlers.Companies().Item(_IdCompany, Credential); } }
        
        #endregion
    }
}
