using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Net.Mail;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Types;
using System.Data.SqlClient;

namespace CSI.Library.Handlers
{
    internal class Notifications
    {
        internal Notifications()
        {
        }

        #region Read Methods

        internal Library.Objects.Notifications.Configuration Configuration()
        {
            Storage.Notifications _dbNotifications = new Storage.Notifications();
            Library.Objects.Notifications.Configuration _config = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbNotifications.Configuration();
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _config = new Library.Objects.Notifications.Configuration(Convert.ToString(_dbRecord["MailSender"]), Convert.ToString(_dbRecord["MailReceiver"]), Convert.ToString(_dbRecord["MailHost"]), Convert.ToInt32(Common.CastNullValues(_dbRecord["MailPort"], 0)), Convert.ToString(_dbRecord["MailUsername"]), Convert.ToString(_dbRecord["MailPassword"]));
            }
            return _config;
        }

        internal Library.Objects.Notifications.Notification Item(Int64 idNotification, Security.Credential credential)
        {
            Storage.Notifications _dbNotifications = new Storage.Notifications();
            Library.Objects.Notifications.Notification _notification = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbNotifications.ReadById(idNotification);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _notification = new Library.Objects.Notifications.Notification(idNotification, Convert.ToDateTime(_dbRecord["Date"]), new Objects.Users.User(Convert.ToInt64(_dbRecord["IdUser"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Firstname"]), Convert.ToString(_dbRecord["Lastname"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdPicture"], 0)), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToBoolean(_dbRecord["IsActive"]), credential), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Message"]));
            }
            return _notification;
        }
        internal Dictionary<Int64, Library.Objects.Notifications.Notification> Items(Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Notifications.Notification> _oItems = new Dictionary<Int64, Library.Objects.Notifications.Notification>();
            Storage.Notifications _dbNotifications= new Storage.Notifications();
            Library.Objects.Notifications.Notification _notification = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbNotifications.ReadAll();

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _notification = new Library.Objects.Notifications.Notification(Convert.ToInt64(_dbRecord["IdNotification"]), Convert.ToDateTime(_dbRecord["Date"]), new Objects.Users.User(Convert.ToInt64(_dbRecord["IdUser"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Firstname"]), Convert.ToString(_dbRecord["Lastname"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdPicture"], 0)), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToBoolean(_dbRecord["IsActive"]), credential), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Message"]));

                _oItems.Add(_notification.IdNotification, _notification);
            }
            return _oItems;
        }
        
        #endregion

        #region Write Functions

        internal Library.Objects.Notifications.Notification Add(Int64 idUser, String title, String message, Security.Credential credential)
        {
            Storage.Notifications _dbNotifications = new Storage.Notifications();

            Int64 _idNotification = _dbNotifications.Create(idUser, title, message);
            return Item(_idNotification, credential);

        }
        internal void Remove(DataTable ids)
        {
            Storage.Notifications _dbNotification = new Storage.Notifications();

            try
            {
                _dbNotification.Delete(ids);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 547)
                    throw new ApplicationException(Resources.Messages.ErrorCannotDeleteExistingRelationship);
                else
                    throw sqlex;
            }
        }

        internal void Notify(Security.Credential credential)
        {
            DataTable _ids = new DataTable();
            _ids.Columns.Add("IdNotification", typeof(Int64));

            Objects.Notifications.Mailer _mailer = new Objects.Notifications.Mailer();
            foreach (Objects.Notifications.Notification _item in Items(credential).Values)
            {
                try
                {
                    _mailer.Send(_item);
                    _ids.Rows.Add(_item.IdNotification);
                }
                catch(SmtpException){}
            }
            Remove(_ids);
        }

        #endregion

    }
}
