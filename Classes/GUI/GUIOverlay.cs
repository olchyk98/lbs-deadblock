using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Deadblock.Generic;
using Deadblock.Tools;

namespace Deadblock.GUI
{
    public class GUIOverlay : DeliveredGameSlot, Logic.IDrawable
    {
        private List<GUIMetric> myMetrics;

        public GUIOverlay(GameProcess aGame) : base(aGame)
        {
            SetupMetrics();
        }

        private void SetupMetrics()
        {
            myMetrics = new List<GUIMetric>();
            var tempScreenDimensions = NativeUtils.GetScreenResolution(gameInstance);

            ////////////////

            var treeMetric = new TreeMetric(gameInstance, new Vector2(tempScreenDimensions.X - 100f, 10f));

            ////////////////

            myMetrics.Add(treeMetric);
        }

        /// <summary>
        /// Draws all metrics presented
        /// in the GUI.
        /// </summary>
        private void DrawMetrics()
        {
            foreach (var metric in myMetrics)
            {
                metric.Draw();
            }
        }

        /// <summary>
        /// Renders GUI on the screen.
        /// </summary>
        public void Draw()
        {
            DrawMetrics();
        }

        /// <summary>
        /// Calls update on all metrics
        /// presented in the GUI.
        /// </summary>
        private void UpdateMetrics()
        {
            foreach (var metric in myMetrics)
            {
                metric.Update();
            }
        }

        /// <summary>
        /// Updates state of
        /// the GUI components.
        /// </summary>
        public void Update()
        {
            UpdateMetrics();
        }
    }
}
