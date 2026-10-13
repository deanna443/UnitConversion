using UnitConverstion.Models;

namespace UnitConverstion.Services
{
    public interface IConvertHandler
    {
        ConversionResult Handle(ConversionRequest request);
    }
}
