/// 	$Id: PlanAction.cs 13932 2010-11-24 11:47:19Z neil.middleton $
///       $LastChangedDate: 2010-11-24 11:47:19 +0000 (Wed, 24 Nov 2010) $
///  	$LastChangedRevision: 13932 $
///       $LastChangedBy: neil.middleton $
///       $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.Domain/Profile.cs $

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.Text;

namespace TR.Profile.Domain
{
    [Serializable]
    public sealed class PlanAction : ICloneable
    {
        private const string PARAMETER_ALIAS = "ACTION_PARAMETER";
        private const string ORDER_ALIAS = "RUNORDER";
		private const string NAME_ALIAS = "ACTION_TYPE_NAME";
        private const string CONTINUE_ON_ERROR_ALIAS = "CONTINUE_ON_ERROR";
        private const string MAX_RETRIES_ALIAS = "MAX_RETRIES";

        public const string CONTINUE = "Y";
        public const string DONOT_CONTINUE = "N";

		public const int MAX_RETRIES = 0;

		private const string PARAMETERS_DELIMITER = "; ";

        #region ctor
        public PlanAction()
        {
            this.Order = 1;
            this.Name = null;
            this.ContinueOnError = DONOT_CONTINUE;
            this.MaxRetries = MAX_RETRIES;

            this.Parameters = new List<ActionParameter>();
        }
        public PlanAction(int pOrder, string pName) : this()
        {
            this.Order = pOrder;
            this.Name = pName;
        }
		#endregion

        [XmlAttribute(ORDER_ALIAS)]
        public int Order { get; set; }

        [XmlAttribute(NAME_ALIAS)]
        public string Name { get; set; }

        [XmlAttribute(CONTINUE_ON_ERROR_ALIAS)]
        public string ContinueOnError { get; set; }

        [XmlAttribute(MAX_RETRIES_ALIAS)]
        public int MaxRetries { get; set; }

        [XmlElement(PARAMETER_ALIAS, typeof(ActionParameter))]
        public List<ActionParameter> Parameters { get; set; }

		[XmlIgnore]
		public string ParametersString
		{
			get
			{
				if (this.Parameters == null || this.Parameters.Count == 0)
					return null;

				StringBuilder result = new StringBuilder();
				this.Parameters.ForEach(
					(p) =>
					{
						if (result.Length > 0)
						{
							result.Append(PARAMETERS_DELIMITER);
						}
						result.Append(p);
					});
				return result.ToString();
			}
			set { ; }
		}

        public void AddParameter(string pName, string pValue)
        {
            if (String.IsNullOrWhiteSpace(pName))
                return;

            this.Parameters.Add(
                new ActionParameter(pName, pValue));
        }

		public static int CompareByOrder(PlanAction x, PlanAction y)
		{
			if (x == null || y == null)
				return 0;

			return x.Order.CompareTo(y.Order);
		}

		public static int OrderComparison(PlanAction x, PlanAction y)
		{
			if (x == null || y == null)
				return 0;

			return x.Order.CompareTo(y.Order);
		}

		public object Clone()
		{
			PlanAction result = new PlanAction();
			result.Order = this.Order;
			result.Name = (string)this.Name.Clone();
			result.ContinueOnError = (string)this.ContinueOnError.Clone();
			result.MaxRetries = this.MaxRetries;
			foreach (ActionParameter item in this.Parameters)
			{
				result.Parameters.Add((ActionParameter)item.Clone());
			}
			return result;
		}

	}

}
