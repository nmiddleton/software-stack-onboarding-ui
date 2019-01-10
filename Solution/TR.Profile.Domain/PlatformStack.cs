using System;
using System.Collections.Generic;
using System.Linq;

namespace TR.Profile.Domain
{
	[Serializable]
	public sealed class PlatformStack : Stack
	{
		#region ctor
		public PlatformStack(FilterLookup pMajorFilter, FilterLookup pMinorFilter, FilterLookup pArchitectureFilter, FilterLookup pBuildFilter, StackLookup pPlatformStackLookup)
			: base(pMajorFilter, pMinorFilter, pPlatformStackLookup)
		{
			this.Architecture = pArchitectureFilter;
			this.Build = pBuildFilter;
		}
		public PlatformStack()
			: base()
		{
			this.Architecture = new FilterLookup();
			this.Build = new FilterLookup();
		}
		#endregion

		public FilterLookup Architecture { get; set; }
		public FilterLookup Build { get; set; }

	}

}
