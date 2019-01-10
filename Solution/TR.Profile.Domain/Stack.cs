///
///                           $Id: Stack.cs 13939 2010-11-24 16:03:20Z neil.middleton $
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
	[Serializable]
    public class Stack
    {
        #region ctor
		public Stack(FilterLookup pMajorFilter, FilterLookup pMinorFilter, StackLookup pStackLookup)
        {
            this.MajorFilter = pMajorFilter;
            this.MinorFilter = pMinorFilter;
            this.StackLookup = pStackLookup;
        }
        public Stack()
        {
            this.MajorFilter = new FilterLookup();
			this.MinorFilter = new FilterLookup();
            this.StackLookup = new StackLookup();
        }
        #endregion

        public FilterLookup MajorFilter { get; set; }
		public FilterLookup MinorFilter { get; set; }
        public StackLookup StackLookup { get; set; }

        public string Code
        {
            get { return (this.StackLookup != null) ? this.StackLookup.Code : null; }
        }
        public string Name
        {
            get { return (this.StackLookup != null) ? this.StackLookup.Name : null; }
        }

    }

}
