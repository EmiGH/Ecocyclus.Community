using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Users
{
    internal class fUserOperator
    {
        internal fUserOperator() { }

        internal static UserOperator CreateOperatorOther(Int64 idOperator, Int64 idUser, Int64 idCompany, DateTime timestamp, String email, String firstname, String lastname, Int64 idPicture, String idLanguage, Boolean isManager, Boolean isActive, Security.Credential credential)
        {
            Users.User _user = credential.CurrentUser;
            
            //Soy Manager de mi empresa
            if (_user is UserOperator)
            {
                //Si es de mi misma empresa devuelvo compañero gestionado
                if (((UserOperator)_user).Company.IdCompany == idCompany)
                {
                    return CreateCoworker(idOperator, idUser, idCompany, timestamp, email, firstname, lastname, idPicture, idLanguage, isManager, isActive, credential);
                }
                else
                {
                    return CreateOther(idOperator, idUser, idCompany, timestamp, email, firstname, lastname, idPicture, idLanguage, isActive, credential);
                }
            }
            else
            {
                //Soy administrador y devuelvo un administrado siempre
                return CreateOther(idOperator, idUser, idCompany, timestamp, email, firstname, lastname, idPicture, idLanguage, isActive, credential);
            }
                        
        }
        internal static UserOperatorMe CreateOperatorMe(Int64 idOperator, Int64 idUser, Int64 idCompany, DateTime timestamp, String email, String firstname, String lastname, Int64 idPicture, String idLanguage, Boolean isManager, Boolean isActive, Security.Credential credential)
        {
            if (isManager)
                return CreateManager(idOperator, idUser, idCompany, timestamp, email, firstname, lastname, idPicture, idLanguage, isManager, isActive, credential);

            return CreateOperator(idOperator, idUser, idCompany, timestamp, email, firstname, lastname, idPicture, idLanguage, isManager, isActive, credential);
        }

        #region My Users

        private static UserOperatorMe CreateOperator(Int64 idOperator, Int64 idUser, Int64 idCompany, DateTime timestamp, String email, String firstname, String lastname, Int64 idPicture, String idLanguage, Boolean isManager, Boolean isActive, Security.Credential credential)
        {
            return new UserOperatorMe(idOperator, idUser, idCompany, timestamp, email, firstname, lastname, idPicture, idLanguage, isManager, isActive, credential);
        }
        private static UserOperatorMeManager CreateManager(Int64 idOperator, Int64 idUser, Int64 idCompany, DateTime timestamp, String email, String firstname, String lastname, Int64 idPicture, String idLanguage, Boolean isManager, Boolean isActive, Security.Credential credential)
        {
            return new UserOperatorMeManager(idOperator, idUser, idCompany, timestamp, email, firstname, lastname, idPicture, idLanguage, isManager, isActive, credential);
        }

        #endregion

        #region Other Users

        private static UserOperatorCoworker CreateCoworker(Int64 idOperator, Int64 idUser, Int64 idCompany, DateTime timestamp, String email, String firstname, String lastname, Int64 idPicture, String idLanguage, Boolean isManager, Boolean isActive, Security.Credential credential)
        {
            return new UserOperatorCoworker(idOperator, idUser, idCompany, timestamp, email, firstname, lastname, idPicture, idLanguage, isManager, isActive, credential);
        }
        private static UserOperator CreateOther(Int64 idOperator, Int64 idUser, Int64 idCompany, DateTime timestamp, String email, String firstname, String lastname, Int64 idPicture, String idLanguage, Boolean isActive, Security.Credential credential)
        {
            return new UserOperator(idOperator, idUser, idCompany, timestamp, email, firstname, lastname, idPicture, idLanguage, isActive, credential);
        }
        

        #endregion
    }
}
