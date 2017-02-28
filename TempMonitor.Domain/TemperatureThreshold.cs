
namespace TempMonitor.Domain
{
	public enum TemperatureDirection
	{
		Increasing,
		Decreasing
	}

	public class TemperatureThreshold
	{
		public TemperatureThreshold()
		{
		}

		public TemperatureThreshold(string name, double temperature, double tolerance, TemperatureDirection direction)
		{
			Name = name;
			Temperature = temperature;
			Tolerance = tolerance;
			Direction = direction;
		}

		public string Name { get; set; }
		public double Temperature { get; set; }
		public double Tolerance { get; set; }
		public TemperatureDirection Direction { get; set; }
	}
}