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
    internal class WaterMeters
    {
        internal WaterMeters() { }
        
        #region Read Methods

        internal IEnumerable<DbDataRecord> ReadAll(Int64 idSite, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteWaterMeters_ReadAll");
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

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteWaterMeters_ReadById");
            _db.AddInParameter(_dbCommand, "IdSiteWaterMeter", DbType.Int64, idMeter);
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

        internal Boolean HasValues(Int64 idMeter)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteWaterMeters_ReadHasValue");
            _db.AddInParameter(_dbCommand, "IdSiteWaterMeter", DbType.Int64, idMeter);
            _db.AddOutParameter(_dbCommand, "HasValue", DbType.Boolean, 1);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToBoolean(_db.GetParameterValue(_dbCommand, "HasValue"));
        }
        internal DateTime? NextDate(Int64 idMeter)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteWaterMeters_ReadNextDate");
            _db.AddInParameter(_dbCommand, "IdSiteWaterMeter", DbType.Int64, idMeter);
            _db.AddOutParameter(_dbCommand, "NextDate", DbType.Date, 8);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            try
            {
                return Convert.ToDateTime(_db.GetParameterValue(_dbCommand, "NextDate"));
            }
            catch 
            {
                return null;
            }
        }
        internal DateTime? LastDate(Int64 idMeter)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteWaterMeters_ReadLastDate");
            _db.AddInParameter(_dbCommand, "IdSiteWaterMeter", DbType.Int64, idMeter);
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
        internal Double LastReading(Int64 idMeter)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteWaterMeters_ReadLastReading");
            _db.AddInParameter(_dbCommand, "IdSiteWaterMeter", DbType.Int64, idMeter);
            _db.AddOutParameter(_dbCommand, "LastReading", DbType.Double, 8);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToDouble(Handlers.Common.CastNullValues(_db.GetParameterValue(_dbCommand, "LastReading"), 0));
        }

        #endregion

        #region Write Methods

        internal Int64 Create(Int64 idSite, String idLanguage, String identification, String description, Boolean isPhysical, DateTime initialDate, Double initialReading, Int64 idEmissionFactor, Int64 idUnit, Int16 frequencyQuantity, Int16 frequencyUnit, Int16 alertBefore, Int16 alertAfter, Boolean alertOnStart)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteWaterMeters_Create");
            _db.AddInParameter(_dbCommand, "IdSite", DbType.Int64, idSite);
            _db.AddInParameter(_dbCommand, "Identification", DbType.String, identification);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "Description", DbType.String, description);
            _db.AddInParameter(_dbCommand, "IsPhysicalMeter", DbType.Boolean, isPhysical);
            _db.AddInParameter(_dbCommand, "IdEmissionFactor", DbType.Int64, idEmissionFactor);
            if (isPhysical)
            {
                _db.AddInParameter(_dbCommand, "InitialDate", DbType.Date, initialDate);
                _db.AddInParameter(_dbCommand, "InitialReading", DbType.Double, initialReading);
            }
            _db.AddInParameter(_dbCommand, "IdUnit", DbType.Int64, idUnit);
            _db.AddInParameter(_dbCommand, "FrequencyQuantity", DbType.Int16, frequencyQuantity);
            _db.AddInParameter(_dbCommand, "FrequencyUnit", DbType.Int16, frequencyUnit);
            _db.AddInParameter(_dbCommand, "AlertBeforeDays", DbType.Int16, alertBefore);
            _db.AddInParameter(_dbCommand, "AlertAfterDays", DbType.Int16, alertAfter);
            _db.AddInParameter(_dbCommand, "AlertOnStart", DbType.Boolean, alertOnStart);
            
            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdSiteWaterMeter", DbType.Int64, 18);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdSiteWaterMeter"));

        }
        internal Int64 Create(Int64 idSite, String idLanguage, String identification, String description, Boolean isPhysical, DateTime initialDate, Double initialReading, Int64 idUnit, Int16 frequencyQuantity, Int16 frequencyUnit, Int16 alertBefore, Int16 alertAfter, Boolean alertOnStart)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteWaterMeters_Create");
            _db.AddInParameter(_dbCommand, "IdSite", DbType.Int64, idSite);
            _db.AddInParameter(_dbCommand, "Identification", DbType.String, identification);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "Description", DbType.String, description);
            _db.AddInParameter(_dbCommand, "IsPhysicalMeter", DbType.Boolean, isPhysical);
            if (isPhysical)
            {
                _db.AddInParameter(_dbCommand, "InitialDate", DbType.Date, initialDate);
                _db.AddInParameter(_dbCommand, "InitialReading", DbType.Double, initialReading);
            }
            _db.AddInParameter(_dbCommand, "IdUnit", DbType.Int64, idUnit);
            _db.AddInParameter(_dbCommand, "FrequencyQuantity", DbType.Int16, frequencyQuantity);
            _db.AddInParameter(_dbCommand, "FrequencyUnit", DbType.Int16, frequencyUnit);
            _db.AddInParameter(_dbCommand, "AlertBeforeDays", DbType.Int16, alertBefore);
            _db.AddInParameter(_dbCommand, "AlertAfterDays", DbType.Int16, alertAfter);
            _db.AddInParameter(_dbCommand, "AlertOnStart", DbType.Boolean, alertOnStart);

            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdSiteWaterMeter", DbType.Int64, 18);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdSiteWaterMeter"));

        }
        internal void Delete(Int64 idMeter)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteWaterMeters_Delete");
            _db.AddInParameter(_dbCommand, "IdSiteWaterMeter", DbType.Int64, idMeter);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Update(Int64 idMeter, String idLanguage, String identification, String description, Int64 idEmissionFactor, Int64 idUnit, Int16 frequencyQuantity, Int16 frequencyUnit, Int16 alertBefore, Int16 alertAfter, Boolean alertOnStart)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteWaterMeters_Update");
            _db.AddInParameter(_dbCommand, "IdSiteWaterMeter", DbType.Int64, idMeter);
            _db.AddInParameter(_dbCommand, "Identification", DbType.String, identification);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "Description", DbType.String, description);
            _db.AddInParameter(_dbCommand, "IsPhysicalMeter", DbType.Boolean, false);
            _db.AddInParameter(_dbCommand, "IdEmissionFactor", DbType.Int64, idEmissionFactor);
            _db.AddInParameter(_dbCommand, "IdUnit", DbType.Int64, idUnit);
            _db.AddInParameter(_dbCommand, "FrequencyQuantity", DbType.Int16, frequencyQuantity);
            _db.AddInParameter(_dbCommand, "FrequencyUnit", DbType.Int16, frequencyUnit);
            _db.AddInParameter(_dbCommand, "AlertBeforeDays", DbType.Int16, alertBefore);
            _db.AddInParameter(_dbCommand, "AlertAfterDays", DbType.Int16, alertAfter);
            _db.AddInParameter(_dbCommand, "AlertOnStart", DbType.Boolean, alertOnStart);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Update(Int64 idMeter, String idLanguage, String identification, String description, Int64 idUnit, Int16 frequencyQuantity, Int16 frequencyUnit, Int16 alertBefore, Int16 alertAfter, Boolean alertOnStart)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteWaterMeters_Update");
            _db.AddInParameter(_dbCommand, "IdSiteWaterMeter", DbType.Int64, idMeter);
            _db.AddInParameter(_dbCommand, "Identification", DbType.String, identification);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "Description", DbType.String, description);
            _db.AddInParameter(_dbCommand, "IsPhysicalMeter", DbType.Boolean, false);
            _db.AddInParameter(_dbCommand, "IdUnit", DbType.Int64, idUnit);
            _db.AddInParameter(_dbCommand, "FrequencyQuantity", DbType.Int16, frequencyQuantity);
            _db.AddInParameter(_dbCommand, "FrequencyUnit", DbType.Int16, frequencyUnit);
            _db.AddInParameter(_dbCommand, "AlertBeforeDays", DbType.Int16, alertBefore);
            _db.AddInParameter(_dbCommand, "AlertAfterDays", DbType.Int16, alertAfter);
            _db.AddInParameter(_dbCommand, "AlertOnStart", DbType.Boolean, alertOnStart);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Update(Int64 idMeter, String idLanguage, String identification, String description, DateTime initialDate, Double initialReading, Int64 idEmissionFactor, Int64 idUnit, Int16 frequencyQuantity, Int16 frequencyUnit, Int16 alertBefore, Int16 alertAfter, Boolean alertOnStart)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteWaterMeters_Update");
            _db.AddInParameter(_dbCommand, "IdSiteWaterMeter", DbType.Int64, idMeter);
            _db.AddInParameter(_dbCommand, "Identification", DbType.String, identification);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "Description", DbType.String, description);
            _db.AddInParameter(_dbCommand, "IsPhysicalMeter", DbType.Boolean, true);
            _db.AddInParameter(_dbCommand, "InitialReading", DbType.Double, initialReading);
            _db.AddInParameter(_dbCommand, "InitialDate", DbType.Date, initialDate);
            _db.AddInParameter(_dbCommand, "IdEmissionFactor", DbType.Int64, idEmissionFactor);
            _db.AddInParameter(_dbCommand, "IdUnit", DbType.Int64, idUnit);
            _db.AddInParameter(_dbCommand, "FrequencyQuantity", DbType.Int16, frequencyQuantity);
            _db.AddInParameter(_dbCommand, "FrequencyUnit", DbType.Int16, frequencyUnit);
            _db.AddInParameter(_dbCommand, "AlertBeforeDays", DbType.Int16, alertBefore);
            _db.AddInParameter(_dbCommand, "AlertAfterDays", DbType.Int16, alertAfter);
            _db.AddInParameter(_dbCommand, "AlertOnStart", DbType.Boolean, alertOnStart);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Update(Int64 idMeter, String idLanguage, String identification, String description, DateTime initialDate, Double initialReading, Int64 idUnit, Int16 frequencyQuantity, Int16 frequencyUnit, Int16 alertBefore, Int16 alertAfter, Boolean alertOnStart)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteWaterMeters_Update");
            _db.AddInParameter(_dbCommand, "IdSiteWaterMeter", DbType.Int64, idMeter);
            _db.AddInParameter(_dbCommand, "Identification", DbType.String, identification);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "Description", DbType.String, description);
            _db.AddInParameter(_dbCommand, "IsPhysicalMeter", DbType.Boolean, true);
            _db.AddInParameter(_dbCommand, "InitialDate", DbType.Date, initialDate);
            _db.AddInParameter(_dbCommand, "InitialReading", DbType.Double, initialReading);
            _db.AddInParameter(_dbCommand, "IdUnit", DbType.Int64, idUnit);
            _db.AddInParameter(_dbCommand, "FrequencyQuantity", DbType.Int16, frequencyQuantity);
            _db.AddInParameter(_dbCommand, "FrequencyUnit", DbType.Int16, frequencyUnit);
            _db.AddInParameter(_dbCommand, "AlertBeforeDays", DbType.Int16, alertBefore);
            _db.AddInParameter(_dbCommand, "AlertAfterDays", DbType.Int16, alertAfter);
            _db.AddInParameter(_dbCommand, "AlertOnStart", DbType.Boolean, alertOnStart);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }


        #endregion
    }
}
