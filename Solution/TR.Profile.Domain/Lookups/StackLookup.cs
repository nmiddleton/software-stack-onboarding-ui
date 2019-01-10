///
///                           $Id: PlatformStackLookup.cs 13936 2010-11-24 14:57:22Z neil.middleton $
///              $LastChangedDate: 2010-11-24 14:57:22 +0000 (Wed, 24 Nov 2010) $
///          $LastChangedRevision: 13936 $
///                $LastChangedBy: neil.middleton $
///                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.Domain/Lookups/CapabilityLookup.cs $
///

using System;
using System.Collections.Generic;
using System.Linq;

namespace TR.Profile.Domain
{
	[Serializable]
	public sealed class StackLookup : ILookup
	{
		public const string DEFAULT_CODE = "NONE";
		private const string DEFAULT_NAME = "NONE";

		#region ctor
		public StackLookup(string pCode, string pName, string pDescription)
			: this()
		{
			this.Code = pCode;
			this.Name = pName;
			this.Description = pDescription;
		}
		public StackLookup()
		{
			this.Code = DEFAULT_CODE;
			this.Name = DEFAULT_NAME;
			this.Description = DEFAULT_NAME;
			this.IsLoaded = false;
		}
		#endregion

		public string Code { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public bool IsLoaded { get; set; }
		public bool IsDefault
		{
			get { return this.Code == DEFAULT_CODE; }
		}

		public static int ComparerByName(StackLookup x, StackLookup y)
		{
			return x.Name.CompareTo(y.Name);
		}
	}

	public sealed class StackLookupEqualityComparer : IEqualityComparer<StackLookup>
	{
		public bool Equals(StackLookup x, StackLookup y)
		{
			if (x == null || y == null)
			{
				return false;
			}
			else
			{
				return x.Name.ToUpper().Trim().Equals(y.Name.ToUpper().Trim());
			}
		}

		public int GetHashCode(StackLookup x)
		{
			return x.Name.ToUpper().Trim().GetHashCode();
		}
	}

}