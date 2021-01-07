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
    internal class ElectricityMeterLoads
    {
        internal ElectricityMeterLoads() { }

        #region Read Methods

        internal IEnumerable<DbDataRecord> ReadAll(Int64 idMeter, String idLanguage)
        {
            return ReadAll(idMeter, idLanguage, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadAll(Int64 idMeter, String idLanguage, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteElectricityMeterLoads_ReadAll");
            _db.AddInParameter(_dbCommand, "IdSiteElectricityMeter", DbType.Int64, idMeter);
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
        internal IEnumerable<DbDataRecord> ReadById(Int64 idLoad, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteElectricityMeterLoads_ReadByID");
            _db.AddInParameter(_dbCommand, "IdSiteElectricityMeterLoad", DbType.Int64, idLoad);
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
        internal IEnumerable<DbDataRecord> ReadPrevious(Int64 idMeter, Int64 idLoad, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteElectricityMeterLoads_ReadPreviousID");
            _db.AddInParameter(_dbCommand, "IdSiteElectricityMeter", DbType.Int64, idMeter);
            _db.AddInParameter(_dbCommand, "IdSiteElectricityMeterLoad", DbType.Int64, idLoad);
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
        internal IEnumerable<DbDataRecord> ReadNext(Int64 idMeter, Int64 idLoad, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteElectricityMeterLoads_ReadNextID");
            _db.AddInParameter(_dbCommand, "IdSiteElectricityMeter", DbType.Int64, idMeter);
            _db.AddInParameter(_dbCommand, "IdSiteElectricityMeterLoad", DbType.Int64, idLoad);
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
        internal IEnumerable<DbDataRecord> ReadFirst(Int64 idMeter, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteElectricityMeterLoads_ReadFirst");
            _db.AddInParameter(_dbCommand, "IdSiteElectricityMeter", DbType.Int64, idMeter);
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

        internal Int64 Create(Int64 idMeter, Int64 idOperator, DateTime from, DateTime to, Double value, Double valueInput, Int64 idUnit, Double emissionFactorValue, Int64 idEmissionFactor)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteElectricityMeterLoads_Create");
            _db.AddInParameter(_dbCommand, "IdSiteElectricityMeter", DbType.Int64, idMeter);
            _db.AddInParameter(_dbCommand, "IdCompanyUser", DbType.Int64, idOperator);
            if(from != DateTime.MinValue) _db.AddInParameter(_dbCommand, "DateFrom", DbType.Date, from);
            if(to != DateTime.MaxValue) _db.AddInParameter(_dbCommand, "DateTo", DbType.Date, to);
            _db.AddInParameter(_dbCommand, "Value", DbType.Double, value);
            _db.AddInParameter(_dbCommand, "ValueInput", DbType.Double, valueInput);
            _db.AddInParameter(_dbCommand, "IdUnit", DbType.Int64, idUnit);
            _db.AddInParameter(_dbCommand, "EF", DbType.Double, emissionFactorValue);
            _db.AddInParameter(_dbCommand, "IdEmissionFactor", DbType.Int64, idEmissionFactor);

            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdSiteElectricityMeterLoad", DbType.Int64, 18);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdSiteElectricityMeterLoad"));
        }
        internal void Delete(Int64 idMeter, DateTime from)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteElectricityMeterLoads_Delete");
            _db.AddInParameter(_dbCommand, "IdSiteElectricityMeter", DbType.Int64, idMeter);
            if(from!=DateTime.MinValue)
                if(from != DateTime.MinValue) _db.AddInParameter(_dbCommand, "DateFrom", DbType.Date, from);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Update(Int64 idLoad, Int64 idOperator, Double value, Double valueInput, Int64 idUnit, Double emissionFactorValue, Int64 idEmissionFactor)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteElectricityMeterLoads_Update");
            _db.AddInParameter(_dbCommand, "IdSiteElectricityMeterLoad", DbType.Int64, idLoad);
            _db.AddInParameter(_dbCommand, "IdCompanyUser", DbType.Int64, idOperator);
            _db.AddInParameter(_dbCommand, "Value", DbType.Double, value);
            _db.AddInParameter(_dbCommand, "ValueInput", DbType.Double, valueInput);
            _db.AddInParameter(_dbCommand, "IdUnit", DbType.Int64, idUnit);
            _db.AddInParameter(_dbCommand, "EF", DbType.Double, emissionFactorValue);
            _db.AddInParameter(_dbCommand, "IdEmissionFactor", DbType.Int64, idEmissionFactor);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        
        #endregion
    }
}
