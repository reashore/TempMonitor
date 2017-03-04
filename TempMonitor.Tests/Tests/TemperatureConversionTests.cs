using System.Collections.Generic;
using TempMonitor.Domain;
using Xunit;

namespace TempMonitor.Tests.Tests
{
	public class TemperatureConversionTests
	{
		[Fact]
		public void ConvertFahrenheitTemperatureTest()
		{
			// Arrange
			List<TemperatureThreshold> temperatureThresholdList = TestUtils.CreateTemperatureThresholds(TemperatureDirection.Decreasing);
			Thermometer thermometer = new Thermometer();
			thermometer.SetTemperatureThresholds(temperatureThresholdList);
			thermometer.Temperature = new Temperature(40, TemperatureType.Fahrenheit);

			// Act
			thermometer.Temperature = new Temperature(32, TemperatureType.Fahrenheit);

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);
			const string expectedThresholdName = "Freezing";
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);
		}
	}
}
