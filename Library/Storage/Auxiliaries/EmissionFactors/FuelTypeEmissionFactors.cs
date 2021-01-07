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
    internal class FuelTypeEmissionFactors
    {
        internal FuelTypeEmissionFactors() { }

        #region Read Methods

        internal IEnumerable<DbDataRecord> ReadById(Int64 idEmissionFactor, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("FuelTypeEmissionFactors_ReadById");
            _db.AddInParameter(_dbCommand, "IdEmissionFactor", DbType.Int64, idEmissionFactor);
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
        internal IEnumerable<DbDataRecord> ReadDefault(Int64 idFuelType, Int64 idCountry, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("FuelTypeEmissionFactors_ReadDefault");
            _db.AddInParameter(_dbCommand, "IdFuelType", DbType.Int64, idFuelType);
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
        internal IEnumerable<DbDataRecord> ReadGlobal(Int64 idFuelType, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("FuelTypeEmissionFactors_ReadGlobal");
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
        internal IEnumerable<DbDataRecord> ReadAll(Int64 idFuelType, Int64 idCountry, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("FuelTypeEmissionFactors_ReadAllByCountry");
            _db.AddInParameter(_dbCommand, "IdFuelType", DbType.Int64, idFuelType);
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
        internal IEnumerable<DbDataRecord> ReadAllDefault(Int64 idCountry, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("FuelTypeEmissionFactors_ReadAllDefaultByCountry");
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
        internal IEnumerable<DbDataRecord> ReadAllGlobal(String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("FuelTypeEmissionFactors_ReadAllGlobal");
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
        internal IEnumerable<DbDataRecord> ReadAll(Int64 idFuelType, Int64 idCountry, Int64 idCompany, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("FuelTypeEmissionFactors_ReadAllByCountryForCompany");
            _db.AddInParameter(_dbCommand, "IdFuelType", DbType.Int64, idFuelType);
            _db.AddInParameter(_dbCommand, "IdCountry", DbType.Int64, idCountry);
            _db.AddInParameter(_dbCommand, "IdCompany", DbType.Int64, idCompany);
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
        internal IEnumerable<DbDataRecord> ReadCountries(Int64 idFuelType, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("FuelTypeEmissionFactors_ReadCountriesWithFactors");
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

        internal Boolean IsUsed(Int64 idEmissionFactor)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("FuelTypeEmissionFactors_ReadIsUsed");
            _db.AddInParameter(_dbCommand, "IdFuelTypeEmissionFactor", DbType.Int64, idEmissionFactor);
            _db.AddOutParameter(_dbCommand, "IsUsed", DbType.Boolean, 1);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToBoolean(_db.GetParameterValue(_dbCommand, "IsUsed"));
        }

        #endregion

        #region Write Methods

        internal Int64 Create(Int64 idEmissionFactor, Int64 idFuelType, Boolean isPropietary)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("FuelTypeEmissionFactors_CreatePropietary");
            _db.AddInParameter(_dbCommand, "IdFuelType", DbType.Int64, idFuelType);
            _db.AddInParameter(_dbCommand, "IdEmissionFactor", DbType.Int64, idEmissionFactor);
            _db.AddInParameter(_dbCommand, "IsPropietary", DbType.Boolean, isPropietary);

            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdFuelTypeEmissionFactor", DbType.Int64, 18);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdFuelTypeEmissionFactor"));
        }
        internal void Delete(Int64 idFuelTypeEmissionFactor)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("FuelTypeEmissionFactors_Delete");
            _db.AddInParameter(_dbCommand, "IdFuelTypeEmissionFactor", DbType.Int64, idFuelTypeEmissionFactor);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

        }
       
        #endregion
    }
}
