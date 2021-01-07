using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;

namespace CSI.Library.Storage
{
    internal class Permissions
    {
        internal Permissions() { }

        #region Read Methods

        internal IEnumerable<DbDataRecord> ReadAllBySite(Int64 idSite)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteUsers_ReadAllBySite");
            _db.AddInParameter(_dbCommand, "IdSite", DbType.Int64, idSite);
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
        internal IEnumerable<DbDataRecord> ReadAllNotInSite(Int64 idSite)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteUsers_ReadAllNotInSite");
            _db.AddInParameter(_dbCommand, "IdSite", DbType.Int64, idSite);
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
        internal IEnumerable<DbDataRecord> ReadAllByUser(Int64 idOperator)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteUsers_ReadAllByUser");
            _db.AddInParameter(_dbCommand, "IdCompanyUser", DbType.Int64, idOperator);
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
        internal IEnumerable<DbDataRecord> ReadAllNotForUser(Int64 idOperator, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteUsers_ReadAllNotForUser");
            _db.AddInParameter(_dbCommand, "IdCompanyUser", DbType.Int64, idOperator);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
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
        internal IEnumerable<DbDataRecord> ReadById(Int64 idSiteUser)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteUsers_ReadById");
            _db.AddInParameter(_dbCommand, "IdSiteUser", DbType.Int64, idSiteUser);
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
        internal IEnumerable<DbDataRecord> ReadManagersBySite(Int64 idSite)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteUsers_ReadManagers");
            _db.AddInParameter(_dbCommand, "IdSite", DbType.Int64, idSite);
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


        internal Boolean HasPermissionForRead(Int64 idSite, Int64 idOperator)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteUsers_ReadHasPermissionForRead");
            _db.AddInParameter(_dbCommand, "IdSite", DbType.Int64, idSite);
            _db.AddInParameter(_dbCommand, "IdCompanyUser", DbType.Int64, idOperator);

            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "HasPermission", DbType.Boolean, 1);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToBoolean(_db.GetParameterValue(_dbCommand, "HasPermission"));
        }
        internal Boolean HasPermissionForLoad(Int64 idSite, Int64 idOperator)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteUsers_ReadHasPermissionForLoad");
            _db.AddInParameter(_dbCommand, "IdSite", DbType.Int64, idSite);
            _db.AddInParameter(_dbCommand, "IdCompanyUser", DbType.Int64, idOperator);

            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "HasPermission", DbType.Boolean, 1);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToBoolean(_db.GetParameterValue(_dbCommand, "HasPermission"));
        }
        internal Boolean HasPermissionForManage(Int64 idSite, Int64 idOperator)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteUsers_ReadHasPermissionForManage");
            _db.AddInParameter(_dbCommand, "IdSite", DbType.Int64, idSite);
            _db.AddInParameter(_dbCommand, "IdCompanyUser", DbType.Int64, idOperator);

            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "HasPermission", DbType.Boolean, 1);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToBoolean(_db.GetParameterValue(_dbCommand, "HasPermission"));
        }

        #endregion

        #region Write Methods

        internal Int64 Create(Int64 idSite, Int64 idOperator, Boolean manage, Boolean load)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteUsers_Create");
            _db.AddInParameter(_dbCommand, "IdSite", DbType.Int64, idSite);
            _db.AddInParameter(_dbCommand, "IdCompanyUser", DbType.Int64, idOperator);
            _db.AddInParameter(_dbCommand, "Manage", DbType.Boolean, manage);
            _db.AddInParameter(_dbCommand, "Load", DbType.Boolean, load);

            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdSiteUser", DbType.Int64, 18);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdSiteUser"));

        }
        internal void CreateBySite(Int64 idSite, DataTable permissions)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            SqlCommand _dbCommand = new SqlCommand("SiteUsers_CreateBySite");
            _dbCommand.CommandType = CommandType.StoredProcedure;

            _db.AddInParameter(_dbCommand, "IdSite", DbType.Int64, idSite);
            
            SqlParameter tvpParam = new SqlParameter("Permissions", permissions);
            tvpParam.SqlDbType = SqlDbType.Structured;
            tvpParam.TypeName = "dbo.SiteUsersPermissions_Type";
            _dbCommand.Parameters.Add(tvpParam);
                        
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

        }
        internal void CreateByUser(Int64 idOperator, DataTable permissions)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            SqlCommand _dbCommand = new SqlCommand("SiteUsers_CreateByUser");
            _dbCommand.CommandType = CommandType.StoredProcedure;

            _db.AddInParameter(_dbCommand, "IdCompanyUser", DbType.Int64, idOperator);

            SqlParameter tvpParam = new SqlParameter("Permissions", permissions);
            tvpParam.SqlDbType = SqlDbType.Structured;
            tvpParam.TypeName = "dbo.SiteUsersPermissions_Type";
            _dbCommand.Parameters.Add(tvpParam);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

        }
        
        internal void Delete(Int64 idSiteUser)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteUsers_Delete");
            _db.AddInParameter(_dbCommand, "IdSiteUser", DbType.Int64, idSiteUser);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void DeleteAllByOperator(Int64 idOperator)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteUsers_DeleteAllByOperator");
            _db.AddInParameter(_dbCommand, "IdCompanyUser", DbType.Int64, idOperator);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
            
        }
        internal void Update(Int64 idSiteUser, Boolean manage, Boolean load)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteUsers_Update");
            _db.AddInParameter(_dbCommand, "IdSiteUser", DbType.Int64, idSiteUser); 
            _db.AddInParameter(_dbCommand, "Manage", DbType.Boolean, manage);
            _db.AddInParameter(_dbCommand, "Load", DbType.Boolean, load);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
            
        }

        #endregion
    }
}
