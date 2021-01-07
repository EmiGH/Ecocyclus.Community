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
    internal class TransportMeterEmissionFactors
    {
        internal TransportMeterEmissionFactors() { }

        #region Read Methods

        internal IEnumerable<DbDataRecord> ReadAll(Int64 idSite, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteTransportMeterEmissionFactors_ReadAll");
            _db.AddInParameter(_dbCommand, "IdSiteTransportMeter", DbType.Int64, idSite);
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
        internal IEnumerable<DbDataRecord> ReadById(Int64 idMeterEmissionFactor, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteTransportMeterEmissionFactors_ReadById");
            _db.AddInParameter(_dbCommand, "IdSiteTransportMeterEmissionFactor", DbType.Int64, idMeterEmissionFactor);
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
        internal IEnumerable<DbDataRecord> ReadById(Int64 idMeter, Int64 idTransportType, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteTransportMeterEmissionFactors_ReadByMeterAndTransportType");
            _db.AddInParameter(_dbCommand, "IdSiteTransportMeter", DbType.Int64, idMeter);
            _db.AddInParameter(_dbCommand, "IdTransportType", DbType.Int64, idTransportType);
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

        #endregion

        #region Write Methods

        internal Int64 Create(Int64 idMeter, Int64 idEmissionFactor)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteTransportMeterEmissionFactor_Create");
            _db.AddInParameter(_dbCommand, "IdSiteTransportMeter", DbType.Int64, idMeter);
            _db.AddInParameter(_dbCommand, "IdTransportTypeEmissionFactor", DbType.Int64, idEmissionFactor);

            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdSiteTransportMeterEmissionFactor", DbType.Int64, 18);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdSiteTransportMeterEmissionFactor"));

        }
        internal void Delete(Int64 idMeterEmissionFactor)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteTransportMeterEmissionFactor_Delete");
            _db.AddInParameter(_dbCommand, "IdSiteTransportMeterEmissionFactor", DbType.Int64, idMeterEmissionFactor);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Update(Int64 idMeterEmissionFactor, Int64 idEmissionFactor)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteTransportMeterEmissionFactor_Update");
            _db.AddInParameter(_dbCommand, "IdSiteTransportMeterEmissionFactor", DbType.Int64, idMeterEmissionFactor);
            _db.AddInParameter(_dbCommand, "IdTransportTypeEmissionFactor", DbType.Int64, idEmissionFactor);


            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }


        #endregion
    }
}