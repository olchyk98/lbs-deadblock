using Microsoft.Xna.Framework.Graphics;
using MonoColor = Microsoft.Xna.Framework.Color;
using MonoVector2 = Microsoft.Xna.Framework.Vector2;

namespace Deadblock.Generic
{
    public class SpriteBlock : DeliveredGameSlot, ISpriteBlock
    {
        private Texture2D myTexture;

        public SpriteBlock (GameProcess aGame, string aTextureKey) : base(aGame)
        {
            LoadTexture(aTextureKey);
        }

        private void LoadTexture (string aTextureKey)
        {
            myTexture = gameInstance.GameContents.GetTexture(aTextureKey);
        }

        public void Draw (MonoVector2 aPosition)
        {
            gameInstance.SpriteBatch.Draw(myTexture, aPosition, MonoColor.White);
        }
    }
}
