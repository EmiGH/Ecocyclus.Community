using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Companies
{
    internal class fCompany
    {
        internal fCompany()
        { }

        static internal Company CreateCompany(Int64 idCompany, String name, DateTime date, Auxiliaries.Geographic.Contact contact, Int64 idLogo, Security.Credential credential)
        {
            Users.User _currentUser = credential.CurrentUser;

            if (_currentUser is Users.UserAdministrator || _currentUser is Users.UserAnonymous)
                return CreateOtherCompany(idCompany, name, date, contact, idLogo, credential);

            if (((Users.UserOperator)_currentUser).Company.IdCompany == idCompany)
                return CreateOwnCompany(idCompany, name, date, contact, idLogo, credential);
            else
                return CreateOtherCompany(idCompany, name, date, contact, idLogo, credential);
        }
        static internal Company CreateOwnCompany(Int64 idCompany, String name, DateTime date, Auxiliaries.Geographic.Contact contact, Int64 idLogo, Security.Credential credential)
        {
            return new CompanyMine(idCompany, name, date, contact, idLogo, credential);
        }
        static internal Company CreateOtherCompany(Int64 idCompany, String name, DateTime date, Auxiliaries.Geographic.Contact contact, Int64 idLogo, Security.Credential credential)
        {
            return new Company(idCompany, name, date, contact, idLogo, credential);
        }
    }
}
