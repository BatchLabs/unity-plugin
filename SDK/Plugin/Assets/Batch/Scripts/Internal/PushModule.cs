using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using LitJson;
using Batch;

namespace Batch.Internal
{
	public class PushModule : ModuleBase
	{
		internal PushModule (Bridge _bridge) : base(_bridge)
		{
		}
		
		/// <summary>
		///Gets the last known push token.
        /// Batch MUST be started in order to use this method.
        /// You will get the result in a callback you need to provide to this function.
        ///
        /// The returned token might be outdated and invalid if this method is called
        /// too early in your application lifecycle.
        ///
        /// On iOS, your application should still register for remote notifications
        /// once per launch, in order to keep this value valid.
        ///
		/// </summary>
		/// <value>The last known push token.</value>
		public string LastKnownPushToken
		{
			get
			{
				string token = getLastKnownPushToken();
				if (token == null)
				{
					token = "";
				}
				return token;
			}
			private set {}
		}

		/// <summary>
		/// Sets the GCM Sender ID and enable push support
		/// </summary>
		/// <value><c>""</c> in any case: Do not use the getter.</value>
		public string GCMSenderID
		{
			private get
			{
				//Dummy value, getter is never used
				return "";
			}
			set
			{
				setGCMSenderID(value);
			}
		}
		
		/// <summary>
		/// Sets the notification types.
		/// </summary>
		/// <value><c>""</c> in any case: Do not use the getter.</value>
		public AndroidNotificationType AndroidNotificationTypes
		{
			private get
			{
				//Dummy value, getter is never used
				return AndroidNotificationType.None;
			}
			set
			{
				setAndroidNotificationTypes(value);
			}
		}
		
		private void setAndroidNotificationTypes(AndroidNotificationType type)
		{
			JsonData data = new JsonData();
			data["notifTypes"] = (int)type;
			bridge.Call("push.setAndroidNotifTypes", JsonMapper.ToJson(data));
		}

		/// <summary>
		/// Sets the notification types.
		/// </summary>
		/// <value><c>""</c> in any case: Do not use the getter.</value>
		public IOSNotificationType IOSNotificationTypes
		{
			private get
			{
				//Dummy value, getter is never used
				return IOSNotificationType.None;
			}
			set
			{
				setiOSNotificationTypes(value);
			}
		}

		private void setiOSNotificationTypes(IOSNotificationType type)
		{
			JsonData data = new JsonData();
			data["notifTypes"] = (int)type;
			bridge.Call("push.setIOSNotifTypes", JsonMapper.ToJson(data));
		}

		/// <summary>
		/// Setups Batch Push.
		/// </summary>
		public void Setup()
		{
			bridge.Call("push.setup", "");
		}

		public void RegisterForRemoteNotifications()
		{
			bridge.Call("push.register", "");
		}

		public void ClearBadge()
		{
			bridge.Call("push.clearBadge", "");
		}

		public void DismissNotifications()
		{
			bridge.Call("push.dismissNotifications", "");
		}

		private void setGCMSenderID(string senderID)
		{
			#if UNITY_ANDROID
			try
			{
				JsonData data = new JsonData();
				data["senderID"] = senderID;
				bridge.Call("push.setGCMSenderID", JsonMapper.ToJson(data));
			}
			catch(Exception e)
			{
				Logger.Error(true, "setGCMSenderID", e);
			}
			#endif
		}

		private string getLastKnownPushToken()
		{
			try
			{
				return bridge.Call("push.getLastKnownPushToken", "");
			}
			catch(Exception e)
			{
				Logger.Error(true, "getLastKnownPushToken", e);
				return "";
			}
		}
	}
}

