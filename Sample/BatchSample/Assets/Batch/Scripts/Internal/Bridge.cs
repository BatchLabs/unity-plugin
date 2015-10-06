using System;
using UnityEngine;
using System.Runtime.InteropServices;

namespace Batch.Internal
{
	internal class Bridge
	{
		#if UNITY_ANDROID
		private AndroidJavaClass plugin;
		#elif UNITY_IPHONE
		[DllImport("__Internal")]
		private static extern IntPtr _call(string action, string parameter);
		#endif

		public Bridge ()
		{
			#if UNITY_ANDROID
			if(!UnityEngine.Application.isEditor)
			{
				plugin = new AndroidJavaClass("com.batch.android.unity.BatchUnityBridge");
			}
			#endif
		}

		public string Call(string action, string parameters)
		{
			string result = null;
			try
			{
				if(!UnityEngine.Application.isEditor)
				{
					#if UNITY_ANDROID
					if(plugin != null)
					{
						result = plugin.CallStatic<string>("call", action, parameters);
					}
					#elif UNITY_IPHONE
					result = Marshal.PtrToStringAuto(_call(action, parameters));
					#endif

					Logger.Log(true,"Call result: ",result);
				}
			}
			catch(Exception e)
			{
				Logger.Error(false, "Call", e);
			}

			return result;
		}

		public static bool ResultToBool(String result)
		{
			if("true".Equals(result, StringComparison.InvariantCultureIgnoreCase))
			{
				return true;
			}
			return false;
		}
	}
}

