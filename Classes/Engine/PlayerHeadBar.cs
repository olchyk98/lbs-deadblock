using Deadblock.Logic;
using Microsoft.Xna.Framework;

namespace Deadblock.Engine
{
    public abstract class PlayerHeadBar : HeadBar
    {
        public Player TargetPlayer
        {
            get => (Player)TargetEntity;
        }

        public PlayerHeadBar(GameProcess aGame, Player aPlayer, int someHeight, Color aColor) : base(aGame, aPlayer, someHeight, aColor)
        { }
    }
}
