///
///                           $Id: PlatformStackDbTable.cs 13939 2010-11-24 16:03:20Z neil.middleton $
///              $LastChangedDate: 2010-11-24 16:03:20 +0000 (Wed, 24 Nov 2010) $
///          $LastChangedRevision: 13939 $
///                $LastChangedBy: neil.middleton $
///                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.Data/ITICDbTable.cs $
///

using System;
using System.Collections.Generic;
using System.Linq;

namespace TR.Profile.Data
{
	public sealed class PlatformStackDbTable : StackDbTable
	{
		private const string TABLE = "green_stacks_platform_view";

		public const string BUILD_COLUMN = "guitrbuild";
		public const string ARCHITECTURE_COLUMN = "guiarchitecture";

		public PlatformStackDbTable()
		{
			_nonKeyColumnNames.Add(BUILD_COLUMN);
			_nonKeyColumnNames.Add(ARCHITECTURE_COLUMN);
		}

		public override string TableName
		{
			get { return TABLE; }
		}

	}

}
