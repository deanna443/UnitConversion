using UnitConverstion.Models;

namespace UnitConverstion.Services
{
    public interface IUnitConverter
    {
        ConversionResult Convert(string fromUnit, string toUnit, double value, string? category = null);
    }
}
