using System;
using Calculus.Data;

namespace Calculus.Functions
{
	public class AdditionFn : Function
	{
		private IDataProducer? _num1;
		private IDataProducer? _num2;

		public AdditionFn(Guid id) : base(id)
		{
		}

		public AdditionFn() : base(Guid.NewGuid())
		{
		}

		public void SetParameters(IDataProducer num1Producer, IDataProducer num2Producer)
		{
			_num1 = num1Producer;
			_num2 = num2Producer;
		}

		public override IData GetValue()
		{
			decimal num1 = (NumericData)(_num1?.GetValue() ?? throw new Exception($"Function {Id} Parameters have not been set yet"));
			decimal num2 = (NumericData)(_num2?.GetValue() ?? throw new Exception($"Function {Id} Parameters have not been set yet"));

			return new NumericData(num1 + num2);
		}
	}
}
