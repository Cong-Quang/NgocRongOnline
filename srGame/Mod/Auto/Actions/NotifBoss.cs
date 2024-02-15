using System;
public class NotifBoss
{
    public string tenboss;
    public string khuvuc;
    public int idmap;
    public DateTime timeboss;
    public static bool tbBoss = true;
    public static MyVector bBoss = new MyVector();
    public static bool dokhuBoss = false;

    // Constructor của lớp boss, khởi tạo các thuộc tính
    public NotifBoss(string a)
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

    // Phương thức trả về ID của map dựa trên tên khu vực
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

    // Phương thức vẽ thông tin về boss trên màn hình
    public void paintboss(mGraphics a, int b, int c, int d)
    {
        if (tbBoss)
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