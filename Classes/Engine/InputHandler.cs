using System.Collections.Generic;
using Deadblock.Generic;
using Microsoft.Xna.Framework.Input;

namespace Deadblock.Engine
{
    public class InputSystem : DeliveredGameSlot
    {
        #region Buttons
        public UniversalEvent<Keys> OnPressButton { get; } = new UniversalEvent<Keys>();

        public UniversalEvent OnMoveUp { get; } = new UniversalEvent();
        public UniversalEvent OnMoveRight { get; } = new UniversalEvent();
        public UniversalEvent OnMoveLeft { get; } = new UniversalEvent();
        public UniversalEvent OnMoveDown { get; } = new UniversalEvent();

        public UniversalEvent OnRegularUse { get; } = new UniversalEvent();
        public UniversalEvent OnEscape { get; } = new UniversalEvent();
        #endregion

        private Dictionary<Keys, UniversalEvent> myActiveKeys;

        public InputSystem(GameProcess aGame) : base(aGame)
        {
            myActiveKeys = new Dictionary<Keys, UniversalEvent>() {
                { Keys.S, OnMoveDown },
                { Keys.D, OnMoveRight },
                { Keys.W, OnMoveUp },
                { Keys.A, OnMoveLeft },
                { Keys.E, OnRegularUse },
                { Keys.Escape, OnEscape },
            };
        }

        /// <summary>
        /// Should be called on each frame.
        /// Processes the key actions on tick.
        /// </summary>
        public void Update()
        {
            KeyboardState state = Keyboard.GetState();

            //////////////////

            foreach ((var key, var caller) in myActiveKeys)
            {
                if (!state.IsKeyDown(key)) continue;

                caller.Invoke();
                OnPressButton.Invoke(key);
            }
        }
    }
}
