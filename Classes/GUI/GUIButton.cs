using System;
using Microsoft.Xna.Framework;
using Deadblock.Tools;
using Microsoft.Xna.Framework.Input;
using Deadblock.Engine;
using Deadblock.Generic;

namespace Deadblock.GUI
{
    public class GUIButton : DeliveredGameSlot
    {
        private WorkerTexture myTexture;

        public Vector2 Position { get; private set; }
        public UniversalEvent OnClick { get; private set; }

        public GUIButton (GameProcess aGame,
                Vector2 aPosition,
                string aTextureKey) : base(aGame)
        {
            Position = aPosition;
            myTexture = aGame.GameContents.GetTexture(aTextureKey);

            OnClick = new UniversalEvent();
        }

        /// <summary>
        /// Draws button texture on
        /// the canvas.
        /// </summary>
        public void Draw()
        {
            var tempTexture = myTexture.GetDefaultTexture();
            gameInstance.SpriteBatch.Draw(tempTexture, Position, Color.White);
        }

        /// <summary>
        /// Updates internal values,
        /// and checks for user interactions.
        /// </summary>
        // TODO: Abstract in the future.
        public void Update ()
        {
            var tempDimensions = GetDimensions();

            MouseState mouseState = Mouse.GetState();
            var mousePosition = mouseState.Position.ToVector2();

            var isClicked = mouseState.LeftButton == ButtonState.Pressed;

            if(isClicked && EngineUtils.CheckIfPointIsInRectangle(mousePosition, Position, tempDimensions))
            {
                OnClick.Invoke();
            }
        }

        /// <summary>
        /// Sets position for the button.
        /// The specified position will be
        /// used to properly render and
        /// check if the button was pressed.
        /// </summary>
        /// <param name="aPosition">
        /// Targeted position.
        /// </param>
        /// <returns>
        /// New position of the button.
        /// </returns>
        /// <throws>
        /// AggregateException, if the specified
        /// position is out of the canvas range.
        /// </throws>
        public Vector2 SetPosition (Vector2 aPosition)
        {
            if(!NativeUtils.IsPointOnCanvas(gameInstance, aPosition))
            {
                throw new AggregateException("The specified button position is out-of-canvas. Contact DEV.");
            }

            Position = aPosition;
            return Position;
        }

        /// <summary>
        /// Returns dimensions of
        /// the button.
        /// </summary>
        public Vector2 GetDimensions ()
        {
            var tempTexture = myTexture.GetDefaultTexture();
            return new Vector2(tempTexture.Width, tempTexture.Height);
        }
    }
}
