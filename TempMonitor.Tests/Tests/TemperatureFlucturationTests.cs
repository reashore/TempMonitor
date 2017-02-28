using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempMonitor.Domain;
using Xunit;

namespace TempMonitor.Tests.Tests
{
	public class TemperatureFlucturationTests
	{
		[Fact]
		public void TemperatureFlucturationTest()
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
			Assert.True(thermometer.IsAtTemperatureThreshold);
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);

			// Act
			thermometer.Temperature = -0.1;

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);

			// Act
			thermometer.Temperature = +0.1;

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);
		}

	}
}
