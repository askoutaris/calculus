using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Calculus.Cells;
using Calculus.Functions;

namespace Calculus.Calculations
{
	public class Calculation : ICalculation
	{
		public Guid Id { get; }
		private readonly Dictionary<Guid, Binding> _bindings;
		private readonly Dictionary<Guid, IUnit> _units;
		private Dictionary<Guid, IDataProducer> _outputs;

		public Calculation()
		{
			Id = Guid.NewGuid();
			_bindings = new Dictionary<Guid, Binding>();
			_units = new Dictionary<Guid, IUnit>();
			_outputs = new Dictionary<Guid, IDataProducer>();
		}

		public void AddUnit(params IUnit[] units)
		{
			foreach (var unit in units)
				_units.Add(unit.Id, unit);
		}

		public void AddBinding(Guid targetId, params Guid[] sourceIds)
		{
			_bindings.Add(targetId, new Binding(targetId, sourceIds));
		}
		public void AddBinding(IUnit target, params IUnit[] sources)
		{
			AddBinding(target.Id, sources.Select(x => x.Id).ToArray());
		}
		public void ApplyBindings()
		{
			BindFunctionParameters();
			SetOutputs();
		}

		public void SetInput(IUnit target, IDataProducer dataProducer)
		{
			var referenceCell = (ReferenceCell)_units[target.Id];
			referenceCell.SetDataProducer(dataProducer);
		}
		public void SetInput(Guid id, IDataProducer dataProducer)
		{
			var referenceCell = (ReferenceCell)_units[id];
			referenceCell.SetDataProducer(dataProducer);
		}

		public ReferenceCell[] GetInputs()
		{
			return _units.Values
				.OfType<ReferenceCell>()
				.ToArray();
		}
		public IDataProducer GetOutput(Guid id)
		{
			return _outputs[id];
		}

		public Dictionary<Guid, IDataProducer> GetOutputs()
		{
			return _outputs.ToDictionary(x => x.Key, x => x.Value);
		}

		private void SetOutputs()
		{
			var functions = GetFunctions();
			_outputs = functions
				.Cast<IDataProducer>()
				.ToDictionary(x => x.Id, x => x);
		}

		private void BindFunctionParameters()
		{
			var functions = GetFunctions();
			foreach (var func in functions)
			{
				if (!_bindings.TryGetValue(func.Id, out Binding binding))
					throw new ArgumentException($"Missing binding for function with Id {func.Id}");

				var parameters = binding.SourceIds
					.Select(id =>
					{
						if (!_units.TryGetValue(id, out IUnit unit))
							throw new ArgumentException($"Missing parameter with id {id} for function with id {func.Id}. You should probably use AddUnit()!");
						else
							return unit;
					})
					.ToArray();

#warning on parameters count mismatch exception, throw param names and pass arguments
				MethodInfo methodInfo = func.GetType().GetMethod("SetParameters");
				methodInfo.Invoke(func, parameters);
			}
		}

		private Function[] GetFunctions()
		{
			return _units.Values
				.OfType<Function>()
				.ToArray();
		}
	}
}
