using System;

namespace CoordinateDistance.Library.Formula
{
    public static class UnitConverter
    {
        /// <summary>
        /// Degrees to radian. Source https://en.wikipedia.org/wiki/Radian
        /// </summary>
        /// <returns>Ouput in radian.</returns>
        /// <param name="degree">input in degree</param>
        public static double DegreeToRadian(double degree){
            return degree * Math.PI / 180;
        }
    }
}
