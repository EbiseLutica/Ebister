namespace Ebister
{
	public static class EbiTypeHelper
	{
		public static bool IsValidType(EbiType inType, EbiType outType)
		{
			// 型が一致していればtrue
			// any型に対しては全部true
			if (inType == outType || outType == EbiType.Any)
				return true;

			// TODO: キャストとかをやる

			return false;
		}

		public static bool IsAllValidTypes(EbiType[] inTypes, EbiType[] outTypes)
		{
			if (inTypes.Length != outTypes.Length) return false;
			for (var i = 0; i < inTypes.Length; i++)
				if (!IsValidType(inTypes[i], outTypes[i])) return false;
			return true;
		}
	}
}
