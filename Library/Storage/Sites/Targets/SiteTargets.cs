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
    internal class SiteTargets
    {
        internal SiteTargets() { }
        
        #region Read Methods

        internal IEnumerable<DbDataRecord> ReadById(Int64 idSite)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteTargets_ReadById");
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

        internal void Update(Int64 idSite, Double electricityConsumption, Int64 idElectricityUnit, Double electricityCO2, Double fuelConsumption, Int64 idFuelUnit, Double fuelCO2, Double transportConsumption, Int64 idTransportUnit, Double transportCO2, Double wasteConsumption, Int64 idWasteUnit, Double wasteCO2, Double waterConsumption, Int64 idWaterUnit, Double waterCO2, Double totalCO2)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteTargets_Update");
            _db.AddInParameter(_dbCommand, "IdSite", DbType.Int64, idSite);
            _db.AddInParameter(_dbCommand, "ElectricityConsumption", DbType.Double, electricityConsumption);
            _db.AddInParameter(_dbCommand, "IdElectricityUnit", DbType.Int64, idElectricityUnit);
            _db.AddInParameter(_dbCommand, "ElectricityCO2", DbType.Double, electricityCO2);
            _db.AddInParameter(_dbCommand, "FuelConsumption", DbType.Double, fuelConsumption);
            _db.AddInParameter(_dbCommand, "IdFuelUnit", DbType.Int64, idFuelUnit);
            _db.AddInParameter(_dbCommand, "FuelCO2", DbType.Double, fuelCO2);
            _db.AddInParameter(_dbCommand, "TransportConsumption", DbType.Double, transportConsumption);
            _db.AddInParameter(_dbCommand, "IdTransportUnit", DbType.Int64, idTransportUnit);
            _db.AddInParameter(_dbCommand, "TransportCO2", DbType.Double, transportCO2);
            _db.AddInParameter(_dbCommand, "WasteConsumption", DbType.Double, wasteConsumption);
            _db.AddInParameter(_dbCommand, "IdWasteUnit", DbType.Int64, idWasteUnit);
            _db.AddInParameter(_dbCommand, "WasteCO2", DbType.Double, wasteCO2);
            _db.AddInParameter(_dbCommand, "WaterConsumption", DbType.Double, waterConsumption);
            _db.AddInParameter(_dbCommand, "IdWaterUnit", DbType.Int64, idWaterUnit);
            _db.AddInParameter(_dbCommand, "WaterCO2", DbType.Double, waterCO2);
            _db.AddInParameter(_dbCommand, "TotalCO2", DbType.Double, totalCO2);
            
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void UpdateStatus(Int64 idSite, Boolean surpassed)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteTargets_UpdateStatus");
            _db.AddInParameter(_dbCommand, "IdSite", DbType.Int64, idSite);
            _db.AddInParameter(_dbCommand, "Surpassed", DbType.Boolean, surpassed);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        #endregion
    }
}
