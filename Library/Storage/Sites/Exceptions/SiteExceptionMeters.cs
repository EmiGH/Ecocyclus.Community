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
    internal class SiteExceptionMeters
    {
        internal SiteExceptionMeters() { }

        #region Read Methods

        internal IEnumerable<DbDataRecord> ReadAllByElectricity(Int64 idSite)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteExceptionElectricityMeters_ReadAll");
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
        internal IEnumerable<DbDataRecord> ReadAllUnresolvedByElectricity(Int64 idSite)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteExceptionElectricityMeters_ReadAllUnresolved");
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
        internal IEnumerable<DbDataRecord> ReadAllByElectricityMeter(Int64 idSiteElectricityMeter)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteExceptionElectricityMeters_ReadAllByMeter");
            _db.AddInParameter(_dbCommand, "IdSiteElectricityMeter", DbType.Int64, idSiteElectricityMeter);
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
        internal IEnumerable<DbDataRecord> ReadByIdByElectricity(Int64 idSiteExceptionElectricityMeter)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteExceptionElectricityMeters_ReadById");
            _db.AddInParameter(_dbCommand, "IdSiteExceptionElectricityMeter", DbType.Int64, idSiteExceptionElectricityMeter);
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
        
        internal IEnumerable<DbDataRecord> ReadAllByFuel(Int64 idSite)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteExceptionFuelMeters_ReadAll");
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
        internal IEnumerable<DbDataRecord> ReadAllUnresolvedByFuel(Int64 idSite)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteExceptionFuelMeters_ReadAllUnresolved");
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
        internal IEnumerable<DbDataRecord> ReadAllByFuelMeter(Int64 idSiteFuelMeter)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteExceptionFuelMeters_ReadAllByMeter");
            _db.AddInParameter(_dbCommand, "IdSiteFuelMeter", DbType.Int64, idSiteFuelMeter);
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
        internal IEnumerable<DbDataRecord> ReadByIdByFuel(Int64 idSiteExceptionFuelMeter)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteExceptionFuelMeters_ReadById");
            _db.AddInParameter(_dbCommand, "IdSiteExceptionFuelMeter", DbType.Int64, idSiteExceptionFuelMeter);
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

        internal IEnumerable<DbDataRecord> ReadAllByTransport(Int64 idSite)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteExceptionTransportMeters_ReadAll");
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
        internal IEnumerable<DbDataRecord> ReadAllUnresolvedByTransport(Int64 idSite)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteExceptionTransportMeters_ReadAllUnresolved");
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
        internal IEnumerable<DbDataRecord> ReadAllByTransportMeter(Int64 idSiteTransportMeter)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteExceptionTransportMeters_ReadAllByMeter");
            _db.AddInParameter(_dbCommand, "IdSiteTransportMeter", DbType.Int64, idSiteTransportMeter);
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
        internal IEnumerable<DbDataRecord> ReadByIdByTransport(Int64 idSiteExceptionTransportMeter)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteExceptionTransportMeters_ReadById");
            _db.AddInParameter(_dbCommand, "IdSiteExceptionTransportMeter", DbType.Int64, idSiteExceptionTransportMeter);
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

        internal IEnumerable<DbDataRecord> ReadAllByWaste(Int64 idSite)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteExceptionWasteMeters_ReadAll");
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
        internal IEnumerable<DbDataRecord> ReadAllUnresolvedByWaste(Int64 idSite)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteExceptionWasteMeters_ReadAllUnresolved");
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
        internal IEnumerable<DbDataRecord> ReadAllByWasteMeter(Int64 idSiteWasteMeter)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteExceptionWasteMeters_ReadAllByMeter");
            _db.AddInParameter(_dbCommand, "IdSiteWasteMeter", DbType.Int64, idSiteWasteMeter);
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
        internal IEnumerable<DbDataRecord> ReadByIdByWaste(Int64 idSiteExceptionWasteMeter)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteExceptionWasteMeters_ReadById");
            _db.AddInParameter(_dbCommand, "IdSiteExceptionWasteMeter", DbType.Int64, idSiteExceptionWasteMeter);
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

        internal IEnumerable<DbDataRecord> ReadAllByWater(Int64 idSite)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteExceptionWaterMeters_ReadAll");
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
        internal IEnumerable<DbDataRecord> ReadAllUnresolvedByWater(Int64 idSite)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteExceptionWaterMeters_ReadAllUnresolved");
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
        internal IEnumerable<DbDataRecord> ReadAllByWaterMeter(Int64 idSiteWaterMeter)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteExceptionWaterMeters_ReadAllByMeter");
            _db.AddInParameter(_dbCommand, "IdSiteWaterMeter", DbType.Int64, idSiteWaterMeter);
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
        internal IEnumerable<DbDataRecord> ReadByIdByWater(Int64 idSiteExceptionWaterMeter)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteExceptionWaterMeters_ReadById");
            _db.AddInParameter(_dbCommand, "IdSiteExceptionWaterMeter", DbType.Int64, idSiteExceptionWaterMeter);
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

        internal IEnumerable<DbDataRecord> CalculateOverdueExceptions(Int64 idSite)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteExceptionMeters_Calculate");
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

        #endregion

        #region Write Methods

        internal Int64 CreateForElectricityMeter(Int64 idSiteElectricityMeter)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteExceptionElectricityMeters_Create");
            _db.AddInParameter(_dbCommand, "IdSiteElectricityMeter", DbType.Int64, idSiteElectricityMeter);
            
            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdSiteExceptionElectricityMeter", DbType.Int64, 16);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdSiteExceptionElectricityMeter"));

        }
        internal void DeleteForElectricityMeter(Int64 idSiteExceptionElectricityMeter)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteExceptionElectricityMeters_Delete");
            _db.AddInParameter(_dbCommand, "IdSiteExceptionElectricityMeter", DbType.Int64, idSiteExceptionElectricityMeter);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void ResolveForElectricityMeter(Int64 idSiteElectricityMeter, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteExceptionElectricityMeters_Resolve");
            _db.AddInParameter(_dbCommand, "IdSiteElectricityMeter", DbType.Int64, idSiteElectricityMeter);
            _db.AddInParameter(_dbCommand, "From", DbType.Date, from);
            _db.AddInParameter(_dbCommand, "To", DbType.Date, to);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal Int64 CreateForFuelMeter(Int64 idSiteFuelMeter)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteExceptionFuelMeters_Create");
            _db.AddInParameter(_dbCommand, "IdSiteFuelMeter", DbType.Int64, idSiteFuelMeter);

            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdSiteExceptionFuelMeter", DbType.Int64, 16);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdSiteExceptionFuelMeter"));

        }
        internal void DeleteForFuelMeter(Int64 idSiteExceptionFuelMeter)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteExceptionFuelMeters_Delete");
            _db.AddInParameter(_dbCommand, "IdSiteExceptionFuelMeter", DbType.Int64, idSiteExceptionFuelMeter);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void ResolveForFuelMeter(Int64 idSiteFuelMeter, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteExceptionFuelMeters_Resolve");
            _db.AddInParameter(_dbCommand, "IdSiteFuelMeter", DbType.Int64, idSiteFuelMeter);
            _db.AddInParameter(_dbCommand, "From", DbType.Date, from);
            _db.AddInParameter(_dbCommand, "To", DbType.Date, to);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal Int64 CreateForTransportMeter(Int64 idSiteTransportMeter)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteExceptionTransportMeters_Create");
            _db.AddInParameter(_dbCommand, "IdSiteTransportMeter", DbType.Int64, idSiteTransportMeter);

            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdSiteExceptionTransportMeter", DbType.Int64, 16);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdSiteExceptionTransportMeter"));

        }
        internal void DeleteForTransportMeter(Int64 idSiteExceptionTransportMeter)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteExceptionTransportMeters_Delete");
            _db.AddInParameter(_dbCommand, "IdSiteExceptionTransportMeter", DbType.Int64, idSiteExceptionTransportMeter);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void ResolveForTransportMeter(Int64 idSiteTransportMeter, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteExceptionTransportMeters_Resolve");
            _db.AddInParameter(_dbCommand, "IdSiteTransportMeter", DbType.Int64, idSiteTransportMeter);
            _db.AddInParameter(_dbCommand, "From", DbType.Date, from);
            _db.AddInParameter(_dbCommand, "To", DbType.Date, to);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal Int64 CreateForWasteMeter(Int64 idSiteWasteMeter)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteExceptionWasteMeters_Create");
            _db.AddInParameter(_dbCommand, "IdSiteWasteMeter", DbType.Int64, idSiteWasteMeter);

            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdSiteExceptionWasteMeter", DbType.Int64, 16);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdSiteExceptionWasteMeter"));

        }
        internal void DeleteForWasteMeter(Int64 idSiteExceptionWasteMeter)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteExceptionWasteMeters_Delete");
            _db.AddInParameter(_dbCommand, "IdSiteExceptionWasteMeter", DbType.Int64, idSiteExceptionWasteMeter);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void ResolveForWasteMeter(Int64 idSiteWasteMeter, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteExceptionWasteMeters_Resolve");
            _db.AddInParameter(_dbCommand, "IdSiteWasteMeter", DbType.Int64, idSiteWasteMeter);
            _db.AddInParameter(_dbCommand, "From", DbType.Date, from);
            _db.AddInParameter(_dbCommand, "To", DbType.Date, to);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal Int64 CreateForWaterMeter(Int64 idSiteWaterMeter)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteExceptionWaterMeters_Create");
            _db.AddInParameter(_dbCommand, "IdSiteWaterMeter", DbType.Int64, idSiteWaterMeter);

            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdSiteExceptionWaterMeter", DbType.Int64, 16);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdSiteExceptionWaterMeter"));

        }
        internal void DeleteForWaterMeter(Int64 idSiteExceptionWaterMeter)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteExceptionWaterMeters_Delete");
            _db.AddInParameter(_dbCommand, "IdSiteExceptionWaterMeter", DbType.Int64, idSiteExceptionWaterMeter);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void ResolveForWaterMeter(Int64 idSiteWaterMeter, DateTime from, DateTime to)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteExceptionWaterMeters_Resolve");
            _db.AddInParameter(_dbCommand, "IdSiteWaterMeter", DbType.Int64, idSiteWaterMeter);
            _db.AddInParameter(_dbCommand, "From", DbType.Date, from);
            _db.AddInParameter(_dbCommand, "To", DbType.Date, to);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        #endregion
    }
}
