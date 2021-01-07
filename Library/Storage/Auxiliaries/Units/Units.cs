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
    internal class Units
    {
        internal Units() { }

        #region Read Methods


        internal IEnumerable<DbDataRecord> ReadAllByMagnitude(Int64 idMagnitude, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Units_ReadAllByMagnitude");
            _db.AddInParameter(_dbCommand, "IdMagnitude", DbType.Int64, idMagnitude);
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
        
        internal IEnumerable<DbDataRecord> ReadById(Int64 idUnit, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Units_ReadById");
            _db.AddInParameter(_dbCommand, "IdUnit", DbType.Int64, idUnit);
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
        internal IEnumerable<DbDataRecord> ReadForSQL(String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Units_ReadForSQL");
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
        internal IEnumerable<DbDataRecord> ReadGeoDefault(String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Units_ReadGeoDefault");
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
        internal IEnumerable<DbDataRecord> ReadForGoogle(String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Units_ReadForGoogle");
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

        internal Int64 Create(String idLanguage, String symbol, String name, Double numerator, Double denominator, Double exponent, Double constant, Boolean isPattern, Boolean isForElectricity, Boolean isForWater, Boolean isForTransport, Boolean isForFuels, Boolean isForWaste)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Units_Create");
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "Symbol", DbType.String, symbol);
            _db.AddInParameter(_dbCommand, "Name", DbType.String, name);
            _db.AddInParameter(_dbCommand, "Numerator", DbType.Double, numerator);
            _db.AddInParameter(_dbCommand, "Denominator", DbType.Double, denominator);
            _db.AddInParameter(_dbCommand, "Exponent", DbType.Double, exponent);
            _db.AddInParameter(_dbCommand, "Constant", DbType.Double, constant);
            _db.AddInParameter(_dbCommand, "IsPattern", DbType.Boolean, isPattern);
            _db.AddInParameter(_dbCommand, "IsForElectricity", DbType.Boolean, isForElectricity);
            _db.AddInParameter(_dbCommand, "IsForFuels", DbType.Boolean, isForFuels);
            _db.AddInParameter(_dbCommand, "IsForTransport", DbType.Boolean, isForTransport);
            _db.AddInParameter(_dbCommand, "IsForWaste", DbType.Boolean, isForWaste);
            _db.AddInParameter(_dbCommand, "IsForWater", DbType.Boolean, isForWater);

            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdUnit", DbType.Int64, 18);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdUnit"));

        }
        internal void Delete(Int64 idUnit)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Units_Delete");
            _db.AddInParameter(_dbCommand, "IdUnit", DbType.Int64, idUnit);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Update(Int64 idUnit, String idLanguage, String symbol, String name, Double numerator, Double denominator, Double exponent, Double constant, Boolean isPattern, Boolean isForElectricity, Boolean isForWater, Boolean isForTransport, Boolean isForFuels, Boolean isForWaste)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Units_Update");
            _db.AddInParameter(_dbCommand, "IdUnit", DbType.Int64, idUnit);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "Symbol", DbType.String, symbol);
            _db.AddInParameter(_dbCommand, "Name", DbType.String, name);
            _db.AddInParameter(_dbCommand, "Numerator", DbType.Double, numerator);
            _db.AddInParameter(_dbCommand, "Denominator", DbType.Double, denominator);
            _db.AddInParameter(_dbCommand, "Exponent", DbType.Double, exponent);
            _db.AddInParameter(_dbCommand, "Constant", DbType.Double, constant);
            _db.AddInParameter(_dbCommand, "IsPattern", DbType.Boolean, isPattern);
            _db.AddInParameter(_dbCommand, "IsForElectricity", DbType.Boolean, isForElectricity);
            _db.AddInParameter(_dbCommand, "IsForFuels", DbType.Boolean, isForFuels);
            _db.AddInParameter(_dbCommand, "IsForTransport", DbType.Boolean, isForTransport);
            _db.AddInParameter(_dbCommand, "IsForWaste", DbType.Boolean, isForWaste);
            _db.AddInParameter(_dbCommand, "IsForWater", DbType.Boolean, isForWater);


            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        #endregion
    }
}
