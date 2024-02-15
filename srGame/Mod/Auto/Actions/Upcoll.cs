using System.Collections.Generic;
using System.Threading;

namespace Mod.Auto.Actions
{
    public class upColl : ThreadActionUpdate<upColl>
    {
        public override int Interval => 500;
        private int zone;
        public bool dokhu;
        private static bool isRunningAnMD = false;
        private Thread anMDThread;
        List<WaypointPosition> waypointPositions;
        protected override void Update()
        {
            if (checkTileMap())
            {
                nextMap();
            }
            nextMapQtl();
            if (TileMap.mapID == 99)
            {
                dokhux();
            }
            if (TileMap.mapID == 99 && !isRunningAnMD)
            {
                anMDThread = new Thread(anMD);
                anMDThread.Start();
            }
        }

        private void anMD()
        {
            isRunningAnMD = true;

            if (!AutoAttack.gI.IsActing)
                AutoAttack.gI.Toggle();

            if (!TanSat.Interval)
                TanSat.Interval = true;

            if (TileMap.mapID == 99)
            {
                Helper.useItem(379, 0);
                Thread.Sleep(1800000); // Chờ 1800000 giây (30 phút)

                for (int i = 0; i < 500; i++)
                {
                    Helper.useItem(380, 0);
                    Thread.Sleep(50);
                }
            }

            isRunningAnMD = false;
        }

        private void dokhux()
        {
            while (dokhu || checkKhu())
            {
                Utilities.ChuyenKu(zone++);
                Thread.Sleep(1340);
            }
            if (zone == 11)
            {
                zone = 0;
            }
        }

        private bool checkKhu()
        {
            for (int i = 0; i < GameScr.vCharInMap.size(); i++)
            {
                Char nvat = (Char)GameScr.vCharInMap.elementAt(i);
                if (nvat != null)
                    return true;
            }
            return false;
        }

        private void nextMap()
        {
            int cx = 0;
            int cy = 0;
            waypointPositions = MapUtils.GetWaypointsPositions();
            switch (TileMap.mapID)
            {
                case 102:
                case 92:
                case 97:
                    cx = waypointPositions[0].X;
                    cy = waypointPositions[0].Y;
                    break;
                case 93:
                case 94:
                case 96:
                case 98:
                    cx = waypointPositions[1].X;
                    cy = waypointPositions[1].Y;
                    break;
                default:
                    // Do something if TileMap.mapID doesn't match any case
                    break;
            }

            if (TileMap.mapID != 99)
            {
                Char.myCharz().cx = cx;
                Char.myCharz().cy = cy;
                Service.gI().charMove();
            }
        }
        private void nextMapQtl()
        {
            if (!checkTileMap())
            {
                Thread.Sleep(500);
                if (TileMap.mapID != 27 && TileMap.mapID != 102)
                {
                    Helper.useItem(194, 0);
                    Service.gI().requestMapSelect(9);
                }
                else
                {
                    Service.gI().openMenu(38);
                    Service.gI().confirmMenu(38, 1);
                }
            }
        }

        public static bool checkTileMap()
        {
            HashSet<int> validMapIDs = new HashSet<int> { 102, 92, 93, 94, 96, 97, 98, 99 };
            return validMapIDs.Contains(TileMap.mapID);
        }
    }
}
