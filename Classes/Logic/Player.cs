using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Deadblock.Tools;

namespace Deadblock.Logic
{
    public class Player : RototableEntity
    {
        public PlayerBag Bag { get; private set; }

        public Player(GameProcess aGame) : base(aGame, "ent/player", 100)
        {
            PlaceEntity();
            ConnectInput();

            SetSpeed(3);
            SetStrength(10);
            SetAttackRange(100);
            SetAttackSpeed(500);

            Bag = new PlayerBag(gameInstance);
        }

        /// <summary>
        /// Places player on
        /// the default position in the world.
        /// </summary>
        protected void PlaceEntity()
        {
            var tempScreenSize = NativeUtils.GetScreenCenterPosition(gameInstance);
            var tempDimensions = Texture.GetDimensions();

            var tempX = tempScreenSize.X - tempDimensions.X / 2;
            var tempY = tempScreenSize.Y - tempDimensions.Y / 2;

            var tempNextPosition = new Vector2(tempX, tempY);
            SetPosition(tempNextPosition);
        }

        /// <summary>
        /// Interacts with all blocks
        /// that are in front of the player.
        /// </summary>
        protected void InteractWithEnvironment()
        {
            var currentPosition = Position;
            var forwardPosition = Position + Direction * GameGlobals.SCREEN_BLOCK_SIZE;

            var currentBlocks = gameInstance.World.GetBlocksOnPosition(currentPosition);
            var forwardBlocks = gameInstance.World.GetBlocksOnPosition(forwardPosition);

            var blocks = currentBlocks.Union(forwardBlocks).ToArray();

            foreach (var block in blocks)
            {
                if (!(block is InteractableDynamicBlock)) continue;
                InteractableDynamicBlock interactableBlock = (InteractableDynamicBlock)block;
                interactableBlock.InteractWith(this);
            }
        }

        /// <summary>
        /// Interacts with nearby entities.
        /// </summary>
        /// <returns>
        /// Returns false if there are no
        /// other entities in the interaction range.
        /// </returns>
        protected bool InteractWithEntities()
        {
            var nearestMonster = gameInstance.World.GetNearestMonster(Position, AttackRange);
            if (nearestMonster == null) return false;

            AttackEntity(nearestMonster);
            return true;
        }

        /// <summary>
        /// Searches for the nearby entities,
        /// to interact with them.
        /// If there are no other entities,
        /// tries to interact with nearby environment,
        /// such as block and block-like entities.
        /// </summary>
        protected void InteractWithWorld()
        {
            if (!InteractWithEntities())
                InteractWithEnvironment();
        }

        /// <summary>
        /// Assigns listeners
        /// to the input handler to
        /// give user the ability
        /// to control the player.
        /// </summary>
        protected void ConnectInput()
        {
            Action<Vector2> Move = (direction) => MoveEntity(direction);

            //////////////////////

            gameInstance.InputSystem.OnMoveUp.Subscribe((bool isActive) => Move(new Vector2(0, -1)));

            gameInstance.InputSystem.OnMoveRight.Subscribe((bool isActive) => Move(new Vector2(1, 0)));

            gameInstance.InputSystem.OnMoveDown.Subscribe((bool isActive) => Move(new Vector2(0, 1)));

            gameInstance.InputSystem.OnMoveLeft.Subscribe((bool isActive) => Move(new Vector2(-1, 0)));

            //////////////////////

            gameInstance.InputSystem.OnRegularUse.Subscribe((bool isActive) => InteractWithWorld());
        }
    }
}
