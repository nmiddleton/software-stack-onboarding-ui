///
///                           $Id: FilterLookup.cs 13936 2010-11-24 14:57:22Z neil.middleton $
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
    public sealed class FilterLookup : ILookup
    {
		public const string DEFAULT_CODE = "--- select ---";

        #region ctor
        public FilterLookup(string pCode)
            : this()
        {
            this.Code = pCode;
        }
        public FilterLookup()
        {
            this.Code = DEFAULT_CODE;
            this.IsLoaded = false;
        }
        #endregion

        public string Code { get; set; }

        public bool IsLoaded { get; set; }
        public bool IsDefault
        {
            get { return this.Code == DEFAULT_CODE; }
        }

    }

}
