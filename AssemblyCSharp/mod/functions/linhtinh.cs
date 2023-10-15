using System;
using System.Threading;

namespace AssemblyCSharp.mod.functions
{
    public class linhtinh
    {
        public static linhtinh gI() 
        { 
            if (gi == null) { gi = new linhtinh(); }
            return gi; 
        }

        public void saleItem(int iditem)
        {
            new Thread(() =>
            {
                int index = 0;
                while (index < global::Char.myCharz().arrItemBag.Length)
                {
                    if (global::Char.myCharz().arrItemBag[index].template.id == iditem)
                    {
                        Service.gI().saleItem((sbyte)1, 1, (short)index);
                        break;
                    }
                    index++;
                }
            }).Start();
        }
        public void useItem(int item)
        {
            new Thread(() =>
            {
                int index = 0;
                while (index < global::Char.myCharz().arrItemBag.Length)
                {
                    if (global::Char.myCharz().arrItemBag[index].template.id == item)
                    {
                        Service.gI().useItem(0,1, (sbyte)index,-1);
                        break;
                    }
                    index++;
                }
            }).Start();
        }

        public void dapdox()
        {
            for (int i = 0; i < GameCanvas.panel.vItemCombine.size(); i++)
            {
                if (GameCanvas.panel.vItemCombine.elementAt(i) != null)//check trang đập đồ
                {
                    saleItem(457);
                    Service.gI().combine(1, GameCanvas.panel.vItemCombine); // trọn ô 2
                    GameCanvas.gI().keyPressedz(-5); // key f1
                }
            }
        }
        
        public void AktoMob()
        {
            try
            {
                MyVector myVector = new MyVector();
                myVector.addElement(Char.myCharz().mobFocus);
                Service.gI().sendPlayerAttack(myVector, new MyVector(), 1);
            }
            catch
            {

            }
        }
        public void AktoChar()
        {
            try
            {
                MyVector myVector = new MyVector();
                myVector.addElement(Char.myCharz().charFocus);
                Service.gI().sendPlayerAttack(new MyVector(), myVector, 2);
            }
            catch 
            {

            }
        }
        private static linhtinh gi;

        public bool isDapdo = false;
        public bool isUpcskb = false;
        public static long timeAK;
        public static int solanmua = 59999;
        public static bool ismuado = false;
    }
    public class Func
    {
        public static Func Gi()
        {
            if (gi == null) 
            { 
                gi = new Func();
            }
            return gi;
        }
        public void updateInfo(string info)
        {
            if (check_String(info, "Sư phụ ơi"))// buff đậu
            {
                GameScr.gI().doUseHP();
            }
        }
        public void updateGame()
        {

        }
        public void menuSystem()
        {
            MyVector m = new MyVector();
            m.addElement(new Command(Infos ? "tắt thông báo boss" : "bật thông báo boss", 20001));
            GameCanvas.menu.startAt(m, 1);
            return;
        }
        public void acctions(int acctions) // acction cho menu
        {
            if (acctions == 20001)
            {
                Infos = !Infos;
                
            }
        }
        public bool check_String(string t1, string t2)
        {
            int len1 = t1.Length;
            int len2 = t2.Length;
            for (int i = 0; i <= len1 - len2 ;i++)
            {
                int j;
                for (j = 0;j < len2; j++)
                {
                    if ((t1[i + j] == t2[j]))
                    {
                        break;
                    }
                }
                if (j == len2)
                {
                    return true;
                }
            }
            return false;
        }
        public bool checkboos;
        public static bool Infos;
        private static Func gi;

    }
    
    public class boss 
    {
        public string tenboss;
        public string khuvuc;
        public int idmap;
        public DateTime timeboss;
        public boss(string a)
        {
            a = a.Replace("boss ","");
            a = a.Replace(" vừa xuất hiện tại ","|");
            a = a.Replace("khu vực ","|");
            string[] array = a.Split('|');
            this.tenboss = array[0].Trim();
            this.khuvuc = array[1].Trim();
            this.idmap = MapId(this.khuvuc);
            this.timeboss = DateTime.Now;
        }
        public int MapId(string a)
        {
            for (int i = 0; i < TileMap.mapName.Length; i++)
            {
                if (TileMap.mapName[i].Equals(a))
                {
                    return i;
                }
            }
            return -1;
        }
        public void paintboss(mGraphics a, int b, int c, int d)
        {
            if (Func.Infos)
            {
                TimeSpan timespan = DateTime.Now.Subtract(this.timeboss);
                int num = (int)timespan.TotalSeconds;
                mFont mFont = mFont.tahoma_7b_red;
                if (TileMap.mapID == this.idmap)
                {
                    mFont = mFont.tahoma_7b_red;
                    for (int i = 0; i < GameScr.vCharInMap.size(); i++)
                    {
                        if (((global::Char)GameScr.vCharInMap.elementAt(i)).cName.Equals(this.tenboss))
                        {
                            mFont = mFont.tahoma_7b_blue;
                            break;
                        }
                    }
                }
                mFont.drawString(a, string.Concat(new object[]
                {
                this.tenboss," - ", this.khuvuc, " - ",
                (num < 60)? (num+"s") : (timespan.Minutes + "p")
                ," trước  <<<"
                }), b, c, d);
            }
        }
    }
}
