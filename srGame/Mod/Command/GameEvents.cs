using System.Collections;
using System.Security.Policy;
using static System.Net.Mime.MediaTypeNames;
public class GameEvents
{
    /// <summary>
    /// Kích hoạt khi người chơi chat.
    /// </summary>
    /// <param name="text">Nội dung chat.</param>
    /// <returns>true nếu huỷ bỏ nội dung chat.</returns>
    public static bool onSendChat(string text)
    {
        bool result = ChatCommandHandler.checkAndExecuteChatCommand(text);
        return result;
    }
    public static void onPaintChatTextField(mGraphics g)
    {

    }
    public static void onUpdateChatTextField()
    {
    }
    public static void onStartChatTextField()           
    {
    }
    public static void onUpdateGameScr()
    {
        if (GameDataStorage.dapdo && GameCanvas.gameTick % 10 == 0)
        {
            linhtinh.dapdo();
        }
    }
    
    /// <summary>
    /// Nhận thông tin boss
    /// </summary>
    public static void ThongTinNhanDuoc(string s)
    {
        if (s.ToLower().StartsWith("boss"))
        {
            GameDataStorage.bBoss.addElement(new boss(s));
            if (GameDataStorage.bBoss.size() > 5)
            {
                GameDataStorage.bBoss.removeElementAt(0);
            }
        }
    }
    public static void ThongBaoNhanDuoc(string s)
    {
        if (s.ToLower().Equals("không thể thực hiện"))
        {
            GameDataStorage.dokhuBoss = false;
        }
    }
    /// <summary>
    /// hiển thị thông tin ra màn hình
    /// </summary>
    public static void onPaint(mGraphics g)
    {
        if (GameDataStorage.tbBoss)
        {
            int num = 42;
            for (int i = 0; i < GameDataStorage.bBoss.size(); i++)
            {
                g.setColor(2721889, 0.5f);
                g.fillRect(GameCanvas.w - 23, num + 2, 25, 9);
                ((boss)GameDataStorage.bBoss.elementAt(i)).paintboss(g, GameCanvas.w - 2, num, mFont.RIGHT);
                num += 10;
            }
        }
        if (true)
        {
            g.setColor(UnityEngine.Color.red);
            for (int i = 0; i < GameScr.vCharInMap.size(); i++)
            {
                Char nvat = (Char)GameScr.vCharInMap.elementAt(i);
                if (nvat.cTypePk == 5)
                {
                    if (GameDataStorage.dokhuBoss == true)
                    {
                        GameDataStorage.dokhuBoss = false;
                    }
                    g.drawLine(Char.myCharz().cx - GameScr.cmx, Char.myCharz().cy - GameScr.cmy, nvat.cx-GameScr.cmx, nvat.cy - GameScr.cmy);
                    mFont.tahoma_7_white.drawString(g, nvat.cName, 180, 2, 0);
                }
            }
        }
        
        mFont.tahoma_7b_red.drawString(g, $"{TileMap.mapName} Khu {TileMap.zoneID} ID {TileMap.mapID}", 100, 35, 0); 
    }
    /// <summary>
    /// Kích hoạt sau khi game khởi động thành công.
    /// </summary>
    public static void onGameStarted()
    {
        ChatCommandHandler.loadDefalutChatCommands();
    }

    /// <summary>
    /// Kích hoạt sau khi load xong KeyMap.
    /// </summary>
    /// <param name="h"></param>
    public static void onKeyMapLoaded(Hashtable h)
    {
        Utilities.AddKeyMap(h);
    }

    /// <summary>       
    /// Kích hoạt khi nhấn phím tắt (GameScr) chưa được xử lý.
    /// </summary>
    public static void onGameScrPressHotkeysUnassigned()
    {
        Utilities.AddHotkeys();
    }
}