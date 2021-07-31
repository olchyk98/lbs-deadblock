using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Deadblock.Engine;
using Deadblock.GUI;
using Deadblock.Sessions;

namespace Deadblock
{
    public class GameProcess : Game
    {
        private GraphicsDeviceManager myGraphics;
        private SessionOrchestrator mySessionOrchestrator;

#region AlwaysAlive
        public SpriteBatch SpriteBatch { get; private set; }
        public ContentWorker GameContents { get; private set; }
#endregion

#region PlaySession
        public InputSystem InputSystem { get => mySessionOrchestrator.PlaySession.InputSystem; }
        public GUIOverlay GUIOverlay { get => mySessionOrchestrator.PlaySession.GUIOverlay; }
        public World World { get => mySessionOrchestrator.PlaySession.World; }
#endregion

        public GameProcess()
        {
            myGraphics = new GraphicsDeviceManager(this);
            mySessionOrchestrator = new SessionOrchestrator(this);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            myGraphics.IsFullScreen = true;
            myGraphics.PreferredBackBufferWidth = 800;
            myGraphics.PreferredBackBufferHeight = 600;
        }

        override protected void Initialize()
        {
            GameContents = new ContentWorker(this, @"./Content/SpriteSpecs/main.txt");
            mySessionOrchestrator.Initialize();

            base.Initialize();
        }

        override protected void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        override protected void Update(GameTime gameTime)
        {
            mySessionOrchestrator.Update();

            base.Update(gameTime);
        }

        override protected void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SpriteBatch.Begin();
            mySessionOrchestrator.Draw();
            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
