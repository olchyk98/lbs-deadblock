using Microsoft.Xna.Framework;
using Deadblock.Tools;
using System;

namespace Deadblock.Logic
{
    public class Player : DrawableEntity
    {
        public Player(GameProcess aGame) : base(aGame, "ent/player", 100)
        {
            PlaceEntity();
            ConnectInput();

            SetSpeed(3);
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
            if (Rotation.X == 1)
            {
                SetSpriteVariant("Right");
                return;
            }

            if (Rotation.X == -1)
            {
                SetSpriteVariant("Left");
                return;
            }

            if (Rotation.Y == 1)
            {
                SetSpriteVariant("Down");
                return;
            }

            if (Rotation.Y == -1)
            {
                SetSpriteVariant("Up");
                return;
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

        }

        public override void Update()
        { }
    }
}
