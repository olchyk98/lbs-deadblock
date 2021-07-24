namespace Deadblock.Engine
{
    public class Spider : Monster
    {
        public override string Name { get; } = "Spider";

        public Spider(GameProcess aGame) : base(aGame, "ent/spider", 25f)
        { }

        public override void Update()
        {
            return;
        }
    }
}
