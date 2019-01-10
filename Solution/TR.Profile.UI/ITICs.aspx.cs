using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TR.Profile.Domain;
using System.Threading;
using System.Threading.Tasks;
using TR.Profile.Common.Data;
using TR.Profile.Data;
using TR.Profile.Common;

namespace TR.Profile.UI
{
	public partial class ITICs : Page
	{
		private const string INFRASTUCTURE_PAGE_STATE = "Infrastructure";
		private const string CAPABILITY_PAGE_STATE = "Capability";
		private const string LSG_PAGE_STATE = "LogicalSystemGroup";

		private Exception _exception = null;
		private ITICHierarchy _ITICHierarchy = null;

		private string SelectedInfrastructure
		{
			get { return (InfrastructureDropDownList.SelectedIndex >= 0) ? InfrastructureDropDownList.SelectedValue : null; }
		}
		private string SelectedCapability
		{
			get { return (CapabilityDropDownList.SelectedIndex >= 0) ? CapabilityDropDownList.SelectedValue : null; }
		}

		protected void Page_Init(object sender, EventArgs e)
		{
			using (EventWaitHandle synchronizer = new AutoResetEvent(false))
			{
				try
				{
					_ITICHierarchy = new ITICHierarchy();
					using (DataTransactionBase dt = new OracleDataTransaction(Configuration.ITICConnectionString))
					{
						ITICGateway gateway = new ITICGateway(dt.Transaction);
						gateway.Load(_ITICHierarchy);
					}
				}
				catch (Exception ex)
				{
					_exception = ex;
				}
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (_exception != null)
			{
				UIHelper.SetMessage(MessageImage, MessageLabel, MessageType.Error, _exception.Message);
				return;
			}

			if (!this.IsPostBack)
			{
				BindInfrastructure();
				UIHelper.SetMessage(MessageImage, MessageLabel, MessageType.None, null);
			}
		}

		protected void InfrastructureDropDownList_SelectedIndexChanged(object sender, EventArgs e)
		{
			SetPageState(CAPABILITY_PAGE_STATE);
		}
		protected void CapabilityDropDownList_SelectedIndexChanged(object sender, EventArgs e)
		{
			SetPageState(LSG_PAGE_STATE);
		}

		private void BindInfrastructure()
		{
			List<InfrastructureLookup> infrastructures =
				(from itic in _ITICHierarchy.Codes
				 group itic by itic.Infrastructure.Code into i
				 select i.First().Infrastructure)
				 .ToList<InfrastructureLookup>();
			infrastructures.Add(new InfrastructureLookup());
			infrastructures.Sort(InfrastructureLookup.CompareByName);

			InfrastructureDropDownList.DataSource = infrastructures;
			InfrastructureDropDownList.DataValueField = "Code";
			InfrastructureDropDownList.DataTextField = "Name";
			this.DataBind();
		}
		private void SetPageState(string pPageState)
		{
			if (pPageState == INFRASTUCTURE_PAGE_STATE)
			{
				EnableDisableCapability(false);
				EnableDisableLogicalSystemGroup(false);
				this.DataBind();
			}
			else if (pPageState == CAPABILITY_PAGE_STATE)
			{
				EnableDisableCapability(true);
				EnableDisableLogicalSystemGroup(false);
				this.DataBind();
			}
			else if (pPageState == LSG_PAGE_STATE)
			{
				EnableDisableLogicalSystemGroup(true);
				this.DataBind();
			}
		}
		private void EnableDisableCapability(bool pEnable)
		{
			if (pEnable)
			{
				List<CapabilityLookup> capabilities =
					(from itic in _ITICHierarchy.Codes
					 where itic.Infrastructure.Code == SelectedInfrastructure
					 group itic by itic.Capability.Code into c
					 select c.First().Capability)
					 .ToList<CapabilityLookup>();
				capabilities.Add(new CapabilityLookup());
				capabilities.Sort(CapabilityLookup.CompareByName);

				CapabilityDropDownList.DataSource = capabilities;
				CapabilityDropDownList.DataValueField = "Code";
				CapabilityDropDownList.DataTextField = "Name";
				CapabilityDropDownList.Enabled = true;
			}
			else
			{
				CapabilityDropDownList.DataSource = new List<CapabilityLookup> { new CapabilityLookup() };
				CapabilityDropDownList.DataValueField = "Code";
				CapabilityDropDownList.DataTextField = "Name";
				CapabilityDropDownList.Enabled = false;
			}
		}
		private void EnableDisableLogicalSystemGroup(bool pEnable)
		{
			if (pEnable)
			{
				List<LogicalSystemGroupLookup> lsg =
					(from itic in _ITICHierarchy.Codes
					 where itic.Capability.Code == SelectedCapability
					 group itic by itic.LogicalSystemGroup.Code into l
					 select l.First().LogicalSystemGroup)
					 .ToList<LogicalSystemGroupLookup>();
				lsg.Add(new LogicalSystemGroupLookup());
				lsg.Sort(LogicalSystemGroupLookup.CompareByName);

				LogicalSystemGroupDropDownList.DataSource = lsg;
				LogicalSystemGroupDropDownList.DataValueField = "Code";
				LogicalSystemGroupDropDownList.DataTextField = "Name";
				LogicalSystemGroupDropDownList.Enabled = true;
			}
			else
			{
				LogicalSystemGroupDropDownList.DataSource = new List<LogicalSystemGroupLookup> { new LogicalSystemGroupLookup() };
				LogicalSystemGroupDropDownList.DataValueField = "Code";
				LogicalSystemGroupDropDownList.DataTextField = "Name";
				LogicalSystemGroupDropDownList.Enabled = false;
			}
		}


	}

}