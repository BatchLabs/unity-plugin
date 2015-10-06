using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using LitJson;
using Batch;

namespace Batch.Internal
{
	public class UnlockModule : ModuleBase
	{
		internal UnlockModule (Bridge _bridge) : base(_bridge)
		{
		}

		#region Handlers
		public event RedeemAutomaticOfferHandler 	RedeemAutomaticOffer;
		public event RedeemURLCodeFoundHandler 		RedeemURLCodeFound;
		public event RedeemURLSuccessHandler 		RedeemURLSuccess;
		public event RedeemURLFailedHandler 		RedeemURLFailed;
		public event RedeemCodeSuccessHandler 		RedeemCodeSuccess;
		public event RedeemCodeFailedHandler 		RedeemCodeFailed;
		public event RestoreSuccessHandler 			RestoreSuccess;
		public event RestoreFailedHandler 			RestoreFailed;
		#endregion

		/// <summary>
		/// Redeems offer using a code.
		/// </summary>
		/// <param name="code">Code.</param>
		public void RedeemCode(string code)
		{
			if ( RedeemCodeSuccess == null ) 
			{
				Logger.Error(false, "RedeemCode", "Cannot redeem a code without anything listening to RedeemCodeSuccess.");
				return;
			}
			
			if ( RedeemCodeFailed == null ) 
			{
				Logger.Error(false, "RedeemCode", "Cannot redeem a code without anything listening to RedeemCodeFailed.");
				return;
			}
			
			try
			{
				JsonData data = new JsonData();
				data["code"] = code;
				bridge.Call("redeemCode", JsonMapper.ToJson(data));
			}
			catch(Exception e)
			{
				Logger.Error(true, "RedeemCode", e);
			}
		}
		
		/// <summary>
		/// Restore previously unlocked features.
		/// </summary>
		public void Restore()
		{
			if ( RestoreSuccess == null ) 
			{
				Logger.Error(false, "Restore", "Cannot perform restore without anything listening to RestoreSuccess.");
				return;
			}
			
			if ( RestoreFailed == null ) 
			{
				Logger.Error(false, "Restore", "Cannot perform restore without anything listening to RestoreFailed.");
				return;
			}
			
			bridge.Call("restore", "");
		}

		public void OnRedeemAutomaticOffer(string response)
		{
			try
			{
				Response answer = new Response(response);
				
				// Treat the offer.
				Offer offer = answer.GetOffer();
				if (offer == null)
				{
					throw new NullReferenceException("The returned offer is null.");
				}
				
				if (RedeemAutomaticOffer != null)
				{
					RedeemAutomaticOffer(offer);
				}
			}
			catch (Exception e)
			{
				Logger.Error(true, "onRedeemAutomaticOffer", e);
			}
		}
		
		public void OnRedeemURLSuccess(string response)
		{
			try
			{
				Response answer = new Response(response);
				
				// Treat the offer.
				Offer offer = answer.GetOffer();
				if (offer == null)
				{
					throw new NullReferenceException("The returned offer is null.");
				}
				
				string code = answer.GetCode();
				if (code == null)
				{
					throw new NullReferenceException("The returned code is null.");
				}
				
				if (RedeemURLSuccess != null)
				{
					RedeemURLSuccess(code, offer);
				}
			}
			catch (Exception e)
			{
				Logger.Error(true, "onRedeemURLSuccess", e);
				if (RedeemURLFailed != null)
				{
					RedeemURLFailed(null, FailReason.UNEXPECTED_ERROR, null);
				}
			}
		}
		
		public void OnRedeemURLFailed(string response)
		{
			try
			{
				Response answer = new Response(response);
				
				FailReason failReason = answer.GetFailReason();
				
				string code = answer.GetFailedCode();
				if (code == null)
				{
					throw new NullReferenceException("The returned invalid code is null.");
				}
				
				if (RedeemURLFailed != null)
				{
					RedeemURLFailed(code, failReason, answer.GetCodeErrorInfos());
				}
			}
			catch (Exception e)
			{
				Logger.Error(true, "onRedeemURLFailed", e);
				if (RedeemURLFailed != null)
				{
					RedeemURLFailed(null, FailReason.UNEXPECTED_ERROR, null);
				}
			}
		}
		
		public void OnRedeemURLCodeFound(string response)
		{
			try
			{
				Response answer = new Response(response);
				
				string code = answer.GetCode();
				if (code == null)
				{
					throw new NullReferenceException("The returned code is null.");
				}
				
				if (RedeemURLCodeFound != null)
				{
					RedeemURLCodeFound(code);
				}
			}
			catch (Exception e)
			{
				Logger.Error(true, "onRedeemURLCodeFound", e);
			}
		}
		
		public void OnRedeemCodeSuccess(string response)
		{
			try
			{
				Response answer = new Response(response);
				
				// Treat the offer.
				Offer offer = answer.GetOffer();
				if (offer == null)
				{
					throw new NullReferenceException("The returned offer is null.");
				}
				
				string code = answer.GetCode();
				if (code == null)
				{
					throw new NullReferenceException("The returned code is null.");
				}
				
				if (RedeemCodeSuccess != null)
				{
					RedeemCodeSuccess(code, offer);
				}
			}
			catch (Exception e)
			{
				Logger.Error(true, "onRedeemCodeSuccess", e);
				if (RedeemCodeFailed != null)
				{
					RedeemCodeFailed(null, FailReason.UNEXPECTED_ERROR, null);
				}
			}
		}
		
		public void OnRedeemCodeFailed(string response)
		{
			try
			{
				Response answer = new Response(response);
				
				FailReason failReason = answer.GetFailReason();
				
				string code = answer.GetFailedCode();
				if (code == null)
				{
					throw new NullReferenceException("The returned invalid code is null.");
				}
				
				if (RedeemCodeFailed != null)
				{
					RedeemCodeFailed(code, failReason, answer.GetCodeErrorInfos());
				}
			}
			catch (Exception e)
			{
				Logger.Error(true, "onRedeemCodeFailed", e);
				if (RedeemCodeFailed != null)
				{
					RedeemCodeFailed(null, FailReason.UNEXPECTED_ERROR, null);
				}
			}
		}
		
		public void OnRestoreSuccess(string response)
		{
			try
			{
				Response answer = new Response(response);
				
				List<Feature> features = answer.GetFeatures();
				if (features == null)
				{
					throw new NullReferenceException("The returned features are null.");
				}
				
				if (RestoreSuccess != null)
				{
					RestoreSuccess(features);
				}
			}
			catch (Exception e)
			{
				Logger.Error(true, "onRestoreSuccess", e);
				
				if (RestoreFailed != null)
				{
					RestoreFailed(FailReason.UNEXPECTED_ERROR);
				}
			}
		}
		
		public void OnRestoreFailed(string response)
		{
			try
			{
				Response answer = new Response(response);
				
				FailReason failReason = answer.GetFailReason();
				
				if (RestoreFailed != null)
				{
					RestoreFailed(failReason);
				}
			}
			catch (Exception e)
			{
				Logger.Error(true, "onRestoreFailed", e);
				
				if (RestoreFailed != null)
				{
					RestoreFailed(FailReason.UNEXPECTED_ERROR);
				}
			}
		}

		public bool HasRedeemAutomaticOffer()
		{
			return RedeemAutomaticOffer != null;
		}
	}
}

