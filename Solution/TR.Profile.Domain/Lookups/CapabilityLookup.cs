///
///                           $Id: CapabilityLookup.cs 14380 2011-01-04 10:18:24Z neil.middleton $
///              $LastChangedDate: 2011-01-04 10:18:24 +0000 (Tue, 04 Jan 2011) $
///          $LastChangedRevision: 14380 $
///                $LastChangedBy: neil.middleton $
///                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.Domain/Lookups/CapabilityLookup.cs $
///

﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace TR.Profile.Domain
{
	[Serializable]
    public sealed class CapabilityLookup : ILookup
    {
		private const int CODE_MINIMUM_LENGTH = 3;
		private const int CODE_MAXIMUM_LENGTH = 4;

		private const string DEFAULT_CODE = "---";
        private const string DEFAULT_NAME = "--- select ---";

        #region ctor
        public CapabilityLookup(string pCode, string pName)
            : this()
        {
            this.Code = pCode;
            this.Name = pName;
        }
        public CapabilityLookup()
        {
            this.Code = DEFAULT_CODE;
            this.Name = DEFAULT_NAME;
            this.IsLoaded = false;
        }
        #endregion

        public string Code { get; set; }

        public string Name { get; set; }

        public bool IsLoaded { get; set; }

        public bool IsDefault
        {
            get { return this.Code == DEFAULT_CODE; }
        }

		public static bool ValidateCode(string pCode)
		{
			if (String.IsNullOrWhiteSpace(pCode))
				return false;

			if (pCode.Length < CODE_MINIMUM_LENGTH || pCode.Length > CODE_MAXIMUM_LENGTH)
				return false;

			return pCode
				.ToList<Char>()
				.TrueForAll((c) => Char.IsLetterOrDigit(c));
		}

		public static int CompareByName(CapabilityLookup x, CapabilityLookup y)
        {
            if (x == null || y == null || x.Name == null || y.Name == null)
                return 0;

            return x.Name.CompareTo(y.Name);
        }

    }

}
