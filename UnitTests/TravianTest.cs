using System;
using libTravian;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace UnitTests
{
  
    /// <summary>
    ///This is a test class for TravianTest and is intended
    ///to contain all TravianTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TravianTest
    {

        /// <summary>
        ///A test for GetBuildingLevel
        ///</summary>
        [TestMethod()]
        public void GetBuildingLevelCN()
        {
            Travian target = new Travian();
            int gid = 17;
            string pageContent = "<h1>市场 <span class=\"level\">等级 20</span></h1>";
            target.SetGidLang(gid, "市场");
            Assert.AreEqual(20, target.GetBuildingLevel(gid, pageContent));

            target.SetGidLang(gid, "XX");
            Assert.AreEqual(-1, target.GetBuildingLevel(gid, pageContent));
        }

        /// <summary>
        ///A test for GetBuildingLevel
        ///</summary>
        [TestMethod()]
        public void GetBuildingLevelUS()
        {
            Travian target = new Travian();
            int gid = 5;
            string pageContent = "<h1>Main Building <span class=\"level\">level 3</span></h1>";
            target.SetGidLang(gid, "Main Building");
            Assert.AreEqual(3, target.GetBuildingLevel(gid, pageContent));
        }

        /// <summary>
        /// A test for NewParseTroops
        ///</summary>
        [TestMethod()]
        public void NewParseTroopsTiny()
        {
            Travian target = new Travian();
            target.TD = new Data();

            int villageId = 1;
            TVillage village = new TVillage();
            target.TD.Villages[villageId] = village;

            TTroop troops = new TTroop();
            village.Troop = troops;

            target.SetGidLang(16, "Rally Point");
            target.NewParseTroops(villageId, Properties.Resources.RallyPointTiny);
            Assert.AreEqual(3, troops.Troops.Count);

            TTInfo troop = troops.Troops[0];
            Assert.AreEqual("Tiny", troop.Owner);
            Assert.AreEqual(270225, troop.OwnerVillageZ);
            Assert.AreEqual("Own troops", troop.VillageName);
            Assert.AreEqual(8, troop.Troops[0]);
            Assert.AreEqual(TTroopType.InVillage, troop.TroopType);
            Assert.AreEqual(DateTime.MinValue, troop.FinishTime);
            Assert.AreEqual(3, troop.Tribe);

            troop = troops.Troops[1];
            Assert.AreEqual("Crazy", troop.Owner);
            Assert.AreEqual(217466, troop.OwnerVillageZ);
            Assert.AreEqual("abc's troops", troop.VillageName);
            Assert.AreEqual(1, troop.Troops[3]);
            Assert.AreEqual(TTroopType.InVillage, troop.TroopType);
            Assert.AreEqual(DateTime.MinValue, troop.FinishTime);
            Assert.AreEqual(2, troop.Tribe);

            troop = troops.Troops[2];
            Assert.AreEqual("Tiny", troop.Owner);
            Assert.AreEqual(270225, troop.OwnerVillageZ);
            Assert.AreEqual("Reinforcement for lalala Village", troop.VillageName);
            Assert.AreEqual(1, troop.Troops[10]);
            Assert.AreEqual(TTroopType.Outgoing, troop.TroopType);
            Assert.IsTrue(troop.FinishTime.AddHours(-11) > DateTime.Now);
            Assert.IsTrue(troop.FinishTime.AddHours(-12) < DateTime.Now);
            Assert.AreEqual(3, troop.Tribe);
        }

        /// <summary>
        /// Another test for NewParseTroops
        ///</summary>
        [TestMethod()]
        public void NewParseTroopsCrazy()
        {
            Travian target = new Travian();
            target.TD = new Data();

            int villageId = 1;
            TVillage village = new TVillage();
            target.TD.Villages[villageId] = village;

            TTroop troops = new TTroop();
            village.Troop = troops;

            target.SetGidLang(16, "Rally Point");
            target.NewParseTroops(villageId, Properties.Resources.RallyPointCrazy);
            Assert.AreEqual(15, troops.Troops.Count);

            TTInfo troop = troops.Troops[0];
            Assert.AreEqual(2, troop.Tribe);
            Assert.AreEqual("Crazy", troop.Owner);
            Assert.AreEqual("Return from Jeffo Village", troop.VillageName);
            Assert.AreEqual(1, troop.Troops[3]);
            Assert.IsTrue(troop.FinishTime > DateTime.Now.AddMinutes(6));
            Assert.IsTrue(troop.FinishTime < DateTime.Now.AddMinutes(8));
            Assert.AreEqual(TTroopType.Incoming, troop.TroopType);

            troop = troops.Troops[1];
            Assert.AreEqual(2, troop.Tribe);
            Assert.AreEqual("Crazy", troop.Owner);
            Assert.AreEqual("Return from laraelaine40 Village", troop.VillageName);
            Assert.AreEqual(4, troop.Troops[0]);
            Assert.IsTrue(troop.FinishTime > DateTime.Now.AddMinutes(10));
            Assert.IsTrue(troop.FinishTime < DateTime.Now.AddMinutes(15));
            Assert.AreEqual(TTroopType.Incoming, troop.TroopType);

            troop = troops.Troops[7];
            Assert.AreEqual(2, troop.Tribe);
            Assert.AreEqual("Crazy", troop.Owner);
            Assert.AreEqual("Own troops", troop.VillageName);
            Assert.AreEqual(4, troop.Troops[0]);
            Assert.AreEqual(1, troop.Troops[10]);
            Assert.AreEqual(TTroopType.InVillage, troop.TroopType);

            troop = troops.Troops[8];
            Assert.AreEqual(TTroopType.Outgoing, troop.TroopType);
            Assert.AreEqual(2, troop.Tribe);
            Assert.AreEqual("Crazy", troop.Owner);
            Assert.AreEqual("Raid on hotmamapam Village", troop.VillageName);
            Assert.AreEqual(4, troop.Troops[0]);
        }
    }
}
