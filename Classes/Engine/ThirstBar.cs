using Deadblock.Logic;
using Microsoft.Xna.Framework;

namespace Deadblock.Engine
{
    public class ThirstBar : PlayerHeadBar
    {
        public ThirstBar(GameProcess aGame, Player aPlayer) : base(aGame, aPlayer, 5, Color.Blue)
        { }

        public override void Draw(float someGapY)
        {
            var tempThirstCoef = TargetPlayer.Bag.WaterFillment;
            base.Draw(someGapY, tempThirstCoef);
        }
    }
}
