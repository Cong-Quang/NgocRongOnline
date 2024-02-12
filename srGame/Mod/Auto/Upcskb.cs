using System.Threading;

namespace Mod.Auto
{
    public class AutoCSKB : ThreadActionUpdate<AutoCSKB>
    {
        public override int Interval => 1800000;

        protected override void update()
        {
            linhtinh.useItem(379, 0);
            if (AutoAttack.gI.isActing == false)
            {
                AutoAttack.gI.toggle();
            }
            for (int i = 0; i < 250; i++)
            {
                linhtinh.useItem(380, 0);
                Thread.Sleep(100);
            }
        }
    }
}