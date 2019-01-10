///
///                           $Id: ITICHierarchy.cs 14023 2010-11-26 15:17:41Z neil.middleton $
///              $LastChangedDate: 2010-11-26 15:17:41 +0000 (Fri, 26 Nov 2010) $
///          $LastChangedRevision: 14023 $
///                $LastChangedBy: neil.middleton $
///                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.Domain/ITICHierarchy.cs $
///

﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace TR.Profile.Domain
{
    public sealed class ITICHierarchy
    {
        #region ctor
        public ITICHierarchy()
        {
            this.Codes = new List<ITIC>();
            this.Infrastructures = new List<InfrastructureLookup>();
            this.Capabilities = new List<CapabilityLookup>();
            this.LogicalSystemGroups = new List<LogicalSystemGroupLookup>();
        }
        #endregion

		private object _codesLocker = new object();
        public ICollection<ITIC> Codes { get; private set; }

        public List<InfrastructureLookup> Infrastructures { get; private set; }
        public List<CapabilityLookup> Capabilities { get; private set; }
        public List<LogicalSystemGroupLookup> LogicalSystemGroups { get; private set; }

        public void AddITIC(InfrastructureLookup pInfrastructure, CapabilityLookup pCapability, LogicalSystemGroupLookup pLogicalSystemGroup)
        {
            if (pInfrastructure == null || pCapability == null || pLogicalSystemGroup == null)
                return;

			//ITIC code = this.Codes.FirstOrDefault((itic) => (itic.Infrastructure == pInfrastructure && itic.Capability == pCapability && itic.LogicalSystemGroup == pLogicalSystemGroup));
			//if (code == null)
			//{
			//    code = new ITIC(pInfrastructure, pCapability, pLogicalSystemGroup);
			//    this.Codes.Add(code);
			//}

			ITIC code = new ITIC(pInfrastructure, pCapability, pLogicalSystemGroup);
			lock (_codesLocker)
			{
				this.Codes.Add(code);
			}
		}

        public InfrastructureLookup GenerateInfrastructure(string pCode, string pName)
        {
            if (String.IsNullOrWhiteSpace(pCode) || String.IsNullOrWhiteSpace(pName))
                return null;

            InfrastructureLookup result = this.Infrastructures.FirstOrDefault((i) => (i.Code == pCode && i.Name == pName));
            if (result == null)
            {
                result = new InfrastructureLookup(pCode, pName);
                this.Infrastructures.Add(result);
            }
            return result;
        }
        public CapabilityLookup GenerateCapability(string pCode, string pName)
        {
            if (String.IsNullOrWhiteSpace(pCode) || String.IsNullOrWhiteSpace(pName))
                return null;

            CapabilityLookup result = this.Capabilities.FirstOrDefault((i) => (i.Code == pCode && i.Name == pName));
            if (result == null)
            {
                result = new CapabilityLookup(pCode, pName);
                this.Capabilities.Add(result);
            }
            return result;
        }
        public LogicalSystemGroupLookup GenerateLogicalSystemGroup(string pCode, string pName)
        {
            if (String.IsNullOrWhiteSpace(pCode) || String.IsNullOrWhiteSpace(pName))
                return null;

            LogicalSystemGroupLookup result = this.LogicalSystemGroups.FirstOrDefault((i) => (i.Code == pCode && i.Name == pName));
            if (result == null)
            {
                result = new LogicalSystemGroupLookup(pCode, pName);
                this.LogicalSystemGroups.Add(result);
            }
            return result;
        }

        public void SortLookups()
        {
            this.Infrastructures.Sort(InfrastructureLookup.CompareByName);
            this.Capabilities.Sort(CapabilityLookup.CompareByName);
            this.LogicalSystemGroups.Sort(LogicalSystemGroupLookup.CompareByName);
        }

        public InfrastructureLookup FindInfrastructureByCode(string pCode)
        {
            if (String.IsNullOrWhiteSpace(pCode))
                return null;

            return this.Infrastructures.FirstOrDefault((i) => i.Code == pCode);
        }
        public CapabilityLookup FindCapabilityByCode(string pCode)
        {
            if (String.IsNullOrWhiteSpace(pCode))
                return null;

            return this.Capabilities.FirstOrDefault((i) => i.Code == pCode);
        }
        public LogicalSystemGroupLookup FindLogicalSystemGroupByCode(string pCode)
        {
            if (String.IsNullOrWhiteSpace(pCode))
                return null;

            return this.LogicalSystemGroups.FirstOrDefault((i) => i.Code == pCode);
        }

    }

}
