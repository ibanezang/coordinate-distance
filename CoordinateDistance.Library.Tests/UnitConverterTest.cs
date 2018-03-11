using Xunit;
using Shouldly;
using CoordinateDistance.Library.Formula;

namespace CoordinateDistance.Library.Tests
{
    public class UnitConverterTest
    {
        [Theory]
        [InlineData(1, "0.01745")]
        [InlineData(0, "0.00000")]
        [InlineData(0.30, "0.00524")]
        [InlineData(5.106, "0.08912")]
        [InlineData(180, "3.14159")]
        [InlineData(-105.106, "-1.83445")]
        public void DegreeToRadian_VaryingDegreeInput_ShouldGetExpectedResult(double degreeInput, string expected)
        {
            UnitConverter.DegreeToRadian(degreeInput).ToString("F5").ShouldBe(expected);
        }

        [Fact]
        public void Coordinate_ConvertToRadian_ShouldGetCorrectResult()
        {
            var coordinate = new Coordinate(180, -105.106);
            coordinate.LatitudeInRadian.ToString("F5").ShouldBe("3.14159");
            coordinate.LongitudeInRadian.ToString("F5").ShouldBe("-1.83445");
        }
    }
}
