using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Handlers
{
    internal class MeterLoadFunctions
    {        
        //General Date Check
        internal static void CheckDateValidity(Objects.Auxiliaries.Units.TimeRange siteValidLoadRange, DateTime loadDate)
        {
            //Payment
            if (!siteValidLoadRange.IsInRange(loadDate))
                throw new ApplicationException(Resources.Messages.SiteLoadDateViolation);

            //Today
            if (loadDate > DateTime.Now)
                throw new ApplicationException(Resources.Messages.LoadDateViolation);

        }
    }
}
