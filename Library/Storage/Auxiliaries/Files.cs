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
    internal class Files
    {
        internal Files() { }

        #region Read Methods

        internal IEnumerable<DbDataRecord> ReadAll()
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Files_ReadAll");
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
        internal IEnumerable<DbDataRecord> ReadById(Int64 idFile)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Files_ReadById");
            _db.AddInParameter(_dbCommand, "IdFile", DbType.Int64, idFile);
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
        internal IEnumerable<DbDataRecord> ReadStream(Int64 idFile)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Files_ReadStream");
            _db.AddInParameter(_dbCommand, "IdFile", DbType.Int64, idFile);
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
        
        internal Int64 Create(String name, String type, Byte[] content)
        {
            Database _db = DatabaseFactory.CreateDatabase();


            DbCommand _dbCommand = _db.GetStoredProcCommand("Files_Create");
            _db.AddInParameter(_dbCommand, "Name", DbType.String, name);
            _db.AddInParameter(_dbCommand, "Type", DbType.String, type);
            _db.AddInParameter(_dbCommand, "Content", DbType.Binary, content);

            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdFile", DbType.Int64, 18);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdFile"));

        }
        internal void Delete(Int64 idFile)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Files_Delete");
            _db.AddInParameter(_dbCommand, "IdFile", DbType.Int64, idFile);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        
        #endregion


    }
}
