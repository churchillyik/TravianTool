/*
 * The contents of this file are subject to the Mozilla Public License
 * Version 1.1 (the "License"); you may not use this file except in
 * compliance with the License. You may obtain a copy of the License at
 * http://www.mozilla.org/MPL/
 * 
 * Software distributed under the License is distributed on an "AS IS"
 * basis, WITHOUT WARRANTY OF ANY KIND, either express or implied. See the
 * License for the specific language governing rights and limitations
 * under the License.
 * 
 * The Initial Developer of the Original Code is [MeteorRain <msg7086@gmail.com>].
 * Copyright (C) MeteorRain 2007, 2008. All Rights Reserved.
 * Contributor(s): [MeteorRain].
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Net;
using System.Threading;
using System.Drawing;

namespace libTravian
{
    public partial class Travian : IPageQuerier
    {
        public event EventHandler<StatusChanged> StatusUpdate;

        public void CallStatusUpdate(Object sender, StatusChanged e)
        {
            StatusUpdate(sender, e);
        }

        public enum ChangedType
        {
            None,
            Villages,
            Buildings,
            Queue,
            Research,
            Stop,
            Market,
            PageCount,
            OasisFound
        }
        public class StatusChanged : EventArgs
        {
            public ChangedType ChangedData { get; set; }
            public int VillageID { get; set; }
            public int Param { get; set; }
        }

        private WebClient wc;
        public IPageQuerier pageQuerier;

        static public bool NoMB = false;
        static public double[] resrate = new double[4] { 10, 10, 9, 7 };

        public Data TD { get; set; }
        public Travian()
        {
            this.pageQuerier = this;

            //Thread.GetDomain().UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
        }

        public Travian(Data TravianData, Dictionary<string, string> Options)
        {
            TD = TravianData;
            //DB.Instance.Initialize(TravianData.Server);
            //DB.Instance.Initialize(TravianData.key);
            this.pageQuerier = this;

            int StdSpeed = 24;
            if (TD.Tribe != 0)
            {
                StdSpeed = Buildings.BaseSpeed[TD.Tribe][10];
            }
            //if(TD.Tribe == 1)
            //    StdSpeed = 16;
            //else if(TD.Tribe == 2)
            //    StdSpeed = 12;
            //else
            //    StdSpeed = 24;
            int MarketSpeedX = 1;

            TD.MarketSpeed = StdSpeed * MarketSpeedX;

            LoadOptions(Options);
            TD.Dirty = true;

            //AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            //Thread.GetDomain().UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
        }

        public void LoadOptions(Dictionary<string, string> Options)
        {
            if (Options.ContainsKey("NoMB"))
            {
                //DebugLog("Get option: NoMB", DebugLevel.I);
                NoMB = Convert.ToBoolean(Options["NoMB"]);
            }
            if (Options.ContainsKey("resrate"))
            {
                //DebugLog("Get option: ResRate", DebugLevel.I);
                var r = Options["resrate"].Split(':');
                if (r.Length == 4)
                {
                    resrate = new double[4];
                    for (int i = 0; i < 4; i++)
                        resrate[i] = Convert.ToDouble(r[i]);
                }
            }
            if (Options.ContainsKey("remotestop"))
                RemoteStopWord = Options["remotestop"];
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
        }

        static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="VillageID"></param>
        /// <param name="Q"></param>
        /// <returns>0 -> Directly buildable. positive -> pre-build gid. negative -> impossible.</returns>
        [Obsolete]
        public int testPossibleNow(int VillageID, TQueue Q)
        {
            var CV = TD.Villages[VillageID];
            if (CV.Buildings.ContainsKey(Q.Bid) && CV.Buildings[Q.Bid].Gid == Q.Gid && CV.Buildings[Q.Bid].Level != 0)
                return Buildings.CheckLevelFull(Q.Gid, CV.Buildings[Q.Bid].Level, CV.isCapital) ? -1 : 0;
            return testPossibleNewNow(TD.Tribe, TD.Villages, CV, Q.Gid, Q.Bid);
        }

        public int testPossibleNow(int VillageID, BuildingQueue Q)
        {
            var CV = TD.Villages[VillageID];
            if (CV.Buildings.ContainsKey(Q.Bid) && CV.Buildings[Q.Bid].Gid == Q.Gid && CV.Buildings[Q.Bid].Level != 0)
                return Buildings.CheckLevelFull(Q.Gid, CV.Buildings[Q.Bid].Level, CV.isCapital) ? -1 : 0;
            return testPossibleNewNow(TD.Tribe, TD.Villages, CV, Q.Gid, Q.Bid);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Tribe"></param>
        /// <param name="Villages"></param>
        /// <param name="CV"></param>
        /// <param name="Gid"></param>
        /// <param name="Bid"></param>
        /// <returns>0:directly, -1:impossible, >0:pre-upgrade gid</returns>
        static public int testPossibleNewNow(int Tribe, Dictionary<int, TVillage> Villages, TVillage CV, int Gid, int Bid)
        {
            List<int> CapitalNo = new List<int> { 27, 29, 30 };
            List<int> NotCapitalNo = new List<int> { 34, 35 };
            List<int> Repeatable = new List<int> { 10, 11, 23, 36, 38, 39 };
            //TQueue Q = CV.Queue[QueueID];
            // Extend

            if (Gid == 36 && Tribe != 3)
                return -1;
            if (Gid == 35 && Tribe != 2)
                return -1;
            if (Gid == 41 && Tribe != 1)
                return -1;

            if (Gid < 5)
                return 0;

            // Below are building new one
            if (CV.isCapital && CapitalNo.Contains(Gid))
                return -1;
            if (!CV.isCapital && NotCapitalNo.Contains(Gid))
                return -1;
            // Residence/Palace problem
            if (Gid == 26)
            {
                int PCount = 0;
                foreach (var x in Villages)
                    if (x.Value.isBuildingInitialized == 2)
                        foreach (var y in x.Value.Buildings)
                            if (y.Value.Gid == 26 && y.Value.Level > 0)
                            {
                                PCount++;
                                break;
                            }
                            else if (y.Value.Gid == 25)
                                break;
                if (PCount == 0)
                {
                    bool NotFound = true;
                    foreach (var x in Buildings.Depends[Gid])
                    {
                        int canUp = 0;
                        foreach (var y in CV.Buildings)
                            if (x.Gid == y.Value.Gid)
                                if (x.Level > y.Value.Level)
                                    canUp = y.Key;
                                else
                                {
                                    NotFound = false;
                                    break;
                                }
                        if (NotFound && canUp != 0)
                            return canUp;
                    }
                    if (NotFound)
                        return -1;
                    else
                        return 0;
                }
                else
                    return -1;
            }

            // Check duplicate
            int toBuild = 0;
            if (Repeatable.Contains(Gid))
            {
                foreach (var x in CV.Buildings)
                {
                    if (x.Key == Bid)
                        continue;
                    if (x.Value.Gid == Gid)
                        if (Buildings.CheckLevelFull(x.Value.Gid, x.Value.Level, CV.isCapital))
                        {
                            toBuild = 0;
                            break;
                        }
                        else
                            toBuild = x.Key;
                }
                if (toBuild != 0)
                    return toBuild;
                else
                    return 0;
            }
            // Check duplicate for non-repeatable
            foreach (var x in CV.Buildings)
                if (x.Value.Gid == Gid && x.Key != Bid)
                    return -1;

            // Check depend
            if (!Buildings.Depends.ContainsKey(Gid))
                return 0;
            bool gNotFound = false;
            foreach (var x in Buildings.Depends[Gid])
            {
                bool NotFound = true;
                int canUp = 0;
                foreach (var y in CV.Buildings)
                    if (x.Gid == y.Value.Gid)
                        if (x.Level > y.Value.Level)
                            canUp = y.Key;
                        else
                        {
                            NotFound = false;
                            break;
                        }
                if (NotFound && canUp != 0)
                    return canUp;
                gNotFound = gNotFound || NotFound;
                if (gNotFound)
                    break;
            }
            return gNotFound ? -1 : 0;
        }
    }
}
