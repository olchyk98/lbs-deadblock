using Microsoft.Xna.Framework.Graphics;

namespace Engine
{
    public interface IContentWorker
    {
        /// <summary>
        /// Loads file from the path 
        /// </summary>
        /// <param name="aSourceName">
        /// Name of the targeted texture in the store.
        /// </param>
        /// <returns>
        /// Texture loaded from the storage.
        /// </returns>
        public Texture2D GetTexture (string aSourceName);

        /// <summary>
        /// Replaces existing source path in storage
        /// with the provided one.
        /// </summary>
        /// <param name="aSourceName">
        /// Name of the source.
        /// </param>
        /// <param name="aPath">
        /// Path for loadable file.
        /// </param>
        public void WritePath (string aSourceName, string aPath);
    }
}
