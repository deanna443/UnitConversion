namespace UnitConverstion.Models
{
    public class ConversionResult
    {
        public string FromUnit { get; set; } = string.Empty;
        public string ToUnit { get; set; } = string.Empty;
        public double OriginalValue { get; set; }
        public double ConvertedValue { get; set; }
        public string Category { get; set; } = string.Empty;
    }
}
