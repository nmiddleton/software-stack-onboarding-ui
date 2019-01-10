using System;
using System.Collections.Generic;
using System.Linq;

namespace TR.Profile.Domain
{
	public sealed class PlatformStackHierarchy : StackHierarchy
	{
		#region ctor
		public PlatformStackHierarchy()
			: base()
        {
            this.ArchitectureFilters = new List<FilterLookup>();
			this.BuildFilters = new List<FilterLookup>();
        }
        #endregion

		public List<FilterLookup> ArchitectureFilters { get; set; }
		public List<FilterLookup> BuildFilters { get; set; }

		public FilterLookup GenerateArchitectureFilter(string pCode)
		{
			if (String.IsNullOrWhiteSpace(pCode))
				return null;

			FilterLookup result = this.ArchitectureFilters.FirstOrDefault((i) => (i.Code == pCode));
			if (result == null)
			{
				result = new FilterLookup(pCode);
				this.ArchitectureFilters.Add(result);
			}
			return result;
		}
		public FilterLookup GenerateBuildFilter(string pCode)
		{
			if (String.IsNullOrWhiteSpace(pCode))
				return null;

			FilterLookup result = this.BuildFilters.FirstOrDefault((i) => (i.Code == pCode));
			if (result == null)
			{
				result = new FilterLookup(pCode);
				this.BuildFilters.Add(result);
			}
			return result;
		}

		public FilterLookup FindArchitectureByCode(string pCode)
		{
			if (String.IsNullOrWhiteSpace(pCode))
				return null;

			return this.ArchitectureFilters.FirstOrDefault((i) => i.Code == pCode);
		}
		public FilterLookup FindBuildByCode(string pCode)
		{
			if (String.IsNullOrWhiteSpace(pCode))
				return null;

			return this.BuildFilters.FirstOrDefault((i) => i.Code == pCode);
		}

		public void AddPlatformStack(FilterLookup pMajorFilter, FilterLookup pMinorFilter, FilterLookup pArchitectureFilter, FilterLookup pBuildFilter, StackLookup pPlatformStackLookup)
		{
			if (pMajorFilter == null || pMinorFilter == null || pArchitectureFilter == null || pBuildFilter == null || pPlatformStackLookup == null)
				return;

			Stack ps = base.Stacks.OfType<PlatformStack>().FirstOrDefault(
				(item) => (item.MajorFilter == pMajorFilter && item.MinorFilter == pMinorFilter && item.Architecture == pArchitectureFilter && item.Build == pBuildFilter && item.StackLookup == pPlatformStackLookup));
			if (ps == null)
			{
				ps = new PlatformStack(pMajorFilter, pMinorFilter, pArchitectureFilter, pBuildFilter, pPlatformStackLookup);
				this.Stacks.Add(ps);
			}
		}

		//[Obsolete("NOT TESTED !")]
		//public IList<FilterLookup> GatherMinorFilters(FilterLookup pMajorFilter)
		//{
		//    if (pMajorFilter == null)
		//        throw new ArgumentException("pMajorFilter");

		//    return 
		//        (
		//            from item in this.Stacks.OfType<PlatformStack>()
		//            where (pMajorFilter.IsDefault || (!pMajorFilter.IsDefault && item.MajorFilter.Code == pMajorFilter.Code)) 
		//            select item.MinorFilter
		//        )
		//        .ToList<FilterLookup>();
		//}
		//[Obsolete("NOT TESTED !")]
		//public IList<FilterLookup> GatherArchitectureFilters(FilterLookup pMajorFilter, FilterLookup pMinorFilter)
		//{
		//    if (pMajorFilter == null)
		//        throw new ArgumentException("pMajorFilter");
		//    if (pMinorFilter == null)
		//        throw new ArgumentException("pMinorFilter");

		//    return
		//        (
		//            from item in this.Stacks.OfType<PlatformStack>()
		//            where 
		//                (pMajorFilter.IsDefault || (!pMajorFilter.IsDefault && item.MajorFilter.Code == pMajorFilter.Code)) &&
		//                (pMinorFilter.IsDefault || (!pMinorFilter.IsDefault && item.MinorFilter.Code == pMinorFilter.Code))
		//            select item.Architecture
		//        )
		//        .ToList<FilterLookup>();
		//}
		//[Obsolete("NOT TESTED !")]
		//public IList<FilterLookup> GatherBuildFilters(FilterLookup pMajorFilter, FilterLookup pMinorFilter, FilterLookup pArchitectureFilter)
		//{
		//    if (pMajorFilter == null)
		//        throw new ArgumentException("pMajorFilter");
		//    if (pMinorFilter == null)
		//        throw new ArgumentException("pMinorFilter");
		//    if (pArchitectureFilter == null)
		//        throw new ArgumentException("pArchitectureFilter");

		//    return
		//        (
		//            from item in this.Stacks.OfType<PlatformStack>()
		//            where
		//                (pMajorFilter.IsDefault || (!pMajorFilter.IsDefault && item.MajorFilter.Code == pMajorFilter.Code)) && 
		//                (pMinorFilter.IsDefault || (!pMinorFilter.IsDefault && item.MinorFilter.Code == pMinorFilter.Code)) &&
		//                (pArchitectureFilter.IsDefault || (!pArchitectureFilter.IsDefault && item.Architecture.Code == pArchitectureFilter.Code))
		//            select item.Build
		//        )
		//        .ToList<FilterLookup>();
		//}

		public ICollection<StackLookup> GatherPlatformStacks(FilterLookup pMajorFilter, FilterLookup pMinorFilter, FilterLookup pArchirectureFilter, FilterLookup pBuildFilter)
		{
			return 
				(
					from item in base.Stacks.OfType<PlatformStack>()
					where 
						(pMajorFilter.IsDefault || (!pMajorFilter.IsDefault && item.MajorFilter.Code == pMajorFilter.Code)) &&
						(pMinorFilter.IsDefault || (!pMinorFilter.IsDefault && item.MinorFilter.Code == pMinorFilter.Code)) &&
						(pArchirectureFilter.IsDefault || (!pArchirectureFilter.IsDefault && item.Architecture.Code == pArchirectureFilter.Code)) &&
						(pBuildFilter.IsDefault || (!pBuildFilter.IsDefault && item.Build.Code == pBuildFilter.Code))
					select item.StackLookup
				).Distinct<StackLookup>(new StackLookupEqualityComparer())
				.ToList<StackLookup>();
		}

		public IList<PlatformStack> GatherPlatformStacksByName(string pName)
		{
			if (String.IsNullOrWhiteSpace(pName))
				return null;

			return
				(
					from item in base.Stacks.OfType<PlatformStack>()
					where item.Name == pName
					select item
				).ToList<PlatformStack>();
		}

	}

}
