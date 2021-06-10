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
    internal class FuelTypes
    {
        internal FuelTypes() { }

        #region Read Methods

        internal IEnumerable<DbDataRecord> ReadAllByCountry(Int64 idCountry, string idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("FuelTypes_ReadAllByCountry");
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
        internal IEnumerable<DbDataRecord> ReadById(Int64 idFuelType, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("FuelTypes_ReadById");
            _db.AddInParameter(_dbCommand, "IdFuelType", DbType.Int64, idFuelType);
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

        internal Int64 Create(String idLanguage, String name, String description, Double ef, Int64 idIcon)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("FuelTypes_Create");
            _db.AddInParameter(_dbCommand, "Name", DbType.String, name);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "Description", DbType.String, description);
            _db.AddInParameter(_dbCommand, "EF", DbType.Double, ef);
            _db.AddInParameter(_dbCommand, "IdIcon", DbType.Int64, idIcon);

            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdFuelType", DbType.Int64, 18);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdFuelType"));

        }
        internal void Delete(Int64 idFuelType)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("FuelTypes_Delete");
            _db.AddInParameter(_dbCommand, "IdFuelType", DbType.Int64, idFuelType);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Update(Int64 idFuelType, String idLanguage, String name, String description, Double ef, Int64 idIcon)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("FuelTypes_Update");
            _db.AddInParameter(_dbCommand, "IdFuelType", DbType.Int64, idFuelType);
            _db.AddInParameter(_dbCommand, "Name", DbType.String, name);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "Description", DbType.String, description);
            _db.AddInParameter(_dbCommand, "EF", DbType.Double, ef);
            _db.AddInParameter(_dbCommand, "IdIcon", DbType.Int64, idIcon);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        #endregion
    }
}
