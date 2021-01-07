using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Data.SqlClient;

namespace CSI.Library.Handlers
{
    internal class Administrators
    {
        #region private Fields

        #endregion

        internal Administrators()
        {
        }

        #region Read Functions

        internal Library.Objects.Users.UserAdministrator Item(Int64 idAdministrator, Security.Credential credential)
        {
            Storage.Administrators _dbUsers = new Storage.Administrators();
            Library.Objects.Users.UserAdministrator _userAdministrator = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbUsers.ReadById(idAdministrator);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _userAdministrator = Library.Objects.Users.fAdministrators.CreateAdministrator(Convert.ToInt64(_dbRecord["IdAdministrator"]), Convert.ToInt64(_dbRecord["IdUser"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Firstname"]), Convert.ToString(_dbRecord["Lastname"]), Convert.ToInt64(_dbRecord["IdPicture"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToBoolean(_dbRecord["IsActive"]), credential);
            }
            return _userAdministrator;
        }
        internal Library.Objects.Users.UserAdministratorMe Item(String email, Security.Credential credential)
        {
            Storage.Administrators _dbUsers = new Storage.Administrators();
            Library.Objects.Users.UserAdministratorMe _userAdministrator = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbUsers.ReadByEmail(email);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _userAdministrator = new Library.Objects.Users.UserAdministratorMe(Convert.ToInt64(_dbRecord["IdAdministrator"]), Convert.ToInt64(_dbRecord["IdUser"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Firstname"]), Convert.ToString(_dbRecord["Lastname"]), Convert.ToInt64(_dbRecord["IdPicture"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToBoolean(_dbRecord["IsActive"]), credential);
            }
            return _userAdministrator;
        }
        internal Dictionary<Int64, Library.Objects.Users.UserAdministrator> Items(Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Users.UserAdministrator> _oItems = new Dictionary<Int64, Library.Objects.Users.UserAdministrator>();
            Storage.Administrators _dbUsers = new Storage.Administrators();
            Library.Objects.Users.UserAdministrator _userAdministrator = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbUsers.ReadAll();

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _userAdministrator = Library.Objects.Users.fAdministrators.CreateAdministrator(Convert.ToInt64(_dbRecord["IdAdministrator"]), Convert.ToInt64(_dbRecord["IdUser"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Firstname"]), Convert.ToString(_dbRecord["Lastname"]), Convert.ToInt64(_dbRecord["IdPicture"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToBoolean(_dbRecord["IsActive"]), credential);
                _oItems.Add(_userAdministrator.IdUser, _userAdministrator);
            }
            return _oItems;
        }

        #endregion

        #region Write Functions

        internal Library.Objects.Users.UserAdministrator Add(Int64 idUser, String email, String firstname, String lastname, String password, Int64 idPicture, Boolean isActive, String idLanguage, Security.Credential credential)
        {
            Storage.Administrators _dbAdministrators = new Storage.Administrators();
            Int64 _idAdministrator = 0;

            try
            {
                using (TransactionScope _transactionScope = new TransactionScope())
                {
                    Int64 _idUser = new Handlers.Users().Add(email, firstname, lastname, password, idPicture, isActive, idLanguage);
                    _idAdministrator = _dbAdministrators.Create(_idUser);

                    _transactionScope.Complete();
                }
                return Item(_idAdministrator, credential);

            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 2601 || sqlex.Number == 2627)
                    throw new ApplicationException(Resources.Messages.DuplicatedRecord);
                else
                    throw sqlex;
            }
        }
        internal void Remove(Int64 idAdministrator)
        {
            Storage.Administrators _dbUsers = new Storage.Administrators();

            try
            {
                _dbUsers.Delete(idAdministrator);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 547)
                    throw new ApplicationException(Resources.Messages.ErrorCannotDeleteExistingRelationship);
                else
                    throw sqlex;
            }
        }
        
        #endregion

    }
}
