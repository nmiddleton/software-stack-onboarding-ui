///
///                           $Id: LocalPropertiesContext.cs 13938 2010-11-24 15:18:38Z neil.middleton $
///              $LastChangedDate: 2010-11-24 15:18:38 +0000 (Wed, 24 Nov 2010) $
///          $LastChangedRevision: 13938 $
///                $LastChangedBy: neil.middleton $
///                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.UI/App_Code/MessageType.cs $
///

//#define DEBUG_ME

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.SessionState;
using TR.Profile.Domain;
using TR.Profile.Common.Data;
using TR.Profile.Common;
using TR.Profile.Data;
using System.ComponentModel;

namespace TR.Profile.UI
{
	public sealed class LocalPropertiesContext
	{
#if DEBUG_ME
		private const string DEBUG_CATEGORY = "[TR.Profile.UI.LocalPropertiesContext]";
#endif

		private const string KEY = "Local_Properties_Context";

		public static void Initialize(HttpSessionState pSession)
		{
			if (pSession == null)
				return;

			pSession[KEY] = Load();
		}
		public static void Initialize(HttpSessionState pSession, ICollection<LocalPropertyLookup> pItems)
		{
			if (pSession == null || pItems == null)
				return;

			pSession[KEY] = pItems;
		}
		public static ICollection<LocalPropertyLookup> Get(HttpSessionState pSession)
		{
			if (pSession == null)
				return null;

			return pSession[KEY] as ICollection<LocalPropertyLookup>;
		}

		public static List<LocalPropertyLookup> Load()
		{
			List<LocalPropertyLookup> items = new List<LocalPropertyLookup>();
			using (DataTransactionBase dt = new OracleDataTransaction(Configuration.StacksConnectionString))
			{
				LocalPropertyGateway gateway = new LocalPropertyGateway(dt.Transaction);
				gateway.Load(items);
			}

			return items;
		}

		[DataObjectMethod(DataObjectMethodType.Select)]
		public static ICollection<LocalPropertyLookup> Get(List<LocalPropertyLookup> Items)
		{
#if DEBUG_ME
			System.Diagnostics.Debug.WriteLine(string.Format("Get(); items:{0}", Items.Count), DEBUG_CATEGORY);
#endif
			return Items;
		}

		[DataObjectMethod(DataObjectMethodType.Update)]
		public static void UpdateDatacentreSuffix(List<LocalPropertyLookup> Items, string Infrastructure, string DatacentreSuffix)
		{
#if DEBUG_ME
			System.Diagnostics.Debug.WriteLine("UpdateDatacentreSuffix()", DEBUG_CATEGORY);
#endif

			Items
				.First((i) => i.Infrastructure == Infrastructure)
				.DatacentreSuffix = DatacentreSuffix;
		}

	}

}
