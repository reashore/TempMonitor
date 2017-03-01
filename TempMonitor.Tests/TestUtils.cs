using System.Collections.Generic;
using TempMonitor.Domain;

namespace TempMonitor.Tests
{
	public static class TestUtils
	{
		public static List<TemperatureThreshold> CreateTemperatureThresholds(TemperatureDirection temperatureDirection)
		{
			List<TemperatureThreshold> temperatureThresholdList = new List<TemperatureThreshold>
			{
				new TemperatureThreshold
				{
					Name = "Freezing",
					Temperature = 0,
					Tolerance = 0.5,
					Direction = temperatureDirection
				},

				new TemperatureThreshold
				{
					Name = "Room Temperature",
					Temperature = 20,
					Tolerance = 0.5,
					Direction = temperatureDirection
				},

				new TemperatureThreshold
				{
					Name = "Boiling",
					Temperature = 100,
					Tolerance = 0.5,
					Direction = temperatureDirection
				}
			};

			return temperatureThresholdList;
		}
	}
}
