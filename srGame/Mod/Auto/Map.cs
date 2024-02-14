using System.Collections.Generic;

public class WaypointPosition
{
    public int X { get; private set; }
    public int Y { get; private set; }

    public WaypointPosition(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public class MapUtils
{
    // Lấy vị trí của các waypoint trên map hiện tại
    public static List<WaypointPosition> GetWaypointsPositions()
    {
        List<WaypointPosition> waypointPositions = new List<WaypointPosition>();

        // Lặp qua tất cả các waypoint trên map hiện tại
        for (int i = 0; i < TileMap.vGo.size(); i++)
        {
            Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt(i);
            int x = (waypoint.minX + waypoint.maxX) / 2;
            int y = waypoint.maxY;
            waypointPositions.Add(new WaypointPosition(x, y));
        }

        return waypointPositions;
    }
}
