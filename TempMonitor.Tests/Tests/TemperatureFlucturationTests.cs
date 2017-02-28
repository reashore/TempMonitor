using System.Collections.Generic;
using TempMonitor.Domain;
using Xunit;

namespace TempMonitor.Tests.Tests
{
	public class TemperatureFlucturationTests
	{
		[Fact]
		public void TemperatureFlucturationWithDecreasingTemperaturesTest()
		{
			// Arrange
			List<TemperatureThreshold> temperatureThresholdList = TestUtils.CreateTemperatureThresholds(TemperatureDirection.Decreasing);
			Thermometer thermometer = new Thermometer();
			thermometer.SetTemperatureThresholds(temperatureThresholdList);
			thermometer.Temperature = 2;
			const string expectedThresholdName = "Freezing";

			// Act
			thermometer.Temperature = 0;

			// Assert
			Assert.True(thermometer.FireTemperatureThreshold);
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);

			// Act
			thermometer.Temperature = -0.1;

			// Assert
			Assert.True(thermometer.FireTemperatureThreshold);
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);

			// Act
			thermometer.Temperature = -0.2;

			// Assert
			Assert.True(thermometer.FireTemperatureThreshold);
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);

			// Act
			thermometer.Temperature = -0.3;

			// Assert
			Assert.True(thermometer.FireTemperatureThreshold);
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);
		}

		[Fact]
		public void TemperatureFlucturationWithIncreasingAndDecreasingTemperaturesTest()
		{
			// Arrange
			List<TemperatureThreshold> temperatureThresholdList = TestUtils.CreateTemperatureThresholds(TemperatureDirection.Decreasing);
			Thermometer thermometer = new Thermometer();
			thermometer.SetTemperatureThresholds(temperatureThresholdList);
			thermometer.Temperature = 2.0;
			const string expectedThresholdName = "Freezing";

			// Act
			thermometer.Temperature = 0.0;

			// Assert
			Assert.True(thermometer.FireTemperatureThreshold);
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);

			// Act
			thermometer.Temperature = -0.1;

			// Assert
			Assert.True(thermometer.FireTemperatureThreshold);
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);

			// Act
			thermometer.Temperature = 0.0;

			// Assert
			Assert.True(thermometer.FireTemperatureThreshold);
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);

			// Act
			thermometer.Temperature = -0.1;

			// Assert
			Assert.True(thermometer.FireTemperatureThreshold);
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);
		}

	}
}
