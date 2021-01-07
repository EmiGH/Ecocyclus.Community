using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace CSI.Library.Handlers
{
    internal class Languages
    {
        internal Languages() { }
        
        #region Read Functions

        internal Library.Objects.Auxiliaries.Globalization.Language Item(String idLanguage)
        {
            Storage.Languages _dbLanguages = new Storage.Languages();
            Library.Objects.Auxiliaries.Globalization.Language _Language = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbLanguages.ReadById(idLanguage);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _Language = new Library.Objects.Auxiliaries.Globalization.Language(idLanguage, Convert.ToString(_dbRecord["Name"]), Convert.ToBoolean(_dbRecord["IsDefault"]), Convert.ToBoolean(_dbRecord["Enable"]));
            }
            return _Language;
        }
        internal Library.Objects.Auxiliaries.Globalization.Language ItemDefault()
        {
            Storage.Languages _dbLanguages = new Storage.Languages();
            Library.Objects.Auxiliaries.Globalization.Language _Language = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbLanguages.ReadDefault();
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _Language = new Library.Objects.Auxiliaries.Globalization.Language(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToBoolean(_dbRecord["IsDefault"]), Convert.ToBoolean(_dbRecord["Enable"]));
            }
            return _Language;
        }
        internal Dictionary<String, Library.Objects.Auxiliaries.Globalization.Language> Items()
        {
            Dictionary<String, Library.Objects.Auxiliaries.Globalization.Language> _oItems = new Dictionary<String, Library.Objects.Auxiliaries.Globalization.Language>();
            Storage.Languages _dbLanguages = new Storage.Languages();
            Library.Objects.Auxiliaries.Globalization.Language _Language = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbLanguages.ReadAll();

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _Language = new Library.Objects.Auxiliaries.Globalization.Language(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToBoolean(_dbRecord["IsDefault"]), Convert.ToBoolean(_dbRecord["Enable"]));

                _oItems.Add(_Language.Name, _Language);
            }
            return _oItems;
        }
        internal static Dictionary<String, Library.Objects.Auxiliaries.Globalization.Language> Options()
        {
            Dictionary<String, Library.Objects.Auxiliaries.Globalization.Language> _oItems = new Dictionary<String, Library.Objects.Auxiliaries.Globalization.Language>();
            Storage.Languages _dbLanguages = new Storage.Languages();
            Library.Objects.Auxiliaries.Globalization.Language _Language = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbLanguages.ReadEnable();

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _Language = new Library.Objects.Auxiliaries.Globalization.Language(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToBoolean(_dbRecord["IsDefault"]), Convert.ToBoolean(_dbRecord["Enable"]));

                _oItems.Add(_Language.Name, _Language);
            }
            return _oItems;
        }

        #endregion

        #region Write Functions

        internal Library.Objects.Auxiliaries.Globalization.Language Add(String idLanguage, String name, Boolean enable)
        {
            Storage.Languages _dbLanguages = new Storage.Languages();

            try{
                _dbLanguages.Create(idLanguage, name, enable);
                return Item(idLanguage);

            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 2601 || sqlex.Number == 2627)
                    throw new ApplicationException(Resources.Messages.DuplicatedRecord);
                else
                    throw sqlex;
            }

        }
        internal void Remove(String idLanguage)
        {
            Storage.Languages _dbLanguages = new Storage.Languages();

            try
            {
                _dbLanguages.Delete(idLanguage);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 547)
                    throw new ApplicationException(Resources.Messages.ErrorCannotDeleteExistingRelationship);
                else
                    throw sqlex;
            }
        }
        internal void Modify(String idLanguage, String name, Boolean enable)
        {
            Storage.Languages _dbLanguages = new Storage.Languages();

            try
            {
                _dbLanguages.Update(idLanguage, name, enable);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 2601 || sqlex.Number == 2627)
                    throw new ApplicationException(Resources.Messages.DuplicatedRecord);
                else
                    throw sqlex;
            }
        }

        #endregion

    }
}
