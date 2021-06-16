using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Transactions;

namespace CSI.Library.Handlers
{
    internal class Units
    {
        internal Units() { }
        
        #region Read Functions

        internal Dictionary<Int64, Library.Objects.Auxiliaries.Units.Unit> ItemsByMagnitude(Int64 idMagnitude, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Auxiliaries.Units.Unit> _oItems = new Dictionary<Int64, Library.Objects.Auxiliaries.Units.Unit>();
            Storage.Units _dbUnits = new Storage.Units();
            Library.Objects.Auxiliaries.Units.Unit _unit = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbUnits.ReadAllByMagnitude(idMagnitude, _idLanguage);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _unit = new Library.Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential);

                _oItems.Add(_unit.IdUnit, _unit);
            }
            return _oItems;
        }
        internal Library.Objects.Auxiliaries.Units.Unit Item(Int64 idUnit, Security.Credential credential)
        {
            Storage.Units _dbUnits = new Storage.Units();
            Library.Objects.Auxiliaries.Units.Unit _unit = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbUnits.ReadById(idUnit, _idLanguage);

            Boolean _insert = true; 
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_unit!=null)
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                    {
                        _unit = new Library.Objects.Auxiliaries.Units.Unit(idUnit, Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential);
                        _insert = false;
                    }
                }
                if (_insert)
                    _unit = new Library.Objects.Auxiliaries.Units.Unit(idUnit, Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential);

                _insert = true;
            }
            return _unit;
        }
        internal Library.Objects.Auxiliaries.Units.Unit ItemForSQL(Security.Credential credential)
        {
            Storage.Units _dbUnits = new Storage.Units();
            Library.Objects.Auxiliaries.Units.Unit _unit = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbUnits.ReadForSQL(_idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_unit != null)
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                    {
                        _unit = new Library.Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential);
                        _insert = false;
                    }
                }
                if (_insert)
                    _unit = new Library.Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential);

                _insert = true;
            }
            return _unit;
        }
        internal Library.Objects.Auxiliaries.Units.Unit ItemGeoDefault(Security.Credential credential)
        {
            Storage.Units _dbUnits = new Storage.Units();
            Library.Objects.Auxiliaries.Units.Unit _unit = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbUnits.ReadGeoDefault(_idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_unit != null)
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                    {
                        _unit = new Library.Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential);
                        _insert = false;
                    }
                }
                if (_insert)
                    _unit = new Library.Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential);

                _insert = true;
            }
            return _unit;
        }
        internal Library.Objects.Auxiliaries.Units.Unit ItemForGoogle(Security.Credential credential)
        {
            Storage.Units _dbUnits = new Storage.Units();
            Library.Objects.Auxiliaries.Units.Unit _unit = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbUnits.ReadForGoogle(_idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_unit != null)
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                    {
                        _unit = new Library.Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential);
                        _insert = false;
                    }
                }
                if (_insert)
                    _unit = new Library.Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential);

                _insert = true;
            }
            return _unit;
        }

        #endregion
    }
}
