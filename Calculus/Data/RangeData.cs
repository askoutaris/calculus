using Newtonsoft.Json;

namespace Calculus.Data
{
	public class RangeData : IData
	{
		public IData[] Value { get; private set; }

		public RangeData(IData[] range)
		{
			Value = range;
		}

		public static implicit operator IData[](RangeData range) => range.Value;

		public override string ToString() => JsonConvert.SerializeObject(Value);
	}
}
