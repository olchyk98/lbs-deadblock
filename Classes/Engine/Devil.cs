namespace Deadblock.Engine
{
    public sealed class Devil : Monster
    {
        public Devil(GameProcess aGame) : base(aGame, "ent/devil", 25f)
        {
            SetSpeed(2);
            SetStrength(6);
            SetAttackSpeed(1000);
            SetAttackRange(5);
        }

        override public void Update()
        {
            base.Update();
        }
    }
}
