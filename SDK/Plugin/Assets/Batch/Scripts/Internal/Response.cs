using System;
using System.Collections.Generic;
using LitJson;

using UnityEngine;

namespace Batch.Internal
{
	internal static class JsonDataExtensions
	{
		public static string GetString(this JsonData jsonData, string key, string defaultValue)
		{
			try
			{
				JsonData value = jsonData[key];
				if (value.IsString)
				{
					return (string)value;
				}
				else if (!value.IsArray && !value.IsObject)
				{
					return value.ToString();
				}
				return defaultValue;
			}
			catch
			{
				return defaultValue;
			}
		}

		public static int GetInt(this JsonData jsonData, string key, int defaultValue)
		{
			try
			{
				return (int)jsonData[key];
			}
			catch
			{
				return defaultValue;
			}
		}

		public static long GetLong(this JsonData jsonData, string key, long defaultValue)
		{
			try
			{
				return (long)jsonData[key];
			}
			catch
			{
				try
				{
					int val = (int)jsonData[key];
					return Convert.ToInt64(val);
				}
				catch
				{
					return defaultValue;
				}
			}
		}

		public static bool GetBool(this JsonData jsonData, string key, bool defaultValue)
		{
			try
			{
				return (bool)jsonData[key];
			}
			catch
			{
				return defaultValue;
			}
		}

		public static bool HasKey(this JsonData jsonData, string key)
		{
			try
			{
				return jsonData[key] != null;
			}
			catch
			{
				return false;
			}
		}
	}

	internal class Response
	{
		private string responseString;

		private JsonData responseData;

		public Response(string response)
		{
			if( response == null )
			{
				throw new ArgumentNullException("The JSON string is null.");
			}
			if( response.Length == 0 )
			{
				throw new FormatException("The JSON string is empty.");
			}

			Logger.Log(true, "Response", "BAResponse JSON : " + response);

			responseString = response;
			responseData = JsonMapper.ToObject(responseString);
			if( responseData == null )
			{
				throw new FormatException("Error while parsing response JSON.");
			}
		}

		public FailReason GetFailReason()
		{
			if ( !responseData.HasKey("failReason") ) 
			{
				return FailReason.UNEXPECTED_ERROR;
			}

			return (FailReason) Enum.Parse(typeof(FailReason), responseData.GetString("failReason", "UNEXPECTED_ERROR"));
		}

		public CodeErrorInfo GetCodeErrorInfos()
		{
			if ( !responseData.HasKey("invalidCodeReason") ) 
			{
				return null;
			}

			CodeErrorInfoType type = (CodeErrorInfoType)Enum.Parse (typeof(CodeErrorInfoType), responseData.GetString("invalidCodeReason", null));
			List<Application> missingApplications = new List<Application>();

			if ( responseData.HasKey("missingApplications") ) 
			{
				JsonData apps = responseData["missingApplications"];

				for (var i = 0; i < apps.Count; i++)
				{
					JsonData app = apps[i];
					missingApplications.Add(new Application(app.HasKey("bundleId") ? app.GetString("bundleId", null) :  app.GetString("scheme", null), app.HasKey("scheme")));
				}
			}

			return new CodeErrorInfo (type, missingApplications);
		}

		public string GetCode()
		{
			return responseData.GetString("code", null);
		}

		public string GetFailedCode()
		{
			return responseData.GetString ("invalidCode", null);
		}

		public Offer GetOffer()
		{
			Offer offer = new Offer(responseData.GetString("reference", null));

			if ( responseData.HasKey("features") )
			{			
				JsonData features = responseData["features"];
				for (var i = 0; i < features.Count; i++)
				{
					offer.Features.Add(ParseFeature(features[i]));
				}
			}

			if ( responseData.HasKey("resources") )
			{
				JsonData resources = responseData["resources"];
				for (var i = 0; i < resources.Count; i++)
				{
					offer.Resources.Add(ParseResource(resources[i]));
				}
			}

			if ( responseData.HasKey("bundlesReferences") )
			{
				JsonData bundlesReferences = responseData["bundlesReferences"];
				for (var i = 0; i < bundlesReferences.Count; i++)
				{
					offer.BundlesReferences.Add((string)bundlesReferences[i]);
				}
			}

			if ( responseData.HasKey("offerAdditionalParameters") )
			{
				JsonData additionalParameters = responseData["offerAdditionalParameters"];
				foreach (var key in additionalParameters.Keys)
				{
					string value = additionalParameters.GetString(key, null);
					if (value != null)
					{
						offer.AdditionalParameters.Add(key, value);
					}
				}
			}
			
			return offer;
		}

		public List<Feature> GetFeatures()
		{
			List<Feature> features = new List<Feature>();

			if ( responseData.HasKey("features") )
			{
				JsonData featuresJson = responseData["features"];
				for (var i = 0; i < featuresJson.Count; i++)
				{
					features.Add(ParseFeature(featuresJson[i]));
				}
			}

			return features;
		}
		
		public List<Resource> GetResources()
		{
			List<Resource> resources = new List<Resource>();

			if ( responseData.HasKey("resources") )
			{
				JsonData resourcesJson = responseData["resources"];
				for (var i = 0; i < resourcesJson.Count; i++)
				{
					resources.Add(ParseResource(resourcesJson[i]));
				}
			}
			
			return resources;
		}

		public String GetPlacement()
		{
			return responseData.GetString("placement", null);
		}

		public IntPtr GetHandle()
		{
			return new IntPtr(responseData.GetLong("handle", 0));
		}

		public bool GetDevMode()
		{
			return responseData.GetBool("devMode", false);
		}

		private Feature ParseFeature(JsonData data)
		{
			return new Feature(data.GetString("reference", null), data.GetString("bundleReference", null), data.GetString("value", null), data.GetLong("ttl", 0L));
		}

		private Resource ParseResource(JsonData data)
		{
			return new Resource(data.GetString("reference", null), data.GetString("bundleReference", null), data.GetInt("quantity", 0));
		}
	}
}

