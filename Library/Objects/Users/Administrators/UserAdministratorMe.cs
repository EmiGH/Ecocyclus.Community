using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Users
{
    public class UserAdministratorMe : UserAdministrator
    {
        internal UserAdministratorMe(Int64 idAdministrator, Int64 idUser, DateTime timestamp, String email, String firstname, String lastname, Int64 idPicture, String idLanguage, Boolean isActive, Security.Credential credential)
            : base(idAdministrator, idUser, timestamp, email, firstname, lastname, idPicture, idLanguage, isActive, credential)
        {
           
        }

        #region Private Fields

        #endregion

        #region Public Properties

        #endregion
    }
}
