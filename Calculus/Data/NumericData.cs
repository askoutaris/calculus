using Newtonsoft.Json;

namespace Calculus.Data
{
	public class NumericData : IData
	{
		public decimal Value { get; private set; }

		public NumericData(decimal value)
		{
			Value = value;
		}

		public static implicit operator decimal(NumericData num) => num.Value;
		public static implicit operator NumericData(decimal num) => new NumericData(num);

		public override string ToString() => JsonConvert.SerializeObject(Value);
	}
}
