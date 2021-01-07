using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Types;
using System.Data.SqlTypes;

namespace CSI.Library.Objects.Auxiliaries.Geographic
{
    public class Position
    {
        
        internal Position(SqlGeography geography)
        {
            _Latitude = (Double)geography.Lat;
            _Longitude = (Double)geography.Long;
        }
        public Position(Double latitude, Double longitude)
        {
            _Latitude = latitude;
            _Longitude = longitude;
        }
        public Position(String coordenates)
        {
            if (coordenates != "")
            {
                coordenates = coordenates.Replace(',', '.');
                String[] _coordenates = coordenates.Split(';');
                _Latitude = Double.Parse(_coordenates[0], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture);
                _Longitude = Double.Parse(_coordenates[1], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture);
            }
        }

        #region Private Fields

        private const int _SRID = 4326;
        private Double _Latitude;
        private Double _Longitude;

        #endregion

        #region Public Properties

        internal SqlGeography ToSqlGeography
        {
            get
            {
                String _point = "POINT (" + _Longitude.ToString(System.Globalization.CultureInfo.InvariantCulture) + " " + _Latitude.ToString(System.Globalization.CultureInfo.InvariantCulture) + ")";
                return SqlGeography.STPointFromText(new SqlChars(new SqlString(_point)), _SRID);
            }
        }
               
        public Double Latitude
        { get { return _Latitude; } }
        public Double Longitude
        { get { return _Longitude; } }

        public String Coordenates
        { get { return Latitude.ToString() + ";" + Longitude.ToString(); } }
        internal static Units.Unit UnitPattern(Security.Credential credential)
        { return new Handlers.Units().ItemForSQL(credential); }

        #endregion
    }

    public class GeoLocation
    {
        private double radLat;  // latitude in radians
        private double radLon;  // longitude in radians

        private double degLat;  // latitude in degrees
        private double degLon;  // longitude in degrees

        private static double MIN_LAT = CDTR(-90);  // -PI/2
        private static double MAX_LAT = CDTR(90);   //  PI/2
        private static double MIN_LON = CDTR(-180); // -PI
        private static double MAX_LON = CDTR(180);  //  PI

        private const double earthRadius = 6371.01;

        private GeoLocation()
        {
        }

        /// <summary>
        /// Return GeoLocation from Degrees
        /// </summary>
        /// <param name="latitude">The latitude, in degrees.</param>
        /// <param name="longitude">The longitude, in degrees.</param>
        /// <returns>GeoLocation in Degrees</returns>
        public static GeoLocation FromDegrees(double latitude, double longitude)
        {
            GeoLocation result = new GeoLocation
            {
                radLat = CDTR(latitude),
                radLon = CDTR(longitude),
                degLat = latitude,
                degLon = longitude
            };
            result.CheckBounds();
            return result;
        }

        /// <summary>
        /// Return GeoLocation from Radians
        /// </summary>
        /// <param name="latitude">The latitude, in radians.</param>
        /// <param name="longitude">The longitude, in radians.</param>
        /// <returns>GeoLocation in Radians</returns>
        public static GeoLocation FromRadians(double latitude, double longitude)
        {
            GeoLocation result = new GeoLocation
            {
                radLat = latitude,
                radLon = longitude,
                degLat = CRTD(latitude),
                degLon = CRTD(longitude)
            };
            result.CheckBounds();
            return result;
        }

        private void CheckBounds()
        {
            if (radLat < MIN_LAT || radLat > MAX_LAT ||
                    radLon < MIN_LON || radLon > MAX_LON)
                throw new Exception("Arguments are out of bounds");
        }

        /**
         * @return the latitude, in degrees.
         */
        public double getLatitudeInDegrees()
        {
            return degLat;
        }

        /**
         * @return the longitude, in degrees.
         */
        public double getLongitudeInDegrees()
        {
            return degLon;
        }

        /**
         * @return the latitude, in radians.
         */
        public double getLatitudeInRadians()
        {
            return radLat;
        }

        /**
         * @return the longitude, in radians.
         */
        public double getLongitudeInRadians()
        {
            return radLon;
        }

        public override string ToString()
        {
            return "(" + degLat + "\u00B0, " + degLon + "\u00B0) = (" +
                     radLat + " rad, " + radLon + " rad)";
        }

        /// <summary>
        /// Computes the great circle distance between this GeoLocation instance and the location argument.
        /// </summary>
        /// <param name="location">Location to act as the centre point</param>
        /// <returns>the distance, measured in the same unit as the radius argument.</returns>
        public double DistanceTo(GeoLocation location)
        {
            return Math.Acos(Math.Sin(radLat) * Math.Sin(location.radLat) +
                    Math.Cos(radLat) * Math.Cos(location.radLat) *
                    Math.Cos(radLon - location.radLon)) * earthRadius;
        }

        /// <summary>
        /// Computes the bounding coordinates of all points on the surface
        /// of a sphere that have a great circle distance to the point represented
        /// by this GeoLocation instance that is less or equal to the distance
        /// argument.
        /// For more information about the formulae used in this method visit
        /// http://JanMatuschek.de/LatitudeLongitudeBoundingCoordinates
        /// </summary>
        /// <param name="distance">The distance from the point represented by this 
        /// GeoLocation instance. Must me measured in the same unit as the radius argument.
        /// </param>
        /// <returns>An array of two GeoLocation objects such that:
        /// 
        /// a) The latitude of any point within the specified distance is greater
        /// or equal to the latitude of the first array element and smaller or
        /// equal to the latitude of the second array element.
        /// If the longitude of the first array element is smaller or equal to
        /// the longitude of the second element, then
        /// the longitude of any point within the specified distance is greater
        /// or equal to the longitude of the first array element and smaller or
        /// equal to the longitude of the second array element.
        /// 
        /// b) If the longitude of the first array element is greater than the
        /// longitude of the second element (this is the case if the 180th
        /// meridian is within the distance), then
        /// the longitude of any point within the specified distance is greater
        /// or equal to the longitude of the first array element
        /// or smaller or equal to the longitude of the second
        /// array element.</returns>
        public GeoLocation[] BoundingCoordinates(double distance)
        {

            if (distance < 0d)
                throw new Exception("Distance cannot be less than 0");

            // angular distance in radians on a great circle
            double radDist = distance / earthRadius;

            double minLat = radLat - radDist;
            double maxLat = radLat + radDist;

            double minLon, maxLon;
            if (minLat > MIN_LAT && maxLat < MAX_LAT)
            {
                double deltaLon = Math.Asin(Math.Sin(radDist) /
                    Math.Cos(radLat));
                minLon = radLon - deltaLon;
                if (minLon < MIN_LON) minLon += 2d * Math.PI;
                maxLon = radLon + deltaLon;
                if (maxLon > MAX_LON) maxLon -= 2d * Math.PI;
            }
            else
            {
                // a pole is within the distance
                minLat = Math.Max(minLat, MIN_LAT);
                maxLat = Math.Min(maxLat, MAX_LAT);
                minLon = MIN_LON;
                maxLon = MAX_LON;
            }

            return new GeoLocation[]
            {
                FromRadians(minLat, minLon),
                FromRadians(maxLat, maxLon)
            };
        }


        public static double CDTR(double degrees)
        {
            return ((Math.PI / 180) * degrees);
        }
        public static double CRTD(double radians)
        {
            return ((180 / Math.PI) * radians);
        }
    }
}
