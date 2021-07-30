using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Deadblock.Engine;
using Deadblock.GUI;
using Deadblock.Session;

namespace Deadblock
{
    public class GameProcess : Game
    {
        private GraphicsDeviceManager myGraphics;

#region Sessions
        private MainMenuSession myMenuSession;
        private PlaySession myPlaySession;
#endregion

#region AlwaysAlive
        public SpriteBatch SpriteBatch { get; private set; }
        public ContentWorker GameContents { get; private set; }
#endregion

#region PlaySession
        public InputSystem InputSystem { get => myPlaySession.InputSystem; }
        public GUIOverlay GUIOverlay { get => myPlaySession.GUIOverlay; }
        public World World { get => myPlaySession.World; }
#endregion

        public GameProcess()
        {
            myGraphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            myGraphics.IsFullScreen = true;
            myGraphics.PreferredBackBufferWidth = 940;
            myGraphics.PreferredBackBufferHeight = 540;
        }

        override protected void Initialize()
        {
            GameContents = new ContentWorker(this, @"./Content/SpriteSpecs/main.txt");

            myMenuSession = new MainMenuSession(this);
            myPlaySession = new PlaySession(this);

            myMenuSession.OnPlay.Subscribe(() => Console.WriteLine("Play"));
            myMenuSession.OnQuit.Subscribe(() => Exit());

            //////////////////////////

            myMenuSession.Initialize();
            //myPlaySession.Initialize();

            //////////////////////////

            base.Initialize();
        }

        override protected void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        override protected void Update(GameTime gameTime)
        {
            myMenuSession.Update();
            //myPlaySession.Update();

            base.Update(gameTime);
        }

        override protected void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SpriteBatch.Begin();

            // TODO: Abstract to SessionOrchestrator
            myMenuSession.Draw();
            //myPlaySession.Draw();
            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
