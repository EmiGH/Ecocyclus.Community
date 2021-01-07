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
    internal class SitePaymentScales
    {
        internal SitePaymentScales() { }

        #region Read Methods

        internal IEnumerable<DbDataRecord> ReadAll(String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SitePaymentScales_ReadAll");
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
        internal IEnumerable<DbDataRecord> ReadByCountry(Int64 idCountry, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SitePaymentScales_ReadByCountry");
            _db.AddInParameter(_dbCommand, "IdCountry", DbType.Int64, idCountry);
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
        internal IEnumerable<DbDataRecord> ReadGlobal(String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SitePaymentScales_ReadGlobal");
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
        internal IEnumerable<DbDataRecord> ReadById(Int64 idSitePaymentScale, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SitePaymentScales_ReadById");
            _db.AddInParameter(_dbCommand, "IdSitePaymentScale", DbType.Int64, idSitePaymentScale);
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
        internal IEnumerable<DbDataRecord> FindMatch(Int64 idCountry, Double siteValue, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SitePaymentScales_Match");
            _db.AddInParameter(_dbCommand, "IdCountry", DbType.Int64, idCountry);
            _db.AddInParameter(_dbCommand, "SiteValue", DbType.Double, siteValue);
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

        internal Int64 Create(Int64 idCountry, Int64 minValue, Int64 maxValue, Double amount, Int64 idCurrency, Int32 monthsFree, Double firstPayment)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SitePaymentScales_Create");
            _db.AddInParameter(_dbCommand, "IdCountry", DbType.Int64, idCountry);
            _db.AddInParameter(_dbCommand, "MinValue", DbType.Currency, minValue);
            _db.AddInParameter(_dbCommand, "MaxValue", DbType.Currency, maxValue);
            _db.AddInParameter(_dbCommand, "IdCurrency", DbType.Int64, idCurrency);
            _db.AddInParameter(_dbCommand, "MonthsFree", DbType.Int32, monthsFree);
            _db.AddInParameter(_dbCommand, "FirstPayment", DbType.Double, firstPayment);

            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdSitePaymentScale", DbType.Int64, 18);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdSitePaymentScale"));

        }
        internal Int64 Create(Int64 minValue, Int64 maxValue, Double amount, Int64 idCurrency, Int32 monthsFree, Double firstPayment)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SitePaymentScales_Create");
            _db.AddInParameter(_dbCommand, "MinValue", DbType.Currency, minValue);
            _db.AddInParameter(_dbCommand, "MaxValue", DbType.Currency, maxValue);
            _db.AddInParameter(_dbCommand, "IdCurrency", DbType.Int64, idCurrency);
            _db.AddInParameter(_dbCommand, "MonthsFree", DbType.Int32, monthsFree);
            _db.AddInParameter(_dbCommand, "FirstPayment", DbType.Double, firstPayment);

            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdSitePaymentScale", DbType.Int64, 18);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdSitePaymentScale"));

        }
        internal void Delete(Int64 idPaymentScale)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SitePaymentScales_Delete");
            _db.AddInParameter(_dbCommand, "IdSitePaymentScale", DbType.Int64, idPaymentScale);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Update(Int64 idPaymentScale, Int64 idCountry, Int64 minValue, Int64 maxValue, Double amount, Int64 idCurrency, Int32 monthsFree, Double firstPayment)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SitePaymentScaless_Update");
            _db.AddInParameter(_dbCommand, "IdSitePaymentScale", DbType.Int64, idPaymentScale);
            _db.AddInParameter(_dbCommand, "IdCountry", DbType.Int64, idCountry);
            _db.AddInParameter(_dbCommand, "MinValue", DbType.Currency, minValue);
            _db.AddInParameter(_dbCommand, "MaxValue", DbType.Currency, maxValue);
            _db.AddInParameter(_dbCommand, "IdCurrency", DbType.Int64, idCurrency);;
            _db.AddInParameter(_dbCommand, "MonthsFree", DbType.Int32, monthsFree);
            _db.AddInParameter(_dbCommand, "FirstPayment", DbType.Double, firstPayment);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Update(Int64 idPaymentScale, Int64 minValue, Int64 maxValue, Double amount, Int64 idCurrency, Int32 monthsFree, Double firstPayment)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SitePaymentScaless_Update");
            _db.AddInParameter(_dbCommand, "IdSitePaymentScale", DbType.Int64, idPaymentScale);
            _db.AddInParameter(_dbCommand, "MinValue", DbType.Currency, minValue);
            _db.AddInParameter(_dbCommand, "MaxValue", DbType.Currency, maxValue);
            _db.AddInParameter(_dbCommand, "IdCurrency", DbType.Int64, idCurrency);
            _db.AddInParameter(_dbCommand, "MonthsFree", DbType.Int32, monthsFree);
            _db.AddInParameter(_dbCommand, "FirstPayment", DbType.Double, firstPayment);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        #endregion
    }
}
