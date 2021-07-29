using Deadblock.Engine;
using Deadblock.Generic;
using Deadblock.Extensions;
using Microsoft.Xna.Framework;

namespace Deadblock.GUI
{
    public abstract class GUIMetric : DeliveredGameSlot, Logic.IDrawable
    {
        private Vector2 myPosition;
        private WorkerTexture myIconTexture;
        private FontDrawer myFontDrawer;
        protected string myCurrentValue;

        // Result width of the icon.
        private readonly static int IconSize = 40;
        private readonly static int IconValueMargin = 20;

        public GUIMetric(GameProcess aGame, Vector2 aPosition, string anIconKey) : base(aGame)
        {
            myPosition = aPosition;
            myCurrentValue = "";
            myIconTexture = gameInstance.GameContents.GetTexture(anIconKey);

            myFontDrawer = new FontDrawer(aGame);
        }

        /// <summary>
        /// Draws loaded icon on the canvas.
        /// </summary>
        private void DrawIcon()
        {
            var tempIcon = myIconTexture.GetDefaultTexture();

            // Makes icon fit the width.
            var tempScale = new Vector2(IconSize / (float)tempIcon.Width, 1);

            // Makes icon center-aligned to the label.
            var tempPosition = myPosition - Vector2.UnitY * tempIcon.Height / 2;

            gameInstance.SpriteBatch.Draw(tempIcon, myPosition, Color.White, scale: tempScale);
        }

        private void DrawValue()
        {
            var tempFont = myFontDrawer.PullFont("Arial");
            var tempPosition = myPosition + Vector2.UnitX * (IconSize + IconValueMargin);

            gameInstance.SpriteBatch.DrawString(tempFont, myCurrentValue, tempPosition, Color.White);
        }

        /// <summary>
        /// Renders metric on the saved position.
        /// Base Method renders icon and value label.
        /// </summary>
        virtual public void Draw()
        {
            DrawValue();
            DrawIcon();
        }

        /// <summary>
        /// Updates metric value.
        /// </summary>
        virtual public void Update()
        { }
    }
}
