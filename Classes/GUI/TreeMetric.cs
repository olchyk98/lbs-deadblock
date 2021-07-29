using Microsoft.Xna.Framework;

namespace Deadblock.GUI
{
    public class TreeMetric : GUIMetric
    {
        private int myMaxValue;

        public TreeMetric(GameProcess aGame, Vector2 aPosition) : base(aGame, aPosition, "icon/tree")
        {
            myMaxValue = gameInstance.World.MainPlayer.Bag.MaxNumberOfTrees;
            myCurrentValue = $"0/{myMaxValue}";
        }

        override public void Update()
        {
            var nOfTrees = gameInstance.World.MainPlayer.Bag.Trees;
            myCurrentValue = $"{nOfTrees}/{myMaxValue}";

            base.Update();
        }
    }
}
