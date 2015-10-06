using System;

namespace Batch
{
	public class Config
	{

		public Nullable<bool> CanUseIDFA;
		public Nullable<bool> CanUseAndroidID;
		public string		  AndroidAPIKey;
		public string		  IOSAPIKey;

		public Config ()
		{
			CanUseIDFA = null;
			CanUseAndroidID = null;
			AndroidAPIKey = null;
			IOSAPIKey = null;
		}
	}
}

