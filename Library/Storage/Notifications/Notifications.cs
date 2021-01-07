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
    internal class Notifications
    {
        internal Notifications() { }

        #region Read Methods

        internal IEnumerable<DbDataRecord> Configuration()
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Configuration_ReadAll");
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
        internal IEnumerable<DbDataRecord> ReadById(Int64 idNotification)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Notifications_ReadById");
            _db.AddInParameter(_dbCommand, "IdNotification", DbType.Int64, idNotification);
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
        internal IEnumerable<DbDataRecord> ReadAll()
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Notifications_ReadAll");
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

        internal Int64 Create(Int64 idUser, String title, String message)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Notifications_Create");
            _db.AddInParameter(_dbCommand, "IdUser", DbType.Int64, idUser);
            _db.AddInParameter(_dbCommand, "Title", DbType.String, title);
            _db.AddInParameter(_dbCommand, "Message", DbType.String, message);

            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdNotification", DbType.Int64, 16);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdNotification"));

        }
        internal void Delete(DataTable ids)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            SqlCommand _dbCommand = new SqlCommand("Notifications_Delete");
            _dbCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter tvpParam = new SqlParameter("Notifications", ids);
            tvpParam.SqlDbType = SqlDbType.Structured;
            tvpParam.TypeName = "dbo.Notifications_Type";
            _dbCommand.Parameters.Add(tvpParam);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        #endregion
    }
}
