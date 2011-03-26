using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using libTravian;

namespace UnitTests
{
    
    
    /// <summary>
    ///This is a test class for RaidOptionTest and is intended
    ///to contain all RaidOptionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RaidOptionTest
    {
        private Travian travian;
        private MockPageQuerier pageQuerier;

        private Data travianData;
        private int villageID;
        private TVillage village;
        private TTroop troop;

        private RaidQueue target;
        private int[] troops;
        private List<TPoint> targets;

        /// <summary>
        /// Init Travian object
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            this.travianData = new Data()
            {
                Server = "none",
                Username = "none"
            };

            Dictionary<string, string> Options = new Dictionary<string, string>();
            this.travian = new Travian(travianData, Options);

            this.villageID = 1;
            this.village = new TVillage();
            this.travianData.Villages[villageID] = village;

            this.village.Name = "Home";
            this.village.isTroopInitialized = 2;
            this.village.Troop = this.troop = new TTroop();
            int[] troopsAtHome = new int[11];
            troopsAtHome[0] = 8;
            TTInfo troopAtHome = new TTInfo
            {
                Troops = troopsAtHome,
                Owner = this.village.Name,
                TroopType = TTroopType.InVillage,
                VillageName = "Own troops"
            };
            this.troop.Troops.Add(troopAtHome);

            int[] troopsOnTheRood = new int[11];
            troopsOnTheRood[0] = 5;
            TTInfo troopOnTheRoad = new TTInfo
            {
                Troops = troopsAtHome,
                TroopType = TTroopType.MyReturnWay,
                FinishTime = DateTime.Now.AddSeconds(100),
                VillageName = "Raid on abc Village"
            };
            this.troop.Troops.Add(troopOnTheRoad);

            this.troops = new int[11];
            this.troops[0] = 5;

            this.targets = new List<TPoint>();
            this.targets.Add(new TPoint(1, 2));
            this.targets.Add(new TPoint(0, 0));

            this.target = new RaidQueue()
            {
                UpCall = this.travian,
                VillageID = this.villageID,
                Troops = this.troops,
                Targets = this.targets,
                RaidType = RaidType.AttackRaid,
            };

            this.pageQuerier = new MockPageQuerier(this.villageID);
            this.travian.pageQuerier = pageQuerier;
        }

        /// <summary>
        /// A test for CountDown
        ///</summary>
        [TestMethod()]
        public void CountDownTest()
        {
            // Unknown village
            target.VillageID = 9999;
            Assert.AreEqual(86400, target.CountDown);
            Assert.IsTrue(target.MarkDeleted);

            // Village not initialized
            target.VillageID = this.villageID;
            this.village.isTroopInitialized = 1;
            Assert.AreEqual(86400, target.CountDown);

            // Troop not available
            this.village.isTroopInitialized = 2;
            this.troops[0] = 100;
            Assert.IsTrue(target.CountDown > 0);
            Assert.IsTrue(target.CountDown <= 105);

            // No troop on the road
            this.troop.Troops.RemoveAt(1);
            Assert.AreEqual(86400, target.CountDown);

            // Finally we're ready
            this.troops[0] = 5;
            Assert.AreEqual(0, target.CountDown);
        }

        /// <summary>
        ///A test for Action
        ///</summary>
        [TestMethod()]
        public void ActionTest()
        {
            this.pageQuerier.AddPage("a2b.php?z=319200", null, Properties.Resources.SendTroopsForm);

            Dictionary<string, string> requestData = new Dictionary<string, string>();
            requestData.Add("timestamp", "1259379679");
            requestData.Add("timestamp_checksum", "719106");
            requestData.Add("b", "1");
            requestData.Add("c", "4");
            requestData.Add("x", this.target.Targets[0].X.ToString());
            requestData.Add("y", this.target.Targets[0].Y.ToString());
            requestData.Add("t1", "5");
            requestData.Add("t2", "");
            requestData.Add("t3", "");
            requestData.Add("t4", "");
            requestData.Add("t5", "");
            requestData.Add("t6", "");
            requestData.Add("t7", "");
            requestData.Add("t8", "");
            requestData.Add("t9", "");
            requestData.Add("t10", "");
            requestData.Add("t11", "");
            this.pageQuerier.AddPage("a2b.php", requestData, Properties.Resources.ConfirmForm);

            Dictionary<string,string> confirmData = new Dictionary<string,string>();
            confirmData.Add("timestamp", "1259379719");
            confirmData.Add("timestamp_checksum", "057e55");
            confirmData.Add("id", "39");
            confirmData.Add("a", "9225796");
            confirmData.Add("c", "4");
            confirmData.Add("kid", "538519");
            confirmData.Add("t1", "8");
            confirmData.Add("t2", "0");
            confirmData.Add("t3", "0");
            confirmData.Add("t4", "0");
            confirmData.Add("t5", "0");
            confirmData.Add("t6", "0");
            confirmData.Add("t7", "0");
            confirmData.Add("t8", "0");
            confirmData.Add("t9", "0");
            confirmData.Add("t10", "0");
            confirmData.Add("t11", "0");
            this.pageQuerier.AddPage("a2b.php", confirmData, Properties.Resources.ConfirmResult);

            this.target.Action();

            Assert.AreEqual(1, this.target.TargetID);
            Assert.IsTrue(this.target.CountDown > 2088);
        }

        /// <summary>
        /// Calling Action() when there's no sufficient troops to lanuch the attach
        /// </summary>
        [TestMethod()]
        public void ActionWithoutTroops()
        {
            this.pageQuerier.AddPage("a2b.php?z=319200", null, Properties.Resources.SendTroopsForm);
            this.pageQuerier.AddPage("a2b.php", null, Properties.Resources.ConfirmForm);

            this.target.Troops[0] = 100;
            this.target.Action();
            Assert.AreEqual(0, this.target.TargetID);
        }

        /// <summary>
        /// Provide mock pages for matching queries
        /// </summary>
        private class MockPageQuerier : IPageQuerier
        {
            private struct MockPage
            {
                public string url;
                public Dictionary<string, string> postData;
                public string result;
            }

            private List<MockPage> mockPages = new List<MockPage>();
            private int villageId;

            public MockPageQuerier(int villageId)
            {
                this.villageId = villageId;
            }

            public void AddPage(string url, Dictionary<string, string> postData, string result)
            {
                MockPage page = new MockPage();
                page.url = url;
                page.postData = postData;
                page.result = result;
                this.mockPages.Add(page);
            }

            public string PageQuery(int villageID, string uri, Dictionary<string, string> data, bool checkLogin, bool noParser)
            {
                Assert.AreEqual(this.villageId, villageID);

                foreach (MockPage page in this.mockPages)
                {
                    if (page.url == uri)
                    {
                        bool matchesAll = true;
                        if (page.postData != null)
                        {
                            foreach (string key in page.postData.Keys)
                            {
                                if (data == null || !data.ContainsKey(key) || data[key] != page.postData[key])
                                {
                                    Console.WriteLine("Missing POST data {0} = {1}", key, page.postData[key]);
                                    matchesAll = false;
                                    break;
                                }
                            }
                        }

                        if (matchesAll)
                        {
                            return page.result;
                        }
                    }
                }

                Console.WriteLine("Failed to match URL: " + uri);
                return null;
            }
        }
    }
}
