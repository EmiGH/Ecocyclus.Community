using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Types;
using System.Data.SqlClient;
using System.Transactions;

namespace CSI.Library.Handlers
{
    internal class Companies
    {
        internal Companies() { }
        
        #region Read Functions

        internal Library.Objects.Companies.Company Item(Int64 idCompany, Security.Credential credential)
        {
            Storage.Companies _dbCompanies = new Storage.Companies();
            Library.Objects.Companies.Company _Company = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbCompanies.ReadById(idCompany);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _Company = Create(idCompany, Convert.ToString(_dbRecord["Name"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToString(_dbRecord["Location"]), Convert.ToString(_dbRecord["Position"]), Convert.ToString(_dbRecord["Telephone"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Website"]), Convert.ToString(_dbRecord["Facebook"]), Convert.ToString(_dbRecord["Twitter"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdLogo"], 0)), credential, false);
            }
            return _Company;
        }
        internal Library.Objects.Companies.Company ItemForMe(Int64 idCompany, Security.Credential credential)
        {
            Storage.Companies _dbCompanies = new Storage.Companies();
            Library.Objects.Companies.Company _Company = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbCompanies.ReadById(idCompany);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _Company = Create(idCompany, Convert.ToString(_dbRecord["Name"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToString(_dbRecord["Location"]), Convert.ToString(_dbRecord["Position"]), Convert.ToString(_dbRecord["Telephone"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Website"]), Convert.ToString(_dbRecord["Facebook"]), Convert.ToString(_dbRecord["Twitter"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdLogo"], 0)), credential, true);
            }
            return _Company;
        }
        internal Library.Objects.Companies.Company Item(String name, Security.Credential credential)
        {
            Storage.Companies _dbCompanies = new Storage.Companies();
            Library.Objects.Companies.Company _Company = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbCompanies.ReadByName(name);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _Company = Create(Convert.ToInt64(_dbRecord["IdCompany"]), Convert.ToString(_dbRecord["Name"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToString(_dbRecord["Location"]), Convert.ToString(_dbRecord["Position"]), Convert.ToString(_dbRecord["Telephone"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Website"]), Convert.ToString(_dbRecord["Facebook"]), Convert.ToString(_dbRecord["Twitter"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdLogo"], 0)), credential, false);
            }
            return _Company;
        }
        internal Dictionary<Int64, Library.Objects.Companies.Company> Items(Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Companies.Company> _oItems = new Dictionary<Int64, Library.Objects.Companies.Company>();
            Storage.Companies _dbCompanies = new Storage.Companies();
            Library.Objects.Companies.Company _Company = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbCompanies.ReadAll();

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _Company = Create(Convert.ToInt64(_dbRecord["IdCompany"]), Convert.ToString(_dbRecord["Name"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToString(_dbRecord["Location"]), Convert.ToString(_dbRecord["Position"]), Convert.ToString(_dbRecord["Telephone"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Website"]), Convert.ToString(_dbRecord["Facebook"]), Convert.ToString(_dbRecord["Twitter"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdLogo"], 0)), credential, false);
              
                _oItems.Add(_Company.IdCompany, _Company);
            }
            return _oItems;
        }

        private Objects.Companies.Company Create(Int64 idCompany, String name, DateTime timestamp, String location, String position, String telephone, String email, String website, String facebook, String twitter, Int64 idLogo, Security.Credential credential, Boolean isForMe)
        {
            if(isForMe)
                if (position == "")
                    return Objects.Companies.fCompany.CreateOwnCompany(idCompany, name, timestamp, new Objects.Auxiliaries.Geographic.Contact(telephone, email, website, facebook, twitter), idLogo, credential);
                else
                return Objects.Companies.fCompany.CreateOwnCompany(idCompany, name, timestamp, new Objects.Auxiliaries.Geographic.Contact(new Objects.Auxiliaries.Geographic.Location(location, new Objects.Auxiliaries.Geographic.Position(SqlGeography.Parse(position))), telephone, email, website, facebook, twitter), idLogo, credential);
            
            else
                if (position == "")
                    return Objects.Companies.fCompany.CreateCompany(idCompany, name, timestamp, new Objects.Auxiliaries.Geographic.Contact(telephone, email, website, facebook, twitter), idLogo, credential);
                else
                    return Objects.Companies.fCompany.CreateCompany(idCompany, name, timestamp, new Objects.Auxiliaries.Geographic.Contact(new Objects.Auxiliaries.Geographic.Location(location, new Objects.Auxiliaries.Geographic.Position(SqlGeography.Parse(position))), telephone, email, website, facebook, twitter), idLogo, credential);
        }

        internal Boolean CheckName(String name)
        {
            Storage.Companies _dbCompanies = new Storage.Companies();
            return _dbCompanies.CheckName(name);
        }
        internal Boolean CheckEmail(String email)
        {
            Storage.Companies _dbCompanies = new Storage.Companies();
            return _dbCompanies.CheckEmail(email);
        }

        internal Double TotalArea(Int64 idCompany)
        {
            Storage.Companies _dbCompanies = new Storage.Companies();
            return Common.CastNullValues(_dbCompanies.ReadTotalArea(idCompany), 0);
        }
        internal Objects.Auxiliaries.Units.Cost TotalCost(Int64 idCompany, Security.Credential credential)
        {
            Storage.Companies _dbCompanies = new Storage.Companies();
            Library.Objects.Auxiliaries.Units.Cost _cost=null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbCompanies.ReadTotalCost(idCompany, _idLanguage);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage.ToUpper())
                    return new Objects.Auxiliaries.Units.Cost(Convert.ToDouble(string.IsNullOrEmpty(_dbRecord["Cost"].ToString()) ? 0: _dbRecord["Cost"]), new Objects.Auxiliaries.Units.Currency(Convert.ToInt64(_dbRecord["IdCurrency"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["ConversionIndex"]), Convert.ToBoolean(_dbRecord["IsPattern"]), Convert.ToString(_dbRecord["PaymentSystemCode"]), credential));

                else
                    _cost = new Objects.Auxiliaries.Units.Cost(Convert.ToDouble(string.IsNullOrEmpty(_dbRecord["Cost"].ToString()) ? 0 : _dbRecord["Cost"]), new Objects.Auxiliaries.Units.Currency(Convert.ToInt64(_dbRecord["IdCurrency"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["ConversionIndex"]), Convert.ToBoolean(_dbRecord["IsPattern"]), Convert.ToString(_dbRecord["PaymentSystemCode"]), credential));
           }
            return _cost;
        }
     
        internal List<String> SearchClients(Int64 idCompany, String clientPattern)
        {
            List<String> _oItems = new List<String>();
            
            Storage.Companies _dbCompanies = new Storage.Companies();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbCompanies.SearchClients(idCompany, clientPattern);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _oItems.Add(Convert.ToString(_dbRecord["Client"]));
            }
            return _oItems;
        }
        internal List<String> SearchAgents(Int64 idCompany, String agentPattern)
        {
            List<String> _oItems = new List<String>();

            Storage.Companies _dbCompanies = new Storage.Companies();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbCompanies.SearchAgents(idCompany, agentPattern);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _oItems.Add(Convert.ToString(_dbRecord["Agent"]));
            }
            return _oItems;
        }
        internal List<String> SearchContractors(Int64 idCompany, String contractorPattern)
        {
            List<String> _oItems = new List<String>();

            Storage.Companies _dbCompanies = new Storage.Companies();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbCompanies.SearchContractors(idCompany, contractorPattern);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _oItems.Add(Convert.ToString(_dbRecord["Contractor"]));
            }
            return _oItems;
        }
        internal List<String> SearchResponsible(Int64 idCompany, String responsiblePattern)
        {
            List<String> _oItems = new List<String>();

            Storage.Companies _dbCompanies = new Storage.Companies();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbCompanies.SearchResponsible(idCompany, responsiblePattern);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _oItems.Add(Convert.ToString(_dbRecord["Responsible"]));
            }
            return _oItems;
        }
        internal List<String> SearchManagers(Int64 idCompany, String managerPattern)
        {
            List<String> _oItems = new List<String>();

            Storage.Companies _dbCompanies = new Storage.Companies();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbCompanies.SearchManagers(idCompany, managerPattern);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _oItems.Add(Convert.ToString(_dbRecord["Manager"]));
            }
            return _oItems;
        }

        #endregion

        #region Write Functions

        internal Library.Objects.Companies.Company Register(String name, String location, Objects.Auxiliaries.Geographic.Position position, String telephone, String email, String website, String facebook, String twitter, Int64 idLogo, Boolean active, Security.Credential credential)
        {
            Storage.Companies _dbCompanies = new Storage.Companies();

            try
            {
                Int64 _idCompany = _dbCompanies.Create(name, location, position, telephone, email, website, facebook, twitter, idLogo, active);
                return ItemForMe(_idCompany, credential);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 2601 || sqlex.Number == 2627)
                    throw new ApplicationException(Resources.Messages.DuplicatedRecord);
                else
                    throw sqlex;
            }
        }
        internal Library.Objects.Companies.Company Register(String name, String telephone, String email, String website, String facebook, String twitter, Int64 idLogo, Boolean active, Security.Credential credential)
        {
            Storage.Companies _dbCompanies = new Storage.Companies();

            try
            {
                Int64 _idCompany = _dbCompanies.Create(name, telephone, email, website, facebook, twitter, idLogo, active);
                return ItemForMe(_idCompany, credential);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 2601 || sqlex.Number == 2627)
                    throw new ApplicationException(Resources.Messages.DuplicatedRecord);
                else
                    throw sqlex;
            }
        }
        internal Library.Objects.Companies.Company Add(String name, String location, Objects.Auxiliaries.Geographic.Position position, String telephone, String email, String website, String facebook, String twitter, Int64 idLogo, Boolean active, Security.Credential credential)
        {
            Storage.Companies _dbCompanies = new Storage.Companies();

            try
            {
                Int64 _idCompany = _dbCompanies.Create(name, location, position, telephone, email, website, facebook, twitter, idLogo, active);
                return Item(_idCompany, credential);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 2601 || sqlex.Number == 2627)
                    throw new ApplicationException(Resources.Messages.DuplicatedRecord);
                else
                    throw sqlex;
            }            
        }
        internal Library.Objects.Companies.Company Add(String name, String telephone, String email, String website, String facebook, String twitter, Int64 idLogo, Boolean active, Security.Credential credential)
        {
            Storage.Companies _dbCompanies = new Storage.Companies();

            try
            {
                Int64 _idCompany = _dbCompanies.Create(name, telephone, email, website, facebook, twitter, idLogo, active);
                return Item(_idCompany, credential);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 2601 || sqlex.Number == 2627)
                    throw new ApplicationException(Resources.Messages.DuplicatedRecord);
                else
                    throw sqlex;
            }
        }
        internal void Remove(Int64 idCompany)
        {
            Storage.Companies _dbCompanies = new Storage.Companies();

            try
            {
                _dbCompanies.Delete(idCompany);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 547)
                    throw new ApplicationException(Resources.Messages.ErrorCannotDeleteExistingRelationship);
                else
                    throw sqlex;
            }
        }
        internal void Modify(Int64 idCompany, String name, String location, Objects.Auxiliaries.Geographic.Position position, String telephone, String email, String website, String facebook, String twitter, Int64 idLogo)
        {
            Storage.Companies _dbCompanies = new Storage.Companies();
            
            try
            {
                _dbCompanies.Update(idCompany, name, location, position, telephone, email, website, facebook, twitter, idLogo);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 2601 || sqlex.Number == 2627)
                    throw new ApplicationException(Resources.Messages.DuplicatedRecord);
                else
                    throw sqlex;
            }
        }
        internal void Modify(Int64 idCompany, String name, String telephone, String email, String website, String facebook, String twitter, Int64 idLogo)
        {
            Storage.Companies _dbCompanies = new Storage.Companies();

            try
            {
                _dbCompanies.Update(idCompany, name, telephone, email, website, facebook, twitter, idLogo);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 2601 || sqlex.Number == 2627)
                    throw new ApplicationException(Resources.Messages.DuplicatedRecord);
                else
                    throw sqlex;
            }
        }

        internal void ChangeActiveStatus(Int64 idCompany, Boolean active)
        {
            Storage.Companies _dbCompanies = new Storage.Companies();
            Storage.Operators _dbOperators = new Storage.Operators();

            using (TransactionScope _transactionScope = new TransactionScope())
            {
                _dbCompanies.UpdateActive(idCompany, active);
                //if inactive then inactive all, else active only managers
                if (!active)
                    _dbOperators.UpdateActiveAll(idCompany, active);
                else
                    _dbOperators.UpdateActiveManagers(idCompany, active);
                
                _transactionScope.Complete();
            }
        }

        #endregion

    }
}
