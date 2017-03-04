using System.Collections.Generic;
using TempMonitor.Domain;
using Xunit;

namespace TempMonitor.Tests.Tests
{
	public class HitTemperatureThresholdTests
	{
		// The temperature can be increasing (for example, -2C -> 0C) or decreasing (for example, 2C -> 0C)
		// The temperature threshold can be TemperatureDirection.Increasing or TemperatureDirection.Decreasing
		// Therefore, there are 4 combinations of test cases as in the following table:
		//
		//    ThresholdDirection  TemperatureDirection    	 TestResult
		//       increasing             increasing			hits threshold
		//       increasing             dereasing			misses threshold
		//       decreasing				increasing			misses threshold
		//       decreasing				decreasing			hits threshold


		// todo fix this
		//[Theory]
		//[InlineData("Freezing", TemperatureDirection.Increasing, new Temperature(-2), 0, true)]      //  increasing threshold, increasing temperature
		//[InlineData("Freezing", TemperatureDirection.Increasing, 2, 0, false)]      //  increasing threshold, decreasing temperature
		//[InlineData("Freezing", TemperatureDirection.Decreasing, -2, 0, false)]     //  decreasing threshold, increasing temperature
		//[InlineData("Freezing", TemperatureDirection.Decreasing,  2, 0, true)]		//  decreasing threshold, decreasing temperature
		//public void HitsTemperatureThresholdTest(string thresholdName, TemperatureDirection temperatureDirection, Temperature previousTemperature, Temperature thresholdTemperature, bool hitsTemperatureThreshold)
		//{
		//	// Arrange
		//	List<TemperatureThreshold> temperatureThresholdList = new List<TemperatureThreshold>
		//	{
		//		new TemperatureThreshold
		//		{
		//			Name = thresholdName,
		//			Temperature = thresholdTemperature,
		//			Tolerance =  new Temperature(0.5),
		//			Direction = temperatureDirection
		//		}
		//	};

		//	Thermometer thermometer = new Thermometer();
		//	thermometer.SetTemperatureThresholds(temperatureThresholdList);
		//	thermometer.Temperature = previousTemperature;

		//	// Act
		//	thermometer.Temperature = thresholdTemperature;

		//	// Assert
		//	Assert.Equal(hitsTemperatureThreshold, thermometer.IsAtTemperatureThreshold);
		//	if (thermometer.IsAtTemperatureThreshold)
		//	{
		//		string expectedThresholdName = thresholdName;
		//		Assert.Equal(expectedThresholdName, thermometer.CurrentTemperatureThreshold.Name);
		//	}
		//}
	}
}
