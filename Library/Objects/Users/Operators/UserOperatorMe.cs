using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace CSI.Library.Objects.Users
{
    public class UserOperatorMe : UserOperatorCoworker
    {
        internal UserOperatorMe(Int64 idOperator, Int64 idUser, Int64 idCompany, DateTime timestamp, String email, String firstname, String lastname, Int64 idPicture, String idLanguage, Boolean isManager, Boolean isActive, Security.Credential credential)
            : base(idOperator, idUser, idCompany, timestamp, email, firstname, lastname, idPicture, idLanguage, isManager, isActive, credential)
        {
            _Credential = credential;
        }

        #region Private Fields

        #endregion

        #region Public Methods

        #region Auxiliary

        #region Read Methods

        public Dictionary<String, Objects.Auxiliaries.Globalization.Language> GetLanguagesAvailable()
        {
            return Handlers.Languages.Options();
        }
        public Objects.Auxiliaries.Globalization.Language GetLanguageDefault()
        {
            return new Handlers.Languages().ItemDefault();
        }
        public Objects.Auxiliaries.Globalization.Language GetLanguageAvailable(String idLanguage)
        {
            return new Handlers.Languages().Item(idLanguage);
        }

        public Dictionary<Int64, Auxiliaries.Geographic.Country> GetCountries()
        {
            return new Handlers.Countries().Items(Credential);
        }
        public Auxiliaries.Geographic.Country GetCountry(Int64 idCountry)
        {
            return new Handlers.Countries().Item(idCountry, Credential);
        }
        public Auxiliaries.Geographic.Country GetCountry(String code)
        {
            return new Handlers.Countries().Item(code, Credential);
        }

        public Dictionary<Int64, Auxiliaries.Units.Unit> GetElectricityUnits()
        {
            return new Handlers.ElectricityUnits().Items(Credential);
        }
        public Dictionary<Int64, Auxiliaries.Units.Unit> GetFuelUnits()
        {
            return new Handlers.FuelsUnits().Items(Credential);
        }
        public Dictionary<Int64, Auxiliaries.Units.Unit> GetTransportUnits()
        {
            return new Handlers.TransportUnits().Items(Credential);
        }
        public Dictionary<Int64, Auxiliaries.Units.Unit> GetWaterUnits()
        {
            return new Handlers.WaterUnits().Items(Credential);
        }
        public Dictionary<Int64, Auxiliaries.Units.Unit> GetWasteUnits()
        {
            return new Handlers.WasteUnits().Items(Credential);
        }
        public Auxiliaries.Units.Unit GetUnit(Int64 idUnit)
        {
            return new Handlers.Units().Item(idUnit, Credential);
        }
        public Library.Objects.Auxiliaries.Units.Unit CO2DefaultUnit()
        { return new Handlers.CO2Units().ItemPattern(_Credential); }
        public Dictionary<Int64, Auxiliaries.Units.Currency> GetCurrencies()
        {
            return new Handlers.Currencies().Items(Credential);
        }
        public Auxiliaries.Units.Currency GetCurrency(Int64 idCurrency)
        {
            return new Handlers.Currencies().Item(idCurrency, Credential);
        }

        public Dictionary<Int64, Auxiliaries.Types.FuelType> GetFuelTypes()
        {
            return new Handlers.FuelTypes().Items(Credential);
        }
        public Auxiliaries.Types.FuelType GetFuelType(Int64 idFuelType)
        {
            return new Handlers.FuelTypes().Item(idFuelType, Credential);
        }
        public Dictionary<Int64, Auxiliaries.Types.WasteType> GetWasteTypes()
        {
            return new Handlers.WasteTypes().Items(Credential);
        }
        public Auxiliaries.Types.WasteType GetWasteType(Int64 idWasteType)
        {
            return new Handlers.WasteTypes().Item(idWasteType, Credential);
        }
        public Dictionary<Int64, Auxiliaries.Types.TransportType> GetTransportTypes()
        {
            return new Handlers.TransportTypes().Items(Credential);
        }
        public Auxiliaries.Types.TransportType GetTransportType(Int64 idTransportType)
        {
            return new Handlers.TransportTypes().Item(idTransportType, Credential);
        }

        public Dictionary<Int64, Auxiliaries.Types.SiteType> GetSiteTypes()
        {
            return new Handlers.SiteTypes().Items(Credential);
        }
        public Auxiliaries.Types.SiteType GetSiteType(Int64 idSiteType)
        {
            return new Handlers.SiteTypes().Item(idSiteType, Credential);
        }

        #endregion

        #endregion

        #region Directory

        #region Write Methods

        public void ChangePassword(String oldPassword, String newPassword)
        {
            new Handlers.Users().ChangePassword(IdUser, Security.Cryptography.Hash(oldPassword), Security.Cryptography.Hash(newPassword));
        }
        public void Modify(String lastname, String firstname, String email, String idLanguage, String fileName, String fileType, Byte[] fileContent)
        {
            using (TransactionScope _transaction = new TransactionScope())
            {
                Auxiliaries.Files.File _picture = new Handlers.Files().Add(fileName, fileType, fileContent);
                new Handlers.Operators().Modify(IdOperator, IdUser, Company.IdCompany, email, firstname, lastname, _picture.IdFile, IsActive, IsManager, idLanguage);

                _transaction.Complete();
            }
            
        }
        public void Modify(String lastname, String firstname, String email, String idLanguage)
        {
            Auxiliaries.Files.File _picture = Picture;
            Int64 _idPicture = _picture != null ? _picture.IdFile : 0;

            new Handlers.Operators().Modify(IdOperator, IdUser, Company.IdCompany, email, firstname, lastname, _idPicture, IsActive, IsManager, idLanguage);
        }

        #endregion

        #region Read Methods

        public override Companies.Company Company
        { get { return new Handlers.Companies().ItemForMe(idCompany, _Credential); } }

        public Companies.Company GetCompany(Int64 idCompany)
        {
            return new Handlers.Companies().Item(idCompany, _Credential);
        }
        public UserOperator GetOperator(Int64 idOperator)
        {
            return new Handlers.Operators().Item(idOperator, Credential);
        }

        #endregion

        #endregion

        #region Sites
        
        #region Read Methods

        public Dictionary<Int64, Sites.Site> GetSites()
        {
            return new Handlers.Sites().ItemsByOperator(Credential);
        }
        public Sites.Site GetSite(Int64 idSite)
        {
            Library.Objects.Sites.Site _site = new Handlers.Sites().ItemByOperator(idSite, Credential);
            return _site;
        }

        #endregion
                
        #region Meters

        #region Read Methods

        public Sites.Meters.WaterMeter GetWaterMeter(Int64 idMeter)
        {
            return new Handlers.WaterMeters().Item(idMeter, Credential);
        }
        public Sites.Meters.FuelMeter GetFuelMeter(Int64 idMeter)
        {
            return new Handlers.FuelMeters().Item(idMeter, Credential);
        }
        public Sites.Meters.WasteMeter GetWasteMeter(Int64 idMeter)
        {
            return new Handlers.WasteMeters().Item(idMeter, Credential);
        }
        public Sites.Meters.TransportMeter GetTransportMeter(Int64 idMeter)
        {
            return new Handlers.TransportMeters().Item(idMeter, Credential);
        }
        public Sites.Meters.ElectricityMeter GetElectricityMeter(Int64 idMeter)
        {
            return new Handlers.ElectricityMeters().Item(idMeter, Credential);
        }
        
        #endregion

        #region Write Methods

        public Sites.Meters.WaterMeter AddMeterWater(Sites.SiteMineOpen site, String identification, String description, List<KeyValuePair<String, String>> descriptionTranslations, Sites.Meters.Series.WaterDataEmissionFactor emissionFactor, Boolean isPhysical, DateTime initialDate, Double initialReading, Auxiliaries.Units.Unit unit, Int16 frequencyQuantity, Auxiliaries.Units.TimeUnit.Units frequencyUnit, Int16 alertBefore, Int16 alertAfter, Boolean alertOnStart)
        {
            //Permission Check
            if (!site.IsGranted(this, Security.Authority.PermissionTypes.SiteManager))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            return new Handlers.WaterMeters().Add(site, identification, description, descriptionTranslations, isPhysical, initialDate, initialReading, emissionFactor, unit.IdUnit, frequencyQuantity, (Int16)frequencyUnit, alertBefore, alertAfter, alertOnStart, Credential);
        }
        public Sites.Meters.WaterMeter AddMeterWater(Sites.SiteMineOpen site, String identification, String description, List<KeyValuePair<String, String>> descriptionTranslations, Boolean isPhysical, DateTime initialDate, Double initialReading, Auxiliaries.Units.Unit unit, Int16 frequencyQuantity, Auxiliaries.Units.TimeUnit.Units frequencyUnit, Int16 alertBefore, Int16 alertAfter, Boolean alertOnStart)
        {
            //Permission Check
            if (!site.IsGranted(this, Security.Authority.PermissionTypes.SiteManager))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            return new Handlers.WaterMeters().Add(site, identification, description, descriptionTranslations, isPhysical, initialDate, initialReading, unit.IdUnit, frequencyQuantity, (Int16)frequencyUnit, alertBefore, alertAfter, alertOnStart, Credential);
        }
        public void ModifyMeterWater(Sites.Meters.WaterMeter meter, String identification, String description, List<KeyValuePair<String, String>> descriptionTranslations, Sites.Meters.Series.WaterDataEmissionFactor emissionFactor, Auxiliaries.Units.Unit unit, Int16 frequencyQuantity, Auxiliaries.Units.TimeUnit.Units frequencyUnit, Int16 alertBefore, Int16 alertAfter, Boolean alertOnStart)
        {
            //Check if it is Open
            if(!(meter.Site is Sites.SiteMineOpen))
                throw new ApplicationException(Resources.Messages.SiteClosed);

            //Permission Check
            if (!((Sites.SiteMine)meter.Site).IsGranted(this, Security.Authority.PermissionTypes.SiteManager))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            Handlers.WaterMeters _handler = new Handlers.WaterMeters();

            if (_handler.HasValues(meter.IdMeter))
                throw new ApplicationException(Resources.Messages.MeterHasValue);

            new Handlers.WaterMeters().Modify(meter, identification, description, descriptionTranslations, emissionFactor, unit, frequencyQuantity, (Int16)frequencyUnit, alertBefore, alertAfter, alertOnStart, Credential);
        }
        public void ModifyMeterWater(Sites.Meters.WaterMeter meter, String identification, String description, List<KeyValuePair<String, String>> descriptionTranslations, Auxiliaries.Units.Unit unit, Int16 frequencyQuantity, Auxiliaries.Units.TimeUnit.Units frequencyUnit, Int16 alertBefore, Int16 alertAfter, Boolean alertOnStart)
        {
            //Check if it is Open
            if (!(meter.Site is Sites.SiteMineOpen))
                throw new ApplicationException(Resources.Messages.SiteClosed);

            //Permission Check
            if (!((Sites.SiteMine)meter.Site).IsGranted(this, Security.Authority.PermissionTypes.SiteManager))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            Handlers.WaterMeters _handler = new Handlers.WaterMeters();

            new Handlers.WaterMeters().Modify(meter, identification, description, descriptionTranslations, meter.DefaultUnit, frequencyQuantity, (Int16)frequencyUnit, alertBefore, alertAfter, alertOnStart, Credential);
        }
        public void ModifyMeterWater(Sites.Meters.WaterMeterPhysical meter, String identification, String description, List<KeyValuePair<String, String>> descriptionTranslations, Sites.Meters.Series.WaterDataEmissionFactor emissionFactor, DateTime initialDate, Double initialReading, Auxiliaries.Units.Unit unit, Int16 frequencyQuantity, Auxiliaries.Units.TimeUnit.Units frequencyUnit, Int16 alertBefore, Int16 alertAfter, Boolean alertOnStart)
        {
            //Check if it is Open
            if (!(meter.Site is Sites.SiteMineOpen))
                throw new ApplicationException(Resources.Messages.SiteClosed);

            //Permission Check
            if (!((Sites.SiteMine)meter.Site).IsGranted(this, Security.Authority.PermissionTypes.SiteManager))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            Handlers.WaterMeters _handler = new Handlers.WaterMeters();

            if (_handler.HasValues(meter.IdMeter))
                throw new ApplicationException(Resources.Messages.MeterHasValue);

            new Handlers.WaterMeters().Modify(meter, identification, description, descriptionTranslations, initialDate, initialReading, emissionFactor, unit, frequencyQuantity, (Int16)frequencyUnit, alertBefore, alertAfter, alertOnStart, Credential);
        }
        public void ModifyMeterWater(Sites.Meters.WaterMeterPhysical meter, String identification, String description, List<KeyValuePair<String, String>> descriptionTranslations, DateTime initialDate, Double initialReading, Auxiliaries.Units.Unit unit, Int16 frequencyQuantity, Auxiliaries.Units.TimeUnit.Units frequencyUnit, Int16 alertBefore, Int16 alertAfter, Boolean alertOnStart)
        {
            //Check if it is Open
            if (!(meter.Site is Sites.SiteMineOpen))
                throw new ApplicationException(Resources.Messages.SiteClosed);

            //Permission Check
            if (!((Sites.SiteMine)meter.Site).IsGranted(this, Security.Authority.PermissionTypes.SiteManager))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            Handlers.WaterMeters _handler = new Handlers.WaterMeters();

            new Handlers.WaterMeters().Modify(meter, identification, description, descriptionTranslations, initialDate, initialReading, meter.DefaultUnit, frequencyQuantity, (Int16)frequencyUnit, alertBefore, alertAfter, alertOnStart, Credential);
        }

        public Sites.Meters.WasteMeter AddMeterWaste(Sites.SiteMineOpen site, String identification, String description, List<KeyValuePair<String, String>> descriptionTranslations, Auxiliaries.Units.Unit defaultUnit, List<Sites.Meters.Series.WasteDataEmissionFactor> emissionFactors)
        {
            //Permission Check
            if (!site.IsGranted(this, Security.Authority.PermissionTypes.SiteManager))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            return new Handlers.WasteMeters().Add(site.IdSite, identification, description, descriptionTranslations, defaultUnit.IdUnit, emissionFactors, Credential);
        }
        public Sites.Meters.WasteMeter AddMeterWaste(Sites.SiteMineOpen site, String identification, String description, List<KeyValuePair<String, String>> descriptionTranslations, Auxiliaries.Units.Unit defaultUnit)
        {
            //Permission Check
            if (!site.IsGranted(this, Security.Authority.PermissionTypes.SiteManager))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            return new Handlers.WasteMeters().Add(site.IdSite, identification, description, descriptionTranslations, defaultUnit.IdUnit, Credential);
        }
        public void ModifyMeterWaste(Sites.Meters.WasteMeter meter, String identification, String description, List<KeyValuePair<String, String>> descriptionTranslations, Auxiliaries.Units.Unit defaultUnit, List<Sites.Meters.Series.WasteDataEmissionFactor> emissionFactors)
        {
            //Check if it is Open
            if (!(meter.Site is Sites.SiteMineOpen))
                throw new ApplicationException(Resources.Messages.SiteClosed);

            //Permission Check
            if (!((Sites.SiteMine)meter.Site).IsGranted(this, Security.Authority.PermissionTypes.SiteManager))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            new Handlers.WasteMeters().Modify(meter, identification, description, descriptionTranslations, defaultUnit.IdUnit, emissionFactors, Credential);
        }
        public void ModifyMeterWaste(Sites.Meters.WasteMeter meter, String identification, String description, List<KeyValuePair<String, String>> descriptionTranslations, Auxiliaries.Units.Unit defaultUnit)
        {
            //Check if it is Open
            if (!(meter.Site is Sites.SiteMineOpen))
                throw new ApplicationException(Resources.Messages.SiteClosed);

            //Permission Check
            if (!((Sites.SiteMine)meter.Site).IsGranted(this, Security.Authority.PermissionTypes.SiteManager))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            new Handlers.WasteMeters().Modify(meter, identification, description, descriptionTranslations, defaultUnit.IdUnit, Credential);
        }

        public Sites.Meters.FuelMeter AddMeterFuel(Sites.SiteMineOpen site, String identification, String description, List<KeyValuePair<String, String>> descriptionTranslations, Auxiliaries.Units.Unit defaultUnit, List<Sites.Meters.Series.FuelDataEmissionFactor> emissionFactors)
        {
            //Permission Check
            if (!site.IsGranted(this, Security.Authority.PermissionTypes.SiteManager))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            return new Handlers.FuelMeters().Add(site.IdSite, identification, description, descriptionTranslations, defaultUnit.IdUnit, emissionFactors, Credential);
        }
        public Sites.Meters.FuelMeter AddMeterFuel(Sites.SiteMineOpen site, String identification, String description, List<KeyValuePair<String, String>> descriptionTranslations, Auxiliaries.Units.Unit defaultUnit)
        {
            //Permission Check
            if (!site.IsGranted(this, Security.Authority.PermissionTypes.SiteManager))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            return new Handlers.FuelMeters().Add(site.IdSite, identification, description, descriptionTranslations, defaultUnit.IdUnit, Credential);
        }
        public void ModifyMeterFuel(Sites.Meters.FuelMeter meter, String identification, String description, List<KeyValuePair<String, String>> descriptionTranslations, Auxiliaries.Units.Unit defaultUnit, List<Sites.Meters.Series.FuelDataEmissionFactor> emissionFactors)
        {
            //Check if it is Open
            if (!(meter.Site is Sites.SiteMineOpen))
                throw new ApplicationException(Resources.Messages.SiteClosed);

            //Permission Check
            if (!((Sites.SiteMine)meter.Site).IsGranted(this, Security.Authority.PermissionTypes.SiteManager))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            new Handlers.FuelMeters().Modify(meter, identification, description, descriptionTranslations, defaultUnit.IdUnit, emissionFactors, Credential);
        }
        public void ModifyMeterFuel(Sites.Meters.FuelMeter meter, String identification, String description, List<KeyValuePair<String, String>> descriptionTranslations, Auxiliaries.Units.Unit defaultUnit)
        {
            //Check if it is Open
            if (!(meter.Site is Sites.SiteMineOpen))
                throw new ApplicationException(Resources.Messages.SiteClosed);

            //Permission Check
            if (!((Sites.SiteMine)meter.Site).IsGranted(this, Security.Authority.PermissionTypes.SiteManager))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            new Handlers.FuelMeters().Modify(meter, identification, description, descriptionTranslations, defaultUnit.IdUnit, Credential);
        }

        public Sites.Meters.ElectricityMeter AddMeterElectricity(Sites.SiteMineOpen site, String identification, String description, List<KeyValuePair<String, String>> descriptionTranslations, Sites.Meters.Series.ElectricityDataEmissionFactor emissionFactor, Boolean isPhysical, DateTime initialDate, Double initialReading, Auxiliaries.Units.Unit unit, Int16 frequencyQuantity, Auxiliaries.Units.TimeUnit.Units frequencyUnit, Int16 alertBefore, Int16 alertAfter, Boolean alertOnStart)
        {
            //Permission Check
            if (!site.IsGranted(this, Security.Authority.PermissionTypes.SiteManager))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            return new Handlers.ElectricityMeters().Add(site, identification, description, descriptionTranslations, isPhysical, initialDate, initialReading, emissionFactor, unit.IdUnit, frequencyQuantity, (Int16)frequencyUnit, alertBefore, alertAfter, alertOnStart, Credential);
        }
        public Sites.Meters.ElectricityMeter AddMeterElectricity(Sites.SiteMineOpen site, String identification, String description, List<KeyValuePair<String, String>> descriptionTranslations, Boolean isPhysical, DateTime initialDate, Double initialReading, Auxiliaries.Units.Unit unit, Int16 frequencyQuantity, Auxiliaries.Units.TimeUnit.Units frequencyUnit, Int16 alertBefore, Int16 alertAfter, Boolean alertOnStart)
        {
            //Permission Check
            if (!site.IsGranted(this, Security.Authority.PermissionTypes.SiteManager))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            return new Handlers.ElectricityMeters().Add(site, identification, description, descriptionTranslations, isPhysical, initialDate, initialReading, unit.IdUnit, frequencyQuantity, (Int16)frequencyUnit, alertBefore, alertAfter, alertOnStart, Credential);
        }
        public void ModifyMeterElectricity(Sites.Meters.ElectricityMeter meter, String identification, String description, List<KeyValuePair<String, String>> descriptionTranslations, Sites.Meters.Series.ElectricityDataEmissionFactor emissionFactor, Auxiliaries.Units.Unit unit, Int16 frequencyQuantity, Auxiliaries.Units.TimeUnit.Units frequencyUnit, Int16 alertBefore, Int16 alertAfter, Boolean alertOnStart)
        {
            //Check if it is Open
            if (!(meter.Site is Sites.SiteMineOpen))
                throw new ApplicationException(Resources.Messages.SiteClosed);

            //Permission Check
            if (!((Sites.SiteMine)meter.Site).IsGranted(this, Security.Authority.PermissionTypes.SiteManager))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            Handlers.ElectricityMeters _handler = new Handlers.ElectricityMeters();

            if (_handler.HasValues(meter.IdMeter))
                throw new ApplicationException(Resources.Messages.MeterHasValue);

            new Handlers.ElectricityMeters().Modify(meter, identification, description, descriptionTranslations, emissionFactor, unit, frequencyQuantity, (Int16)frequencyUnit, alertBefore, alertAfter, alertOnStart, Credential);
        }
        public void ModifyMeterElectricity(Sites.Meters.ElectricityMeter meter, String identification, String description, List<KeyValuePair<String, String>> descriptionTranslations, Auxiliaries.Units.Unit unit, Int16 frequencyQuantity, Auxiliaries.Units.TimeUnit.Units frequencyUnit, Int16 alertBefore, Int16 alertAfter, Boolean alertOnStart)
        {
            //Check if it is Open
            if (!(meter.Site is Sites.SiteMineOpen))
                throw new ApplicationException(Resources.Messages.SiteClosed);

            //Permission Check
            if (!((Sites.SiteMine)meter.Site).IsGranted(this, Security.Authority.PermissionTypes.SiteManager))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            Handlers.ElectricityMeters _handler = new Handlers.ElectricityMeters();

            new Handlers.ElectricityMeters().Modify(meter, identification, description, descriptionTranslations, meter.DefaultUnit, frequencyQuantity, (Int16)frequencyUnit, alertBefore, alertAfter, alertOnStart, Credential);
        }
        public void ModifyMeterElectricity(Sites.Meters.ElectricityMeterPhysical meter, String identification, String description, List<KeyValuePair<String, String>> descriptionTranslations, Sites.Meters.Series.ElectricityDataEmissionFactor emissionFactor, DateTime initialDate, Double initialReading, Auxiliaries.Units.Unit unit, Int16 frequencyQuantity, Auxiliaries.Units.TimeUnit.Units frequencyUnit, Int16 alertBefore, Int16 alertAfter, Boolean alertOnStart)
        {
            //Check if it is Open
            if (!(meter.Site is Sites.SiteMineOpen))
                throw new ApplicationException(Resources.Messages.SiteClosed);

            //Permission Check
            if (!((Sites.SiteMine)meter.Site).IsGranted(this, Security.Authority.PermissionTypes.SiteManager))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            Handlers.ElectricityMeters _handler = new Handlers.ElectricityMeters();

            if (_handler.HasValues(meter.IdMeter))
                throw new ApplicationException(Resources.Messages.MeterHasValue);

            new Handlers.ElectricityMeters().Modify(meter, identification, description, descriptionTranslations, initialDate, initialReading, emissionFactor, unit, frequencyQuantity, (Int16)frequencyUnit, alertBefore, alertAfter, alertOnStart, Credential);
        }
        public void ModifyMeterElectricity(Sites.Meters.ElectricityMeterPhysical meter, String identification, String description, List<KeyValuePair<String, String>> descriptionTranslations, DateTime initialDate, Double initialReading, Auxiliaries.Units.Unit unit, Int16 frequencyQuantity, Auxiliaries.Units.TimeUnit.Units frequencyUnit, Int16 alertBefore, Int16 alertAfter, Boolean alertOnStart)
        {
            //Check if it is Open
            if (!(meter.Site is Sites.SiteMineOpen))
                throw new ApplicationException(Resources.Messages.SiteClosed);

            //Permission Check
            if (!((Sites.SiteMine)meter.Site).IsGranted(this, Security.Authority.PermissionTypes.SiteManager))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            Handlers.ElectricityMeters _handler = new Handlers.ElectricityMeters();

            new Handlers.ElectricityMeters().Modify(meter, identification, description, descriptionTranslations, initialDate, initialReading, meter.DefaultUnit, frequencyQuantity, (Int16)frequencyUnit, alertBefore, alertAfter, alertOnStart, Credential);
        }

        public Sites.Meters.TransportMeter AddMeterTransport(Sites.SiteMineOpen site, String identification, String description, List<KeyValuePair<String, String>> descriptionTranslations, Auxiliaries.Units.Unit defaultUnit, List<Sites.Meters.Series.TransportDataEmissionFactor> emissionFactors)
        {
            //Permission Check
            if (!site.IsGranted(this, Security.Authority.PermissionTypes.SiteManager))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            return new Handlers.TransportMeters().Add(site.IdSite, identification, description, descriptionTranslations, defaultUnit.IdUnit, emissionFactors, Credential);
        }
        public Sites.Meters.TransportMeter AddMeterTransport(Sites.SiteMineOpen site, String identification, String description, List<KeyValuePair<String, String>> descriptionTranslations, Auxiliaries.Units.Unit defaultUnit)
        {
            //Permission Check
            if (!site.IsGranted(this, Security.Authority.PermissionTypes.SiteManager))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            return new Handlers.TransportMeters().Add(site.IdSite, identification, description, descriptionTranslations, defaultUnit.IdUnit, Credential);
        }
        public void ModifyMeterTransport(Sites.Meters.TransportMeter meter, String identification, String description, List<KeyValuePair<String, String>> descriptionTranslations, Auxiliaries.Units.Unit defaultUnit, List<Sites.Meters.Series.TransportDataEmissionFactor> emissionFactors)
        {
            //Check if it is Open
            if (!(meter.Site is Sites.SiteMineOpen))
                throw new ApplicationException(Resources.Messages.SiteClosed);

            //Permission Check
            if (!((Sites.SiteMine)meter.Site).IsGranted(this, Security.Authority.PermissionTypes.SiteManager))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            new Handlers.TransportMeters().Modify(meter, identification, description, descriptionTranslations, defaultUnit.IdUnit, emissionFactors, Credential);
        }
        public void ModifyMeterTransport(Sites.Meters.TransportMeter meter, String identification, String description, List<KeyValuePair<String, String>> descriptionTranslations, Auxiliaries.Units.Unit defaultUnit)
        {
            //Check if it is Open
            if (!(meter.Site is Sites.SiteMineOpen))
                throw new ApplicationException(Resources.Messages.SiteClosed);

            //Permission Check
            if (!((Sites.SiteMine)meter.Site).IsGranted(this, Security.Authority.PermissionTypes.SiteManager))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            new Handlers.TransportMeters().Modify(meter, identification, description, descriptionTranslations, defaultUnit.IdUnit, Credential);
        }

        public void RemoveMeterElectricity(Sites.Meters.ElectricityMeter meter)
        {
            //Check if it is Open
            if (!(meter.Site is Sites.SiteMineOpen))
                throw new ApplicationException(Resources.Messages.SiteClosed);

            //Permission Check
            if (!((Sites.SiteMine)meter.Site).IsGranted(this, Security.Authority.PermissionTypes.SiteManager))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            new Handlers.ElectricityMeters().Remove(meter);
        }
        public void RemoveMeterFuels(Sites.Meters.FuelMeter meter)
        {
            //Check if it is Open
            if (!(meter.Site is Sites.SiteMineOpen))
                throw new ApplicationException(Resources.Messages.SiteClosed);

            //Permission Check
            if (!((Sites.SiteMine)meter.Site).IsGranted(this, Security.Authority.PermissionTypes.SiteManager))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            new Handlers.FuelMeters().Remove(meter);
        }
        public void RemoveMeterTransport(Sites.Meters.TransportMeter meter)
        {
            //Check if it is Open
            if (!(meter.Site is Sites.SiteMineOpen))
                throw new ApplicationException(Resources.Messages.SiteClosed);

            //Permission Check
            if (!((Sites.SiteMine)meter.Site).IsGranted(this, Security.Authority.PermissionTypes.SiteManager))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            new Handlers.TransportMeters().Remove(meter);
        }
        public void RemoveMeterWater(Sites.Meters.WaterMeter meter)
        {
            //Check if it is Open
            if (!(meter.Site is Sites.SiteMineOpen))
                throw new ApplicationException(Resources.Messages.SiteClosed);

            //Permission Check
            if (!((Sites.SiteMine)meter.Site).IsGranted(this, Security.Authority.PermissionTypes.SiteManager))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            new Handlers.WaterMeters().Remove(meter);
        }
        public void RemoveMeterWaste(Sites.Meters.WasteMeter meter)
        {
            //Check if it is Open
            if (!(meter.Site is Sites.SiteMineOpen))
                throw new ApplicationException(Resources.Messages.SiteClosed);

            //Permission Check
            if (!((Sites.SiteMine)meter.Site).IsGranted(this, Security.Authority.PermissionTypes.SiteManager))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            new Handlers.WasteMeters().Remove(meter);
        }

        #endregion
        
        #endregion

        #region Data

        #region Read Methods

        
        #endregion

        #region Write Methods

        public void AddElectricityData(Objects.Sites.Meters.ElectricityMeter meter, List<Objects.Sites.Meters.Series.ElectricityData> data)
        {
            //Check if it is Open
            if (!(meter.Site is Sites.SiteMineOpen))
                throw new ApplicationException(Resources.Messages.SiteClosed);

            
            //Permission Check
            if (!((Sites.SiteMine)meter.Site).IsGranted(this, Security.Authority.PermissionTypes.SiteOperator))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            Handlers.ElectricityMeterLoads _handler = new Handlers.ElectricityMeterLoads(); ;
            if(meter is Sites.Meters.ElectricityMeterPhysical)
                _handler.Add((Sites.Meters.ElectricityMeterPhysical)meter, data, Credential);
            else
                _handler.Add(meter, data, Credential);

        }
        public void RemoveElectricityData(Objects.Sites.Meters.ElectricityMeter meter, Objects.Sites.Meters.Series.ElectricityLoad load)
        {
            //Check if it is Open
            if (!(meter.Site is Sites.SiteMineOpen))
                throw new ApplicationException(Resources.Messages.SiteClosed);

            //Permission Check
            if (!((Sites.SiteMine)meter.Site).IsGranted(this, Security.Authority.PermissionTypes.SiteOperator))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            new Handlers.ElectricityMeterLoads().Remove(meter, load.From);
        }
        public void RemoveElectricityData(Objects.Sites.Meters.ElectricityMeter meter, DateTime from)
        {
            //Check if it is Open
            if (!(meter.Site is Sites.SiteMineOpen))
                throw new ApplicationException(Resources.Messages.SiteClosed);

            //Permission Check
            if (!((Sites.SiteMine)meter.Site).IsGranted(this, Security.Authority.PermissionTypes.SiteOperator))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            new Handlers.ElectricityMeterLoads().Remove(meter, from);
        }
        public void ModifyElectricityData(Objects.Sites.Meters.ElectricityMeter meter, Sites.Meters.Series.ElectricityLoad load, Double data, Auxiliaries.Units.Unit unit)
        {
            //Check if it is Open
            if (!(meter.Site is Sites.SiteMineOpen))
                throw new ApplicationException(Resources.Messages.SiteClosed);

            //Permission Check
            if (!((Sites.SiteMine)meter.Site).IsGranted(this, Security.Authority.PermissionTypes.SiteOperator))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            if(meter is Sites.Meters.ElectricityMeterPhysical)
                new Handlers.ElectricityMeterLoads().Modify((Sites.Meters.ElectricityMeterPhysical)meter, load, data, unit.IdUnit, Credential);
            else
                new Handlers.ElectricityMeterLoads().Modify(meter, load, data, unit.IdUnit, Credential);
        }
        
        public void AddWaterData(Objects.Sites.Meters.WaterMeter meter, List<Objects.Sites.Meters.Series.WaterData> data)
        {
            //Check if it is Open
            if (!(meter.Site is Sites.SiteMineOpen))
                throw new ApplicationException(Resources.Messages.SiteClosed);

            
            //Permission Check
            if (!((Sites.SiteMine)meter.Site).IsGranted(this, Security.Authority.PermissionTypes.SiteOperator))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            Handlers.WaterMeterLoads _handler = new Handlers.WaterMeterLoads();
            if(meter is Objects.Sites.Meters.WaterMeterPhysical)
                _handler.Add((Objects.Sites.Meters.WaterMeterPhysical)meter, data, Credential);
            else
                _handler.Add(meter, data, Credential);

        }
        public void RemoveWaterData(Objects.Sites.Meters.WaterMeter meter, Objects.Sites.Meters.Series.WaterLoad load)
        {
            //Check if it is Open
            if (!(meter.Site is Sites.SiteMineOpen))
                throw new ApplicationException(Resources.Messages.SiteClosed);

            //Permission Check
            if (!((Sites.SiteMine)meter.Site).IsGranted(this, Security.Authority.PermissionTypes.SiteOperator))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            new Handlers.WaterMeterLoads().Remove(meter, load.From);
        }
        public void RemoveWaterData(Objects.Sites.Meters.WaterMeter meter, DateTime from)
        {
            //Check if it is Open
            if (!(meter.Site is Sites.SiteMineOpen))
                throw new ApplicationException(Resources.Messages.SiteClosed);

            //Permission Check
            if (!((Sites.SiteMine)meter.Site).IsGranted(this, Security.Authority.PermissionTypes.SiteOperator))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            new Handlers.WaterMeterLoads().Remove(meter, from);
        }
        public void ModifyWaterData(Objects.Sites.Meters.WaterMeter meter, Sites.Meters.Series.WaterLoad load, Double data, Auxiliaries.Units.Unit unit)
        {
            //Check if it is Open
            if (!(meter.Site is Sites.SiteMineOpen))
                throw new ApplicationException(Resources.Messages.SiteClosed);

            //Permission Check
            if (!((Sites.SiteMine)meter.Site).IsGranted(this, Security.Authority.PermissionTypes.SiteOperator))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            if(meter is Sites.Meters.WaterMeterPhysical)
                new Handlers.WaterMeterLoads().Modify((Sites.Meters.WaterMeterPhysical)meter, load, data, unit.IdUnit, Credential);
            else
                new Handlers.WaterMeterLoads().Modify(meter, load, data, unit.IdUnit, Credential);
        }
        
        public void AddFuelData(Objects.Sites.Meters.FuelMeter meter, List<Objects.Sites.Meters.Series.FuelData> data)
        {
            //Check if it is Open
            if (!(meter.Site is Sites.SiteMineOpen))
                throw new ApplicationException(Resources.Messages.SiteClosed);

            
            //Permission Check
            if (!((Sites.SiteMine)meter.Site).IsGranted(this, Security.Authority.PermissionTypes.SiteOperator))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            Handlers.FuelMeterSeries _handler = new Handlers.FuelMeterSeries(); ;
            _handler.Add(meter, data, Credential);

        }
        public void ModifyFuelData(Objects.Sites.Meters.FuelMeter meter, Sites.Meters.Series.FuelSerie serie, Objects.Sites.Meters.Series.FuelData data)
        {
            //Check if it is Open
            if (!(meter.Site is Sites.SiteMineOpen))
                throw new ApplicationException(Resources.Messages.SiteClosed);

            //Permission Check
            if (!((Sites.SiteMine)meter.Site).IsGranted(this, Security.Authority.PermissionTypes.SiteOperator))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            new Handlers.FuelMeterSeries().Modify(meter, serie.IdSerie, data, Credential);
        }
        public void RemoveFuelData(Objects.Sites.Meters.FuelMeter meter, Objects.Sites.Meters.Series.FuelSerie serie)
        {
            //Check if it is Open
            if (!(meter.Site is Sites.SiteMineOpen))
                throw new ApplicationException(Resources.Messages.SiteClosed);

            //Permission Check
            if (!((Sites.SiteMine)meter.Site).IsGranted(this, Security.Authority.PermissionTypes.SiteOperator))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            new Handlers.FuelMeterSeries().Remove(serie.IdSerie);
        }

        public void AddTransportData(Objects.Sites.Meters.TransportMeter meter, List<Objects.Sites.Meters.Series.TransportData> data)
        {
            //Check if it is Open
            if (!(meter.Site is Sites.SiteMineOpen))
                throw new ApplicationException(Resources.Messages.SiteClosed);

            
            //Permission Check
            if (!((Sites.SiteMine)meter.Site).IsGranted(this, Security.Authority.PermissionTypes.SiteOperator))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            Handlers.TransportMeterSeries _handler = new Handlers.TransportMeterSeries(); ;
            _handler.Add(meter, data, Credential);

        }
        public void ModifyTransportData(Objects.Sites.Meters.TransportMeter meter, Sites.Meters.Series.TransportSerie serie, Objects.Sites.Meters.Series.TransportData data)
        {
            //Check if it is Open
            if (!(meter.Site is Sites.SiteMineOpen))
                throw new ApplicationException(Resources.Messages.SiteClosed);

            //Permission Check
            if (!((Sites.SiteMine)meter.Site).IsGranted(this, Security.Authority.PermissionTypes.SiteOperator))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            new Handlers.TransportMeterSeries().Modify(meter, serie.IdSerie, data, Credential);
        }
        public void RemoveTransportData(Objects.Sites.Meters.TransportMeter meter, Objects.Sites.Meters.Series.TransportSerie serie)
        {
            //Check if it is Open
            if (!(meter.Site is Sites.SiteMineOpen))
                throw new ApplicationException(Resources.Messages.SiteClosed);

            //Permission Check
            if (!((Sites.SiteMine)meter.Site).IsGranted(this, Security.Authority.PermissionTypes.SiteOperator))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            new Handlers.TransportMeterSeries().Remove(serie.IdSerie);
        }

        public void AddWasteData(Objects.Sites.Meters.WasteMeter meter, List<Objects.Sites.Meters.Series.WasteData> data)
        {
            //Check if it is Open
            if (!(meter.Site is Sites.SiteMineOpen))
                throw new ApplicationException(Resources.Messages.SiteClosed);

            
            //Permission Check
            if (!((Sites.SiteMine)meter.Site).IsGranted(this, Security.Authority.PermissionTypes.SiteOperator))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            Handlers.WasteMeterSeries _handler = new Handlers.WasteMeterSeries(); ;
            _handler.Add(meter, data, Credential);

        }
        public void ModifyWasteData(Objects.Sites.Meters.WasteMeter meter, Sites.Meters.Series.WasteSerie serie, Objects.Sites.Meters.Series.WasteData data)
        {
            //Check if it is Open
            if (!(meter.Site is Sites.SiteMineOpen))
                throw new ApplicationException(Resources.Messages.SiteClosed);

            //Permission Check
            if (!((Sites.SiteMine)meter.Site).IsGranted(this, Security.Authority.PermissionTypes.SiteOperator))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            new Handlers.WasteMeterSeries().Modify(meter, serie.IdSerie, data, Credential);
        }
        public void RemoveWasteData(Objects.Sites.Meters.WasteMeter meter, Objects.Sites.Meters.Series.WasteSerie serie)
        {
            //Check if it is Open
            if (!(meter.Site is Sites.SiteMineOpen))
                throw new ApplicationException(Resources.Messages.SiteClosed);

            //Permission Check
            if (!((Sites.SiteMine)meter.Site).IsGranted(this, Security.Authority.PermissionTypes.SiteOperator))
                throw new ApplicationException(Resources.Messages.PermissionDenied);

            new Handlers.WasteMeterSeries().Remove(serie.IdSerie);
        }

        #endregion

        #endregion
        
        #endregion

        #endregion
    }
}
