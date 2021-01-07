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
    internal class SitePayments
    {
        internal SitePayments() { }

        #region Read Methods

        internal IEnumerable<DbDataRecord> ReadAll(String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SitePayments_ReadAll");
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
        internal IEnumerable<DbDataRecord> ReadApproved(String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SitePayments_ReadApproved");
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
        internal IEnumerable<DbDataRecord> ReadBySite(Int64 idSite, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SitePayments_ReadBySite");
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
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
        internal IEnumerable<DbDataRecord> ReadApprovedBySite(Int64 idSite, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SitePayments_ReadApprovedBySite");
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
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
        internal IEnumerable<DbDataRecord> ReadById(Int64 idSitePayment, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SitePayments_ReadById");
            _db.AddInParameter(_dbCommand, "IdSitePayment", DbType.Int64, idSitePayment);
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

        internal Int64 Create(Int64 idSite, Int64 idOperator, DateTime from, DateTime to, Double amount, Int64 idCurrency, String idTransaction, String data)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SitePayments_Create");
            _db.AddInParameter(_dbCommand, "IdSite", DbType.Int64, idSite);
            _db.AddInParameter(_dbCommand, "IdCompanyUser", DbType.Int64, idOperator);
            _db.AddInParameter(_dbCommand, "From", DbType.DateTime, from);
            _db.AddInParameter(_dbCommand, "To", DbType.DateTime, to);
            _db.AddInParameter(_dbCommand, "Amount", DbType.Currency, amount);
            _db.AddInParameter(_dbCommand, "IdCurrency", DbType.Int64, idCurrency);
            _db.AddInParameter(_dbCommand, "IdTransaction", DbType.String, idTransaction);
            _db.AddInParameter(_dbCommand, "Data", DbType.String, data);

            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdSitePayment", DbType.Int64, 16);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdSitePayment"));

        }
        internal void Update(Int64 idPayment, String idTransaction, Boolean confirmed, DateTime confirmedDate, String confirmedMessage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SitePayments_Update");
            _db.AddInParameter(_dbCommand, "IdSitePayment", DbType.Int64, idPayment);
            _db.AddInParameter(_dbCommand, "IdTransaction", DbType.String, idTransaction);
            _db.AddInParameter(_dbCommand, "Confirmed", DbType.Boolean, confirmed);
            _db.AddInParameter(_dbCommand, "ConfirmedDate", DbType.DateTime, confirmedDate);
            _db.AddInParameter(_dbCommand, "ConfirmedMessage", DbType.String, confirmedMessage);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

        }

        #endregion
    }
}
