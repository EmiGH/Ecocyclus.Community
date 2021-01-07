using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Notifications
{
    internal class Configuration
    {
        private String _Sender;
        private String _Receiver;
        private String _Host;
        private Int32 _Port;
        private String _Username;
        private String _Password;

        internal Configuration(String sender, String receiver, String host, int port, String username, String password)
        {
            _Sender = sender;
            _Receiver = receiver;
            _Host = host;
            _Port = port;
            _Username = username;
            _Password = password;
        }

        public String Sender
        { get { return _Sender; } }
        public String Receiver
        { get { return _Receiver; } }
        public String Host
        { get { return _Host; } }
        public int Port
        { get { return _Port; } }
        public String Username
        { get { return _Username; } }
        public String Password
        { get { return _Password; } }
    }
}
