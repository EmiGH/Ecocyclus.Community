using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.SqlServer;
using Microsoft.SqlServer.Types;

namespace CSI.Library.Storage
{
    internal class Companies
    {
        internal Companies() { }

        #region Read Methods

        internal IEnumerable<DbDataRecord> ReadAll()
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Companies_ReadAll");
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
        internal IEnumerable<DbDataRecord> ReadById(Int64 idCompany)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Companies_ReadById");
            _db.AddInParameter(_dbCommand, "IdCompany", DbType.Int64, idCompany);
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
        internal IEnumerable<DbDataRecord> ReadByName(String name)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Companies_ReadByName");
            _db.AddInParameter(_dbCommand, "Name", DbType.String, name);
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

        internal IEnumerable<DbDataRecord> SearchClients(Int64 idCompany, String clientPattern)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Companies_SearchClients");
            _db.AddInParameter(_dbCommand, "IdCompany", DbType.Int64, idCompany);
            _db.AddInParameter(_dbCommand, "Pattern", DbType.String, clientPattern);
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
        internal IEnumerable<DbDataRecord> SearchAgents(Int64 idCompany, String agentPattern)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Companies_SearchAgents");
            _db.AddInParameter(_dbCommand, "IdCompany", DbType.Int64, idCompany);
            _db.AddInParameter(_dbCommand, "Pattern", DbType.String, agentPattern);
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
        internal IEnumerable<DbDataRecord> SearchContractors(Int64 idCompany, String contractorPattern)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Companies_SearchContractors");
            _db.AddInParameter(_dbCommand, "IdCompany", DbType.Int64, idCompany);
            _db.AddInParameter(_dbCommand, "Pattern", DbType.String, contractorPattern);
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
        internal IEnumerable<DbDataRecord> SearchResponsible(Int64 idCompany, String responsiblePattern)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Companies_SearchResponsible");
            _db.AddInParameter(_dbCommand, "IdCompany", DbType.Int64, idCompany);
            _db.AddInParameter(_dbCommand, "Pattern", DbType.String, responsiblePattern);
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
        internal IEnumerable<DbDataRecord> SearchManagers(Int64 idCompany, String managerPattern)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Companies_SearchManagers");
            _db.AddInParameter(_dbCommand, "IdCompany", DbType.Int64, idCompany);
            _db.AddInParameter(_dbCommand, "Pattern", DbType.String, managerPattern);
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
        
        internal Boolean CheckName(String name)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Companies_CheckName");
            _db.AddInParameter(_dbCommand, "Name", DbType.String, name);

            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "Exists", DbType.Boolean, 18);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToBoolean(_db.GetParameterValue(_dbCommand, "Exists"));
        }
        internal Boolean CheckEmail(String email)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Companies_CheckEmail");
            _db.AddInParameter(_dbCommand, "Email", DbType.String, email);

            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "Exists", DbType.Boolean, 18);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToBoolean(_db.GetParameterValue(_dbCommand, "Exists"));
        }

        internal Double ReadTotalArea(Int64 idCompany)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Companies_ReadTotalArea");
            _db.AddInParameter(_dbCommand, "IdCompany", DbType.Int64, idCompany);

            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "Area", DbType.Double, 16);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToDouble(_db.GetParameterValue(_dbCommand, "Area"));
        }
        internal IEnumerable<DbDataRecord> ReadTotalCost(Int64 idCompany, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Companies_ReadTotalCost");
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

        #endregion

        #region Write Methods

        internal Int64 Create(String name, String location, Objects.Auxiliaries.Geographic.Position position, String telephone, String email, String website, String facebook, String twitter, Int64 idLogo, Boolean active)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            SqlCommand _dbCommand = new SqlCommand("Companies_Create");
            _dbCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter _param = _dbCommand.CreateParameter(); 
            _param.ParameterName = "@Position";
            _param.Value = position.ToSqlGeography;
            _param.UdtTypeName = "Geography";
            _dbCommand.Parameters.Add(_param);

            _db.AddInParameter(_dbCommand, "Name", DbType.String, name);
            _db.AddInParameter(_dbCommand, "Location", DbType.String, location);
            _db.AddInParameter(_dbCommand, "Telephone", DbType.String, telephone);
            _db.AddInParameter(_dbCommand, "Email", DbType.String, email);
            _db.AddInParameter(_dbCommand, "Website", DbType.String, website);
            _db.AddInParameter(_dbCommand, "Facebook", DbType.String, facebook);
            _db.AddInParameter(_dbCommand, "Twitter", DbType.String, twitter);
            _db.AddInParameter(_dbCommand, "IdLogo", DbType.Int64, Auxiliaries.Common.CastValueToNull(idLogo,DBNull.Value));
            _db.AddInParameter(_dbCommand, "Active", DbType.Boolean, active);

            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdCompany", DbType.Int64, 18);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
            
            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdCompany"));

        }
        internal Int64 Create(String name, String telephone, String email, String website, String facebook, String twitter, Int64 idLogo, Boolean active)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            SqlCommand _dbCommand = new SqlCommand("Companies_Create");
            _dbCommand.CommandType = CommandType.StoredProcedure;

            _db.AddInParameter(_dbCommand, "Name", DbType.String, name);
            _db.AddInParameter(_dbCommand, "Telephone", DbType.String, telephone);
            _db.AddInParameter(_dbCommand, "Email", DbType.String, email);
            _db.AddInParameter(_dbCommand, "Website", DbType.String, website);
            _db.AddInParameter(_dbCommand, "Facebook", DbType.String, facebook);
            _db.AddInParameter(_dbCommand, "Twitter", DbType.String, twitter);
            _db.AddInParameter(_dbCommand, "IdLogo", DbType.Int64, Auxiliaries.Common.CastValueToNull(idLogo, DBNull.Value));
            _db.AddInParameter(_dbCommand, "Active", DbType.Boolean, active);

            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdCompany", DbType.Int64, 18);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdCompany"));

        }
        internal void Delete(Int64 idCompany)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Companies_Delete");
            _db.AddInParameter(_dbCommand, "IdCompany", DbType.Int64, idCompany);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Update(Int64 idCompany, String name, String location, Objects.Auxiliaries.Geographic.Position position, String telephone, String email, String website, String facebook, String twitter, Int64 idLogo)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            SqlCommand _dbCommand = new SqlCommand("Companies_Update");
            _dbCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter _param = _dbCommand.CreateParameter();
            _param.ParameterName = "@Position";
            _param.Value = position.ToSqlGeography;
            _param.UdtTypeName = "Geography";
            _dbCommand.Parameters.Add(_param);

            _db.AddInParameter(_dbCommand, "IdCompany", DbType.Int64, idCompany);
            _db.AddInParameter(_dbCommand, "Name", DbType.String, name);
            _db.AddInParameter(_dbCommand, "Location", DbType.String, location);
            _db.AddInParameter(_dbCommand, "Telephone", DbType.String, telephone);
            _db.AddInParameter(_dbCommand, "Email", DbType.String, email);
            _db.AddInParameter(_dbCommand, "Website", DbType.String, website);
            _db.AddInParameter(_dbCommand, "Facebook", DbType.String, facebook);
            _db.AddInParameter(_dbCommand, "Twitter", DbType.String, twitter);
            _db.AddInParameter(_dbCommand, "IdLogo", DbType.Int64, Auxiliaries.Common.CastValueToNull(idLogo, DBNull.Value));

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
            
        }
        internal void Update(Int64 idCompany, String name, String telephone, String email, String website, String facebook, String twitter, Int64 idLogo)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            SqlCommand _dbCommand = new SqlCommand("Companies_Update");
            _dbCommand.CommandType = CommandType.StoredProcedure;
            
            _db.AddInParameter(_dbCommand, "IdCompany", DbType.Int64, idCompany);
            _db.AddInParameter(_dbCommand, "Name", DbType.String, name);
            _db.AddInParameter(_dbCommand, "Telephone", DbType.String, telephone);
            _db.AddInParameter(_dbCommand, "Email", DbType.String, email);
            _db.AddInParameter(_dbCommand, "Website", DbType.String, website);
            _db.AddInParameter(_dbCommand, "Facebook", DbType.String, facebook);
            _db.AddInParameter(_dbCommand, "Twitter", DbType.String, twitter);
            _db.AddInParameter(_dbCommand, "IdLogo", DbType.Int64, Auxiliaries.Common.CastValueToNull(idLogo, DBNull.Value));

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

        }

        internal void UpdateActive(Int64 idCompany, Boolean active)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Companies_UpdateActive");
            _db.AddInParameter(_dbCommand, "IdCompany", DbType.Int64, idCompany);
            _db.AddInParameter(_dbCommand, "Active", DbType.Boolean, active);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

        }
        

        #endregion

    }
}
