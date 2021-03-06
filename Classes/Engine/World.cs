using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Deadblock.Generic;
using Deadblock.Logic;
using Deadblock.Tools;
using Deadblock.Sessions;

namespace Deadblock.Engine
{
    public class World : DeliveredGameSlot, IDisposable
    {
        public Player MainPlayer { get; private set; }
        public MonsterSpawner MonsterSpawner { get; private set; }

        // NOTE: This thing, could potentially be
        // improved using a container with matrixPosition
        // member, but then it would be much harder
        // to handle layers.
        private ISpriteBlock[,,] myMap;
        private WorkerTexture[] myMapTextures;
        private List<DrawableEntity> myEntities;

        public UniversalEvent<GameEndScenario> OnEnd { get; private set; }

        private static string[] MapReferenceLayerPaths = new string[] {
            @"./Content/Levels/TheMain/ground.txt",
            @"./Content/Levels/TheMain/interactable.txt"
        };
        private bool myIsDisposing;

        public World(GameProcess aGame) : base(aGame)
        {
            MonsterSpawner = new MonsterSpawner(aGame);
            OnEnd = new UniversalEvent<GameEndScenario>();

            LoadMap();
            InstantiateEntities();
        }

        /// <summary>
        /// Sets block to a cell position.
        /// </summary>
        /// <param name="aBlock">
        /// Targeted block.
        /// Can be null if the position
        /// should be empty.
        /// </param>
        /// <param name="aMatrixPosition">
        /// Position in the matrix,
        /// which is represented as Vector3,
        /// where Z axis references layerIndex.
        /// </param>
        private void AssignBlockToMapCell(ISpriteBlock aBlock, Vector3 aMatrixPosition)
        {
            var Z = (int)aMatrixPosition.Z;
            var X = (int)aMatrixPosition.X;
            var Y = (int)aMatrixPosition.Y;
            myMap[Z, Y, X] = aBlock;
        }

        /// <summary>
        /// Loads texture to the spritemap
        /// with the specified id.
        /// </summary>
        /// <param name="aReferenceKey">
        /// Key of the texture,
        /// that was already registered
        /// in targeted ContentWorker.
        /// </param>
        /// <param name="aCellPosition">
        /// Position of the cell, represented
        /// as vector3, where Z axis corresponds
        /// to the layer index.
        /// </param>
        private void InstantiateMapCell(string aReferenceKey, Vector3 aCellPosition)
        {
            ISpriteBlock tempInstance = default;
            var tempSpec = GetEnvTexture(aReferenceKey);

            // Handle air
            if (aReferenceKey == "0")
            {
                AssignBlockToMapCell(null, aCellPosition);
                return;
            }

            if (tempSpec == null)
            {
                throw new AggregateException($"An invalid referenceKey detected: { aReferenceKey }. Looks like a logic problem. Contact DEV.");
            }

            // Handle special block
            if (tempSpec.ActiveInstanceName != null)
            {
                Type tempType = Type.GetType(tempSpec.ActiveInstanceName);

                if (tempType == null)
                {
                    throw new AggregateException($"An error in the schema file. Referenced an invalid active instance: { tempSpec.ActiveInstanceName }. Contact DEV.");
                }

                tempInstance = (ISpriteBlock)Activator.CreateInstance(
                        tempType,
                        gameInstance,
                        tempSpec.Name
                        );
            }
            else
            {
                // Handle regular sprite block
                tempInstance = new SpriteBlock(gameInstance, tempSpec.Name);
            }

            AssignBlockToMapCell(tempInstance, aCellPosition);
        }

        /// <summary>
        /// Returns registered env texture
        /// with the specified id.
        /// </summary>
        /// <param name="anId">
        /// Id of the targeted env texture.
        /// </param>
        /// <returns>
        /// Targeted env texture or null
        /// if it's not found.
        /// </returns>
        private WorkerTexture GetEnvTexture(string anId)
        {
            return myMapTextures
                .ToList()
                .Find((f) => f.ID == anId);
        }

        /// <summary>
        /// Creates and fills spritemap with pre-defined
        /// textures, which are represented as spriteblocks.
        /// </summary>
        private void LoadMap()
        {
            myMapTextures = gameInstance.GameContents.GetTexturesByPrefix("env");
            var tempMapReference = FileUtils.ReadAs3DSequence(MapReferenceLayerPaths);

            /////////////////

            myMap = new ISpriteBlock[tempMapReference.Length, tempMapReference[0].Length, tempMapReference[0][0].Length];

            /////////////////

            for (var ml = 0; ml < tempMapReference.Length; ++ml)
                for (var my = 0; my < tempMapReference[0].Length; ++my)
                    for (var mx = 0; mx < tempMapReference[0][0].Length; mx++)
                    {
                        var referenceKey = tempMapReference[ml][my][mx];
                        var cellPosition = new Vector3(mx, my, ml);

                        InstantiateMapCell(referenceKey, cellPosition);
                    }
        }

        /// <summary>
        /// Spawns player at
        /// the center of the map.
        ///
        /// Should be called
        /// once the environment is loaded.
        /// </summary>
        private void InstantiateEntities()
        {
            myEntities = new List<DrawableEntity>();

            ////////////////////////

            var player = new Player(gameInstance);

            ////////////////////////

            player.OnFinish.Subscribe(OnEnd.Invoke);

            ////////////////////////

            myEntities.Add(player);
            MainPlayer = player;
        }

        /// <summary>
        /// Renders level on the screen
        /// using reference layer files.
        /// </summary>
        private void RenderMap()
        {
            var blockSize = GameGlobals.SCREEN_BLOCK_SIZE;

            //////////////////////////////

            Action<int, int, int> renderPosition = (layerIndex, x, y) =>
            {
                var block = myMap[layerIndex, y, x];

                // Ignore air
                if (block == null) return;

                // Centered to the block
                var position = new Vector2(blockSize * x, blockSize * y);
                block.Render(position);
            };

            //////////////////////////////

            for (var ml = 0; ml < myMap.GetLength(0); ++ml)
                for (var my = 0; my < myMap.GetLength(1); ++my)
                    for (var mx = 0; mx < myMap.GetLength(2); ++mx)
                        renderPosition(ml, mx, my);
        }

        /// <summary>
        /// Runs draw on
        /// all available entities.
        /// </summary>
        private void RenderEntities()
        {
            foreach (var entity in myEntities)
            {
                entity.Draw();
            }
        }

        /// <summary>
        /// Runs update on
        /// all available entities.
        /// </summary>
        private void UpdateEntities()
        {
            foreach (var entity in myEntities)
            {
                entity.Update();
            }
        }

        /// <summary>
        /// May potentially spawn
        /// monster by calling the spawner.
        /// </summary>
        /// <returns>
        /// True, if a new
        /// monster was summoned.
        /// </returns>
        private bool SpawnMonster()
        {
            Monster tempNewMonster = MonsterSpawner.ProcessSpawnTick();
            if (tempNewMonster == null) return false;

            //////////////////

            tempNewMonster.OnDie.Subscribe(() => myEntities.Remove(tempNewMonster));

            //////////////////

            myEntities.Add(tempNewMonster);
            return true;
        }

        /// <summary>
        /// Renders entities and
        /// constructed environment.
        /// </summary>
        public void Draw()
        {
            RenderMap();
            RenderEntities();
        }

        /// <summary>
        /// Updates entities
        /// and constructed environment.
        /// </summary>
        public void Update()
        {
            if (myIsDisposing) return;

            SpawnMonster();
            UpdateEntities();
        }

        /// <summary>
        /// Going through internal storages
        /// and collects all active blocks
        /// (for example, SpriteBlocks and DynamicBlocks) that
        /// correspond to the targeted position.
        /// </summary>
        /// <param name="aPosition">
        /// The targeted position,
        /// </param>
        /// <returns>
        /// Array of blocks.
        ///
        /// An empty array,
        /// if the point is of the bounds.
        /// </returns>
        public ISpriteBlock[] GetBlocksOnPosition(Vector2 aPosition)
        {
            var tempBlockSize = GameGlobals.SCREEN_BLOCK_SIZE;
            var tempLayersCount = myMap.GetLength(0);
            var tempBlocks = new List<ISpriteBlock>();

            var tempMatrixX = (int)Math.Floor(aPosition.X / tempBlockSize);
            var tempMatrixY = (int)Math.Floor(aPosition.Y / tempBlockSize) + 1;

            //////////////////////////

            var isOutBounds = (
                    tempMatrixY < 0 || tempMatrixY > myMap.GetLength(1) - 1
                    || tempMatrixX < 0 || tempMatrixX > myMap.GetLength(2) - 1
                    );

            if (isOutBounds) return new ISpriteBlock[] { };

            //////////////////////////

            for (var layerIndex = 0; layerIndex < myMap.GetLength(0); ++layerIndex)
            {

                var targetBlock = myMap[layerIndex, tempMatrixY, tempMatrixX];

                // Ignore air
                if (targetBlock == null) continue;

                tempBlocks.Add(targetBlock);
            }

            //////////////////////////

            return tempBlocks.ToArray();
        }

        /// <summary>
        /// Returns nearest monsters
        /// to the specified position.
        /// </summary>
        /// <param name="aPosition">
        /// Origo position for the algorithm.
        /// </param>
        /// <param name="aRange">
        /// Detection range for the algorithm.
        /// </param>
        /// <returns>
        /// A monster that is positioned the
        /// neariest to the origo point.
        /// Will return null if there is not monster nearby.
        /// </returns>
        public Monster GetNearestMonster(Vector2 aPosition, int aRange)
        {
            Monster tempMonster = null;
            int tempNearestDistance = default;

            foreach (var entity in myEntities)
            {
                if (!(entity is Monster)) continue;
                var tempEntityDistance = (int)Vector2.Distance(entity.Position, aPosition);

                if (tempMonster == null
                        || tempEntityDistance < tempNearestDistance)
                {
                    tempMonster = entity as Monster;
                    tempNearestDistance = tempEntityDistance;
                }
            }

            return (tempNearestDistance <= aRange) ? tempMonster : null;
        }

        // Part of "Dispose Pattern" implementation
        protected virtual void Dispose(bool isDisposing)
        {
            if (!isDisposing) return;
            myIsDisposing = true;
        }

        // Part of "Dispose Pattern" implementation
        public void Dispose()
        {
            Dispose(isDisposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
