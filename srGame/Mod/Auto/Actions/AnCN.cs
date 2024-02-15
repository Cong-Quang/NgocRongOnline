namespace Mod.Auto.Actions
{
    public class AnCN : ThreadActionUpdate<AnCN>
    {
        public override int Interval => 600000;//30 phút

        protected override void Update()
        {
            linhtinh.useItem(381, 0);
            linhtinh.useItem(1099, 0);
        }
    }
}