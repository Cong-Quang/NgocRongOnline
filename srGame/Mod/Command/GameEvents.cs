﻿using Mod.Auto; // Sử dụng namespace Mod.Auto để truy cập các lớp và phương thức trong đó
using System.Collections; // Sử dụng namespace System.Collections để sử dụng các cấu trúc dữ liệu dạng ICollection
using System.Threading; // Sử dụng namespace System.Threading để sử dụng luồng và đa luồng
using UnityEngine; // Sử dụng namespace UnityEngine để sử dụng các thành phần của Unity

public class GameEvents
{
    // Phương thức OnSendChat() được gọi khi có tin nhắn chat được gửi đi, kiểm tra và thực thi lệnh chat
    public static bool OnSendChat(string text)
    {
        return ChatCommandHandler.checkAndExecuteChatCommand(text);
    }

    // Phương thức OnUpdateGameScr() được gọi để cập nhật trạng thái trò chơi
    public static void OnUpdateGameScr()
    {
        // Kiểm tra và thực hiện tính năng tấn sát
        if (TanSat.Interval && GameCanvas.gameTick % 20 == 0)
        {
            TanSat.tansat();
        }

        // Kiểm tra và thực hiện tính năng "đập đồ"
        if (GameDataStorage.dapdo && GameCanvas.gameTick % 10 == 0)
        {
            linhtinh.dapdo();
        }

        // Mở giao diện khu vực mỗi 10 giây (tính theo thời gian thực của trò chơi)
        if (GameCanvas.gameTick % (int)(10 * Time.timeScale) == 0)
        {
            Service.gI().openUIZone();
        }
    }

    // Phương thức ThongTinNhanDuoc() được gọi khi nhận được thông tin từ server, kiểm tra và xử lý thông tin về boss
    public static void ThongTinNhanDuoc(string s)
    {
        if (s.ToLower().StartsWith("boss"))
        {
            GameDataStorage.bBoss.addElement(new boss(s));
            if (GameDataStorage.bBoss.size() > 7)
            {
                GameDataStorage.bBoss.removeElementAt(0);
            }
        }
    }

    // Phương thức ThongBaoNhanDuoc() được gọi khi nhận được thông báo từ server, kiểm tra và xử lý thông báo "không thể thực hiện"
    public static void ThongBaoNhanDuoc(string s)
    {
        if (s.ToLower().Equals("không thể thực hiện"))
        {
            GameDataStorage.dokhuBoss = false;
        }
    }

    // Phương thức OnPaint() được gọi để vẽ giao diện trò chơi
    public static void OnPaint(mGraphics g)
    {
        // Vẽ thông tin về boss trên giao diện
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

        // Vẽ đường dẫn đến nhân vật địch trên bản đồ
        g.setColor(UnityEngine.Color.red);
        for (int i = 0; i < GameScr.vCharInMap.size(); i++)
        {
            Char nvat = (Char)GameScr.vCharInMap.elementAt(i);
            if (nvat.cTypePk == 5)
            {
                AutoCSKB.gI.dokhu = true;
                if (GameDataStorage.dokhuBoss == true)
                {
                    GameDataStorage.dokhuBoss = false;
                }
                g.drawLine(Char.myCharz().cx - GameScr.cmx, Char.myCharz().cy - GameScr.cmy, nvat.cx - GameScr.cmx, nvat.cy - GameScr.cmy);
                mFont.tahoma_7_white.drawString(g, nvat.cName, 180, 2, 0);
            }
        }

        // Hiển thị thông tin về vị trí bản đồ và khu vực hiện tại
        mFont.tahoma_7b_red.drawString(g, $"{TileMap.mapName} Khu {TileMap.zoneID} ID {TileMap.mapID}", 100, 35, 0);
    }

    // Phương thức OnGameStarted() được gọi khi trò chơi bắt đầu, tải các lệnh chat mặc định
    public static void OnGameStarted()
    {
        ChatCommandHandler.loadDefalutChatCommands();
    }

    // Phương thức OnKeyMapLoaded() được gọi khi tải các phím tắt, thêm các phím tắt vào hệ thống
    public static void OnKeyMapLoaded(Hashtable h)
    {
        Utilities.AddKeyMap(h);
    }

    // Phương thức OnGameScrPressHotkeysUnassigned() được gọi khi có phím tắt chưa được gán, thêm các phím tắt vào trò chơi
    public static void OnGameScrPressHotkeysUnassigned()
    {
        Utilities.AddHotkeys();
    }
}
