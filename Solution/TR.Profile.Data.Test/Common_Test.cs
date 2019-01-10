///
///                           $Id: Common_Test.cs 13997 2010-11-25 10:42:25Z neil.middleton $
///              $LastChangedDate: 2010-11-25 10:42:25 +0000 (Thu, 25 Nov 2010) $
///          $LastChangedRevision: 13997 $
///                $LastChangedBy: neil.middleton $
///                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.Data.Test/ITIC_Test.cs $
///

﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TR.Profile.Common;
using System.Diagnostics;

namespace TR.Profile.Data.Test
{
    [TestClass]
    public class Common_Test
    {
		[TestMethod]
		public void IndexOfByNumber_Test()
		{
			const char DL = '_';
			const string TEST_STRING = "ABC_DEF_ZXDF_123_456";

			Debug.WriteLine(TEST_STRING.IndexOfByNumber(DL, 3));
			Assert.AreEqual(12, TEST_STRING.IndexOfByNumber(DL, 3));
		}

		[TestMethod]
		public void ConvertHyperLinks_Test()
		{
			string TEST_STRING = "ABC http://link.link DEF http://link.link, QWER http://link.link. HKLO http://link.link, ";

			Debug.WriteLine(TEST_STRING.ConvertHyperLinks());
		}

	}

}
