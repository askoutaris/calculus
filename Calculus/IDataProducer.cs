using Calculus.Data;

namespace Calculus
{
	public interface IDataProducer : IUnit
	{
		IData GetValue();
	}
}
