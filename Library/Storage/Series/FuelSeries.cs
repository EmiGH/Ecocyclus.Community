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
    internal class FuelSeries
    {
        internal FuelSeries() { }

        #region Read Methods
        
        #region By Company

        internal Int64 ReadUnitForCompany(Int64 idCompany)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("CompanyFuelSeries_ReadUnit");
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

            DbCommand _dbCommand = _db.GetStoredProcCommand("CompanyFuelSeries_Statistics");
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
        internal IEnumerable<DbDataRecord> ReadCompanyStatistics(Int64 idCompany, Int64 idFuelType)
        {
            return ReadCompanyStatistics(idCompany, idFuelType, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadCompanyStatistics(Int64 idCompany, Int64 idFuelType, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("CompanyFuelSeries_Statistics");
            _db.AddInParameter(_dbCommand, "IdCompany", DbType.Int64, idCompany);
            _db.AddInParameter(_dbCommand, "IdFuelType", DbType.Int64, idFuelType);
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

            DbCommand _dbCommand = _db.GetStoredProcCommand("CompanyFuelSeries_ReadDaily");
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
        internal IEnumerable<DbDataRecord> ReadCompanyDaily(Int64 idCompany, Int64 idFuelType)
        {
            return ReadCompanyDaily(idCompany, idFuelType, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadCompanyDaily(Int64 idCompany, Int64 idFuelType, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("CompanyFuelSeries_ReadDaily");
            _db.AddInParameter(_dbCommand, "IdCompany", DbType.Int64, idCompany);
            _db.AddInParameter(_dbCommand, "IdFuelType", DbType.Int64, idFuelType);
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

            DbCommand _dbCommand = _db.GetStoredProcCommand("CompanyFuelSeries_ReadWeekly");
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
        internal IEnumerable<DbDataRecord> ReadCompanyWeekly(Int64 idCompany, Int64 idFuelType)
        {
            return ReadCompanyWeekly(idCompany, idFuelType, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadCompanyWeekly(Int64 idCompany, Int64 idFuelType, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("CompanyFuelSeries_ReadWeekly");
            _db.AddInParameter(_dbCommand, "IdCompany", DbType.Int64, idCompany);
            _db.AddInParameter(_dbCommand, "IdFuelType", DbType.Int64, idFuelType);
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

            DbCommand _dbCommand = _db.GetStoredProcCommand("CompanyFuelSeries_ReadMonthly");
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
        internal IEnumerable<DbDataRecord> ReadCompanyMonthly(Int64 idCompany, Int64 idFuelType)
        {
            return ReadCompanyMonthly(idCompany, idFuelType, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadCompanyMonthly(Int64 idCompany, Int64 idFuelType, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("CompanyFuelSeries_ReadMonthly");
            _db.AddInParameter(_dbCommand, "IdCompany", DbType.Int64, idCompany);
            _db.AddInParameter(_dbCommand, "IdFuelType", DbType.Int64, idFuelType);
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

            DbCommand _dbCommand = _db.GetStoredProcCommand("CompanyFuelSeries_ReadYearly");
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
        internal IEnumerable<DbDataRecord> ReadCompanyYearly(Int64 idCompany, Int64 idFuelType)
        {
            return ReadCompanyYearly(idCompany, idFuelType, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadCompanyYearly(Int64 idCompany, Int64 idFuelType, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("CompanyFuelSeries_ReadYearly");
            _db.AddInParameter(_dbCommand, "IdCompany", DbType.Int64, idCompany);
            _db.AddInParameter(_dbCommand, "IdFuelType", DbType.Int64, idFuelType);
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

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteFuelSeries_ReadUnit");
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

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteFuelSeries_Statistics");
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
        internal IEnumerable<DbDataRecord> ReadSiteStatistics(Int64 idSite, Int64 idFuelType)
        {
            return ReadSiteStatistics(idSite, idFuelType, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadSiteStatistics(Int64 idSite, Int64 idFuelType, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteFuelSeries_Statistics");
            _db.AddInParameter(_dbCommand, "IdSite", DbType.Int64, idSite);
            _db.AddInParameter(_dbCommand, "IdFuelType", DbType.Int64, idFuelType);
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

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteFuelSeries_StatisticsByTypes");
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

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteFuelSeries_ReadDaily");
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
        internal IEnumerable<DbDataRecord> ReadSiteDaily(Int64 idSite, Int64 idFuelType)
        {
            return ReadSiteDaily(idSite, idFuelType, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadSiteDaily(Int64 idSite, Int64 idFuelType, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteFuelSeries_ReadDaily");
            _db.AddInParameter(_dbCommand, "IdSite", DbType.Int64, idSite);
            _db.AddInParameter(_dbCommand, "IdFuelType", DbType.Int64, idFuelType);
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

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteFuelSeries_ReadWeekly");
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
        internal IEnumerable<DbDataRecord> ReadSiteWeekly(Int64 idSite, Int64 idFuelType)
        {
            return ReadSiteWeekly(idSite, idFuelType, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadSiteWeekly(Int64 idSite, Int64 idFuelType, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteFuelSeries_ReadWeekly");
            _db.AddInParameter(_dbCommand, "IdSite", DbType.Int64, idSite);
            _db.AddInParameter(_dbCommand, "IdFuelType", DbType.Int64, idFuelType);
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

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteFuelSeries_ReadMonthly");
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
        internal IEnumerable<DbDataRecord> ReadSiteMonthly(Int64 idSite, Int64 idFuelType)
        {
            return ReadSiteMonthly(idSite, idFuelType, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadSiteMonthly(Int64 idSite, Int64 idFuelType, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteFuelSeries_ReadMonthly");
            _db.AddInParameter(_dbCommand, "IdSite", DbType.Int64, idSite);
            _db.AddInParameter(_dbCommand, "IdFuelType", DbType.Int64, idFuelType);
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

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteFuelSeries_ReadYearly");
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
        internal IEnumerable<DbDataRecord> ReadSiteYearly(Int64 idSite, Int64 idFuelType)
        {
            return ReadSiteYearly(idSite, idFuelType, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadSiteYearly(Int64 idSite, Int64 idFuelType, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteFuelSeries_ReadYearly");
            _db.AddInParameter(_dbCommand, "IdSite", DbType.Int64, idSite);
            _db.AddInParameter(_dbCommand, "IdFuelType", DbType.Int64, idFuelType);
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

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteFuelMeterSeries_ReadUnit");
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

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteFuelMeterSeries_Statistics");
            _db.AddInParameter(_dbCommand, "IdSiteFuelMeter", DbType.Int64, idMeter);
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
        internal IEnumerable<DbDataRecord> ReadMeterStatistics(Int64 idMeter, Int64 idFuelType)
        {
            return ReadMeterStatistics(idMeter, idFuelType, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadMeterStatistics(Int64 idMeter, Int64 idFuelType, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteFuelMeterSeries_Statistics");
            _db.AddInParameter(_dbCommand, "IdSiteFuelMeter", DbType.Int64, idMeter);
            _db.AddInParameter(_dbCommand, "IdFuelType", DbType.Int64, idFuelType);
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

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteFuelMeterSeries_StatisticsByTypes");
            _db.AddInParameter(_dbCommand, "IdSiteFuelMeter", DbType.Int64, idMeter);
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

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteFuelMeterSeries_ReadDaily");
            _db.AddInParameter(_dbCommand, "IdSiteFuelMeter", DbType.Int64, idMeter);
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
        internal IEnumerable<DbDataRecord> ReadMeterDaily(Int64 idMeter, Int64 idFuelType)
        {
            return ReadMeterDaily(idMeter, idFuelType, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadMeterDaily(Int64 idMeter, Int64 idFuelType, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteFuelMeterSeries_ReadDaily");
            _db.AddInParameter(_dbCommand, "IdSiteFuelMeter", DbType.Int64, idMeter);
            _db.AddInParameter(_dbCommand, "IdFuelType", DbType.Int64, idFuelType);
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

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteFuelMeterSeries_ReadWeekly");
            _db.AddInParameter(_dbCommand, "IdSiteFuelMeter", DbType.Int64, idMeter);
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
        internal IEnumerable<DbDataRecord> ReadMeterWeekly(Int64 idMeter, Int64 idFuelType)
        {
            return ReadMeterWeekly(idMeter, idFuelType, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadMeterWeekly(Int64 idMeter, Int64 idFuelType, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteFuelMeterSeries_ReadWeekly");
            _db.AddInParameter(_dbCommand, "IdSiteFuelMeter", DbType.Int64, idMeter);
            _db.AddInParameter(_dbCommand, "IdFuelType", DbType.Int64, idFuelType);
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

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteFuelMeterSeries_ReadMonthly");
            _db.AddInParameter(_dbCommand, "IdSiteFuelMeter", DbType.Int64, idMeter);
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
        internal IEnumerable<DbDataRecord> ReadMeterMonthly(Int64 idMeter, Int64 idFuelType)
        {
            return ReadMeterMonthly(idMeter, idFuelType, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadMeterMonthly(Int64 idMeter, Int64 idFuelType, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteFuelMeterSeries_ReadMonthly");
            _db.AddInParameter(_dbCommand, "IdSiteFuelMeter", DbType.Int64, idMeter);
            _db.AddInParameter(_dbCommand, "IdFuelType", DbType.Int64, idFuelType);
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

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteFuelMeterSeries_ReadYearly");
            _db.AddInParameter(_dbCommand, "IdSiteFuelMeter", DbType.Int64, idMeter);
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
        internal IEnumerable<DbDataRecord> ReadMeterYearly(Int64 idMeter, Int64 idFuelType)
        {
            return ReadMeterYearly(idMeter, idFuelType, DateTime.MinValue, DateTime.MaxValue);
        }
        internal IEnumerable<DbDataRecord> ReadMeterYearly(Int64 idMeter, Int64 idFuelType, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteFuelMeterSeries_ReadYearly");
            _db.AddInParameter(_dbCommand, "IdSiteFuelMeter", DbType.Int64, idMeter);
            _db.AddInParameter(_dbCommand, "IdFuelType", DbType.Int64, idFuelType);
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
