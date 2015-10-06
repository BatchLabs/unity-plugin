using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using LitJson;

namespace Batch.Internal
{
	public class DefaultUserProfile : Batch.UserProfile
	{

		internal Bridge bridge;
		
		internal DefaultUserProfile (Bridge _bridge)
		{
			bridge = _bridge;
		}

		/// <summary>
		/// Sets and get a custom user identifier.
		/// </summary>
		public override string CustomUserID
		{
			get
			{
				return getCustomUserID();
			}
			set
			{
				setCustomUserID(value);
			}
		}
		
		/// <summary>
		/// Sets and get an application language.
		/// </summary>
		public override string ApplicationLanguage
		{
			get
			{
				return getAppLanguage();
			}
			set
			{
				setAppLanguage(value);
			}
		}
		
		/// <summary>
		/// Sets and get an application region.
		/// </summary>
		public override string ApplicationRegion
		{
			get
			{
				return getAppRegion();
			}
			set
			{
				setAppRegion(value);
			}
		}

		private string getCustomUserID()
		{
			string identifier = null;
			try
			{
				identifier = bridge.Call("getCustomID", "");
			}
			catch(Exception e)
			{
				Logger.Error(true, "getCustomUserID", e);
			}
			
			return identifier;
		}
		
		private void setCustomUserID(string userID)
		{
			try
			{
				JsonData data = new JsonData();
				data["customID"] = userID;
				bridge.Call("setCustomID", JsonMapper.ToJson(data));
			}
			catch(Exception e)
			{
				Logger.Error(true, "setCustomUserID", e);
			}
		}
		
		private string getAppLanguage()
		{
			string language = null;
			try
			{
				language = bridge.Call("getAppLanguage", "");
			}
			catch(Exception e)
			{
				Logger.Error(true, "getAppLanguage", e);
			}
			
			return language;
		}
		
		private void setAppLanguage(string language)
		{
			try
			{
				JsonData data = new JsonData();
				data["language"] = language;
				bridge.Call("setAppLanguage", JsonMapper.ToJson(data));
			}
			catch(Exception e)
			{
				Logger.Error(true, "setAppLanguage", e);
			}
		}
		
		private string getAppRegion()
		{
			string region = null;
			try
			{
				region = bridge.Call("getAppRegion", "");
			}
			catch(Exception e)
			{
				Logger.Error(true, "getAppRegion", e);
			}
			
			return region;
		}
		
		private void setAppRegion(string region)
		{
			try
			{
				JsonData data = new JsonData();
				data["region"] = region;
				bridge.Call("setAppRegion", JsonMapper.ToJson(data));
			}
			catch(Exception e)
			{
				Logger.Error(true, "setAppRegion", e);
			}
		}
	}
}