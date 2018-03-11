namespace CoordinateDistance.Library.Formula
{
    public class Coordinate
    {
        /// <summary>
        /// Gets the latitude in degree.
        /// </summary>
        /// <value>The latitude in degree.</value>
        public double Latitude { get; }

        /// <summary>
        /// Gets the longitude in degree.
        /// </summary>
        /// <value>The longitude in degree.</value>
        public double Longitude { get; }

        /// <summary>
        /// Gets the latitude in radian.
        /// </summary>
        /// <value>The latitude in radian.</value>
        public double LatitudeInRadian
        {
            get
            {
                return UnitConverter.DegreeToRadian(Latitude);
            }
        }

        /// <summary>
        /// Gets the longitude in radian.
        /// </summary>
        /// <value>The longitude in radian.</value>
        public double LongitudeInRadian
        {
            get
            {
                return UnitConverter.DegreeToRadian(Longitude);
            }
        }

        public Coordinate(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
