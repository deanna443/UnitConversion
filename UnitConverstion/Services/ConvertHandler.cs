using UnitConverstion.Models;

namespace UnitConverstion.Services
{
    public class ConvertHandler : IConvertHandler
    {
        private readonly IUnitConverter _converter;

        public ConvertHandler(IUnitConverter converter)
        {
            _converter = converter;
        }

        public ConversionResult Handle(ConversionRequest request)
        {
            if (request is null)
                return new ConversionResult { Message = "Request is null." };

            return _converter.Convert(request.FromUnit, request.ToUnit, request.Value, request.Category);
        }
    }
}
