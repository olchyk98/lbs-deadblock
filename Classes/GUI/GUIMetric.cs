using Deadblock.Engine;
using Deadblock.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Deadblock.GUI
{
    public abstract class GUIMetric : DeliveredGameSlot, Logic.IDrawable
    {
        private Vector2 myPosition;
        protected string myCurrentValue;
        private WorkerTexture myIconTexture;

        public GUIMetric (GameProcess aGame, Vector2 aPosition, string anIconKey) : base(aGame)
        {
            myPosition = aPosition;
            myCurrentValue = "";
            myIconTexture = gameInstance.GameContents.GetTexture(anIconKey);
        }

        /// <summary>
        /// Draws loaded icon on the canvas.
        /// </summary>
        /// <returns>
        /// Returns rendered 2D texture.
        /// </returns>
        private Texture2D DrawIcon ()
        {
            var tempIcon = myIconTexture.GetDefaultTexture();
            gameInstance.SpriteBatch.Draw(tempIcon, myPosition, Color.White);

            return tempIcon;
        }

        /// <summary>
        /// Renders metric on the saved position.
        ///
        /// Base Method renders icon and value label.
        /// </summary>
        public virtual void Draw()
        {
            var tempIcon = DrawIcon();
            var tempIconWidth = tempIcon.Width;
        }

        /// <summary>
        /// Updates metric value.
        /// </summary>
        public virtual void Update ()
        {  }
    }
}
