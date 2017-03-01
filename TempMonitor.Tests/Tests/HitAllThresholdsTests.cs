using System.Collections.Generic;
using TempMonitor.Domain;
using Xunit;

namespace TempMonitor.Tests.Tests
{
	public class HitAllTemperatureThresholdsTests
	{
		[Fact]
		public void HitAllTemperatureThresholdsDecreasingTest()
		{
			// Arrange
			Thermometer thermometer = new Thermometer();
			List<TemperatureThreshold> temperatureThresholdList = TestUtils.CreateTemperatureThresholds(TemperatureDirection.Decreasing);
			thermometer.SetTemperatureThresholds(temperatureThresholdList);
			thermometer.Temperature = 110;

			// Act
			thermometer.Temperature = 100; // decreasing temperature

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);
			string expectedThresholdName = "Boiling";
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);

			// Act
			thermometer.Temperature = 20; // decreasing temperature

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);
			expectedThresholdName = "Room Temperature";
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);

			// Act
			thermometer.Temperature = 0; // decreasing temperature

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);
			expectedThresholdName = "Freezing";
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);
		}

		[Fact]
		public void HitAllTemperatureThresholdsIncreasingTest()
		{
			// Arrange
			Thermometer thermometer = new Thermometer();
			List<TemperatureThreshold> temperatureThresholdList = TestUtils.CreateTemperatureThresholds(TemperatureDirection.Increasing);
			thermometer.SetTemperatureThresholds(temperatureThresholdList);
			thermometer.Temperature = -10;

			// Act
			thermometer.Temperature = 0; // increasing temperature

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);
			string expectedThresholdName = "Freezing";
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);

			// Act
			thermometer.Temperature = 20; // increasing temperature

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);
			expectedThresholdName = "Room Temperature";
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);

			// Act
			thermometer.Temperature = 100; // increasing temperature

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);
			expectedThresholdName = "Boiling";
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);
		}
	}
}

