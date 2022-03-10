using System;
using Calculus.Data;

namespace Calculus.Cells
{
	public class ReferenceCell : Cell
	{
		public IDataProducer? DataProducer { get; private set; }
		public override IData GetValue() => DataProducer?.GetValue() ?? throw new Exception($"Cell {Id} has no value set");
		public void SetDataProducer(IDataProducer dataProducer) => DataProducer = dataProducer;

		public ReferenceCell(Guid id) : base(id)
		{

		}

		public ReferenceCell() : base(Guid.NewGuid())
		{

		}
	}
}
