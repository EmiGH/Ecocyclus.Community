using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Handlers
{
    internal class SiteExceptionMeters
    {
        internal SiteExceptionMeters()
        { }

        #region Read Functions

        internal Dictionary<Int64, Library.Objects.Sites.Exceptions.ExceptionMeter> Items(Int64 idSite, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Sites.Exceptions.ExceptionMeter> _oItems = new Dictionary<Int64, Library.Objects.Sites.Exceptions.ExceptionMeter>();
            Storage.SiteExceptionMeters _dbExceptions = new Storage.SiteExceptionMeters();
            Library.Objects.Sites.Exceptions.ExceptionMeter _exception = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbExceptions.ReadAllByElectricity(idSite);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _exception = new Library.Objects.Sites.Exceptions.ExceptionMeterElectricity(Convert.ToInt64(_dbRecord["IdSiteExceptionElectricityMeter"]), Convert.ToDateTime(_dbRecord["Date"]), Objects.Sites.Meters.fElectricityMeter.CreateMeter(Convert.ToInt64(_dbRecord["IdElectricityMeter"]), Convert.ToInt64(_dbRecord["IdSite"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToString(_dbRecord["Description"]), Convert.ToDateTime(Common.CastNullValues(_dbRecord["InitialDate"], DateTime.MinValue)), Convert.ToDouble(Common.CastNullValues(_dbRecord["InitialReading"], -1)), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdEmissionFactor"], 0)), Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToBoolean(_dbRecord["IsPhysicalMeter"]), Convert.ToInt16(_dbRecord["FrequencyQuantity"]), Convert.ToInt16(_dbRecord["FrequencyUnit"]), Convert.ToInt16(_dbRecord["AlertBeforeDays"]), Convert.ToInt16(_dbRecord["AlertAfterDays"]), Convert.ToBoolean(_dbRecord["AlertOnStart"]), credential));
                _oItems.Add(_exception.IdException, _exception);
            }

            _record = _dbExceptions.ReadAllByFuel(idSite);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _exception = new Library.Objects.Sites.Exceptions.ExceptionMeterFuel(Convert.ToInt64(_dbRecord["IdSiteExceptionFuelMeter"]), Convert.ToDateTime(_dbRecord["Date"]), new Objects.Sites.Meters.FuelMeter(Convert.ToInt64(_dbRecord["IdFuelMeter"]), Convert.ToInt64(_dbRecord["IdSite"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(_dbRecord["IdDefaultUnit"]), credential));
                _oItems.Add(_exception.IdException, _exception);
            }

            _record = _dbExceptions.ReadAllByTransport(idSite);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _exception = new Library.Objects.Sites.Exceptions.ExceptionMeterTransport(Convert.ToInt64(_dbRecord["IdSiteExceptionTransportMeter"]), Convert.ToDateTime(_dbRecord["Date"]), new Objects.Sites.Meters.TransportMeter(Convert.ToInt64(_dbRecord["IdTranportMeter"]), Convert.ToInt64(_dbRecord["IdSite"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(_dbRecord["IdDefaultUnit"]), credential));
                _oItems.Add(_exception.IdException, _exception);
            }

            _record = _dbExceptions.ReadAllByWaste(idSite);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _exception = new Library.Objects.Sites.Exceptions.ExceptionMeterWaste(Convert.ToInt64(_dbRecord["IdSiteExceptionWasteMeter"]), Convert.ToDateTime(_dbRecord["Date"]), new Objects.Sites.Meters.WasteMeter(Convert.ToInt64(_dbRecord["IdWasteMeter"]), Convert.ToInt64(_dbRecord["IdSite"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(_dbRecord["IdDefaultUnit"]), credential));
                _oItems.Add(_exception.IdException, _exception);
            }

            _record = _dbExceptions.ReadAllByWater(idSite);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _exception = new Library.Objects.Sites.Exceptions.ExceptionMeterWater(Convert.ToInt64(_dbRecord["IdSiteExceptionWaterMeter"]), Convert.ToDateTime(_dbRecord["Date"]), Objects.Sites.Meters.fWaterMeter.CreateMeter(Convert.ToInt64(_dbRecord["IdWaterMeter"]), Convert.ToInt64(_dbRecord["IdSite"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToString(_dbRecord["Description"]), Convert.ToDateTime(Common.CastNullValues(_dbRecord["InitialDate"], DateTime.MinValue)), Convert.ToDouble(Common.CastNullValues(_dbRecord["InitialReading"], -1)), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdEmissionFactor"], 0)), Convert.ToInt16(_dbRecord["IdUnit"]), Convert.ToBoolean(_dbRecord["IsPhysicalMeter"]), Convert.ToInt16(_dbRecord["FrequencyQuantity"]), Convert.ToInt16(_dbRecord["FrequencyUnit"]), Convert.ToInt16(_dbRecord["AlertBeforeDays"]), Convert.ToInt16(_dbRecord["AlertAfterDays"]), Convert.ToBoolean(_dbRecord["AlertOnStart"]), credential));
                _oItems.Add(_exception.IdException, _exception);
            }

            return _oItems;
        }
        internal Dictionary<Int64, Library.Objects.Sites.Exceptions.ExceptionMeter> ItemsUnresolved(Int64 idSite, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Sites.Exceptions.ExceptionMeter> _oItems = new Dictionary<Int64, Library.Objects.Sites.Exceptions.ExceptionMeter>();
            Storage.SiteExceptionMeters _dbExceptions = new Storage.SiteExceptionMeters();
            Library.Objects.Sites.Exceptions.ExceptionMeter _exception = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbExceptions.ReadAllUnresolvedByElectricity(idSite);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _exception = new Library.Objects.Sites.Exceptions.ExceptionMeterElectricity(Convert.ToInt64(_dbRecord["IdSiteExceptionElectricityMeter"]), Convert.ToDateTime(_dbRecord["Date"]), Objects.Sites.Meters.fElectricityMeter.CreateMeter(Convert.ToInt64(_dbRecord["IdElectricityMeter"]), Convert.ToInt64(_dbRecord["IdSite"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToString(_dbRecord["Description"]), Convert.ToDateTime(Common.CastNullValues(_dbRecord["InitialDate"], DateTime.MinValue)), Convert.ToDouble(Common.CastNullValues(_dbRecord["InitialReading"], -1)), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdEmissionFactor"], 0)), Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToBoolean(_dbRecord["IsPhysicalMeter"]), Convert.ToInt16(_dbRecord["FrequencyQuantity"]), Convert.ToInt16(_dbRecord["FrequencyUnit"]), Convert.ToInt16(_dbRecord["AlertBeforeDays"]), Convert.ToInt16(_dbRecord["AlertAfterDays"]), Convert.ToBoolean(_dbRecord["AlertOnStart"]), credential));
                _oItems.Add(_exception.IdException, _exception);
            }

            _record = _dbExceptions.ReadAllUnresolvedByFuel(idSite);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _exception = new Library.Objects.Sites.Exceptions.ExceptionMeterFuel(Convert.ToInt64(_dbRecord["IdSiteExceptionFuelMeter"]), Convert.ToDateTime(_dbRecord["Date"]), new Objects.Sites.Meters.FuelMeter(Convert.ToInt64(_dbRecord["IdFuelMeter"]), Convert.ToInt64(_dbRecord["IdSite"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(_dbRecord["IdDefaultUnit"]), credential));
                _oItems.Add(_exception.IdException, _exception);
            }

            _record = _dbExceptions.ReadAllUnresolvedByTransport(idSite);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _exception = new Library.Objects.Sites.Exceptions.ExceptionMeterTransport(Convert.ToInt64(_dbRecord["IdSiteExceptionTransportMeter"]), Convert.ToDateTime(_dbRecord["Date"]), new Objects.Sites.Meters.TransportMeter(Convert.ToInt64(_dbRecord["IdTranportMeter"]), Convert.ToInt64(_dbRecord["IdSite"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(_dbRecord["IdDefaultUnit"]), credential));
                _oItems.Add(_exception.IdException, _exception);
            }

            _record = _dbExceptions.ReadAllUnresolvedByWaste(idSite);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _exception = new Library.Objects.Sites.Exceptions.ExceptionMeterWaste(Convert.ToInt64(_dbRecord["IdSiteExceptionWasteMeter"]), Convert.ToDateTime(_dbRecord["Date"]), new Objects.Sites.Meters.WasteMeter(Convert.ToInt64(_dbRecord["IdWasteMeter"]), Convert.ToInt64(_dbRecord["IdSite"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(_dbRecord["IdDefaultUnit"]), credential));
                _oItems.Add(_exception.IdException, _exception);
            }

            _record = _dbExceptions.ReadAllUnresolvedByWater(idSite);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _exception = new Library.Objects.Sites.Exceptions.ExceptionMeterWater(Convert.ToInt64(_dbRecord["IdSiteExceptionWaterMeter"]), Convert.ToDateTime(_dbRecord["Date"]), Objects.Sites.Meters.fWaterMeter.CreateMeter(Convert.ToInt64(_dbRecord["IdWaterMeter"]), Convert.ToInt64(_dbRecord["IdSite"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToString(_dbRecord["Description"]), Convert.ToDateTime(Common.CastNullValues(_dbRecord["InitialDate"], DateTime.MinValue)), Convert.ToDouble(Common.CastNullValues(_dbRecord["InitialReading"], -1)), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdEmissionFactor"], 0)), Convert.ToInt16(_dbRecord["IdUnit"]), Convert.ToBoolean(_dbRecord["IsPhysicalMeter"]), Convert.ToInt16(_dbRecord["FrequencyQuantity"]), Convert.ToInt16(_dbRecord["FrequencyUnit"]), Convert.ToInt16(_dbRecord["AlertBeforeDays"]), Convert.ToInt16(_dbRecord["AlertAfterDays"]), Convert.ToBoolean(_dbRecord["AlertOnStart"]), credential));
                _oItems.Add(_exception.IdException, _exception);
            }

            return _oItems;
        }

        internal Library.Objects.Sites.Exceptions.ExceptionMeterElectricity ItemByElectricity(Int64 idException, Security.Credential credential)
        {
            Storage.SiteExceptionMeters _dbExceptions = new Storage.SiteExceptionMeters();
            Library.Objects.Sites.Exceptions.ExceptionMeterElectricity _exception = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbExceptions.ReadByIdByElectricity(idException);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _exception = new Library.Objects.Sites.Exceptions.ExceptionMeterElectricity(Convert.ToInt64(_dbRecord["IdSiteExceptionElectricityMeter"]), Convert.ToDateTime(_dbRecord["Date"]), Objects.Sites.Meters.fElectricityMeter.CreateMeter(Convert.ToInt64(_dbRecord["IdElectricityMeter"]), Convert.ToInt64(_dbRecord["IdSite"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToString(_dbRecord["Description"]), Convert.ToDateTime(Common.CastNullValues(_dbRecord["InitialDate"], DateTime.MinValue)), Convert.ToDouble(Common.CastNullValues(_dbRecord["InitialReading"], -1)), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdEmissionFactor"], 0)), Convert.ToInt16(_dbRecord["IdUnit"]), Convert.ToBoolean(_dbRecord["IsPhysicalMeter"]), Convert.ToInt16(_dbRecord["FrequencyQuantity"]), Convert.ToInt16(_dbRecord["FrequencyUnit"]), Convert.ToInt16(_dbRecord["AlertBeforeDays"]), Convert.ToInt16(_dbRecord["AlertAfterDays"]), Convert.ToBoolean(_dbRecord["AlertOnStart"]), credential));
            }
            return _exception;
        }
        internal Dictionary<Int64, Library.Objects.Sites.Exceptions.ExceptionMeterElectricity> ItemsByElectricity(Int64 idSite, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Sites.Exceptions.ExceptionMeterElectricity> _oItems = new Dictionary<Int64, Library.Objects.Sites.Exceptions.ExceptionMeterElectricity>();
            Storage.SiteExceptionMeters _dbExceptions = new Storage.SiteExceptionMeters();
            Library.Objects.Sites.Exceptions.ExceptionMeterElectricity _exception = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbExceptions.ReadAllByElectricity(idSite);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _exception = new Library.Objects.Sites.Exceptions.ExceptionMeterElectricity(Convert.ToInt64(_dbRecord["IdSiteExceptionElectricityMeter"]), Convert.ToDateTime(_dbRecord["Date"]), Objects.Sites.Meters.fElectricityMeter.CreateMeter(Convert.ToInt64(_dbRecord["IdElectricityMeter"]), Convert.ToInt64(_dbRecord["IdSite"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToString(_dbRecord["Description"]), Convert.ToDateTime(Common.CastNullValues(_dbRecord["InitialDate"], DateTime.MinValue)), Convert.ToDouble(Common.CastNullValues(_dbRecord["InitialReading"], -1)), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdEmissionFactor"], 0)), Convert.ToInt16(_dbRecord["IdUnit"]), Convert.ToBoolean(_dbRecord["IsPhysicalMeter"]), Convert.ToInt16(_dbRecord["FrequencyQuantity"]), Convert.ToInt16(_dbRecord["FrequencyUnit"]), Convert.ToInt16(_dbRecord["AlertBeforeDays"]), Convert.ToInt16(_dbRecord["AlertAfterDays"]), Convert.ToBoolean(_dbRecord["AlertOnStart"]), credential));

                _oItems.Add(_exception.IdException, _exception);
            }
            return _oItems;
        }
        internal Dictionary<Int64, Library.Objects.Sites.Exceptions.ExceptionMeterElectricity> ItemsByElectricityMeter(Int64 idMeter, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Sites.Exceptions.ExceptionMeterElectricity> _oItems = new Dictionary<Int64, Library.Objects.Sites.Exceptions.ExceptionMeterElectricity>();
            Storage.SiteExceptionMeters _dbExceptions = new Storage.SiteExceptionMeters();
            Library.Objects.Sites.Exceptions.ExceptionMeterElectricity _exception = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbExceptions.ReadAllByElectricityMeter(idMeter);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _exception = new Library.Objects.Sites.Exceptions.ExceptionMeterElectricity(Convert.ToInt64(_dbRecord["IdSiteExceptionElectricityMeter"]), Convert.ToDateTime(_dbRecord["Date"]), Objects.Sites.Meters.fElectricityMeter.CreateMeter(Convert.ToInt64(_dbRecord["IdElectricityMeter"]), Convert.ToInt64(_dbRecord["IdSite"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToString(_dbRecord["Description"]), Convert.ToDateTime(Common.CastNullValues(_dbRecord["InitialDate"], DateTime.MinValue)), Convert.ToDouble(Common.CastNullValues(_dbRecord["InitialReading"], -1)), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdEmissionFactor"], 0)), Convert.ToInt16(_dbRecord["IdUnit"]), Convert.ToBoolean(_dbRecord["IsPhysicalMeter"]), Convert.ToInt16(_dbRecord["FrequencyQuantity"]), Convert.ToInt16(_dbRecord["FrequencyUnit"]), Convert.ToInt16(_dbRecord["AlertBeforeDays"]), Convert.ToInt16(_dbRecord["AlertAfterDays"]), Convert.ToBoolean(_dbRecord["AlertOnStart"]), credential));

                _oItems.Add(_exception.IdException, _exception);
            }
            return _oItems;
        }

        internal Library.Objects.Sites.Exceptions.ExceptionMeterFuel ItemByFuel(Int64 idException, Security.Credential credential)
        {
            Storage.SiteExceptionMeters _dbExceptions = new Storage.SiteExceptionMeters();
            Library.Objects.Sites.Exceptions.ExceptionMeterFuel _exception = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbExceptions.ReadByIdByFuel(idException);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _exception = new Library.Objects.Sites.Exceptions.ExceptionMeterFuel(Convert.ToInt64(_dbRecord["IdSiteExceptionFuelMeter"]), Convert.ToDateTime(_dbRecord["Date"]), new Objects.Sites.Meters.FuelMeter(Convert.ToInt64(_dbRecord["IdFuelMeter"]), Convert.ToInt64(_dbRecord["IdSite"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(_dbRecord["IdDefaultUnit"]), credential));
            }
            return _exception;
        }
        internal Dictionary<Int64, Library.Objects.Sites.Exceptions.ExceptionMeterFuel> ItemsByFuel(Int64 idSite, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Sites.Exceptions.ExceptionMeterFuel> _oItems = new Dictionary<Int64, Library.Objects.Sites.Exceptions.ExceptionMeterFuel>();
            Storage.SiteExceptionMeters _dbExceptions = new Storage.SiteExceptionMeters();
            Library.Objects.Sites.Exceptions.ExceptionMeterFuel _exception = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbExceptions.ReadAllByFuel(idSite);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _exception = new Library.Objects.Sites.Exceptions.ExceptionMeterFuel(Convert.ToInt64(_dbRecord["IdSiteExceptionFuelMeter"]), Convert.ToDateTime(_dbRecord["Date"]), new Objects.Sites.Meters.FuelMeter(Convert.ToInt64(_dbRecord["IdFuelMeter"]), Convert.ToInt64(_dbRecord["IdSite"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(_dbRecord["IdDefaultUnit"]), credential));

                _oItems.Add(_exception.IdException, _exception);
            }
            return _oItems;
        }
        internal Dictionary<Int64, Library.Objects.Sites.Exceptions.ExceptionMeterFuel> ItemsByFuelMeter(Int64 idMeter, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Sites.Exceptions.ExceptionMeterFuel> _oItems = new Dictionary<Int64, Library.Objects.Sites.Exceptions.ExceptionMeterFuel>();
            Storage.SiteExceptionMeters _dbExceptions = new Storage.SiteExceptionMeters();
            Library.Objects.Sites.Exceptions.ExceptionMeterFuel _exception = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbExceptions.ReadAllByFuelMeter(idMeter);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _exception = new Library.Objects.Sites.Exceptions.ExceptionMeterFuel(Convert.ToInt64(_dbRecord["IdSiteExceptionFuelMeter"]), Convert.ToDateTime(_dbRecord["Date"]), new Objects.Sites.Meters.FuelMeter(Convert.ToInt64(_dbRecord["IdFuelMeter"]), Convert.ToInt64(_dbRecord["IdSite"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(_dbRecord["IdDefaultUnit"]), credential));

                _oItems.Add(_exception.IdException, _exception);
            }
            return _oItems;
        }

        internal Library.Objects.Sites.Exceptions.ExceptionMeterTransport ItemByTransport(Int64 idException, Security.Credential credential)
        {
            Storage.SiteExceptionMeters _dbExceptions = new Storage.SiteExceptionMeters();
            Library.Objects.Sites.Exceptions.ExceptionMeterTransport _exception = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbExceptions.ReadByIdByTransport(idException);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _exception = new Library.Objects.Sites.Exceptions.ExceptionMeterTransport(Convert.ToInt64(_dbRecord["IdSiteExceptionTransportMeter"]), Convert.ToDateTime(_dbRecord["Date"]), new Objects.Sites.Meters.TransportMeter(Convert.ToInt64(_dbRecord["IdTranportMeter"]), Convert.ToInt64(_dbRecord["IdSite"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(_dbRecord["IdDefaultUnit"]), credential));
            }
            return _exception;
        }
        internal Dictionary<Int64, Library.Objects.Sites.Exceptions.ExceptionMeterTransport> ItemsByTransport(Int64 idSite, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Sites.Exceptions.ExceptionMeterTransport> _oItems = new Dictionary<Int64, Library.Objects.Sites.Exceptions.ExceptionMeterTransport>();
            Storage.SiteExceptionMeters _dbExceptions = new Storage.SiteExceptionMeters();
            Library.Objects.Sites.Exceptions.ExceptionMeterTransport _exception = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbExceptions.ReadAllByTransport(idSite);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _exception = new Library.Objects.Sites.Exceptions.ExceptionMeterTransport(Convert.ToInt64(_dbRecord["IdSiteExceptionTransportMeter"]), Convert.ToDateTime(_dbRecord["Date"]), new Objects.Sites.Meters.TransportMeter(Convert.ToInt64(_dbRecord["IdTranportMeter"]), Convert.ToInt64(_dbRecord["IdSite"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(_dbRecord["IdDefaultUnit"]), credential));

                _oItems.Add(_exception.IdException, _exception);
            }
            return _oItems;
        }
        internal Dictionary<Int64, Library.Objects.Sites.Exceptions.ExceptionMeterTransport> ItemsByTransportMeter(Int64 idMeter, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Sites.Exceptions.ExceptionMeterTransport> _oItems = new Dictionary<Int64, Library.Objects.Sites.Exceptions.ExceptionMeterTransport>();
            Storage.SiteExceptionMeters _dbExceptions = new Storage.SiteExceptionMeters();
            Library.Objects.Sites.Exceptions.ExceptionMeterTransport _exception = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbExceptions.ReadAllByTransportMeter(idMeter);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _exception = new Library.Objects.Sites.Exceptions.ExceptionMeterTransport(Convert.ToInt64(_dbRecord["IdSiteExceptionTransportMeter"]), Convert.ToDateTime(_dbRecord["Date"]), new Objects.Sites.Meters.TransportMeter(Convert.ToInt64(_dbRecord["IdTranportMeter"]), Convert.ToInt64(_dbRecord["IdSite"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(_dbRecord["IdDefaultUnit"]), credential));

                _oItems.Add(_exception.IdException, _exception);
            }
            return _oItems;
        }

        internal Library.Objects.Sites.Exceptions.ExceptionMeterWaste ItemByWaste(Int64 idException, Security.Credential credential)
        {
            Storage.SiteExceptionMeters _dbExceptions = new Storage.SiteExceptionMeters();
            Library.Objects.Sites.Exceptions.ExceptionMeterWaste _exception = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbExceptions.ReadByIdByWaste(idException);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _exception = new Library.Objects.Sites.Exceptions.ExceptionMeterWaste(Convert.ToInt64(_dbRecord["IdSiteExceptionWasteMeter"]), Convert.ToDateTime(_dbRecord["Date"]), new Objects.Sites.Meters.WasteMeter(Convert.ToInt64(_dbRecord["IdWasteMeter"]), Convert.ToInt64(_dbRecord["IdSite"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(_dbRecord["IdDefaultUnit"]), credential));
            }
            return _exception;
        }
        internal Dictionary<Int64, Library.Objects.Sites.Exceptions.ExceptionMeterWaste> ItemsByWaste(Int64 idSite, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Sites.Exceptions.ExceptionMeterWaste> _oItems = new Dictionary<Int64, Library.Objects.Sites.Exceptions.ExceptionMeterWaste>();
            Storage.SiteExceptionMeters _dbExceptions = new Storage.SiteExceptionMeters();
            Library.Objects.Sites.Exceptions.ExceptionMeterWaste _exception = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbExceptions.ReadAllByWaste(idSite);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _exception = new Library.Objects.Sites.Exceptions.ExceptionMeterWaste(Convert.ToInt64(_dbRecord["IdSiteExceptionWasteMeter"]), Convert.ToDateTime(_dbRecord["Date"]), new Objects.Sites.Meters.WasteMeter(Convert.ToInt64(_dbRecord["IdWasteMeter"]), Convert.ToInt64(_dbRecord["IdSite"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(_dbRecord["IdDefaultUnit"]), credential));

                _oItems.Add(_exception.IdException, _exception);
            }
            return _oItems;
        }
        internal Dictionary<Int64, Library.Objects.Sites.Exceptions.ExceptionMeterWaste> ItemsByWasteMeter(Int64 idMeter, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Sites.Exceptions.ExceptionMeterWaste> _oItems = new Dictionary<Int64, Library.Objects.Sites.Exceptions.ExceptionMeterWaste>();
            Storage.SiteExceptionMeters _dbExceptions = new Storage.SiteExceptionMeters();
            Library.Objects.Sites.Exceptions.ExceptionMeterWaste _exception = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbExceptions.ReadAllByWasteMeter(idMeter);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _exception = new Library.Objects.Sites.Exceptions.ExceptionMeterWaste(Convert.ToInt64(_dbRecord["IdSiteExceptionWasteMeter"]), Convert.ToDateTime(_dbRecord["Date"]), new Objects.Sites.Meters.WasteMeter(Convert.ToInt64(_dbRecord["IdWasteMeter"]), Convert.ToInt64(_dbRecord["IdSite"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(_dbRecord["IdDefaultUnit"]), credential));

                _oItems.Add(_exception.IdException, _exception);
            }
            return _oItems;
        }

        internal Library.Objects.Sites.Exceptions.ExceptionMeterWater ItemByWater(Int64 idException, Security.Credential credential)
        {
            Storage.SiteExceptionMeters _dbExceptions = new Storage.SiteExceptionMeters();
            Library.Objects.Sites.Exceptions.ExceptionMeterWater _exception = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbExceptions.ReadByIdByWater(idException);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _exception = new Library.Objects.Sites.Exceptions.ExceptionMeterWater(Convert.ToInt64(_dbRecord["IdSiteExceptionWaterMeter"]), Convert.ToDateTime(_dbRecord["Date"]), Objects.Sites.Meters.fWaterMeter.CreateMeter(Convert.ToInt64(_dbRecord["IdWaterMeter"]), Convert.ToInt64(_dbRecord["IdSite"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToString(_dbRecord["Description"]), Convert.ToDateTime(Common.CastNullValues(_dbRecord["InitialDate"], DateTime.MinValue)), Convert.ToDouble(Common.CastNullValues(_dbRecord["InitialReading"], -1)), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdEmissionFactor"], 0)), Convert.ToInt16(_dbRecord["IdUnit"]), Convert.ToBoolean(_dbRecord["IsPhysicalMeter"]), Convert.ToInt16(_dbRecord["FrequencyQuantity"]), Convert.ToInt16(_dbRecord["FrequencyUnit"]), Convert.ToInt16(_dbRecord["AlertBeforeDays"]), Convert.ToInt16(_dbRecord["AlertAfterDays"]), Convert.ToBoolean(_dbRecord["AlertOnStart"]), credential));
            }
            return _exception;
        }
        internal Dictionary<Int64, Library.Objects.Sites.Exceptions.ExceptionMeterWater> ItemsByWater(Int64 idSite, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Sites.Exceptions.ExceptionMeterWater> _oItems = new Dictionary<Int64, Library.Objects.Sites.Exceptions.ExceptionMeterWater>();
            Storage.SiteExceptionMeters _dbExceptions = new Storage.SiteExceptionMeters();
            Library.Objects.Sites.Exceptions.ExceptionMeterWater _exception = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbExceptions.ReadAllByWater(idSite);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _exception = new Library.Objects.Sites.Exceptions.ExceptionMeterWater(Convert.ToInt64(_dbRecord["IdSiteExceptionWaterMeter"]), Convert.ToDateTime(_dbRecord["Date"]), Objects.Sites.Meters.fWaterMeter.CreateMeter(Convert.ToInt64(_dbRecord["IdWaterMeter"]), Convert.ToInt64(_dbRecord["IdSite"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToString(_dbRecord["Description"]), Convert.ToDateTime(Common.CastNullValues(_dbRecord["InitialDate"], DateTime.MinValue)), Convert.ToDouble(Common.CastNullValues(_dbRecord["InitialReading"], -1)), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdEmissionFactor"], 0)), Convert.ToInt16(_dbRecord["IdUnit"]), Convert.ToBoolean(_dbRecord["IsPhysicalMeter"]), Convert.ToInt16(_dbRecord["FrequencyQuantity"]), Convert.ToInt16(_dbRecord["FrequencyUnit"]), Convert.ToInt16(_dbRecord["AlertBeforeDays"]), Convert.ToInt16(_dbRecord["AlertAfterDays"]), Convert.ToBoolean(_dbRecord["AlertOnStart"]), credential));

                _oItems.Add(_exception.IdException, _exception);
            }
            return _oItems;
        }
        internal Dictionary<Int64, Library.Objects.Sites.Exceptions.ExceptionMeterWater> ItemsByWaterMeter(Int64 idMeter, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Sites.Exceptions.ExceptionMeterWater> _oItems = new Dictionary<Int64, Library.Objects.Sites.Exceptions.ExceptionMeterWater>();
            Storage.SiteExceptionMeters _dbExceptions = new Storage.SiteExceptionMeters();
            Library.Objects.Sites.Exceptions.ExceptionMeterWater _exception = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbExceptions.ReadAllByWaterMeter(idMeter);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _exception = new Library.Objects.Sites.Exceptions.ExceptionMeterWater(Convert.ToInt64(_dbRecord["IdSiteExceptionWaterMeter"]), Convert.ToDateTime(_dbRecord["Date"]), Objects.Sites.Meters.fWaterMeter.CreateMeter(Convert.ToInt64(_dbRecord["IdWaterMeter"]), Convert.ToInt64(_dbRecord["IdSite"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToString(_dbRecord["Description"]), Convert.ToDateTime(Common.CastNullValues(_dbRecord["InitialDate"], DateTime.MinValue)), Convert.ToDouble(Common.CastNullValues(_dbRecord["InitialReading"], -1)), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdEmissionFactor"], 0)), Convert.ToInt16(_dbRecord["IdUnit"]), Convert.ToBoolean(_dbRecord["IsPhysicalMeter"]), Convert.ToInt16(_dbRecord["FrequencyQuantity"]), Convert.ToInt16(_dbRecord["FrequencyUnit"]), Convert.ToInt16(_dbRecord["AlertBeforeDays"]), Convert.ToInt16(_dbRecord["AlertAfterDays"]), Convert.ToBoolean(_dbRecord["AlertOnStart"]), credential));

                _oItems.Add(_exception.IdException, _exception);
            }
            return _oItems;
        }

        #endregion

        #region Write Functions

        internal Library.Objects.Sites.Exceptions.ExceptionMeterElectricity AddForElectricity(Int64 idMeter, Security.Credential credential)
        {
            Storage.SiteExceptionMeters _dbExceptions = new Storage.SiteExceptionMeters();

            Int64 _idException = _dbExceptions.CreateForElectricityMeter(idMeter);
            return ItemByElectricity(_idException, credential);

        }
        internal void RemoveForElectricity(Int64 idException)
        {
            Storage.SiteExceptionMeters _dbExceptions = new Storage.SiteExceptionMeters();
            _dbExceptions.DeleteForElectricityMeter(idException);
        }

        internal Library.Objects.Sites.Exceptions.ExceptionMeterFuel AddForFuel(Int64 idMeter, Security.Credential credential)
        {
            Storage.SiteExceptionMeters _dbExceptions = new Storage.SiteExceptionMeters();

            Int64 _idException = _dbExceptions.CreateForFuelMeter(idMeter);
            return ItemByFuel(_idException, credential);

        }
        internal void RemoveForFuel(Int64 idException)
        {
            Storage.SiteExceptionMeters _dbExceptions = new Storage.SiteExceptionMeters();
            _dbExceptions.DeleteForFuelMeter(idException);
        }

        internal Library.Objects.Sites.Exceptions.ExceptionMeterTransport AddForTransport(Int64 idMeter, Security.Credential credential)
        {
            Storage.SiteExceptionMeters _dbExceptions = new Storage.SiteExceptionMeters();

            Int64 _idException = _dbExceptions.CreateForTransportMeter(idMeter);
            return ItemByTransport(_idException, credential);

        }
        internal void RemoveForTransport(Int64 idException)
        {
            Storage.SiteExceptionMeters _dbExceptions = new Storage.SiteExceptionMeters();
            _dbExceptions.DeleteForTransportMeter(idException);
        }

        internal Library.Objects.Sites.Exceptions.ExceptionMeterWaste AddForWaste(Int64 idMeter, Security.Credential credential)
        {
            Storage.SiteExceptionMeters _dbExceptions = new Storage.SiteExceptionMeters();

            Int64 _idException = _dbExceptions.CreateForWasteMeter(idMeter);
            return ItemByWaste(_idException, credential);

        }
        internal void RemoveForWaste(Int64 idException)
        {
            Storage.SiteExceptionMeters _dbExceptions = new Storage.SiteExceptionMeters();
            _dbExceptions.DeleteForWasteMeter(idException);
        }

        internal Library.Objects.Sites.Exceptions.ExceptionMeterWater AddForWater(Int64 idMeter, Security.Credential credential)
        {
            Storage.SiteExceptionMeters _dbExceptions = new Storage.SiteExceptionMeters();

            Int64 _idException = _dbExceptions.CreateForWaterMeter(idMeter);
            return ItemByWater(_idException, credential);

        }
        internal void RemoveForWater(Int64 idException)
        {
            Storage.SiteExceptionMeters _dbExceptions = new Storage.SiteExceptionMeters();
            _dbExceptions.DeleteForWaterMeter(idException);
        }

        #endregion

    }
}
