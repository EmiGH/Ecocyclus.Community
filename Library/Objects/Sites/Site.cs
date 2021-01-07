using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Sites
{
    public class Site
    {
        internal Site(Int64 idSite, Int64 idCompany, Auxiliaries.Types.SiteType type, String idLanguage, DateTime timestamp, DateTime start, Int32 weeks, String title, String number, Objects.Auxiliaries.Geographic.Contact contact, Int64 idCountry, Double value, Int64 idCurrency, Double floorSpace, Int64 units, String client, String agent, String contractor, String responsible, String manager, String description, Boolean isPublic, Int64 idImage, Security.Credential credential)
        {
            _Credential = credential;

            _IdSite = idSite;
            _IdCompany = idCompany;
            _Type = type;
            _IdCountry = idCountry;
            _Timestamp = timestamp;
            _Start = start;
            _Weeks = weeks;
            _Title = title;
            _Number = number;
            _Contact = contact;
            _Value = value;
            _IdCurrency = idCurrency;
            _FloorSpace = floorSpace;
            _Units = units;
            _Client = client;
            _Agent = agent;
            _Contractor = contractor;
            _Responsible = responsible;
            _Manager = manager;
            _Description = description;
            _IsPublic = isPublic;
            _IdImage = idImage;

            _LanguageOption = new SiteLanguageOption(idLanguage, description);
        }

        #region Private Fields

        private Security.Credential _Credential;

        private Int64 _IdSite;
        internal Int64 _IdCompany;
        private Auxiliaries.Types.SiteType _Type;
        private Int64 _IdCountry;
        private DateTime _Timestamp;
        private DateTime _Start;
        private Int32 _Weeks;
        private String _Title;
        private String _Number;
        private String _Description;
        
        private Auxiliaries.Geographic.Contact _Contact;

        private Double _Value;
        private Int64 _IdCurrency;
        private Double _FloorSpace;
        private Int64 _Units;
        private String _Client;
        private String _Agent;
        private String _Contractor;
        private String _Responsible;
        private String _Manager;
        
        private Boolean _IsPublic;
        private Int64 _IdImage;

        private SiteLanguageOption _LanguageOption;

        #endregion

        #region Public Properties

        protected Security.Credential Credential
        { get { return _Credential; } }

        public Int64 IdSite
        { get { return _IdSite; } }
        public Auxiliaries.Types.SiteType Type
        { get { return _Type; } }
        public DateTime Timestamp
        { get { return _Timestamp; } }
        public DateTime Start
        { get { return _Start; } }
        public Int32 Weeks
        { get { return _Weeks; } }
        public Auxiliaries.Geographic.Contact Contact
        { get { return _Contact; } }
        public String Title
        { get { return _Title; } }
        public String Number
        { get { return _Number; } }
        public String Description
        { get { return _Description; } }

        public String Client
        { get { return _Client; } }
        public String Agent
        { get { return _Agent; } }
        public String Contractor
        { get { return _Contractor; } }
        public String Responsible
        { get { return _Responsible; } }
        public String Manager
        { get { return _Manager; } }
        
        public Double Value
        { get { return _Value; } }
        public Auxiliaries.Units.Currency Currency
        { get { return new Handlers.Currencies().Item(_IdCurrency, Credential); } }
        public Auxiliaries.Units.Cost Cost
        { get { return new Auxiliaries.Units.Cost(_Value, Currency); } }
        public Double FloorSpace
        { get { return _FloorSpace; } }
        public Int64 Units
        { get { return _Units; } }

        public Boolean IsPublic
        { get { return _IsPublic; } }
        public Boolean IsFinished
        { get { return _Start.AddDays(_Weeks * 7) < DateTime.Now; } }

        public Auxiliaries.Geographic.Country Country
        { get { return new Handlers.Countries().Item(_IdCountry, Credential); } }
        public Auxiliaries.Files.File Image
        { get { return new Handlers.Files().Item(_IdImage); } }

        public SiteLanguageOption LanguageOption
        { get { return _LanguageOption; } }
        public Dictionary<String, SiteLanguageOption> LanguageOptions
        { get { return new Handlers.SiteLanguageOptions().Items(_IdSite); } }
        
        public Companies.Company Company
        { get { return new Handlers.Companies().Item(_IdCompany, Credential); } }
        
        #endregion

        #region Public Methods

        public Statistics GetStatistics()
        { return new Statistics(_IdSite); }

        #endregion
    }
}
