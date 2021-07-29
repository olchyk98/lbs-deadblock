using System.Collections.Generic;
using Deadblock.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Deadblock.Engine
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
        /// Absolute or Relative paths to
        /// the texture on the running machine.
        /// Represented as dictionary,
        /// in format variantName: texturePath.
        ///
        /// Varient "Default" is always present is the dictionary.
        ///
        /// Example value: Default: ./foo/bar.png
        /// Example value: TopSide: /coreusr/bart/foot.png
        /// </summary>
        public Dictionary<string, string> Paths { get; set; }

        /// <summary>
        /// Texture instances created
        /// from contents of the referenced file.
        /// Represented as dictionary, in format
        /// variantName: texture.
        /// Variant "Default" is always present in the dictionary.
        /// The first specified key will be treated as the default texture.
        /// </summary>
        public Dictionary<string, Texture2D> Textures { get; set; }

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

        /// <summary>
        /// Decides how the sprite should be resized
        /// during rendering.
        ///
        /// ORIGINAL:
        /// Sprite won't be
        /// resized in any way.
        ///
        /// SCALE_X:
        /// Resize the X axis
        /// so it would fit the default
        /// block width.
        ///
        /// SCALE_Y:
        /// Resize the Y axis
        /// so it would fir the default
        /// block height.
        ///
        /// SCALE_FULL:
        /// Fully scale the sprite
        /// to the default block size.
        /// </summary>
        public ResizeMode ResizeMode { get; set; }

        /// <summary>
        /// Returns default texture from
        /// loaded texture variants pack.
        /// May return null if the list is empty.
        /// </summary>
        public Texture2D GetDefaultTexture()
        {
            var tempTextureKey = GetDefaultTextureKey();

            if (tempTextureKey == default) return null;
            return Textures[tempTextureKey];
        }

        /// <summary>
        /// Returns an array of available
        /// texture keys for the texture.
        /// </summary>
        public string[] GetAvailableTextureKeys()
        {
            // NOTE: Could not fix usings.
            var tempKeys = new List<string>();

            foreach (var key in Textures.Keys)
                tempKeys.Add(key);

            return tempKeys.ToArray();
        }

        /// <summary>
        /// Returns default texture key from loaded texture variants
        /// pack. May return null if the list is empty.
        /// </summary>
        public string GetDefaultTextureKey()
        {
            var tempKeysCollection = GetAvailableTextureKeys();
            var tempKeys = new string[tempKeysCollection.Length];

            tempKeysCollection.CopyTo(tempKeys, 0);

            if (tempKeys.Length <= 0) return null;
            return tempKeys[0];
        }
    }
}
