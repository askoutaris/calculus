using System;
using Calculus.Calculations;
using Calculus.Cells;
using Calculus.Data;
using Calculus.Functions;
using Calculus.Spreadsheets;

namespace Workbench
{
	class Program
	{
		static void Main(string[] args)
		{
			var cell1 = new ValueCell((NumericData)1);
			var cell2 = new ValueCell((NumericData)1);
			var cell3 = new ValueCell((NumericData)10);
			var cellsAddFn = new AdditionFn();
			var finalAddFn = new AdditionFn();

			var calculation = new Calculation();
			calculation.AddUnit(cell1, cell2, cell3, cellsAddFn, finalAddFn);
			calculation.AddBinding(cellsAddFn.Id, cell1.Id, cell2.Id);
			calculation.AddBinding(finalAddFn.Id, cellsAddFn.Id, cell3.Id);
			calculation.ApplyBindings();

			var output = calculation.GetOutput(finalAddFn.Id);
			var data = output.GetValue();

			Case3();


			Console.WriteLine("Hello World!");
		}

		private static void Case3()
		{
			var cellRef1 = new ReferenceCell();
			var cellRef2 = new ReferenceCell();
			var cellRefRangeFrom = new ReferenceCell();
			var cellRefRangeTo = new ReferenceCell();
			var additionFunc = new AdditionFn();
			var rangeFunc = new RangeFn();
			var multiplicationFunc = new MultiplicationFn();
			var foreachFunc = new ForEachFn();
			var cellRefMultiplicationInput = new ReferenceCell();

			var additionCalculation = new Calculation();
			additionCalculation.AddUnit(cellRef1, cellRef2, additionFunc);
			additionCalculation.AddBinding(additionFunc, cellRef1, cellRef2);
			additionCalculation.ApplyBindings();

			var multiplicationCalculation = new Calculation();
			var cellMultiplier = new ValueCell((NumericData)100);
			multiplicationCalculation.AddUnit(cellRefMultiplicationInput, cellMultiplier);
			multiplicationCalculation.AddUnit(multiplicationFunc);
			multiplicationCalculation.AddBinding(multiplicationFunc, cellRefMultiplicationInput, cellMultiplier);
			multiplicationCalculation.ApplyBindings();

			var rangeCalculation = new Calculation();
			var cellStep = new ValueCell((NumericData)1);
			rangeCalculation.AddUnit(cellRefRangeFrom, cellRefRangeTo, cellStep);
			rangeCalculation.AddUnit(rangeFunc, foreachFunc);
			rangeCalculation.AddUnit(multiplicationCalculation);
			rangeCalculation.AddBinding(rangeFunc, cellRefRangeFrom, cellRefRangeTo, cellStep);
			rangeCalculation.AddBinding(foreachFunc, rangeFunc, multiplicationCalculation);
			rangeCalculation.ApplyBindings();

			var cell1 = new ValueCell((NumericData)1);
			var cell2 = new ValueCell((NumericData)1);
			var cellRangeTo = new ValueCell((NumericData)10);

			var spreadsheet = new Spreadsheet();
			spreadsheet.SetInput(cell1, cell2, cellRangeTo);
			spreadsheet.AddCaclulation(additionCalculation, rangeCalculation);
			spreadsheet.AddBinding(additionCalculation, cellRef1, cell1);
			spreadsheet.AddBinding(additionCalculation, cellRef2, cell2);
			spreadsheet.AddBinding(rangeCalculation, cellRefRangeFrom, additionFunc);
			spreadsheet.AddBinding(rangeCalculation, cellRefRangeTo, cellRangeTo);
			spreadsheet.ApplyBindings();

			var sumOut = spreadsheet.GetOutput(additionFunc.Id).GetValue();
			var rangeOut = spreadsheet.GetOutput(rangeFunc.Id).GetValue();
			var rangeMultiOut = spreadsheet.GetOutput(foreachFunc.Id).GetValue();
		}

		//private static void Case2()
		//{
		//	var cell1 = new ValueCell { Id = NewId, Value = new Number(5) };
		//	var cell2 = new ValueCell { Id = NewId, Value = new Number(10) };
		//	var cell3 = new ValueCell { Id = NewId, Value = new Number(100) };

		//	var additionCalculation = GetAdditionCalculation();

		//	var spreadsheet = new Spreadsheet.Spreadsheet();

		//	spreadsheet.SetInput(cell1);
		//	spreadsheet.SetInput(cell2);
		//	spreadsheet.SetInput(cell3);

		//	spreadsheet.AddCaclulation(additionCalculation);
		//	spreadsheet.AddBinding(additionCalculation.Id, 1000, cell1.Id);
		//	spreadsheet.AddBinding(additionCalculation.Id, 2000, cell2.Id);

		//	spreadsheet.ApplyBindings();

		//	var outputs = spreadsheet.GetOutputs();
		//	var sum = outputs[8000].GetValue();
		//}

		//static ICalculation GetAdditionCalculation()
		//{
		//	var inputCell1 = new ReferenceCell { Id = 1000 };
		//	var inputCell2 = new ReferenceCell { Id = 2000 };

		//	var cell3 = new ValueCell { Id = 6000, Value = new Number(100) };
		//	var stepCell = new ValueCell { Id = 7000, Value = new Number(1) };

		//	var additionfn = new AdditionFn { Id = 3000 };
		//	var rangefn = new RangeFn { Id = 4000 };
		//	var foreachfn = new ForEachFn { Id = 8000 };

		//	ICalculation foreachCalculation = GetForeachCalculation();
		//	Calculation calculation = new Calculation { Id = NewId };
		//	calculation.AddUnit(inputCell1);
		//	calculation.AddUnit(inputCell2);
		//	calculation.AddUnit(cell3);
		//	calculation.AddUnit(stepCell);
		//	calculation.AddUnit(foreachCalculation);

		//	calculation.AddUnit(additionfn);
		//	calculation.AddUnit(rangefn);
		//	calculation.AddUnit(foreachfn);

		//	calculation.AddBinding(additionfn, inputCell1, inputCell2);
		//	calculation.AddBinding(rangefn, additionfn, cell3, stepCell);
		//	calculation.AddBinding(foreachfn, rangefn, foreachCalculation);
		//	calculation.ApplyBindings();

		//	return calculation;
		//}

		//static ICalculation GetForeachCalculation()
		//{
		//	var inputCell1 = new ReferenceCell { Id = NewId };
		//	var cell2 = new ValueCell { Id = NewId, Value = new Number(10) };

		//	var multiplicationfn = new MultiplicationFn { Id = NewId };

		//	Calculation calculation = new Calculation { Id = NewId };
		//	calculation.AddUnit(inputCell1);
		//	calculation.AddUnit(cell2);

		//	calculation.AddUnit(multiplicationfn);

		//	calculation.AddBinding(multiplicationfn, inputCell1, cell2);
		//	calculation.ApplyBindings();

		//	return calculation;
		//}

		//static void Case1()
		//{
		//	var cell1 = new ValueCell { Id = 1, Value = new Number(5) };
		//	var cell2 = new ValueCell { Id = 2, Value = new Number(10) };
		//	var cell3 = new ValueCell { Id = 3, Value = new Number(100) };
		//	var stepCell = new ValueCell { Id = 10, Value = new Number(100) };

		//	var additionfn = new AdditionFn { Id = 4 };
		//	additionfn.SetParameters(cell1, cell2);
		//	IData sum = additionfn.GetValue();

		//	var rangefn = new RangeFn { Id = 5 };
		//	rangefn.SetParameters(additionfn, cell3, new ValueCell { Value = new Number(1) });
		//	IData range = rangefn.GetValue();

		//	Calculation calculation = new Calculation();
		//	calculation.AddUnit(cell1);
		//	calculation.AddUnit(cell2);
		//	calculation.AddUnit(cell3);
		//	calculation.AddUnit(stepCell);
		//	calculation.AddUnit(additionfn);
		//	calculation.AddUnit(rangefn);
		//	calculation.AddBinding(additionfn, cell1, cell2);
		//	calculation.AddBinding(rangefn, additionfn, cell3, stepCell);
		//	calculation.ApplyBindings();

		//	IData calculationRange = calculation.GetOutputs().Values.First().GetValue();
		//}
	}
}
