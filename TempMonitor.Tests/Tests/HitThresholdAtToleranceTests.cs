using System.Collections.Generic;
using TempMonitor.Domain;
using Xunit;

namespace TempMonitor.Tests.Tests
{
	public class HitThresholdAtToleranceTests
	{
		[Fact]
		public void HitsFreezingThresholdWhenTemperatureIsDeceasingTest()
		{
			// Arrange
			Thermometer thermometer = new Thermometer();
			List<TemperatureThreshold> temperatureThresholdList = TestUtils.CreateTemperatureThresholds(TemperatureDirection.Decreasing);
			thermometer.SetTemperatureThresholds(temperatureThresholdList);
			thermometer.Temperature = new Temperature(2);
			const string expectedThresholdName = "Freezing";

			// Act
			thermometer.Temperature = new Temperature(.5);			

			// Assert
			Assert.False(thermometer.IsAtTemperatureThreshold);

			// Act
			thermometer.Temperature = new Temperature(.4);			

			// Assert
			Assert.False(thermometer.IsAtTemperatureThreshold);

			// Act
			thermometer.Temperature = new Temperature(0);			

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);

			// Act
			thermometer.Temperature = new Temperature(-.4);			

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);

			// Act
			thermometer.Temperature = new Temperature(+.4);			
							
			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);

			// Act
			thermometer.Temperature = new Temperature(-.5);       

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);

			// Act
			thermometer.Temperature = new Temperature(+.5);      

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);
		}

		[Fact]
		public void HitsFreezingThresholdWhenTemperatureIsIncreasingTest()
		{
			// Arrange
			Thermometer thermometer = new Thermometer();
			List<TemperatureThreshold> temperatureThresholdList = TestUtils.CreateTemperatureThresholds(TemperatureDirection.Increasing);
			thermometer.SetTemperatureThresholds(temperatureThresholdList);
			thermometer.Temperature = new Temperature(-2);
			const string expectedThresholdName = "Freezing";

			// Act
			thermometer.Temperature = new Temperature(-.5);			

			// Assert
			Assert.False(thermometer.IsAtTemperatureThreshold);

			// Act
			thermometer.Temperature = new Temperature(-.4);         

			// Assert
			Assert.False(thermometer.IsAtTemperatureThreshold);

			// Act
			thermometer.Temperature = new Temperature(0.0);        

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);

			// Act
			thermometer.Temperature = new Temperature(.4);          

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);

			// Act
			thermometer.Temperature = new Temperature(-.4);         

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);

			// Act
			thermometer.Temperature = new Temperature(+.5);       

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);

			// Act
			thermometer.Temperature = new Temperature(-.5);       

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);
		}

		[Fact]
		public void HitsFreezingThresholdWhenTemperatureIsIncreasingAndDecreasingTest()
		{
			// Arrange
			Thermometer thermometer = new Thermometer();
			List<TemperatureThreshold> temperatureThresholdList = TestUtils.CreateTemperatureThresholds(TemperatureDirection.Decreasing);
			thermometer.SetTemperatureThresholds(temperatureThresholdList);
			thermometer.Temperature = new Temperature(2);
			const string expectedThresholdName = "Freezing";

			// Act
			thermometer.Temperature = new Temperature(.5);			

			// Assert
			Assert.False(thermometer.IsAtTemperatureThreshold);

			// Act
			thermometer.Temperature = new Temperature(0);         

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);

			// Act
			thermometer.Temperature = new Temperature(0.6);        

			// Assert
			Assert.False(thermometer.IsAtTemperatureThreshold);

			// Act
			thermometer.Temperature = new Temperature(0);          

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);

			// Act
			thermometer.Temperature = new Temperature(-.6);         

			// Assert
			Assert.False(thermometer.IsAtTemperatureThreshold);

			// Act
			thermometer.Temperature = new Temperature(.6);       

			// Assert
			Assert.False(thermometer.IsAtTemperatureThreshold);

			// Act
			thermometer.Temperature = new Temperature(0);       

			// Assert
			Assert.True(thermometer.IsAtTemperatureThreshold);
			Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);
		}
	}
}
