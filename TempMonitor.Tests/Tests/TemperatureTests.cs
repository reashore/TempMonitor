using Xunit;
using TempMonitor.Domain;

namespace TempMonitor.Tests.Tests
{
	public class TemperatureTests
	{
		[Fact]
		public void CompareTemperaturesTest()
		{
			// Arrange
			Temperature tolerance = new Temperature(.0001);
			Temperature freezingCelsius = new Temperature(0);
			Temperature freezingFahrenheit = new Temperature(32, TemperatureType.Fahrenheit);

			// Assert
			Assert.True(Temperature.AreEqualWithinTolerance(freezingCelsius, freezingFahrenheit, tolerance));

			// Arrange
			Temperature boilingCelsius = new Temperature(100);
			Temperature boilingFahrenheit = new Temperature(212, TemperatureType.Fahrenheit);

			// Assert
			Assert.True(Temperature.AreEqualWithinTolerance(boilingCelsius, boilingFahrenheit, tolerance));
		}
	}
}
