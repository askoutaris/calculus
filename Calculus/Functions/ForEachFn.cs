using System;
using System.Linq;
using Calculus.Calculations;
using Calculus.Cells;
using Calculus.Data;

namespace Calculus.Functions
{
	public class ForEachFn : Function
	{
		private IDataProducer? _range;
		private ICalculation? _calculation;

		public ForEachFn(Guid id) : base(id)
		{
		}

		public ForEachFn() : base(Guid.NewGuid())
		{
		}

		public void SetParameters(IDataProducer rangeProducer, ICalculation calculation)
		{
			_range = rangeProducer;
			_calculation = calculation;
		}

		public override IData GetValue()
		{
			var input = _calculation?.GetInputs().Single() ?? throw new Exception($"Function {Id} Parameters have not been set yet");
			var outputProducer = _calculation.GetOutputs().Single().Value;

			IData[] range = (RangeData)(_range?.GetValue() ?? throw new Exception($"Function {Id} Parameters have not been set yet"));
			IData[] outputs = new IData[range.Length];
			for (int i = 0; i < range.Length; i++)
			{
				var cell = new ValueCell(id: Guid.NewGuid(), data: range[i]);

				_calculation.SetInput(input.Id, cell);

				outputs[i] = outputProducer.GetValue();
			}

			return new RangeData(outputs);
		}
	}
}
