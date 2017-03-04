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
					Temperature = new Temperature(0),
					Tolerance = new Temperature(0.5),
					Direction = temperatureDirection
				},

				new TemperatureThreshold
				{
					Name = "Room Temperature",
					Temperature = new Temperature(20),
					Tolerance = new Temperature(0.5),
					Direction = temperatureDirection
				},

				new TemperatureThreshold
				{
					Name = "Boiling",
					Temperature = new Temperature(100),
					Tolerance = new Temperature(0.5),
					Direction = temperatureDirection
				}
			};

			return temperatureThresholdList;
		}
	}
}
