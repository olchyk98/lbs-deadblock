using Deadblock.Logic;
using Microsoft.Xna.Framework;

namespace Deadblock.Engine
{
    public class HealthBar : HeadBar
    {
        public HealthBar(GameProcess aGame, DrawableEntity anEntity) : base(aGame, anEntity, 10, Color.Red)
        { }

        override public void Draw(float someGapY)
        {
            var tempHealthCoef = TargetEntity.Health / TargetEntity.MaxHealth;
            base.Draw(someGapY, tempHealthCoef);
        }
    }
}
