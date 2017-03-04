﻿using System;

namespace TempMonitor.Domain
{
	public enum TemperatureType
	{
		Celsius,
		Fahrenheit
	}

	public class Temperature
	{
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


		private static double ConvertFahrenheitToCelsius(double fahrenheitTemperature)
		{
			const double factor = 5.0 / 9.0;
			double celsiusTemperature = factor * (fahrenheitTemperature - 32);

			return celsiusTemperature;
		}

		public static bool AreEqualWithinTolerance(Temperature temperature1, Temperature temperature2, Temperature tolerance)
		{
			// convert to Celsius for comparison
			double temperatureCelsius1 = temperature1.Value;
			double temperatureCelsius2 = temperature2.Value;
			double toleranceCelsius = tolerance.Value;

			if (temperature1.TemperatureType == TemperatureType.Fahrenheit)
			{
				temperatureCelsius1 = ConvertFahrenheitToCelsius(temperature1.Value);
			}

			if (temperature2.TemperatureType == TemperatureType.Fahrenheit)
			{
				temperatureCelsius2 = ConvertFahrenheitToCelsius(temperature2.Value);
			}

			if (tolerance.TemperatureType == TemperatureType.Fahrenheit)
			{
				toleranceCelsius = ConvertFahrenheitToCelsius(tolerance.Value);
			}

			return Math.Abs(temperatureCelsius1 - temperatureCelsius2) <= toleranceCelsius;
		}

		public override string ToString()
		{
			string temperatureSymbol = TemperatureType == TemperatureType.Celsius ? "C" : "F";
			string displayString = $"{Value}{temperatureSymbol}";
			return displayString;
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
	}
}