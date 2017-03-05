﻿
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TempMonitor.Domain;

using static System.Console;

namespace TempMonitor.UI
{
	public class Program
	{
		public static void Main()
		{
			Program program = new Program();
			program.RunThermometerMonitor();

			WriteLine("\nPress any key to exit.");
			ReadKey();
		}

		public void RunThermometerMonitor()
		{
			Thermometer thermometer = new Thermometer();
			List<TemperatureThreshold> temperatureThresholdList = CreateTemperatureThresholds();
			thermometer.SetTemperatureThresholds(temperatureThresholdList);
			thermometer.TemperatureThresholdReached += HandleThermometerThresholdReached;

			WriteLine("\nWelcome to the temperature monitor.");
			WriteLine(thermometer.ToString());
			WriteLine("Temperature values are followed by C or F.");
			WriteLine("Examples: 100C, 100 C, 100c, 20.0C, 32.0F, 32 F, 0.0C, -0.1C, +0.1C");
			WriteLine("Enter blank line to exit.\n");

			while (true)
			{
				Write("Enter temperature >");
				string input = ReadLine();

				if (string.IsNullOrWhiteSpace(input))
				{
					break;
				}

				Temperature temperature;
				bool readTemperature = ReadTemperature(input, out temperature);

				if (readTemperature)
				{
					WriteLine($"Temperature = {temperature}");
					thermometer.Temperature = temperature;
				}
				else
				{
					WriteLine("Could not parse input. Try again.");
				}
			}
		}

		public static List<TemperatureThreshold> CreateTemperatureThresholds()
		{
			List<TemperatureThreshold> temperatureThresholdList = new List<TemperatureThreshold>
			{
				new TemperatureThreshold
				{
					Name = "Freezing",
					Temperature = new Temperature(0),
					Tolerance = new Temperature(0.5),
					Direction = TemperatureDirection.Decreasing
				},

				new TemperatureThreshold
				{
					Name = "Room Temperature",
					Temperature = new Temperature(20),
					Tolerance = new Temperature(0.5),
					Direction = TemperatureDirection.Increasing
				},

				new TemperatureThreshold
				{
					Name = "Boiling",
					Temperature = new Temperature(100),
					Tolerance = new Temperature(0.5),
					Direction = TemperatureDirection.Increasing
				}
			};

			return temperatureThresholdList;
		}

		public static void HandleThermometerThresholdReached(object sender, TemperatureThresholdEventArgs temperatureThresholdEventArgs)
		{
			TemperatureThreshold temperatureThreshold = temperatureThresholdEventArgs.TemperatureThreshold;
			string name = temperatureThreshold.Name;
			Temperature temperature = temperatureThreshold.Temperature;
			TemperatureDirection direction = temperatureThreshold.Direction;
			string message = $"Reached temperature threshold: {name}, temperature = {temperature:F1}, Direction = {direction}";
			WriteLine(message);
		}

		public static bool ReadTemperature(string input, out Temperature temperature)
		{
			input = input.Trim();

			// Match a double without exponent followed by optional white space and C or F (case insensitive)
			// For example: 6.0 C, +32.0 F, -10.123 C, -.12345 F, 2 C, 2. C

			const string regularExpression = @"([-+]?[\d]*\.?[\d]*)\s*([CcFf])";
			Match match = Regex.Match(input, regularExpression);

			if (!match.Success)
			{
				temperature = new Temperature(0.0);
				return false;
			}

			string temperatureValue = match.Groups[1].ToString();
			string temperatureType = match.Groups[2].ToString();

			double temp = Convert.ToDouble(temperatureValue);
			bool isCelsius = temperatureType == "C" || temperatureType == "C";

			temperature = isCelsius ? new Temperature(temp) : new Temperature(temp, TemperatureType.Fahrenheit);

			return true;
		}
	}
}
