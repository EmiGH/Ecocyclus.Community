using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Sites
{
    public class SiteMineOpen : SiteMine
    {
        internal SiteMineOpen(Int64 idSite, Int64 idCompany, Auxiliaries.Types.SiteType type, String idLanguage, DateTime timestamp, DateTime start, Int32 weeks, Auxiliaries.Units.TimeRange loadTimeRange, String title, String number, Objects.Auxiliaries.Geographic.Contact contact, Int64 idCountry, Double value, Int64 idCurrency, Double floorSpace, Int64 units, String client, String agent, String contractor, String responsible, String manager, String description, Boolean isPublic, Int64 idImage, Boolean loadOverdue, Boolean targetSurpassed, Security.Credential credential)
            : base(idSite, idCompany, type, idLanguage, timestamp, start, weeks, loadTimeRange, title, number, contact, idCountry, value, idCurrency, floorSpace, units, client, agent, contractor, responsible, manager, description, isPublic, idImage, loadOverdue, targetSurpassed, credential)
        { }

        #region Public Methods

        #endregion
    }
}
