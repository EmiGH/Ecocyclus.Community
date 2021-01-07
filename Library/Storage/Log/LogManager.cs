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
    internal class LogManager
    {
        internal LogManager() { }

        internal void LogError(Int64 idUser, String error)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Log_Create");
            _db.AddInParameter(_dbCommand, "IdUser", DbType.Int64, idUser);
            _db.AddInParameter(_dbCommand, "Message", DbType.String, error);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
    }
}
