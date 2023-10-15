using System;
using System.Threading;
using UnityEngine;
namespace AssemblyCSharp.mod.functions
{
    public static class usedChat
    {
        [ChatMethod("s","speed")]
        public static void speedGame(string s)
        {
            try
            {
                Time.timeScale = int.Parse(s);
                GameScr.info1.addInfo($"tốc dộ game ->{s}", 0);
            }
            catch { }
        }
        [ChatMethod("s","speed")]
        public static void speedGame()
        {
            Time.timeScale = 10;
            GameScr.info1.addInfo($"tốc dộ game  10", 0);
        }
        [ChatMethod("g", "give")]
        public static void give(string id, string choise,string quantity)
        {
            new Thread(() =>
            {
                for (int i = 0; i < int.Parse(quantity); i++)
                {
                    Service.gI().openMenu(int.Parse(id));
                    Service.gI().confirmMenu(short.Parse(id), sbyte.Parse(choise));
                    Thread.Sleep(50);
                }
            }).Start();
        }
        [ChatMethod("m", "mua")]
        public static void muaitem(string sl)
        {
            linhtinh.solanmua = int.Parse(sl);
            GameScr.info1.addInfo($"Số lượng mới được thiết lập là {sl}", 0);
        }
        public static void muaitem()
        {
            linhtinh.solanmua = 1000;
            GameScr.info1.addInfo($"Số lượng mua mặc định  {1000}", 0);
        }
        [ChatMethod("upcskb", "ucskb")]
        public static void upcskb()
        {
            linhtinh.gI().isUpcskb = true;
            new Thread(() =>
            {
                linhtinh.gI().useItem(521);
                while (linhtinh.gI().isUpcskb)
                {
                    linhtinh.gI().useItem(379);
                    for (int i = 0; i < 99; i++)
                    {
                        linhtinh.gI().useItem(380);
                    }
                    Thread.Sleep(TimeSpan.FromMinutes(30));
                }
            }).Start();
        }
        [ChatMethod("dokhu","dk")]
        public static void dokhu()
        {
            new Thread(() =>
            {
                Func.Gi().checkboos = false;
                for (int i = 0; i < 20; i++)
                {
                    if (Func.Gi().checkboos)
                    {
                        GameScr.info1.addInfo($"Xong", 0);
                        break;
                    }
                    Service.gI().requestChangeZone(i, -1);
                    Thread.Sleep(TimeSpan.FromSeconds(11));
                    GameScr.info1.addInfo($"Đang ở khu {i+1}", 0);
                }
            }).Start();
           // Service.gI().upPotential(0,10); cộng tìm năng
        }
    }
}