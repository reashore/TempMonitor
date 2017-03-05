﻿using System.Collections.Generic;
using TempMonitor.Domain;
using Xunit;

namespace TempMonitor.Tests.Tests
{
	public class TemperatureThresholdEventHandlerTests
	{
		private string _name;
		private Temperature _temperature;
		private Temperature _tolerance;
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
			thermometer.Temperature = new Temperature(2);
			_eventHandlerCalledCount = 0;

			// Act
			thermometer.Temperature = new Temperature(0);

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);
			const string expectedThresholdName = "Freezing";
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);

			Assert.Equal(_eventHandlerCalledCount, 1);
			Assert.Equal(expectedThresholdName, _name);

			// todo Need value comparison not reference compariosn (or override equality)
			Assert.Equal(new Temperature(0), _temperature);
			Assert.Equal(new Temperature(.5), _tolerance);
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
			thermometer.Temperature = new Temperature(2);
			_eventHandlerCalledCount = 0;

			// Act
			thermometer.Temperature = new Temperature(0);

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);

			// Act
			thermometer.Temperature = new Temperature(-0.1);

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);

			// Act
			thermometer.Temperature = new Temperature(-0.2);

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);

			// Act
			thermometer.Temperature = new Temperature(-0.3);

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
			
			_eventHandlerCalledCount++;
		}
	}
}
