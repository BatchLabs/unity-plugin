using System;
using UnityEngine;

namespace Batch.Internal
{
	public static class Logger
	{
		// Controls if we should print log flagged as debug log or not.
		private const bool DEV_MODE = false;

		public static void Error(bool debug, string method, string message)
		{
			#pragma warning disable 0162
			if (debug && !DEV_MODE)
			{
				return;
			}
			#pragma warning restore 0162
			Debug.LogError(string.Format("[Batch][{0}] {1}", method, message));
		}
		
		public static void Error(bool debug, string method, Exception exception)
		{
			Error(debug, method, string.Format("{0}\n{1}", exception.Message, exception.StackTrace));
		}

		public static void Log(bool debug, string method, string message)
		{
			#pragma warning disable 0162
			if (debug && !DEV_MODE)
			{
				return;
			}
			#pragma warning restore 0162
			Debug.Log(string.Format("[Batch][{0}] {1}", method, message));
		}
		
		public static void Log(bool debug, string method, Exception exception)
		{
			Log(debug, method, string.Format("{0}\n{1}", exception.Message, exception.StackTrace));
		}
	}
}

