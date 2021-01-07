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
    internal class FuelMeterSeries
    {
        internal FuelMeterSeries()
        { }

        #region Read Methods

        internal IEnumerable<DbDataRecord> ReadAll(Int64 idMeter, String idLanguage)
        {
            return ReadAll(idMeter, idLanguage, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadAll(Int64 idMeter, String idLanguage, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteFuelMeterSeries_ReadAll");
            _db.AddInParameter(_dbCommand, "IdSiteFuelMeter", DbType.Int64, idMeter);
            if(from != DateTime.MinValue) _db.AddInParameter(_dbCommand, "DateFrom", DbType.Date, from);
            if (to != DateTime.MaxValue) _db.AddInParameter(_dbCommand, "DateTo", DbType.Date, to);
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
        internal IEnumerable<DbDataRecord> ReadById(Int64 idSerie, String idLanguage)
        {
            return ReadById(idSerie, idLanguage, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadById(Int64 idSerie, String idLanguage, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteFuelMeterSeries_ReadByID");
            _db.AddInParameter(_dbCommand, "IdSiteFuelMeterSerie", DbType.Int64, idSerie);
            if(from != DateTime.MinValue) _db.AddInParameter(_dbCommand, "DateFrom", DbType.Date, from);
            if (to != DateTime.MaxValue) _db.AddInParameter(_dbCommand, "DateTo", DbType.Date, to);
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

        internal IEnumerable<DbDataRecord> ReadMagnitudes(Int64 idMeter)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteFuelMeterSeries_ReadMagnitudes");
            _db.AddInParameter(_dbCommand, "IdSiteFuelMeter", DbType.Int64, idMeter);

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
        internal Int64 ReadMagnitude(Int64 idMeter)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteFuelMeterSeries_ReadMagnitude");
            _db.AddInParameter(_dbCommand, "IdSiteFuelMeter", DbType.Int64, idMeter);
            _db.AddOutParameter(_dbCommand, "IdMagnitude", DbType.Int64, 8);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(Auxiliaries.Common.CastValueToNull(_db.GetParameterValue(_dbCommand, "IdMagnitude"), 0));
        }

        #endregion

        #region Write Methods

        internal Int64 Create(Int64 idMeter, DateTime date, Int64 idFuelType, Double value, Double valuePattern, Int64 idUnit, Double emissionFactorValue, Int64 idEmissionFactor, Double totalCO2, Int64 idOperator)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteFuelMeterSeries_Create");

            _db.AddInParameter(_dbCommand, "IdSiteFuelMeter", DbType.Int64, idMeter);
            _db.AddInParameter(_dbCommand, "Date", DbType.Date, date);
            _db.AddInParameter(_dbCommand, "IdFuelType", DbType.Int64, idFuelType);
            _db.AddInParameter(_dbCommand, "Value", DbType.Double, value);
            _db.AddInParameter(_dbCommand, "ValuePattern", DbType.Double, valuePattern);
            _db.AddInParameter(_dbCommand, "IdUnit", DbType.Int64, idUnit);
            _db.AddInParameter(_dbCommand, "EF", DbType.Double, emissionFactorValue);
            _db.AddInParameter(_dbCommand, "IdFuelTypeEmissionFactor", DbType.Int64, idEmissionFactor);
            _db.AddInParameter(_dbCommand, "TotalCO2", DbType.Double, totalCO2);
            _db.AddInParameter(_dbCommand, "IdCompanyUser", DbType.Int64, idOperator);

            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdSiteFuelMeterSerie", DbType.Int64, 18);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdSiteFuelMeterSerie"));

        }
        internal void Delete(Int64 IdSerie)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteFuelMeterSeries_Delete");
            _db.AddInParameter(_dbCommand, "IdSiteFuelMeterSerie", DbType.Int64, IdSerie);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void DeleteAll(Int64 IdMeter)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteFuelMeterSeries_DeleteAll");
            _db.AddInParameter(_dbCommand, "IdSiteFuelMeter", DbType.Int64, IdMeter);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Update(Int64 IdSerie, DateTime date, Int64 idFuelType, Double value, Double valuePattern, Int64 idUnit, Double emissionFactorValue, Int64 idEmissionFactor, Double totalCO2, Int64 idOperator)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteFuelMeterSeries_Update");            
           
            _db.AddInParameter(_dbCommand, "IdSiteFuelMeterSerie", DbType.Int64, IdSerie);
            _db.AddInParameter(_dbCommand, "Date", DbType.Date, date);
            _db.AddInParameter(_dbCommand, "IdFuelType", DbType.Int64, idFuelType);
            _db.AddInParameter(_dbCommand, "Value", DbType.Double, value);
            _db.AddInParameter(_dbCommand, "ValuePattern", DbType.Double, valuePattern);
            _db.AddInParameter(_dbCommand, "IdUnit", DbType.Int64, idUnit);
            _db.AddInParameter(_dbCommand, "EF", DbType.Double, emissionFactorValue);
            _db.AddInParameter(_dbCommand, "IdFuelTypeEmissionFactor", DbType.Int64, idEmissionFactor);
            _db.AddInParameter(_dbCommand, "TotalCO2", DbType.Double, totalCO2);
            _db.AddInParameter(_dbCommand, "IdCompanyUser", DbType.Int64, idOperator);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

        }


        #endregion
    }
}
