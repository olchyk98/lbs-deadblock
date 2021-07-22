using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Deadblock.Generic;

namespace Deadblock.GUI
{
    public class GUIOverlay : DeliveredGameSlot, Logic.IDrawable
    {
        private GUIMetric[] myMetrics;

        public GUIOverlay (GameProcess aGame) : base(aGame)
        {
            SetupMetrics();
        }

        private void SetupMetrics ()
        {
            var tempMetrics = new List<GUIMetric>();

            var trees = new TreeMetric(gameInstance, new Vector2(200f, 200f));
        }

        /// <summary>
        /// Renders GUI on the screen.
        /// </summary>
        public void Draw ()
        {

        }
    }
}
