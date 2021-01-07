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
    internal class WaterMeterSeries
    {
        internal WaterMeterSeries()
        { }

        #region Write Methods

        internal Int64 Create(Int64 idLoad, DateTime day, Double value, Double valuePattern, Double totalCO2)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteWaterMeterSeries_Create");
            _db.AddInParameter(_dbCommand, "IdSiteWaterMeterLoad", DbType.Int64, idLoad);
            _db.AddInParameter(_dbCommand, "Day", DbType.Date, day);
            _db.AddInParameter(_dbCommand, "Value", DbType.Double, value);
            _db.AddInParameter(_dbCommand, "ValuePattern", DbType.Double, valuePattern);
            _db.AddInParameter(_dbCommand, "TotalCO2", DbType.Double, totalCO2);

            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdSiteWaterMeterSerie", DbType.Int64, 18);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdSiteWaterMeterSerie"));

        }
        internal void Update(Int64 idLoad, Double value, Double valuePattern, Double totalCO2)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SiteWaterMeterSeries_Update");
            _db.AddInParameter(_dbCommand, "IdSiteWaterMeterLoad", DbType.Int64, idLoad);
            _db.AddInParameter(_dbCommand, "Value", DbType.Double, value);
            _db.AddInParameter(_dbCommand, "ValuePattern", DbType.Double, valuePattern);
            _db.AddInParameter(_dbCommand, "TotalCO2", DbType.Double, totalCO2);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

        }

        #endregion
    }
}
