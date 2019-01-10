using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace TR.Profile.Domain
{
	[Serializable]
	public sealed class ProfileContext
	{
		private const string PLATFROM_NAME_ALIAS = "PLATFORM";
		private const string PLATFORM_VERSION_ALIAS = "PLATFORM_VERSION";
		private const string ARCHITECTURE_ALIAS = "ARCHITECTURE";
		private const string TR_BUILD_ALIAS = "TRBUILD";
		private const string PROFILE_VERSION_ALIAS = "PROFILE_VERSION";
		private const string NAME_ALIAS = "GMI_MANAGEMENT_PROFILE";

		private const char NAME_DELIMITER = '_';

		public ProfileContext()
		{
			this.ProfileVersion = "1";
		}

		[XmlAttribute(PLATFROM_NAME_ALIAS)]
		public string PlatformName { get; set; }
		[XmlAttribute(PLATFORM_VERSION_ALIAS)]
		public string PlatformVersion { get; set; }
		[XmlAttribute(ARCHITECTURE_ALIAS)]
		public string Architecture { get; set; }
		[XmlAttribute(TR_BUILD_ALIAS)]
		public string TRBuild { get; set; }

		[XmlAttribute(PROFILE_VERSION_ALIAS)]
		public string ProfileVersion { get; set; }

		[XmlAttribute(NAME_ALIAS)]
		public string Name { get; set; }

	}

}
