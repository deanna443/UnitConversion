using UnitConverstion.Commands;
using UnitConverstion.Models;
using UnitConverstion.Services;

namespace UnitConverstion.CommandHandlers
{
    public class ConvertUnitCommandHandler
    {
        private readonly IUnitConverter _unitConverter;

        public ConvertUnitCommandHandler(IUnitConverter unitConverter)
        {
            _unitConverter = unitConverter;
        }

        public ConversionResult Handle(ConvertUnitCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.FromUnit) ||
                string.IsNullOrWhiteSpace(command.ToUnit))
            {
                return new ConversionResult
                {
                    Message = "FromUnit and ToUnit are required."
                };
            }

            if (!double.IsFinite(command.Value))
            {
                return new ConversionResult
                {
                    Message = "Value must be a finite number."
                };
            }

            return _unitConverter.Convert(
                command.FromUnit,
                command.ToUnit,
                command.Value,
                command.Category);
        }
    }
}