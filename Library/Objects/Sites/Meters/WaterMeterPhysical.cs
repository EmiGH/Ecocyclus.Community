using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Sites.Meters
{
    public class WaterMeterPhysical: WaterMeter
    {
        internal WaterMeterPhysical(Int64 idMeter, Int64 idSite, String identification, String description, DateTime initialDate, Double initialReading, Int64 idEmissionFactor, Int64 idUnit, Int16 frequencyQuantity, Int16 frequencyUnit, Int16 alertBeforeDays, Int16 alertAfterDays, Boolean alertOnStart, Security.Credential credential)
            :base(idMeter, idSite, identification, description, idEmissionFactor, idUnit, frequencyQuantity,frequencyUnit, alertBeforeDays, alertAfterDays, alertOnStart, credential)
        {
            _InitialDate = initialDate;
            _InitialReading = initialReading;
        }

        #region Private Fields

        DateTime _InitialDate;
        Double _InitialReading;

        #endregion

        #region public Properties

        
        public DateTime InitialDate
        {
            get
            {
                return _InitialDate;

            }
        }
        public Double InitialReading
        {
            get
            {
                return _InitialReading;

            }
        }
        public Double LastReading
        {
            get
            {
                if (HasValue())
                    return new Handlers.WaterMeters().LastReading(IdMeter);
                return _InitialReading;

            }
        }

        #endregion

    }
}
