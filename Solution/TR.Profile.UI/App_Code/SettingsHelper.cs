///
///                           $Id: SettingsHelper.cs 14802 2011-01-24 14:54:50Z neil.middleton $
///              $LastChangedDate: 2011-01-24 14:54:50 +0000 (Mon, 24 Jan 2011) $
///          $LastChangedRevision: 14802 $
///                $LastChangedBy: neil.middleton $
///                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.UI/App_Code/SettingsHelper.cs $
///

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace TR.Profile.UI
{
    public sealed class SettingsHelper
    {
        #region singleton
        private static SettingsHelper _instance = null;
        public static SettingsHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SettingsHelper();
                }
                return _instance;
            }
        }
        private SettingsHelper()
        {
        }
        #endregion

        private readonly Version VERSION = new Version(0, 2, 6, 19);

        private const string ITIC_CONNECTION_STRING_KEY = "ITIC_db";
		private const string STACKS_CONNECTION_STRING_KEY = "Stacks_db";

		public const string XML_SCHEMA_FILENAME = "Profile.xsd";

        public Version Version
        {
            get { return VERSION; }
        }

        private string _ITICConnectionString = null;
        public string ITICConnectionString
        {
            get
            {
                if (_ITICConnectionString == null)
                {
                    try
                    {
                        _ITICConnectionString = ConfigurationManager.ConnectionStrings[ITIC_CONNECTION_STRING_KEY].ConnectionString;
                    }
                    catch
                    {
                        ;
                    }
                }
                return _ITICConnectionString;
            }
        }

        private string _stacksConnectionString = null;
        public string StacksConnectionString
        {
            get
            {
                if (_stacksConnectionString == null)
                {
                    try
                    {
                        _stacksConnectionString = ConfigurationManager.ConnectionStrings[STACKS_CONNECTION_STRING_KEY].ConnectionString;
                    }
                    catch
                    {
                        ;
                    }
                }
                return _stacksConnectionString;
            }
        }

		private const string HOME_PAGE_URL_KEY = "HOME_PAGE_URL";
		private string _homePageUrl = null;
		public string HomePageUrl
		{
			get
			{
				if (_homePageUrl == null)
				{
					try
					{
						_homePageUrl = ConfigurationManager.AppSettings[HOME_PAGE_URL_KEY];
					}
					catch
					{
						;
					}
				}
				return _homePageUrl;
			}
		}

	}

}
