using System.Globalization;
using UnitConverstion.Models;

namespace UnitConverstion.Services
{
    public class UnitConverter : IUnitConverter
    {
        private static readonly Dictionary<string, double> LengthToMeter = new(StringComparer.OrdinalIgnoreCase)
        {
            {"m", 1}, {"meter", 1}, {"metre", 1},
            {"km", 1000}, {"kilometer", 1000}, {"kilometre", 1000},
            {"cm", 0.01}, {"centimeter", 0.01}, {"centimetre", 0.01},
            {"mm", 0.001}, {"millimeter", 0.001},
            {"in", 0.0254}, {"inch", 0.0254},
            {"ft", 0.3048}, {"foot", 0.3048}, {"feet", 0.3048},
            {"yd", 0.9144}, {"yard", 0.9144},
            {"mi", 1609.344}, {"mile", 1609.344}
        };

        private static readonly Dictionary<string, double> WeightToKilogram = new(StringComparer.OrdinalIgnoreCase)
        {
            {"kg", 1}, {"kilogram", 1},
            {"g", 0.001}, {"gram", 0.001},
            {"mg", 0.000001}, {"milligram", 0.000001},
            {"lb", 0.45359237}, {"pound", 0.45359237},
            {"oz", 0.028349523125}, {"ounce", 0.028349523125},
            {"ton", 1000}, {"t", 1000}
        };

        public ConversionResult Convert(string fromUnit, string toUnit, double value, string? category = null)
        {
            var result = new ConversionResult
            {
                FromUnit = fromUnit,
                ToUnit = toUnit,
                OriginalValue = value,
                ConvertedValue = double.NaN,
                Category = string.Empty
            };

            if (string.IsNullOrWhiteSpace(fromUnit) || string.IsNullOrWhiteSpace(toUnit))
            {
                result.Message = "FromUnit and ToUnit must be provided.";
                return result;
            }

            var f = fromUnit.Trim();
            var t = toUnit.Trim();

            // Temperature special handling
            if (IsTemperatureUnit(f) || IsTemperatureUnit(t) || string.Equals(category, "temperature", StringComparison.OrdinalIgnoreCase))
            {
                result.Category = "temperature";
                var success = TryConvertTemperature(f, t, value, out var converted);
                if (!success)
                {
                    result.Message = $"Unsupported temperature unit pair: {fromUnit} -> {toUnit}";
                    return result;
                }

                result.ConvertedValue = converted;
                return result;
            }

            // Length
            if (LengthToMeter.ContainsKey(f) && LengthToMeter.ContainsKey(t) || string.Equals(category, "length", StringComparison.OrdinalIgnoreCase))
            {
                result.Category = "length";
                if (!LengthToMeter.TryGetValue(f, out var fromFactor) || !LengthToMeter.TryGetValue(t, out var toFactor))
                {
                    result.Message = $"Unsupported length unit pair: {fromUnit} -> {toUnit}";
                    return result;
                }

                // Convert to meters then to target
                var meters = value * fromFactor;
                result.ConvertedValue = meters / toFactor;
                return result;
            }

            // Weight
            if (WeightToKilogram.ContainsKey(f) && WeightToKilogram.ContainsKey(t) || string.Equals(category, "weight", StringComparison.OrdinalIgnoreCase) || string.Equals(category, "mass", StringComparison.OrdinalIgnoreCase))
            {
                result.Category = "weight";
                if (!WeightToKilogram.TryGetValue(f, out var fromFactor) || !WeightToKilogram.TryGetValue(t, out var toFactor))
                {
                    result.Message = $"Unsupported weight unit pair: {fromUnit} -> {toUnit}";
                    return result;
                }

                var kg = value * fromFactor;
                result.ConvertedValue = kg / toFactor;
                return result;
            }

            result.Message = "Unable to determine conversion category or unsupported units.";
            return result;
        }

        private static bool IsTemperatureUnit(string u)
        {
            var s = u.Trim().ToLowerInvariant();
            return s is "c" or "celsius" or "f" or "fahrenheit" or "k" or "kelvin";
        }

        private static bool TryConvertTemperature(string from, string to, double value, out double converted)
        {
            converted = double.NaN;
            var f = from.Trim().ToLowerInvariant();
            var t = to.Trim().ToLowerInvariant();

            double toCelsius(double v, string u) => u switch
            {
                "c" or "celsius" => v,
                "f" or "fahrenheit" => (v - 32) * 5.0 / 9.0,
                "k" or "kelvin" => v - 273.15,
                _ => double.NaN
            };

            double fromCelsius(double c, string u) => u switch
            {
                "c" or "celsius" => c,
                "f" or "fahrenheit" => (c * 9.0 / 5.0) + 32,
                "k" or "kelvin" => c + 273.15,
                _ => double.NaN
            };

            var c = toCelsius(value, f);
            if (double.IsNaN(c)) return false;
            var outV = fromCelsius(c, t);
            if (double.IsNaN(outV)) return false;
            converted = outV;
            return true;
        }
    }
}
