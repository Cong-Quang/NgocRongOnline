using System;
using System.Threading;

public class linhtinh
{
    public static bool checkUseItem = true;

    /// <summary>
    /// Hàm này được sử dụng để sử dụng hoặc bán item [0 = sử dụng] [1 = bán]
    /// </summary> 
    public static void useItem(int itemId, int select)
    {
        checkUseItem = true;
        ThreadPool.QueueUserWorkItem((state) =>
        {
            var charInstance = global::Char.myCharz(); // Lấy một lần để tránh gọi nhiều lần trong vòng lặp
            for (int index = 0; index < charInstance.arrItemBag.Length; index++)
            {
                var currentItem = charInstance.arrItemBag[index];
                if (currentItem.template.id == itemId)
                {
                    try
                    {
                        switch (select)
                        {
                            case 0:
                                Service.gI().useItem(0, 1, (sbyte)index, -1);
                                break;
                            case 1:
                                Service.gI().saleItem((sbyte)1, 1, (short)index);
                                break;
                            default:
                                break;
                        }
                    }
                    catch (Exception)
                    {
                        checkUseItem = false;
                    }
                    break;
                }
            }
        });
    }

    // Phương thức dapdo() thực hiện logic sử dụng kỹ năng "đập đồ"
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

    // Constructor của lớp boss, khởi tạo các thuộc tính
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
