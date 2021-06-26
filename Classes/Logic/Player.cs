using Microsoft.Xna.Framework;
using Deadblock.Tools;

namespace Deadblock.Logic
{
    public class Player : DrawableEntity
    {
        public Player (GameProcess aGame) : base(aGame, "ent/player", 100)
        {
            PlaceEntity();
            ConnectInput();

            SetSpeed(3);
        }

        /// <summary>
        /// Places player on
        /// the default position in the world.
        /// </summary>
        protected void PlaceEntity ()
        {
            var tempScreenSize = NativeUtils.GetScreenCenterPosition(gameInstance);
            var tempDimensions = Texture.GetDimensions();

            var tempX = tempScreenSize.X - tempDimensions.X / 2;
            var tempY = tempScreenSize.Y - tempDimensions.Y / 2;

            var tempNextPosition = new Vector2(tempX, tempY);
            SetPosition(tempNextPosition);
        }

        /// <summary>
        /// Assigns listeners
        /// to the input handler to
        /// give user the ability
        /// to control the player.
        /// </summary>
        protected void ConnectInput ()
        {
            gameInstance.InputHandler.OnMoveUp.Subscribe((bool isActive) => {
                MoveEntity(new Vector2(0, -1));
            });

            gameInstance.InputHandler.OnMoveRight.Subscribe((bool isActive) => {
                MoveEntity(new Vector2(1, 0));
            });

            gameInstance.InputHandler.OnMoveDown.Subscribe((bool isActive) => {
                MoveEntity(new Vector2(0, 1));
            });

            gameInstance.InputHandler.OnMoveLeft.Subscribe((bool isActive) => {
                MoveEntity(new Vector2(-1, 0));
            });

        }

        public override void Update()
        { }
    }
}
