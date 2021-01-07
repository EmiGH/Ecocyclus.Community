using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace CSI.Library.Handlers
{
    internal class Users
    {
        #region private Fields

        #endregion

        internal Users()
        {
        }

        #region Read Functions

        internal Library.Objects.Users.User ItemForMe(String email, Security.Credential credential)
        {
            Storage.Users _dbUsers = new Storage.Users();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbUsers.ReadByEmail(email);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (Convert.ToBoolean(_dbRecord["IsAdministrator"]))
                    return new Handlers.Administrators().Item(email, credential);

                return new Handlers.Operators().Item(email, credential);
            }

            return null;
        }
        
        #endregion

        #region Write Functions

        internal Int64 Add(String email, String firstname, String lastname, String password, Int64 idPicture, Boolean isActive, String idLanguage)
        {
            Storage.Users _dbUsers = new Storage.Users();

            try 
            {
                Int64 _idUser = _dbUsers.Create(email, firstname, lastname, password, idPicture, isActive, idLanguage);
                return _idUser;
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 2601 || sqlex.Number == 2627)
                    throw new ApplicationException(Resources.Messages.DuplicatedRecord);
                else
                    throw sqlex;
            }

        }
        internal void Remove(Int64 idUser)
        {
            Storage.Users _dbUsers = new Storage.Users();

            try
            {
                _dbUsers.Delete(idUser);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 547)
                    throw new ApplicationException(Resources.Messages.ErrorCannotDeleteExistingRelationship);
                else
                    throw sqlex;
            }
        }
        internal void Modify(Int64 idUser, String email, String firstname, String lastname, Int64 idPicture, Boolean isActive, String idLanguage)
        {
            Storage.Users _dbUsers = new Storage.Users();
            _dbUsers.Update(idUser, email, firstname, lastname, idPicture, isActive, idLanguage);
        }

        internal void ChangePassword(Int64 idUser, String oldPassword, String newPassword)
        {
            Storage.Users _dbUsers = new Storage.Users();
            _dbUsers.ChangePassword(idUser, oldPassword, newPassword);
        }
        internal void ResetPassword(Int64 idUser, String newPassword)
        {
            Storage.Users _dbUsers = new Storage.Users();
            _dbUsers.ResetPassword(idUser, newPassword);
        }
        internal void ChangeActiveStatus(Int64 idUser, Boolean active)
        {
            Storage.Users _dbUsers = new Storage.Users();

            try
            {
                _dbUsers.UpdateActive(idUser, active);
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
