using System;
using System.Collections.Generic;

namespace Calculus.Spreadsheets
{
	class CalculationBindings
	{
		private readonly Dictionary<Guid, Binding> _bindings;
		public readonly Guid CalculationId;

		public CalculationBindings(Guid calculationId)
		{
			CalculationId = calculationId;
			_bindings = new Dictionary<Guid, Binding>();
		}

		public void Add(Binding binding)
		{
			_bindings.Add(binding.TargetId, binding);
		}

		public bool TryGet(Guid targetId, out Binding binding)
		{
			return _bindings.TryGetValue(targetId, out binding);
		}
	}
}
