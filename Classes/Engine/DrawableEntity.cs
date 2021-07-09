using Deadblock.Generic;
using Deadblock.Engine;
using System;

namespace Deadblock.Logic
{
    public abstract class DrawableEntity : Entity, IDrawable
    {
        public SpriteBlock Texture { get; private set; }

        public DrawableEntity(GameProcess aGame, string aTextureName, float someHealth) : base(aGame, someHealth)
        {
            LoadTexture(aTextureName);
        }

        /// <summary>
        /// Loads texture for the entity.
        /// </summary>
        private void LoadTexture(string aTextureName)
        {
            Texture = new SpriteBlock(gameInstance, aTextureName);
        }

        /// <summary>
        /// Draws object on the screen
        /// on its position.
        /// </summary>
        public void Draw()
        {
            Texture.Render(Position);
        }

        public virtual void Update()
        { }
    }
}
