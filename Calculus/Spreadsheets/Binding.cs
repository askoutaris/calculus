using System;

namespace Calculus.Spreadsheets
{
	class Binding
	{
		public readonly Guid TargetId;
		public readonly Guid DataProducerId;

		public Binding(Guid targetId, Guid dataProducerId)
		{
			TargetId = targetId;
			DataProducerId = dataProducerId;
		}
	}
}
