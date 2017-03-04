using System;
using System.Collections.Generic;
using System.Text;

namespace TempMonitor.Domain
{
	public class Thermometer : IThermometer
	{
		// The thermometer stores temperatures internally in celsius.
		// When IsFahrenheit = true, temperatues are converted to celsius through the Temperature property

		private Temperature _temperature;
		private Temperature _previousTemperature;
		private bool _eventHandlerFiredForTemperatureThreshold;
		private readonly List<TemperatureThreshold> _temperatureThresholdList = new List<TemperatureThreshold>();

		public Thermometer()
		{
		}

		public TemperatureThreshold CurrentTemperatureThreshold { get; set; }

		public event EventHandler<TemperatureThresholdEventArgs> TemperatureThresholdReached;

	    public void SetTemperatureThresholds(List<TemperatureThreshold> temperatureThresholdList)
	    {
			foreach (TemperatureThreshold temperatureThreshold in temperatureThresholdList)
		    {
				_temperatureThresholdList.Add(temperatureThreshold);
			}
	    }

		public Temperature Temperature
		{
			get
			{
				return _temperature;
			}

			set
			{
				_previousTemperature = _temperature;
				_temperature = value;

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
					Temperature standardTolerance = new Temperature(.01);
					Temperature tolerance = standardTolerance;
					bool previouslyAtThisThreshold = temperatureThreshold == CurrentTemperatureThreshold;

					// If previously at this threshold, then set "wider" tolerance for this threshold to reduce fluctuations
					if (previouslyAtThisThreshold)
					{
						tolerance = temperatureThreshold.Tolerance;
					}

					if (Temperature.AreEqualWithinTolerance(_temperature, temperatureThreshold.Temperature, tolerance))
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
				stringBuilder.Append(temperatureThreshold);
			}

			return stringBuilder.ToString();
	    }

		protected virtual void OnTemperatureThresholdReached(TemperatureThresholdEventArgs eventArgs)
	    {
		    TemperatureThresholdReached?.Invoke(this, eventArgs);
	    }
	}
}
