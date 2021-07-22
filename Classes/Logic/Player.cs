using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Deadblock.Tools;

namespace Deadblock.Logic
{
    public class Player : DrawableEntity
    {
        public PlayerBag Bag { get; private set; }

        public Player(GameProcess aGame) : base(aGame, "ent/player", 100)
        {
            PlaceEntity();
            ConnectInput();

            SetSpeed(3);

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
        /// Sets player sprite according
        /// to the movement direction.
        /// </summary>
        protected void UpdateSprite()
        {
            if (Direction.X == 1)
            {
                SetSpriteVariant("Right");
                return;
            }

            if (Direction.X == -1)
            {
                SetSpriteVariant("Left");
                return;
            }

            if (Direction.Y == 1)
            {
                SetSpriteVariant("Down");
                return;
            }

            if (Direction.Y == -1)
            {
                SetSpriteVariant("Up");
                return;
            }
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
        /// Assigns listeners
        /// to the input handler to
        /// give user the ability
        /// to control the player.
        /// </summary>
        protected void ConnectInput()
        {
            Action<Vector2> Move = (direction) =>
            {
                MoveEntity(direction);
                UpdateSprite();
            };

            gameInstance.InputHandler.OnMoveUp.Subscribe((bool isActive) =>
            {
                Move(new Vector2(0, -1));
            });

            gameInstance.InputHandler.OnMoveRight.Subscribe((bool isActive) =>
            {
                Move(new Vector2(1, 0));
            });

            gameInstance.InputHandler.OnMoveDown.Subscribe((bool isActive) =>
            {
                Move(new Vector2(0, 1));
            });

            gameInstance.InputHandler.OnMoveLeft.Subscribe((bool isActive) =>
            {
                Move(new Vector2(-1, 0));
            });

            gameInstance.InputHandler.OnRegularUse.Subscribe((bool isActive) =>
            {
                InteractWithEnvironment();
            });
        }

        public override void Update()
        { }
    }
}
