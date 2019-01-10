using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace TR.Profile.Domain
{
	[Serializable]
	public sealed class ProvisioningPlan : ICloneable
	{
		private const string NAME_ALIAS = "PLAN_GROUP_NAME";
		private const string TYPE_ALIAS = "PLAN_GROUP_TYPE";

		private const string GLOBAL_PROPERTIES_ALIAS = "PROV_PLAN_PROPERTIES";
		private const string GLOBAL_PROPERTY_ALIAS = "PLAN_PROPERTY";

		private const string PLAN_ACTIONS_ALIAS = "PROV_PLAN_ACTIONS";
		private const string PLAN_ACTION_ALIAS = "ACTION";

		private const string STACK_PLAN_ACTION_NAME_VALUE = "installSoftware";
		private const string STACK_PARAMETER_NAME_VALUE = "softwareName";
		private const string WORKFLOW_ACTION_NAME_VALUE = "runWorkflow";
		private const string WORKFLOW_NAME_PARAMETER_NAME_VALUE = "workflowName";
		private const string WORKFLOW_DATA_PARAMETER_NAME_VALUE = "workflowData";
		private const string DISCOVERY_ACTION_NAME_VALUE = "runDiscovery";
		private const string DISCOVERY_PARAMETER_NAME_VALUE = "discoveryConfigurationName";

		private const string DEFAULT_TYPE = "enrolment";

		#region ctors
		public ProvisioningPlan()
		{
			this.Type = DEFAULT_TYPE;
			this.Properties = new List<GlobalPropertyLookup>();
			this.PlanActions = new List<PlanAction>();
			this.PlatformStack = new PlatformStack();
			this.ApplicationStacks = new List<Stack>();
			this.InitialWorkflows = new List<PlanActionLookup>();
			this.CommonPlanActions = new List<PlanActionLookup>();
			this.FinalWorkflows = new List<PlanActionLookup>();
			this.CustomStacks = new List<string>();
			this.CustomScans = new List<string>();
			this.Workflows = new List<ActionParameter>();
			this.AdvancedConfigurationActions = new List<PlanActionLookup>();
		}
		public ProvisioningPlan(string pType) : this()
		{
			this.Type = pType;
		}
		#endregion

		private bool _isPlanActionPopulated = false;

		[XmlAttribute(NAME_ALIAS)]
		public string Name { get; set; }
		[XmlAttribute(TYPE_ALIAS)]
		public string Type { get; set; }

		[XmlArray(GLOBAL_PROPERTIES_ALIAS)]
		[XmlArrayItem(GLOBAL_PROPERTY_ALIAS, typeof(GlobalPropertyLookup))]
		public List<GlobalPropertyLookup> Properties { get; set; }

		[XmlArray(PLAN_ACTIONS_ALIAS)]
		[XmlArrayItem(PLAN_ACTION_ALIAS, typeof(PlanAction))]
		public List<PlanAction> PlanActions { get; set; }

		[XmlIgnore]
		public PlatformStack PlatformStack { get; set; }

		[XmlIgnore]
		public ICollection<Stack> ApplicationStacks { get; set; }

		[XmlIgnore]
		public ICollection<PlanActionLookup> InitialWorkflows { get; set; }
		[XmlIgnore]
		public ICollection<PlanActionLookup> CommonPlanActions { get; set; }
		[XmlIgnore]
		public ICollection<PlanActionLookup> FinalWorkflows { get; set; }
		[XmlIgnore]
		public ICollection<PlanActionLookup> AdvancedConfigurationActions { get; set; }

		[XmlIgnore]
		public ICollection<string> CustomStacks { get; set; }
		[XmlIgnore]
		public ICollection<string> CustomScans { get; set; }
		[XmlIgnore]
		public ICollection<ActionParameter> Workflows { get; set; }

		public void PopulatePlanActions()
		{
			if (_isPlanActionPopulated)
				return;

			_isPlanActionPopulated = true;
			this.PlanActions.Clear();

			AddInitialWorkflows();
			AddPlatformStack();
			AddApplicationStacks();
			AddCommonPlanActions();
			AddCustomObjects();
			AddAdvancedConfigurationActions();
			AddFinalWorkflows();

			this.PlanActions.Sort(PlanAction.OrderComparison);
		}

		public void SuppressXmlPreparation()
		{
			_isPlanActionPopulated = true;
		}

		public object Clone()
		{
			ProvisioningPlan result = new ProvisioningPlan((string)this.Type.Clone());
			foreach (PlanAction item in this.PlanActions)
			{
				result.PlanActions.Add((PlanAction)item.Clone());
			}
			result.SuppressXmlPreparation();
			return result;
		}

		#region misc
		private void AddPlatformStack()
		{
			if (!this.PlatformStack.StackLookup.IsDefault)
			{
				PlanAction planAction = new PlanAction(this.PlanActions.Max(pa=>pa.Order) + 1, STACK_PLAN_ACTION_NAME_VALUE);
				planAction.AddParameter(STACK_PARAMETER_NAME_VALUE, this.PlatformStack.StackLookup.Code);
				this.PlanActions.Add(planAction);
			}
		}
		private void AddApplicationStacks()
		{
			foreach (Stack item in this.ApplicationStacks)
			{
				PlanAction planAction = new PlanAction(this.PlanActions.Max(pa => pa.Order) + 1, STACK_PLAN_ACTION_NAME_VALUE);
				planAction.AddParameter(STACK_PARAMETER_NAME_VALUE, item.Code);
				this.PlanActions.Add(planAction);
			}
		}
		private void AddInitialWorkflows()
		{
			foreach (PlanActionLookup item in this.InitialWorkflows)
			{
				PlanAction planAction = new PlanAction(
					(item.RunOrder.HasValue) ? Convert.ToInt32(item.RunOrder.Value) : this.PlanActions.Max(pa => pa.Order) + 1,
					WORKFLOW_ACTION_NAME_VALUE);
				planAction.AddParameter(WORKFLOW_NAME_PARAMETER_NAME_VALUE, item.Code);

				this.PlanActions.Add(planAction);
			}
		}
		private void AddCommonPlanActions()
		{
			foreach (PlanActionLookup item in this.CommonPlanActions)
			{
				PlanAction planAction = new PlanAction(
					(item.RunOrder.HasValue) ? Convert.ToInt32(item.RunOrder.Value) : this.PlanActions.Max(pa => pa.Order) + 1,
					DISCOVERY_ACTION_NAME_VALUE);
				planAction.AddParameter(DISCOVERY_PARAMETER_NAME_VALUE, item.Code);

				this.PlanActions.Add(planAction);
			}
		}
		private void AddFinalWorkflows()
		{
			foreach (PlanActionLookup item in this.FinalWorkflows)
			{
				PlanAction planAction = new PlanAction(
					(item.RunOrder.HasValue) ? Convert.ToInt32(item.RunOrder.Value) : this.PlanActions.Max(pa => pa.Order) + 1,
					WORKFLOW_ACTION_NAME_VALUE);
				planAction.AddParameter(WORKFLOW_NAME_PARAMETER_NAME_VALUE, item.Code);

				this.PlanActions.Add(planAction);
			}
		}
		private void AddCustomObjects()
		{
			foreach (string item in this.CustomStacks)
			{
				PlanAction planAction = new PlanAction(
					this.PlanActions.Max(pa => pa.Order) + 1, 
					STACK_PLAN_ACTION_NAME_VALUE);
				planAction.AddParameter(STACK_PARAMETER_NAME_VALUE, item);

				this.PlanActions.Add(planAction);
			}
			foreach (string item in this.CustomScans)
			{
				PlanAction planAction = new PlanAction(
					this.PlanActions.Max(pa => pa.Order) + 1, 
					DISCOVERY_ACTION_NAME_VALUE);
				planAction.AddParameter(DISCOVERY_PARAMETER_NAME_VALUE, item);

				this.PlanActions.Add(planAction);
			}
			foreach (ActionParameter item in this.Workflows)
			{
				PlanAction planAction = new PlanAction(
					this.PlanActions.Max(pa => pa.Order) + 1,
					WORKFLOW_ACTION_NAME_VALUE);
				planAction.Parameters.Add(item);

				this.PlanActions.Add(planAction);
			}
		}
		private void AddAdvancedConfigurationActions()
		{
			foreach (PlanActionLookup item in this.AdvancedConfigurationActions)
			{
				PlanAction planAction = new PlanAction(
					(item.RunOrder.HasValue) ? Convert.ToInt32(item.RunOrder.Value) : this.PlanActions.Max(pa => pa.Order) + 1,
					WORKFLOW_ACTION_NAME_VALUE);
				planAction.AddParameter(WORKFLOW_NAME_PARAMETER_NAME_VALUE, item.Code);
				planAction.AddParameter(WORKFLOW_DATA_PARAMETER_NAME_VALUE, item.Value);

				this.PlanActions.Add(planAction);
			}
		}
		#endregion

	}

}
