///
///                           $Id: PlanActionsContext.cs 13938 2010-11-24 15:18:38Z neil.middleton $
///              $LastChangedDate: 2010-11-24 15:18:38 +0000 (Wed, 24 Nov 2010) $
///          $LastChangedRevision: 13938 $
///                $LastChangedBy: neil.middleton $
///                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.UI/App_Code/MessageType.cs $
///

//#define DEBUG_ME

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using TR.Profile.Domain;
using System.Web.SessionState;
using System.ComponentModel;

namespace TR.Profile.UI
{
	public sealed class PlanActionsContext
	{
#if DEBUG_ME
		private const string DEBUG_CATEGORY = "[TR.Profile.UI.PlanActionsContext]";
#endif

		[DataObjectMethod(DataObjectMethodType.Select)]
		public static List<PlanAction> Get(Domain.Profile Profile)
		{
#if DEBUG_ME
			System.Diagnostics.Debug.WriteLine("Get()", DEBUG_CATEGORY);
#endif
			return (
				from a in Profile.ActiveProvisioningPlan.PlanActions
				where a.Order >= 0
				select a
				).ToList<PlanAction>();
		}

		[DataObjectMethod(DataObjectMethodType.Update)]
		public static void UpdateDatacentreSuffix(Domain.Profile Profile, string ParametersString, string MaxRetries, string ContinueOnError, string Order)
		{
#if DEBUG_ME
			System.Diagnostics.Debug.WriteLine(
				string.Format("UpdateDatacentreSuffix(ps:{0}, mr:{1}, coe:{2}, o:{3})", ParametersString, MaxRetries, ContinueOnError, Order),
				DEBUG_CATEGORY);
#endif

			int maxRetries = 0;
			try
			{
				maxRetries = Convert.ToInt32(MaxRetries);
			}
			catch
			{
				return;
			}

			int order = 0;
			try
			{
				order = Convert.ToInt32(Order);
			}
			catch
			{
				return;
			}

			List<PlanAction> planActions = Profile.ActiveProvisioningPlan.PlanActions;
			PlanAction action = planActions.First((i) => i.ParametersString == ParametersString);
			action.MaxRetries = maxRetries;
			action.ContinueOnError = ContinueOnError;
			action.Order = order;

			planActions.Sort(PlanAction.CompareByOrder);
			Profile.ActiveProvisioningPlan.PlanActions = planActions;
		}

		[DataObjectMethod(DataObjectMethodType.Delete)]
		public static void Delete(Domain.Profile Profile, string ParametersString)
		{
			Profile.ActiveProvisioningPlan.PlanActions.Remove(
				Profile.ActiveProvisioningPlan.PlanActions.First(p => p.ParametersString == ParametersString));
		}

	}

}
