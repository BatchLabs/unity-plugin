using System;
using System.Runtime.Serialization;
using System.Collections;
using System.Collections.Generic;

namespace Batch
{
	public enum CodeErrorInfoType 
	{
		OFFER_PAUSED, 
		OFFER_ALREADY_ACQUIRED, 
		ALREADY_CONSUMED, 
		OFFER_CAPPED, 
		OFFER_EXPIRED, 
		UNKNOWN_CODE, 
		MISSING_CONDITIONS, 
		OFFER_UNSUPPORTED,
		OFFER_NOT_STARTED,
		USER_NOT_TARGETED,
		SERVER_ERROR
	};

	public class CodeErrorInfo
	{
		public CodeErrorInfoType Type { get; protected set; }
		public List<Application> MissingApplications { get; protected set; }

		public CodeErrorInfo () : base()
		{

		}

		public CodeErrorInfo (CodeErrorInfoType type, List<Application> MissingApplications=null) : base()
		{
			this.Type = type;

			if (MissingApplications != null) 
			{
				this.MissingApplications = MissingApplications;
			}
		}

		public bool HasMissingApplications()
		{
			return MissingApplications != null && MissingApplications.Count > 0;
		}
	}
}