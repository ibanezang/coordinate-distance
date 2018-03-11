using CoordinateDistance.Library.Formula;
using Shouldly;
using Xunit;

namespace CoordinateDistance.Library.Tests
{
    public class GeoFormulaTest
    {
        private GeoFormula _geoFormula;
        public GeoFormulaTest()
        {
            _geoFormula = new GeoFormula();
        }

        [Fact]
        public void CalculateCentralAngleOfTwoCoordinates_PointAAndPointBHaveTheSameCoordinate_DistanceShouldBeZero()
        {
            var pointA = new Coordinate(105.03, -30.302);
            var pointB = new Coordinate(105.03, -30.302);
            _geoFormula.CalculateCentralAngleOfTwoCoordinates(pointA, pointB).ShouldBe(0);
        }

        [Theory]
        [InlineData(0, 0, "0.93538")]
        [InlineData(105.0839, -89.93839, "0.71140")]
        [InlineData(-105.0839, -89.93839, "2.48430")]
        [InlineData(-105.0839, 89.93839, "2.43070")]
        public void CalculateCentralAngleOfTwoCoordinates_VaryingPointBCoordinate_ShouldGetExpectedAngle(double longitude, double latitude, string expected)
        {
            var pointA = new Coordinate(53.339428, -6.257664);
            var pointB = new Coordinate(longitude, latitude);
            _geoFormula.CalculateCentralAngleOfTwoCoordinates(pointA, pointB).ToString("F5").ShouldBe(expected);
        }

        [Fact]
        public void Calculate_PointAAndPointBHaveTheSameCoordinate_DistanceShouldBeZero()
        {
            var pointA = new Coordinate(105.03, -30.302);
            var pointB = new Coordinate(105.03, -30.302);
            _geoFormula.CalculateEarthCoordinateDistance(pointA, pointB).ShouldBe(0);
        }

        [Theory]
        [InlineData(0, 0, "5965.92239")]
        [InlineData(105.0839, -89.93839, "4537.40812")]
        [InlineData(-105.0839, -89.93839, "15845.12459")]
        [InlineData(-105.0839, 89.93839, "15503.22982")]
        public void Calculate_VaryingPointBCoordinate_ShouldGetExpectedDistance(double longitude, double latitude, string expected)
        {
            var pointA = new Coordinate(53.339428, -6.257664);
            var pointB = new Coordinate(longitude, latitude);
            _geoFormula.CalculateEarthCoordinateDistance(pointA, pointB).ToString("F5").ShouldBe(expected);
        }
    }
}
