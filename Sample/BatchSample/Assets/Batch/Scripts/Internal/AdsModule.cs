using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using LitJson;
using Batch;

namespace Batch.Internal
{
	[Obsolete("Batch Ads has been discontinued")]
	public class AdsModule : ModuleBase
	{
		internal static bool setupCalled = false;	

#pragma warning disable 67

		#region Handlers
		/// <summary>
		/// Called when an interstitial is ready for the given placement.
		/// </summary>
		public event InterstitialReadyHandler InterstitialReady;

		/// <summary>
		/// Called when no interstitial is available for the given placement.
		/// </summary>
		public event FailedToLoadInterstitialHandler FailedToLoadInterstitial;
		#endregion

		/// <summary>
		/// Called when an ad is available for the given placement.
		/// </summary>
		[Obsolete("Use InterstitialReady instead")]
		public event AdAvailableForPlacementHandler AdAvailableForPlacement;

		/// <summary>
		/// Called when no ad is available for the given placement.
		/// </summary>
		[Obsolete("Use FailedToLoadInterstitialForPlacement instead")]
		public event FailedToLoadAdForPlacementHandler FailedToLoadAdForPlacement;

#pragma warning restore 67

		internal AdsModule (Bridge _bridge) : base(_bridge)
		{
		}

		/// <summary>
		/// Returns if Batch Ads has been setup or not. Can yell in the logs if non null method name.
		/// </summary>
		public static bool CheckModuleSetup(string methodName)
		{
			return false;
		}

		/// <summary>
		/// Sets whether Batch should automatically load ads or not.
		/// Should be called before StartPlugin !
		/// </summary>
		public bool AutoLoad
		{
			private get
			{
				//Dummy value, getter is never used
				return true;
			}
			set
			{
			}
		}

		/// <summary>
		/// Setup Batch Ads
		/// </summary>
		public void Setup()
		{
		}

		/// <summary>
		/// Check if an interstitial is ready for the specified placement.
		/// Returns true if an interstitial is ready, false otherwise
		/// </summary>
		public bool HasInterstitialReady(String placement)
		{
			return false;
		}

		/// <summary>
		/// Load an interstitial. Only useful in manual load mode.
		/// You will be notified of success or failure in InterstitialReady or FailedToLoadInterstitialForPlacement
		/// </summary>
		public void LoadInterstitial(String placement)
		{
			if (FailedToLoadInterstitial != null)
			{
				FailedToLoadInterstitial(placement);
			}

#pragma warning disable 612, 618
			if (FailedToLoadAdForPlacement != null)
			{
				FailedToLoadAdForPlacement(placement);
			}			
#pragma warning restore 612, 618
		}

		/// <summary>
		/// Display an interstitial for a placement.
		/// Returns true on success, false when no interstitials are available
		/// </summary>
		public bool DisplayInterstitial(string placement)
		{
			return false;
		}

		// Deprecated methods

		/// <summary>
		/// Load an Ad. Only useful in manual load mode.
		/// You will be notified of success or failure in AdAvailableForPlacement or FailedToLoadAdForPlacement
		/// </summary>
		[Obsolete("Use LoadInterstitial")]
		public void LoadForPlacement(String placement)
		{
			LoadInterstitial(placement);
		}
		
		/// <summary>
		/// Display an Ad for a placement.
		/// Returns true on success, false when no ads are available
		/// </summary>
		[Obsolete("Use DisplayInterstitial")]
		public bool Display(string placement)
		{
			return false;
		}
		
		/// <summary>
		/// Check if an Ad is ready for the specified placement.
		/// Returns true if an Ad is ready, false otherwise
		/// </summary>
		[Obsolete("Use HasInterstitialReady")]
		public bool HasAdReadyForPlacement(String placement)
		{
			return HasInterstitialReady(placement);
		}
	}
}

