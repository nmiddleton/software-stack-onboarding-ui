///
/// 	                        $Id: Profile.cs 14754 2011-01-21 10:45:55Z neil.middleton $
///        $LastChangedDate: 2011-01-21 10:45:55 +0000 (Fri, 21 Jan 2011) $
///  	$LastChangedRevision: 14754 $
///       $LastChangedBy: neil.middleton $
///       $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.Domain/Profile.cs $
///

using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using TR.Profile.Common;

namespace TR.Profile.Domain
{
    [Serializable]
    [XmlRoot("PROFILE")]
    public class Profile
    {
		private readonly Version STRUCTURE_VERSION = new Version(2, 3, 2, 3);

		private const string STRUCTURE_VERSION_ALIAS = "STRUCTURE_VERSION";
		private const string CREATED_AT_ALIAS = "CREATED_AT";
		private const string UPDATED_AT_ALIAS = "UPDATED_AT";

		private const string PROFILE_CONTEXT_ALIAS = "PROFILE_CONTEXT";

        private const string ITIC_CODE_ALIAS = "ITIC";
        private const string PROVISIONING_PLANS_ALIAS = "PROV_PLANS";
        private const string PROVISIONING_PLAN_ALIAS = "PROV_PLAN";

		private const string LOCAL_PROPERTIES_ALIAS = "LOCAL_PROPERTIES";
		private const string LOCAL_PROPERTY_ALIAS = "LOCAL_PROPERTY";
		internal const string LOCAL_PROPERTY_KEY_ALIAS = "PROPERTY_KEY";
		internal const string LOCAL_PROPERTY_NAME_ALIAS = "PROPERTY_NAME";
		internal const string LOCAL_PROPERTY_VALUE_ALIAS = "PROPERTY_VALUE";

		private const char NAME_DELIMITER = '_';

		#region ctor
        public Profile()
        {
			this.Context = new ProfileContext();
            this.ITICCode = new ITIC();

			this.ProvisioningPlans = new List<ProvisioningPlan>();
			this.LocalProperties = new List<LocalPropertyLookup>();
			this.CreatedAt = DateTime.Now;
			this.UpdatedAt = DateTime.Now;
		}
        #endregion

		[XmlIgnore]
		public string FileName
		{
			get { return string.Format("{1}{0}{2}", NAME_DELIMITER, this.Context.Name, this.Context.ProfileVersion); }
			set { ; }
		}

		[XmlAttribute(STRUCTURE_VERSION_ALIAS)]
		public string StructureVersion
		{
			get { return STRUCTURE_VERSION.ToString(); }
			set { ; }
		}
		[XmlAttribute(CREATED_AT_ALIAS)]
		public DateTime CreatedAt { get; set; }
		[XmlAttribute(UPDATED_AT_ALIAS)]
		public DateTime UpdatedAt { get; set; }

		[XmlElement(PROFILE_CONTEXT_ALIAS)]
		public ProfileContext Context { get; set; }

        [XmlElement(ITIC_CODE_ALIAS)]
        public ITIC ITICCode { get; set; }

		[XmlArray(PROVISIONING_PLANS_ALIAS)]
		[XmlArrayItem(PROVISIONING_PLAN_ALIAS, typeof(ProvisioningPlan))]
		public List<ProvisioningPlan> ProvisioningPlans { get; set; }

		[XmlArray(LOCAL_PROPERTIES_ALIAS)]
		[XmlArrayItem(LOCAL_PROPERTY_ALIAS, typeof(LocalPropertyLookup))]
		public List<LocalPropertyLookup> LocalProperties { get; set; }

		[XmlIgnore]
		public ProvisioningPlan ActiveProvisioningPlan { get; set; }

		[XmlIgnore]
		public string Xml
		{
			get
			{
				this.ProvisioningPlans.ForEach(p=>p.PopulatePlanActions());
				return Serializer.Serialize(this);
			}
		}

		public void SuppressXmlPreparation()
		{
			this.ProvisioningPlans.ForEach(p => p.SuppressXmlPreparation());
		}

		public ProvisioningPlan DuplicateProvisioningPlan(string pType, ProvisioningPlan pProvisioningPlan)
		{
			if (String.IsNullOrWhiteSpace(pType) || pProvisioningPlan == null)
				return null;

			ProvisioningPlan result = (ProvisioningPlan) pProvisioningPlan.Clone();
			result.Type = pType;
			this.ProvisioningPlans.Add(result);
			return result;
		}

		public static string BuildFullName(string pITICCode, string pName)
		{
			return string.Format("{1}{0}{2}", NAME_DELIMITER, pITICCode, pName);
		}
		public static string ExtractName(string pITICCode, string pFullName)
		{
			if (String.IsNullOrWhiteSpace(pITICCode) || String.IsNullOrWhiteSpace(pFullName))
				return pFullName;

			string code = string.Format("{0}{1}", pITICCode, NAME_DELIMITER);
			if (pFullName.Contains(code))
			{
				return pFullName.Replace(code, string.Empty);
			}
			else
			{
				return pFullName;
			}
		}

    }

}
