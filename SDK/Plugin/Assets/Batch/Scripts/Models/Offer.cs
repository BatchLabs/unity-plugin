using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Batch
{
	public class Offer
	{
		public string Reference { get; protected set; }
		public List<Feature> Features { get; protected set; }
		public List<Resource> Resources { get; protected set; }
		public List<String> BundlesReferences { get; protected set; }
		public Dictionary<string, string> AdditionalParameters { get; protected set; }
		
		public Offer(string reference)
		{
			Reference = reference;
			Features = new List<Feature>();
			Resources = new List<Resource>();
			BundlesReferences = new List<String>();
			AdditionalParameters = new Dictionary<string, string>();
		}

		public bool HasBundles()
		{
			return BundlesReferences.Count > 0;
		}

		public bool HasFeatures()
		{
			return Features.Count > 0;
		}

		public bool HasResources()
		{
			return Resources.Count > 0;
		}

		public bool ContainsFeature(string featureReference)
		{
			foreach (var feature in Features)
			{
				if (feature.Reference.Equals(featureReference))
				{
					return true;
				}
			}
			return false;
		}

		public bool ContainsResource(string resourceReference)
		{
			foreach (var resource in Resources)
			{
				if (resource.Reference.Equals(resourceReference))
				{
					return true;
				}
			}
			return false;
		}

		public bool ContainsItem(string itemReference)
		{
			if (ContainsFeature(itemReference))
			{
				return true;
			}
			return ContainsResource(itemReference);
		}
	}
}

