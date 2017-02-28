using System.Collections.Generic;
using TempMonitor.Domain;
using Xunit;

namespace TempMonitor.Tests.Tests
{
	public class RemainsAtThresholdTests
	{
		[Fact]
		public void RemainsAtDecreasingThresholdWithDecreasingTemperatureTest()
		{
			// Arrange
			Thermometer thermometer = new Thermometer();
			List<TemperatureThreshold> temperatureThresholdList = TestUtils.CreateTemperatureThresholds(TemperatureDirection.Decreasing);
			thermometer.SetTemperatureThresholds(temperatureThresholdList);
			thermometer.Temperature = 2;

			// Act
			thermometer.Temperature = 0;

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);

			thermometer.Temperature = -.1;

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);
		}

		[Fact]
		public void RemainsAtIncreasingThresholdWithIncreasingTemperatureTest()
		{
			// Arrange
			Thermometer thermometer = new Thermometer();
			List<TemperatureThreshold> temperatureThresholdList = TestUtils.CreateTemperatureThresholds(TemperatureDirection.Increasing);
			thermometer.SetTemperatureThresholds(temperatureThresholdList);
			thermometer.Temperature = -2;

			// Act
			thermometer.Temperature = 0;

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);

			thermometer.Temperature = .1;

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);
		}
	}
}
