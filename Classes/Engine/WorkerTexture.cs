using Deadblock.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Deadmind.Engine
{
    public class WorkerTexture
    {
        /// <summary>
        /// Id of the texture.
        /// Mainly used in the map layers,
        /// which are usually represented as
        /// number of lists with different number-chars.
        ///
        /// Should be a char-number.
        /// Example value: '4'
        /// </summary>
        public char ID { get; set; }
        /// <summary>
        /// Name of texture.
        /// Mainly used to load and
        /// reference the texture.
        ///
        /// Example value: ent/player
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Absolute or Relative path to
        /// the texture on the running machine.
        ///
        /// Example value: ./foo/bar.png
        /// Example value: /coreusr/bart/foot.png
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// Texture instance created
        /// from contents of the referenced file.
        /// </summary>
        public Texture2D Texture { get; set; }
        /// <summary>
        /// Position mode, which decides
        /// how the texture will be placed during rendering.
        ///
        /// Possible Values:
        ///  CENTER_TO_POINT:
        ///  Sets pivot of the texture
        ///  to the center,
        ///  so that it would perfectly
        ///  match the specified position.
        ///
        ///  BOTTOM_TO_POINT:
        ///  Sets pivot of the texture
        ///  to bottom center.
        ///
        ///  TOP_TO_POINT:
        ///  Sets pivot of the texture
        ///  to top center.
        ///
        ///  ORIGINAL_POINT:
        ///  Sets pivot of the texture
        ///  to top left.
        /// </summary>
        public DrawPositionMode DrawPositionMode { get; set; }
        /// <summary>
        /// Name of a ISpriteBlock implementation
        /// that should be used to render
        /// and update the texture.
        ///
        /// Example value: Deadblock.Generic.Vector4
        /// </summary>
        public string ActiveInstanceName { get; set; }
    }
}
