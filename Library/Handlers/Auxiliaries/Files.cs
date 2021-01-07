using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SqlClient;

namespace CSI.Library.Handlers
{
    internal class Files
    {
        internal Files() { }
        
        #region Read Functions

        internal Library.Objects.Auxiliaries.Files.File Item(Int64 idFile)
        {
            Storage.Files _dbFiles = new Storage.Files();
            Library.Objects.Auxiliaries.Files.File _file = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbFiles.ReadById(idFile);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _file = new Library.Objects.Auxiliaries.Files.File(idFile, Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Type"]));
            }
            return _file;
        }
        internal Dictionary<Int64, Library.Objects.Auxiliaries.Files.File> Items()
        {
            Dictionary<Int64, Library.Objects.Auxiliaries.Files.File> _oItems = new Dictionary<Int64, Library.Objects.Auxiliaries.Files.File>();
            Storage.Files _dbFiles = new Storage.Files();
            Library.Objects.Auxiliaries.Files.File _file = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbFiles.ReadAll();

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _file = new Library.Objects.Auxiliaries.Files.File(Convert.ToInt64(_dbRecord["IdFile"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Type"]));

                _oItems.Add(_file.IdFile, _file);
            }
            return _oItems;
        }

        internal static Byte[] StreamByte(Int64 idFile)
        {
            Storage.Files _dbFiles = new Storage.Files();

            Byte[] _FileStream = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbFiles.ReadStream(idFile);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _FileStream = (Byte[])(_dbRecord["Content"]);
            }
            return _FileStream;

        }
        internal static MemoryStream StreamMemory(Int64 idFile)
        {
            MemoryStream _memoryStream = new MemoryStream(StreamByte(idFile));
            return _memoryStream;
        }

        #endregion

        #region Write Functions

        internal Library.Objects.Auxiliaries.Files.File Add(String name, String type, Byte[] content)
        {
            Storage.Files _dbFiles = new Storage.Files();

            Int64 _idFile = _dbFiles.Create(name, type, content);
            return Item(_idFile);
            
        }
        internal void Remove(Int64 idFile)
        {
            Storage.Files _dbFiles = new Storage.Files();

            try
            {
                _dbFiles.Delete(idFile);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 547)
                    throw new ApplicationException(Resources.Messages.ErrorCannotDeleteExistingRelationship);
                else
                    throw sqlex;
            }
        }

        #endregion

        
    }
}
