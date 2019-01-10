///
///                           $Id: ITIC.cs 14737 2011-01-20 16:12:06Z neil.middleton $
///              $LastChangedDate: 2011-01-20 16:12:06 +0000 (Thu, 20 Jan 2011) $
///          $LastChangedRevision: 14737 $
///                $LastChangedBy: neil.middleton $
///                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.Domain/ITIC.cs $
///

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace TR.Profile.Domain
{
    [Serializable]
    public sealed class ITIC
    {
        private const string DELIMITER = "_";

		private const string INFRASTRUCTURE_CODE_ALIAS = "GMI_INFRASTRUCTURE";
		private const string INFRASTRUCTURE_NAME_ALIAS = "GMI_INFRASTRUCTURE_NAME";
		private const string CAPABILITY_CODE_ALIAS = "GMI_CAPABILITY";
		private const string CAPABILITY_NAME_ALIAS = "GMI_CAPABILITY_NAME";
		private const string LOGICAL_SYSTEM_GROUP_CODE_ALIAS = "GMI_SYSTEM_GROUP";
		private const string LOGICAL_SYSTEM_GROUP_NAME_ALIAS = "GMI_SYSTEM_GROUP_NAME";

        #region ctor
        public ITIC(InfrastructureLookup pInfrastructure, CapabilityLookup pCapability, LogicalSystemGroupLookup pLogicalSystemGroup)
        {
            this.Infrastructure = pInfrastructure;
            this.Capability = pCapability;
            this.LogicalSystemGroup = pLogicalSystemGroup;
        }
        public ITIC()
        {
            this.Infrastructure = new InfrastructureLookup();
            this.Capability = new CapabilityLookup();
            this.LogicalSystemGroup = new LogicalSystemGroupLookup();
        }
        #endregion

        [XmlIgnore]
        public InfrastructureLookup Infrastructure { get; set; }
        [XmlAttribute(INFRASTRUCTURE_CODE_ALIAS)]
        public string InfrastructureCode
        {
            get { return (this.Infrastructure != null)? this.Infrastructure.Code : null; }
            set { this.Infrastructure = new InfrastructureLookup() { Code = value }; }
        }
		[XmlAttribute(INFRASTRUCTURE_NAME_ALIAS)]
		public string InfrastructureName
		{
			get { return (this.Infrastructure != null) ? this.Infrastructure.Name : null; }
			set { ; }
		}

        [XmlIgnore]
        public CapabilityLookup Capability { get; set; }
        [XmlAttribute(CAPABILITY_CODE_ALIAS)]
        public string CapabilityCode
        {
            get { return (this.Capability != null) ? this.Capability.Code : null; }
            set { this.Capability = new CapabilityLookup() { Code = value }; }
        }
		[XmlAttribute(CAPABILITY_NAME_ALIAS)]
		public string CapabilityName
		{
			get { return (this.Capability != null) ? this.Capability.Name : null; }
			set { ; }
		}

        [XmlIgnore]
        public LogicalSystemGroupLookup LogicalSystemGroup { get; set; }
        [XmlAttribute(LOGICAL_SYSTEM_GROUP_CODE_ALIAS)]
        public string LogicalSystemGroupCode
        {
            get { return (this.LogicalSystemGroup != null) ? this.LogicalSystemGroup.Code : null; }
            set { this.LogicalSystemGroup = new LogicalSystemGroupLookup() { Code = value }; }
        }
		[XmlAttribute(LOGICAL_SYSTEM_GROUP_NAME_ALIAS)]
		public string LogicalSystemGroupName
		{
			get { return (this.LogicalSystemGroup != null) ? this.LogicalSystemGroup.Name : null; }
			set { ; }
		}

        [XmlIgnore]
        public string Code
        {
            get
            {
                if (this.Infrastructure == null || this.Capability == null || this.LogicalSystemGroup == null)
                    return null;

                return string.Format("{1}{0}{2}{0}{3}", DELIMITER, this.Infrastructure.Code, this.Capability.Code, this.LogicalSystemGroup.Code);
            }
        }

    }

}
