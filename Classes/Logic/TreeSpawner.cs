using Deadblock.Generic;
using Deadblock.Engine;
using Deadblock.Tools;
using Microsoft.Xna.Framework;

namespace Deadblock.Logic
{
    public class TreeSpawner : InteractableDynamicBlock
    {
        private float mySpawnQuota;
        private bool myHasPlant;
        private SpriteBlock myPlantSprite;

        public TreeSpawner(GameProcess aGame, string aMainTextureKey) : base(aGame, aMainTextureKey)
        {
            mySpawnQuota = NativeUtils.RandomizeValue(10f, 20f);
            myPlantSprite = new SpriteBlock(gameInstance, "env/green-tree");
        }

        override public void Render(Vector2 aPosition, bool isRelative = true)
        {
            base.Render(aPosition, false);

            if (myHasPlant)
            {
                myPlantSprite.Render(aPosition, true);
            }
        }

        override public void Update()
        {
            base.Update();

            if (mySpawnQuota > 0)
            {
                mySpawnQuota -= .1f;
            }

            if (mySpawnQuota <= 0 && !myHasPlant)
            {
                SpawnPlant();
            }
        }

        override public void InteractWith(Entity anEntity)
        {
            if (Chop() && anEntity is Player aPlayer)
            {
                aPlayer.Bag.CollectTree();
            }


            base.InteractWith(anEntity);
        }

        /// <summary>
        /// Spawns plant
        /// and enables collider for the block.
        /// </summary>
        private void SpawnPlant()
        {
            myHasPlant = true;
            HasCollider = true;
        }

        /// <summary>
        /// Chops the tree and
        /// resets the quota value.
        /// </summary>
        /// <returns>
        /// Boolean that represents
        /// if the tree has been chopped.
        /// </returns>
        private bool Chop()
        {
            if (!myHasPlant) return false;

            mySpawnQuota = NativeUtils.RandomizeValue(5f, 15f);
            myHasPlant = false;
            HasCollider = false;

            return true;
        }
    }
}
