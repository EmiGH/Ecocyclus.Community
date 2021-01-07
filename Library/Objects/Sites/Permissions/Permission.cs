using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace CSI.Library.Objects.Sites
{
    public class Permission
    {
        internal Permission(Int64 idPermission, Int64 idSite, Int64 idUser, DateTime date, Boolean manage, Boolean load, Security.Credential credential)
        {
            _Credential = credential;

            _IdPermission = idPermission;
            _IdSite = idSite;
            _IdUser = idUser;

            _Date = date;

            _Manage = manage;
            _Load = load;
        }
        
        #region Private Fields

        private Security.Credential _Credential;

        private Int64 _IdPermission;
        private Int64 _IdSite;
        private Int64 _IdUser;
        private DateTime _Date;
        private Boolean _Manage;
        private Boolean _Load;

        #endregion

        #region Public Properties

        public Int64 IdPermission
        { get { return _IdPermission; } }
        public Users.UserOperator Operator
        { get { return new Handlers.Operators().Item(_IdUser, _Credential); } }
        public Sites.Site Site
        { get { return new Handlers.Sites().ItemByOperator(_IdSite, _Credential); } }
        public Boolean Manage
        { get { return _Manage; } }
        public DateTime Date
        { get { return _Date; } }
        public Boolean Load
        { get { return _Load; } }

        #endregion

        #region Public Methods

        public static PermissionsStructure GetStructure()
        {
            return new PermissionsStructure();
        }

        #endregion

        #region Permission Structure

        public class PermissionsStructure
        {
            private List<PermissionStructure> _permissions;

            internal protected PermissionsStructure()
            {
                _permissions = new List<PermissionStructure>();
            }

            public void AddPermission(Int64 idItem, Boolean manage, Boolean load)
            {
                Boolean _found = false;
                for (int i = 0; i < _permissions.Count; i++)
                {
                    if (_permissions[i].IdItem == idItem)
                    {
                        _found = true;
                        break;
                    }
                }
                if (!_found)
                    _permissions.Add(new PermissionStructure(idItem, manage, load));
            }
            public void RemovePermission(Int64 idItem)
            {
                for (int i = 0; i < _permissions.Count; i++)
                    if (_permissions[i].IdItem == idItem)
                    {
                        _permissions.RemoveAt(i);
                        break;
                    }
            }
            public List<PermissionStructure> Permissions
            { get { return _permissions; } }

            internal protected DataTable ToDataTable()
            {
                DataTable _dt = new DataTable("Permissions");
                _dt.Columns.Add(new DataColumn("IdItem", typeof(Int64)));
                _dt.Columns.Add(new DataColumn("Manage", typeof(Boolean)));
                _dt.Columns.Add(new DataColumn("Load", typeof(Boolean)));

                foreach (PermissionStructure _item in _permissions)
                {
                    _dt.Rows.Add(_item.ToDataRow(ref _dt));
                }
                return _dt;
            }

            public class PermissionStructure
            {
                private Int64 _IdItem;
                private Boolean _Manage;
                private Boolean _Load;

                protected internal PermissionStructure(Int64 idItem, Boolean manage, Boolean load)
                {
                    _IdItem = idItem;
                    _Manage = manage;
                    _Load = load;
                }

                protected internal Int64 IdItem
                { get { return _IdItem; } }
                internal protected DataRow ToDataRow(ref DataTable dt)
                {
                    DataRow _dr = dt.NewRow();
                    _dr["Iditem"] = _IdItem;
                    _dr["Manage"] = _Manage;
                    _dr["Load"] = _Load;

                    return _dr;
                }
            }
        }

        #endregion
    }
}
