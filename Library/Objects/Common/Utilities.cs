using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Common
{
    public class Utilities
    {
        public Utilities() { }

        internal static double Root(double x, double root)
        {

            if (x < 0 && root % 2 == 1)

                return -Math.Pow(-x, (1.00 / root));

            else

                return Math.Pow(x, (1.00 / root));

        }

    }

    public class Data
    {
        internal Data() { }

        internal enum Protocols
        { Electricity = 0, Fuel = 1, Transport = 2, Waste = 3, Water = 4, CO2 = 5 }
    }
}
