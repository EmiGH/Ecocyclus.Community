using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace CSI.Library.Handlers
{
    internal class Countries
    {
        internal Countries() { }
        
        #region Read Functions

        internal Library.Objects.Auxiliaries.Geographic.Country Item(Int64 idCountry, Security.Credential credential)
        {
            Storage.Countries _dbCountries = new Storage.Countries();
            Library.Objects.Auxiliaries.Geographic.Country _country = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbCountries.ReadById(idCountry, _idLanguage);

            Boolean _insert=true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_country!=null)
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage.ToUpper())
                    {
                        _country = new Library.Objects.Auxiliaries.Geographic.Country(idCountry, Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Code"]), Convert.ToString(_dbRecord["PhoneCode"]), Convert.ToString(_dbRecord["PaymentSystemCode"]), credential);
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                    _country = new Library.Objects.Auxiliaries.Geographic.Country(idCountry, Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Code"]), Convert.ToString(_dbRecord["PhoneCode"]), Convert.ToString(_dbRecord["PaymentSystemCode"]), credential);

                _insert = true;
            }
            return _country;
        }
        internal Library.Objects.Auxiliaries.Geographic.Country Item(String code, Security.Credential credential)
        {
            Storage.Countries _dbCountries = new Storage.Countries();
            Library.Objects.Auxiliaries.Geographic.Country _country = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbCountries.ReadByCode(code, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_country != null)
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage.ToUpper())
                    {
                        _country = new Library.Objects.Auxiliaries.Geographic.Country(Convert.ToInt64(_dbRecord["IdCountry"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Code"]), Convert.ToString(_dbRecord["PhoneCode"]), Convert.ToString(_dbRecord["PaymentSystemCode"]), credential);
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                    _country = new Library.Objects.Auxiliaries.Geographic.Country(Convert.ToInt64(_dbRecord["IdCountry"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Code"]), Convert.ToString(_dbRecord["PhoneCode"]), Convert.ToString(_dbRecord["PaymentSystemCode"]), credential);

                _insert = true;
            }
            return _country;
        }
        internal Dictionary<Int64, Library.Objects.Auxiliaries.Geographic.Country> Items(Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Auxiliaries.Geographic.Country> _oItems = new Dictionary<Int64, Library.Objects.Auxiliaries.Geographic.Country>();
            Storage.Countries _dbCountries = new Storage.Countries();
            Library.Objects.Auxiliaries.Geographic.Country _Country = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbCountries.ReadAll(_idLanguage);

            Boolean _insert=true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdCountry"])))
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage.ToUpper())
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdCountry"]));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                {
                    _Country = new Library.Objects.Auxiliaries.Geographic.Country(Convert.ToInt64(_dbRecord["IdCountry"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Code"]), Convert.ToString(_dbRecord["PhoneCode"]), Convert.ToString(_dbRecord["PaymentSystemCode"]), credential);
                    _oItems.Add(_Country.IdCountry, _Country);
                }
                _insert=true;
            }
            return _oItems;
        }

        #endregion

        #region Write Functions

        internal Library.Objects.Auxiliaries.Geographic.Country Add(String name, String code, String phoneCode, Security.Credential credential)
        {
            Storage.Countries _dbCountries = new Storage.Countries();
            String _defaultLanguage = new Languages().ItemDefault().IdLanguage;

            try 
            {

                Int64 _idCountry = _dbCountries.Create(_defaultLanguage, name, code, phoneCode);
                return Item(_idCountry, credential);

            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 2601 || sqlex.Number == 2627)
                    throw new ApplicationException(Resources.Messages.DuplicatedRecord);
                else
                    throw sqlex;
            }
            
        }
        internal void Remove(Int64 idCountry)
        {
            Storage.Countries _dbCountries = new Storage.Countries();

            try
            {
                _dbCountries.Delete(idCountry);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 547)
                    throw new ApplicationException(Resources.Messages.ErrorCannotDeleteExistingRelationship);
                else
                    throw sqlex;
            }
        }
        internal void Modify(Int64 idCountry, String name, String code, String phoneCode)
        {
            Storage.Countries _dbCountries = new Storage.Countries();

            String _idLanguage = new Languages().ItemDefault().IdLanguage;

            try
            {
                _dbCountries.Update(idCountry, _idLanguage, name, code, phoneCode);

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
