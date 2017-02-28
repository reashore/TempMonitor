namespace TempMonitor.Domain
{
	public static class TemperatureConversion
	{
		public static double ConvertCelsiusToFahrenheit(double celsiusTemperature)
		{
			const double factor = 9.0 / 5.0;
			double fahrenheitTemperature = celsiusTemperature * factor + 32;

			return fahrenheitTemperature;
		}

		public static double ConvertFahrenheitToCelsius(double fahrenheitTemperature)
		{
			const double factor = 5.0 / 9.0;
			double celsiusTemperature = factor * (fahrenheitTemperature - 32);

			return celsiusTemperature;
		}
	}
}
