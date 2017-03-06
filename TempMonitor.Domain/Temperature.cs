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

		// Temperatures should be compared using value semantics (like string)

		public override bool Equals(object obj)
		{
			if (!(obj is Temperature))
			{
				return false;
			}

			return Equals((Temperature) obj);
		}

		public override int GetHashCode()
		{
			// For hash code algorithm see C# 5.0 in a Nutshell, page 263
			int hash = 17;
			hash = hash * 31 + Value.GetHashCode();
			return hash;
		}

		public bool Equals(Temperature temperature)
		{
			if (ReferenceEquals(temperature, null))
			{
				return false;
			}

			return AreEqualWithinTolerance(this, temperature);
		}

		public static bool operator == (Temperature temperature1, Temperature temperature2)
		{
			if (ReferenceEquals(temperature1, null))
			{
				return ReferenceEquals(temperature2, null);
			}

			//if (ReferenceEquals(temperature2, null))
			//{
			//	return false;
			//}

			return temperature1.Equals(temperature2);
		}

		public static bool operator != (Temperature temperature1, Temperature temperature2)
		{
			if (ReferenceEquals(temperature1, null))
			{
				return !ReferenceEquals(temperature2, null);
			}

			//if (ReferenceEquals(temperature2, null))
			//{
			//	return true;
			//}

			return !temperature1.Equals(temperature2);
		}

		public static bool operator >= (Temperature temperature1, Temperature temperature2)
		{
			// todo check for nulls
			double temperatureCelsius1 = GetCelsiusValue(temperature1);
			double temperatureCelsius2 = GetCelsiusValue(temperature2);

			return temperatureCelsius1 >= temperatureCelsius2;
		}

		public static bool operator <=(Temperature temperature1, Temperature temperature2)
		{
			// todo check for nulls
			double temperatureCelsius1 = GetCelsiusValue(temperature1);
			double temperatureCelsius2 = GetCelsiusValue(temperature2);

			return temperatureCelsius1 <= temperatureCelsius2;
		}

		#endregion

		#region Private Methods

		private static bool AreEqualWithinTolerance(Temperature temperature1, Temperature temperature2)
		{
			double temperatureCelsius1 = GetCelsiusValue(temperature1);
			double temperatureCelsius2 = GetCelsiusValue(temperature2);
			double toleranceCelsius = GetCelsiusValue(Tolerance);

			return Math.Abs(temperatureCelsius1 - temperatureCelsius2) <= toleranceCelsius;
		}

		private static double GetCelsiusValue(Temperature temperature)
		{
			double temperatureCelsius = temperature.Value;

			if (temperature.TemperatureType == TemperatureType.Fahrenheit)
			{
				temperatureCelsius = ConvertFahrenheitToCelsius(temperature.Value);
			}

			return temperatureCelsius;
		}

		private static double ConvertFahrenheitToCelsius(double fahrenheitTemperature)
		{
			const double factor = 5.0 / 9.0;
			double celsiusTemperature = factor * (fahrenheitTemperature - 32);

			return celsiusTemperature;
		}

		#endregion
	}
}
