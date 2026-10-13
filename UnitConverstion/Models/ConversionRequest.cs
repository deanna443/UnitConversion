namespace UnitConverstion.Models
{
    public class ConversionRequest
    {
        // Category such as "length", "temperature", "weight". Optional — will attempt to infer if omitted.
        public string? Category { get; set; }

        // Unit names, e.g. "meter", "foot", "celsius", "fahrenheit", "kilogram", "pound"
        public required string FromUnit { get; set; }
        public required string ToUnit { get; set; }

        public double Value { get; set; }
    }
}
