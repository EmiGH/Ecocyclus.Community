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
    internal class TransportSeries
    {
        internal TransportSeries() { }

        #region Read Methods
        
        #region By Company

        internal Int64 ReadUnitForCompany(Int64 idCompany)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("CompanyTransportSeries_ReadUnit");
            _db.AddInParameter(_dbCommand, "IdCompany", DbType.Int64, idCompany);
            _db.AddOutParameter(_dbCommand, "IdUnit", DbType.Int64, 8);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdUnit"));
        }

        internal IEnumerable<DbDataRecord> ReadCompanyStatistics(Int64 idCompany)
        {
            return ReadCompanyStatistics(idCompany, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadCompanyStatistics(Int64 idCompany, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("CompanyTransportSeries_Statistics");
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
        internal IEnumerable<DbDataRecord> ReadCompanyStatistics(Int64 idCompany, Int64 idTransportType)
        {
            return ReadCompanyStatistics(idCompany, idTransportType, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadCompanyStatistics(Int64 idCompany, Int64 idTransportType, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("CompanyTransportSeries_Statistics");
            _db.AddInParameter(_dbCommand, "IdCompany", DbType.Int64, idCompany);
            _db.AddInParameter(_dbCommand, "IdTransportType", DbType.Int64, idTransportType);
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

            DbCommand _dbCommand = _db.GetStoredProcCommand("CompanyTransportSeries_ReadDaily");
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
        internal IEnumerable<DbDataRecord> ReadCompanyDaily(Int64 idCompany, Int64 idTransportType)
        {
            return ReadCompanyDaily(idCompany, idTransportType, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadCompanyDaily(Int64 idCompany, Int64 idTransportType, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("CompanyTransportSeries_ReadDaily");
            _db.AddInParameter(_dbCommand, "IdCompany", DbType.Int64, idCompany);
            _db.AddInParameter(_dbCommand, "IdTransportType", DbType.Int64, idTransportType);
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

            DbCommand _dbCommand = _db.GetStoredProcCommand("CompanyTransportSeries_ReadWeekly");
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
        internal IEnumerable<DbDataRecord> ReadCompanyWeekly(Int64 idCompany, Int64 idTransportType)
        {
            return ReadCompanyWeekly(idCompany, idTransportType, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadCompanyWeekly(Int64 idCompany, Int64 idTransportType, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("CompanyTransportSeries_ReadWeekly");
            _db.AddInParameter(_dbCommand, "IdCompany", DbType.Int64, idCompany);
            _db.AddInParameter(_dbCommand, "IdTransportType", DbType.Int64, idTransportType);
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

            DbCommand _dbCommand = _db.GetStoredProcCommand("CompanyTransportSeries_ReadMonthly");
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
        internal IEnumerable<DbDataRecord> ReadCompanyMonthly(Int64 idCompany, Int64 idTransportType)
        {
            return ReadCompanyMonthly(idCompany, idTransportType, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadCompanyMonthly(Int64 idCompany, Int64 idTransportType, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("CompanyTransportSeries_ReadMonthly");
            _db.AddInParameter(_dbCommand, "IdCompany", DbType.Int64, idCompany);
            _db.AddInParameter(_dbCommand, "IdTransportType", DbType.Int64, idTransportType);
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

            DbCommand _dbCommand = _db.GetStoredProcCommand("CompanyTransportSeries_ReadYearly");
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
        internal IEnumerable<DbDataRecord> ReadCompanyYearly(Int64 idCompany, Int64 idTransportType)
        {
            return ReadCompanyYearly(idCompany, idTransportType, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadCompanyYearly(Int64 idCompany, Int64 idTransportType, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("CompanyTransportSeries_ReadYearly");
            _db.AddInParameter(_dbCommand, "IdCompany", DbType.Int64, idCompany);
            _db.AddInParameter(_dbCommand, "IdTransportType", DbType.Int64, idTransportType);
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

        internal Int64 ReadUnitForSite(Int64 idSite)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteTransportSeries_ReadUnit");
            _db.AddInParameter(_dbCommand, "IdSite", DbType.Int64, idSite);
            _db.AddOutParameter(_dbCommand, "IdUnit", DbType.Int64, 8);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdUnit"));
        }

        internal IEnumerable<DbDataRecord> ReadSiteStatistics(Int64 idSite)
        {
            return ReadSiteStatistics(idSite, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadSiteStatistics(Int64 idSite, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteTransportSeries_Statistics");
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
        internal IEnumerable<DbDataRecord> ReadSiteStatistics(Int64 idSite, Int64 idTransportType)
        {
            return ReadSiteStatistics(idSite, idTransportType, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadSiteStatistics(Int64 idSite, Int64 idTransportType, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteTransportSeries_Statistics");
            _db.AddInParameter(_dbCommand, "IdSite", DbType.Int64, idSite);
            _db.AddInParameter(_dbCommand, "IdTransportType", DbType.Int64, idTransportType);
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
        internal IEnumerable<DbDataRecord> ReadSiteStatisticsByTypes(Int64 idSite, string idLanguage)
        {
            return ReadSiteStatisticsByTypes(idSite, idLanguage, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadSiteStatisticsByTypes(Int64 idSite, string idLanguage, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteTransportSeries_StatisticsByTypes");
            _db.AddInParameter(_dbCommand, "IdSite", DbType.Int64, idSite);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            if (from != DateTime.MinValue) _db.AddInParameter(_dbCommand, "DateFrom", DbType.Date, from);
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

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteTransportSeries_ReadDaily");
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
        internal IEnumerable<DbDataRecord> ReadSiteDaily(Int64 idSite, Int64 idTransportType)
        {
            return ReadSiteDaily(idSite, idTransportType, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadSiteDaily(Int64 idSite, Int64 idTransportType, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteTransportSeries_ReadDaily");
            _db.AddInParameter(_dbCommand, "IdSite", DbType.Int64, idSite);
            _db.AddInParameter(_dbCommand, "IdTransportType", DbType.Int64, idTransportType);
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

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteTransportSeries_ReadWeekly");
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
        internal IEnumerable<DbDataRecord> ReadSiteWeekly(Int64 idSite, Int64 idTransportType)
        {
            return ReadSiteWeekly(idSite, idTransportType, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadSiteWeekly(Int64 idSite, Int64 idTransportType, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteTransportSeries_ReadWeekly");
            _db.AddInParameter(_dbCommand, "IdSite", DbType.Int64, idSite);
            _db.AddInParameter(_dbCommand, "IdTransportType", DbType.Int64, idTransportType);
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

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteTransportSeries_ReadMonthly");
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
        internal IEnumerable<DbDataRecord> ReadSiteMonthly(Int64 idSite, Int64 idTransportType)
        {
            return ReadSiteMonthly(idSite, idTransportType, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadSiteMonthly(Int64 idSite, Int64 idTransportType, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteTransportSeries_ReadMonthly");
            _db.AddInParameter(_dbCommand, "IdSite", DbType.Int64, idSite);
            _db.AddInParameter(_dbCommand, "IdTransportType", DbType.Int64, idTransportType);
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

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteTransportSeries_ReadYearly");
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
        internal IEnumerable<DbDataRecord> ReadSiteYearly(Int64 idSite, Int64 idTransportType)
        {
            return ReadSiteYearly(idSite, idTransportType, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadSiteYearly(Int64 idSite, Int64 idTransportType, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteTransportSeries_ReadYearly");
            _db.AddInParameter(_dbCommand, "IdSite", DbType.Int64, idSite);
            _db.AddInParameter(_dbCommand, "IdTransportType", DbType.Int64, idTransportType);
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

        #region By Meters

        internal Int64 ReadUnitForMeter(Int64 idMeter)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteTransportMeterSeries_ReadUnit");
            _db.AddInParameter(_dbCommand, "IdMeter", DbType.Int64, idMeter);
            _db.AddOutParameter(_dbCommand, "IdUnit", DbType.Int64, 8);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdUnit"));
        }

        internal IEnumerable<DbDataRecord> ReadMeterStatistics(Int64 idMeter)
        {
            return ReadMeterStatistics(idMeter, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadMeterStatistics(Int64 idMeter, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteTransportMeterSeries_Statistics");
            _db.AddInParameter(_dbCommand, "IdSiteTransportMeter", DbType.Int64, idMeter);
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
        internal IEnumerable<DbDataRecord> ReadMeterStatistics(Int64 idMeter, Int64 idTransportType)
        {
            return ReadMeterStatistics(idMeter, idTransportType, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadMeterStatistics(Int64 idMeter, Int64 idTransportType, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteTransportMeterSeries_Statistics");
            _db.AddInParameter(_dbCommand, "IdSiteTransportMeter", DbType.Int64, idMeter);
            _db.AddInParameter(_dbCommand, "IdTransportType", DbType.Int64, idTransportType);
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
        internal IEnumerable<DbDataRecord> ReadMeterStatisticsByTypes(Int64 idMeter, string idLanguage)
        {
            return ReadMeterStatisticsByTypes(idMeter, idLanguage, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadMeterStatisticsByTypes(Int64 idMeter, string idLanguage, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteTransportMeterSeries_StatisticsByTypes");
            _db.AddInParameter(_dbCommand, "IdSiteTransportMeter", DbType.Int64, idMeter);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            if (from != DateTime.MinValue) _db.AddInParameter(_dbCommand, "DateFrom", DbType.Date, from);
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


        internal IEnumerable<DbDataRecord> ReadMeterDaily(Int64 idMeter)
        {
            return ReadMeterDaily(idMeter, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadMeterDaily(Int64 idMeter, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteTransportMeterSeries_ReadDaily");
            _db.AddInParameter(_dbCommand, "IdSiteTransportMeter", DbType.Int64, idMeter);
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
        internal IEnumerable<DbDataRecord> ReadMeterDaily(Int64 idMeter, Int64 idTransportType)
        {
            return ReadMeterDaily(idMeter, idTransportType, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadMeterDaily(Int64 idMeter, Int64 idTransportType, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteTransportMeterSeries_ReadDaily");
            _db.AddInParameter(_dbCommand, "IdSiteTransportMeter", DbType.Int64, idMeter);
            _db.AddInParameter(_dbCommand, "IdTransportType", DbType.Int64, idTransportType);
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
        internal IEnumerable<DbDataRecord> ReadMeterWeekly(Int64 idMeter)
        {
            return ReadMeterWeekly(idMeter, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadMeterWeekly(Int64 idMeter, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteTransportMeterSeries_ReadWeekly");
            _db.AddInParameter(_dbCommand, "IdSiteTransportMeter", DbType.Int64, idMeter);
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
        internal IEnumerable<DbDataRecord> ReadMeterWeekly(Int64 idMeter, Int64 idTransportType)
        {
            return ReadMeterWeekly(idMeter, idTransportType, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadMeterWeekly(Int64 idMeter, Int64 idTransportType, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteTransportMeterSeries_ReadWeekly");
            _db.AddInParameter(_dbCommand, "IdSiteTransportMeter", DbType.Int64, idMeter);
            _db.AddInParameter(_dbCommand, "IdTransportType", DbType.Int64, idTransportType);
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
        internal IEnumerable<DbDataRecord> ReadMeterMonthly(Int64 idMeter)
        {
            return ReadMeterMonthly(idMeter, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadMeterMonthly(Int64 idMeter, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteTransportMeterSeries_ReadMonthly");
            _db.AddInParameter(_dbCommand, "IdSiteTransportMeter", DbType.Int64, idMeter);
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
        internal IEnumerable<DbDataRecord> ReadMeterMonthly(Int64 idMeter, Int64 idTransportType)
        {
            return ReadMeterMonthly(idMeter, idTransportType, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadMeterMonthly(Int64 idMeter, Int64 idTransportType, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteTransportMeterSeries_ReadMonthly");
            _db.AddInParameter(_dbCommand, "IdSiteTransportMeter", DbType.Int64, idMeter);
            _db.AddInParameter(_dbCommand, "IdTransportType", DbType.Int64, idTransportType);
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
        internal IEnumerable<DbDataRecord> ReadMeterYearly(Int64 idMeter)
        {
            return ReadMeterYearly(idMeter, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadMeterYearly(Int64 idMeter, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteTransportMeterSeries_ReadYearly");
            _db.AddInParameter(_dbCommand, "IdSiteTransportMeter", DbType.Int64, idMeter);
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
        internal IEnumerable<DbDataRecord> ReadMeterYearly(Int64 idMeter, Int64 idTransportType)
        {
            return ReadMeterYearly(idMeter, idTransportType, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadMeterYearly(Int64 idMeter, Int64 idTransportType, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteTransportMeterSeries_ReadYearly");
            _db.AddInParameter(_dbCommand, "IdSiteTransportMeter", DbType.Int64, idMeter);
            _db.AddInParameter(_dbCommand, "IdTransportType", DbType.Int64, idTransportType);
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
