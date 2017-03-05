using System;

namespace TempMonitor.Domain
{
	public enum TemperatureType
	{
		Celsius,
		Fahrenheit
	}

	// Temperature should have a standard tolerance for comparing temperatures
	// the default can be overridden
	public class Temperature
	{
		private static readonly Temperature DefaultTolerance = new Temperature(.0001);

		static Temperature()
		{
			Tolerance = DefaultTolerance;
		}
		public Temperature()
		{
			Value = 0;
			TemperatureType = TemperatureType.Celsius;
		}

		public Temperature(double temperature, TemperatureType temperatureType = TemperatureType.Celsius)
		{
			Value = temperature;
			TemperatureType = temperatureType;
		}

		public double Value { get; }

		public TemperatureType TemperatureType { get; }

		public static Temperature Tolerance { get; set; } 

		public override string ToString()
		{
			string temperatureSymbol = TemperatureType == TemperatureType.Celsius ? "C" : "F";
			string displayString = $"{Value}{temperatureSymbol}";
			return displayString;
		}

		#region Temperature Comparison

		private static double ConvertFahrenheitToCelsius(double fahrenheitTemperature)
		{
			const double factor = 5.0 / 9.0;
			double celsiusTemperature = factor * (fahrenheitTemperature - 32);

			return celsiusTemperature;
		}

		public bool AreEqualWithinTolerance(Temperature temperature1, Temperature temperature2, Temperature tolerance = null )
		{
			if (tolerance == null)
			{
				Tolerance = DefaultTolerance;
			}

			// convert to Celsius for comparison
			double temperatureCelsius1 = temperature1.Value;
			double temperatureCelsius2 = temperature2.Value;
			double toleranceCelsius = Tolerance.Value;

			if (temperature1.TemperatureType == TemperatureType.Fahrenheit)
			{
				temperatureCelsius1 = ConvertFahrenheitToCelsius(temperature1.Value);
			}

			if (temperature2.TemperatureType == TemperatureType.Fahrenheit)
			{
				temperatureCelsius2 = ConvertFahrenheitToCelsius(temperature2.Value);
			}

			if (Tolerance.TemperatureType == TemperatureType.Fahrenheit)
			{
				toleranceCelsius = ConvertFahrenheitToCelsius(Tolerance.Value);
			}

			return Math.Abs(temperatureCelsius1 - temperatureCelsius2) <= toleranceCelsius;
		}

		public override bool Equals(object obj)
		{
			return true;
		}

		public override int GetHashCode()
		{
			return 0;
		}

		// todo need .Equal() and ==
		public bool Equal(Temperature temperature1, Temperature temperature2)
		{           
			// todo values can be null
			return AreEqualWithinTolerance(temperature1, temperature2);

		}

		public static bool operator == (Temperature temperature1, Temperature temperature2)
		{
			// convert to Celsius for comparison
			double temperatureCelsius1 = temperature1.Value;
			double temperatureCelsius2 = temperature2.Value;

			if (temperature1.TemperatureType == TemperatureType.Fahrenheit)
			{
				temperatureCelsius1 = ConvertFahrenheitToCelsius(temperature1.Value);
			}

			if (temperature2.TemperatureType == TemperatureType.Fahrenheit)
			{
				temperatureCelsius2 = ConvertFahrenheitToCelsius(temperature2.Value);
			}

			return temperatureCelsius1 >= temperatureCelsius2;
		}

		public static bool operator != (Temperature temperature1, Temperature temperature2)
		{
			// convert to Celsius for comparison
			double temperatureCelsius1 = temperature1.Value;
			double temperatureCelsius2 = temperature2.Value;

			if (temperature1.TemperatureType == TemperatureType.Fahrenheit)
			{
				temperatureCelsius1 = ConvertFahrenheitToCelsius(temperature1.Value);
			}

			if (temperature2.TemperatureType == TemperatureType.Fahrenheit)
			{
				temperatureCelsius2 = ConvertFahrenheitToCelsius(temperature2.Value);
			}

			return temperatureCelsius1 >= temperatureCelsius2;
		}

		public static bool operator >= (Temperature temperature1, Temperature temperature2)
		{
			// convert to Celsius for comparison
			double temperatureCelsius1 = temperature1.Value;
			double temperatureCelsius2 = temperature2.Value;

			if (temperature1.TemperatureType == TemperatureType.Fahrenheit)
			{
				temperatureCelsius1 = ConvertFahrenheitToCelsius(temperature1.Value);
			}

			if (temperature2.TemperatureType == TemperatureType.Fahrenheit)
			{
				temperatureCelsius2 = ConvertFahrenheitToCelsius(temperature2.Value);
			}

			return temperatureCelsius1 >= temperatureCelsius2;
		}

		public static bool operator <=(Temperature temperature1, Temperature temperature2)
		{
			// convert to Celsius for comparison
			double temperatureCelsius1 = temperature1.Value;
			double temperatureCelsius2 = temperature2.Value;

			if (temperature1.TemperatureType == TemperatureType.Fahrenheit)
			{
				temperatureCelsius1 = ConvertFahrenheitToCelsius(temperature1.Value);
			}

			if (temperature2.TemperatureType == TemperatureType.Fahrenheit)
			{
				temperatureCelsius2 = ConvertFahrenheitToCelsius(temperature2.Value);
			}

			return temperatureCelsius1 <= temperatureCelsius2;
		}

		#endregion
	}
}
