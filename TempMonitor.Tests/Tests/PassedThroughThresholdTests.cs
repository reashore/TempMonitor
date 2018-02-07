using System.Collections.Generic;
using TempMonitor.Domain;
using Xunit;

namespace TempMonitor.Tests.Tests
{
	public class PassedThroughThresholdTests
	{
		[Fact]
		public void PassedThroughThresholdWithDreasingTemperatureTest()
		{
			// Arrange
			Thermometer thermometer = new Thermometer();
			List<TemperatureThreshold> temperatureThresholdList = TestUtils.CreateTemperatureThresholds(TemperatureDirection.Decreasing);
			thermometer.SetTemperatureThresholds(temperatureThresholdList);
			thermometer.Temperature = new Temperature(12);

			// Act
			thermometer.Temperature = new Temperature(+10); 

			// Assert
			Assert.False(thermometer.IsAtTemperatureThreshold);

			// Act
			thermometer.Temperature = new Temperature(-10);

			// Assert
			const string expectedThresholdName = "Boiling";
			Assert.True(thermometer.IsAtTemperatureThreshold);
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);
		}

	}
}
