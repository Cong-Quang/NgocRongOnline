namespace AssemblyCSharp.mod.functions
{
    public static class usedKey
    {
        [KeyMethod("csdb")]
        public static void csdb()
        {
            int index = 0;
            while (index < global::Char.myCharz().arrItemBag.Length)
            {
                if (global::Char.myCharz().arrItemBag[index].template.id == 194)
                {
                    Service.gI().useItem(0, 1, (sbyte)index, -1);
                    break;
                }
                index++;
            }
        }
        [KeyMethod("dapdo")]
        public static void dapdox()
        {
            linhtinh.gI().isDapdo = !linhtinh.gI().isDapdo;
            GameScr.info1.addInfo($"đập đồ {linhtinh.gI().isDapdo}",0);
            GameCanvas.keyAsciiPress = 0;
        }
        [KeyMethod("sd")]
        public static void congSD()
        {
            for (int i = 0; i < 100; i++)
            {
                Service.gI().upPotential(1, 10);
            }
            GameCanvas.keyAsciiPress = 0;
        }
        [KeyMethod("menu")]
        public static void menu() // vẽ menu
        {
            Func.Gi().menuSystem();
        }
        [KeyMethod("exitall")]
        public static void exitall()
        {
            linhtinh.gI().isUpcskb = false;
            linhtinh.ismuado = false;
            linhtinh.gI().isDapdo = false;
            GameCanvas.keyAsciiPress = 0;
        }
    }
}
