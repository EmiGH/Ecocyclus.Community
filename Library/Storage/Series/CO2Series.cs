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
    internal class CO2Series
    {
        internal CO2Series() { }

        #region Read Methods
        
        #region By Company

        internal Objects.Auxiliaries.Units.TimeRange ReadCompanyDatesRange(Int64 idCompany)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            //Read initial date for CO2 data
            DbCommand _dbCommand = _db.GetStoredProcCommand("CompanyCO2Series_ReadDatesRange");
            _db.AddInParameter(_dbCommand, "IdCompany", DbType.Int64, idCompany);
            _db.AddOutParameter(_dbCommand, "DateFrom", DbType.Date, 8);
            _db.AddOutParameter(_dbCommand, "DateTo", DbType.Date, 8);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el rango
            return new Objects.Auxiliaries.Units.TimeRange(Convert.ToDateTime(Handlers.Common.CastNullValues(_db.GetParameterValue(_dbCommand, "DateFrom"), DateTime.Now)), Convert.ToDateTime(Handlers.Common.CastNullValues(_db.GetParameterValue(_dbCommand, "DateTo"), DateTime.Now)));
        }
        
        internal IEnumerable<DbDataRecord> ReadCompanyStatistics(Int64 idCompany)
        {
            return ReadCompanyStatistics(idCompany, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadCompanyStatistics(Int64 idCompany, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("CompanyCO2Series_Statistics");
            _db.AddInParameter(_dbCommand, "IdCompany", DbType.Int64, idCompany);
            if(from != DateTime.MinValue) _db.AddInParameter(_dbCommand, "DateFrom", DbType.Date, from);
            if(to != DateTime.MaxValue) _db.AddInParameter(_dbCommand, "DateTo", DbType.Date, to);
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
        internal IEnumerable<DbDataRecord> ReadCompanyStatisticsByProtocols(Int64 idCompany)
        {
            return ReadCompanyStatisticsByProtocols(idCompany, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadCompanyStatisticsByProtocols(Int64 idCompany, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("CompanyCO2Series_StatisticsByProtocols");
            _db.AddInParameter(_dbCommand, "IdCompany", DbType.Int64, idCompany);
            if(from != DateTime.MinValue) _db.AddInParameter(_dbCommand, "DateFrom", DbType.Date, from);
            if(to != DateTime.MaxValue) _db.AddInParameter(_dbCommand, "DateTo", DbType.Date, to);
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
        internal IEnumerable<DbDataRecord> ReadCompanyStatisticsBySites(Int64 idCompany)
        {
            return ReadCompanyStatisticsBySites(idCompany, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadCompanyStatisticsBySites(Int64 idCompany, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("CompanyCO2Series_StatisticsBySites");
            _db.AddInParameter(_dbCommand, "IdCompany", DbType.Int64, idCompany);
            if(from != DateTime.MinValue) _db.AddInParameter(_dbCommand, "DateFrom", DbType.Date, from);
            if(to != DateTime.MaxValue) _db.AddInParameter(_dbCommand, "DateTo", DbType.Date, to);
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

        internal IEnumerable<DbDataRecord> ReadCompanyDaily(Int64 idCompany)
        {
            return ReadCompanyDaily(idCompany, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadCompanyDaily(Int64 idCompany, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("CompanyCO2Series_ReadDaily");
            _db.AddInParameter(_dbCommand, "IdCompany", DbType.Int64, idCompany);
            if(from != DateTime.MinValue) _db.AddInParameter(_dbCommand, "DateFrom", DbType.Date, from);
            if(to != DateTime.MaxValue) _db.AddInParameter(_dbCommand, "DateTo", DbType.Date, to);
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
        internal IEnumerable<DbDataRecord> ReadCompanyWeekly(Int64 idCompany)
        {
            return ReadCompanyWeekly(idCompany, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadCompanyWeekly(Int64 idCompany, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("CompanyCO2Series_ReadWeekly");
            _db.AddInParameter(_dbCommand, "IdCompany", DbType.Int64, idCompany);
            if(from != DateTime.MinValue) _db.AddInParameter(_dbCommand, "DateFrom", DbType.Date, from);
            if(to != DateTime.MaxValue) _db.AddInParameter(_dbCommand, "DateTo", DbType.Date, to);
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
        internal IEnumerable<DbDataRecord> ReadCompanyMonthly(Int64 idCompany)
        {
            return ReadCompanyMonthly(idCompany, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadCompanyMonthly(Int64 idCompany, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("CompanyCO2Series_ReadMonthly");
            _db.AddInParameter(_dbCommand, "IdCompany", DbType.Int64, idCompany);
            if(from != DateTime.MinValue) _db.AddInParameter(_dbCommand, "DateFrom", DbType.Date, from);
            if(to != DateTime.MaxValue) _db.AddInParameter(_dbCommand, "DateTo", DbType.Date, to);
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
        internal IEnumerable<DbDataRecord> ReadCompanyYearly(Int64 idCompany)
        {
            return ReadCompanyYearly(idCompany, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadCompanyYearly(Int64 idCompany, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("CompanyCO2Series_ReadYearly");
            _db.AddInParameter(_dbCommand, "IdCompany", DbType.Int64, idCompany);
            if(from != DateTime.MinValue) _db.AddInParameter(_dbCommand, "DateFrom", DbType.Date, from);
            if(to != DateTime.MaxValue) _db.AddInParameter(_dbCommand, "DateTo", DbType.Date, to);
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

        #region By Site

        internal Objects.Auxiliaries.Units.TimeRange ReadSiteDatesRange(Int64 idSite)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            //Read initial date for CO2 data
            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteCO2Series_ReadDatesRange");
            _db.AddInParameter(_dbCommand, "IdSite", DbType.Int64, idSite);
            _db.AddOutParameter(_dbCommand, "DateFrom", DbType.Date, 8);
            _db.AddOutParameter(_dbCommand, "DateTo", DbType.Date, 8);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el rango
            return new Objects.Auxiliaries.Units.TimeRange(Convert.ToDateTime(Handlers.Common.CastNullValues(_db.GetParameterValue(_dbCommand, "DateFrom"), DateTime.Now)), Convert.ToDateTime(Handlers.Common.CastNullValues(_db.GetParameterValue(_dbCommand, "DateTo"), DateTime.Now)));
        }
        
        internal IEnumerable<DbDataRecord> ReadSiteStatistics(Int64 idSite)
        {
            return ReadSiteStatistics(idSite, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadSiteStatistics(Int64 idSite, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteCO2Series_Statistics");
            _db.AddInParameter(_dbCommand, "IdSite", DbType.Int64, idSite);
            if(from != DateTime.MinValue) _db.AddInParameter(_dbCommand, "DateFrom", DbType.Date, from);
            if(to != DateTime.MaxValue) _db.AddInParameter(_dbCommand, "DateTo", DbType.Date, to);
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
        internal IEnumerable<DbDataRecord> ReadSiteStatisticsByProtocols(Int64 idSite)
        {
            return ReadSiteStatisticsByProtocols(idSite, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadSiteStatisticsByProtocols(Int64 idSite, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteCO2Series_StatisticsByProtocols");
            _db.AddInParameter(_dbCommand, "IdSite", DbType.Int64, idSite);
            if(from != DateTime.MinValue) _db.AddInParameter(_dbCommand, "DateFrom", DbType.Date, from);
            if(to != DateTime.MaxValue) _db.AddInParameter(_dbCommand, "DateTo", DbType.Date, to);
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

        internal IEnumerable<DbDataRecord> ReadSiteDaily(Int64 idSite)
        {
            return ReadSiteDaily(idSite, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadSiteDaily(Int64 idSite, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteCO2Series_ReadDaily");
            _db.AddInParameter(_dbCommand, "IdSite", DbType.Int64, idSite);
            if(from != DateTime.MinValue) _db.AddInParameter(_dbCommand, "DateFrom", DbType.Date, from);
            if(to != DateTime.MaxValue) _db.AddInParameter(_dbCommand, "DateTo", DbType.Date, to);
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
        internal IEnumerable<DbDataRecord> ReadSiteWeekly(Int64 idSite)
        {
            return ReadSiteWeekly(idSite, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadSiteWeekly(Int64 idSite, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteCO2Series_ReadWeekly");
            _db.AddInParameter(_dbCommand, "IdSite", DbType.Int64, idSite);
            if(from != DateTime.MinValue) _db.AddInParameter(_dbCommand, "DateFrom", DbType.Date, from);
            if(to != DateTime.MaxValue) _db.AddInParameter(_dbCommand, "DateTo", DbType.Date, to);
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
        internal IEnumerable<DbDataRecord> ReadSiteMonthly(Int64 idSite)
        {
            return ReadSiteMonthly(idSite, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadSiteMonthly(Int64 idSite, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteCO2Series_ReadMonthly");
            _db.AddInParameter(_dbCommand, "IdSite", DbType.Int64, idSite);
            if(from != DateTime.MinValue) _db.AddInParameter(_dbCommand, "DateFrom", DbType.Date, from);
            if(to != DateTime.MaxValue) _db.AddInParameter(_dbCommand, "DateTo", DbType.Date, to);
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
        internal IEnumerable<DbDataRecord> ReadSiteYearly(Int64 idSite)
        {
            return ReadSiteYearly(idSite, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadSiteYearly(Int64 idSite, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteCO2Series_ReadYearly");
            _db.AddInParameter(_dbCommand, "IdSite", DbType.Int64, idSite);
            if(from != DateTime.MinValue) _db.AddInParameter(_dbCommand, "DateFrom", DbType.Date, from);
            if(to != DateTime.MaxValue) _db.AddInParameter(_dbCommand, "DateTo", DbType.Date, to);
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

        #endregion

    }
}
