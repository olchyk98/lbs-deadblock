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

        private World myWorld;

        public GameProcess()
        {
            myGraphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            GameContents = new ContentWorker(this, @"./Content/SpriteSpecs/main.txt");
            myWorld = new World(this);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SpriteBatch.Begin();
            myWorld.Draw();
            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
