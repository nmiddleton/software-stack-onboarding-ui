///
///                           $Id: Data_Test.cs 13997 2010-11-25 10:42:25Z neil.middleton $
///              $LastChangedDate: 2010-11-25 10:42:25 +0000 (Thu, 25 Nov 2010) $
///          $LastChangedRevision: 13997 $
///                $LastChangedBy: neil.middleton $
///                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.Data.Test/ITIC_Test.cs $
///

﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TR.Profile.Domain;
using TR.Profile.Common.Data;
using TR.Profile.Common;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;

namespace TR.Profile.Data.Test
{
    [TestClass]
    public class Data_Test
    {
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            Injector.InjectConfiguration();
        }

        [TestMethod]
        public void LoadITICs_Test()
        {
            ITICHierarchy hierarchy = new ITICHierarchy();

            using (DataTransactionBase dt = new OracleDataTransaction(Configuration.ITICConnectionString))
            {
                ITICGateway gateway = new ITICGateway(dt.Transaction);
                gateway.Load(hierarchy);
            }

            Assert.IsNotNull(hierarchy);
            Assert.IsNotNull(hierarchy.Codes);
            Assert.IsTrue(hierarchy.Codes.Count > 0);
			Debug.WriteLine("ITIC:{0}", hierarchy.Codes.Count);
            Assert.IsNotNull(hierarchy.Infrastructures);
            Assert.IsTrue(hierarchy.Infrastructures.Count > 0);
			Debug.WriteLine("Infrastructures:{0}", hierarchy.Infrastructures.Count);
			Assert.IsNotNull(hierarchy.Capabilities);
            Assert.IsTrue(hierarchy.Capabilities.Count > 0);
			Debug.WriteLine("Capabilities:{0}", hierarchy.Capabilities.Count);
			Assert.IsNotNull(hierarchy.LogicalSystemGroups);
            Assert.IsTrue(hierarchy.LogicalSystemGroups.Count > 0);
			Debug.WriteLine("LogicalSystemGroups:{0}", hierarchy.LogicalSystemGroups.Count);

            Assert.AreEqual(hierarchy.Infrastructures.Count, hierarchy.Infrastructures.Count((item) => item.IsLoaded));
            Assert.AreEqual(hierarchy.Capabilities.Count, hierarchy.Capabilities.Count((item) => item.IsLoaded));
            Assert.AreEqual(hierarchy.LogicalSystemGroups.Count, hierarchy.LogicalSystemGroups.Count((item) => item.IsLoaded));
        }

		[TestMethod]
		public void LoadPlatformStacks_Test()
		{
			PlatformStackHierarchy hierarchy = new PlatformStackHierarchy();

			using (DataTransactionBase dt = new OracleDataTransaction(Configuration.StacksConnectionString))
			{
				StackGatewayBase gateway = new PlatformStackGateway(dt.Transaction);
				gateway.Load(hierarchy);
			}

			Assert.IsNotNull(hierarchy);
			Assert.IsNotNull(hierarchy.Stacks);
			Assert.IsTrue(hierarchy.Stacks.Count > 0);
			Debug.WriteLine("PlatformStacks:{0}", hierarchy.Stacks.Count);
			Assert.IsNotNull(hierarchy.MajorFilters);
			Assert.IsTrue(hierarchy.MajorFilters.Count > 0);
			Debug.WriteLine("MajorFilters:{0}", hierarchy.MajorFilters.Count);
			Assert.IsNotNull(hierarchy.MinorFilters);
			Assert.IsTrue(hierarchy.MinorFilters.Count > 0);
			Debug.WriteLine("MinorFilters:{0}", hierarchy.MinorFilters.Count);
			Assert.IsNotNull(hierarchy.ArchitectureFilters);
			Assert.IsTrue(hierarchy.ArchitectureFilters.Count > 0);
			Debug.WriteLine("ArchitectureFilters:{0}", hierarchy.ArchitectureFilters.Count);
			Assert.IsNotNull(hierarchy.BuildFilters);
			Assert.IsTrue(hierarchy.BuildFilters.Count > 0);
			Debug.WriteLine("BuildFilters:{0}", hierarchy.BuildFilters.Count);
			Assert.IsNotNull(hierarchy.StackLookups);
			Assert.IsTrue(hierarchy.StackLookups.Count > 0);
			Debug.WriteLine("PlatformStackLookups:{0}", hierarchy.StackLookups.Count);

			Assert.AreEqual(hierarchy.MajorFilters.Count, hierarchy.MajorFilters.Count((item) => item.IsLoaded));
			Assert.AreEqual(hierarchy.MinorFilters.Count, hierarchy.MinorFilters.Count((item) => item.IsLoaded));
			Assert.AreEqual(hierarchy.ArchitectureFilters.Count, hierarchy.ArchitectureFilters.Count((item) => item.IsLoaded));
			Assert.AreEqual(hierarchy.BuildFilters.Count, hierarchy.BuildFilters.Count((item) => item.IsLoaded));
			Assert.AreEqual(hierarchy.Stacks.Count, hierarchy.StackLookups.Count((item) => item.IsLoaded));
			Assert.AreEqual(hierarchy.Stacks.OfType<PlatformStack>().Count(), hierarchy.Stacks.Count);
		}

		[TestMethod]
		public void LoadApplicationStacks_Test()
		{
			StackHierarchy hierarchy = new StackHierarchy();

			using (DataTransactionBase dt = new OracleDataTransaction(Configuration.StacksConnectionString))
			{
				StackGatewayBase gateway = new ApplicationStackGateway(dt.Transaction);
				gateway.Load(hierarchy);
			}

			Assert.IsNotNull(hierarchy);
			Assert.IsNotNull(hierarchy.Stacks);
			Assert.IsTrue(hierarchy.Stacks.Count > 0);
			Debug.WriteLine("ApplicationStacks:{0}", hierarchy.Stacks.Count);
			Assert.IsNotNull(hierarchy.MajorFilters);
			Assert.IsTrue(hierarchy.MajorFilters.Count > 0);
			Debug.WriteLine("MajorFilters:{0}", hierarchy.MajorFilters.Count);
			Assert.IsNotNull(hierarchy.MinorFilters);
			Assert.IsTrue(hierarchy.MinorFilters.Count > 0);
			Debug.WriteLine("MinorFilters:{0}", hierarchy.MinorFilters.Count);
			Assert.IsNotNull(hierarchy.StackLookups);
			Assert.IsTrue(hierarchy.StackLookups.Count > 0);
			Debug.WriteLine("ApplicationStackLookups:{0}", hierarchy.StackLookups.Count);

			Assert.AreEqual(hierarchy.MajorFilters.Count, hierarchy.MajorFilters.Count((item) => item.IsLoaded));
			Assert.AreEqual(hierarchy.MinorFilters.Count, hierarchy.MinorFilters.Count((item) => item.IsLoaded));
			Assert.AreEqual(hierarchy.Stacks.Count, hierarchy.StackLookups.Count((item) => item.IsLoaded));
		}

        [TestMethod]
        public void LoadITICsAndPlatformStacks_Test()
        {
			ITICHierarchy ITICHierarchy = new ITICHierarchy();
			PlatformStackHierarchy stackHierarchy = new PlatformStackHierarchy();

			using (EventWaitHandle synchronizer = new AutoResetEvent(false))
			{
				Task t1 = Task.Factory.StartNew(
					(obj) =>
					{
						ITICHierarchy h = obj as ITICHierarchy;
						if (h == null)
							return;

						using (DataTransactionBase dt = new OracleDataTransaction(Configuration.ITICConnectionString))
						{
							ITICGateway gateway = new ITICGateway(dt.Transaction);
							gateway.Load(h);
						}
					}, ITICHierarchy);

				Task t2 = Task.Factory.StartNew(
					(obj) =>
					{
						PlatformStackHierarchy h = obj as PlatformStackHierarchy;
						if (h == null)
							return;

						using (DataTransactionBase dt = new OracleDataTransaction(Configuration.StacksConnectionString))
						{
							StackGatewayBase gateway = new PlatformStackGateway(dt.Transaction);
							gateway.Load(h);
						}
					}, stackHierarchy);

				Task.Factory.ContinueWhenAll(new Task[] { t1, t2 }, (ts) => { synchronizer.Set(); });
				synchronizer.WaitOne();
			}

			Assert.IsNotNull(ITICHierarchy);
			Assert.IsNotNull(ITICHierarchy.Codes);
			Assert.IsTrue(ITICHierarchy.Codes.Count > 0);
			Assert.IsNotNull(ITICHierarchy.Infrastructures);
			Assert.IsTrue(ITICHierarchy.Infrastructures.Count > 0);
			Assert.IsNotNull(ITICHierarchy.Capabilities);
			Assert.IsTrue(ITICHierarchy.Capabilities.Count > 0);
			Assert.IsNotNull(ITICHierarchy.LogicalSystemGroups);
			Assert.IsTrue(ITICHierarchy.LogicalSystemGroups.Count > 0);

			Assert.IsNotNull(stackHierarchy);
			Assert.IsNotNull(stackHierarchy.Stacks);
			Assert.IsTrue(stackHierarchy.Stacks.Count > 0);
			Assert.IsNotNull(stackHierarchy.MajorFilters);
			Assert.IsTrue(stackHierarchy.MajorFilters.Count > 0);
			Assert.IsNotNull(stackHierarchy.MinorFilters);
			Assert.IsTrue(stackHierarchy.MinorFilters.Count > 0);
			Assert.IsNotNull(stackHierarchy.ArchitectureFilters);
			Assert.IsTrue(stackHierarchy.ArchitectureFilters.Count > 0);
			Assert.IsNotNull(stackHierarchy.BuildFilters);
			Assert.IsTrue(stackHierarchy.BuildFilters.Count > 0);
			Assert.IsNotNull(stackHierarchy.StackLookups);
			Assert.IsTrue(stackHierarchy.StackLookups.Count > 0);
		}

		[TestMethod]
		public void LoadProvisioningPlanExtras_Test()
		{
			ICollection<PlanActionLookup> initialWorkflows = new List<PlanActionLookup>();
			ICollection<PlanActionLookup> commonPlanActions = new List<PlanActionLookup>();
			ICollection<PlanActionLookup> finalWorkflows = new List<PlanActionLookup>();

			using (DataTransactionBase dt = new OracleDataTransaction(Configuration.StacksConnectionString))
			{
				ProvisioningPlanExtraGateway gateway = new ProvisioningPlanExtraGateway(dt.Transaction);
				gateway.Load(initialWorkflows, commonPlanActions, finalWorkflows);
			}

			Assert.IsNotNull(initialWorkflows);
			Assert.IsTrue(initialWorkflows.Count > 0);
			Debug.WriteLine("initialWorkflows:{0}", initialWorkflows.Count);
			Assert.IsNotNull(commonPlanActions);
			Assert.IsTrue(commonPlanActions.Count > 0);
			Debug.WriteLine("commonPlanActions:{0}", commonPlanActions.Count);
			Assert.IsNotNull(finalWorkflows);
			Assert.IsTrue(finalWorkflows.Count > 0);
			Debug.WriteLine("finalWorkflows:{0}", finalWorkflows.Count);

			Assert.AreEqual(initialWorkflows.Count, initialWorkflows.Count((item) => item.IsLoaded));
			Assert.AreEqual(commonPlanActions.Count, commonPlanActions.Count((item) => item.IsLoaded));
			Assert.AreEqual(finalWorkflows.Count, finalWorkflows.Count((item) => item.IsLoaded));
		}

		[TestMethod]
		public void LoadLocalProperties_Test()
		{
			ICollection<LocalPropertyLookup> items = new List<LocalPropertyLookup>();

			using (DataTransactionBase dt = new OracleDataTransaction(Configuration.StacksConnectionString))
			{
				LocalPropertyGateway gateway = new LocalPropertyGateway(dt.Transaction);
				gateway.Load(items);
			}

			Assert.IsNotNull(items);
			Assert.IsTrue(items.Count > 0);
			Debug.WriteLine("local properties:{0}", items.Count);

			Assert.AreEqual(items.Count, items.Count((item) => item.IsLoaded));
		}

		[TestMethod]
		public void LoadPostConfigurationActions_Test()
		{
			ICollection<PlanActionLookup> items = new List<PlanActionLookup>();
			using (DataTransactionBase dt = new OracleDataTransaction(Configuration.StacksConnectionString))
			{
				ProvisioningPlanExtraGateway gateway = new ProvisioningPlanExtraGateway(dt.Transaction);
				gateway.Load(items);
			}

			Assert.IsNotNull(items);
			Assert.IsTrue(items.Count > 0);
			Debug.WriteLine("Post Configuration Actions:{0}", items.Count);

			Assert.AreEqual(items.Count, items.Count((item) => item.IsLoaded));
		}

		[TestMethod]
		public void LoadGlobalProperties_Test()
		{
			ICollection<GlobalPropertyLookup> items = new List<GlobalPropertyLookup>();

			using (DataTransactionBase dt = new OracleDataTransaction(Configuration.StacksConnectionString))
			{
				GlobalPropertyGateway gateway = new GlobalPropertyGateway(dt.Transaction);
				gateway.Load(items);
			}

			Assert.IsNotNull(items);
			Assert.IsTrue(items.Count > 0);
			Debug.WriteLine("global properties:{0}", items.Count);

			Assert.AreEqual(items.Count, items.Count((item) => item.IsLoaded));
		}

		[TestMethod]
		public void LoadInfrastructure_Test()
		{
			const string CODE = "DCM";
			const string NAME = "DCMS";

			InfrastructureLookup lookup = new InfrastructureLookup(CODE, null);
			using (DataTransactionBase dt = new OracleDataTransaction(Configuration.ITICConnectionString))
			{
				ITICGateway gateway = new ITICGateway(dt.Transaction);
				gateway.LoadInfrastructure(lookup);
			}

			Assert.IsNotNull(lookup);
			Assert.AreEqual(CODE, lookup.Code);
			Assert.AreEqual(NAME, lookup.Name);
			Assert.IsTrue(lookup.IsLoaded);
			Assert.IsFalse(lookup.IsDefault);
		}

	}

}
