using Deadblock.Engine;
using Deadblock.Generic;
using Microsoft.Xna.Framework;

namespace Deadblock.Logic
{
    public class WaterStream : InteractableDynamicBlock
    {
        private AnimatedSpriteBlock myWaterSprite;

        public WaterStream(GameProcess aGame, string aMainTextureKey) : base(aGame, aMainTextureKey)
        {
            myWaterSprite = new AnimatedSpriteBlock(gameInstance, "env/water", 50);
            HasCollider = true;
        }

        override public void Render(Vector2 aPosition, bool isRelative = true)
        {
            base.Render(aPosition, isRelative);
            myWaterSprite.Render(aPosition, false);
        }

        override public void InteractWith(Entity anEntity)
        {
            if (anEntity is Player aPlayer)
            {
                aPlayer.Bag.DrinkWater();
            }

            base.InteractWith(anEntity);
        }
    }
}
