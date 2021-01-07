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
    internal class FuelMeters
    {
        internal FuelMeters() { }

        #region Read Methods

        internal IEnumerable<DbDataRecord> ReadAll(Int64 idSite, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteFuelMeters_ReadAll");
            _db.AddInParameter(_dbCommand, "IdSite", DbType.Int64, idSite);
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
        internal IEnumerable<DbDataRecord> ReadById(Int64 idMeter, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteFuelMeters_ReadById");
            _db.AddInParameter(_dbCommand, "IdSiteFuelMeter", DbType.Int64, idMeter);
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

        internal DateTime? LastDate(Int64 idMeter)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteFuelMeters_ReadLastDate");
            _db.AddInParameter(_dbCommand, "IdSiteFuelMeter", DbType.Int64, idMeter);
            _db.AddOutParameter(_dbCommand, "LastDate", DbType.Date, 8);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            try
            {
                return Convert.ToDateTime(_db.GetParameterValue(_dbCommand, "LastDate"));
            }
            catch
            {
                return null;
            }

        }

        #endregion

        #region Write Methods

        internal Int64 Create(Int64 idSite, String idLanguage, String identification, String description, Int64 idDefaultUnit)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteFuelMeters_Create");
            _db.AddInParameter(_dbCommand, "IdSite", DbType.Int64, idSite);
            _db.AddInParameter(_dbCommand, "Identification", DbType.String, identification);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "Description", DbType.String, description);
            _db.AddInParameter(_dbCommand, "IdDefaultUnit", DbType.Int64, idDefaultUnit);

            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdSiteFuelMeter", DbType.Int64, 18);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdSiteFuelMeter"));

        }
        internal void Delete(Int64 idSiteFuelMeter)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteFuelMeters_Delete");
            _db.AddInParameter(_dbCommand, "IdSiteFuelMeter", DbType.Int64, idSiteFuelMeter);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Update(Int64 idSiteFuelMeter, String idLanguage, String identification, String description, Int64 idDefaultUnit)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteFuelMeters_Update");
            _db.AddInParameter(_dbCommand, "IdSiteFuelMeter", DbType.Int64, idSiteFuelMeter);
            _db.AddInParameter(_dbCommand, "Identification", DbType.String, identification);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "Description", DbType.String, description);
            _db.AddInParameter(_dbCommand, "IdDefaultUnit", DbType.Int64, idDefaultUnit);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        #endregion
    }
}
