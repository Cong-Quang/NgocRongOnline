namespace Mod.Auto
{
    public class atcn : ThreadActionUpdate<atcn>
    {
        public override int Interval => 600000;

        protected override void update()
        {
            linhtinh.useItem(381, 0);
            linhtinh.useItem(1099, 0);
        }
    }
}