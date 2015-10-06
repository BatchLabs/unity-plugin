using System;
using System.Runtime.Serialization;

namespace Batch
{
	public class Item
	{
		public string Reference { get; protected set; }
		public string BundleReference { get; protected set; }

		public Item ()
		{
		}

		public Item (string reference, string bundleRef)
		{
			if (reference == null)
			{
				throw new ArgumentNullException();
			}
			this.Reference = reference;
			this.BundleReference = bundleRef;
		}

		public bool IsInBundle()
		{
			return BundleReference != null;
		}
	}
}

