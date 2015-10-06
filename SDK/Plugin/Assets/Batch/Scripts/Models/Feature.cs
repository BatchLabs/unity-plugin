using System;
using System.Runtime.Serialization;

namespace Batch
{
	public class Feature : Item
	{
		public string Value { get; protected set; }
		public long Ttl { get; protected set; }

		public Feature () : base()
		{
		}

		public Feature (string reference, string bundleRef, string value, long ttl) : base(reference, bundleRef)
		{
			this.Value = value;
			this.Ttl = ttl;
		}

		public bool HasValue()
		{
			return Value != null;
		}

		public bool IsLifetime()
		{
			return Ttl <= 0;
		}
	}
}