using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Deadblock.Generic;
using Logic;
using Tools;

namespace Deadblock.Engine
{
    public class World : DeliveredGameSlot
    {
        private Dictionary<char, SpriteBlock> mySpriteMap;
        private char[][] myMapSequence;

        public World (GameProcess aGame) : base(aGame)
        {
            RegisterTextures(aGame);
            LoadMapSequence();
        }

        /// <summary>
        /// Loads texture to the spritemap
        /// with the specified id.
        /// </summary>
        /// <param name="aGame">
        /// Targeted game process.
        /// </param>
        /// <param name="anId">
        /// Id of the texture used in a
        /// level layer.
        /// </param>
        /// <param name="aKey">
        /// Key of the texture,
        /// that was already registered
        /// in targeted ContentWorker.
        /// </param>
        private void RegisterTexture (GameProcess aGame, char anId, string aKey)
        {
            mySpriteMap[anId] = new SpriteBlock(aGame, aKey);
        }

        /// <summary>
        /// Creates and fills spritemap with pre-defined
        /// textures, which are represented as spriteblocks.
        /// </summary>
        /// <param name="aGame">
        /// Targeted game process.
        /// </param>
        private void RegisterTextures (GameProcess aGame)
        {
            mySpriteMap = new Dictionary<char, SpriteBlock>();

            RegisterTexture(aGame, '1', "env/main-grass");
            RegisterTexture(aGame, '2', "env/green-tree");
            RegisterTexture(aGame, '3', "env/dark-tree");
            RegisterTexture(aGame, '4', "env/regular-flower");
            RegisterTexture(aGame, '5', "env/vertical-gate");
            RegisterTexture(aGame, '6', "env/horizontal-gate");
            RegisterTexture(aGame, '7', "env/stack-sticks");
        }

        /// <summary>
        /// Loads map sequence from specified
        /// file.
        /// </summary>
        private void LoadMapSequence ()
        {
            // TODO: 3D - multilayers
            myMapSequence = FileUtils.ReadAs2DSequence(@"./Content/Levels/TheMain.txt");
        }

        /// <summary>
        /// Renders level on the screen
        /// using reference layer files.
        /// </summary>
        public void RenderMap ()
        {
            var blockSize = GameGlobals.SCREEN_BLOCK_SIZE;

            //////////////////////////////

            Action<int, int> renderPosition = (int x, int y) => {
                var spriteId = myMapSequence[y][x];
                if(!mySpriteMap.ContainsKey(spriteId))
                {
                    throw new AggregateException($"Found unexpected sprite id in the level layer reference file. Position: {x}:{y}; Value: {spriteId}");
                }

                var sprite = mySpriteMap[spriteId];

                // Centered to the block
                var position = new Vector2(blockSize * x, blockSize * y);
                sprite.Draw(position);
            };

            //////////////////////////////

            for(var my = 0; my < myMapSequence.Length; ++my)
                for (var mx = 0; mx < myMapSequence[0].Length; mx++)
                    renderPosition(mx, my);
        }
    }
}
