using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Users
{
    public class UserAdministrator : User
    {
        internal UserAdministrator(Int64 idAdministrator, Int64 idUser, DateTime timestamp, String email, String firstname, String lastname, Int64 idPicture, String idlanguage, Boolean isActive, Security.Credential credential)
            : base(idUser, timestamp, email, firstname, lastname, idPicture, idlanguage, isActive, credential)
        {
            _IdAdministrator = idAdministrator;
        }

        #region Private Fields

        private Int64 _IdAdministrator;

        #endregion

        #region Public Properties

        public Int64 IdAdministrator
        { get { return _IdAdministrator; } }

        #endregion
    }
}
