using System;
using Calculus.Data;

namespace Calculus.Cells
{
	public class ValueCell : Cell, IDataInput
	{
		public IData? Value { get; private set; }
		public void SetValue(IData data) => Value = data;
		public override IData GetValue() => Value ?? throw new Exception($"Cell {Id} has no value set");

		public ValueCell(Guid id) : base(id)
		{

		}

		public ValueCell(Guid id, IData data) : base(id)
		{
			Value = data;
		}

		public ValueCell(IData data) : base(Guid.NewGuid())
		{
			Value = data;
		}
	}
}
