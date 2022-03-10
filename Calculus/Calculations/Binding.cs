using System;

namespace Calculus.Calculations
{
	class Binding
	{
		public readonly Guid TargetId;
		public readonly Guid[] SourceIds;

		public Binding(Guid targetId, Guid[] sourceIds)
		{
			TargetId = targetId;
			SourceIds = sourceIds;
		}
	}
}
