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
    internal class Sites
    {
        internal Sites() { }
        
        #region Read Methods

        internal IEnumerable<DbDataRecord> ReadAllByOperator(Int64 idOperator, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Sites_ReadAllByOperator");
            _db.AddInParameter(_dbCommand, "IdCompanyUser", DbType.Int64, idOperator);
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
        internal IEnumerable<DbDataRecord> ReadByIdByOperator(Int64 idSite, Int64 idOperator, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Sites_ReadByIdByOperator");
            _db.AddInParameter(_dbCommand, "IdSite", DbType.Int64, idSite);
            _db.AddInParameter(_dbCommand, "IdCompanyUser", DbType.Int64, idOperator);
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

        internal IEnumerable<DbDataRecord> ReadAllOverdue(Int16 idForYear, Int16 idForMonth, Int16 idForWeek, Int16 idForDay)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Sites_CalculateScheduleOverdue");
            _db.AddInParameter(_dbCommand, "YearIdUnit", DbType.Int64, idForYear);
            _db.AddInParameter(_dbCommand, "MonthIdUnit", DbType.Int64, idForMonth);
            _db.AddInParameter(_dbCommand, "WeekIdUnit", DbType.Int64, idForWeek);
            _db.AddInParameter(_dbCommand, "DayIdUnit", DbType.Int64, idForDay);

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
        internal IEnumerable<DbDataRecord> ReadAllNotice(Int16 idForYear, Int16 idForMonth, Int16 idForWeek, Int16 idForDay)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Sites_CalculateScheduleNotice");
            _db.AddInParameter(_dbCommand, "YearIdUnit", DbType.Int64, idForYear);
            _db.AddInParameter(_dbCommand, "MonthIdUnit", DbType.Int64, idForMonth);
            _db.AddInParameter(_dbCommand, "WeekIdUnit", DbType.Int64, idForWeek);
            _db.AddInParameter(_dbCommand, "DayIdUnit", DbType.Int64, idForDay);
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

        internal Int32 MetersQuantity(Int64 idSite)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Sites_Meters");
            _db.AddInParameter(_dbCommand, "IdSite", DbType.Int64, idSite);
            _db.AddOutParameter(_dbCommand, "Meters", DbType.Int32, 4);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt32(_db.GetParameterValue(_dbCommand, "Meters"));
        }

        #endregion

        #region Write Methods

        internal Int64 Create(String idLanguage, Int64 idCompany, Int64 idSiteType, DateTime start, Int32 weeks, String title, String number, String location, Objects.Auxiliaries.Geographic.Position position, Int64 idCountry, Double value, Int64 idCurrency, Double floorSpace, Int64 units, String telephone, String email, String website, String facebook, String twitter, String client, String agent, String contractor, String responsible, String manager, String description, Boolean isPublic, Int64 idImage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            SqlCommand _dbCommand = new SqlCommand("Sites_Create");
            _dbCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter _param = _dbCommand.CreateParameter();
            _param.ParameterName = "@Position";
            _param.Value = position.ToSqlGeography;
            _param.UdtTypeName = "Geography";
            _dbCommand.Parameters.Add(_param);
            
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "IdCompany", DbType.Int64, idCompany);
            _db.AddInParameter(_dbCommand, "IdSiteType", DbType.Int64, idSiteType);
            _db.AddInParameter(_dbCommand, "Start", DbType.DateTime, start);
            _db.AddInParameter(_dbCommand, "Weeks", DbType.Int32, weeks);
            _db.AddInParameter(_dbCommand, "Title", DbType.String, title);
            _db.AddInParameter(_dbCommand, "Number", DbType.String, number);
            _db.AddInParameter(_dbCommand, "Location", DbType.String, location);
            _db.AddInParameter(_dbCommand, "IdCountry", DbType.Int64, idCountry);
            _db.AddInParameter(_dbCommand, "Value", DbType.Double, value);
            _db.AddInParameter(_dbCommand, "IdCurrency", DbType.Int64, idCurrency);
            _db.AddInParameter(_dbCommand, "FloorSpace", DbType.Double, floorSpace);
            _db.AddInParameter(_dbCommand, "Units", DbType.Int64, Auxiliaries.Common.CastValueToNull(units, DBNull.Value));
            _db.AddInParameter(_dbCommand, "Telephone", DbType.String, telephone);
            _db.AddInParameter(_dbCommand, "Email", DbType.String, email);
            _db.AddInParameter(_dbCommand, "Website", DbType.String, website);
            _db.AddInParameter(_dbCommand, "Facebook", DbType.String, facebook);
            _db.AddInParameter(_dbCommand, "Twitter", DbType.String, twitter);
            _db.AddInParameter(_dbCommand, "Client", DbType.String, client);
            _db.AddInParameter(_dbCommand, "Agent", DbType.String, agent);
            _db.AddInParameter(_dbCommand, "Responsible", DbType.String, responsible);
            _db.AddInParameter(_dbCommand, "Manager", DbType.String, manager);
            _db.AddInParameter(_dbCommand, "Contractor", DbType.String, contractor);
            _db.AddInParameter(_dbCommand, "Description", DbType.String, description);
            _db.AddInParameter(_dbCommand, "IsPublic", DbType.Boolean, isPublic);
            _db.AddInParameter(_dbCommand, "IdImage", DbType.Int64, Auxiliaries.Common.CastValueToNull(idImage, DBNull.Value));

            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdSite", DbType.Int64, 18);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdSite"));

        }
        internal void Delete(Int64 idSite)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Sites_Delete");
            _db.AddInParameter(_dbCommand, "IdSite", DbType.Int64, idSite);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Update(String idLanguage, Int64 idSite, Int64 idSiteType, DateTime start, Int32 weeks, String title, String number, String location, Objects.Auxiliaries.Geographic.Position position, Int64 idCountry, Double value, Int64 idCurrency, Double floorSpace, Int64 units, String telephone, String email, String website, String facebook, String twitter, String client, String agent, String contractor, String responsible, String manager, String description, Boolean isPublic, Int64 idImage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            SqlCommand _dbCommand = new SqlCommand("Sites_Update");
            _dbCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter _param = _dbCommand.CreateParameter();
            _param.ParameterName = "@Position";
            _param.Value = position.ToSqlGeography;
            _param.UdtTypeName = "Geography";
            _dbCommand.Parameters.Add(_param);

            _db.AddInParameter(_dbCommand, "IdSite", DbType.Int64, idSite);
            _db.AddInParameter(_dbCommand, "IdSiteType", DbType.Int64, idSiteType);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "Start", DbType.DateTime, start);
            _db.AddInParameter(_dbCommand, "Weeks", DbType.Int32, weeks);
            _db.AddInParameter(_dbCommand, "Title", DbType.String, title);
            _db.AddInParameter(_dbCommand, "Number", DbType.String, number);
            _db.AddInParameter(_dbCommand, "Location", DbType.String, location);
            _db.AddInParameter(_dbCommand, "IdCountry", DbType.Int64, idCountry);
            _db.AddInParameter(_dbCommand, "Value", DbType.Double, value);
            _db.AddInParameter(_dbCommand, "IdCurrency", DbType.Int64, idCurrency);
            _db.AddInParameter(_dbCommand, "FloorSpace", DbType.Double, floorSpace);
            _db.AddInParameter(_dbCommand, "Units", DbType.Int64, Auxiliaries.Common.CastValueToNull(units, DBNull.Value));
            _db.AddInParameter(_dbCommand, "Telephone", DbType.String, telephone);
            _db.AddInParameter(_dbCommand, "Email", DbType.String, email);
            _db.AddInParameter(_dbCommand, "Website", DbType.String, website);
            _db.AddInParameter(_dbCommand, "Facebook", DbType.String, facebook);
            _db.AddInParameter(_dbCommand, "Twitter", DbType.String, twitter);
            _db.AddInParameter(_dbCommand, "Client", DbType.String, client);
            _db.AddInParameter(_dbCommand, "Agent", DbType.String, agent);
            _db.AddInParameter(_dbCommand, "Responsible", DbType.String, responsible);
            _db.AddInParameter(_dbCommand, "Manager", DbType.String, manager);
            _db.AddInParameter(_dbCommand, "Contractor", DbType.String, contractor);
            _db.AddInParameter(_dbCommand, "Description", DbType.String, description);
            _db.AddInParameter(_dbCommand, "IsPublic", DbType.Boolean, isPublic);
            _db.AddInParameter(_dbCommand, "IdImage", DbType.Int64, Auxiliaries.Common.CastValueToNull(idImage, DBNull.Value));

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

        }
        internal void UpdateStatus(Int64 idSite, Boolean isClosed)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            SqlCommand _dbCommand = new SqlCommand("Sites_UpdateStatus");
            _dbCommand.CommandType = CommandType.StoredProcedure;

            _db.AddInParameter(_dbCommand, "IdSite", DbType.Int64, idSite);
            _db.AddInParameter(_dbCommand, "IsClosed", DbType.Boolean, isClosed);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

        }
        internal void UpdateValidLoadRange(Int64 idSite, DateTime newStart, DateTime newEnd)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            SqlCommand _dbCommand = new SqlCommand("Sites_UpdateValidLoadRange");
            _dbCommand.CommandType = CommandType.StoredProcedure;
            
            _db.AddInParameter(_dbCommand, "IdSite", DbType.Int64, idSite);
            _db.AddInParameter(_dbCommand, "NewStart", DbType.DateTime, newStart);
            _db.AddInParameter(_dbCommand, "NewEnd", DbType.DateTime, newEnd);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

        }
        internal void UpdateScheduleStatus()
        {
            Database _db = DatabaseFactory.CreateDatabase();

            SqlCommand _dbCommand = new SqlCommand("Sites_UpdateScheduleStatusAll");
            _dbCommand.CommandType = CommandType.StoredProcedure;

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

        }
        internal void UpdateScheduleStatus(Int64 idSite)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            SqlCommand _dbCommand = new SqlCommand("Sites_UpdateScheduleStatus");
            _dbCommand.CommandType = CommandType.StoredProcedure;

            _db.AddInParameter(_dbCommand, "IdSite", DbType.Int64, idSite);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

        }
        
        #endregion

    }
}
