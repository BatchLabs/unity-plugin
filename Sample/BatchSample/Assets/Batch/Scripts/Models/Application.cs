namespace Batch
{
	public class Application
	{
		public string BundleId { get; protected set; }
		public string Scheme { get; protected set; }

		public Application () : base()
		{

		}

		public Application(string value, bool isScheme) : base()
		{
			if (isScheme) 
			{
				this.Scheme = value;
			} 
			else 
			{
				this.BundleId = value;
			}
		}

		public bool HasScheme()
		{
			return Scheme != null;
		}

		public bool HasBundleId()
		{
			return BundleId != null;
		}
	}
}

