
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

		public TemperatureThreshold(string name, Temperature temperature, Temperature tolerance, TemperatureDirection direction)
		{
			Name = name;
			Temperature = temperature;
			Tolerance = tolerance;
			Direction = direction;
		}

		public string Name { get; set; }
		public Temperature Temperature { get; set; }
		public Temperature Tolerance { get; set; }
		public TemperatureDirection Direction { get; set; }

		public override string ToString()
		{
			string name = Name.PadRight(20);
			string temperature = Temperature.ToString().PadLeft(8);
			string tolerance = Tolerance.ToString().PadLeft(6);
			string direction = Direction.ToString();

			return $"{name}: Temperature = {temperature}, Tolerance = {tolerance}, Direction = {direction}\n";
		}
	}
}