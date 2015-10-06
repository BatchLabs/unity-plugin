using UnityEngine;
using System.Collections;
using Batch;

public static class LoggerHelper {

	public static void LogOffer(Offer offer)
	{
		System.Console.WriteLine ("-------------------");
		
		System.Console.WriteLine ("Offer : " + offer.Reference);
		
		System.Console.WriteLine ("Features : "+offer.Features.Count);
		foreach (var feature in offer.Features)
		{
			LogFeature(feature);
		}
		
		System.Console.WriteLine("Resources : "+offer.Resources.Count);
		foreach(var res in offer.Resources)
		{
			LogResource(res);
		}
		
		System.Console.WriteLine("Offer bundles : "+offer.BundlesReferences.Count);
		foreach(var bdl in offer.BundlesReferences)
		{
			System.Console.WriteLine("Bundle : "+bdl);
		}
		
		System.Console.WriteLine("Offer additional parameters : "+offer.AdditionalParameters.Keys.Count);
		foreach(var param in offer.AdditionalParameters.Keys)
		{
			System.Console.WriteLine("Parameter : " + param + " = " + offer.AdditionalParameters[param]);
		}
		
		System.Console.WriteLine ("-------------------");
	}
	
	public static void LogFeature(Feature feature)
	{
		System.Console.WriteLine("-----------------");
		System.Console.WriteLine("Feature : "+feature.Reference);
		System.Console.WriteLine("Feature is lifetime : "+feature.IsLifetime());
		
		if( !feature.IsLifetime() )
		{
			System.Console.WriteLine("Feature TTL : "+feature.Ttl);
		}
		
		System.Console.WriteLine("Feature has value : "+feature.HasValue());
		
		if( feature.HasValue() )
		{
			System.Console.WriteLine("Feature value : "+feature.Value);
		}
		
		System.Console.WriteLine("Feature is in bundle : "+feature.IsInBundle());
		
		if( feature.IsInBundle() )
		{
			System.Console.WriteLine("Feature bundle : "+feature.BundleReference);
		}
		
		System.Console.WriteLine("-----------------");
	}
	
	public static void LogResource(Resource res)
	{
		System.Console.WriteLine("-----------------");
		System.Console.WriteLine("Resource : "+res.Reference);
		System.Console.WriteLine("Resource quantity : "+res.Quantity);
		
		System.Console.WriteLine("Resource is in bundle : "+res.IsInBundle());
		
		if( res.IsInBundle() )
		{
			System.Console.WriteLine("Resource bundle : "+res.BundleReference);
		}
		
		System.Console.WriteLine("-----------------");
	}
	
	
	public static void LogErrorInfos(CodeErrorInfo infos)
	{
		System.Console.WriteLine("-----------------");
		
		if( infos == null )
		{
			System.Console.WriteLine("CodeErrorInfo : Null");
			System.Console.WriteLine("-----------------");
			return;
		}
		
		System.Console.WriteLine("Type : "+infos.Type);
		System.Console.WriteLine("Has missing apps : "+infos.HasMissingApplications());
		
		foreach(var app in infos.MissingApplications)
		{
			LogApplication(app);
		}
		
		System.Console.WriteLine("-----------------");
	}
	
	public static void LogApplication(Batch.Application app)
	{
		System.Console.WriteLine("-----------------");
		
		System.Console.WriteLine("Has scheme : "+app.HasScheme());
		if( app.HasScheme() )
		{
			System.Console.WriteLine("scheme : "+app.Scheme);
		}
		
		System.Console.WriteLine("Has bundle : "+app.HasBundleId());
		if( app.HasBundleId() )
		{
			System.Console.WriteLine("bundle : "+app.BundleId);
		}
		
		System.Console.WriteLine("-----------------");
	}
}
