using Microsoft.Xna.Framework;
using Deadblock.Tools;

namespace Deadblock.Logic
{
    public class Player : DrawableEntity
    {
        public Player (GameProcess aGame) : base(aGame, "ent/player", 100)
        {
            PlaceEntity();
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

        public override void Update()
        {

        }
    }
}
