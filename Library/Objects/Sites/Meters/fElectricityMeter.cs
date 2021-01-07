using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Sites.Meters
{
    internal class fElectricityMeter
    {
        internal fElectricityMeter() { }

        internal static ElectricityMeter CreateMeter(Int64 idMeter, Int64 idSite, String identification, String description, DateTime initialDate, Double initialReading, Int64 idEmissionFactor, Int64 idUnit, Boolean isPhysical, Int16 frequencyQuantity, Int16 frequencyUnit, Int16 alertBeforeDays, Int16 alertAfterDays, Boolean alertOnStart, Security.Credential credential)
        {
            if (isPhysical)
                return CreateMeterPhysical(idMeter, idSite, identification, description, initialDate, initialReading, idEmissionFactor, idUnit, frequencyQuantity, frequencyUnit, alertBeforeDays, alertAfterDays, alertOnStart, credential);
            return CreateMeterNonPhysical(idMeter, idSite, identification, description, idEmissionFactor, idUnit, frequencyQuantity, frequencyUnit, alertBeforeDays, alertAfterDays, alertOnStart, credential);
        }

        private static ElectricityMeter CreateMeterNonPhysical(Int64 idMeter, Int64 idSite, String identification, String description, Int64 idEmissionFactor, Int64 idUnit, Int16 frequencyQuantity, Int16 frequencyUnit, Int16 alertBeforeDays, Int16 alertAfterDays, Boolean alertOnStart, Security.Credential credential)
        {
            return new ElectricityMeter(idMeter, idSite, identification, description, idEmissionFactor, idUnit, frequencyQuantity, frequencyUnit, alertBeforeDays, alertAfterDays, alertOnStart, credential);
         }
        private static ElectricityMeterPhysical CreateMeterPhysical(Int64 idMeter, Int64 idSite, String identification, String description, DateTime initialDate, Double initialReading, Int64 idEmissionFactor, Int64 idUnit, Int16 frequencyQuantity, Int16 frequencyUnit, Int16 alertBeforeDays, Int16 alertAfterDays, Boolean alertOnStart, Security.Credential credential)
        {
            return new ElectricityMeterPhysical(idMeter, idSite, identification, description, initialDate, initialReading, idEmissionFactor, idUnit, frequencyQuantity, frequencyUnit, alertBeforeDays, alertAfterDays, alertOnStart, credential);
        }

    }
}
