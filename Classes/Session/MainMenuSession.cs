using System;
using System.Linq;
using Deadblock.Generic;
using Microsoft.Xna.Framework;
using Deadblock.GUI;
using Deadblock.Tools;
using System.Collections.Generic;

namespace Deadblock.Sessions
{
    public class MainMenuSession : DeliveredGameSlot, ISession
    {
        private List<GUIButton> myButtons;

        public UniversalEvent OnPlay { get; private set; }
        public UniversalEvent OnQuit { get; private set; }

        private readonly static int MarginBetweenButtons = 10;

        public MainMenuSession (GameProcess aGame) : base(aGame)
        {
            OnPlay = new UniversalEvent();
            OnQuit = new UniversalEvent();
        }

        /// <summary>
        /// Instantiates and positions
        /// main buttons.
        /// </summary>
        private void InitializeNavigationButtons()
        {
            myButtons = new List<GUIButton>();

            ////////////////////

            var tempPlayButton = new GUIButton(gameInstance, Vector2.Zero, "uibtn/play");
            var tempQuitButton = new GUIButton(gameInstance, Vector2.Zero, "uibtn/quit");

            ////////////////////

            myButtons.Add(tempPlayButton);
            myButtons.Add(tempQuitButton);

            ////////////////////

            tempPlayButton.OnClick.Subscribe(() => OnPlay.Invoke());
            tempQuitButton.OnClick.Subscribe(() => OnQuit.Invoke());

            ////////////////////

            var tempScreenDimensions = NativeUtils.GetScreenResolution(gameInstance);
            var tempChunkWidth = myButtons.Aggregate(0, (acc, ma) => acc + (int) ma.GetDimensions().X + MarginBetweenButtons);
            var tempChunkX = tempScreenDimensions.X / 2 - tempChunkWidth / 2;

            // A hacky way to progressively
            // position the buttons.
            for(var ma = 0; ma < myButtons.Count; ++ma)
            {
                var button = myButtons[ma];
                var buttonDimensions = button.GetDimensions();

                var positionX = tempChunkX + ma * (buttonDimensions.X + MarginBetweenButtons);
                var positionY = tempScreenDimensions.Y / 2 + buttonDimensions.Y / 2;
                var position = new Vector2(positionX, positionY);

                button.SetPosition(position);
            }
        }

        public void Draw ()
        {
            foreach (var button in myButtons)
                button.Draw();
        }

        public void Update()
        {
            foreach (var button in myButtons)
                button.Update();
        }

        public void Initialize()
        {
            InitializeNavigationButtons();
        }
    }
}
