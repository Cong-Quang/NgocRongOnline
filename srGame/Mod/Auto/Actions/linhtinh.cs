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