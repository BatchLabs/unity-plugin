using System;
namespace Batch
{
	/// <summary>
	/// Ad loading error
	/// </summary>
	[Obsolete("Batch Ads has been discontinued")]
	public enum AdsError
	{
		/// <summary>
		/// There were no network connexion to load an ad
		/// </summary>
		NETWORK_ERROR,
	
		/// <summary>
		/// No ad is currently available for this placement, either you reach the cappings or this placement is disabled
		/// </summary>
		NO_AD_AVAILABLE,
		
		/// <summary>
		/// This placement is unknown, you should check your configuration on Batch dashboard
		/// </summary>
		UNKNOWN_PLACEMENT,
		
		/// <summary>
		/// An unexpected error occured
		/// </summary>	
		UNEXPECTED_ERROR
	}
}

