using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Users
{
    public class UserOperatorCoworker : UserOperator
    {
        internal UserOperatorCoworker(Int64 idOperator, Int64 idUser, Int64 idCompany, DateTime timestamp, String email, String firstname, String lastname, Int64 idPicture, String idLanguage, Boolean isManager, Boolean isActive, Security.Credential credential)
            : base(idOperator, idUser, idCompany, timestamp, email, firstname, lastname, idPicture, idLanguage, isActive, credential)
        {
            _IsManager = isManager;
            
        }

        #region Private Fields
        
        private Boolean _IsManager;
     
        #endregion

        #region Public Properties

        public Boolean IsManager
        { get { return _IsManager; } }


        #region Permissions

        public Dictionary<Int64, Sites.Permission> GetPermissionsGranted()
        {
            return new Handlers.Permissions().ItemsByOperator(IdOperator, Credential);
        }
        public Sites.Permission GetPermissionGranted(Int64 idSiteUser)
        {
            return new Handlers.Permissions().Item(idSiteUser, Credential);
        }
        public Dictionary<Int64, Sites.SiteMine> GetPermissionsNotGranted()
        {
            return new Handlers.Permissions().SitesNotGranted(IdOperator, Credential);
        }
        

        #endregion

        #endregion
    }
}
