using System.Collections.Generic;
using TempMonitor.Domain;
using Xunit;

namespace TempMonitor.Tests.Tests
{
	public class TemperatureThresholdEventHandlerTests
	{
		private bool _eventHandlerFired;
		private string _name;
		private double _temperature;
		private double _tolerance;
		private TemperatureDirection _direction;
		private int _eventHandlerCalledCount;

		[Fact]
		public void TemperatureThresholdEventHandlerFiresTest()
		{
			// Arrange
			Thermometer thermometer = new Thermometer();
			List<TemperatureThreshold> temperatureThresholdList = TestUtils.CreateTemperatureThresholds(TemperatureDirection.Decreasing);
			thermometer.SetTemperatureThresholds(temperatureThresholdList);
			thermometer.TemperatureThresholdReached += HandleTemperatureThresholdReached;
			thermometer.Temperature = 2;
			_eventHandlerFired = false;

			// Act
			thermometer.Temperature = 0;

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);
			const string expectedThresholdName = "Freezing";
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);

			Assert.True(_eventHandlerFired);
			Assert.Equal(expectedThresholdName, _name);
			Assert.Equal(0, _temperature, 3);
			Assert.Equal(.5, _tolerance, 3);
			Assert.Equal(TemperatureDirection.Decreasing, _direction);
		}

		[Fact]
		public void TemperatureThresholdEventHandlerFiresOnceAtThresholdTest()
		{
			// Arrange
			Thermometer thermometer = new Thermometer();
			List<TemperatureThreshold> temperatureThresholdList = TestUtils.CreateTemperatureThresholds(TemperatureDirection.Decreasing);
			thermometer.SetTemperatureThresholds(temperatureThresholdList);
			thermometer.TemperatureThresholdReached += HandleTemperatureThresholdReached;
			thermometer.Temperature = 2;
			_eventHandlerCalledCount = 0;

			// Act
			thermometer.Temperature = 0;

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);

			// Act
			thermometer.Temperature = -0.1;

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);

			// Act
			thermometer.Temperature = -0.2;

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);

			// Act
			thermometer.Temperature = -0.3;

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);

			Assert.Equal(1, _eventHandlerCalledCount);
		}

		private void HandleTemperatureThresholdReached(object sender, TemperatureThresholdEventArgs e)
		{
			TemperatureThreshold temperatureThreshold = e.TemperatureThreshold;
			_name = temperatureThreshold.Name;
			_temperature = temperatureThreshold.Temperature;
			_tolerance = temperatureThreshold.Tolerance;
			_direction = temperatureThreshold.Direction;
			
			_eventHandlerFired = true;
			_eventHandlerCalledCount++;
		}
	}
}
