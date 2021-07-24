using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Deadblock.Engine;
using Deadblock.GUI;

namespace Deadblock
{
    public class GameProcess : Game
    {
        private GraphicsDeviceManager myGraphics;

        public SpriteBatch SpriteBatch { get; private set; }
        public ContentWorker GameContents { get; private set; }
        public InputSystem InputSystem { get; private set; }
        public GUIOverlay GUIOverlay { get; private set; }
        public World World { get; private set; }

        public GameProcess()
        {
            myGraphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            InputSystem = new InputSystem(this);
            GameContents = new ContentWorker(this, @"./Content/SpriteSpecs/main.txt");
            World = new World(this);
            GUIOverlay = new GUIOverlay(this);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            InputSystem.Update();
            World.Update();
            GUIOverlay.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SpriteBatch.Begin();

            World.Draw();
            GUIOverlay.Draw();

            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
