using System;
using System.Threading;
public class linhtinh
{
    /// <summary>
    /// hàm nay dung để bán hoặc mua item [0 = sử dụng] [1 = bán]
    /// </summary> 
    public static void useItem(int IDitem, int select)
    {
        new Thread(() =>
        {
            int index = 0;
            while (index < global::Char.myCharz().arrItemBag.Length)
            {
                if (global::Char.myCharz().arrItemBag[index].template.id == IDitem)
                {
                    if (select == 0)
                    {
                        try
                        {
                            Service.gI().useItem(0, 1, (sbyte)index, -1);
                        }
                        catch (Exception) { }
                        
                    }
                    if (select == 1)
                    {
                        try
                        {
                            Service.gI().saleItem((sbyte)1, 1, (short)index);
                        }
                        catch (Exception) { }
                    }
                    break;
                }
                index++;
            }
        }).Start();
    }
    public static void dapdo()
    {
        for (int i = 0; i < GameCanvas.panel.vItemCombine.size(); i++)
        {
            if (GameCanvas.panel.vItemCombine.elementAt(i) != null && GameDataStorage.dapdo)
            {
                Service.gI().combine(1, GameCanvas.panel.vItemCombine);
                GameCanvas.gI().keyPressedz(-5);
            }
        }
    }
}
public class boss
{
    public string tenboss;
    public string khuvuc;
    public int idmap;
    public DateTime timeboss;
    public boss(string a)
    {
        a = a.Replace("boss ", "");
        a = a.Replace(" vừa xuất hiện tại ", "|");
        a = a.Replace("khu vực ", "|");
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
        if (GameDataStorage.tbBoss)
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
