using UnityEngine;
using Batch.Internal;
using System.Collections;
using System;
using System.Runtime.InteropServices;

namespace Batch
{
	[Obsolete("Batch Ads has been discontinued")]
	public class Interstitial
	{

#region Handlers
		/// <summary>
		/// Called when no ad has been displayed to the user.
		/// It can happens if no ads are available or on error.
		/// </summary>
		public EventHandler NoAdDisplayed;

		/// <summary>
		/// Called when the ad is displayed to the user.
		/// </summary>
		public EventHandler AdDisplayed;

		/// <summary>
		///  Called when the previously displayed ad is closed.
		/// </summary>
		public EventHandler AdClosed;

		/// <summary>
		/// Called when the user cancelled the ad.
		/// This can be due to the used pressing either the close button or the back button.
		/// </summary>
		public EventHandler AdCancelled;

		/// <summary>
		/// Called when the user clicked the Ad.
		/// AdClosed will be called afterwards
		/// </summary>
		public EventHandler AdClicked;
#endregion

		/// <summary>
		/// Get a new instance of <see cref="Batch.Interstitial"/> for a placement.
		/// You should bind event handlers once you instanciated this.
		/// </summary>
		/// <param name="placement">The placement to show this interstitial for</param>
		[Obsolete("Batch Ads has been discontinued")]
		public Interstitial(String placement)
		{
		}

		/// <summary>
		/// Show the interstitial (if possible).
		/// </summary>
		public void Display()
		{
			if( NoAdDisplayed != null)
			{
				NoAdDisplayed(this, EventArgs.Empty);
			}
		}

	}

	[Obsolete("Batch Ads has been discontinued")]
	public class Ad : Interstitial
	{
		/// <summary>
		/// Get a new instance of <see cref="Batch.Ad"/> for a placement.
		/// You should bind event handlers once you instanciated this.
		/// </summary>
		/// <param name="placement">The placement to show this ad for</param>
		[Obsolete("Batch Ads has been discontinued")]
		public Ad(String placement) : base(placement)
		{
		}
	}
}
