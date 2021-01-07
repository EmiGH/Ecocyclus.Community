using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Transactions;

namespace CSI.Library.Handlers
{
    internal class TransportUnits
    {
        internal TransportUnits() { }
        
        #region Read Functions

        internal Dictionary<Int64, Library.Objects.Auxiliaries.Units.Unit> Items(Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Auxiliaries.Units.Unit> _oItems = new Dictionary<Int64, Library.Objects.Auxiliaries.Units.Unit>();
            Storage.TransportUnits _dbUnits = new Storage.TransportUnits();
            Library.Objects.Auxiliaries.Units.Unit _unit = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbUnits.ReadAll(_idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdUnit"])))
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _idLanguage)
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdUnit"]));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                {
                    _unit = new Library.Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential);
                    _oItems.Add(_unit.IdUnit, _unit);
                }
                _insert = true;
            }
            return _oItems;
        }
        
        #endregion
    }
}
