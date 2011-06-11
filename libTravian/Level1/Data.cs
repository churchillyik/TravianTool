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
using System.Collections;

namespace libTravian
{
    public class rinfo_array
    {
        TResAmount[] _data;
        double ratio;
        public int length { get; private set; }
        TResAmount resbase;
        private int mytrunc(double x)
        {
            int t1 = (int)(x / 10) * 10;
            int t2 = (int)x - t1;
            if (t2 >= 7.5)
                t1 += 10;
            else if (t2 >= 2.5)
                t1 += 5;
            return t1;
        }
        public rinfo_array(int Length, double Ratio, TResAmount Resbase)
        {
            resbase = Resbase;
            length = Length;
            ratio = Ratio;
        }
        public TResAmount[] data
        {
            get
            {
                if (_data == null)
                {
                    double[] r = new double[4];
                    int i, j;
                    _data = new TResAmount[length + 1];
                    _data[1] = resbase;
                    for (i = 0; i < 4; i++)
                        r[i] = resbase.Resources[i];
                    for (i = 2; i <= length; i++)
                    {
                        _data[i] = new TResAmount(0, 0, 0, 0);
                        for (j = 0; j < 4; j++)
                        {
                            r[j] *= ratio;
                            _data[i].Resources[j] = Math.Min(mytrunc(r[j]), 1000000);
                        }
                    }
                }
                return _data;
            }
        }
    }

    public class Buildings
    {
        static public rinfo_array[] BuildingCost;
        static public Dictionary<int, Dictionary<int, TResAmount>> UpCost;
        static public Dictionary<int, TResAmount> ResearchCost;
        static public Dictionary<int, TResAmount> TroopCost;
        static public Dictionary<int, TBL[]> Depends;
        static public Dictionary<int, int[]> PreferPos;
        static public  Dictionary<int, int[]> BaseSpeed;
        static public List<TResAmount> PartyCos;
        static public void Init()
        {
            if (BuildingCost == null)
                InitBuildingCost();
            if (Depends == null)
                InitDepend();
            if (PreferPos == null)
                InitPrefer();
            if (UpCost == null)
                InitUpCost();
            if (ResearchCost == null)
                InitResearchCost();
            if (TroopCost == null)
                InitTroopCost();
            if (BaseSpeed == null)
                InitBaseSpeed();
            if (PartyCos == null)
                PartyCos = new List<TResAmount>()
				{
					new TResAmount(6400, 6650, 5940, 1340),
					new TResAmount(29700, 33250, 32000, 6700)
				};
        }
        static void InitBaseSpeed()
        {
            //base speed.key=tribe value=speeds:troops,merchants
            BaseSpeed = new Dictionary<int, int[]>(3);
            BaseSpeed[1] = new int[] { 6, 5, 7, 16, 14, 10, 4, 3, 4, 5, 16 };
            BaseSpeed[2] = new int[] { 7, 7, 6, 9, 10, 9, 4, 3, 4, 5, 12 };
            BaseSpeed[3] = new int[] { 7, 6, 17, 19, 16, 13, 4, 3, 5, 5, 24 };
        }
        static private void InitBuildingCost()
        {
            BuildingCost = new rinfo_array[42];
            BuildingCost[1] = new rinfo_array(21, 1.67, new TResAmount(40, 100, 50, 60));					//	木
            BuildingCost[2] = new rinfo_array(21, 1.67, new TResAmount(80, 40, 80, 50));					//	泥
            BuildingCost[3] = new rinfo_array(21, 1.67, new TResAmount(100, 80, 30, 60));					//	铁
            BuildingCost[4] = new rinfo_array(21, 1.67, new TResAmount(70, 90, 70, 20));					//	粮
            BuildingCost[5] = new rinfo_array(5, 1.8, new TResAmount(520, 380, 290, 90));					//	木材厂
            BuildingCost[6] = new rinfo_array(5, 1.8, new TResAmount(440, 480, 320, 50));					//	砖块厂
            BuildingCost[7] = new rinfo_array(5, 1.8, new TResAmount(200, 450, 510, 120));					//	铸造厂
            BuildingCost[8] = new rinfo_array(5, 1.8, new TResAmount(500, 440, 380, 1240));					//	磨坊
            BuildingCost[9] = new rinfo_array(5, 1.8, new TResAmount(1200, 1480, 870, 1600));				//	面包房
            BuildingCost[10] = new rinfo_array(20, 1.28, new TResAmount(130, 160, 90, 40));					//	仓库
            BuildingCost[11] = new rinfo_array(20, 1.28, new TResAmount(80, 100, 70, 20));					//	粮仓
            BuildingCost[12] = new rinfo_array(20, 1.28, new TResAmount(180, 250, 500, 160));				//	防具铺（已废）
            BuildingCost[13] = new rinfo_array(20, 1.28, new TResAmount(180, 250, 500, 160));				//	铁匠铺，原为(170, 200, 380, 130)
            BuildingCost[14] = new rinfo_array(20, 1.28, new TResAmount(1750, 2250, 1530, 240));			//	竞技场
            BuildingCost[15] = new rinfo_array(20, 1.28, new TResAmount(70, 40, 60, 20));					//	中心大楼
            BuildingCost[16] = new rinfo_array(20, 1.28, new TResAmount(110, 160, 90, 70));					//	集结点
            BuildingCost[17] = new rinfo_array(20, 1.28, new TResAmount(80, 70, 120, 70));					//	市场
            BuildingCost[18] = new rinfo_array(20, 1.28, new TResAmount(180, 130, 150, 80));				//	外交所
            BuildingCost[19] = new rinfo_array(20, 1.28, new TResAmount(210, 140, 260, 120));				//	兵营
            BuildingCost[20] = new rinfo_array(20, 1.28, new TResAmount(260, 140, 220, 100));				//	马厩
            BuildingCost[21] = new rinfo_array(20, 1.28, new TResAmount(460, 510, 600, 320));				//	工场
            BuildingCost[22] = new rinfo_array(20, 1.28, new TResAmount(220, 160, 90, 40));					//	研究院
            BuildingCost[23] = new rinfo_array(10, 1.28, new TResAmount(40, 50, 30, 10));					//	地洞
            BuildingCost[24] = new rinfo_array(20, 1.28, new TResAmount(1250, 1110, 1260, 600));			//	市政厅
            BuildingCost[25] = new rinfo_array(20, 1.28, new TResAmount(580, 460, 350, 180));				//	行宫
            BuildingCost[26] = new rinfo_array(20, 1.28, new TResAmount(550, 800, 750, 250));				//	皇宫
            BuildingCost[27] = new rinfo_array(20, 1.26, new TResAmount(2880, 2740, 2580, 990));			//	宝库
            BuildingCost[28] = new rinfo_array(20, 1.28, new TResAmount(1400, 1330, 1200, 400));			//	交易所
            BuildingCost[29] = new rinfo_array(20, 1.28, new TResAmount(630, 420, 780, 360));				//	大兵营
            BuildingCost[30] = new rinfo_array(20, 1.28, new TResAmount(780, 420, 660, 300));				//	大马厩
            BuildingCost[31] = new rinfo_array(20, 1.28, new TResAmount(70, 90, 170, 70));					//	罗马城墙
            BuildingCost[32] = new rinfo_array(20, 1.28, new TResAmount(120, 200, 0, 80));					//	日耳曼城墙
            BuildingCost[33] = new rinfo_array(20, 1.28, new TResAmount(160, 100, 80, 60));					//	高卢城墙
            BuildingCost[34] = new rinfo_array(20, 1.28, new TResAmount(155, 130, 125, 70));				//	石匠铺
            BuildingCost[35] = new rinfo_array(10, 1.40, new TResAmount(1460, 930, 1250, 1740));			//	酿酒坊
            BuildingCost[36] = new rinfo_array(20, 1.28, new TResAmount(80, 120, 70, 90));					//	陷阱，原为(100, 100, 100, 100)
            BuildingCost[37] = new rinfo_array(20, 1.33, new TResAmount(700, 670, 700, 240));				//	英雄园
            BuildingCost[38] = new rinfo_array(20, 1.28, new TResAmount(650, 800, 450, 200));				//	大仓库
            BuildingCost[39] = new rinfo_array(20, 1.28, new TResAmount(400, 500, 350, 100));				//	大粮仓
            BuildingCost[40] = new rinfo_array(100, 1.0275, new TResAmount(66700, 69050, 72200, 13200));	//	世界奇观
            BuildingCost[41] = new rinfo_array(20, 1.28, new TResAmount(780, 420, 660, 540));				//	饮马槽
        }
        static private void InitDepend()
        {
            Depends = new Dictionary<int, TBL[]>();
            Depends.Add(5, new TBL[] { new TBL(1, 10), new TBL(15, 5) });
            Depends.Add(6, new TBL[] { new TBL(2, 10), new TBL(15, 5) });
            Depends.Add(7, new TBL[] { new TBL(3, 10), new TBL(15, 5) });
            Depends.Add(8, new TBL[] { new TBL(4, 5) });
            Depends.Add(9, new TBL[] { new TBL(4, 10), new TBL(15, 5), new TBL(8, 5) });
            Depends.Add(10, new TBL[] { new TBL(15, 1) });
            Depends.Add(11, new TBL[] { new TBL(15, 1) });
            Depends.Add(12, new TBL[] { new TBL(22, 3) });
            Depends.Add(13, new TBL[] { new TBL(22, 1) });
            Depends.Add(14, new TBL[] { new TBL(16, 15) });
            Depends.Add(17, new TBL[] { new TBL(10, 1), new TBL(11, 1), new TBL(15, 3) });
            Depends.Add(18, new TBL[] { new TBL(15, 1) });
            Depends.Add(19, new TBL[] { new TBL(16, 1) });
            Depends.Add(20, new TBL[] { new TBL(13, 3), new TBL(22, 5) });
            Depends.Add(21, new TBL[] { new TBL(22, 10), new TBL(15, 5) });
            Depends.Add(22, new TBL[] { new TBL(19, 3), new TBL(16, 1) });
            Depends.Add(24, new TBL[] { new TBL(22, 10), new TBL(15, 10) });
            Depends.Add(25, new TBL[] { new TBL(15, 5) });
            Depends.Add(26, new TBL[] { new TBL(18, 1) });
            Depends.Add(27, new TBL[] { new TBL(15, 10) });
            Depends.Add(28, new TBL[] { new TBL(17, 20), new TBL(20, 10) });
            Depends.Add(29, new TBL[] { new TBL(19, 20) });
            Depends.Add(30, new TBL[] { new TBL(20, 20) });
            Depends.Add(34, new TBL[] { new TBL(15, 5) });
            Depends.Add(35, new TBL[] { new TBL(11, 20), new TBL(16, 10) });
            Depends.Add(36, new TBL[] { new TBL(16, 1) });
            Depends.Add(37, new TBL[] { new TBL(15, 3), new TBL(16, 1) });
            Depends.Add(38, new TBL[] { new TBL(15, 10), new TBL(40, 0) });
            Depends.Add(39, new TBL[] { new TBL(15, 10), new TBL(40, 0) });
            Depends.Add(41, new TBL[] { new TBL(20, 20), new TBL(16, 10) });
        }
        static private void InitPrefer()
        {
            PreferPos = new Dictionary<int, int[]>(32);
            PreferPos[5] = new int[] { 28 };								//	木材厂
            PreferPos[6] = new int[] { 24 };								//	砖块厂
            PreferPos[7] = new int[] { 19 };								//	铸造厂
            PreferPos[8] = new int[] { 32 };								//	磨坊
            PreferPos[9] = new int[] { 20 };								//	面包房
            PreferPos[10] = new int[] { 35, 37, 32, 33, 28, 24, 19 };		//	仓库
            PreferPos[11] = new int[] { 36, 38, 20, 29 };					//	粮仓
            PreferPos[13] = new int[] { 25 };								//	铁匠铺
            PreferPos[14] = new int[] { 22 };								//	竞技场
            PreferPos[15] = new int[] { 26 };								//	中心大楼
            PreferPos[16] = new int[] { 39 };								//	集结点
            PreferPos[17] = new int[] { 27 };								//	市场
            PreferPos[18] = new int[] { 33 };								//	外交所
            PreferPos[19] = new int[] { 34 };								//	兵营
            PreferPos[20] = new int[] { 31 };								//	马厩
            PreferPos[21] = new int[] { 31 };								//	工场
            PreferPos[22] = new int[] { 23 };								//	研究院
            PreferPos[24] = new int[] { 33 };								//	市政厅
            PreferPos[25] = new int[] { 21 };								//	行宫
            PreferPos[26] = new int[] { 21 };								//	皇宫
            PreferPos[28] = new int[] { 22 };								//	交易所
        }
        static private void InitUpCost()
        {
            UpCost = new Dictionary<int, Dictionary<int, TResAmount>>(30);
            UpCost[1] = new Dictionary<int, TResAmount>(21);
            UpCost[1][0] = new TResAmount(940, 800, 1250, 370);
            UpCost[1][11] = new TResAmount(6860, 5840, 9125, 2700);
            UpCost[1][1] = new TResAmount(1635, 1395, 2175, 645);
            UpCost[1][12] = new TResAmount(7315, 6225, 9730, 2880);
            UpCost[1][2] = new TResAmount(2265, 1925, 3010, 890);
            UpCost[1][13] = new TResAmount(7765, 6605, 10325, 3055);
            UpCost[1][3] = new TResAmount(2850, 2425, 3790, 1120);
            UpCost[1][14] = new TResAmount(8205, 6980, 10910, 3230);
            UpCost[1][4] = new TResAmount(3405, 2900, 4530, 1340);
            UpCost[1][15] = new TResAmount(8640, 7350, 11485, 3400);
            UpCost[1][5] = new TResAmount(3940, 3355, 5240, 1550);
            UpCost[1][16] = new TResAmount(9065, 7715, 12060, 3570);
            UpCost[1][6] = new TResAmount(4460, 3795, 5930, 1755);
            UpCost[1][17] = new TResAmount(9490, 8080, 12620, 3735);
            UpCost[1][7] = new TResAmount(4960, 4220, 6600, 1955);
            UpCost[1][18] = new TResAmount(9910, 8435, 13180, 3900);
            UpCost[1][8] = new TResAmount(5450, 4640, 7250, 2145);
            UpCost[1][19] = new TResAmount(10325, 8790, 13730, 4065);
            UpCost[1][9] = new TResAmount(5930, 5050, 7885, 2335);
            UpCost[1][20] = new TResAmount(10740, 9140, 14280, 4225);
            UpCost[1][10] = new TResAmount(6400, 5450, 8510, 2520);
            UpCost[2] = new Dictionary<int, TResAmount>(21);
            UpCost[2][0] = new TResAmount(800, 1010, 1320, 650);
            UpCost[2][11] = new TResAmount(5840, 7375, 9635, 4745);
            UpCost[2][1] = new TResAmount(1395, 1760, 2300, 1130);
            UpCost[2][12] = new TResAmount(6225, 7860, 10275, 5060);
            UpCost[2][2] = new TResAmount(1925, 2430, 3180, 1565);
            UpCost[2][13] = new TResAmount(6605, 8340, 10900, 5370);
            UpCost[2][3] = new TResAmount(2425, 3060, 4000, 1970);
            UpCost[2][14] = new TResAmount(6980, 8815, 11520, 5675);
            UpCost[2][4] = new TResAmount(2900, 3660, 4785, 2355);
            UpCost[2][15] = new TResAmount(7350, 9280, 12130, 5975);
            UpCost[2][5] = new TResAmount(3355, 4235, 5535, 2725);
            UpCost[2][16] = new TResAmount(7715, 9745, 12735, 6270);
            UpCost[2][6] = new TResAmount(3795, 4790, 6260, 3085);
            UpCost[2][17] = new TResAmount(8080, 10200, 13330, 6565);
            UpCost[2][7] = new TResAmount(4220, 5330, 6965, 3430);
            UpCost[2][18] = new TResAmount(8435, 10650, 13920, 6855);
            UpCost[2][8] = new TResAmount(4640, 5860, 7655, 3770);
            UpCost[2][19] = new TResAmount(8790, 11095, 14500, 7140);
            UpCost[2][9] = new TResAmount(5050, 6375, 8330, 4100);
            UpCost[2][20] = new TResAmount(9140, 11535, 15080, 7425);
            UpCost[2][10] = new TResAmount(5450, 6880, 8990, 4425);
            UpCost[3] = new Dictionary<int, TResAmount>(21);
            UpCost[3][0] = new TResAmount(1150, 1220, 1670, 720);
            UpCost[3][11] = new TResAmount(8395, 8905, 12190, 5255);
            UpCost[3][1] = new TResAmount(2000, 2125, 2910, 1255);
            UpCost[3][12] = new TResAmount(8950, 9495, 13000, 5605);
            UpCost[3][2] = new TResAmount(2770, 2940, 4020, 1735);
            UpCost[3][13] = new TResAmount(9495, 10075, 13790, 5945);
            UpCost[3][3] = new TResAmount(3485, 3700, 5060, 2185);
            UpCost[3][14] = new TResAmount(10035, 10645, 14575, 6285);
            UpCost[3][4] = new TResAmount(4165, 4420, 6050, 2610);
            UpCost[3][15] = new TResAmount(10570, 11210, 15345, 6615);
            UpCost[3][5] = new TResAmount(4820, 5115, 7000, 3020);
            UpCost[3][16] = new TResAmount(11095, 11770, 16110, 6945);
            UpCost[3][6] = new TResAmount(5455, 5785, 7920, 3415);
            UpCost[3][17] = new TResAmount(11610, 12320, 16865, 7270);
            UpCost[3][7] = new TResAmount(6070, 6440, 8815, 3800);
            UpCost[3][18] = new TResAmount(12125, 12865, 17610, 7590);
            UpCost[3][8] = new TResAmount(6670, 7075, 9685, 4175);
            UpCost[3][19] = new TResAmount(12635, 13400, 18345, 7910);
            UpCost[3][9] = new TResAmount(7255, 7700, 10535, 4545);
            UpCost[3][20] = new TResAmount(13135, 13935, 19075, 8225);
            UpCost[3][10] = new TResAmount(7830, 8310, 11370, 4905);
            UpCost[4] = new Dictionary<int, TResAmount>(21);
            UpCost[4][0] = new TResAmount(540, 610, 170, 220);
            UpCost[4][11] = new TResAmount(3940, 4455, 1240, 1605);
            UpCost[4][1] = new TResAmount(940, 1060, 295, 385);
            UpCost[4][12] = new TResAmount(4205, 4750, 1325, 1710);
            UpCost[4][2] = new TResAmount(1300, 1470, 410, 530);
            UpCost[4][13] = new TResAmount(4460, 5040, 1405, 1815);
            UpCost[4][3] = new TResAmount(1635, 1850, 515, 665);
            UpCost[4][14] = new TResAmount(4715, 5325, 1485, 1920);
            UpCost[4][4] = new TResAmount(1955, 2210, 615, 795);
            UpCost[4][15] = new TResAmount(4960, 5605, 1560, 2020);
            UpCost[4][5] = new TResAmount(2265, 2560, 715, 920);
            UpCost[4][16] = new TResAmount(5210, 5885, 1640, 2120);
            UpCost[4][6] = new TResAmount(2560, 2895, 805, 1045);
            UpCost[4][17] = new TResAmount(5455, 6160, 1715, 2220);
            UpCost[4][7] = new TResAmount(2850, 3220, 895, 1160);
            UpCost[4][18] = new TResAmount(5695, 6430, 1790, 2320);
            UpCost[4][8] = new TResAmount(3130, 3540, 985, 1275);
            UpCost[4][19] = new TResAmount(5930, 6700, 1870, 2415);
            UpCost[4][9] = new TResAmount(3405, 3850, 1075, 1390);
            UpCost[4][20] = new TResAmount(6170, 6970, 1940, 2515);
            UpCost[4][10] = new TResAmount(3675, 4155, 1160, 1500);
            UpCost[5] = new Dictionary<int, TResAmount>(21);
            UpCost[5][0] = new TResAmount(1315, 1060, 815, 285);
            UpCost[5][11] = new TResAmount(9610, 7740, 5940, 2095);
            UpCost[5][1] = new TResAmount(2290, 1845, 1415, 500);
            UpCost[5][12] = new TResAmount(10250, 8250, 6330, 2230);
            UpCost[5][2] = new TResAmount(3170, 2555, 1960, 690);
            UpCost[5][13] = new TResAmount(10875, 8755, 6715, 2365);
            UpCost[5][3] = new TResAmount(3990, 3215, 2465, 870);
            UpCost[5][14] = new TResAmount(11490, 9250, 7100, 2500);
            UpCost[5][4] = new TResAmount(4770, 3840, 2945, 1040);
            UpCost[5][15] = new TResAmount(12100, 9740, 7475, 2635);
            UpCost[5][5] = new TResAmount(5520, 4445, 3410, 1200);
            UpCost[5][16] = new TResAmount(12700, 10225, 7845, 2765);
            UpCost[5][6] = new TResAmount(6245, 5030, 3860, 1360);
            UpCost[5][17] = new TResAmount(13295, 10705, 8215, 2895);
            UpCost[5][7] = new TResAmount(6950, 5595, 4295, 1515);
            UpCost[5][18] = new TResAmount(13885, 11175, 8575, 3025);
            UpCost[5][8] = new TResAmount(7635, 6150, 4715, 1665);
            UpCost[5][19] = new TResAmount(14465, 11645, 8935, 3150);
            UpCost[5][9] = new TResAmount(8310, 6690, 5130, 1810);
            UpCost[5][20] = new TResAmount(15040, 12110, 9290, 3275);
            UpCost[5][10] = new TResAmount(8965, 7220, 5540, 1950);
            UpCost[6] = new Dictionary<int, TResAmount>(21);
            UpCost[6][0] = new TResAmount(990, 1145, 1450, 355);
            UpCost[6][11] = new TResAmount(7210, 8360, 10585, 2590);
            UpCost[6][1] = new TResAmount(1720, 1995, 2525, 620);
            UpCost[6][12] = new TResAmount(7685, 8910, 11285, 2765);
            UpCost[6][2] = new TResAmount(2380, 2755, 3490, 855);
            UpCost[6][13] = new TResAmount(8155, 9455, 11975, 2930);
            UpCost[6][3] = new TResAmount(2995, 3470, 4395, 1075);
            UpCost[6][14] = new TResAmount(8620, 9995, 12655, 3100);
            UpCost[6][4] = new TResAmount(3580, 4150, 5255, 1285);
            UpCost[6][15] = new TResAmount(9075, 10520, 13325, 3260);
            UpCost[6][5] = new TResAmount(4140, 4800, 6080, 1490);
            UpCost[6][16] = new TResAmount(9525, 11045, 13985, 3425);
            UpCost[6][6] = new TResAmount(4685, 5430, 6880, 1685);
            UpCost[6][17] = new TResAmount(9970, 11560, 14640, 3585);
            UpCost[6][7] = new TResAmount(5210, 6045, 7655, 1875);
            UpCost[6][18] = new TResAmount(10410, 12075, 15290, 3745);
            UpCost[6][8] = new TResAmount(5725, 6640, 8410, 2060);
            UpCost[6][19] = new TResAmount(10850, 12580, 15930, 3900);
            UpCost[6][9] = new TResAmount(6230, 7225, 9150, 2240);
            UpCost[6][20] = new TResAmount(11280, 13080, 16565, 4055);
            UpCost[6][10] = new TResAmount(6725, 7795, 9875, 2415);
            UpCost[7] = new Dictionary<int, TResAmount>(21);
            UpCost[7][0] = new TResAmount(2135, 875, 1235, 215);
            UpCost[7][11] = new TResAmount(15575, 6375, 9005, 1580);
            UpCost[7][1] = new TResAmount(3715, 1520, 2145, 375);
            UpCost[7][12] = new TResAmount(16605, 6795, 9600, 1685);
            UpCost[7][2] = new TResAmount(5140, 2105, 2970, 520);
            UpCost[7][13] = new TResAmount(17620, 7210, 10185, 1790);
            UpCost[7][3] = new TResAmount(6465, 2645, 3740, 655);
            UpCost[7][14] = new TResAmount(18620, 7620, 10765, 1890);
            UpCost[7][4] = new TResAmount(7730, 3165, 4470, 785);
            UpCost[7][15] = new TResAmount(19605, 8025, 11335, 1990);
            UpCost[7][5] = new TResAmount(8945, 3660, 5170, 910);
            UpCost[7][16] = new TResAmount(20580, 8425, 11895, 2090);
            UpCost[7][6] = new TResAmount(10120, 4140, 5850, 1030);
            UpCost[7][17] = new TResAmount(21540, 8820, 12455, 2190);
            UpCost[7][7] = new TResAmount(11260, 4610, 6510, 1145);
            UpCost[7][18] = new TResAmount(22495, 9210, 13005, 2285);
            UpCost[7][8] = new TResAmount(12370, 5065, 7155, 1255);
            UpCost[7][19] = new TResAmount(23435, 9595, 13550, 2380);
            UpCost[7][9] = new TResAmount(13460, 5510, 7780, 1365);
            UpCost[7][20] = new TResAmount(24370, 9975, 14090, 2475);
            UpCost[7][10] = new TResAmount(14525, 5945, 8400, 1475);
            UpCost[8] = new Dictionary<int, TResAmount>(21);
            UpCost[8][0] = new TResAmount(1125, 1590, 735, 130);
            UpCost[8][11] = new TResAmount(8215, 11620, 5355, 960);
            UpCost[8][1] = new TResAmount(1960, 2770, 1275, 230);
            UpCost[8][12] = new TResAmount(8755, 12390, 5710, 1025);
            UpCost[8][2] = new TResAmount(2710, 3835, 1765, 315);
            UpCost[8][13] = new TResAmount(9290, 13145, 6055, 1085);
            UpCost[8][3] = new TResAmount(3410, 4825, 2225, 400);
            UpCost[8][14] = new TResAmount(9820, 13890, 6400, 1150);
            UpCost[8][4] = new TResAmount(4075, 5770, 2660, 475);
            UpCost[8][15] = new TResAmount(10340, 14625, 6740, 1210);
            UpCost[8][5] = new TResAmount(4715, 6675, 3075, 550);
            UpCost[8][16] = new TResAmount(10850, 15355, 7075, 1270);
            UpCost[8][6] = new TResAmount(5335, 7550, 3480, 625);
            UpCost[8][17] = new TResAmount(11360, 16070, 7405, 1330);
            UpCost[8][7] = new TResAmount(5940, 8400, 3870, 695);
            UpCost[8][18] = new TResAmount(11860, 16780, 7730, 1390);
            UpCost[8][8] = new TResAmount(6525, 9230, 4255, 765);
            UpCost[8][19] = new TResAmount(12360, 17485, 8055, 1445);
            UpCost[8][9] = new TResAmount(7100, 10045, 4625, 830);
            UpCost[8][20] = new TResAmount(12850, 18180, 8375, 1505);
            UpCost[8][10] = new TResAmount(7660, 10840, 4995, 895);
            UpCost[9] = new Dictionary<int, TResAmount>(21);
            UpCost[9][0] = new TResAmount(43070, 38100, 63040, 52530);
            UpCost[9][11] = new TResAmount(314425, 278145, 460215, 383505);
            UpCost[9][1] = new TResAmount(74990, 66335, 109760, 91465);
            UpCost[9][12] = new TResAmount(335220, 296540, 490650, 408865);
            UpCost[9][2] = new TResAmount(103720, 91755, 151815, 126510);
            UpCost[9][13] = new TResAmount(355695, 314650, 520615, 433835);
            UpCost[9][3] = new TResAmount(130565, 115500, 191100, 159245);
            UpCost[9][14] = new TResAmount(375880, 332505, 550160, 458455);
            UpCost[9][4] = new TResAmount(156080, 138070, 228450, 190370);
            UpCost[9][15] = new TResAmount(395795, 350125, 579310, 482745);
            UpCost[9][5] = new TResAmount(180590, 159750, 264325, 220265);
            UpCost[9][16] = new TResAmount(415465, 367525, 608100, 506740);
            UpCost[9][6] = new TResAmount(204295, 180720, 299015, 249175);
            UpCost[9][17] = new TResAmount(434905, 384720, 636550, 530445);
            UpCost[9][7] = new TResAmount(227325, 201095, 332725, 277265);
            UpCost[9][18] = new TResAmount(454125, 401725, 664690, 553895);
            UpCost[9][8] = new TResAmount(249785, 220965, 365605, 304660);
            UpCost[9][19] = new TResAmount(473150, 418550, 692535, 577095);
            UpCost[9][9] = new TResAmount(271755, 240395, 397755, 331455);
            UpCost[9][20] = new TResAmount(491985, 435210, 720100, 600065);
            UpCost[9][10] = new TResAmount(293285, 259440, 429270, 357715);
            UpCost[10] = new Dictionary<int, TResAmount>(21);
            UpCost[10][0] = new TResAmount(40700, 37200, 50600, 38660);
            UpCost[10][11] = new TResAmount(297125, 271575, 369400, 282230);
            UpCost[10][1] = new TResAmount(70865, 64770, 88100, 67310);
            UpCost[10][12] = new TResAmount(316775, 289535, 393825, 300895);
            UpCost[10][2] = new TResAmount(98015, 89585, 121855, 93100);
            UpCost[10][13] = new TResAmount(336120, 307215, 417880, 319275);
            UpCost[10][3] = new TResAmount(123380, 112770, 153390, 117195);
            UpCost[10][14] = new TResAmount(355195, 324650, 441595, 337390);
            UpCost[10][4] = new TResAmount(147495, 134810, 183370, 140100);
            UpCost[10][15] = new TResAmount(374015, 341855, 464995, 355270);
            UpCost[10][5] = new TResAmount(170655, 155980, 212165, 162100);
            UpCost[10][16] = new TResAmount(392605, 358840, 488100, 372925);
            UpCost[10][6] = new TResAmount(193050, 176450, 240010, 183375);
            UpCost[10][17] = new TResAmount(410970, 375630, 510940, 390375);
            UpCost[10][7] = new TResAmount(214815, 196345, 267070, 204050);
            UpCost[10][18] = new TResAmount(429140, 392235, 533525, 407630);
            UpCost[10][8] = new TResAmount(236040, 215745, 293455, 224210);
            UpCost[10][19] = new TResAmount(447115, 408665, 555870, 424705);
            UpCost[10][9] = new TResAmount(256800, 234715, 319265, 243930);
            UpCost[10][20] = new TResAmount(464910, 424930, 577995, 441610);
            UpCost[10][10] = new TResAmount(277145, 253315, 344560, 263255);
            UpCost[11] = new Dictionary<int, TResAmount>(21);
            UpCost[11][0] = new TResAmount(765, 625, 480, 440);
            UpCost[11][11] = new TResAmount(5585, 4565, 3505, 3210);
            UpCost[11][1] = new TResAmount(1330, 1090, 835, 765);
            UpCost[11][12] = new TResAmount(5955, 4865, 3735, 3425);
            UpCost[11][2] = new TResAmount(1840, 1505, 1155, 1060);
            UpCost[11][13] = new TResAmount(6320, 5160, 3965, 3635);
            UpCost[11][3] = new TResAmount(2320, 1895, 1455, 1335);
            UpCost[11][14] = new TResAmount(6675, 5455, 4190, 3840);
            UpCost[11][4] = new TResAmount(2770, 2265, 1740, 1595);
            UpCost[11][15] = new TResAmount(7030, 5745, 4410, 4045);
            UpCost[11][5] = new TResAmount(3210, 2620, 2015, 1845);
            UpCost[11][16] = new TResAmount(7380, 6030, 4630, 4245);
            UpCost[11][6] = new TResAmount(3630, 2965, 2275, 2085);
            UpCost[11][17] = new TResAmount(7725, 6310, 4845, 4445);
            UpCost[11][7] = new TResAmount(4040, 3300, 2535, 2320);
            UpCost[11][18] = new TResAmount(8065, 6590, 5060, 4640);
            UpCost[11][8] = new TResAmount(4435, 3625, 2785, 2550);
            UpCost[11][19] = new TResAmount(8405, 6865, 5275, 4835);
            UpCost[11][9] = new TResAmount(4825, 3945, 3030, 2775);
            UpCost[11][20] = new TResAmount(8740, 7140, 5485, 5025);
            UpCost[11][10] = new TResAmount(5210, 4255, 3270, 2995);
            UpCost[12] = new Dictionary<int, TResAmount>(21);
            UpCost[12][0] = new TResAmount(1115, 590, 795, 440);
            UpCost[12][11] = new TResAmount(8140, 4305, 5805, 3210);
            UpCost[12][1] = new TResAmount(1940, 1025, 1385, 765);
            UpCost[12][12] = new TResAmount(8680, 4590, 6190, 3425);
            UpCost[12][2] = new TResAmount(2685, 1420, 1915, 1060);
            UpCost[12][13] = new TResAmount(9210, 4875, 6565, 3635);
            UpCost[12][3] = new TResAmount(3380, 1790, 2410, 1335);
            UpCost[12][14] = new TResAmount(9730, 5150, 6940, 3840);
            UpCost[12][4] = new TResAmount(4040, 2140, 2880, 1595);
            UpCost[12][15] = new TResAmount(10245, 5420, 7305, 4045);
            UpCost[12][5] = new TResAmount(4675, 2475, 3335, 1845);
            UpCost[12][16] = new TResAmount(10755, 5690, 7670, 4245);
            UpCost[12][6] = new TResAmount(5290, 2800, 3770, 2085);
            UpCost[12][17] = new TResAmount(11260, 5960, 8030, 4445);
            UpCost[12][7] = new TResAmount(5885, 3115, 4195, 2320);
            UpCost[12][18] = new TResAmount(11755, 6220, 8380, 4640);
            UpCost[12][8] = new TResAmount(6465, 3420, 4610, 2550);
            UpCost[12][19] = new TResAmount(12250, 6480, 8735, 4835);
            UpCost[12][9] = new TResAmount(7035, 3725, 5015, 2775);
            UpCost[12][20] = new TResAmount(12735, 6740, 9080, 5025);
            UpCost[12][10] = new TResAmount(7595, 4020, 5415, 2995);
            UpCost[13] = new Dictionary<int, TResAmount>(21);
            UpCost[13][0] = new TResAmount(1010, 940, 1390, 650);
            UpCost[13][11] = new TResAmount(7375, 6860, 10150, 4745);
            UpCost[13][1] = new TResAmount(1760, 1635, 2420, 1130);
            UpCost[13][12] = new TResAmount(7860, 7315, 10820, 5060);
            UpCost[13][2] = new TResAmount(2430, 2265, 3345, 1565);
            UpCost[13][13] = new TResAmount(8340, 7765, 11480, 5370);
            UpCost[13][3] = new TResAmount(3060, 2850, 4215, 1970);
            UpCost[13][14] = new TResAmount(8815, 8205, 12130, 5675);
            UpCost[13][4] = new TResAmount(3660, 3405, 5035, 2355);
            UpCost[13][15] = new TResAmount(9280, 8640, 12775, 5975);
            UpCost[13][5] = new TResAmount(4235, 3940, 5830, 2725);
            UpCost[13][16] = new TResAmount(9745, 9065, 13410, 6270);
            UpCost[13][6] = new TResAmount(4790, 4460, 6595, 3085);
            UpCost[13][17] = new TResAmount(10200, 9490, 14035, 6565);
            UpCost[13][7] = new TResAmount(5330, 4960, 7335, 3430);
            UpCost[13][18] = new TResAmount(10650, 9910, 14655, 6855);
            UpCost[13][8] = new TResAmount(5860, 5450, 8060, 3770);
            UpCost[13][19] = new TResAmount(11095, 10325, 15270, 7140);
            UpCost[13][9] = new TResAmount(6375, 5930, 8770, 4100);
            UpCost[13][20] = new TResAmount(11535, 10740, 15880, 7425);
            UpCost[13][10] = new TResAmount(6880, 6400, 9465, 4425);
            UpCost[14] = new Dictionary<int, TResAmount>(21);
            UpCost[14][0] = new TResAmount(1220, 800, 550, 510);
            UpCost[14][11] = new TResAmount(8905, 5840, 4015, 3725);
            UpCost[14][1] = new TResAmount(2125, 1395, 960, 890);
            UpCost[14][12] = new TResAmount(9495, 6225, 4280, 3970);
            UpCost[14][2] = new TResAmount(2940, 1925, 1325, 1230);
            UpCost[14][13] = new TResAmount(10075, 6605, 4540, 4210);
            UpCost[14][3] = new TResAmount(3700, 2425, 1665, 1545);
            UpCost[14][14] = new TResAmount(10645, 6980, 4800, 4450);
            UpCost[14][4] = new TResAmount(4420, 2900, 1995, 1850);
            UpCost[14][15] = new TResAmount(11210, 7350, 5055, 4685);
            UpCost[14][5] = new TResAmount(5115, 3355, 2305, 2140);
            UpCost[14][16] = new TResAmount(11770, 7715, 5305, 4920);
            UpCost[14][6] = new TResAmount(5785, 3795, 2610, 2420);
            UpCost[14][17] = new TResAmount(12320, 8080, 5555, 5150);
            UpCost[14][7] = new TResAmount(6440, 4220, 2905, 2690);
            UpCost[14][18] = new TResAmount(12865, 8435, 5800, 5375);
            UpCost[14][8] = new TResAmount(7075, 4640, 3190, 2960);
            UpCost[14][19] = new TResAmount(13400, 8790, 6040, 5605);
            UpCost[14][9] = new TResAmount(7700, 5050, 3470, 3220);
            UpCost[14][20] = new TResAmount(13935, 9140, 6285, 5825);
            UpCost[14][10] = new TResAmount(8310, 5450, 3745, 3475);
            UpCost[15] = new Dictionary<int, TResAmount>(21);
            UpCost[15][0] = new TResAmount(1345, 995, 1115, 345);
            UpCost[15][11] = new TResAmount(9820, 7265, 8140, 2500);
            UpCost[15][1] = new TResAmount(2340, 1730, 1940, 595);
            UpCost[15][12] = new TResAmount(10470, 7745, 8680, 2665);
            UpCost[15][2] = new TResAmount(3240, 2395, 2685, 825);
            UpCost[15][13] = new TResAmount(11110, 8215, 9210, 2830);
            UpCost[15][3] = new TResAmount(4075, 3015, 3380, 1040);
            UpCost[15][14] = new TResAmount(11740, 8685, 9730, 2990);
            UpCost[15][4] = new TResAmount(4875, 3605, 4040, 1240);
            UpCost[15][15] = new TResAmount(12360, 9145, 10245, 3145);
            UpCost[15][5] = new TResAmount(5640, 4170, 4675, 1435);
            UpCost[15][16] = new TResAmount(12975, 9600, 10755, 3305);
            UpCost[15][6] = new TResAmount(6380, 4720, 5290, 1625);
            UpCost[15][17] = new TResAmount(13580, 10045, 11260, 3460);
            UpCost[15][7] = new TResAmount(7100, 5250, 5885, 1810);
            UpCost[15][18] = new TResAmount(14180, 10490, 11755, 3610);
            UpCost[15][8] = new TResAmount(7800, 5770, 6465, 1985);
            UpCost[15][19] = new TResAmount(14775, 10930, 12250, 3765);
            UpCost[15][9] = new TResAmount(8485, 6280, 7035, 2160);
            UpCost[15][20] = new TResAmount(15365, 11365, 12735, 3910);
            UpCost[15][10] = new TResAmount(9160, 6775, 7595, 2330);
            UpCost[16] = new Dictionary<int, TResAmount>(21);
            UpCost[16][0] = new TResAmount(1085, 1235, 1185, 240);
            UpCost[16][11] = new TResAmount(7910, 9015, 8665, 1750);
            UpCost[16][1] = new TResAmount(1885, 2150, 2065, 420);
            UpCost[16][12] = new TResAmount(8430, 9610, 9235, 1870);
            UpCost[16][2] = new TResAmount(2610, 2975, 2860, 580);
            UpCost[16][13] = new TResAmount(8945, 10200, 9800, 1980);
            UpCost[16][3] = new TResAmount(3285, 3745, 3595, 730);
            UpCost[16][14] = new TResAmount(9455, 10780, 10355, 2095);
            UpCost[16][4] = new TResAmount(3925, 4475, 4300, 870);
            UpCost[16][15] = new TResAmount(9955, 11350, 10905, 2205);
            UpCost[16][5] = new TResAmount(4540, 5180, 4975, 1005);
            UpCost[16][16] = new TResAmount(10450, 11915, 11445, 2315);
            UpCost[16][6] = new TResAmount(5140, 5860, 5630, 1140);
            UpCost[16][17] = new TResAmount(10940, 12470, 11980, 2425);
            UpCost[16][7] = new TResAmount(5720, 6520, 6265, 1265);
            UpCost[16][18] = new TResAmount(11425, 13020, 12510, 2530);
            UpCost[16][8] = new TResAmount(6285, 7160, 6880, 1390);
            UpCost[16][19] = new TResAmount(11900, 13565, 13035, 2635);
            UpCost[16][9] = new TResAmount(6835, 7790, 7485, 1515);
            UpCost[16][20] = new TResAmount(12375, 14105, 13555, 2740);
            UpCost[16][10] = new TResAmount(7375, 8410, 8080, 1635);
            UpCost[17] = new Dictionary<int, TResAmount>(21);
            UpCost[17][0] = new TResAmount(2365, 735, 885, 215);
            UpCost[17][11] = new TResAmount(17280, 5355, 6450, 1580);
            UpCost[17][1] = new TResAmount(4120, 1275, 1540, 375);
            UpCost[17][12] = new TResAmount(18420, 5710, 6875, 1685);
            UpCost[17][2] = new TResAmount(5700, 1765, 2125, 520);
            UpCost[17][13] = new TResAmount(19545, 6055, 7295, 1790);
            UpCost[17][3] = new TResAmount(7175, 2225, 2680, 655);
            UpCost[17][14] = new TResAmount(20655, 6400, 7710, 1890);
            UpCost[17][4] = new TResAmount(8575, 2660, 3200, 785);
            UpCost[17][15] = new TResAmount(21750, 6740, 8115, 1990);
            UpCost[17][5] = new TResAmount(9925, 3075, 3705, 910);
            UpCost[17][16] = new TResAmount(22830, 7075, 8520, 2090);
            UpCost[17][6] = new TResAmount(11225, 3480, 4190, 1030);
            UpCost[17][17] = new TResAmount(23900, 7405, 8920, 2190);
            UpCost[17][7] = new TResAmount(12490, 3870, 4660, 1145);
            UpCost[17][18] = new TResAmount(24955, 7730, 9315, 2285);
            UpCost[17][8] = new TResAmount(13725, 4255, 5125, 1255);
            UpCost[17][19] = new TResAmount(26000, 8055, 9705, 2380);
            UpCost[17][9] = new TResAmount(14935, 4625, 5575, 1365);
            UpCost[17][20] = new TResAmount(27035, 8375, 10090, 2475);
            UpCost[17][10] = new TResAmount(16115, 4995, 6015, 1475);
            UpCost[18] = new Dictionary<int, TResAmount>(21);
            UpCost[18][0] = new TResAmount(1065, 1415, 735, 95);
            UpCost[18][11] = new TResAmount(7785, 10340, 5355, 705);
            UpCost[18][1] = new TResAmount(1855, 2465, 1275, 170);
            UpCost[18][12] = new TResAmount(8300, 11025, 5710, 750);
            UpCost[18][2] = new TResAmount(2570, 3410, 1765, 235);
            UpCost[18][13] = new TResAmount(8810, 11700, 6055, 800);
            UpCost[18][3] = new TResAmount(3235, 4295, 2225, 295);
            UpCost[18][14] = new TResAmount(9310, 12365, 6400, 845);
            UpCost[18][4] = new TResAmount(3865, 5135, 2660, 350);
            UpCost[18][15] = new TResAmount(9800, 13020, 6740, 890);
            UpCost[18][5] = new TResAmount(4470, 5940, 3075, 405);
            UpCost[18][16] = new TResAmount(10290, 13665, 7075, 930);
            UpCost[18][6] = new TResAmount(5060, 6720, 3480, 460);
            UpCost[18][17] = new TResAmount(10770, 14305, 7405, 975);
            UpCost[18][7] = new TResAmount(5630, 7475, 3870, 510);
            UpCost[18][18] = new TResAmount(11245, 14935, 7730, 1020);
            UpCost[18][8] = new TResAmount(6185, 8215, 4255, 560);
            UpCost[18][19] = new TResAmount(11720, 15565, 8055, 1060);
            UpCost[18][9] = new TResAmount(6730, 8940, 4625, 610);
            UpCost[18][20] = new TResAmount(12185, 16180, 8375, 1105);
            UpCost[18][10] = new TResAmount(7265, 9645, 4995, 660);
            UpCost[19] = new Dictionary<int, TResAmount>(21);
            UpCost[19][0] = new TResAmount(62150, 46575, 43800, 47640);
            UpCost[19][11] = new TResAmount(453720, 340015, 319755, 347790);
            UpCost[19][1] = new TResAmount(108210, 81090, 76260, 82945);
            UpCost[19][12] = new TResAmount(483720, 362500, 340900, 370790);
            UpCost[19][2] = new TResAmount(149670, 112165, 105480, 114730);
            UpCost[19][13] = new TResAmount(513265, 384640, 361725, 393435);
            UpCost[19][3] = new TResAmount(188405, 141190, 132775, 144415);
            UpCost[19][14] = new TResAmount(542395, 406470, 382250, 415760);
            UpCost[19][4] = new TResAmount(225225, 168785, 158725, 172645);
            UpCost[19][15] = new TResAmount(571135, 428005, 402505, 437790);
            UpCost[19][5] = new TResAmount(260595, 195285, 183650, 199755);
            UpCost[19][16] = new TResAmount(599515, 449275, 422505, 459550);
            UpCost[19][6] = new TResAmount(294795, 220920, 207755, 225970);
            UpCost[19][17] = new TResAmount(627565, 470295, 442275, 481050);
            UpCost[19][7] = new TResAmount(328030, 245825, 231180, 251445);
            UpCost[19][18] = new TResAmount(655305, 491085, 461825, 502315);
            UpCost[19][8] = new TResAmount(360440, 270115, 254020, 276290);
            UpCost[19][19] = new TResAmount(682755, 511655, 481170, 523355);
            UpCost[19][9] = new TResAmount(392140, 293870, 276360, 300590);
            UpCost[19][20] = new TResAmount(709930, 532020, 500320, 544185);
            UpCost[19][10] = new TResAmount(423210, 317150, 298255, 324405);
            UpCost[20] = new Dictionary<int, TResAmount>(21);
            UpCost[20][0] = new TResAmount(50500, 38600, 40800, 45660);
            UpCost[20][11] = new TResAmount(368670, 281795, 297855, 333335);
            UpCost[20][1] = new TResAmount(87925, 67205, 71035, 79500);
            UpCost[20][12] = new TResAmount(393050, 300430, 317550, 355380);
            UpCost[20][2] = new TResAmount(121615, 92955, 98255, 109960);
            UpCost[20][13] = new TResAmount(417055, 318780, 336950, 377085);
            UpCost[20][3] = new TResAmount(153085, 117015, 123680, 138415);
            UpCost[20][14] = new TResAmount(440720, 336870, 356070, 398480);
            UpCost[20][4] = new TResAmount(183005, 139880, 147855, 165465);
            UpCost[20][15] = new TResAmount(464075, 354720, 374935, 419595);
            UpCost[20][5] = new TResAmount(211745, 161850, 171075, 191450);
            UpCost[20][16] = new TResAmount(487135, 372345, 393570, 440450);
            UpCost[20][6] = new TResAmount(239535, 183090, 193525, 216580);
            UpCost[20][17] = new TResAmount(509930, 389765, 411980, 461055);
            UpCost[20][7] = new TResAmount(266540, 203730, 215345, 240995);
            UpCost[20][18] = new TResAmount(532470, 406995, 430195, 481435);
            UpCost[20][8] = new TResAmount(292875, 223860, 236620, 264805);
            UpCost[20][19] = new TResAmount(554775, 424045, 448215, 501605);
            UpCost[20][9] = new TResAmount(318635, 243550, 257430, 288095);
            UpCost[20][20] = new TResAmount(576855, 440925, 466055, 521570);
            UpCost[20][10] = new TResAmount(343880, 262845, 277825, 310920);
            UpCost[21] = new Dictionary<int, TResAmount>(21);
            UpCost[21][0] = new TResAmount(800, 1010, 585, 370);
            UpCost[21][11] = new TResAmount(5840, 7375, 4270, 2700);
            UpCost[21][1] = new TResAmount(1395, 1760, 1020, 645);
            UpCost[21][12] = new TResAmount(6225, 7860, 4555, 2880);
            UpCost[21][2] = new TResAmount(1925, 2430, 1410, 890);
            UpCost[21][13] = new TResAmount(6605, 8340, 4830, 3055);
            UpCost[21][3] = new TResAmount(2425, 3060, 1775, 1120);
            UpCost[21][14] = new TResAmount(6980, 8815, 5105, 3230);
            UpCost[21][4] = new TResAmount(2900, 3660, 2120, 1340);
            UpCost[21][15] = new TResAmount(7350, 9280, 5375, 3400);
            UpCost[21][5] = new TResAmount(3355, 4235, 2455, 1550);
            UpCost[21][16] = new TResAmount(7715, 9745, 5645, 3570);
            UpCost[21][6] = new TResAmount(3795, 4790, 2775, 1755);
            UpCost[21][17] = new TResAmount(8080, 10200, 5905, 3735);
            UpCost[21][7] = new TResAmount(4220, 5330, 3090, 1955);
            UpCost[21][18] = new TResAmount(8435, 10650, 6170, 3900);
            UpCost[21][8] = new TResAmount(4640, 5860, 3395, 2145);
            UpCost[21][19] = new TResAmount(8790, 11095, 6425, 4065);
            UpCost[21][9] = new TResAmount(5050, 6375, 3690, 2335);
            UpCost[21][20] = new TResAmount(9140, 11535, 6680, 4225);
            UpCost[21][10] = new TResAmount(5450, 6880, 3985, 2520);
            UpCost[22] = new Dictionary<int, TResAmount>(21);
            UpCost[22][0] = new TResAmount(1080, 1150, 1495, 580);
            UpCost[22][11] = new TResAmount(7885, 8395, 10915, 4235);
            UpCost[22][1] = new TResAmount(1880, 2000, 2605, 1010);
            UpCost[22][12] = new TResAmount(8405, 8950, 11635, 4515);
            UpCost[22][2] = new TResAmount(2600, 2770, 3600, 1395);
            UpCost[22][13] = new TResAmount(8920, 9495, 12345, 4790);
            UpCost[22][3] = new TResAmount(3275, 3485, 4530, 1760);
            UpCost[22][14] = new TResAmount(9425, 10035, 13045, 5060);
            UpCost[22][4] = new TResAmount(3915, 4165, 5420, 2100);
            UpCost[22][15] = new TResAmount(9925, 10570, 13740, 5330);
            UpCost[22][5] = new TResAmount(4530, 4820, 6270, 2430);
            UpCost[22][16] = new TResAmount(10420, 11095, 14420, 5595);
            UpCost[22][6] = new TResAmount(5125, 5455, 7090, 2750);
            UpCost[22][17] = new TResAmount(10905, 11610, 15095, 5855);
            UpCost[22][7] = new TResAmount(5700, 6070, 7890, 3060);
            UpCost[22][18] = new TResAmount(11385, 12125, 15765, 6115);
            UpCost[22][8] = new TResAmount(6265, 6670, 8670, 3365);
            UpCost[22][19] = new TResAmount(11865, 12635, 16425, 6370);
            UpCost[22][9] = new TResAmount(6815, 7255, 9435, 3660);
            UpCost[22][20] = new TResAmount(12335, 13135, 17075, 6625);
            UpCost[22][10] = new TResAmount(7355, 7830, 10180, 3950);
            UpCost[23] = new Dictionary<int, TResAmount>(21);
            UpCost[23][0] = new TResAmount(645, 575, 170, 220);
            UpCost[23][11] = new TResAmount(4710, 4200, 1240, 1605);
            UpCost[23][1] = new TResAmount(1125, 1000, 295, 385);
            UpCost[23][12] = new TResAmount(5020, 4475, 1325, 1710);
            UpCost[23][2] = new TResAmount(1555, 1385, 410, 530);
            UpCost[23][13] = new TResAmount(5325, 4750, 1405, 1815);
            UpCost[23][3] = new TResAmount(1955, 1745, 515, 665);
            UpCost[23][14] = new TResAmount(5630, 5020, 1485, 1920);
            UpCost[23][4] = new TResAmount(2335, 2085, 615, 795);
            UpCost[23][15] = new TResAmount(5925, 5285, 1560, 2020);
            UpCost[23][5] = new TResAmount(2705, 2410, 715, 920);
            UpCost[23][16] = new TResAmount(6220, 5545, 1640, 2120);
            UpCost[23][6] = new TResAmount(3060, 2725, 805, 1045);
            UpCost[23][17] = new TResAmount(6515, 5805, 1715, 2220);
            UpCost[23][7] = new TResAmount(3405, 3035, 895, 1160);
            UpCost[23][18] = new TResAmount(6800, 6065, 1790, 2320);
            UpCost[23][8] = new TResAmount(3740, 3335, 985, 1275);
            UpCost[23][19] = new TResAmount(7085, 6315, 1870, 2415);
            UpCost[23][9] = new TResAmount(4070, 3630, 1075, 1390);
            UpCost[23][20] = new TResAmount(7370, 6570, 1940, 2515);
            UpCost[23][10] = new TResAmount(4390, 3915, 1160, 1500);
            UpCost[24] = new Dictionary<int, TResAmount>(21);
            UpCost[24][0] = new TResAmount(1275, 1625, 905, 290);
            UpCost[24][11] = new TResAmount(9310, 11865, 6605, 2115);
            UpCost[24][1] = new TResAmount(2220, 2830, 1575, 505);
            UpCost[24][12] = new TResAmount(9925, 12650, 7045, 2255);
            UpCost[24][2] = new TResAmount(3070, 3915, 2180, 700);
            UpCost[24][13] = new TResAmount(10530, 13420, 7475, 2395);
            UpCost[24][3] = new TResAmount(3865, 4925, 2745, 880);
            UpCost[24][14] = new TResAmount(11125, 14180, 7900, 2530);
            UpCost[24][4] = new TResAmount(4620, 5890, 3280, 1050);
            UpCost[24][15] = new TResAmount(11715, 14935, 8315, 2665);
            UpCost[24][5] = new TResAmount(5345, 6815, 3795, 1215);
            UpCost[24][16] = new TResAmount(12300, 15675, 8730, 2795);
            UpCost[24][6] = new TResAmount(6050, 7710, 4295, 1375);
            UpCost[24][17] = new TResAmount(12875, 16410, 9140, 2930);
            UpCost[24][7] = new TResAmount(6730, 8575, 4775, 1530);
            UpCost[24][18] = new TResAmount(13445, 17135, 9540, 3060);
            UpCost[24][8] = new TResAmount(7395, 9425, 5250, 1680);
            UpCost[24][19] = new TResAmount(14005, 17850, 9940, 3185);
            UpCost[24][9] = new TResAmount(8045, 10255, 5710, 1830);
            UpCost[24][20] = new TResAmount(14565, 18560, 10340, 3315);
            UpCost[24][10] = new TResAmount(8680, 11065, 6165, 1975);
            UpCost[25] = new Dictionary<int, TResAmount>(21);
            UpCost[25][0] = new TResAmount(1310, 1205, 1080, 500);
            UpCost[25][11] = new TResAmount(9565, 8795, 7885, 3650);
            UpCost[25][1] = new TResAmount(2280, 2100, 1880, 870);
            UpCost[25][12] = new TResAmount(10195, 9380, 8405, 3890);
            UpCost[25][2] = new TResAmount(3155, 2900, 2600, 1205);
            UpCost[25][13] = new TResAmount(10820, 9950, 8920, 4130);
            UpCost[25][3] = new TResAmount(3970, 3655, 3275, 1515);
            UpCost[25][14] = new TResAmount(11435, 10515, 9425, 4365);
            UpCost[25][4] = new TResAmount(4745, 4365, 3915, 1810);
            UpCost[25][15] = new TResAmount(12040, 11075, 9925, 4595);
            UpCost[25][5] = new TResAmount(5495, 5055, 4530, 2095);
            UpCost[25][16] = new TResAmount(12635, 11625, 10420, 4825);
            UpCost[25][6] = new TResAmount(6215, 5715, 5125, 2370);
            UpCost[25][17] = new TResAmount(13230, 12170, 10905, 5050);
            UpCost[25][7] = new TResAmount(6915, 6360, 5700, 2640);
            UpCost[25][18] = new TResAmount(13815, 12705, 11385, 5270);
            UpCost[25][8] = new TResAmount(7595, 6990, 6265, 2900);
            UpCost[25][19] = new TResAmount(14390, 13240, 11865, 5495);
            UpCost[25][9] = new TResAmount(8265, 7605, 6815, 3155);
            UpCost[25][20] = new TResAmount(14965, 13765, 12335, 5710);
            UpCost[25][10] = new TResAmount(8920, 8205, 7355, 3405);
            UpCost[26] = new Dictionary<int, TResAmount>(21);
            UpCost[26][0] = new TResAmount(1200, 1480, 1640, 450);
            UpCost[26][11] = new TResAmount(8760, 10805, 11985, 3285);
            UpCost[26][1] = new TResAmount(2090, 2575, 2860, 785);
            UpCost[26][12] = new TResAmount(9340, 11520, 12775, 3500);
            UpCost[26][2] = new TResAmount(2890, 3565, 3955, 1085);
            UpCost[26][13] = new TResAmount(9910, 12225, 13560, 3715);
            UpCost[26][3] = new TResAmount(3640, 4485, 4975, 1365);
            UpCost[26][14] = new TResAmount(10475, 12915, 14325, 3925);
            UpCost[26][4] = new TResAmount(4350, 5365, 5950, 1630);
            UpCost[26][15] = new TResAmount(11030, 13600, 15085, 4135);
            UpCost[26][5] = new TResAmount(5030, 6205, 6885, 1885);
            UpCost[26][16] = new TResAmount(11575, 14275, 15835, 4340);
            UpCost[26][6] = new TResAmount(5690, 7020, 7785, 2135);
            UpCost[26][17] = new TResAmount(12115, 14945, 16575, 4545);
            UpCost[26][7] = new TResAmount(6335, 7810, 8665, 2375);
            UpCost[26][18] = new TResAmount(12655, 15605, 17310, 4745);
            UpCost[26][8] = new TResAmount(6960, 8585, 9520, 2610);
            UpCost[26][19] = new TResAmount(13185, 16260, 18035, 4945);
            UpCost[26][9] = new TResAmount(7570, 9340, 10360, 2840);
            UpCost[26][20] = new TResAmount(13705, 16905, 18755, 5140);
            UpCost[26][10] = new TResAmount(8170, 10080, 11180, 3065);
            UpCost[27] = new Dictionary<int, TResAmount>(21);
            UpCost[27][0] = new TResAmount(2250, 1330, 835, 230);
            UpCost[27][11] = new TResAmount(16425, 9695, 6110, 1665);
            UpCost[27][1] = new TResAmount(3915, 2315, 1455, 400);
            UpCost[27][12] = new TResAmount(17510, 10340, 6510, 1775);
            UpCost[27][2] = new TResAmount(5420, 3200, 2015, 550);
            UpCost[27][13] = new TResAmount(18580, 10970, 6910, 1885);
            UpCost[27][3] = new TResAmount(6820, 4025, 2535, 690);
            UpCost[27][14] = new TResAmount(19635, 11595, 7300, 1995);
            UpCost[27][4] = new TResAmount(8155, 4815, 3030, 825);
            UpCost[27][15] = new TResAmount(20675, 12205, 7690, 2100);
            UpCost[27][5] = new TResAmount(9435, 5570, 3510, 955);
            UpCost[27][16] = new TResAmount(21705, 12815, 8070, 2205);
            UpCost[27][6] = new TResAmount(10670, 6300, 3970, 1085);
            UpCost[27][17] = new TResAmount(22720, 13415, 8450, 2305);
            UpCost[27][7] = new TResAmount(11875, 7010, 4415, 1205);
            UpCost[27][18] = new TResAmount(23725, 14005, 8820, 2410);
            UpCost[27][8] = new TResAmount(13050, 7705, 4850, 1325);
            UpCost[27][19] = new TResAmount(24720, 14595, 9190, 2510);
            UpCost[27][9] = new TResAmount(14195, 8380, 5280, 1440);
            UpCost[27][20] = new TResAmount(25700, 15175, 9555, 2610);
            UpCost[27][10] = new TResAmount(15320, 9045, 5695, 1555);
            UpCost[28] = new Dictionary<int, TResAmount>(21);
            UpCost[28][0] = new TResAmount(1135, 1710, 770, 130);
            UpCost[28][11] = new TResAmount(8300, 12470, 5610, 960);
            UpCost[28][1] = new TResAmount(1980, 2975, 1340, 230);
            UpCost[28][12] = new TResAmount(8845, 13295, 5980, 1025);
            UpCost[28][2] = new TResAmount(2735, 4115, 1850, 315);
            UpCost[28][13] = new TResAmount(9385, 14110, 6345, 1085);
            UpCost[28][3] = new TResAmount(3445, 5180, 2330, 400);
            UpCost[28][14] = new TResAmount(9920, 14910, 6705, 1150);
            UpCost[28][4] = new TResAmount(4120, 6190, 2785, 475);
            UpCost[28][15] = new TResAmount(10445, 15700, 7060, 1210);
            UpCost[28][5] = new TResAmount(4765, 7165, 3220, 550);
            UpCost[28][16] = new TResAmount(10965, 16480, 7410, 1270);
            UpCost[28][6] = new TResAmount(5390, 8105, 3645, 625);
            UpCost[28][17] = new TResAmount(11480, 17250, 7760, 1330);
            UpCost[28][7] = new TResAmount(6000, 9015, 4055, 695);
            UpCost[28][18] = new TResAmount(11985, 18015, 8100, 1390);
            UpCost[28][8] = new TResAmount(6590, 9910, 4455, 765);
            UpCost[28][19] = new TResAmount(12485, 18765, 8440, 1445);
            UpCost[28][9] = new TResAmount(7170, 10780, 4850, 830);
            UpCost[28][20] = new TResAmount(12985, 19515, 8775, 1505);
            UpCost[28][10] = new TResAmount(7740, 11635, 5230, 895);
            UpCost[29] = new Dictionary<int, TResAmount>(21);
            UpCost[29][0] = new TResAmount(53840, 79475, 54300, 65665);
            UpCost[29][11] = new TResAmount(393035, 580195, 396410, 479380);
            UpCost[29][1] = new TResAmount(93735, 138375, 94540, 114330);
            UpCost[29][12] = new TResAmount(419025, 618565, 422625, 511080);
            UpCost[29][2] = new TResAmount(129655, 191395, 130765, 158135);
            UpCost[29][13] = new TResAmount(444620, 656345, 448440, 542295);
            UpCost[29][3] = new TResAmount(163205, 240925, 164605, 199060);
            UpCost[29][14] = new TResAmount(469850, 693590, 473885, 573070);
            UpCost[29][4] = new TResAmount(195100, 288010, 196780, 237965);
            UpCost[29][15] = new TResAmount(494745, 730340, 498995, 603435);
            UpCost[29][5] = new TResAmount(225740, 333235, 227680, 275330);
            UpCost[29][16] = new TResAmount(519330, 766635, 523790, 633420);
            UpCost[29][6] = new TResAmount(255365, 376970, 257560, 311465);
            UpCost[29][17] = new TResAmount(543630, 802505, 548300, 663060);
            UpCost[29][7] = new TResAmount(284155, 419470, 286595, 346580);
            UpCost[29][18] = new TResAmount(567660, 837980, 572535, 692370);
            UpCost[29][8] = new TResAmount(312235, 460920, 314915, 380825);
            UpCost[29][19] = new TResAmount(591440, 873080, 596520, 721370);
            UpCost[29][9] = new TResAmount(339690, 501455, 342610, 414320);
            UpCost[29][20] = new TResAmount(614980, 907835, 620260, 750085);
            UpCost[29][10] = new TResAmount(366605, 541185, 369755, 447145);
            UpCost[30] = new Dictionary<int, TResAmount>(21);
            UpCost[30][0] = new TResAmount(38600, 49100, 37300, 34460);
            UpCost[30][11] = new TResAmount(281795, 358450, 272305, 251570);
            UpCost[30][1] = new TResAmount(67205, 85490, 64945, 60000);
            UpCost[30][12] = new TResAmount(300430, 382150, 290310, 268205);
            UpCost[30][2] = new TResAmount(92955, 118245, 89825, 82985);
            UpCost[30][13] = new TResAmount(318780, 405495, 308045, 284590);
            UpCost[30][3] = new TResAmount(117015, 148845, 113070, 104465);
            UpCost[30][14] = new TResAmount(336870, 428505, 325525, 300740);
            UpCost[30][4] = new TResAmount(139880, 177935, 135170, 124880);
            UpCost[30][15] = new TResAmount(354720, 451210, 342770, 316675);
            UpCost[30][5] = new TResAmount(161850, 205875, 156400, 144490);
            UpCost[30][16] = new TResAmount(372345, 473630, 359805, 332410);
            UpCost[30][6] = new TResAmount(183090, 232895, 176925, 163455);
            UpCost[30][17] = new TResAmount(389765, 495790, 376640, 347965);
            UpCost[30][7] = new TResAmount(203730, 259150, 196870, 181880);
            UpCost[30][18] = new TResAmount(406995, 517705, 393290, 363345);
            UpCost[30][8] = new TResAmount(223860, 284760, 216325, 199850);
            UpCost[30][19] = new TResAmount(424045, 539395, 409765, 378565);
            UpCost[30][9] = new TResAmount(243550, 309800, 235345, 217430);
            UpCost[30][20] = new TResAmount(440925, 560865, 426075, 393630);
            UpCost[30][10] = new TResAmount(262845, 334345, 253995, 234655);

        }
        static private void InitResearchCost()
        {
            ResearchCost = new Dictionary<int, TResAmount>(24);

            ResearchCost[2] = new TResAmount(700, 620, 1480, 580);
            ResearchCost[3] = new TResAmount(1000, 740, 1880, 640);
            ResearchCost[4] = new TResAmount(940, 740, 360, 400);
            ResearchCost[5] = new TResAmount(3400, 1860, 2760, 760);
            ResearchCost[6] = new TResAmount(3400, 2660, 6600, 1240);
            ResearchCost[7] = new TResAmount(5500, 1540, 4200, 580);
            ResearchCost[8] = new TResAmount(5800, 5500, 5000, 700);
            ResearchCost[9] = new TResAmount(15880, 13800, 36400, 22660);

            ResearchCost[12] = new TResAmount(970, 380, 880, 400);
            ResearchCost[13] = new TResAmount(880, 580, 1560, 580);
            ResearchCost[14] = new TResAmount(1060, 500, 600, 460);
            ResearchCost[15] = new TResAmount(2320, 1180, 2520, 610);
            ResearchCost[16] = new TResAmount(2800, 2160, 4040, 640);
            ResearchCost[17] = new TResAmount(6100, 1300, 3000, 580);
            ResearchCost[18] = new TResAmount(5500, 4900, 5000, 520);
            ResearchCost[19] = new TResAmount(18250, 13500, 20400, 16480);

            ResearchCost[22] = new TResAmount(940, 700, 1680, 520);
            ResearchCost[23] = new TResAmount(1120, 700, 360, 400);
            ResearchCost[24] = new TResAmount(2200, 1900, 2040, 520);
            ResearchCost[25] = new TResAmount(2260, 1420, 2440, 880);
            ResearchCost[26] = new TResAmount(3100, 2580, 5600, 1180);
            ResearchCost[27] = new TResAmount(5800, 2320, 2840, 610);
            ResearchCost[28] = new TResAmount(5860, 5900, 5240, 700);
            ResearchCost[29] = new TResAmount(15880, 22900, 25200, 22660);
        }
        static private void InitTroopCost()
        {
            TroopCost = new Dictionary<int, TResAmount>(30);
            TroopCost[1] = new TResAmount(120, 100, 150, 30);
            TroopCost[2] = new TResAmount(100, 130, 160, 70);
            TroopCost[3] = new TResAmount(150, 160, 210, 80);
            TroopCost[4] = new TResAmount(140, 160, 20, 40);
            TroopCost[5] = new TResAmount(550, 440, 320, 100);
            TroopCost[6] = new TResAmount(550, 640, 800, 180);
            TroopCost[7] = new TResAmount(900, 360, 500, 70);
            TroopCost[8] = new TResAmount(950, 1350, 600, 90);
            TroopCost[9] = new TResAmount(30750, 27200, 45000, 37500);
            TroopCost[10] = new TResAmount(5800, 5300, 7200, 5500);
            TroopCost[11] = new TResAmount(95, 75, 40, 40);
            TroopCost[12] = new TResAmount(145, 70, 85, 40);
            TroopCost[13] = new TResAmount(130, 120, 170, 70);
            TroopCost[14] = new TResAmount(160, 100, 50, 50);
            TroopCost[15] = new TResAmount(370, 270, 290, 75);
            TroopCost[16] = new TResAmount(450, 515, 480, 80);
            TroopCost[17] = new TResAmount(1000, 300, 350, 70);
            TroopCost[18] = new TResAmount(900, 1200, 600, 60);
            TroopCost[19] = new TResAmount(35500, 26600, 25000, 27200);
            TroopCost[20] = new TResAmount(7200, 5500, 5800, 6500);
            TroopCost[21] = new TResAmount(100, 130, 55, 30);
            TroopCost[22] = new TResAmount(140, 150, 185, 60);
            TroopCost[23] = new TResAmount(170, 150, 20, 40);
            TroopCost[24] = new TResAmount(350, 450, 230, 60);
            TroopCost[25] = new TResAmount(360, 330, 280, 120);
            TroopCost[26] = new TResAmount(500, 620, 675, 170);
            TroopCost[27] = new TResAmount(950, 555, 330, 75);
            TroopCost[28] = new TResAmount(960, 1450, 630, 90);
            TroopCost[29] = new TResAmount(30750, 45400, 31000, 37500);
            TroopCost[30] = new TResAmount(5500, 7000, 5300, 4900);
        }
        static public TResAmount Cost(int gid, int level)
        {
            try
            {
                if (gid < 0)
                    return new TResAmount(0, 0, 0, 0);
                if (level >= BuildingCost[gid].data.Length)
                    return new TResAmount(0, 0, 0, 0);
                else
                    return BuildingCost[gid].data[level];
            }
            catch (Exception)
            {
                //throw (new Exception("_cost[" + gid.ToString() + ", " + level.ToString() + "]"));
                return new TResAmount(0, 0, 0, 0);
            }

        }
        static public bool CheckLevelFull(int gid, int level, bool capital)
        {
            return (!capital && gid < 5 && level >= 10) || level >= BuildingCost[gid].length;
        }
    }
    public class TBL
    {
        public int Gid, Level;
        public TBL(int Gid, int Level)
        {
            this.Gid = Gid;
            this.Level = Level;
        }
    }
}
