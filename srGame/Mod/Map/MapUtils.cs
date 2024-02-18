using System.Collections.Generic;
public class MapUtils
{
    public static bool xoataubay(object obj)
    {
        Teleport teleport = (Teleport)obj;
        if (teleport.isMe)
        {
            Char.myCharz().isTeleport = false;
            if (teleport.type == 0)
            {
                Controller.isStopReadMessage = false;
                Char.ischangingMap = true;
            }
            Teleport.vTeleport.removeElement(teleport);
            return true;
        }
        return false;
    }
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
    // Lấy ID của map tiếp theo dựa trên vị trí Waypoint và hành tinh
    public static int GetMapIdForWaypoint(WaypointPosition waypointPosition, byte planetID)
    {
        // Tạo một bản đồ (dictionary) để lưu trữ ánh xạ từ vị trí của Waypoint sang ID của map
        Dictionary<WaypointPosition, int> mapIdMap = GetMapIdMapForPlanet(planetID);

        // Kiểm tra xem có ánh xạ nào cho vị trí của Waypoint không
        if (mapIdMap.ContainsKey(waypointPosition))
        {
            // Nếu có, trả về ID của map tương ứng
            return mapIdMap[waypointPosition];
        }
        else
        {
            // Nếu không, trả về giá trị không hợp lệ
            return -1;
        }
    }

    // Tạo bản đồ ánh xạ từ vị trí của Waypoint sang ID của map cho mỗi hành tinh
    private static Dictionary<WaypointPosition, int> GetMapIdMapForPlanet(byte planetID)
    {
        Dictionary<WaypointPosition, int> mapIdMap = new Dictionary<WaypointPosition, int>();

        // Tùy thuộc vào hành tinh, thêm ánh xạ từ vị trí của Waypoint sang ID của map tương ứng
        switch (planetID)
        {
            case 1: // Hành tinh A
                mapIdMap.Add(new WaypointPosition(10, 20), 1); // Ví dụ: ánh xạ từ vị trí Waypoint (10, 20) sang ID của Map A
                mapIdMap.Add(new WaypointPosition(30, 40), 2); // Ví dụ: ánh xạ từ vị trí Waypoint (30, 40) sang ID của Map B
                // Thêm ánh xạ cho các Waypoint khác tại đây
                break;
            case 2: // Hành tinh B
                // Thêm ánh xạ cho hành tinh B tại đây
                break;
            // Thêm các case cho các hành tinh khác nếu cần
            default:
                // Xử lý cho trường hợp hành tinh không hợp lệ (nếu có)
                break;
        }

        return mapIdMap;
    }
}
