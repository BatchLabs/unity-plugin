using UnityEngine;
using System.Collections;

namespace Batch
{
	public class UserProfile
	{
		/// <summary>
		/// Sets and get a custom user identifier.
		/// </summary>
		public virtual string CustomUserID { get; set; }
		
		/// <summary>
		/// Sets and get an application language.
		/// </summary>
		public virtual string ApplicationLanguage {	get; set; }
		
		/// <summary>
		/// Sets and get an application region.
		/// </summary>
		public virtual string ApplicationRegion { get; set; }
	}
}