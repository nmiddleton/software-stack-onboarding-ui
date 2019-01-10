///
///                           $Id: PlatformStacks.aspx.cs 14754 2011-01-21 10:45:55Z neil.middleton $
///              $LastChangedDate: 2011-01-21 10:45:55 +0000 (Fri, 21 Jan 2011) $
///          $LastChangedRevision: 14754 $
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
using TR.Profile.UI;
using TR.Profile.Common.Data;
using TR.Profile.Common;
using TR.Profile.Data;
using System.Drawing;

namespace TR.Profile.UI
{
	public partial class PlatformStacksPage : Page
	{
		private const string MESSAGE = "Specify Platform Stack";

		private Exception _exception = null;
		private IList<PlatformStack> _items = null;

		private Domain.Profile CurrentProfile
		{
			get { return this.Session[UIHelper.PROFILE_SESSION_KEY] as Domain.Profile; }
			set { this.Session[UIHelper.PROFILE_SESSION_KEY] = value; }
		}
		private PlatformStack SelectedPlatformStack
		{
			get { return _items.First((s) => s.Code == StacksRadioButtonList.SelectedValue); }
		}

		protected void Page_Init(object sender, EventArgs e)
		{
			PlatformStackHierarchy hierarchy = new PlatformStackHierarchy();
			try
			{
				using (DataTransactionBase dt = new OracleDataTransaction(Configuration.StacksConnectionString))
				{
					StackGatewayBase gateway = new PlatformStackGateway(dt.Transaction);
					gateway.Load(hierarchy);
				}
			}
			catch (Exception ex)
			{
				_exception = ex;
			}

			if (_exception == null)
			{
				string name = Domain.Profile.ExtractName(this.CurrentProfile.ITICCode.Code, this.CurrentProfile.Context.Name);
				_items = hierarchy.GatherPlatformStacksByName(name);
				_items.Insert(0, new PlatformStack());
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

			if (!this.IsPostBack)
			{
				ITICLabel.Text = this.CurrentProfile.ITICCode.Code;
				InfrastructureLabel.Text = this.CurrentProfile.ITICCode.Infrastructure.Name;
				CapabilityLabel.Text = this.CurrentProfile.ITICCode.Capability.Name;
				LogicalSystemGroupLabel.Text = this.CurrentProfile.ITICCode.LogicalSystemGroup.Name;
				FilenameLabel.Text = this.CurrentProfile.Context.Name;

				ImagesRepeater.DataSource = _items;
				StacksRadioButtonList.DataSource = _items;
				StacksRadioButtonList.DataTextField = "Code";
				StacksRadioButtonList.DataValueField = "Code";
				DataBind();

				StacksRadioButtonList.SelectedIndex = GetIndex(this.CurrentProfile.ActiveProvisioningPlan.PlatformStack.Code);
				UIHelper.SetMessage(MessageImage, MessageLabel, MessageType.None, null);
			}
		}

		protected void StacksRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
		{
			UIHelper.SetMessage(MessageImage, MessageLabel, MessageType.None, null);
		}

		protected void AcceptButton_Click(object sender, EventArgs e)
		{
			this.CurrentProfile.ActiveProvisioningPlan.PlatformStack = this.SelectedPlatformStack;

			Navigator.RedirectToApplicationStacksPage(this.CurrentProfile, this.Response);
		}

		private int GetIndex(string pCode)
		{
			int i = _items.Count - 1;
			while (i >= 0 && _items[i].Code != pCode)
			{
				i--;
			}
			return i;
		}

	}

}
