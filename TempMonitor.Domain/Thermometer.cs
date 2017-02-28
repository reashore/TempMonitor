
using System;
using System.Collections.Generic;
using System.Text;

namespace TempMonitor.Domain
{
	public interface IThermometer
	{
		bool IsFahrenheit { get; set; }

		double Temperature { get; set; }

		void SetTemperatureThresholds(List<TemperatureThreshold> temperatureThresholdList);

		bool IsAtTemperatureThreshold { get; }

		TemperatureThreshold CurrentTemperatureThreshold { get; set; }

		event EventHandler<TemperatureThresholdEventArgs> TemperatureThresholdReached;

		string ToString();
	}

	public class Thermometer : IThermometer
	{
		// The thermometer stores temperatures internally in centigrade.
		// When IsFahrenheit = true, temperatues are converted to centigrade.

		private double _temperature;
		private double _previousTemperature;
		private readonly List<TemperatureThreshold> _temperatureThresholdList = new List<TemperatureThreshold>();

		public Thermometer(bool isFahrenheit = false)
		{
			IsFahrenheit = isFahrenheit;
		}

		public bool IsFahrenheit { get; set; }
		public TemperatureThreshold CurrentTemperatureThreshold { get; set; }
		public event EventHandler<TemperatureThresholdEventArgs> TemperatureThresholdReached;

	    public void SetTemperatureThresholds(List<TemperatureThreshold> temperatureThresholdList)
	    {
		    foreach (TemperatureThreshold temperatureThreshold in temperatureThresholdList)
		    {
				_temperatureThresholdList.Add(temperatureThreshold);
			}
	    }

		public double Temperature
		{
			get
			{
				return IsFahrenheit ? ConvertCelsiusToFahrenheit(_temperature) : _temperature;
			}

			set
			{
				if (IsFahrenheit)
				{
					double temp = ConvertFahrenheitToCelsius(value);
					_previousTemperature = _temperature;
					_temperature = temp;
				}
				else
				{
					_previousTemperature = _temperature;
					_temperature = value;
				}

				if (IsAtTemperatureThreshold)
				{
					OnTemperatureThresholdReached(new TemperatureThresholdEventArgs(CurrentTemperatureThreshold));
				}
			}
		}

		public bool IsAtTemperatureThreshold
		{
			get
			{
				foreach (TemperatureThreshold temperatureThreshold in _temperatureThresholdList)
				{
					double tolerance = .01;

					// If currently at this threshold, then set the tolerance for this threshold
					if (temperatureThreshold == CurrentTemperatureThreshold)
					{
						tolerance = temperatureThreshold.Tolerance;
					}

					if (AreEqualWithinTolerance(_temperature, temperatureThreshold.Temperature, tolerance))
					{
						TemperatureDirection temperatureDirection = _temperature >= _previousTemperature ? TemperatureDirection.Increasing : TemperatureDirection.Decreasing;
						bool isSameTemperatureDirection = temperatureDirection == temperatureThreshold.Direction;

						CurrentTemperatureThreshold = temperatureThreshold;
						return isSameTemperatureDirection;
					}
				}

				CurrentTemperatureThreshold = null;
				return false;
			}
		}

		public override string ToString()
	    {
		    StringBuilder stringBuilder = new StringBuilder();

		    stringBuilder.AppendLine("Configured temperature thresholds:");

		    foreach (TemperatureThreshold temperatureThreshold in _temperatureThresholdList)
		    {
			    string name = temperatureThreshold.Name.PadRight(30);
			    double temperature = temperatureThreshold.Temperature;
			    double tolerance = temperatureThreshold.Tolerance;
			    string direction = temperatureThreshold.Direction.ToString();

				if (IsFahrenheit)
			    {
				    double fahrenheitTemperature = ConvertCelsiusToFahrenheit(temperature);
				    string fahrenheitTemperatureString = fahrenheitTemperature.ToString("F2").PadLeft(8);
				    string toleranceString = tolerance.ToString("F2").PadLeft(6);
				    string directionString = direction.PadLeft(10);
					const string format = "{0}: Temperature = {1} F, Tolerance = {2}, Direction = {3}\n";

					stringBuilder.AppendFormat(format, name, fahrenheitTemperatureString, toleranceString, directionString);
			    }
			    else
				{
					string temperatureString = temperature.ToString("F2").PadLeft(8);
					string toleranceString = tolerance.ToString("F2").PadLeft(6);
					const string format = "{0}: Temperature = {1} C, Tolerance = {2}, Direction = {3}\n";

					stringBuilder.AppendFormat(format, name, temperatureString, toleranceString, direction);
				}
		    }

		    return stringBuilder.ToString();
	    }

	    protected virtual void OnTemperatureThresholdReached(TemperatureThresholdEventArgs eventArgs)
	    {
		    TemperatureThresholdReached?.Invoke(this, eventArgs);
	    }

		#region Private Methods

	    private static bool AreEqualWithinTolerance(double x, double y, double tolerance)
	    {
		    return Math.Abs(x - y) <= tolerance;
		}

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

		#endregion
	}
}
