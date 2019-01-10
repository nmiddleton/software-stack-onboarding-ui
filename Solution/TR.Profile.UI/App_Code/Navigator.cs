///
///                           $Id: Navigator.cs 13938 2010-11-24 15:18:38Z neil.middleton $
///              $LastChangedDate: 2010-11-24 15:18:38 +0000 (Wed, 24 Nov 2010) $
///          $LastChangedRevision: 13938 $
///                $LastChangedBy: neil.middleton $
///                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.UI/App_Code/MessageType.cs $
///

using System;
using System.Collections.Generic;
using System.Linq;
using TR.Profile.Domain;
using System.Web;
using TR.Profile.Common;

namespace TR.Profile.UI
{
	public static class Navigator
	{
		private const string DEFAULT_PAGE_URL = "Default.aspx";
		private const string ITIC_PAGE_URL = "ITIC.aspx";
		public const string PLATFORM_STACKS_PAGE_URL = "PlatformStacks.aspx";
		public const string APPLICATION_STACKS_PAGE_URL = "ApplicationStacks.aspx";
		public const string PROVISIONING_PLAN_EXTRAS_PAGE_URL = "ProvisioningPlanExtras.aspx";
		public const string ADVANCED_SETTINGS_PAGE_URL = "AdvancedSettings.aspx";
		public const string CUSTOMER_OBJECTS_PAGE_URL = "CustomerObjects.aspx";
		public const string ADVANCED_CONFIGURATION_ACTIONS_PAGE_URL = "AdvancedConfigurationActions.aspx";
		public const string DOMAIN_NAME_RESOLUTION_PAGE_URL = "DomainNameResolution.aspx";
		public const string SUMMARY_PAGE_URL = "Summary.aspx";
		public const string ADVANCED_SUMMARY_PAGE_URL = "AdvancedSummary.aspx";
		private const string DOCUMENT_PAGE_URL = "Document.aspx";
		private const string NEW_PROVISIONING_PLAN_PAGE_URL = "NewProvisioningPlan.aspx";

		public const string BACK_TO_PARAMETER = "BackTo";

		public static void RedirectToHomePage(HttpResponse pResponse)
		{
			if (pResponse == null)
				return;

			pResponse.Redirect(Configuration.HomePageUrl);
		}

		public static void RedirectToDefaultPage(HttpResponse pResponse)
		{
			if (pResponse == null)
				return;

			pResponse.Redirect(DEFAULT_PAGE_URL);
		}
		public static void RedirectToITICPage(Domain.Profile pProfile, HttpResponse pResponse)
		{
			if (pProfile == null || pResponse == null)
				return;

			//if (pProfile.StructureVersion.Major == 0)
			//    pResponse.Redirect(ITIC_0_PAGE_URL);
			//else
			//    ;
			pResponse.Redirect(ITIC_PAGE_URL);
		}
		public static void RedirectToPlatformStacksPage(Domain.Profile pProfile, HttpResponse pResponse)
		{
			if (pProfile == null || pResponse == null)
				return;

			//if (pProfile.StructureVersion.Major == 0)
			//    pResponse.Redirect(PLATFORM_STACKS_0_PAGE_URL);
			//else
			//    ;
			pResponse.Redirect(PLATFORM_STACKS_PAGE_URL);
		}
		public static void RedirectToApplicationStacksPage(Domain.Profile pProfile, HttpResponse pResponse)
		{
			if (pProfile == null || pResponse == null)
				return;

			pResponse.Redirect(APPLICATION_STACKS_PAGE_URL);
		}
		public static void RedirectToProvisioningPlanExtrasPage(Domain.Profile pProfile, HttpResponse pResponse)
		{
			if (pProfile == null || pResponse == null)
				return;

			pResponse.Redirect(PROVISIONING_PLAN_EXTRAS_PAGE_URL);
		}
		public static void RedirectToCustomerObjectsPage(Domain.Profile pProfile, HttpResponse pResponse)
		{
			if (pProfile == null || pResponse == null)
				return;

			pResponse.Redirect(CUSTOMER_OBJECTS_PAGE_URL);
		}
		public static void RedirectToAdvancedConfigurationActionsPage(Domain.Profile pProfile, HttpResponse pResponse)
		{
			if (pProfile == null || pResponse == null)
				return;

			pResponse.Redirect(ADVANCED_CONFIGURATION_ACTIONS_PAGE_URL);
		}
		public static void RedirectToDomainNameResolutionPage(Domain.Profile pProfile, HttpResponse pResponse)
		{
			if (pProfile == null || pResponse == null)
				return;

			pResponse.Redirect(DOMAIN_NAME_RESOLUTION_PAGE_URL);
		}
		public static void RedirectToSummaryPage(Domain.Profile pProfile, HttpResponse pResponse)
		{
			if (pProfile == null || pResponse == null)
				return;

			pResponse.Redirect(SUMMARY_PAGE_URL);
		}
		public static void RedirectToAdvancedSummaryPage(Domain.Profile pProfile, HttpResponse pResponse)
		{
			if (pProfile == null || pResponse == null)
				return;

			pResponse.Redirect(ADVANCED_SUMMARY_PAGE_URL);
		}

		public static void RedirectToDocumentPage(Domain.Profile pProfile, HttpResponse pResponse, string pBackPage)
		{
			if (pProfile == null || pResponse == null)
				return;

			string url = string.Format("{0}?{1}={2}", DOCUMENT_PAGE_URL, BACK_TO_PARAMETER, pBackPage);
			pResponse.Redirect(url);
		}

		public static void RedirectToNewProvisioningPlanPage(Domain.Profile pProfile, HttpResponse pResponse)
		{
			if (pProfile == null || pResponse == null)
				return;

			pResponse.Redirect(NEW_PROVISIONING_PLAN_PAGE_URL);
		}

	}

}
