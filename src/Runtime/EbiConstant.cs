using System.Diagnostics.CodeAnalysis;

namespace Ebister
{
	public class EbiConstant
	{
		public EbiValueBase Value
		{
			get => _value;
			[MemberNotNull(nameof(_value))]
			protected set
			{
				_value = IsValidType(value) ? value : throw new RuntimeException("Type mismatch");
			}
		}
		public EbiType SuitableType { get; }

		public EbiConstant(EbiValueBase value)
		{
			SuitableType = value.Type;
			Value = value;
		}

		public EbiConstant(EbiValueBase value, EbiType suitableType)
		{
			SuitableType = suitableType;
			Value = value;
		}

		protected bool IsValidType(EbiValueBase value)
		{
			// TODO: キャストとかをやる
			return value.Type == SuitableType;
		}

		private EbiValueBase _value;
	}
}
