namespace Ebister
{
	public abstract class EbiPrimitive<T> : EbiValueBase
	{
		public override string TypeName => "primitive object";

		public EbiPrimitive(T t) => value = t;
		public virtual T ToDotNetObject() => value;
		protected readonly T value;
	}
}
