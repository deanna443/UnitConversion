# Unit Conversion API

Simple ASP.NET Core Web API that converts numeric values between units across categories: Length, Temperature, and Weight/Mass. Conversions are implemented with hardcoded factors and formulas.

Supported categories and example units:
- Length: meter (m), kilometer (km), centimeter (cm), millimeter (mm), inch (in), foot (ft), yard (yd), mile (mi)
- Temperature: celsius (c), fahrenheit (f), kelvin (k)
- Weight/Mass: kilogram (kg), gram (g), milligram (mg), pound (lb), ounce (oz), ton (t)

Run locally
1. Ensure .NET 10 SDK is installed.
2. From the repository root run:

   dotnet run --project UnitConverstion

3. By default the app runs on HTTPS; use the health endpoint to verify:

   GET https://localhost:5001/api/unitconversion/health

Convert endpoint
- POST https://localhost:5001/api/unitconversion/convert
- Body (JSON):

  {
	"fromUnit": "meter",
	"toUnit": "foot",
	"value": 1
  }

Response:

  {
	"fromUnit": "meter",
	"toUnit": "foot",
	"originalValue": 1.0,
	"convertedValue": 3.280839895013123,
	"category": "length",
	"message": null
  }

Design notes
- Conversions are hardcoded and performed by UnitConverter. Length and weight use factor maps to a base unit; temperature uses formulas via conversion to/from Celsius.
- The implementation is intentionally minimal to satisfy the assessment requirements and can be extended to support more units, persistence, or an admin UI.
