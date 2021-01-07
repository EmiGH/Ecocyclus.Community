using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Handlers
{
    internal class Mail
    {
        internal Mail() { }

        internal Objects.Notifications.Configuration Configuration()
        {
            Storage.Mail _dbMail = new Storage.Mail();
            Library.Objects.Notifications.Configuration _configuration = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbMail.ReadAll();
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _configuration = new Library.Objects.Notifications.Configuration(Convert.ToString(_dbRecord["Sender"]), Convert.ToString(_dbRecord[3]), Convert.ToString(_dbRecord["Host"]), Convert.ToInt32(_dbRecord["Port"]), Convert.ToString(_dbRecord["Username"]), Convert.ToString(_dbRecord["Password"]));
            }
            return _configuration;
        }
    }
}
