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
    internal class WasteMeterEmissionFactors
    {
        internal WasteMeterEmissionFactors() { }

        #region Read Methods

        internal IEnumerable<DbDataRecord> ReadAll(Int64 idSite, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteWasteMeterEmissionFactors_ReadAll");
            _db.AddInParameter(_dbCommand, "IdSiteWasteMeter", DbType.Int64, idSite);
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

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteWasteMeterEmissionFactors_ReadById");
            _db.AddInParameter(_dbCommand, "IdSiteWasteMeterEmissionFactor", DbType.Int64, idMeterEmissionFactor);
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
        internal IEnumerable<DbDataRecord> ReadById(Int64 idMeter, Int64 idWasteType, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteWasteMeterEmissionFactors_ReadByMeterAndWasteType");
            _db.AddInParameter(_dbCommand, "IdSiteWasteMeter", DbType.Int64, idMeter);
            _db.AddInParameter(_dbCommand, "IdWasteType", DbType.Int64, idWasteType);
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

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteWasteMeterEmissionFactor_Create");
            _db.AddInParameter(_dbCommand, "IdSiteWasteMeter", DbType.Int64, idMeter);
            _db.AddInParameter(_dbCommand, "IdWasteTypeEmissionFactor", DbType.Int64, idEmissionFactor);

            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdSiteWasteMeterEmissionFactor", DbType.Int64, 18);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdSiteWasteMeterEmissionFactor"));

        }
        internal void Delete(Int64 idMeterEmissionFactor)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteWasteMeterEmissionFactor_Delete");
            _db.AddInParameter(_dbCommand, "IdSiteWasteMeterEmissionFactor", DbType.Int64, idMeterEmissionFactor);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Update(Int64 idMeterEmissionFactor, Int64 idEmissionFactor)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteWasteMeterEmissionFactor_Update");
            _db.AddInParameter(_dbCommand, "IdSiteWasteMeterEmissionFactor", DbType.Int64, idMeterEmissionFactor);
            _db.AddInParameter(_dbCommand, "IdWasteTypeEmissionFactor", DbType.Int64, idEmissionFactor);


            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }


        #endregion
    }
}
