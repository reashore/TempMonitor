using Xunit;
using TempMonitor.Domain;

namespace TempMonitor.Tests.Tests
{
	public class TemperatureTests
	{
		[Fact]
		public void CompareNonNullTemperaturesTest()
		{
			// Arrange
			Temperature freezingCelsius = new Temperature(0);
			Temperature freezingFahrenheit = new Temperature(32, TemperatureType.Fahrenheit);

			// Assert
			Assert.True(freezingCelsius == freezingFahrenheit);

			// Arrange
			Temperature boilingCelsius = new Temperature(100);
			Temperature boilingFahrenheit = new Temperature(212, TemperatureType.Fahrenheit);

			// Assert
			Assert.True(boilingCelsius == boilingFahrenheit);
		}

		[Fact]
		public void CompareNonNullTemperatureWithNullTemperaturesTest()
		{
			// Arrange
			Temperature freezingCelsius = new Temperature(0);
			Temperature nullTemperature = null;

			// Assert
			Assert.True(freezingCelsius != nullTemperature);
		}

		[Fact]
		public void CompareNullTemperatureWithNonNullTemperaturesTest()
		{
			// Arrange
			Temperature freezingCelsius = new Temperature(0);
			Temperature nullTemperature = null;

			// Assert
			Assert.True(nullTemperature != freezingCelsius);
		}

		[Fact]
		public void CompareTwoNullTemperaturesTest()
		{
			// Arrange
			Temperature nullTemperature = null;

			// Assert
			Assert.True(nullTemperature == (Temperature) null);
		}

		//-------------------------------------------------

		[Fact]
		public void CompareNonNullTemperaturesWithEqualTest()
		{
			// Arrange
			Temperature freezingCelsius = new Temperature(0);
			Temperature freezingFahrenheit = new Temperature(32, TemperatureType.Fahrenheit);

			// Assert
			Assert.True(freezingCelsius.Equals(freezingFahrenheit));

			// Arrange
			Temperature boilingCelsius = new Temperature(100);
			Temperature boilingFahrenheit = new Temperature(212, TemperatureType.Fahrenheit);

			// Assert
			Assert.True(boilingCelsius.Equals(boilingFahrenheit));
		}

		[Fact]
		public void CompareNonNullTemperatureWithNullTemperaturesWithEqualTest()
		{
			// Arrange
			Temperature freezingCelsius = new Temperature(0);

			// Assert
			Assert.False(freezingCelsius.Equals(null));
		}

		//-------------------------------------------------

		[Fact]
		public void CompareNonNullTemperaturesWithCustomToleranceTest()
		{
			// Arrange
			Temperature temperature1 = new Temperature(10);
			Temperature temperature2 = new Temperature(11);
			Temperature.Tolerance = new Temperature(2);

			// Assert
			Assert.True(temperature1 == temperature2);

			Temperature.ResetToleranceToDefault();

			Assert.False(temperature1 == temperature2);
		}

		//-------------------------------------------------

		[Fact]
		public void CompareTemperaturesWithGreaterThanAndLessThanTest()
		{
			// Arrange
			Temperature temperature1 = new Temperature(10);
			Temperature temperature2 = new Temperature(11);

			// Assert
			Assert.True(temperature1 <= temperature2);

			Temperature.ResetToleranceToDefault();

			Assert.False(temperature1 >= temperature2);
		}
	}
}
