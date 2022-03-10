using System;
using Calculus.Data;

namespace Calculus.Functions
{
	public class RangeFn : Function
	{
		private IDataProducer? _from;
		private IDataProducer? _to;
		private IDataProducer? _step;

		public RangeFn(Guid id) : base(id)
		{
		}

		public RangeFn() : base(Guid.NewGuid())
		{
		}

		public void SetParameters(IDataProducer fromProducer, IDataProducer toProducer, IDataProducer stepProducer)
		{
			_from = fromProducer;
			_to = toProducer;
			_step = stepProducer;
		}

		public override IData GetValue()
		{
			decimal from = (NumericData)(_from?.GetValue() ?? throw new Exception($"Function {Id} Parameters have not been set yet"));
			decimal to = (NumericData)(_to?.GetValue() ?? throw new Exception($"Function {Id} Parameters have not been set yet"));
			decimal step = (NumericData)(_step?.GetValue() ?? throw new Exception($"Function {Id} Parameters have not been set yet"));

			int pos = 0, size = (int)((to - from) / step);
			NumericData[] range = new NumericData[size + 1];
			for (decimal i = from; i <= to; i += step)
				range[pos++] = new NumericData(i);

			return new RangeData(range);
		}
	}
}
