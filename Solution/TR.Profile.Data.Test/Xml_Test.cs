///
///                           $Id: XmlXslt_Test.cs 13997 2010-11-25 10:42:25Z neil.middleton $
///              $LastChangedDate: 2010-11-25 10:42:25 +0000 (Thu, 25 Nov 2010) $
///          $LastChangedRevision: 13997 $
///                $LastChangedBy: neil.middleton $
///                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.Data.Test/XsltHelper_Test.cs $
///

﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.IO;
using TR.Profile.Common;
using TR.Profile.Domain;
using System.Xml;
using System.Xml.Schema;

namespace TR.Profile.Data.Test
{
	[TestClass]
	public class Xml_Test
	{
		private const string XML_FILENAME = "Profile.xml";
		private const string XSD_FILENAME = "Profile.xsd";
		private const string XSLT_FILENAME = "Profile.xslt";
		private const string UI_FOLDER = "TR.Profile.UI";

		private static string XmlPath
		{
			get
			{
				string result = Path.Combine(Environment.CurrentDirectory, "..");
				result = Path.Combine(result, "..");
				result = Path.Combine(result, "..");
				result = Path.Combine(result, UI_FOLDER);
				result = Path.Combine(result, XML_FILENAME);

				return result;
			}
		}
		private static string XsdPath
		{
			get
			{
				string result = Path.Combine(Environment.CurrentDirectory, "..");
				result = Path.Combine(result, "..");
				result = Path.Combine(result, "..");
				result = Path.Combine(result, UI_FOLDER);
				result = Path.Combine(result, XSD_FILENAME);

				return result;
			}
		}
		private static string XsltPath
		{
			get
			{

				string result = Path.Combine(Environment.CurrentDirectory, "..");
				result = Path.Combine(result, "..");
				result = Path.Combine(result, "..");
				result = Path.Combine(result, UI_FOLDER);
				result = Path.Combine(result, XSLT_FILENAME);

				return result;
			}
		}

		[TestMethod]
		public void CreateXml_Test()
		{
			Domain.Profile profile = CreateTestProfile();
			Debug.WriteLine(profile.Xml);
		}

		[TestMethod]
		public void XsltTransform_Test()
		{
			string xslt = IOHelper.ReadText(XsltPath);

			Domain.Profile profile = CreateTestProfile();

			string html = XmlHelper.Transform(xslt, profile.Xml);

			Debug.WriteLine(profile.Xml);
			Debug.WriteLine("");
			Debug.WriteLine(html);
		}

		[TestMethod]
		public void ValidateXml_Test()
		{
			XmlHelper.ValidateXml(XmlReader.Create(XmlPath), XsdPath);
		}

		private Domain.Profile CreateTestProfile()
		{
			Domain.Profile result = new Domain.Profile();
			result.ProvisioningPlans = new List<ProvisioningPlan>
			{
				new ProvisioningPlan()
			};
			result.ActiveProvisioningPlan = result.ProvisioningPlans[0];

			// ITIC
			InfrastructureLookup inf = new InfrastructureLookup("III", "Test Infrastructure");
			CapabilityLookup cap = new CapabilityLookup("SSS", "Test Capability");
			LogicalSystemGroupLookup lsg = new LogicalSystemGroupLookup("LSG8", "Test LSG");
			result.ITICCode = new ITIC(inf, cap, lsg);

			// Platform Stack
			FilterLookup mjf = new FilterLookup("MjF");
			FilterLookup mnf = new FilterLookup("MiF");
			StackLookup ps = new StackLookup("WIN_x86_2008R_64_2.7.0_GREEN_SK", "Test Windows 2008 64bit Build 2.7.0", "Test descr");
			result.ActiveProvisioningPlan.PlatformStack = new PlatformStack(mjf, mnf, new FilterLookup("64bit"), new FilterLookup("2.7.0"), ps);

			// Application Stacks
			mjf = new FilterLookup("MjF2");
			mnf = new FilterLookup("MiF2");
			ps = new StackLookup("TEST_APP_STACK", "Test 2", "Test descr 2");
			result.ActiveProvisioningPlan.ApplicationStacks.Add(new Stack(mjf, mnf, ps));
			mjf = new FilterLookup("MjF3");
			mnf = new FilterLookup("MiF3");
			ps = new StackLookup("TEST_APP_STACK_3", "Test 3", "Test descr 3");
			result.ActiveProvisioningPlan.ApplicationStacks.Add(new Stack(mjf, mnf, ps));

			// Provisioning Plan Extras
			PlanActionLookup p = new PlanActionLookup() { Code = "CODE1", Name = "NAME1", Description = "DESc1", IsSelected = true };
			result.ActiveProvisioningPlan.InitialWorkflows.Add(p);
			p = new PlanActionLookup() { Code = "CODE2", Name = "NAME2", Description = "DESc2", IsSelected = true };
			result.ActiveProvisioningPlan.CommonPlanActions.Add(p);
			p = new PlanActionLookup() { Code = "CODE3", Name = "NAME3", Description = "DESc3", IsSelected = true };
			result.ActiveProvisioningPlan.FinalWorkflows.Add(p);

			// Post Configuration Actions
			PlanActionLookup pca = new PlanActionLookup() { Code = "PCA1", Name = "PCA1_NAME1", Description = "PCA1_DESc1", IsSelected = true, IsTextBox = true, DefaultValue = "TEST VALUE" };
			result.ActiveProvisioningPlan.AdvancedConfigurationActions.Add(pca);

			return result;
		}

	}

}

