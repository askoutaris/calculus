using Newtonsoft.Json;

namespace Calculus.Data
{
	public class TextData : IData
	{
		public string Value { get; private set; }

		public TextData(string value)
		{
			Value = value;
		}

		public override string ToString() => JsonConvert.SerializeObject(Value);
	}
}
