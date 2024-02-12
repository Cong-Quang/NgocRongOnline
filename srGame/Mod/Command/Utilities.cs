using Mod.Auto;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using UnityEngine;
public class Utilities
{
    [ChatCommand("tdc")]
    [ChatCommand("cspeed")]
    [ChatCommand("s")]
    public static void editSpeedRun(int speed)
    {
        Char.myCharz().cspeed = speed;
        GameScr.info1.addInfo("Tốc độ chạy: " + speed, 0);
    }
    [ChatCommand("cheat")]
    [ChatCommand("chs")]
    public static void Cheat(float speed)
    {
        Time.timeScale = speed;
        GameScr.info1.addInfo("Tốc độ game: " + speed, 0);
    }
    /// <summary>
    /// Sử dụng skill Trị thương của namec vào bản thân
    /// </summary>
    [ChatCommand("hsme")]
    public static void buffMe()
    {
        sbyte idSkill = Char.myCharz().myskill.template.id;

        SkillTemplate skillTemplate = new();
        skillTemplate.id = 7;
        Skill skill = Char.myCharz().getSkill(skillTemplate);

        Service.gI().selectSkill(skillTemplate.id);

        MyVector vMe = new();
        vMe.addElement(Char.myCharz());

        Service.gI().sendPlayerAttack(new MyVector(), vMe, -1);

        skill.lastTimeUseThisSkill = mSystem.currentTimeMillis();
        Service.gI().selectSkill(idSkill);  
    }
    public static void AddKeyMap(Hashtable h)
    {
        h.Add(KeyCode.Slash, 47);
    }
   /* [ChatCommand("move")]
    public static void movemap()
    {
        Char.myCharz().cx = 50;
        Char.myCharz().cy = 50;
        Service.gI().charMove();
    }*/
    [ChatCommand("ak")]
    public static void toggleAutoAttack()
    {
        AutoAttack.gI.toggle();

        if (AutoAttack.gI.IsActing)
            GameScr.info1.addInfo("Đang tự tấn công", 0);
        else
            GameScr.info1.addInfo("Đã tắt tự tấn công", 0);
    }
    [ChatCommand("bongtai")]
    public static void bongtai()
    {
        linhtinh.useItem(454,0);
    }
    [ChatCommand("dungcsdb")]
    public static void dungcsdb()
    {               
        linhtinh.useItem(194, 0);
    }
    [ChatCommand("tbb")]
    public static void thongbaoboss()
    {
        GameDataStorage.tbBoss = !GameDataStorage.tbBoss;
        GameScr.info1.addInfo($"Thông báo boss [{GameDataStorage.tbBoss}]",0);
    }
    [ChatCommand("fcb")]
    public static void fcboss()
    {
        for (int i = 0; i < GameScr.vCharInMap.size(); i++)
        {
            global::Char @char = (Char)GameScr.vCharInMap.elementAt(i);
            if (@char.charID < 0 && @char.cTypePk == 5)
            {
                global::Char.myCharz().charFocus.charID = @char.charID;
            }
        }
    }
    [ChatCommand("k")]
    public static void chuyenKu(int khu)
    {
        Service.gI().requestChangeZone(khu,-1);
    }
    [ChatCommand("gohomsp")]
    public static void gohomsp()
    {
        linhtinh.useItem(194, 0);
        Service.gI().requestMapSelect(0);
    }

    [ChatCommand("dokhu")]
    public static void dokhu()
    {
        GameDataStorage.dokhuBoss = !GameDataStorage.dokhuBoss;
        new Thread(() =>
        {
            int i = 0;
            while (GameDataStorage.dokhuBoss)
            {
                chuyenKu(i++);
                Thread.Sleep(1340);
            }
        }).Start();
    }
    [ChatCommand("dokhu")]
    public static void dokhu(int start)
    {
        GameDataStorage.dokhuBoss = !GameDataStorage.dokhuBoss;
        new Thread(() =>
        {
            int i = start;
            while (GameDataStorage.dokhuBoss)
            {
                chuyenKu(i++);
                Thread.Sleep(1340);
            }
        }).Start();
    }
    [ChatCommand("anitem")]
    public static void UseItem()
    {
        GameScr.info1.addInfo($"Đã ăn item", 0);
        linhtinh.useItem(381, 0);
        linhtinh.useItem(1099, 0);
        linhtinh.useItem(1101, 0);
        linhtinh.useItem(384, 0);
        linhtinh.useItem(531, 0);
        linhtinh.useItem(1100, 0);
        linhtinh.useItem(382, 0);
    }
    [ChatCommand("upcskb")]
    public static void Upcskb()
    {
        AutoCSKB.gI.toggle();

        if (AutoCSKB.gI.IsActing)
            GameScr.info1.addInfo("Đang tự tấn công", 0);
        else
            GameScr.info1.addInfo("Đã tắt tự tấn công", 0);
    }
    [ChatCommand("acn")]
    public static void acn()
    {
        atcn.gI.toggle();

        if (atcn.gI.IsActing)
            GameScr.info1.addInfo("Đang tự tấn công", 0);
        else
            GameScr.info1.addInfo("Đã tắt tự tấn công", 0);
    }

    public static void AddHotkeys()
    {
        if (GameCanvas.keyAsciiPress == '/')
        {
            ChatTextField.gI().startChat('/', GameScr.gI(), string.Empty);
            return;
        }

        // Tạo một từ điển ánh xạ từ ký tự vào chuỗi tương ứng
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
        // Kiểm tra xem ký tự có tồn tại trong từ điển không
        if (hotkeyMap.TryGetValue((char)GameCanvas.keyAsciiPress, out string chat))
        {
            GameEvents.onSendChat(chat);
        }
        else if (GameCanvas.keyAsciiPress == 'd' || GameCanvas.keyAsciiPress == 'đ')
        {
            GameDataStorage.dapdo = !GameDataStorage.dapdo;
            GameScr.info1.addInfo($"Đập đồ {GameDataStorage.dapdo}", 0);
        }
        GameCanvas.keyAsciiPress = 0;
    }

}