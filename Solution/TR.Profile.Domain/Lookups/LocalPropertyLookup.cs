///
///                           $Id: LocalPropertyLookup.cs 14023 2010-11-26 15:17:41Z neil.middleton $
///              $LastChangedDate: 2010-11-26 15:17:41 +0000 (Fri, 26 Nov 2010) $
///          $LastChangedRevision: 14023 $
///                $LastChangedBy: neil.middleton $
///                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.Domain/Lookups/LogicalSystemGroupLookup.cs $
///

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace TR.Profile.Domain
{
	[Serializable]
	public sealed class LocalPropertyLookup : ILookup
	{
		private const string DEFAULT_CODE = "DNS";

		#region ctor
		public LocalPropertyLookup(string pInfrastructurePrefix, string pInfrastructureSuffix, string pDatacentreSuffix, string pClientsitedSuffix)
			: this()
		{
			this.InfrastructurePrefix = pInfrastructurePrefix;
			this.InfrastructureSuffix = pInfrastructureSuffix;
			this.DatacentreSuffix = pDatacentreSuffix;
			this.ClientsitedSuffix = pClientsitedSuffix;
		}
		public LocalPropertyLookup()
		{
			this.IsLoaded = false;
			this.Code = DEFAULT_CODE;
		}
		#endregion

		[XmlIgnore]
		public string InfrastructurePrefix { get; set; }
		[XmlIgnore]
		public string InfrastructureSuffix { get; set; }

		[XmlAttribute(Profile.LOCAL_PROPERTY_NAME_ALIAS)]
		public string Code { get; set; }

		[XmlAttribute(Profile.LOCAL_PROPERTY_KEY_ALIAS)]
		public string Infrastructure { get; set; }

		[XmlAttribute(Profile.LOCAL_PROPERTY_VALUE_ALIAS)]
		public string DatacentreSuffix { get; set; }

		[XmlIgnore]
		public string ClientsitedSuffix { get; set; }

		[XmlIgnore]
		public bool IsLoaded { get; set; }

	}

}
