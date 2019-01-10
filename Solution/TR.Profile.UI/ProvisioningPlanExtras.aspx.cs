///
///                           $Id: ProvisioningPlanExtras.aspx.cs 14089 2010-11-30 10:16:09Z neil.middleton $
///              $LastChangedDate: 2010-11-30 10:16:09 +0000 (Tue, 30 Nov 2010) $
///          $LastChangedRevision: 14089 $
///                $LastChangedBy: neil.middleton $
///                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.UI/PlatformStacks.aspx.cs $
///

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TR.Profile.Domain;
using TR.Profile.Common.Data;
using TR.Profile.Data;
using TR.Profile.Common;
using System.Drawing;
using System.Threading.Tasks;

namespace TR.Profile.UI
{
	public partial class ProvisioningPlanExtrasPage : Page
	{
		private Exception _exception = null;
		private ICollection<PlanActionLookup> _initialWorkflows = null;
		private ICollection<PlanActionLookup> _commonPlanActions = null;
		private ICollection<PlanActionLookup> _finalWorkflows = null;

		private Domain.Profile CurrentProfile
		{
			get { return this.Session[UIHelper.PROFILE_SESSION_KEY] as Domain.Profile; }
			set { this.Session[UIHelper.PROFILE_SESSION_KEY] = value; }
		}
		private IEnumerable<CheckBox> InitialWorkflowsCheckBoxes
		{
			get { return CollectCheckBoxes(InitailWorkflowsRepeater); }
		}
		private IEnumerable<CheckBox> CommonPlanActionsCheckBoxes
		{
			get { return CollectCheckBoxes(CommonPlanActionsRepeater); }
		}
		private IEnumerable<CheckBox> FinalWorkflowsCheckBoxes
		{
			get { return CollectCheckBoxes(FinalWorkflowsRepeater); }
		}

		protected void Page_Init(object sender, EventArgs e)
		{
			_initialWorkflows = new List<PlanActionLookup>();
			_commonPlanActions = new List<PlanActionLookup>();
			_finalWorkflows = new List<PlanActionLookup>();
			try
			{
				using (DataTransactionBase dt = new OracleDataTransaction(Configuration.StacksConnectionString))
				{
					ProvisioningPlanExtraGateway gateway = new ProvisioningPlanExtraGateway(dt.Transaction);
					gateway.Load(_initialWorkflows, _commonPlanActions, _finalWorkflows);
				}
			}
			catch (Exception ex)
			{
				_exception = ex;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (this.CurrentProfile == null)
			{
				Navigator.RedirectToDefaultPage(this.Response);
			}
			if (_exception != null)
			{
				UIHelper.SetMessage(MessageImage, MessageLabel, MessageType.Error, _exception.Message);
				return;
			}

			if (!IsPostBack)
			{
				// Init Controls
				ITICLabel.Text = this.CurrentProfile.ITICCode.Code;
				InfrastructureLabel.Text = this.CurrentProfile.ITICCode.Infrastructure.Name;
				CapabilityLabel.Text = this.CurrentProfile.ITICCode.Capability.Name;
				LogicalSystemGroupLabel.Text = this.CurrentProfile.ITICCode.LogicalSystemGroup.Name;
				FilenameLabel.Text = this.CurrentProfile.Context.Name;

				// Bindings
				InitailWorkflowsRepeater.DataSource = _initialWorkflows;
				CommonPlanActionsRepeater.DataSource = _commonPlanActions;
				FinalWorkflowsRepeater.DataSource = _finalWorkflows;
				DataBind();

				// Check CheckBoxes
				UpdateCheckBoxList(InitialWorkflowsCheckBoxes, this.CurrentProfile.ActiveProvisioningPlan.InitialWorkflows);
				UpdateCheckBoxList(CommonPlanActionsCheckBoxes, this.CurrentProfile.ActiveProvisioningPlan.CommonPlanActions);
				UpdateCheckBoxList(FinalWorkflowsCheckBoxes, this.CurrentProfile.ActiveProvisioningPlan.FinalWorkflows);

				// Set initial State
				UIHelper.SetMessage(MessageImage, MessageLabel, MessageType.None, null);
			}
		}

		protected void AcceptButton_Click(object sender, EventArgs e)
		{
			UpdateProfile();
			Navigator.RedirectToCustomerObjectsPage(this.CurrentProfile, this.Response);
		}
		protected void SubmitButton_Click(object sender, EventArgs e)
		{
			UpdateProfile();
			Navigator.RedirectToSummaryPage(this.CurrentProfile, this.Response);
		}

		private void UpdateProfile()
		{
			AddItems(InitialWorkflowsCheckBoxes, this.CurrentProfile.ActiveProvisioningPlan.InitialWorkflows, _initialWorkflows);
			AddItems(CommonPlanActionsCheckBoxes, this.CurrentProfile.ActiveProvisioningPlan.CommonPlanActions, _commonPlanActions);
			AddItems(FinalWorkflowsCheckBoxes, this.CurrentProfile.ActiveProvisioningPlan.FinalWorkflows, _finalWorkflows);
		}

		private void UpdateCheckBoxList(IEnumerable<CheckBox> pCheckBoxes, ICollection<PlanActionLookup> pItems)
		{
			if (pCheckBoxes == null || pItems == null)
				return;

			IEnumerator<CheckBox> en = pCheckBoxes.GetEnumerator();
			while (en.MoveNext())
			{
				PlanActionLookup lookup = pItems.FirstOrDefault((item) => item.Name == en.Current.Text);
				en.Current.Checked = (lookup != null) ? lookup.IsSelected : false;
			}
		}
		private void AddItems(IEnumerable<CheckBox> pCheckBoxes, ICollection<PlanActionLookup> pItems, ICollection<PlanActionLookup> pLookups)
		{
			if (pCheckBoxes == null || pItems == null || pLookups == null)
				return;

			pItems.Clear();
			IEnumerator<CheckBox> en = pCheckBoxes.GetEnumerator();
			while (en.MoveNext())
			{
				if (en.Current.Checked)
				{
					PlanActionLookup lookup = pLookups.First((item) => item.Name == en.Current.Text);
					lookup.IsSelected = true;
					pItems.Add(lookup);
				}
			}
		}

		private IEnumerable<CheckBox> CollectCheckBoxes(Repeater pRepeater)
		{
			if (pRepeater != null)
			{
				foreach (RepeaterItem repeaterItem in pRepeater.Items)
				{
					foreach (CheckBox checkBox in repeaterItem.Controls.OfType<CheckBox>())
					{
						yield return checkBox;
					}
				}
			}
		}

	}

}
