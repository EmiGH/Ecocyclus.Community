using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Users
{
    internal class fAdministrators
    {
        internal fAdministrators() { }

        internal static UserAdministrator CreateAdministrator(Int64 idAdministrator, Int64 idUser, DateTime timestamp, String email, String firstname, String lastname, Int64 idPicture, String idLanguage, Boolean isActive, Security.Credential credential)
        {
            Users.User _currentUser = credential.CurrentUser;
            if (_currentUser is UserAdministratorMe && _currentUser.IdUser == idUser)
                return new Users.UserAdministratorMe(idAdministrator, idUser, timestamp, email, firstname, lastname, idPicture, idLanguage, isActive, credential);

            return new Users.UserAdministrator(idAdministrator, idUser, timestamp, email, firstname, lastname, idPicture, idLanguage, isActive, credential);
        }
    }
}
