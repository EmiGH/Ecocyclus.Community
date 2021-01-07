using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Data;


namespace CSI.Library.Objects.Users
{
    public class UserOperatorMeManager : UserOperatorMe
    {
        internal UserOperatorMeManager(Int64 idOperator, Int64 idUser, Int64 idCompany, DateTime timestamp, String email, String firstname, String lastname, Int64 idPicture, String idLanguage, Boolean isManager, Boolean isActive, Security.Credential credential)
            : base(idOperator, idUser, idCompany, timestamp, email, firstname, lastname, idPicture, idLanguage, isManager, isActive, credential)
        { }


        #region Private Fields

        #endregion

        #region Public Methods

        #region Read Methods

        public List<String> SearchClients(String clientPattern)
        {
            return new Handlers.Companies().SearchClients(Company.IdCompany, clientPattern);
        }
        public List<String> SearchAgents(String agentPattern)
        {
            return new Handlers.Companies().SearchClients(Company.IdCompany, agentPattern);
        }
        public List<String> SearchContractors(String contractorPattern)
        {
            return new Handlers.Companies().SearchContractors(Company.IdCompany, contractorPattern);
        }
        public List<String> SearchResponsible(String responsiblePattern)
        {
            return new Handlers.Companies().SearchClients(Company.IdCompany, responsiblePattern);
        }
        public List<String> SearchManagers(String managerPattern)
        {
            return new Handlers.Companies().SearchClients(Company.IdCompany, managerPattern);
        }

        #endregion

        #region Write Methods

        #region Directory

        public void ModifyCompany(String name, String location, Objects.Auxiliaries.Geographic.Position position, String telephone, String email, String website, String facebook, String twitter, String fileName, String fileType, Byte[] fileContent)
        {
            using (TransactionScope _transaction = new TransactionScope())
            {
                Auxiliaries.Files.File _logo = new Handlers.Files().Add(fileName, fileType, fileContent);
                ModifyCompany(name, location, position, telephone, email, website, facebook, twitter, _logo.IdFile);

                _transaction.Complete();
            }
        }
        public void ModifyCompany(String name, String location, Objects.Auxiliaries.Geographic.Position position, String telephone, String email, String website, String facebook, String twitter)
        {
            Companies.Company _company = Company;
            Auxiliaries.Files.File _logo = _company.Logo;
            Int64 _idLogo = _logo != null ? _logo.IdFile : 0;

            ModifyCompany(name, location, position, telephone, email, website, facebook, twitter, _idLogo);
        }
        private void ModifyCompany(String name, String location, Objects.Auxiliaries.Geographic.Position position, String telephone, String email, String website, String facebook, String twitter, Int64 idLogo)
        {
            Companies.Company _company = Company;

            //Check name and email
            String _name, _email;
            _name = _company.Name == name ? "" : name;

            if (email != "")
            {
                _email = _company.Contact.Email;
                _email = _email == email ? "" : email;
            }
            else
                _email = "";

            Security.Authority.CheckCompanyData(_name, _email);

            Handlers.Companies _companies = new Handlers.Companies();
            _companies.Modify(_company.IdCompany, name, location, position, telephone, email, website, facebook, twitter, idLogo);
        }
        public void ModifyCompany(String name, String telephone, String email, String website, String facebook, String twitter, String fileName, String fileType, Byte[] fileContent)
        {
            using (TransactionScope _transaction = new TransactionScope())
            {
                Auxiliaries.Files.File _logo = new Handlers.Files().Add(fileName, fileType, fileContent);
                ModifyCompany(name, telephone, email, website, facebook, twitter, _logo.IdFile);

                _transaction.Complete();
            }
        }
        public void ModifyCompany(String name, String telephone, String email, String website, String facebook, String twitter)
        {
            Companies.Company _company = Company;
            Auxiliaries.Files.File _logo = _company.Logo;
            Int64 _idLogo = _logo != null ? _logo.IdFile : 0;

            ModifyCompany(name, telephone, email, website, facebook, twitter, _idLogo);
        }
        private void ModifyCompany(String name, String telephone, String email, String website, String facebook, String twitter, Int64 idLogo)
        {
            Companies.Company _company = Company;

            //Check name and email
            String _name, _email;
            _name = _company.Name == name ? "" : name;

            if (email != "")
            {
                _email = _company.Contact.Email;
                _email = _email == email ? "" : email;
            }
            else
                _email = "";

            Security.Authority.CheckCompanyData(_name, _email);

            Handlers.Companies _companies = new Handlers.Companies();
            _companies.Modify(_company.IdCompany, name, telephone, email, website, facebook, twitter, idLogo);
        }

        public UserOperator AddOperator(String firstname, String lastname, String email, String password, String idLanguage, Boolean isManager, Boolean isActive, String fileName, String fileType, Byte[] fileContent)
        {
            using (TransactionScope _transaction = new TransactionScope())
            {
                Auxiliaries.Files.File _picture = new Handlers.Files().Add(fileName, fileType, fileContent);
                UserOperator _operator = SaveOperator(firstname, lastname, email, password, idLanguage, isManager, isActive, _picture.IdFile);

                _transaction.Complete();

                return _operator;
            }
        }
        public UserOperator AddOperator(String firstname, String lastname, String email, String password, String idLanguage, Boolean isManager, Boolean isActive)
        {
            return SaveOperator(firstname, lastname, email, password, idLanguage, isManager, isActive, 0);
        }
        public void ModifyOperator(UserOperatorCoworker coworker, String firstname, String lastname, String email, String idLanguage, Boolean isManager, Boolean isActive, String fileName, String fileType, Byte[] fileContent)
        {
            using (TransactionScope _transaction = new TransactionScope())
            {
                Auxiliaries.Files.File _picture = new Handlers.Files().Add(fileName, fileType, fileContent);
                SaveOperator(coworker.IdOperator, coworker.IdUser, firstname, lastname, email, idLanguage, isManager, isActive, _picture.IdFile);

                _transaction.Complete();
            }
        }
        public void ModifyOperator(UserOperatorCoworker coworker, String firstname, String lastname, String email, String idLanguage, Boolean isManager, Boolean isActive)
        {
            Auxiliaries.Files.File _picture = this.GetOperator(coworker.IdOperator).Picture;
            Int64 _idPicture = _picture != null ? _picture.IdFile : 0;

            SaveOperator(coworker.IdOperator, coworker.IdUser, firstname, lastname, email, idLanguage, isManager, isActive, _idPicture);
        }

        private UserOperator SaveOperator(String firstname, String lastname, String email, String password, String idLanguage, Boolean isManager, Boolean isActive, Int64 idPicture)
        {
            Objects.Users.UserOperator _operator;
            
            using (TransactionScope _transaction = new TransactionScope())
            {
                _operator = new Handlers.Operators().Add(Company.IdCompany, email, firstname, lastname, Security.Cryptography.Hash(password), idPicture, isActive, isManager, idLanguage, Credential);

                //Send mail
                Objects.Notifications.Mailer _mailer = new Objects.Notifications.Mailer();
                String _body = Resources.Messages.AccountNewMailBody;
                _body = _body.Replace("[user]", email);
                _body = _body.Replace("[password]", password);
                _body += Resources.Messages.MailFooter;
                _mailer.Send(email, Resources.Messages.AccountNewMailSubject, _body);

                _transaction.Complete();
            }
            return _operator;
        }
        private void SaveOperator(Int64 idOperator, Int64 idUser, String firstname, String lastname, String email, String idLanguage, Boolean isManager, Boolean isActive, Int64 idPicture)
        {
            new Handlers.Operators().Modify(idOperator, idUser, Company.IdCompany, email, firstname, lastname, idPicture, isActive, isManager, idLanguage);
        }

        public void RemoveOperator(UserOperatorCoworker coworker)
        {
            new Handlers.Operators().Remove(coworker.Company.IdCompany, coworker.IdOperator, coworker.IdUser, Credential);
        }
        public void AddPermissions(Users.UserOperatorCoworker coworker, Objects.Sites.Permission.PermissionsStructure permissions)
        {
            //Only if user is not a manager
            if(!coworker.IsManager)
                new Handlers.Permissions().AddByOperator(coworker.IdOperator, permissions.ToDataTable());
        }

        #endregion

        #region Sites

        public Sites.Site AddSite(Int64 idSiteType, DateTime start, Int32 weeks, String title, String number, String location, Objects.Auxiliaries.Geographic.Position position, Int64 idCountry, Double value, Int64 idCurrency, Double floorSpace, Int64 units, String telephone, String email, String website, String facebook, String twitter, String client, String agent, String contractor, String responsible, String manager, String description, List<KeyValuePair<String, String>> descriptionTranslations, Boolean isPublic, String fileName, String fileType, Byte[] fileContent)
        {
            using (TransactionScope _transaction = new TransactionScope())
            {
                Auxiliaries.Files.File _image = new Handlers.Files().Add(fileName, fileType, fileContent);
                Sites.Site _site = AddSite(idSiteType, start, weeks, title, number, location, position, idCountry, value, idCurrency, floorSpace, units, telephone, email, website, facebook, twitter, client, agent, contractor, responsible, manager, description, descriptionTranslations, isPublic, _image.IdFile);

                _transaction.Complete();

                return _site;
            }
        }
        public Sites.Site AddSite(Int64 idSiteType, DateTime start, Int32 weeks, String title, String number, String location, Objects.Auxiliaries.Geographic.Position position, Int64 idCountry, Double value, Int64 idCurrency, Double floorSpace, Int64 units, String telephone, String email, String website, String facebook, String twitter, String client, String agent, String contractor, String responsible, String manager, String description, List<KeyValuePair<String, String>> descriptionTranslations, Boolean isPublic)
        {
            return AddSite(idSiteType, start, weeks, title, number, location, position, idCountry, value, idCurrency, floorSpace, units, telephone, email, website, facebook, twitter, client, agent, contractor, responsible, manager, description, descriptionTranslations, isPublic, 0);
        }
        private Sites.Site AddSite(Int64 idSiteType, DateTime start, Int32 weeks, String title, String number, String location, Objects.Auxiliaries.Geographic.Position position, Int64 idCountry, Double value, Int64 idCurrency, Double floorSpace, Int64 units, String telephone, String email, String website, String facebook, String twitter, String client, String agent, String contractor, String responsible, String manager, String description, List<KeyValuePair<String, String>> descriptionTranslations, Boolean isPublic, Int64 idImage)
        {
            return new Handlers.Sites().Add(this.Company.IdCompany, idSiteType, start, weeks, title, number, location, position, idCountry, value, idCurrency, floorSpace, units, telephone, email, website, facebook, twitter, client, agent, contractor, responsible, manager, description, descriptionTranslations, isPublic, idImage, Credential);
        }

        public void ModifySite(Sites.SiteMineOpen site, Int64 idSiteType, DateTime start, Int32 weeks, String title, String number, String location, Objects.Auxiliaries.Geographic.Position position, Int64 idCountry, Double value, Int64 idCurrency, Double floorSpace, Int64 units, String telephone, String email, String website, String facebook, String twitter, String client, String agent, String contractor, String responsible, String manager, String description, List<KeyValuePair<String, String>> descriptionTranslations, Boolean isPublic, String fileName, String fileType, Byte[] fileContent)
        {
            using (TransactionScope _transaction = new TransactionScope())
            {
                Auxiliaries.Files.File _image = new Handlers.Files().Add(fileName, fileType, fileContent);
                ModifySite(site, idSiteType, start, weeks, title, number, location, position, idCountry, value, idCurrency, floorSpace, units, telephone, email, website, facebook, twitter, client, agent, contractor, responsible, manager, description, descriptionTranslations, isPublic, _image.IdFile);

                _transaction.Complete();
            }
        }
        public void ModifySite(Sites.SiteMineOpen site, Int64 idSiteType, DateTime start, Int32 weeks, String title, String number, String location, Objects.Auxiliaries.Geographic.Position position, Int64 idCountry, Double value, Int64 idCurrency, Double floorSpace, Int64 units, String telephone, String email, String website, String facebook, String twitter, String client, String agent, String contractor, String responsible, String manager, String description, List<KeyValuePair<String, String>> descriptionTranslations, Boolean isPublic)
        {
            Auxiliaries.Files.File _image = this.GetSite(site.IdSite).Image;
            Int64 _idImage = _image != null ? _image.IdFile : 0;

            ModifySite(site, idSiteType, start, weeks, title, number, location, position, idCountry, value, idCurrency, floorSpace, units, telephone, email, website, facebook, twitter, client, agent, contractor, responsible, manager, description, descriptionTranslations, isPublic, _idImage);
        }
        private void ModifySite(Sites.SiteMineOpen site, Int64 idSiteType, DateTime start, Int32 weeks, String title, String number, String location, Objects.Auxiliaries.Geographic.Position position, Int64 idCountry, Double value, Int64 idCurrency, Double floorSpace, Int64 units, String telephone, String email, String website, String facebook, String twitter, String client, String agent, String contractor, String responsible, String manager, String description, List<KeyValuePair<String, String>> descriptionTranslations, Boolean isPublic, Int64 idImage)
        {
            new Handlers.Sites().Modify(site.IdSite, idSiteType, start, weeks, title, number, location, position, idCountry, value, idCurrency, floorSpace, units, telephone, email, website, facebook, twitter, client, agent, contractor, responsible, manager, description, descriptionTranslations, isPublic, idImage);
        }

        public void RemoveSite(Sites.SiteMine site)
        {
            new Handlers.Sites().Remove(site.IdSite);
        }

        public void ToggleSite(Sites.SiteMine site)
        {
            new Handlers.Sites().UpdateStatus(site.IdSite, site is Sites.SiteMineOpen);
        }

        #region Payments

        public Dictionary<Int64, Sites.Payments.Payment> Payments(Sites.SiteMine site)
        {
            return new Handlers.SitePayments().ItemsBySite(site.IdSite, Credential);
        }
        public Sites.Payments.Payment Payment(Int64 idSitePayment)
        {
            return new Handlers.SitePayments().Item(idSitePayment, Credential);
        }
        public Sites.Payments.Payment AddPayment(Sites.SiteMineOpen site, DateTime from, DateTime to, Double amount, Auxiliaries.Units.Currency currency, String idTransaction, String data)
        {
            return new Handlers.SitePayments().Add(site, IdOperator, from, to, amount, currency.IdCurrency, idTransaction, data, Credential);
        }
        public Sites.Payments.Payment ModifyPayment(Sites.SiteMineOpen site, Sites.Payments.Payment payment, String idTransaction, Boolean confirmed, DateTime confirmedDate, String confirmedMessage)
        {
            return new Handlers.SitePayments().Modify(site, payment, idTransaction, confirmed, confirmedDate, confirmedMessage, Credential);
        }
        
        #endregion
        
        #region Targets

        public void ModifyTargets(Sites.SiteMineOpen site, Double electricityConsumption, Int64 idElectricityUnit, Double electricityCO2, Double fuelConsumption, Int64 idFuelUnit, Double fuelCO2, Double transportConsumption, Int64 idTransportUnit, Double transportCO2, Double wasteConsumption, Int64 idWasteUnit, Double wasteCO2, Double waterConsumption, Int64 idWaterUnit, Double waterCO2, Double totalCO2)
        {
            //Permission Check
            if (!site.IsGranted(this, Security.Authority.PermissionTypes.SiteManager))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            new Handlers.SiteTargets().Modify(site.IdSite, electricityConsumption, idElectricityUnit, electricityCO2, fuelConsumption, idFuelUnit, fuelCO2, transportConsumption, idTransportUnit, transportCO2, wasteConsumption, idWasteUnit, wasteCO2, waterConsumption, idWaterUnit, waterCO2, totalCO2);
        }

        #endregion
        
        #region Permission

        public void AddPermissions(Sites.SiteMine site, Objects.Sites.Permission.PermissionsStructure permissions)
        {
            //Permission Check
            if (!(site.IsGranted(this, Security.Authority.PermissionTypes.SiteOperator)))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            //Remove user if a manager is on the list
            foreach (Sites.Permission.PermissionsStructure.PermissionStructure _item in permissions.Permissions)
                if (((UserOperatorCoworker)GetOperator(_item.IdItem)).IsManager)
                    throw new ApplicationException(Resources.Messages.PermissionsCannotChangeManager);

            new Handlers.Permissions().AddBySite(site.IdSite, permissions.ToDataTable());
        }

        #endregion

        #endregion

        #endregion

        #endregion
    }
}
