using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Batch;

public class MainMenu : MonoBehaviour {

	/*
	 * The size our UI is designed for
	 * The scaling code will scale based on the real screen size
	 */
	public const float DESIGN_HEIGHT = 500;
	public const float DESIGN_WIDTH = 290;

	// Injected from a GameObject
	public BatchPlugin BatchPlugin;

	// Injected from a GameObject
	public UnlockManager UnlockManager;

	private enum MenuKind
	{
		Main,
		Unlock,
		RedeemCode,
		PushSettings,
		PleaseWait,
		Alert
	}
	
	private MenuKind currentMenu = MenuKind.Main;

	private String enteredCode = "";
	
	private String alertText = "";

	// Use this for initialization
	void Start () {
		BatchPlugin.Unlock.RedeemAutomaticOffer += new RedeemAutomaticOfferHandler(BatchRedeemAutomaticOffer);
		BatchPlugin.Unlock.RedeemCodeSuccess += new RedeemCodeSuccessHandler(BatchRedeemCodeSuccess);
		BatchPlugin.Unlock.RedeemCodeFailed += new RedeemCodeFailedHandler(BatchRedeemCodeFailed);
		BatchPlugin.Unlock.RestoreSuccess += new RestoreSuccessHandler(BatchRestoreSuccess);
		BatchPlugin.Unlock.RestoreFailed += new RestoreFailedHandler(BatchRestoreFailed);
		// Use the same handlers as code since we don't need any distinction here.
		// You could separate them for tracking purposes
		BatchPlugin.Unlock.RedeemURLCodeFound += new RedeemURLCodeFoundHandler(BatchRedeemURLCodeFound);
		BatchPlugin.Unlock.RedeemURLSuccess += new RedeemURLSuccessHandler(BatchRedeemCodeSuccess);
		BatchPlugin.Unlock.RedeemURLFailed += new RedeemURLFailedHandler(BatchRedeemCodeFailed);
		
		Config config = new Config();
		config.IOSAPIKey = "YOUR_API_KEY";
		config.AndroidAPIKey = "YOUR_API_KEY";
		BatchPlugin.Push.GCMSenderID = "YOUR_SENDER_ID";
		BatchPlugin.Push.RegisterForRemoteNotifications();

		updatePushNotificationSettings();

		BatchPlugin.StartPlugin(config);
		
		string language = BatchPlugin.DefaultUserProfile.ApplicationLanguage;
		Debug.Log("[Batch] Application language: " + language);
		Debug.Log("[Batch] Dev Mode : " + BatchPlugin.IsRunningInDevMode);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// GUI Methods

	public static void ScaleGUI()
	{
		float ry = Screen.height / DESIGN_HEIGHT;
		GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3 (ry, ry, 1));
	}
	
	void OnGUI()
	{
		ScaleGUI();
		switch (currentMenu)
		{
		case MenuKind.Main:
			OnMainMenu();
			break;
		case MenuKind.Unlock:
			OnUnlockMenu();
			break;
		case MenuKind.PushSettings:
			OnPushSettingsMenu();
			break;
		case MenuKind.RedeemCode:
			OnRedeemCodeMenu();
			break;
		case MenuKind.PleaseWait:
			OnPleaseWaitMenu();
			break;
		case MenuKind.Alert:
			OnAlertMenu();
			break;
		}
	}

	private void OnMainMenu()
	{
		// Make a group on the center of the screen
		GUI.BeginGroup (new Rect (DESIGN_WIDTH / 2 - 75, DESIGN_HEIGHT / 2 - 60, 150, 180));
		// All rectangles are now adjusted to the group. (0,0) is the topleft corner of the group.
		
		GUI.Box (new Rect (0,0,150,120), "Batch Sample");
		if (GUI.Button (new Rect (20,40,110,30), "Unlock"))
		{
			currentMenu = MenuKind.Unlock;
		}
		if (GUI.Button (new Rect (20,80,110,30), "Notif. Settings"))
		{
			currentMenu = MenuKind.PushSettings;
		}
		
		GUI.EndGroup ();
	}

	private void OnRedeemCodeMenu()
	{
		int menuWidth = 200;
		int menuHeight = 110;
		GUI.BeginGroup (new Rect (DESIGN_WIDTH / 2 - (menuWidth / 2), 80, menuWidth, menuHeight));
		
		GUI.Box (new Rect (0,0,menuWidth,menuHeight), "Enter your offer code");
		enteredCode = GUI.TextField(new Rect (10,30,180,30), enteredCode);
		if (GUI.Button (new Rect (20,70,160,30), "Redeem"))
		{
			currentMenu = MenuKind.PleaseWait;
			BatchPlugin.Unlock.RedeemCode(enteredCode);
		}
		
		GUI.EndGroup ();
	}

	private void OnPleaseWaitMenu()
	{
		int menuWidth = 200;
		int menuHeight = 110;
		GUI.BeginGroup (new Rect (DESIGN_WIDTH / 2 - (menuWidth / 2), 80, menuWidth, menuHeight));
		
		GUI.Box (new Rect (0,0,menuWidth,menuHeight), "");
		GUI.Label(new Rect (10,30,180,30), "Please wait");
		
		GUI.EndGroup ();
	}

	private void OnAlertMenu()
	{
		int menuWidth = 200;
		int menuHeight = 110;
		GUI.BeginGroup (new Rect (DESIGN_WIDTH / 2 - (menuWidth / 2), 80, menuWidth, menuHeight));
		
		GUI.Box (new Rect (0,0,menuWidth,menuHeight), "");
		GUI.Label(new Rect (10,30,180,50), alertText);
		if (GUI.Button (new Rect (20,70,160,30), "OK"))
		{
			currentMenu = MenuKind.Unlock;
		}
		
		GUI.EndGroup ();
	}

	private void OnUnlockMenu()
	{
		int menuWidth = 200;
		int menuHeight = 200;
		GUI.BeginGroup (new Rect (DESIGN_WIDTH / 2 - (menuWidth / 2), DESIGN_HEIGHT / 2 - (menuHeight / 2), menuWidth, menuHeight));
		
		GUI.Box (new Rect (0,0,menuWidth,menuHeight), "Options");

		bool hasNoAds = UnlockManager.HasNoAds;
		bool hasProTrial = UnlockManager.HasProTrial;
		Int64 proTrialTimeLeft = UnlockManager.TimeLeftForProTrial;
		Int64 proTrialDaysLeft = (Int64) Math.Floor((double)proTrialTimeLeft / 86400);
		int lives = UnlockManager.LivesCount;
		GUI.Toggle (new Rect (10,40,180,30), hasNoAds, "No Ads");
		GUI.Toggle (new Rect (10,70,180,30), hasProTrial, proTrialDaysLeft <= 0 ? "Pro Trial" : "Pro Trial - " + proTrialDaysLeft + " day(s) left");
		GUI.Label (new Rect (10,100,180,30), "Lives: " + lives);
		
		if (GUI.Button (new Rect (10,menuHeight-80,80,30), "Redeem"))
		{
			currentMenu = MenuKind.RedeemCode;
		}
		if (GUI.Button (new Rect (110,menuHeight-80,80,30), "Restore"))
		{
			currentMenu = MenuKind.PleaseWait;
			BatchPlugin.Unlock.Restore();
		}
		if (GUI.Button (new Rect (20,menuHeight-40,160,30), "Done"))
		{
			currentMenu = MenuKind.Main;
		}
		
		GUI.EndGroup ();
	}

	private void OnPushSettingsMenu()
	{
		// Please note that this is effective for Android only

		int menuWidth = 200;
		int menuHeight = 260;
		GUI.BeginGroup (new Rect (DESIGN_WIDTH / 2 - (menuWidth / 2), DESIGN_HEIGHT / 2 - (menuHeight / 2), menuWidth, menuHeight));
		
		GUI.Box (new Rect (0,0,menuWidth,menuHeight), "Options");
		bool areNotificationsEnabled = Convert.ToBoolean(PlayerPrefs.GetInt("notificationsEnabled", 1));
		bool isNotifSoundEnabled = Convert.ToBoolean(PlayerPrefs.GetInt("notificationSoundEnabled", 1));
		bool isNotifVibrationEnabled = Convert.ToBoolean(PlayerPrefs.GetInt("notificationVibrationEnabled", 1));
		bool areNotifLightsEnabled = Convert.ToBoolean(PlayerPrefs.GetInt("notificationLightsEnabled", 1));

		if (GUI.Toggle (new Rect (10,40,180,30), areNotificationsEnabled, "Notifications") != areNotificationsEnabled)
		{
			PlayerPrefs.SetInt("notificationsEnabled", 1-Convert.ToInt32(areNotificationsEnabled));
			updatePushNotificationSettings();
		}

		if (GUI.Toggle (new Rect (10,70,180,30), isNotifSoundEnabled, "Sound") != isNotifSoundEnabled)
		{
			PlayerPrefs.SetInt("notificationSoundEnabled", 1-Convert.ToInt32(isNotifSoundEnabled));
			updatePushNotificationSettings();
		}

		if (GUI.Toggle (new Rect (10,100,180,30), isNotifVibrationEnabled, "Vibrate") != isNotifVibrationEnabled)
		{
			PlayerPrefs.SetInt("notificationVibrationEnabled", 1-Convert.ToInt32(isNotifVibrationEnabled));
			updatePushNotificationSettings();
		}

		if (GUI.Toggle (new Rect (10,130,180,30), areNotifLightsEnabled, "Lights") != areNotifLightsEnabled)
		{
			PlayerPrefs.SetInt("notificationLightsEnabled", 1-Convert.ToInt32(areNotifLightsEnabled));
			updatePushNotificationSettings();
		}

		if (GUI.Button (new Rect (20,menuHeight-80,160,30), "Log Push Token"))
		{
			Debug.Log("[Batch] Last Known Token: " + BatchPlugin.Push.LastKnownPushToken);
		}

		if (GUI.Button (new Rect (20,menuHeight-40,160,30), "Done"))
		{
			currentMenu = MenuKind.Main;
		}
		
		GUI.EndGroup ();
	}

	private void updatePushNotificationSettings()
	{
		bool areNotificationsEnabled = Convert.ToBoolean(PlayerPrefs.GetInt("notificationsEnabled", 1));
		bool isNotifSoundEnabled = Convert.ToBoolean(PlayerPrefs.GetInt("notificationSoundEnabled", 1));
		bool isNotifVibrationEnabled = Convert.ToBoolean(PlayerPrefs.GetInt("notificationVibrationEnabled", 1));
		bool areNotifLightsEnabled = Convert.ToBoolean(PlayerPrefs.GetInt("notificationLightsEnabled", 1));

		AndroidNotificationType notificationTypes = AndroidNotificationType.None;

		if (areNotificationsEnabled)
		{
			notificationTypes |= AndroidNotificationType.Alert;
		}

		if (isNotifSoundEnabled)
		{
			notificationTypes |= AndroidNotificationType.Sound;
		}

		if (isNotifVibrationEnabled)
		{
			notificationTypes |= AndroidNotificationType.Vibrate;
		}

		if (areNotifLightsEnabled)
		{
			notificationTypes |= AndroidNotificationType.Lights;
		}

		BatchPlugin.Push.AndroidNotificationTypes = notificationTypes;
	}

	public void ShowRedeemAlert(Offer offer)
	{
		if (offer.AdditionalParameters.ContainsKey("reward_message")) {
			alertText = offer.AdditionalParameters["reward_message"];
			currentMenu = MenuKind.Alert;
		}
	}

	// Batch Callbacks

	private void BatchRedeemAutomaticOffer(Offer offer)
	{
		Debug.Log("BatchRedeemAutomaticOffer - offer: " + offer.Reference);

		UnlockManager.UnlockItems(offer);

		ShowRedeemAlert(offer);
	}

	private void BatchRedeemCodeSuccess(string code, Offer offer)
	{
		System.Console.WriteLine ("BatchRedeemCodeSuccess: " + code);

		UnlockManager.UnlockItems(offer);

		alertText = "Code redeemed!";
		currentMenu = MenuKind.Alert;
	}
	
	private void BatchRedeemCodeFailed(string code, FailReason reason, CodeErrorInfo infos)
	{
		System.Console.WriteLine("Batch - Redeem failed : "+reason.ToString());
		LoggerHelper.LogErrorInfos (infos);
		
		alertText = "Redeem failed\n";
		currentMenu = MenuKind.Alert;
		
		if (reason == FailReason.NETWORK_ERROR) // 1
		{
			// Show network error, you can ask your user to check the connectivity 
			// and retry when network connectivity is regained.
			alertText += "Network error";
		}
		else if (reason == FailReason.INVALID_CODE)
		{
			switch( infos.Type )
			{
			case CodeErrorInfoType.UNKNOWN_CODE : // 2
				// Display an error that explains the promocode entered is invalid.
				alertText += "Unknown code";
				break;
			case CodeErrorInfoType.OFFER_EXPIRED : // 3.1
				// Explain the promotion expiration to the user.
				alertText += "Offer expired";
				break;
			case CodeErrorInfoType.OFFER_NOT_STARTED : // 3.2
				// Explain that the promotion has not yet started.
				alertText += "Offer not started";
				break;
			case CodeErrorInfoType.OFFER_CAPPED : // 3.3
				// Explain that the user is late because the cap has already been reached,
				// tell them to be quicker next time!
				alertText += "Offer capped";
				break;
			case CodeErrorInfoType.ALREADY_CONSUMED : // 3.4
				// Explain to the user that he already used this promocode.
				// You can tell him to use the Restore button if you included it
				// with the Restore function of Batch.
				alertText += "Already consumed";
				break;
			case CodeErrorInfoType.MISSING_CONDITIONS : // 3.5
				// Tell the user that they have to install another app to enjoy the offer.
				foreach (var app in infos.MissingApplications)
				{
					if (app.HasBundleId())
					{
						// You can get the bundle id of the missing app by calling app.getBundleId();
					}
					else if (app.HasScheme())
					{
						// You can get the url scheme of the missing app by calling app.getScheme();
					}
				}
				alertText += "Missing required apps";
				break;
			case CodeErrorInfoType.USER_NOT_TARGETED : // 3.6
				// Explain that the user is not eligible for this offer.
				alertText += "User not eligible";
				break;
			default :
				// Display a generic error asking them to retry later.
				alertText += "Unknown error";
				break;
			}	
		}
		else
		{
			// Display a generic error asking them to retry later.
			alertText += "Unknown error";
		}
	}
	
	public void BatchRedeemURLCodeFound(string code)
	{
		System.Console.WriteLine("Batch - Redeem URL code found : "+code);
	}
	
	private void BatchRestoreSuccess(List<Feature> features)
	{
		System.Console.WriteLine("Batch - Restore Success : "+features.Count+" features restored");

		UnlockManager.UnlockFeatures(features);

		foreach (var feature in features)
		{
			LoggerHelper.LogFeature(feature);
		}
		alertText = "Restore success";
		currentMenu = MenuKind.Alert;
	}
	
	private void BatchRestoreFailed(FailReason reason)
	{
		System.Console.WriteLine("Batch - Restore failed : "+reason.ToString());
		
		alertText = "Restore failed";
		currentMenu = MenuKind.Alert;
	}
}
