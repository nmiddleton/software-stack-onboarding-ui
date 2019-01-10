/// 	$Id: ActionParameter.cs 13932 2010-11-24 11:47:19Z neil.middleton $
///       $LastChangedDate: 2010-11-24 11:47:19 +0000 (Wed, 24 Nov 2010) $
///  	$LastChangedRevision: 13932 $
///       $LastChangedBy: neil.middleton $
///       $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.Domain/Profile.cs $

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace TR.Profile.Domain
{
    [Serializable]
    public sealed class ActionParameter : ICloneable
    {
		private const string NAME_ALIAS = "PARAM_NAME";
        private const string VALUE_ALIAS = "VALUE";

		private const char DELIMITER = ':';

        #region ctors
        public ActionParameter()
        {
        }
        public ActionParameter(string pName, string pValue)
        {
            this.Name = pName;
            this.Value = pValue;
        }
        #endregion

        [XmlAttribute(NAME_ALIAS)]
        public string Name { get; set; }

        [XmlAttribute(VALUE_ALIAS)]
        public string Value { get; set; }

		[XmlIgnore]
		public string NameValue
		{
			get { return string.Format("{1}{0}{2}", DELIMITER, this.Name, this.Value); }
		}

		public override string ToString()
		{
			return this.NameValue;
		}

		public object Clone()
		{
			ActionParameter result = new ActionParameter();
			result.Name = (string)this.Name;
			result.Value = (string)this.Value;
			return result;
		}

	}

}
