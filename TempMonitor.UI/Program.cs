
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TempMonitor.Domain;

namespace TempMonitor.UI
{
	public class Program
	{
		public static void Main()
		{
			Program program = new Program();
			program.StartThermometerMonitor();

			Console.WriteLine("\nPress any key to exit.");
			Console.ReadKey();
		}

		public void StartThermometerMonitor()
		{
			Thermometer thermometer = new Thermometer();
			List<TemperatureThreshold> temperatureThresholdList = CreateTemperatureThresholds();
			thermometer.SetTemperatureThresholds(temperatureThresholdList);
			thermometer.TemperatureThresholdReached += HandleThermometerThresholdReached;

			Console.WriteLine("\nWelcome to the temperature monitor.\n");
			Console.WriteLine(thermometer.ToString());
			Console.WriteLine("Temperature values must be followed by a space and temperature type (C or F).\n");

			bool done;

			do
			{
				Console.Write("Enter temperature (or blank line to exit) >");
				string input = Console.ReadLine();

				done = string.IsNullOrWhiteSpace(input);

				if (done)
				{
					continue;
				}

				double temperature;
				bool readTemperature = ReadTemperature(input, out temperature);

				if (readTemperature)
				{
					thermometer.Temperature = temperature;
				}
				else
				{
					Console.WriteLine("Could not parse input. Try again.");
				}
			}
			while (!done);
		}

		public static List<TemperatureThreshold> CreateTemperatureThresholds()
		{
			List<TemperatureThreshold> temperatureThresholdList = new List<TemperatureThreshold>
			{
				new TemperatureThreshold
				{
					Name = "Freezing",
					Temperature = 0,
					Tolerance = 0.5,
					Direction = TemperatureDirection.Decreasing
				},

				new TemperatureThreshold
				{
					Name = "Room Temperature",
					Temperature = 20,
					Tolerance = 0.5,
					Direction = TemperatureDirection.Increasing
				},

				new TemperatureThreshold
				{
					Name = "Boiling",
					Temperature = 100,
					Tolerance = 0.5,
					Direction = TemperatureDirection.Increasing
				}
			};

			return temperatureThresholdList;
		}

		public static void HandleThermometerThresholdReached(object sender, TemperatureThresholdEventArgs temperatureThresholdEventArgs)
		{
			TemperatureThreshold temperatureThreshold = temperatureThresholdEventArgs.TemperatureThreshold;
			string name = temperatureThreshold.Name;
			double temperature = temperatureThreshold.Temperature;
			string message = $"Reached temperature threshold: {name}, temperature = {temperature:F1} C";
			Console.WriteLine(message);
		}

		public static bool ReadTemperature(string input, out double temperature)
		{
			input = input.Trim();

			// Match a double without exponent followed by single white space and C or F, for example: 6.0 C, +32.0 F, -10.123 C, -.12345 F
			const string regularExpression = @"([-+]?[0-9]*\.?[0-9]+)\s([CF])";
			Match match = Regex.Match(input, regularExpression);

			if (!match.Success)
			{
				temperature = 0.0;
				return false;
			}

			string temperatureValue = match.Groups[1].ToString();
			string temperatureType = match.Groups[2].ToString();

			// Conversion will always succeed since the regular expression matched a double without exponent
			temperature = Convert.ToDouble(temperatureValue);

			bool isFahrenheit = temperatureType == "F";

			if (isFahrenheit)
			{
				temperature = ConvertFahrenheitToCelsius(temperature);
			}

			return true;
		}

		public static double ConvertFahrenheitToCelsius(double fahrenheitTemperature)
		{
			const double factor = 5.0/9.0;
			double celsiusTemperature = (fahrenheitTemperature - 32) * factor;
			
			return celsiusTemperature;
		}
	}
}
