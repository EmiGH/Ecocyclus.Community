using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Auxiliaries.Geographic
{
    public class Contact
    {
        internal Contact(Location location, String telephone, String email, String website, String facebook, String twitter)
        {
            _Location = location;
            _Telephone = telephone;
            _Email = email;
            _Website = website;
            _Facebook = facebook;
            _Twitter = twitter;
        }
        internal Contact(String telephone, String email, String website, String facebook, String twitter)
        {
            _Telephone = telephone;
            _Email = email;
            _Website = website;
            _Facebook = facebook;
            _Twitter = twitter;
        }

        #region Private Fields

        private Location _Location;
        private String _Telephone;
        private String _Email;
        private String _Website;
        private String _Facebook;
        private String _Twitter;
        
        #endregion

        #region Public Properties

        public Location Location
        { get { return _Location; } }
        public String Telephone
        { get { return _Telephone; } }
        public String Email
        { get { return _Email; } }
        public String Website
        { get { return _Website; } }
        public String Facebook
        { get { return _Facebook; } }
        public String Twitter
        { get { return _Twitter; } }


        #endregion

    }
}
