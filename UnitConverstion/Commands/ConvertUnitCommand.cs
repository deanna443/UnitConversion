namespace UnitConverstion.Commands
{
    public class ConvertUnitCommand
    {
        public string FromUnit { get; set; } = string.Empty;

        public string ToUnit { get; set; } = string.Empty;

        public double Value { get; set; }

        public string? Category { get; set; }
    }
}