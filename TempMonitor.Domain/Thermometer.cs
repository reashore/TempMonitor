using System;
using System.Collections.Generic;
using System.Text;

namespace TempMonitor.Domain
{
	public class Thermometer : IThermometer
	{
		private Temperature _temperature;
		private Temperature _previousTemperature;
		private bool _eventHandlerFiredForTemperatureThreshold;
		private readonly List<TemperatureThreshold> _temperatureThresholdList = new List<TemperatureThreshold>();

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
					Temperature.ResetToleranceToDefault();
					bool previouslyAtThisThreshold = temperatureThreshold == CurrentTemperatureThreshold;

					// If previously at this threshold, then set "wider" tolerance for this threshold to reduce fluctuations
					if (previouslyAtThisThreshold)
					{
						Temperature.Tolerance = temperatureThreshold.Tolerance;
					}

					if (PassedThroughTemperatureThreshold(temperatureThreshold))
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
		//public bool IsAtTemperatureThreshold
		//{
		//	get
		//	{
		//		foreach (TemperatureThreshold temperatureThreshold in _temperatureThresholdList)
		//		{
		//			Temperature.ResetToleranceToDefault();
		//			bool previouslyAtThisThreshold = temperatureThreshold == CurrentTemperatureThreshold;

		//			// If previously at this threshold, then set "wider" tolerance for this threshold to reduce fluctuations
		//			if (previouslyAtThisThreshold)
		//			{
		//				Temperature.Tolerance = temperatureThreshold.Tolerance;
		//			}

		//			if (_temperature == temperatureThreshold.Temperature)
		//			{
		//				if (previouslyAtThisThreshold)
		//				{
		//					_eventHandlerFiredForTemperatureThreshold = true;
		//					return true;
		//				}

		//				TemperatureDirection temperatureDirection = _temperature >= _previousTemperature ? TemperatureDirection.Increasing : TemperatureDirection.Decreasing;
		//				bool isSameTemperatureDirection = temperatureDirection == temperatureThreshold.Direction;

		//				if (isSameTemperatureDirection)
		//				{
		//					CurrentTemperatureThreshold = temperatureThreshold;
		//					_eventHandlerFiredForTemperatureThreshold = false;

		//				}

		//				return isSameTemperatureDirection;
		//			}
		//		}

		//		return false;
		//	}
		//}

		public bool PassedThroughTemperatureThreshold(TemperatureThreshold temperatureThreshold)
		{
			Temperature thresholdTemperature = temperatureThreshold.Temperature;
			TemperatureDirection thresholdDirection = temperatureThreshold.Direction;

			bool passedThroughThreshold = _previousTemperature <= thresholdTemperature && thresholdTemperature <= _temperature;
			bool rightDirection = _previousTemperature <= _temperature && thresholdDirection == TemperatureDirection.Increasing;

			return passedThroughThreshold && rightDirection;
		}

		protected virtual void OnTemperatureThresholdReached(TemperatureThresholdEventArgs eventArgs)
	    {
		    TemperatureThresholdReached?.Invoke(this, eventArgs);
		}
		public override string ToString()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.AppendLine("Configured temperature thresholds:");

			foreach (TemperatureThreshold temperatureThreshold in _temperatureThresholdList)
			{
				stringBuilder.Append(temperatureThreshold);
			}

			return stringBuilder.ToString();
		}
	}
}
