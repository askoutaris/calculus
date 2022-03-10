using FluentAssertions;
using Moq;
using Calculus;
using Calculus.Data;
using Calculus.Functions;
using Xunit;

namespace Tests.Functions
{
	public class MultiplicationFnTests
	{
		[Theory]
		[InlineData(1, 1)]
		[InlineData(2.5, 5.5)]
		[InlineData(-2.5, 5.5)]
		public void Multiplying_Two_Numbers_Must_Return_Product(decimal num1, decimal num2)
		{
			var cell1 = new Mock<IDataProducer>();
			cell1.Setup(x => x.GetValue()).Returns((NumericData)num1);

			var cell2 = new Mock<IDataProducer>();
			cell2.Setup(x => x.GetValue()).Returns((NumericData)num2);

			var function = new MultiplicationFn();
			function.SetParameters(cell1.Object, cell2.Object);

			var data = (NumericData)function.GetValue();

			data.Value.Should().Be(num1 * num2);
		}
	}
}
