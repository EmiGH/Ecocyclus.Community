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
    internal class Operators
    {
        internal Operators() { }

        #region Read Methods

        internal IEnumerable<DbDataRecord> ReadAllByCompany(Int64 idCompany)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("CompanyUsers_ReadByCompany");
            _db.AddInParameter(_dbCommand, "IdCompany", DbType.Int64, idCompany);
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
        internal IEnumerable<DbDataRecord> ReadManagersByCompany(Int64 idCompany)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("CompanyUsers_ReadManagersByCompany");
            _db.AddInParameter(_dbCommand, "IdCompany", DbType.Int64, idCompany);
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
        internal IEnumerable<DbDataRecord> ReadById(Int64 idCompanyUser)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("CompanyUsers_ReadById");
            _db.AddInParameter(_dbCommand, "IdCompanyUser", DbType.Int64, idCompanyUser);
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

            DbCommand _dbCommand = _db.GetStoredProcCommand("CompanyUsers_ReadByEmail");
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
        internal IEnumerable<DbDataRecord> Authenticate(String email, String password)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Security_AuthenticateOperator");
            _db.AddInParameter(_dbCommand, "Email", DbType.String, email);
            _db.AddInParameter(_dbCommand, "Password", DbType.String, password);
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
        #endregion

        #region Write Methods

        internal Int64 Create(Int64 idCompany, Int64 idUser, Boolean isManager)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("CompanyUsers_Create");
            _db.AddInParameter(_dbCommand, "IdCompany", DbType.Int64, idCompany);
            _db.AddInParameter(_dbCommand, "IdUser", DbType.Int64, idUser);
            _db.AddInParameter(_dbCommand, "IsManager", DbType.Boolean, isManager);

            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdCompanyUser", DbType.Int64, 18);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdCompanyUser"));

        }
        internal void Delete(Int64 idCompanyUser)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("CompanyUsers_Delete");
            _db.AddInParameter(_dbCommand, "IdCompanyUser", DbType.Int64, idCompanyUser);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Update(Int64 idCompanyUser, Boolean isManager)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("CompanyUsers_Update");
            _db.AddInParameter(_dbCommand, "IdCompanyUser", DbType.Int64, idCompanyUser);
            _db.AddInParameter(_dbCommand, "IsManager", DbType.Boolean, isManager);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
            
        }
        internal void UpdateActiveManagers(Int64 idCompany, Boolean active)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("CompanyUsers_UpdateActiveManagers");
            _db.AddInParameter(_dbCommand, "IdCompany", DbType.Int64, idCompany);
            _db.AddInParameter(_dbCommand, "Active", DbType.Boolean, active);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

        }
        internal void UpdateActiveAll(Int64 idCompany, Boolean active)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("CompanyUsers_UpdateActiveAll");
            _db.AddInParameter(_dbCommand, "IdCompany", DbType.Int64, idCompany);
            _db.AddInParameter(_dbCommand, "Active", DbType.Boolean, active);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

        }


        #endregion
    }
}
