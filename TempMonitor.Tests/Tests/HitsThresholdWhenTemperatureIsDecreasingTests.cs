using System.Collections.Generic;
using TempMonitor.Domain;
using Xunit;

namespace TempMonitor.Tests.Tests
{
	// Obsolete
	public class HitsThresholdWhenTemperatureIsDecreasingTests
	{
		[Fact]
		public void HitsFreezingThresholdWhenTemperatureIsDeceasingTest()
		{
			// Arrange
			Thermometer thermometer = new Thermometer();
			List<TemperatureThreshold> temperatureThresholdList = TestUtils.CreateTemperatureThresholds(TemperatureDirection.Decreasing);
			thermometer.SetTemperatureThresholds(temperatureThresholdList);
			// Set previous temperature so that temperature change direction is decreasing
			thermometer.Temperature = 2;

			// Act
			thermometer.Temperature = 0;

			// Assert
			Assert.True(thermometer.FireTemperatureThreshold);
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
			// Set previous temperature so that temperature change direction is decreasing
			thermometer.Temperature = 22;

			// Act
			thermometer.Temperature = 20;

			// Assert
			Assert.True(thermometer.FireTemperatureThreshold);
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
			// Set previous temperature so that temperature change direction is decreasing
			thermometer.Temperature = 102;

			// Act
			thermometer.Temperature = 100;

			// Assert
			Assert.True(thermometer.FireTemperatureThreshold);
			const string expectedThresholdName = "Boiling";
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);
		}
	}
}