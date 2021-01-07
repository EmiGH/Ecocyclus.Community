using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace CSI.Library.Storage
{
    internal class Users
    {
        internal Users() { }

        #region Read Methods

        internal IEnumerable<DbDataRecord> ReadAll()
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Users_ReadAll");
            SqlDataReader _reader = (SqlDataReader)(((RefCountingDataReader)_db.ExecuteReader(_dbCommand)).InnerReader);

            try
            {
                foreach (DbDataRecord _record in _reader)
                {
                    yield return _record;
                }
            }
            finally
            {
                _reader.Close();
            }
        }
        internal IEnumerable<DbDataRecord> ReadById(Int64 idUser)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Users_ReadById");
            _db.AddInParameter(_dbCommand, "IdUser", DbType.Int64, idUser);
            SqlDataReader _reader = (SqlDataReader)(((RefCountingDataReader)_db.ExecuteReader(_dbCommand)).InnerReader);

            try
            {
                foreach (DbDataRecord _record in _reader)
                {
                    yield return _record;
                }
            }
            finally
            {
                _reader.Close();
            }
        }
        internal IEnumerable<DbDataRecord> ReadByEmail(String email)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Users_ReadByEmail");
            _db.AddInParameter(_dbCommand, "Email", DbType.String, email);
            SqlDataReader _reader = (SqlDataReader)(((RefCountingDataReader)_db.ExecuteReader(_dbCommand)).InnerReader);

            try
            {
                foreach (DbDataRecord _record in _reader)
                {
                    yield return _record;
                }
            }
            finally
            {
                _reader.Close();
            }
        }
        internal Boolean Authenticate(String email, String password, String ipAddress)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Users_Login");
            _db.AddInParameter(_dbCommand, "Email", DbType.String, email);
            _db.AddInParameter(_dbCommand, "Password", DbType.String, password);
            _db.AddOutParameter(_dbCommand, "Authentication", DbType.Boolean, 1);

            _db.ExecuteNonQuery(_dbCommand);
            return (Boolean)_db.GetParameterValue(_dbCommand, "Authentication");
            
        }
        
        #endregion

        #region Write Methods

        internal Int64 Create(String email, String firstname, String lastname, String password, Int64 idPicture, Boolean isActive, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Users_Create");
            _db.AddInParameter(_dbCommand, "Email", DbType.String, email);
            _db.AddInParameter(_dbCommand, "Firstname", DbType.String, firstname);
            _db.AddInParameter(_dbCommand, "Lastname", DbType.String, lastname);
            _db.AddInParameter(_dbCommand, "Password", DbType.String, password);
            _db.AddInParameter(_dbCommand, "IdPicture", DbType.Int64, Auxiliaries.Common.CastValueToNull(idPicture, DBNull.Value));
            _db.AddInParameter(_dbCommand, "IsActive", DbType.Boolean, isActive);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            
            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdUser", DbType.Int64, 18);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdUser"));

        }
        internal void Delete(Int64 idUser)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Users_Delete");
            _db.AddInParameter(_dbCommand, "IdUser", DbType.Int64, idUser);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Update(Int64 idUser, String email, String firstname, String lastname, Int64 idPicture, Boolean isActive, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Users_Update");
            _db.AddInParameter(_dbCommand, "IdUser", DbType.Int64, idUser);
            _db.AddInParameter(_dbCommand, "Email", DbType.String, email);
            _db.AddInParameter(_dbCommand, "Firstname", DbType.String, firstname);
            _db.AddInParameter(_dbCommand, "Lastname", DbType.String, lastname);
            _db.AddInParameter(_dbCommand, "IdPicture", DbType.Int64, Auxiliaries.Common.CastValueToNull(idPicture, DBNull.Value));
            _db.AddInParameter(_dbCommand, "IsActive", DbType.Boolean, isActive);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

        }

        internal void ChangePassword(Int64 idUser, String oldPassword, String newPassword)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Users_ChangePassword");
            _db.AddInParameter(_dbCommand, "IdUser", DbType.Int64, idUser);
            _db.AddInParameter(_dbCommand, "OldPassword", DbType.String, oldPassword);
            _db.AddInParameter(_dbCommand, "NewPassword", DbType.String, newPassword);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void ResetPassword(Int64 idUser, String newPassword)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Users_ResetPassword");
            _db.AddInParameter(_dbCommand, "IdUser", DbType.Int64, idUser);
            _db.AddInParameter(_dbCommand, "NewPassword", DbType.String, newPassword);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void UpdateActive(Int64 idUser, Boolean active)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Users_UpdateActive");
            _db.AddInParameter(_dbCommand, "IdUser", DbType.Int64, idUser);
            _db.AddInParameter(_dbCommand, "Active", DbType.Boolean, active);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        #endregion
    }
}
