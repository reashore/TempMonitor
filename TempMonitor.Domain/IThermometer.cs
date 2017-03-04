using System;
using System.Collections.Generic;

namespace TempMonitor.Domain
{
	public interface IThermometer
	{
		Temperature Temperature { get; set; }

		void SetTemperatureThresholds(List<TemperatureThreshold> temperatureThresholdList);

		bool IsAtTemperatureThreshold { get; }

		TemperatureThreshold CurrentTemperatureThreshold { get; set; }

		event EventHandler<TemperatureThresholdEventArgs> TemperatureThresholdReached;

		string ToString();
	}
}