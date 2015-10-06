using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Batch;

public class UnlockManager : MonoBehaviour {
	private const int UNLIMITED_PRO_TRIAL = -1;

	private const string NO_ADS_KEY = "no_ads";
	private const string LIVES_KEY = "lives";
	private const string PRO_TRIAL_KEY = "pro_trial";

	private const bool NO_ADS_DEFAULT_VALUE = false;
	private const int LIVES_DEFAUT_VALUE = 10;
	private const string PRO_TRIAL_DEFAULT_VALUE = "0";

	private const string NO_ADS_REFERENCE = "NO_ADS";
	private const string LIVES_REFERENCE = "LIVES";
	private const string PRO_TRIAL_REFERENCE = "PRO_TRIAL";

	public int LivesCount {
		get {
			return PlayerPrefs.GetInt(LIVES_KEY, LIVES_DEFAUT_VALUE);
		}
		set {
			PlayerPrefs.SetInt(LIVES_KEY, value);
		}
	}

	public bool HasNoAds {
		get {
			return Convert.ToBoolean(PlayerPrefs.GetInt(NO_ADS_KEY, LIVES_DEFAUT_VALUE));
		}
		set {
			PlayerPrefs.SetInt(NO_ADS_KEY, value ? 1 : 0);
		}
	}

	public bool HasProTrial {
		get {
			var timeLeft = PlayerPrefs.GetInt(PRO_TRIAL_KEY, LIVES_DEFAUT_VALUE);
			return timeLeft != 0 && timeLeft != UNLIMITED_PRO_TRIAL;
		}
		private set {}
	}

	public Int64 TimeLeftForProTrial {
		get {
			var expirationDate = Convert.ToInt64(PlayerPrefs.GetString(PRO_TRIAL_KEY, PRO_TRIAL_DEFAULT_VALUE));
			if (expirationDate == UNLIMITED_PRO_TRIAL) {
				return UNLIMITED_PRO_TRIAL;
			}
			return Math.Max(0, expirationDate - (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds);
		}
		private set {}
	}

	public void UnlockItems(Offer offer) {
		LoggerHelper.LogOffer(offer);

		if (offer.Features.Count > 0) {
			UnlockFeatures(offer.Features);
		}

		foreach (var resource in offer.Resources) {
			if (LIVES_REFERENCE.Equals(resource.Reference, StringComparison.InvariantCultureIgnoreCase)) {
				LivesCount += resource.Quantity;
			}
		}
	}

	public void UnlockFeatures(List<Feature> features) {
		foreach (var feature in features) {
			var reference = feature.Reference;
			if (NO_ADS_REFERENCE.Equals(reference, StringComparison.InvariantCultureIgnoreCase)) {
				PlayerPrefs.SetInt(NO_ADS_KEY, 1);
			} else if (PRO_TRIAL_REFERENCE.Equals(reference, StringComparison.InvariantCultureIgnoreCase)) {
				if (feature.IsLifetime()) {
					PlayerPrefs.SetString(PRO_TRIAL_KEY, "" + UNLIMITED_PRO_TRIAL);
				} else {
					PlayerPrefs.SetString(PRO_TRIAL_KEY, "" + (feature.Ttl + (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds));
				}
			}
		}
	}
}
