///
///                           $Id: StackHierarchy.cs 13939 2010-11-24 16:03:20Z neil.middleton $
///              $LastChangedDate: 2010-11-24 16:03:20 +0000 (Wed, 24 Nov 2010) $
///          $LastChangedRevision: 13939 $
///                $LastChangedBy: neil.middleton $
///                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.Data/ITICGateway.cs $
///

using System;
using System.Collections.Generic;
using System.Linq;

namespace TR.Profile.Domain
{
    public class StackHierarchy
    {
        #region ctor
        public StackHierarchy()
        {
            this.Stacks = new List<Stack>();
            this.MajorFilters = new List<FilterLookup>();
			this.MinorFilters = new List<FilterLookup>();
            this.StackLookups = new List<StackLookup>();
        }
        #endregion

        public ICollection<Stack> Stacks { get; set; }

        public List<FilterLookup> MajorFilters { get; set; }
		public List<FilterLookup> MinorFilters { get; set; }
        public List<StackLookup> StackLookups { get; set; }

        public FilterLookup GenerateMajorFilter(string pCode)
        {
            if (String.IsNullOrWhiteSpace(pCode))
                return null;

            FilterLookup result = this.MajorFilters.FirstOrDefault((i) => (i.Code == pCode));
            if (result == null)
            {
                result = new FilterLookup(pCode);
                this.MajorFilters.Add(result);
            }
            return result;
        }
		public FilterLookup GenerateMinorFilter(string pCode)
        {
            if (String.IsNullOrWhiteSpace(pCode))
                return null;

			FilterLookup result = this.MinorFilters.FirstOrDefault((i) => (i.Code == pCode));
            if (result == null)
            {
				result = new FilterLookup(pCode);
                this.MinorFilters.Add(result);
            }
            return result;
        }
        public StackLookup GeneratePlatformStackLookup(string pCode, string pName, string pDescription)
        {
            if (String.IsNullOrWhiteSpace(pCode) || String.IsNullOrWhiteSpace(pName) || String.IsNullOrWhiteSpace(pDescription))
                return null;

            StackLookup result = this.StackLookups.FirstOrDefault((i) => (i.Code == pCode && i.Name == pName));
            if (result == null)
            {
                result = new StackLookup(pCode, pName, pDescription);
                this.StackLookups.Add(result);
            }
            return result;
        }

		public void AddStack(FilterLookup pMajorFilter, FilterLookup pMinorFilter, StackLookup pPlatformStackLookup)
        {
            if (pMajorFilter == null || pMinorFilter == null || pPlatformStackLookup == null)
                return;

            Stack ps = this.Stacks.FirstOrDefault((item) => (item.MajorFilter == pMajorFilter && item.MinorFilter == pMinorFilter && item.StackLookup == pPlatformStackLookup));
            if (ps == null)
            {
                ps = new Stack(pMajorFilter, pMinorFilter, pPlatformStackLookup);
                this.Stacks.Add(ps);
            }
        }

        public FilterLookup FindMajorFilterByCode(string pCode)
        {
            if (String.IsNullOrWhiteSpace(pCode))
                return null;

            return this.MajorFilters.FirstOrDefault((i) => i.Code == pCode);
        }
		public FilterLookup FindMinorFilterByCode(string pCode)
        {
            if (String.IsNullOrWhiteSpace(pCode))
                return null;

            return this.MinorFilters.FirstOrDefault((i) => i.Code == pCode);
        }
        public StackLookup FindPlatformStackLookupByCode(string pCode)
        {
            if (String.IsNullOrWhiteSpace(pCode))
                return null;

            return this.StackLookups.FirstOrDefault((i) => i.Code == pCode);
        }

    }

}
