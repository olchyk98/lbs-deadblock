using System.Collections.Generic;
using Deadblock.Generic;
using Deadblock.Engine;

namespace Deadblock.Logic
{
    public abstract class DrawableEntity : Entity, IDrawable

    {
        public SpriteBlock Texture { get; private set; }
        private List<HeadBar> myHeadBars;

        private readonly static float AlphaPerTick = .01f;
        private readonly static int HeadBarsMargin = 5;

        public DrawableEntity(GameProcess aGame, string aTextureName, float someHealth) : base(aGame, someHealth)
        {
            InitializeTexture(aTextureName);
            InitializeHeadBars();
        }

        /// <summary>
        /// Loads texture for the entity.
        /// </summary>
        private void InitializeTexture(string aTextureName)
        {
            Texture = new SpriteBlock(gameInstance, aTextureName);
            Texture.SetAlpha(1f);
        }

        /// <summary>
        /// Setups a new list head bars.
        /// </summary>
        private void InitializeHeadBars()
        {
            var tempHealthBar = new HealthBar(gameInstance, this);

            ////////////

            myHeadBars = new List<HeadBar>();
            myHeadBars.Add(tempHealthBar);
        }

        /// <summary>
        /// Adds head bar to the pipeline.
        /// The added head bar will be directly rendered.
        /// </summary>
        /// <param name="aHeadbar">
        /// Targeted head bar.
        /// </param>
        public List<HeadBar> RegisterHeadBar(HeadBar aHeadbar)
        {
            myHeadBars.Add(aHeadbar);
            return myHeadBars;
        }

        /// <summary>
        /// Removes head bar from the pipeline.
        /// The remove head bar will be directly removed.
        /// </summary>
        /// <param name="aHeadbar">
        /// Targeted head bar.
        /// </param>
        public List<HeadBar> RemoveHeadBar(HeadBar aHeadbar)
        {
            myHeadBars.Remove(aHeadbar);
            return myHeadBars;
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
        virtual public void Draw()
        {
            Texture.Render(Position);
            DrawHeadBars();
        }

        virtual public void Update()
        {
            if (Texture.Alpha < 1f)
                Texture.SetAlpha(Texture.Alpha + AlphaPerTick);
        }

        /// <summary>
        /// Draws bars for
        /// the entity on canvas.
        /// </summary>
        public void DrawHeadBars()
        {
            int renderedPixelsY = 0;

            for (var ma = 0; ma < myHeadBars.Count; ++ma)
            {
                HeadBar tempHeadBar = myHeadBars[ma];

                int tempBarHeight = (int)tempHeadBar.GetDimensions().Y;
                renderedPixelsY = renderedPixelsY + HeadBarsMargin + tempBarHeight;

                tempHeadBar.Draw(renderedPixelsY);
            }
        }

        override public void ApplyDamage(float someDamage)
        {
            Texture.SetAlpha(.4f);
            base.ApplyDamage(someDamage);
        }
    }
}
