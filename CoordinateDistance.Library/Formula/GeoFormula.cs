using System;

namespace CoordinateDistance.Library.Formula
{
    /// <summary>
    /// Earth coordinate distance formula. Source https://en.wikipedia.org/wiki/Great-circle_distance
    /// use the first formula
    /// </summary>
    public interface IGeoFormula
    {
        double CalculateEarthCoordinateDistance(Coordinate pointA, Coordinate pointB);
        double CalculateCentralAngleOfTwoCoordinates(Coordinate pointA, Coordinate pointB);
    }

    public class GeoFormula : IGeoFormula
    {
        /// <summary>
        /// Central angle of two coordinates. Source https://en.wikipedia.org/wiki/Great-circle_distance
        /// </summary>
        /// <returns>The central angle of two coordinates.</returns>
        /// <param name="pointA">Point a.</param>
        /// <param name="pointB">Point b.</param>
        public double CalculateCentralAngleOfTwoCoordinates(Coordinate pointA, Coordinate pointB)
        {
            var absLongitudeDelta = Math.Abs(pointA.LongitudeInRadian - pointB.LongitudeInRadian);
            var sineResult = Math.Sin(pointA.LatitudeInRadian) * Math.Sin(pointB.LatitudeInRadian);
            var cosineResult = Math.Cos(pointA.LatitudeInRadian) * Math.Cos(pointB.LatitudeInRadian) * Math.Cos(absLongitudeDelta);
            return Math.Acos(sineResult + cosineResult);
        }

        /// <summary>
        /// Earth coordinate distance formula. Source https://en.wikipedia.org/wiki/Great-circle_distance
        /// </summary>
        /// <returns>The earth coordinate distance.</returns>
        /// <param name="pointA">Point a.</param>
        /// <param name="pointB">Point b.</param>
        public double CalculateEarthCoordinateDistance(Coordinate pointA, Coordinate pointB)
        {
            return Constant.EarthRadiusInKm * CalculateCentralAngleOfTwoCoordinates(pointA, pointB);
        }
    }
}
