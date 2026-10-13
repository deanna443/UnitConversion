using System;
using UnitConverstion.Services;
using Xunit;

namespace UnitConverstion.Tests
{
    public class UnitConverterTests
    {
        [Fact]
        public void MeterToFoot_ConvertsCorrectly()
        {
            var converter = new UnitConverter();
            var res = converter.Convert("meter", "foot", 1);
            Assert.True(string.IsNullOrEmpty(res.Message));
            Assert.Equal("length", res.Category);
            // 1 meter = 3.280839895013123 feet
            Assert.InRange(res.ConvertedValue, 3.280839, 3.28084);
        }

        [Fact]
        public void CelsiusToFahrenheit_ConvertsCorrectly()
        {
            var converter = new UnitConverter();
            var res = converter.Convert("celsius", "fahrenheit", 0);
            Assert.True(string.IsNullOrEmpty(res.Message));
            Assert.Equal("temperature", res.Category);
            Assert.Equal(32, Math.Round(res.ConvertedValue));
        }
    }
}
