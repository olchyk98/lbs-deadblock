using Deadblock.Engine;
using Deadblock.Generic;
using Deadblock.GUI;

namespace Deadblock.Session
{
    public enum GameEndScenario
    {
        PLAYER_WON,
        PLAYER_LOST,
    }

    public class PlaySession : DeliveredGameSlot, ISession
    {
        public InputSystem InputSystem { get; private set; }
        public GUIOverlay GUIOverlay { get; private set; }
        public World World { get; private set; }

        public PlaySession (GameProcess aGame) : base(aGame)
        {  }

        public void Draw()
        {
            World.Draw();
            GUIOverlay.Draw();
        }

        public void Update ()
        {
            InputSystem.Update();
            World.Update();
            GUIOverlay.Update();
        }

        public void Initialize ()
        {
            InputSystem = new InputSystem(gameInstance);
            World = new World(gameInstance);
            GUIOverlay = new GUIOverlay(gameInstance);
        }
        // 🌈 writing code on mdma is amazzzing 🌈
        // nocom 30.7.21
    }
}
