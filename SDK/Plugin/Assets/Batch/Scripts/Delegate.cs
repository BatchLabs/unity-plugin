using System.Collections.Generic;
using System;

namespace Batch
{
	/*** Start Handlers ***/

	/// <summary>
	/// Redeem offer handler.
	/// Called by Batch each time a conditional offer is automatically unlocked.
	/// </summary>
	public delegate void RedeemAutomaticOfferHandler(Offer offer);

	/*** URL Handlers ***/

	/// <summary>
	/// Redeem URL code found handler.
	/// Called by Batch when a code has been found in the input URL.
	/// </summary>
	public delegate void RedeemURLCodeFoundHandler(string code);
    
	/// <summary>
	/// Redeem URL success handler.
	/// Called by Batch when the URL treatment has succeded.
	/// </summary>
	public delegate void RedeemURLSuccessHandler(string code, Offer offer);

	/// <summary>
	/// Redeem URL failed handler.
	/// Called by Batch when Redeem usring the URL has failed.
	/// </summary>
	public delegate void RedeemURLFailedHandler(string code, FailReason reason, CodeErrorInfo infos);

	/*** Code Handlers ***/

	/// <summary>
	/// Redeem code success handler.
	/// Called by Batch when the code successfully unlock an offer.
	/// </summary>
	public delegate void RedeemCodeSuccessHandler(string code, Offer offer);

	/// <summary>
	/// Redeem code failed handler.
	/// Caled by Batch when the code failed to unlock any content.
	/// </summary>
	public delegate void RedeemCodeFailedHandler(string code, FailReason reason, CodeErrorInfo infos);

	/*** Restore Handlers ***/

	/// <summary>
	/// Restore success handler.
	/// Called by Batch when restoring has succeded, even if there is nothing to restore.
	/// </summary>
	public delegate void RestoreSuccessHandler(List<Feature> features);

	/// <summary>
	/// Restore failed handler.
	/// Called by Batch when restoring has failed.
	/// </summary>
	public delegate void RestoreFailedHandler(FailReason reason);

	/*** Ad Handlers ***/

	/// <summary>
	/// Interstitial ready handler.
	/// Called by Batch when an interstitial is ready for the given placement.
	/// </summary>
	public delegate void InterstitialReadyHandler(string placement);
	
	/// <summary>
	/// Interstitial load failure handler.
	/// Called by Batch when no interstitial is available for the given placement.
	/// </summary>
	public delegate void FailedToLoadInterstitialHandler(string placement);

	/// <summary>
	/// Ad available handler.
	/// Called by Batch when an ad is available for the given placement.
	/// </summary>
	[Obsolete("Use InterstitialReadyHandler")]
	public delegate void AdAvailableForPlacementHandler(string placement);
	
	/// <summary>
	/// Ad load failure handler.
	/// Called by Batch when no ad is available for the given placement.
	/// </summary>
	[Obsolete("Use FailedToLoadInterstitialForPlacementHandler")]
	public delegate void FailedToLoadAdForPlacementHandler(string placement);
}
