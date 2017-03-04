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
			thermometer.Temperature = new Temperature(2);
			const string expectedThresholdName = "Freezing";

			// Act
			thermometer.Temperature = new Temperature(0);			// decreasing temperature

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);

			// Act
			thermometer.Temperature = new Temperature(-0.1);		// decreasing temperature

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);

			// Act
			thermometer.Temperature = new Temperature(-0.2);		// decreasing temperature

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);

			// Act
			thermometer.Temperature = new Temperature(-0.3);		// decreasing temperature

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);
		}

		[Fact]
		public void TemperatureFlucturationWithIncreasingTemperaturesTest()
		{
			// Arrange
			List<TemperatureThreshold> temperatureThresholdList = TestUtils.CreateTemperatureThresholds(TemperatureDirection.Increasing);
			Thermometer thermometer = new Thermometer();
			thermometer.SetTemperatureThresholds(temperatureThresholdList);
			thermometer.Temperature = new Temperature(-2);
			const string expectedThresholdName = "Freezing";

			// Act
			thermometer.Temperature = new Temperature(0);		// increasing temperature

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);

			// Act
			thermometer.Temperature = new Temperature(0.1);      // increasing temperature

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);

			// Act
			thermometer.Temperature = new Temperature(0.2);      // increasing temperature

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);

			// Act
			thermometer.Temperature = new Temperature(0.3);      // increasing temperature

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);
		}

		[Fact]
		public void TemperatureFlucturationWithIncreasingAndDecreasingTemperaturesTest()
		{
			// Arrange
			List<TemperatureThreshold> temperatureThresholdList = TestUtils.CreateTemperatureThresholds(TemperatureDirection.Decreasing);
			Thermometer thermometer = new Thermometer();
			thermometer.SetTemperatureThresholds(temperatureThresholdList);
			thermometer.Temperature = new Temperature(2.0);
			const string expectedThresholdName = "Freezing";

			// Act
			thermometer.Temperature = new Temperature(0.0);			// decreasing temperature

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);

			// Act
			thermometer.Temperature = new Temperature(-0.1);		// decreasing temperature

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);

			// Act
			thermometer.Temperature = new Temperature(0.0);			// increasing temperature

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);

			// Act
			thermometer.Temperature = new Temperature(-0.1);		// decreasing temperature

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);

			// Act
			thermometer.Temperature = new Temperature(0.2);			// increasing temperature

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);
		}
	}
}
