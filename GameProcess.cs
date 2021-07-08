using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Deadblock.Engine;

namespace Deadblock
{
    public class GameProcess : Game
    {
        private GraphicsDeviceManager myGraphics;

        public SpriteBatch SpriteBatch { get; private set; }
        public ContentWorker GameContents { get; private set; }
        public InputHandler InputHandler { get; private set; }
        public World World { get; private set; }

        public GameProcess()
        {
            myGraphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            InputHandler = new InputHandler(this);
            GameContents = new ContentWorker(this, @"./Content/SpriteSpecs/main.txt");
            World = new World(this);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            InputHandler.Update();
            World.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SpriteBatch.Begin();
            World.Draw();
            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
