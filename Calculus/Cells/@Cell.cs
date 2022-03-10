using System;
using Calculus.Data;

namespace Calculus.Cells
{
	public abstract class Cell : IDataProducer, IUnit
	{
		public Guid Id { get; }
		public abstract IData GetValue();

		protected Cell(Guid id)
		{
			Id = id;
		}
	}
}
