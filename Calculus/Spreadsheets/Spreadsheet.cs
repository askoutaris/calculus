using System;
using System.Collections.Generic;
using System.Linq;
using Calculus.Calculations;

namespace Calculus.Spreadsheets
{
	public class Spreadsheet
	{
		private readonly Dictionary<Guid, CalculationBindings> _calculationBindings;
		private readonly Dictionary<Guid, IUnit> _units;
		private Dictionary<Guid, IDataProducer> _outputs;

		public Spreadsheet()
		{
			_calculationBindings = new Dictionary<Guid, CalculationBindings>();
			_units = new Dictionary<Guid, IUnit>();
			_outputs = new Dictionary<Guid, IDataProducer>();
		}

		public void SetInput(params IDataInput[] inputs)
		{
			foreach (var input in inputs)
				_units[input.Id] = input;
		}

		public void AddCaclulation(params ICalculation[] calculations)
		{
			foreach (var calculation in calculations)
			{
				AddUnit(calculation);

				foreach (var output in calculation.GetOutputs().Values)
					AddUnit(output);
			}
		}

		public void AddBinding(Guid calculationId, Guid targetId, Guid dataProducerId)
		{
			if (!_calculationBindings.TryGetValue(calculationId, out CalculationBindings bindings))
			{
				bindings = new CalculationBindings(calculationId);
				_calculationBindings.Add(calculationId, bindings);
			}

			bindings.Add(new Binding(targetId, dataProducerId));
		}

		public void AddBinding(ICalculation calculation, IUnit target, IDataProducer dataProducer)
		{
			AddBinding(calculation.Id, target.Id, dataProducer.Id);
		}

		public void ApplyBindings()
		{
			BindCalculationInputs();
			SetOutputs();
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
			var calculations = GetCalculations();
			_outputs = calculations
				.SelectMany(x => x.GetOutputs())
				.ToDictionary(x => x.Key, x => x.Value);
		}

		private void BindCalculationInputs()
		{
			var calculations = GetCalculations();
			foreach (var calc in calculations)
			{
				var inputs = calc.GetInputs();
				if (inputs.Length == 0)
					continue;

				if (!_calculationBindings.TryGetValue(calc.Id, out CalculationBindings bindings))
					throw new ArgumentException($"Missing binding for calculation with Id {calc.Id}");

				foreach (var input in inputs)
				{
					if (!bindings.TryGet(input.Id, out Binding binding))
						throw new ArgumentException($"Missing binding for calculation input with CalculationId {calc.Id} and InputId {input.Id}");

					var dataProducer = (IDataProducer)_units[binding.DataProducerId];

					calc.SetInput(input.Id, dataProducer);
				}
			}
		}

		private void AddUnit(IUnit unit)
		{
			_units.Add(unit.Id, unit);
		}

		private Calculation[] GetCalculations()
		{
			return _units.Values
				.OfType<Calculation>()
				.ToArray();
		}
	}
}
