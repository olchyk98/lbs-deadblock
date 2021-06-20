using Deadblock.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Deadmind.Engine
{
    public class WorkerTexture
    {
        public char ID { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public Texture2D Texture { get; set; }
        public DrawPositionMode DrawPositionMode { get; set; }
    }
}
