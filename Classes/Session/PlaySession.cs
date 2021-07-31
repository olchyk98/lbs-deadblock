using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Deadblock.Tools;
using Deadblock.Engine;
using Deadblock.Generic;
using Deadblock.GUI;

namespace Deadblock.Sessions
{
    public enum GameEndScenario
    {
        PLAYER_WON,
        PLAYER_LOST,
        PLAYER_LOST_NOHEALTH,
        PLAYER_LOST_NODRINK,
    }

    public class PlaySession : DeliveredGameSlot, ISession
    {
        private GameEndScenario? myActiveScenario;

        public InputSystem InputSystem { get; private set; }
        public GUIOverlay GUIOverlay { get; private set; }
        public World World { get; private set; }

        public UniversalEvent OnEnd { get; private set; }

        public PlaySession (GameProcess aGame) : base(aGame)
        {
            OnEnd = new UniversalEvent();
        }

        /// <summary>
        /// Draws current scenario message
        /// in the middle of the canvas.
        /// </summary>
        private void DrawScenarioStatus ()
        {
            string tempStatus = default;

            ////////////////////////////////

            switch(myActiveScenario)
            {
                case GameEndScenario.PLAYER_WON:
                    tempStatus = "You Won!";
                    break;
                case GameEndScenario.PLAYER_LOST_NODRINK:
                    tempStatus = "You Lost! You forgot to drink water!";
                    break;
                case GameEndScenario.PLAYER_LOST_NOHEALTH:
                    tempStatus = "You are Dead!";
                    break;
                default:break;
            }

            if(tempStatus == default) throw new Exception();

            ////////////////////////////////

            var tempFont = gameInstance.GameContents.FontDrawer.PullFont("Arial");
            var tempTextDimensions = tempFont.MeasureString(tempStatus);
            var tempScreenDimensions = NativeUtils.GetScreenResolution(gameInstance);
            var tempPosition = tempScreenDimensions / 2 - tempTextDimensions / 2;

            gameInstance.SpriteBatch.DrawString(tempFont, tempStatus, tempPosition, Color.White);
        }

        /// <summary>
        /// Coroutine.
        /// Responsible for
        /// handling the timeout
        /// between game ending and fade to menu.
        /// </summary>
        /// <param name="aScenario">
        /// Targeted scenario.
        /// </param>
        private void FadeToMenuWithScenario(GameEndScenario aScenario)
        {
        }

        private void HandleGameFinished(GameEndScenario aScenario)
        {
            myActiveScenario = aScenario;

            // NOTE: Freezing thread is a bad
            // practice, but since I (the main dev on this)
            // don't have enough time for a non-production
            // project, I'll leave like that.
            // NOTE: In case of optimization, this
            // solution needs to be destroyed. Everything
            // should be implemented in a separate thread,
            // with a reliable persistent events pipeline.
            // NOTE: This approach is fine for now,
            // as the game itself is stupid simple.
            // It does not do anything in background,
            // therefore it's acceptable to block
            // the thread for 3s (!). In any production
            // codebase, this kind of
            // implementation would be a huge problem.
            // NOTE: Moving this logic to a separate logic may
            // produce some problems with SessionOrchestrator,
            // as it disposes and instantiates objects on fly,
            // when active session changes. Therefore,
            // in case of thread implementation is the task,
            // it's important to consider creating a
            // Memsafe implementation.
            Thread.Sleep(3000);

            myActiveScenario = null;

            Initialize();
            OnEnd.Invoke();
        }

        public void Draw()
        {
            if(myActiveScenario != null)
            {
                DrawScenarioStatus();
                return;
            }

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

            World.OnEnd.Subscribe(HandleGameFinished);

            myActiveScenario = null;
        }
        // 🌈 writing code on mdma is amazzzing 🌈
        // nocom 30.7.21
    }
}
