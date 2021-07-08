using Deadblock.Generic;
using Microsoft.Xna.Framework;

namespace Deadblock.Logic
{
    public abstract class DynamicBlock : DeliveredGameSlot, ISpriteBlock
    {
        // NOTE: Class implements ISpriteBlock using
        // a native instance of SpriteBlock.
        // This is kind of solution is designed
        // to bring more freedom for extending and supporting this class.
        public SpriteBlock MainSprite { get; private set; }
        public bool HasCollider { get; } = true;

        public DynamicBlock(GameProcess aGame, string aTextureKey) : base(aGame)
        {
            MainSprite = new SpriteBlock(aGame, aTextureKey);
        }

        virtual public void Render(Vector2 aPosition, bool isRelative = true) {
            MainSprite.Render(aPosition, isRelative);
            Update();
        }

        virtual public void Update() { }

        virtual public Vector2 GetDimensions()
        {
            return MainSprite.GetDimensions();
        }
    }
}
