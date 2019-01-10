///
///                           $Id: ProvisioningPlan.cs 13936 2010-11-24 14:57:22Z neil.middleton $
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
	public sealed class PlanActionLookup : ILookup
    {
        #region ctor
		public PlanActionLookup(string pCode, bool pIsSelected, string pDefaultValue) : this()
		{
			this.Code = pCode;
			this.IsSelected = pIsSelected;
			this.DefaultValue = pDefaultValue;
		}
		public PlanActionLookup()
        {
            this.IsLoaded = false;
        }
        #endregion

        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsLoaded { get; set; }

		public string Value
		{
			get { return (this.IsTextBox) ? this.DefaultValue : string.Empty; }
			set { ; }
		}

		public bool IsSelected { get; set; }

		public bool IsTextBox { get; set; }

		public string DefaultValue { get; set; }

		public decimal? RunOrder { get; set; }

	}

}
