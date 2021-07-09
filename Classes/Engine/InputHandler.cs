using Deadblock.Generic;
using Microsoft.Xna.Framework.Input;

namespace Deadblock.Engine
{
    public class InputHandler : DeliveredGameSlot
    {
        public UniversalEvent<Keys> OnPressButton { get; }

        public UniversalEvent<bool> OnMoveUp { get; }
        public UniversalEvent<bool> OnMoveRight { get; }
        public UniversalEvent<bool> OnMoveLeft { get; }
        public UniversalEvent<bool> OnMoveDown { get; }

        public InputHandler(GameProcess aGame) : base(aGame)
        {
            OnMoveDown = new UniversalEvent<bool>();
            OnMoveUp = new UniversalEvent<bool>();
            OnMoveRight = new UniversalEvent<bool>();
            OnMoveLeft = new UniversalEvent<bool>();

            OnPressButton = new UniversalEvent<Keys>();
        }

        /// <summary>
        /// Should be called on each frame.
        /// Processes the key actions on tick.
        /// </summary>
        public void Update()
        {
            KeyboardState state = Keyboard.GetState();

            //////////////////

            foreach (var key in state.GetPressedKeys())
                OnPressButton.Invoke(key);

            //////////////////

            if (state.IsKeyDown(Keys.S))
            {
                OnMoveDown.Invoke(true);
            }

            if (state.IsKeyDown(Keys.D))
            {
                OnMoveRight.Invoke(true);
            }

            if (state.IsKeyDown(Keys.W))
            {
                OnMoveUp.Invoke(true);
            }

            if (state.IsKeyDown(Keys.A))
            {
                OnMoveLeft.Invoke(true);
            }
        }
    }
}
