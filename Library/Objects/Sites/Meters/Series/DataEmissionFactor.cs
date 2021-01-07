using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Sites.Meters.Series
{
    public class DataEmissionFactor
    {
        private Auxiliaries.Geographic.Country _Country;
        private Double _Value;
        private String _Description;
        private List<KeyValuePair<String, String>> _Descriptions;

        internal DataEmissionFactor(Auxiliaries.Geographic.Country country, Double value, String description, List<KeyValuePair<String, String>> descriptions)
        {
            _Country = country;
            _Description = description;
            _Descriptions = descriptions;
            _Value = value;
        }

        public Double Value
        { get { return _Value; } }
        public Auxiliaries.Geographic.Country Country;
        public String Description
        { get { return _Description; } }
        public List<KeyValuePair<String, String>> Descriptions
        { get { return _Descriptions; } }

    }
}
