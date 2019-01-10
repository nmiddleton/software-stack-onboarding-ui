///
///                           $Id: GlobalPropertyLookup.cs 14023 2010-11-26 15:17:41Z neil.middleton $
///              $LastChangedDate: 2010-11-26 15:17:41 +0000 (Fri, 26 Nov 2010) $
///          $LastChangedRevision: 14023 $
///                $LastChangedBy: neil.middleton $
///                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.Domain/Lookups/LogicalSystemGroupLookup.cs $
///

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace TR.Profile.Domain
{
	[Serializable]
	public sealed class GlobalPropertyLookup : ILookup
	{
		public const string TEXTBOX_TYPE = "TEXT";
		public const string CHECKBOX_TYPE = "BOOLEAN";

		internal const string PROPERTY_KEY_ALIAS = "PROPERTY_NAME";
		internal const string PROPERTY_NAME_ALIAS = "PROPERTY_TITLE";
		internal const string PROPERTY_DESCRIPTION_ALIAS = "PROPERTY_DESCRIPTION";
		internal const string PROPERTY_VALUE_ALIAS = "PROPERTY_VALUE";

		#region ctor
        public GlobalPropertyLookup(string pCode, string pName, string pDescription)
            : this()
        {
            this.Code = pCode;
			this.Name = pName;
			this.Description = pDescription;
		}
		public GlobalPropertyLookup()
        {
            this.IsLoaded = false;
        }
        #endregion

		[XmlAttribute(PROPERTY_KEY_ALIAS)]
        public string Code { get; set; }

		[XmlAttribute(PROPERTY_VALUE_ALIAS)]
		public string Value
		{
			get { return (this.IsTextBox) ? this.DefaultValue : this.IsSelected.ToString().ToUpper(); }
			set
			{
				bool b;
				try
				{
					b = Convert.ToBoolean(value);
				}
				catch
				{
					this.DataType = TEXTBOX_TYPE;
					this.DefaultValue = value;
					return;
				}
				this.DataType = CHECKBOX_TYPE;
				this.IsSelected = b;
			}
		}

		[XmlAttribute(PROPERTY_NAME_ALIAS)]
		public string Name { get; set; }

		[XmlAttribute(PROPERTY_DESCRIPTION_ALIAS)]
		public string Description { get; set; }

		[XmlIgnore]
		public bool IsLoaded { get; set; }

		[XmlIgnore]
		public bool IsSelected { get; set; }

		[XmlIgnore]
		public string DataType { get; set; }
		[XmlIgnore]
		public bool IsTextBox
		{
			get { return (this.DataType == TEXTBOX_TYPE); }
		}
		[XmlIgnore]
		public bool IsCheckBox
		{
			get { return (this.DataType == CHECKBOX_TYPE); }
		}

		[XmlIgnore]
		public string DefaultValue { get; set; }

	}

}
