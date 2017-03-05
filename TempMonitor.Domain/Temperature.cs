using System;
using System.Diagnostics;

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
		private static readonly Temperature DefaultTolerance = new Temperature(.01);

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

		public static void ResetToleranceToDefault()
		{
			Tolerance = DefaultTolerance;
		}

		public override string ToString()
		{
			string temperatureSymbol = TemperatureType == TemperatureType.Celsius ? "C" : "F";
			string displayString = $"{Value}{temperatureSymbol}";
			return displayString;
		}

		#region Temperature Comparison

		// Reference equality is of little value for Temperatures.
		// Instead, Temperatures should be compared for equal temperature values

		private static double ConvertFahrenheitToCelsius(double fahrenheitTemperature)
		{
			const double factor = 5.0 / 9.0;
			double celsiusTemperature = factor * (fahrenheitTemperature - 32);

			return celsiusTemperature;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is Temperature))
			{
				return false;
			}

			return Equals((Temperature) obj);
		}

		public bool Equals(Temperature temperature)
		{
			if (ReferenceEquals(temperature, null))
			{
				return false;
			}

			return AreEqualWithinTolerance(this, temperature);
		}

		public override int GetHashCode()
		{
			// For hash algorithm see C# 5.0 in a Nutshell, page 263
			int hash = 17;
			hash = hash * 31 + Value.GetHashCode();
			return hash;
		}

		public static bool operator == (Temperature temperature1, Temperature temperature2)
		{
			if (ReferenceEquals(temperature1, null))
			{
				return ReferenceEquals(temperature2, null);
			}

			return temperature1.Equals(temperature2);
		}

		public static bool operator != (Temperature temperature1, Temperature temperature2)
		{
			if (ReferenceEquals(temperature1, null))
			{
				return !ReferenceEquals(temperature2, null);
			}

			if (ReferenceEquals(temperature2, null))
			{
				return true;
			}

			return !temperature1.Equals(temperature2);
		}

		public static bool operator >= (Temperature temperature1, Temperature temperature2)
		{
			//if (ReferenceEquals(temperature1, null))
			//{
			//	return !ReferenceEquals(temperature2, null);
			//}

			//if (ReferenceEquals(temperature2, null))
			//{
			//	return true;
			//}

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

		private static bool AreEqualWithinTolerance(Temperature temperature1, Temperature temperature2)
		{
			// convert all temperatures to Celsius for comparison
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

		#endregion
	}
}
