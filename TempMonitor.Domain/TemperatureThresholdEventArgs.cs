
using System;

namespace TempMonitor.Domain
{
	public class TemperatureThresholdEventArgs : EventArgs
	{
		public TemperatureThreshold TemperatureThreshold;

		public TemperatureThresholdEventArgs(TemperatureThreshold temperatureThreshold)
		{
			TemperatureThreshold = temperatureThreshold;
		}
	}
}