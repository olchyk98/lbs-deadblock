using Deadmind.Engine;

namespace Deadblock.Engine
{
    public interface IContentWorker
    {
        /// <summary>
        /// Gets previously loaded texture spec.
        /// </summary>
        /// <param name="aSourceName">
        /// Name of the targeted texture in the store.
        /// </param>
        /// <returns>
        /// Specification instance of the texture
        /// with included Texture2D object.
        /// </returns>
        public WorkerTexture GetTexture(string aSourceName);

        /// <summary>
        /// Returns all textures with specified prefix.
        /// Texture should be previously registered
        /// with prefix in that format: {prefix}/{name}.
        /// </summary>
        /// <param name="aPrefix">
        /// Filter Query represented as prefix.
        /// </param>
        /// <returns>
        /// Found texture specs
        /// with the specified prefix.
        /// </returns>
        public WorkerTexture[] GetTexturesByPrefix(string aPrefix);
    }
}
