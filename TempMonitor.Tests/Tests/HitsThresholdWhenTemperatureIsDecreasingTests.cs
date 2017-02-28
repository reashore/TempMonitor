using System.Collections.Generic;
using TempMonitor.Domain;
using Xunit;

namespace TempMonitor.Tests.Tests
{
	public class HitsThresholdWhenTemperatureIsDecreasingTests
	{
		[Fact]
		public void HitsFreezingThresholdWhenTemperatureIsDeceasingTest()
		{
			// Arrange
			Thermometer thermometer = new Thermometer();
			List<TemperatureThreshold> temperatureThresholdList = TestUtils.CreateTemperatureThresholds(TemperatureDirection.Decreasing);
			thermometer.SetTemperatureThresholds(temperatureThresholdList);
			thermometer.Temperature = 2;

			// Act
			thermometer.Temperature = 0;		// decreasing temperature

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);
			const string expectedThresholdName = "Freezing";
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);
		}

		[Fact]
		public void HitsRoomTemperatureThresholdWhenTemperatureIsDeceasingTest()
		{
			// Arrange
			Thermometer thermometer = new Thermometer();
			List<TemperatureThreshold> temperatureThresholdList = TestUtils.CreateTemperatureThresholds(TemperatureDirection.Decreasing);
			thermometer.SetTemperatureThresholds(temperatureThresholdList);
			thermometer.Temperature = 22;

			// Act
			thermometer.Temperature = 20;		// decreasing temperature

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);
			const string expectedThresholdName = "Room Temperature";
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);
		}

		[Fact]
		public void HitsBoilingThresholdWhenTemperatureIsDeceasingTest()
		{
			// Arrange
			Thermometer thermometer = new Thermometer();
			List<TemperatureThreshold> temperatureThresholdList = TestUtils.CreateTemperatureThresholds(TemperatureDirection.Decreasing);
			thermometer.SetTemperatureThresholds(temperatureThresholdList);
			thermometer.Temperature = 102;

			// Act
			thermometer.Temperature = 100;		// decreasing temperature

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);
			const string expectedThresholdName = "Boiling";
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);
		}
	}
}