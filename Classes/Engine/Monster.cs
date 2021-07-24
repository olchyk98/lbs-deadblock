using Deadblock.Logic;

namespace Deadblock.Engine
{
    public abstract class Monster : DrawableEntity
    {
        public virtual string Name { get; }

        public Monster(GameProcess aGame, string aTextureKey, float someHealth) : base(aGame, aTextureKey, someHealth)
        { }
    }
}
