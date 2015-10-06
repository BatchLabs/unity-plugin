using System;
using System.Runtime.Serialization;

namespace Batch
{
	public class Resource : Item
	{
		public int Quantity { get; protected set; }

		public Resource () : base()
		{
		}

		public Resource (string reference, string bundleRef, int quantity) : base(reference, bundleRef)
		{
			this.Quantity = quantity;
		}
	}
}

