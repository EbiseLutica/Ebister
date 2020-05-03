namespace Ebister
{
	using Parsing;
	using Runtime;

	public class Ebister
	{
		public EbisterConfiguration Configuration { get; }

		public Ebister(EbisterConfiguration? cfg = null)
		{
			Configuration = cfg ?? new EbisterConfiguration();
		}

		/// <summary>
		/// ソースコードを実行します。
		/// </summary>
		/// <param name="sourceText"></param>
		public void Evaluate(string sourceText)
		{
			try
			{
				var tree = EbisterParser.Parse(sourceText);

				var options = tree.Root.FindByTermName("options");
				var statements = tree.Root.FindByTermName("statements");

				if (options == null || statements == null)
					throw new RuntimeException("bug! options or statements are null");

				// todo ストリクトモードを実装する
				// var isStrict = options.FindByTermName("strict") != null || Configuration.ForceStrict;

				// グローバルな関数定義・グループ定義を登録する



			}
			finally
			{
				// 必要ならコンテキストを初期化
				if (!Configuration.PreserveContext)
				{
					globalCtx = new EbiObject();
				}
			}
		}

		private EbiObject globalCtx = new EbiObject();
	}
}
