namespace Batch
{
	/// <summary>
	/// Reason for Batch failure
	/// </summary>
	public enum FailReason
	{
		/// <summary>
		/// Network is not available or not responding
		/// </summary>
		NETWORK_ERROR,

		/// <summary>
		/// Your API key is invalid
		/// </summary>
		INVALID_API_KEY,

		/// <summary>
		/// Your API key has been deactivated
		/// </summary>
		DEACTIVATED_API_KEY,

		/// <summary>
		/// The promocode is invalid
		/// </summary>
		INVALID_CODE,

		/// <summary>
		/// An unexpected error occured, a future retry can succeed 
		/// </summary>
		UNEXPECTED_ERROR,

		/// <summary>
		/// The requested code is not valid because we are not meeting conditions
		/// </summary>
		MISMATCH_CONDITIONS
	}
}

