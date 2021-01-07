using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Sites
{
    internal class fSite
    {
        internal fSite()
        { }

        internal static Site CreateSite(Int64 idCompany, Int64 idSite, Auxiliaries.Types.SiteType type, String idLanguage, DateTime timestamp, DateTime start, Int32 weeks, Objects.Auxiliaries.Units.TimeRange loadTimeRange, String title, String number, Objects.Auxiliaries.Geographic.Contact contact, Int64 idCountry, Double value, Int64 idCurrency, Double floorSpace, Int64 units, String client, String agent, String contractor, String responsible, String manager, String description, Boolean isPublic, Int64 idImage, Boolean loadOverdue, Boolean targetSurpassed, Boolean isClosed, Security.Credential credential)
        {
            if (credential.CurrentUser is Users.UserAdministrator)
                return CreateSiteOther(idSite, idCompany, type, idLanguage, timestamp, start, weeks, title, number, contact, idCountry, value, idCurrency, floorSpace, units, client, agent, contractor, responsible, manager, description, isPublic, idImage, credential);
            else
            {
                if (idCompany == ((Users.UserOperator)credential.CurrentUser).Company.IdCompany)
                    return CreateSiteMine(idSite, idCompany, type, idLanguage, timestamp, start, weeks, loadTimeRange, title, number, contact, idCountry, value, idCurrency, floorSpace, units, client, agent, contractor, responsible, manager, description, isPublic, idImage, loadOverdue, targetSurpassed, isClosed, credential);
                return CreateSiteOther(idSite, idCompany, type, idLanguage, timestamp, start, weeks, title, number, contact, idCountry, value, idCurrency, floorSpace, units, client, agent, contractor, responsible, manager, description, isPublic, idImage, credential);
            }
        }
        private static SiteMine CreateSiteMine(Int64 idSite, Int64 idCompany, Auxiliaries.Types.SiteType type, String idLanguage, DateTime timestamp, DateTime start, Int32 weeks, Objects.Auxiliaries.Units.TimeRange loadTimeRange, String title, String number, Objects.Auxiliaries.Geographic.Contact contact, Int64 idCountry, Double value, Int64 idCurrency, Double floorSpace, Int64 units, String client, String agent, String contractor, String responsible, String manager, String description, Boolean isPublic, Int64 idImage, Boolean loadOverdue, Boolean targetSurpassed, Boolean isClosed, Security.Credential credential)
        {
            if(isClosed)
                return new SiteMine(idSite, idCompany, type, idLanguage, timestamp, start, weeks, loadTimeRange, title, number, contact, idCountry, value, idCurrency, floorSpace, units, client, agent, contractor, responsible, manager, description, isPublic, idImage, loadOverdue, targetSurpassed, credential);

            return new SiteMineOpen(idSite, idCompany, type, idLanguage, timestamp, start, weeks, loadTimeRange, title, number, contact, idCountry, value, idCurrency, floorSpace, units, client, agent, contractor, responsible, manager, description, isPublic, idImage, loadOverdue, targetSurpassed, credential); 
        }
        private static Site CreateSiteOther(Int64 idSite, Int64 idCompany, Auxiliaries.Types.SiteType type, String idLanguage, DateTime timestamp, DateTime start, Int32 weeks, String title, String number, Objects.Auxiliaries.Geographic.Contact contact, Int64 idCountry, Double value, Int64 idCurrency, Double floorSpace, Int64 units, String client, String agent, String contractor, String responsible, String manager, String description, Boolean isPublic, Int64 idImage, Security.Credential credential)
        {
            return new Site(idSite, idCompany, type, idLanguage, timestamp, start, weeks, title, number, contact, idCountry, value, idCurrency, floorSpace, units, client, agent, contractor, responsible, manager, description, isPublic, idImage, credential); 
        }
    }
}
