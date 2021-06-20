using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Deadblock.Generic;
using Logic;
using Tools;
using Deadmind.Engine;

namespace Deadblock.Engine
{
    public class World : DeliveredGameSlot
    {
        private Dictionary<char, SpriteBlock> mySpriteMap;
        private char[][][] myMapSequence;

        private static string[] MapSequenceLayerPaths = new string[] {
            @"./Content/Levels/TheMain/ground.txt",
            @"./Content/Levels/TheMain/interactable.txt"
        };

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
            WorkerTexture[] envTextures = gameInstance.GameContents.GetTexturesByPrefix("env");

            foreach (var spec in envTextures)
            {
                mySpriteMap[spec.ID] = new SpriteBlock(gameInstance, spec.Name);
            }
        }

        /// <summary>
        /// Loads map sequence from specified
        /// file.
        /// </summary>
        private void LoadMapSequence ()
        {
            myMapSequence = FileUtils.ReadAs3DSequence(MapSequenceLayerPaths);
        }

        /// <summary>
        /// Renders level on the screen
        /// using reference layer files.
        /// </summary>
        public void RenderMap ()
        {
            var blockSize = GameGlobals.SCREEN_BLOCK_SIZE;

            //////////////////////////////

            Action<int, int, int> renderPosition = (int layerIndex, int x, int y) => {
                char spriteId = myMapSequence[layerIndex][y][x];

                // Void should be ignored
                if(spriteId == '0') return;

                // DEV should be notified about a potential bug
                if(!mySpriteMap.ContainsKey(spriteId))
                {
                    throw new AggregateException($"Found unexpected sprite id in the level layer reference file. Position: {x}:{y}; Value: {spriteId}. Please contact DEV.");
                }

                var sprite = mySpriteMap[spriteId];

                // Centered to the block
                var position = new Vector2(blockSize * x, blockSize * y);
                sprite.Draw(position);
            };

            //////////////////////////////

            for(var ml = 0; ml < myMapSequence.Length; ++ml)
                for(var my = 0; my < myMapSequence[0].Length; ++my)
                    for (var mx = 0; mx < myMapSequence[0][0].Length; mx++)
                        renderPosition(ml, mx, my);
        }
    }
}
