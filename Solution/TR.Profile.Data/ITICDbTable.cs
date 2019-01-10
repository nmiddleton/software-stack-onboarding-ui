///
///                           $Id: ITICDbTable.cs 14016 2010-11-26 14:07:39Z neil.middleton $
///              $LastChangedDate: 2010-11-26 14:07:39 +0000 (Fri, 26 Nov 2010) $
///          $LastChangedRevision: 14016 $
///                $LastChangedBy: neil.middleton $
///                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.Data/ITICDbTable.cs $
///

﻿using System;
using System.Collections.Generic;
using System.Linq;
using TR.Profile.Common.Data;

namespace TR.Profile.Data
{
    public sealed class ITICDbTable : IDbTable
    {
        private const string TABLE = "itic_short_code_map";

        public const string INFRASTRUCTURE_CODE_COLUMN = "infrastructure_short_code";
        public const string INFRASTRUCTURE_NAME_COLUMN = "infrastructure";
        public const string CAPABILITY_CODE_COLUMN = "capability_short_code";
        public const string CAPABILITY_NAME_COLUMN = "capability";
        public const string LOGICAL_SYSTEM_GROUP_CODE_COLUMN = "logical_system_short_code";
        public const string LOGICAL_SYSTEM_GROUP_NAME_COLUMN = "logical_system";

        private ICollection<string> _keyColumnNames = new List<string>
        {
            INFRASTRUCTURE_CODE_COLUMN,
            CAPABILITY_CODE_COLUMN,
            LOGICAL_SYSTEM_GROUP_CODE_COLUMN
        };
        private ICollection<string> _nonKeyColumnNames = new List<string>
        {
            INFRASTRUCTURE_NAME_COLUMN,
            CAPABILITY_NAME_COLUMN,
            LOGICAL_SYSTEM_GROUP_NAME_COLUMN
        };

        public string TableName
        {
            get { return TABLE; }
        }

        public ICollection<string> KeyColumnNames
        {
            get { return this._keyColumnNames; }
        }

        public ICollection<string> NonKeyColumnNames
        {
            get { return this._nonKeyColumnNames; }
        }

        public ICollection<string> ColumnNames
        {
            get
            {
                return (
                    from c in _keyColumnNames select c)
                    .Union(
                    from c in _nonKeyColumnNames select c)
                    .ToList<string>();
            }
        }

    }

}
