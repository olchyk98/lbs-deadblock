using System;
using Deadblock.Generic;
using Deadblock.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Deadblock.Tools;
using Deadblock.Extensions;

namespace Deadblock.Logic
{
    public abstract class DrawableEntity : Entity, IDrawable
    {
        public SpriteBlock Texture { get; private set; }
        private Texture2D myHealthBarTexture;

        private readonly static float BrightnessPerTick = .01f;

        public DrawableEntity(GameProcess aGame, string aTextureName, float someHealth) : base(aGame, someHealth)
        {
            InitializeTexture(aTextureName);
            InitializeHealthBar();
        }

        /// <summary>
        /// Loads texture for the entity.
        /// </summary>
        private void InitializeTexture(string aTextureName)
        {
            Texture = new SpriteBlock(gameInstance, aTextureName);
            Texture.SetBrightness(1f);
        }

        /// <summary>
        /// Creates a texture for the health bar.
        /// </summary>
        private void InitializeHealthBar()
        {
            myHealthBarTexture = NativeUtils.ConstructRectangle(gameInstance,
                someDimensions: new Vector2(60, 10),
                Color.Red);
        }

        /// <summary>
        /// Sets variant for the sprite.
        /// </summary>
        /// <param name="aVariant">
        /// Targeted variant.
        /// </param>
        public void SetSpriteVariant(string aVariant)
        {
            Texture.SetTextureVariant(aVariant);
        }

        /// <summary>
        /// Draws object on the screen
        /// on its position.
        /// </summary>
        public virtual void Draw()
        {
            Texture.Render(Position);
            DrawHealthBar();
        }

        public virtual void Update()
        {
            if (Texture.Brightness < 1f)
                Texture.SetBrightness(Texture.Brightness + BrightnessPerTick);
        }

        /// <summary>
        /// Draws healthbar for
        /// the entity on canvas.
        /// </summary>
        public void DrawHealthBar()
        {
            var tempTextureSize = Texture.GetDimensions();
            var tempPosition = new Vector2(Position.X + tempTextureSize.X / 2 - myHealthBarTexture.Width / 2,
                Position.Y - myHealthBarTexture.Height - 3);

            var tempHealthStat = Math.Clamp(Health / MaxHealth, 0, 1);
            var tempScale = new Vector2(tempHealthStat, 1);

            gameInstance.SpriteBatch.Draw(myHealthBarTexture,
                tempPosition,
                Color.White,
                scale: tempScale);
        }

        public override void ApplyDamage(float someDamage)
        {
            Texture.SetBrightness(.4f);
            base.ApplyDamage(someDamage);
        }
    }
}
