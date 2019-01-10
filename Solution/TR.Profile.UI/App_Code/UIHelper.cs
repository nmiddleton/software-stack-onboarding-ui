///
///                           $Id: UIHelper.cs 14016 2010-11-26 14:07:39Z neil.middleton $
///              $LastChangedDate: 2010-11-26 14:07:39 +0000 (Fri, 26 Nov 2010) $
///          $LastChangedRevision: 14016 $
///                $LastChangedBy: neil.middleton $
///                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.UI/App_Code/Workflow.cs $
///

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using TR.Profile.Domain;
using System.Web.UI.WebControls;
using TR.Profile.Common.Data;
using TR.Profile.Data;
using TR.Profile.Common;

namespace TR.Profile.UI
{
	public static class UIHelper
	{
		public const string PROFILE_SESSION_KEY = "Profile";

		private const string ERROR_IMAGE_URL = "Images/Error24x24p.png";
		private const string WARNING_IMAGE_URL = "Images/Warning24x24p.png";
		private const double IMAGE_WIDTH = 24;
		private const double IMAGE_HEIGHT = 24;

		public static Domain.Profile PopulateProfile(HttpSessionState pSession)
		{
			if (pSession == null)
				return null;

			Domain.Profile result = new Domain.Profile();
			result.ProvisioningPlans = new List<ProvisioningPlan>
			{
				new ProvisioningPlan()
			};
			result.ActiveProvisioningPlan = result.ProvisioningPlans[0];

			PopulateDefaults(result);

			pSession[PROFILE_SESSION_KEY] = result;
			return result;
		}

		public static void SetMessage(Image pImage, Label pLabel, MessageType pType, string pMessage)
		{
			if (pImage == null || pLabel == null)
				return;

			if (pType == MessageType.None)
			{
				pImage.ImageUrl = null;
				pImage.Visible = false;
				pLabel.Text = pMessage;
				pLabel.ForeColor = System.Drawing.Color.Black;
			}
			else if (pType == MessageType.Error)
			{
				pImage.ImageUrl = ERROR_IMAGE_URL;
				pImage.Width = new Unit(IMAGE_WIDTH);
				pImage.Height = new Unit(IMAGE_HEIGHT);
				pImage.Visible = true;
				pLabel.Text = pMessage;
				pLabel.ForeColor = System.Drawing.Color.Red;
			}
			else if (pType == MessageType.Warning)
			{
				pImage.ImageUrl = WARNING_IMAGE_URL;
				pImage.Width = new Unit(IMAGE_WIDTH);
				pImage.Height = new Unit(IMAGE_HEIGHT);
				pImage.Visible = true;
				pLabel.Text = pMessage;
				pLabel.ForeColor = System.Drawing.Color.Blue;
			}
			else if (pType == MessageType.None)
			{
				pImage.ImageUrl = null;
				pImage.Visible = false;
				pLabel.Text = null;
				pLabel.ForeColor = System.Drawing.Color.Black;
			}
		}

		public static string UploadXml(FileUpload pControl)
		{
			if (pControl == null)
				return null;
			if (!pControl.HasFile)
				return null;

			byte[] buffer = new byte[pControl.PostedFile.ContentLength];
			pControl.FileContent.Read(buffer, 0, pControl.PostedFile.ContentLength);

			return System.Text.Encoding.ASCII.GetString(buffer);
		}

		public static void UpdateITIC(ITIC pITIC)
		{
			if (pITIC == null)
				return;

			pITIC.Infrastructure = new InfrastructureLookup(pITIC.InfrastructureCode, null);
			pITIC.Capability = new CapabilityLookup(pITIC.CapabilityCode, null);
			pITIC.LogicalSystemGroup = new LogicalSystemGroupLookup(pITIC.LogicalSystemGroupCode, null);

			using (DataTransactionBase dt = new OracleDataTransaction(Configuration.ITICConnectionString))
			{
				ITICGateway gateway = new ITICGateway(dt.Transaction);
				gateway.LoadInfrastructure(pITIC.Infrastructure);
				gateway.LoadCapability(pITIC.Capability);
				gateway.LoadLogicalSystemGroup(pITIC.LogicalSystemGroup);
			}
		}

		private static void PopulateDefaults(Domain.Profile pProfile)
		{
			if (pProfile == null)
				return;

			ICollection<PlanActionLookup> planActions = new List<PlanActionLookup>();
			List<GlobalPropertyLookup> globalProperties = new List<GlobalPropertyLookup>();
			ICollection<PlanActionLookup> initialWorkflows = new List<PlanActionLookup>();
			ICollection<PlanActionLookup> commonPlanActions = new List<PlanActionLookup>();
			ICollection<PlanActionLookup> finalWorkflows = new List<PlanActionLookup>();

			using (DataTransactionBase dt = new OracleDataTransaction(Configuration.StacksConnectionString))
			{
				ProvisioningPlanExtraGateway ppGateway = new ProvisioningPlanExtraGateway(dt.Transaction);
				ppGateway.Load(planActions);
				ppGateway.Load(initialWorkflows, commonPlanActions, finalWorkflows);

				GlobalPropertyGateway gpGateway = new GlobalPropertyGateway(dt.Transaction);
				gpGateway.Load(globalProperties);
			}

			pProfile.ActiveProvisioningPlan.AdvancedConfigurationActions.Clear();
			foreach (PlanActionLookup item in (from g in planActions where g.IsSelected select g))
			{
				pProfile.ActiveProvisioningPlan.AdvancedConfigurationActions.Add(item);
			}

			pProfile.ProvisioningPlans[0].Properties = globalProperties;

			pProfile.ActiveProvisioningPlan.InitialWorkflows = initialWorkflows;
			pProfile.ActiveProvisioningPlan.CommonPlanActions = commonPlanActions;
			pProfile.ActiveProvisioningPlan.FinalWorkflows = finalWorkflows;
		}

	}

}
