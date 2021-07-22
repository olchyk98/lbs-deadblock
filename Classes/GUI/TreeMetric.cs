using Microsoft.Xna.Framework;

namespace Deadblock.GUI
{
    public class TreeMetric : GUIMetric
    {
        public TreeMetric(GameProcess aGame, Vector2 aPosition) : base(aGame, aPosition, "icon/tree")
        {
            myCurrentValue = "0";
        }

        public override void Draw ()
        {
            base.Draw();
        }

        public override void Update ()
        {
            var nOfTrees = gameInstance.World.myMainPlayer.Bag.Trees;
            myCurrentValue = nOfTrees.ToString();

            base.Update();
        }
    }
}
