using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Data.SqlClient;

namespace CSI.Library.Handlers
{
    internal class Operators
    {
        #region private Fields

        #endregion

        internal Operators()
        { 
            
        }

        #region Read Functions

        internal Library.Objects.Users.UserOperator Item(Int64 idCompanyUser, Security.Credential credential)
        {
            Storage.Operators _dbOperators = new Storage.Operators();
            Library.Objects.Users.UserOperator _userOperator = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbOperators.ReadById(idCompanyUser);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _userOperator = Library.Objects.Users.fUserOperator.CreateOperatorOther(idCompanyUser, Convert.ToInt64(_dbRecord["IdUser"]), Convert.ToInt64(_dbRecord["IdCompany"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Firstname"]), Convert.ToString(_dbRecord["Lastname"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdPicture"], 0)), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToBoolean(_dbRecord["IsManager"]), Convert.ToBoolean(_dbRecord["IsActive"]), credential);
            }
            return _userOperator;
        }
        internal Library.Objects.Users.UserOperatorMe Item(String email, Security.Credential credential)
        {
            Storage.Operators _dbOperators = new Storage.Operators();
            Library.Objects.Users.UserOperatorMe _userOperator = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbOperators.ReadByEmail(email);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _userOperator = Library.Objects.Users.fUserOperator.CreateOperatorMe(Convert.ToInt64(_dbRecord["IdCompanyUser"]), Convert.ToInt64(_dbRecord["IdUser"]), Convert.ToInt64(_dbRecord["IdCompany"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Firstname"]), Convert.ToString(_dbRecord["Lastname"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdPicture"], 0)), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToBoolean(_dbRecord["IsManager"]), Convert.ToBoolean(_dbRecord["IsActive"]), credential);
            }
            return _userOperator;
        }
        internal Dictionary<Int64, Library.Objects.Users.UserOperator> Items(Int64 idCompany, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Users.UserOperator> _oItems = new Dictionary<Int64, Library.Objects.Users.UserOperator>();
            Storage.Operators _dbOperators = new Storage.Operators();
            Library.Objects.Users.UserOperator _userOperator = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbOperators.ReadAllByCompany(idCompany);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _userOperator = Library.Objects.Users.fUserOperator.CreateOperatorOther(Convert.ToInt64(_dbRecord["IdCompanyUser"]), Convert.ToInt64(_dbRecord["IdUser"]), Convert.ToInt64(_dbRecord["IdCompany"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Firstname"]), Convert.ToString(_dbRecord["Lastname"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdPicture"], 0)), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToBoolean(_dbRecord["IsManager"]), Convert.ToBoolean(_dbRecord["IsActive"]), credential);
                _oItems.Add(_userOperator.IdUser, _userOperator);
            }
            return _oItems;
        }

        internal Dictionary<Int64, Library.Objects.Users.UserOperatorCoworker> Coworkers(Int64 idCompany, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Users.UserOperatorCoworker> _oItems = new Dictionary<Int64, Library.Objects.Users.UserOperatorCoworker>();
            Storage.Operators _dbOperators = new Storage.Operators();
            Library.Objects.Users.UserOperatorCoworker _userOperator = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbOperators.ReadAllByCompany(idCompany);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _userOperator = (Objects.Users.UserOperatorCoworker)Library.Objects.Users.fUserOperator.CreateOperatorOther(Convert.ToInt64(_dbRecord["IdCompanyUser"]), Convert.ToInt64(_dbRecord["IdUser"]), Convert.ToInt64(_dbRecord["IdCompany"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Firstname"]), Convert.ToString(_dbRecord["Lastname"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdPicture"], 0)), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToBoolean(_dbRecord["IsManager"]), Convert.ToBoolean(_dbRecord["IsActive"]), credential);
                _oItems.Add(_userOperator.IdUser, _userOperator);
            }
            return _oItems;
        }
        internal Dictionary<Int64, Library.Objects.Users.UserOperatorCoworker> Managers(Int64 idCompany, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Users.UserOperatorCoworker> _oItems = new Dictionary<Int64, Library.Objects.Users.UserOperatorCoworker>();
            Storage.Operators _dbOperators = new Storage.Operators();
            Library.Objects.Users.UserOperatorCoworker _userOperator = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbOperators.ReadManagersByCompany(idCompany);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _userOperator = (Objects.Users.UserOperatorCoworker)Library.Objects.Users.fUserOperator.CreateOperatorOther(Convert.ToInt64(_dbRecord["IdCompanyUser"]), Convert.ToInt64(_dbRecord["IdUser"]), Convert.ToInt64(_dbRecord["IdCompany"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Firstname"]), Convert.ToString(_dbRecord["Lastname"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdPicture"], 0)), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToBoolean(_dbRecord["IsManager"]), Convert.ToBoolean(_dbRecord["IsActive"]), credential);
                _oItems.Add(_userOperator.IdUser, _userOperator);
            }
            return _oItems;
        }

        #endregion

        #region Write Functions

        internal Library.Objects.Users.UserOperator Add(Int64 idCompany, String email, String firstname, String lastname, String password, Int64 idPicture, Boolean isActive, Boolean isManager, String idLanguage, Security.Credential credential)
        {
            Storage.Operators _dbOperators = new Storage.Operators();
            Int64 _idUserOperator = 0;

            try 
            {
                using (TransactionScope _transactionScope = new TransactionScope())
                {
                    Int64 _idUser = new Handlers.Users().Add(email, firstname, lastname, password, idPicture, isActive, idLanguage);
                    _idUserOperator = _dbOperators.Create(idCompany, _idUser, isManager);
                
                    _transactionScope.Complete();
                }

                return Item(_idUserOperator, credential);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 2601 || sqlex.Number == 2627)
                    throw new ApplicationException(Resources.Messages.DuplicatedRecord);
                else
                    throw sqlex;
            }
        }
        internal void Remove(Int64 idCompany, Int64 idCompanyUser, Int64 idUser, Security.Credential credential)
        {
            Storage.Users _dbUsers = new Storage.Users();
            Storage.Permissions _dbPermissions = new Storage.Permissions();

            try
            {
                if (Items(idCompany, credential).Count == 1)
                    throw new ApplicationException(Resources.Messages.ErrorCannotDeleteLastUser);

                using (TransactionScope _scope = new TransactionScope())
                {
                    _dbPermissions.DeleteAllByOperator(idCompanyUser);
                    _dbUsers.Delete(idUser);

                    _scope.Complete();
                }
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 547)
                    throw new ApplicationException(Resources.Messages.ErrorCannotDeleteExistingRelationship);
                else
                    throw sqlex;
            }
        }
        internal void Modify(Int64 idCompanyUser, Int64 idUser, Int64 idCompany, String email, String firstname, String lastname, Int64 idPicture, Boolean isActive, Boolean isManager, String idLanguage)
        {
            Storage.Operators _dbOperators = new Storage.Operators();

            try 
            {
                using (TransactionScope _transactionScope = new TransactionScope())
                {
                    new Handlers.Users().Modify(idUser, email, firstname, lastname, idPicture, isActive, idLanguage);
                    _dbOperators.Update(idCompanyUser, isManager);

                    _transactionScope.Complete();
                }

            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 2601 || sqlex.Number == 2627)
                    throw new ApplicationException(Resources.Messages.DuplicatedRecord);
                else
                    throw sqlex;
            }
        }

        #endregion

    }
}
