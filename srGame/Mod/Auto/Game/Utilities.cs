using UnityEngine;
using Mod.Auto.Actions;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
public class Utilities
{
    // Phương thức EditSpeedRun() cho phép chỉnh sửa tốc độ chạy của nhân vật
    [ChatCommand("tdc", "cspeed", "s")]
    public static void EditSpeedRun(int speed)
    {
        Char.myCharz().cspeed = speed;
        GameScr.info1.addInfo("Tốc độ chạy: " + speed, 0);
    }

    // Phương thức Cheat() cho phép chỉnh sửa tốc độ chơi của trò chơi
    [ChatCommand("chs", "cheat")]
    public static void Cheat(float speed)
    {
        Time.timeScale = speed;
        GameScr.info1.addInfo("Tốc độ game: " + speed, 0);
    }
    // Phương thức AddKeyMap() thêm các phím tắt vào bản đồ phím tắt
    public static void AddKeyMap(Hashtable h)
    {
        h.Add(KeyCode.Slash, 47);
    }

    // Phương thức ToggleAutoAttack() bật hoặc tắt tính năng tự động tấn công
    [ChatCommand("ak")]
    public static void ToggleAutoAttack()
    {
        AutoAttack.gI.Toggle();
        string message = AutoAttack.gI.IsActing ? "Đang tự tấn công" : "Đã tắt tự tấn công";
        GameScr.info1.addInfo(message, 0);
    }

    // Các phương thức dùng item cụ thể
    [ChatCommand("bongtai")]
    public static void Bongtai() => Helper.useItem(921, 0);

    [ChatCommand("dungcsdb")]
    public static void Dungcsdb() => Helper.useItem(194, 0);

    // Phương thức ThongBaoBoss() bật hoặc tắt thông báo xuất hiện của boss
    [ChatCommand("tbb")]
    public static void ThongBaoBoss()
    {
        NotifBoss.tbBoss = !NotifBoss.tbBoss;
        string message = $"Thông báo boss [{NotifBoss.tbBoss}]";
        GameScr.info1.addInfo(message, 0);
    }

    // Phương thức FocusCharBoss() chuyển sự chú ý của nhân vật chính tới boss
    [ChatCommand("fcb")]
    public static void FocusCharBoss()
    {
        for (int i = 0; i < GameScr.vCharInMap.size(); i++)
        {
            Char @char = (Char)GameScr.vCharInMap.elementAt(i);
            if (@char.charID < 0 && @char.cTypePk == 5)
            {
                Char.myCharz().charFocus.charID = @char.charID;
                break;
            }
        }
    }
    // Phương thức ChuyenKu() chuyển khu vực của nhân vật chính
    [ChatCommand("k")]
    public static void ChuyenKu(int khu) => Service.gI().requestChangeZone(khu, -1);

    // Phương thức Gohomsp() chuyển về nhà và mở trang bản đồ
    [ChatCommand("gohomsp")]
    public static void Gohomsp()
    {
        Helper.useItem(194, 0);
        Service.gI().requestMapSelect(0);
    }

    // Phương thức Dokhu() bật hoặc tắt chế độ đỏ khu
    [ChatCommand("dokhu")]
    public static void Dokhu() => StartDokhu(0);

    [ChatCommand("dokhu")]
    public static void Dokhu(int start) => StartDokhu(start);

    // Phương thức StartDokhu() bắt đầu chế độ đỏ khu
    private static void StartDokhu(int start)
    {
        NotifBoss.dokhuBoss = !NotifBoss.dokhuBoss;
        new Thread(() =>
        {
            int i = start;
            while (NotifBoss.dokhuBoss)
            {
                ChuyenKu(i++);
                Thread.Sleep(1340);
            }
        }).Start();
    }

    // Phương thức UseItem() sử dụng một số item cụ thể
    [ChatCommand("anitem")]
    public static void UseItem()
    {
        GameScr.info1.addInfo($"Đã ăn item", 0);
        List<int> idItems = new List<int>() { 381, 1099 , 1101, 384 , 531, 1100 , 382 };
        foreach (int idItem in idItems)
        {
            Helper.useItem(idItem, 0);
        }
    }

    // Phương thức Upcskb() bật hoặc tắt tính năng tự động nâng cấp cơ sở kiến thức
    [ChatCommand("upcskb")]
    public static void Upcskb()
    {
        AutoCSKB.gI.Toggle();
        TanSat.Interval = !TanSat.Interval;

        string message = AutoCSKB.gI.IsActing ? "Đang bật Upcskb" : "Đang tắt Upcskb";
        GameScr.info1.addInfo(message, 0);
    }

    // Phương thức ToggleTanSat() bật hoặc tắt tính năng tấn sát
    [ChatCommand("tansat", "ts")]
    public static void ToggleTanSat() => TanSat.Interval = !TanSat.Interval;

    // Phương thức Test() được sử dụng cho mục đích kiểm tra
    [ChatCommand("t")]
    public static void Test() 
    {
       
    }
    [ChatCommand("t")]
    public static void Test(int p) 
    {
        
    }

    // Phương thức AddHotkeys() thêm các phím tắt vào trò chơi
    public static void AddHotkeys()
    {
        // Nếu phím nhấn là phím "/", bắt đầu chat
        if (GameCanvas.keyAsciiPress == '/')
        {
            ChatTextField.gI().startChat('/', GameScr.gI(), string.Empty);
            return;
        }

        // Tạo một bản đồ ánh xạ từ ký tự phím tắt sang lệnh chat
        Dictionary<char, string> hotkeyMap = new Dictionary<char, string>
        {
            {'a', "/ak"},
            {'â', "/ak"},
            {'l', "/anitem"},
            {'c', "/dungcsdb"},
            {'b', "/bongtai"},
            {'t', "/fcb"},
            {'h', "/gohomsp"},
        };

        // Nếu phím nhấn có trong bản đồ ánh xạ, gửi lệnh chat tương ứng
        if (hotkeyMap.TryGetValue((char)GameCanvas.keyAsciiPress, out string chat))
        {
            GameEvents.OnSendChat(chat);
        }
        // Nếu phím nhấn là phím 'd' hoặc 'đ', bật hoặc tắt tính năng đập đồ
        else if (GameCanvas.keyAsciiPress == 'd' || GameCanvas.keyAsciiPress == 'đ')
        {
            GameDataStorage.dapdo = !GameDataStorage.dapdo;
            GameScr.info1.addInfo($"Đập đồ {GameDataStorage.dapdo}", 0);
        }
        GameCanvas.keyAsciiPress = 0;
    }
}
