using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Transactions;

namespace CSI.Library.Handlers
{
    internal class Currencies
    {
        internal Currencies() { }
        
        #region Read Functions

        internal Dictionary<Int64, Library.Objects.Auxiliaries.Units.Currency> Items(Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Auxiliaries.Units.Currency> _oItems = new Dictionary<Int64, Library.Objects.Auxiliaries.Units.Currency>();
            Storage.Currencies _dbUnits = new Storage.Currencies();
            Library.Objects.Auxiliaries.Units.Currency _currency = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbUnits.ReadAll(_idLanguage);

            Boolean _insert = true; 
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdCurrency"])))
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdCurrency"]));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                {
                    _currency = new Library.Objects.Auxiliaries.Units.Currency(Convert.ToInt64(_dbRecord["IdCurrency"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["ConversionIndex"]), Convert.ToBoolean(_dbRecord["IsPattern"]), Convert.ToString(_dbRecord["PaymentSystemCode"]), credential);
                    _oItems.Add(_currency.IdCurrency, _currency);
                }
                _insert = true;
            }
            return _oItems;
        }
        internal Library.Objects.Auxiliaries.Units.Currency Item(Int64 idCurrency, Security.Credential credential)
        {
            Storage.Currencies _dbUnits = new Storage.Currencies();
            Library.Objects.Auxiliaries.Units.Currency _currency = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbUnits.ReadById(idCurrency, _idLanguage);

            Boolean _insert = true; 
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_currency!=null)
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                    {
                        _currency = new Library.Objects.Auxiliaries.Units.Currency(Convert.ToInt64(_dbRecord["IdCurrency"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["ConversionIndex"]), Convert.ToBoolean(_dbRecord["IsPattern"]), Convert.ToString(_dbRecord["PaymentSystemCode"]), credential);
                        _insert = false;
                    }
                }
                if (_insert)
                    _currency = new Library.Objects.Auxiliaries.Units.Currency(Convert.ToInt64(_dbRecord["IdCurrency"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["ConversionIndex"]), Convert.ToBoolean(_dbRecord["IsPattern"]), Convert.ToString(_dbRecord["PaymentSystemCode"]), credential);

                _insert = true;
            }
            return _currency;
        }
       
        #endregion
    }
}
