using Deadblock.Engine;

namespace Deadblock.Logic
{
    public abstract class InteractableDynamicBlock : DynamicBlock
    {
        public InteractableDynamicBlock(GameProcess aGame, string aTextureKey) : base(aGame, aTextureKey)
        {  }

        virtual public void InteractWith (Entity anEntity)
        {  }
    }
}
