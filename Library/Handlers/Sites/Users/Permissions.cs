using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Types;
using System.Data.SqlClient;

namespace CSI.Library.Handlers
{
    internal class Permissions
    {
        internal Permissions() { }

        #region Read Functions

        internal Library.Objects.Sites.Permission Item(Int64 idSiteUser, Security.Credential credential)
        {
            Storage.Permissions _dbPermissions = new Storage.Permissions();
            Library.Objects.Sites.Permission _permission = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPermissions.ReadById(idSiteUser);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _permission = new Library.Objects.Sites.Permission(idSiteUser, Convert.ToInt64(_dbRecord["IdSite"]), Convert.ToInt64(_dbRecord["IdCompanyUser"]), Convert.ToDateTime(_dbRecord["Date"]), Convert.ToBoolean(_dbRecord["Manage"]), Convert.ToBoolean(_dbRecord["Load"]), credential);
            }
            return _permission;
        }
        internal Dictionary<Int64, Library.Objects.Sites.Permission> ItemsBySite(Int64 idSite, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Sites.Permission> _oItems = new Dictionary<Int64, Library.Objects.Sites.Permission>();
            Storage.Permissions _dbPermissions = new Storage.Permissions();
            Library.Objects.Sites.Permission _permission = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPermissions.ReadAllBySite(idSite);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _permission = new Library.Objects.Sites.Permission(Convert.ToInt64(_dbRecord["IdSiteUser"]), idSite, Convert.ToInt64(_dbRecord["IdCompanyUser"]), Convert.ToDateTime(_dbRecord["Date"]), Convert.ToBoolean(_dbRecord["Manage"]), Convert.ToBoolean(_dbRecord["Load"]), credential); 
                _oItems.Add(_permission.IdPermission, _permission);
            }
            return _oItems;
        }
        internal Dictionary<Int64, Library.Objects.Sites.Permission> ItemsByOperator(Int64 idOperator, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Sites.Permission> _oItems = new Dictionary<Int64, Library.Objects.Sites.Permission>();
            Storage.Permissions _dbPermissions = new Storage.Permissions();
            Library.Objects.Sites.Permission _permission = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPermissions.ReadAllByUser(idOperator);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _permission = new Library.Objects.Sites.Permission(Convert.ToInt64(_dbRecord["IdSiteUser"]), Convert.ToInt64(_dbRecord["IdSite"]), Convert.ToInt64(_dbRecord["IdCompanyUser"]), Convert.ToDateTime(_dbRecord["Date"]), Convert.ToBoolean(_dbRecord["Manage"]), Convert.ToBoolean(_dbRecord["Load"]), credential);
                _oItems.Add(_permission.IdPermission, _permission);
            }
            return _oItems;
        }
        internal Dictionary<Int64, Library.Objects.Users.UserOperatorCoworker> UsersNotGranted(Int64 idSite, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Users.UserOperatorCoworker> _oItems = new Dictionary<Int64, Library.Objects.Users.UserOperatorCoworker>();
            Storage.Permissions _dbPermissions = new Storage.Permissions();
            Library.Objects.Users.UserOperatorCoworker _user = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPermissions.ReadAllNotInSite(idSite);

            Int64 _idOperator = ((Objects.Users.UserOperator)credential.CurrentUser).IdOperator;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (Convert.ToInt64(_dbRecord["IdCompanyUser"]) != _idOperator)
                {
                    _user = (Objects.Users.UserOperatorCoworker)Objects.Users.fUserOperator.CreateOperatorOther(Convert.ToInt64(_dbRecord["IdCompanyUser"]), Convert.ToInt64(_dbRecord["IdUser"]), Convert.ToInt64(_dbRecord["IdCompany"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Firstname"]), Convert.ToString(_dbRecord["Lastname"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdPicture"], 0)), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToBoolean(_dbRecord["IsManager"]), Convert.ToBoolean(_dbRecord["IsActive"]), credential);
                    _oItems.Add(_user.IdOperator, _user);
                }
            }
            return _oItems;
        }
        internal Dictionary<Int64, Library.Objects.Sites.SiteMine> SitesNotGranted(Int64 idUser, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Sites.SiteMine> _sites = new Dictionary<Int64, Library.Objects.Sites.SiteMine>();
            Storage.Permissions _dbPermissions = new Storage.Permissions();
            Library.Objects.Sites.SiteMine _site = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;
            
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPermissions.ReadAllNotForUser(idUser, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_sites.ContainsKey(Convert.ToInt64(_dbRecord["IdSite"])))
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                    {
                        _sites.Remove(Convert.ToInt64(_dbRecord["IdSite"]));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                {
                    _site = (Objects.Sites.SiteMine)Objects.Sites.fSite.CreateSite(Convert.ToInt64(_dbRecord["IdCompany"]), Convert.ToInt64(_dbRecord["IdSite"]), new Objects.Auxiliaries.Types.SiteType(Convert.ToInt64(_dbRecord["idSiteType"]), Convert.ToString(_dbRecord["idLanguage"]), Convert.ToString(_dbRecord["TypeName"]), Convert.ToString(_dbRecord["TypeDescription"])), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToDateTime(_dbRecord["Start"]), Convert.ToInt32(_dbRecord["Weeks"]), new Objects.Auxiliaries.Units.TimeRange(Convert.ToDateTime(_dbRecord["ValidLoadFrom"]), Convert.ToDateTime(_dbRecord["ValidLoadTo"])), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Number"]), new Objects.Auxiliaries.Geographic.Contact(new Objects.Auxiliaries.Geographic.Location(Convert.ToString(_dbRecord["Location"]), new Objects.Auxiliaries.Geographic.Position(SqlGeography.Parse(Convert.ToString(_dbRecord["Position"])))), Convert.ToString(_dbRecord["Telephone"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Website"]), Convert.ToString(_dbRecord["Facebook"]), Convert.ToString(_dbRecord["Twitter"])), Convert.ToInt64(_dbRecord["IdCountry"]), Convert.ToDouble(_dbRecord["Value"]), Convert.ToInt64(_dbRecord["IdCurrency"]), Convert.ToDouble(_dbRecord["FloorSpace"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["Units"], 0)), Convert.ToString(_dbRecord["Client"]), Convert.ToString(_dbRecord["Agent"]), Convert.ToString(_dbRecord["Contractor"]), Convert.ToString(_dbRecord["Responsible"]), Convert.ToString(_dbRecord["Manager"]), Convert.ToString(_dbRecord["Description"]), Convert.ToBoolean(_dbRecord["isPublic"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdImage"], 0)), Convert.ToBoolean(_dbRecord["LoadOverdue"]), Convert.ToBoolean(_dbRecord["TargetSurpassed"]), Convert.ToBoolean(_dbRecord["IsClosed"]), credential);
                    _sites.Add(_site.IdSite, _site);
                }
                _insert = true;

            }
            return _sites;
        }
        internal Dictionary<Int64, Library.Objects.Users.UserOperatorCoworker> ItemsManagersBySite(Int64 idSite, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Users.UserOperatorCoworker> _oItems = new Dictionary<Int64, Library.Objects.Users.UserOperatorCoworker>();
            Storage.Permissions _dbPermissions = new Storage.Permissions();
            Library.Objects.Users.UserOperatorCoworker _user = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPermissions.ReadManagersBySite(idSite);

            Int64 _idOperator = ((Objects.Users.UserOperator)credential.CurrentUser).IdOperator;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (Convert.ToInt64(_dbRecord["IdCompanyUser"]) == _idOperator)
                {
                    _user = (Objects.Users.UserOperatorMe)Objects.Users.fUserOperator.CreateOperatorMe(Convert.ToInt64(_dbRecord["IdCompanyUser"]), Convert.ToInt64(_dbRecord["IdUser"]), Convert.ToInt64(_dbRecord["IdCompany"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Firstname"]), Convert.ToString(_dbRecord["Lastname"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdPicture"], 0)), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToBoolean(_dbRecord["IsManager"]), Convert.ToBoolean(_dbRecord["IsActive"]), credential);
                    _oItems.Add(_user.IdOperator, _user);
                }
                else
                {
                    _user = (Objects.Users.UserOperatorCoworker)Objects.Users.fUserOperator.CreateOperatorOther(Convert.ToInt64(_dbRecord["IdCompanyUser"]), Convert.ToInt64(_dbRecord["IdUser"]), Convert.ToInt64(_dbRecord["IdCompany"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Firstname"]), Convert.ToString(_dbRecord["Lastname"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdPicture"], 0)), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToBoolean(_dbRecord["IsManager"]), Convert.ToBoolean(_dbRecord["IsActive"]), credential);
                    _oItems.Add(_user.IdOperator, _user);
                }

            }
            return _oItems;
        }
        

        internal Boolean HasPermission(Int64 idSite, Int64 idOperator, Security.Authority.PermissionTypes permission)
        {
            Storage.Permissions _dbPermissions = new Storage.Permissions();
            switch (permission)
            {
                case CSI.Library.Security.Authority.PermissionTypes.SiteReader:
                    return _dbPermissions.HasPermissionForRead(idSite, idOperator);
                case CSI.Library.Security.Authority.PermissionTypes.SiteOperator:
                    return _dbPermissions.HasPermissionForLoad(idSite, idOperator);
                case CSI.Library.Security.Authority.PermissionTypes.SiteManager:
                    return _dbPermissions.HasPermissionForManage(idSite, idOperator);
                default:
                    return false;
            }
            
        }

        #endregion

        #region Write Functions

        internal Library.Objects.Sites.Permission Add(Int64 idSite, Int64 idOperator, Boolean manage, Boolean load, Security.Credential credential)
        {
            Storage.Permissions _dbPermissions = new Storage.Permissions();

            try
            {
                Int64 _idFile = _dbPermissions.Create(idSite, idOperator, manage, load);
                return Item(_idFile, credential);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 2601 || sqlex.Number == 2627)
                    throw new ApplicationException(Resources.Messages.DuplicatedRecord);
                else
                    throw sqlex;
            }
        }
        internal void AddBySite(Int64 idSite, DataTable permissions)
        {
            new Storage.Permissions().CreateBySite(idSite, permissions);
        }
        internal void AddByOperator(Int64 idOperator, DataTable permissions)
        {
            new Storage.Permissions().CreateByUser(idOperator, permissions);
        }
        internal void Remove(Int64 idSiteUser)
        {
            Storage.Permissions _dbPermissions = new Storage.Permissions();

            try
            {
                _dbPermissions.Delete(idSiteUser);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 547)
                    throw new ApplicationException(Resources.Messages.ErrorCannotDeleteExistingRelationship);
                else
                    throw sqlex;
            }
        }

        #endregion

        
    }
}
