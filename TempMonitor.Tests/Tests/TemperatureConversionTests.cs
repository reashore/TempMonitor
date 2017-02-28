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
			const bool isFahrenheit = true;
			Thermometer thermometer = new Thermometer(isFahrenheit);
			thermometer.SetTemperatureThresholds(temperatureThresholdList);
			thermometer.Temperature = 40;

			// Act
			thermometer.Temperature = 32;

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);
			const string expectedThresholdName = "Freezing";
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);
		}

		[Fact]
		public void ConvertFahrenheitToCelsiousTest()
		{
			// Act
			double celsius = Thermometer.ConvertFahrenheitToCelsius(32);

			// Assert
			Assert.Equal(0.0, celsius, 2);

			// Act
			celsius = Thermometer.ConvertFahrenheitToCelsius(212);

			// Assert
			Assert.Equal(100.0, celsius, 2);
		}

		[Fact]
		public void ConvertCelsiousToFahrenheitTest()
		{
			// Act
			double fahrenheit = Thermometer.ConvertCelsiusToFahrenheit(0);

			// Assert
			Assert.Equal(32.0, fahrenheit, 2);

			// Act
			fahrenheit = Thermometer.ConvertCelsiusToFahrenheit(100);

			// Assert
			Assert.Equal(212.0, fahrenheit, 2);
		}
	}
}
