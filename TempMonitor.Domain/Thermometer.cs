using System;
using System.Collections.Generic;
using System.Text;

namespace TempMonitor.Domain
{
	public class Thermometer : IThermometer
	{
		// The thermometer stores temperatures internally in celsius.
		// When IsFahrenheit = true, temperatues are converted to celsius through the Temperature property

		private double _temperature;
		private double _previousTemperature;
		private bool _eventHandlerFiredForTemperatureThreshold;
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
				return IsFahrenheit ? TemperatureConversion.ConvertCelsiusToFahrenheit(_temperature) : _temperature;
			}

			set
			{
				if (IsFahrenheit)
				{
					double tempTemperature = TemperatureConversion.ConvertFahrenheitToCelsius(value);
					_previousTemperature = _temperature;
					_temperature = tempTemperature;
				}
				else
				{
					_previousTemperature = _temperature;
					_temperature = value;
				}

				if (IsAtTemperatureThreshold && !_eventHandlerFiredForTemperatureThreshold)
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
					const double standardTolerance = .01;
					double tolerance = standardTolerance;
					bool previouslyAtThisThreshold = temperatureThreshold == CurrentTemperatureThreshold;

					// If previously at this threshold, then set "wider" tolerance for this threshold to reduce fluctuations
					if (previouslyAtThisThreshold)
					{
						tolerance = temperatureThreshold.Tolerance;
					}

					if (AreEqualWithinTolerance(_temperature, temperatureThreshold.Temperature, tolerance))
					{
						if (previouslyAtThisThreshold)
						{
							_eventHandlerFiredForTemperatureThreshold = true;
							return true;
						}
						
						TemperatureDirection temperatureDirection = _temperature >= _previousTemperature ? TemperatureDirection.Increasing : TemperatureDirection.Decreasing;
						bool isSameTemperatureDirection = temperatureDirection == temperatureThreshold.Direction;

						if (isSameTemperatureDirection)
						{
							CurrentTemperatureThreshold = temperatureThreshold;
							_eventHandlerFiredForTemperatureThreshold = false;

						}

						return isSameTemperatureDirection;
					}
				}

				return false;
			}
		}

		public override string ToString()
	    {
		    StringBuilder stringBuilder = new StringBuilder();

		    stringBuilder.AppendLine("Configured temperature thresholds:");

		    foreach (TemperatureThreshold temperatureThreshold in _temperatureThresholdList)
		    {
			    string name = temperatureThreshold.Name.PadRight(20);
			    double temperature = temperatureThreshold.Temperature;
			    double tolerance = temperatureThreshold.Tolerance;
			    string direction = temperatureThreshold.Direction.ToString();

				if (IsFahrenheit)
			    {
				    double fahrenheitTemperature = TemperatureConversion.ConvertCelsiusToFahrenheit(temperature);
				    string fahrenheitTemperatureString = fahrenheitTemperature.ToString("F2").PadLeft(8);
				    string toleranceString = tolerance.ToString("F2").PadLeft(6);
				    string directionString = direction.PadLeft(10);
					const string format = "{0}: Temperature = {1}F, Tolerance = {2}, Direction = {3}\n";

					stringBuilder.AppendFormat(format, name, fahrenheitTemperatureString, toleranceString, directionString);
			    }
			    else
				{
					string temperatureString = temperature.ToString("F2").PadLeft(8);
					string toleranceString = tolerance.ToString("F2").PadLeft(6);
					const string format = "{0}: Temperature = {1}C, Tolerance = {2}, Direction = {3}\n";

					stringBuilder.AppendFormat(format, name, temperatureString, toleranceString, direction);
				}
		    }

		    return stringBuilder.ToString();
	    }

	    protected virtual void OnTemperatureThresholdReached(TemperatureThresholdEventArgs eventArgs)
	    {
		    TemperatureThresholdReached?.Invoke(this, eventArgs);
	    }

	    private static bool AreEqualWithinTolerance(double x, double y, double tolerance)
	    {
		    return Math.Abs(x - y) <= tolerance;
		}
	}
}
