using AssemblyCSharp.Mod.Xmap;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Mod.Auto
{
    public class AutoCSKB : ThreadActionUpdate<AutoCSKB>
    {
        public override int Interval => 500;

        protected override void update()
        {
            // nextMapQtl();
            Waypoint w = FindWaypoint(TileMap.mapID);

            MoveMyChar(720,720);
        }
        private void nextMapQtl()
        {
            if (!checkTileMap())
            {
                Thread.Sleep(500);
                if (TileMap.mapID != 27 && TileMap.mapID != 102)
                {
                    linhtinh.useItem(194, 0);
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
            HashSet<int> validMapIDs = new HashSet<int> { 102, 92, 93, 94, 96, 98, 99 };
            return validMapIDs.Contains(TileMap.mapID);
        }
        private Waypoint FindWaypoint(int idMap)
        {
            Waypoint waypoint;
            string textPopup;
            for (int i = 0; i < TileMap.vGo.size(); i++)
            {
                waypoint = (Waypoint)TileMap.vGo.elementAt(i);
                textPopup = GetTextPopup(waypoint.popup);
                if (textPopup.Equals(TileMap.mapNames[idMap]))
                {
                    return waypoint;
                }
            }
            return null;
        }
        private  string GetTextPopup(PopUp popUp)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < popUp.says.Length; i++)
            {
                stringBuilder.Append(popUp.says[i]);
                stringBuilder.Append(" ");
            }
            return stringBuilder.ToString().Trim();
        }
        private int GetPosWaypointX(Waypoint waypoint)
        {
            if (waypoint.maxX < 60)
                return 15;
            if (waypoint.minX > TileMap.pxw - 60)
                return TileMap.pxw - 15;
            return waypoint.minX + 30;
        }
        private int GetPosWaypointY(Waypoint waypoint)
        {
            return waypoint.maxY;
        }
        public static void MoveMyChar(int x, int y)
        {
            Char.myCharz().cx = x;
            Char.myCharz().cy = y;
            Service.gI().charMove();
            Char.myCharz().cx = x;
            Char.myCharz().cy = y + 1;
            Service.gI().charMove();
            Char.myCharz().cx = x;
            Char.myCharz().cy = y;
            Service.gI().charMove();
        }
    }
}