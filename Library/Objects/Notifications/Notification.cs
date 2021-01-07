using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Notifications
{
    public class Notification
    {
        internal Notification(Int64 idNotification, DateTime date, Users.User user, String title, String message)
        {
            _IdNotification = idNotification;
            _Date = date;
            _User = user;
            _Title = title;
            _Message = message;

        }

        #region Private Properties

        private Int64 _IdNotification;
        private DateTime _Date;
        private Users.User _User;
        private String _Title;
        private String _Message;

        #endregion

        #region Public Properties

        public Int64 IdNotification
        { get { return _IdNotification; } }
        public DateTime Date
        { get { return _Date; } }
        public Users.User User
        { get { return _User; } }
        public String Title
        { get { return _Title; } }
        public String Message
        { get { return _Message; } }


        #endregion
    }
}
