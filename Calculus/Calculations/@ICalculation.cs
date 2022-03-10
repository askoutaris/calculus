using System;
using System.Collections.Generic;
using Calculus.Cells;

namespace Calculus.Calculations
{
	public interface ICalculation : IUnit
	{
		ReferenceCell[] GetInputs();
		Dictionary<Guid, IDataProducer> GetOutputs();
		void SetInput(Guid id, IDataProducer dataProducer);
	}
}
