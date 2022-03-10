using System;
using Calculus.Data;

namespace Calculus.Functions
{
	public abstract class Function : IDataProducer, IUnit
	{
		public Guid Id { get; }
		public abstract IData GetValue();

		protected Function(Guid id)
		{
			Id = id;
		}
	}
}
