using System;
using System.Collections.Generic;
using System.Linq;
using Deadblock.Generic;
using Deadblock.Tools;
using Microsoft.Xna.Framework.Graphics;

namespace Deadblock.Engine
{
    public class ContentWorker : DeliveredGameSlot, IContentWorker
    {
        private Dictionary<string, WorkerTexture> mySources;
        public FontDrawer FontDrawer { get; private set; }

        public ContentWorker(GameProcess aGame, string aConfigsPath) : base(aGame)
        {
            LoadSources(aConfigsPath);
            FontDrawer = new FontDrawer(aGame);
        }

        /// <summary>
        /// Loads all textures from the spec.
        /// </summary>
        /// <param name="aSpec">
        /// Dictionary in format:
        /// variant: relative or absolute path to the texture.
        /// </param>
        /// <returns>
        /// Dictionary in format:
        /// variant: texture.
        /// </returns>
        private Dictionary<string, Texture2D> LoadTexturesFromSpec(Dictionary<string, string> aSpec)
        {
            var tempTextures = new Dictionary<string, Texture2D>();

            foreach (var pair in aSpec)
            {
                tempTextures[pair.Key] = Texture2D.FromFile(gameInstance.GraphicsDevice, pair.Value);
            }

            return tempTextures;
        }

        /// <summary>
        /// Loads source configs
        /// from the specified file.
        /// </summary>
        /// <param name="aConfigsPath">
        /// Targeted file.
        /// </param>
        private void LoadSources(string aConfigsPath)
        {
            List<Dictionary<string, string>> tempConfigs = FileUtils.ReadAsConfigBlocks(aConfigsPath).ToList();
            var tempSources = new Dictionary<string, WorkerTexture>();

            foreach (var config in tempConfigs)
            {
                var textureSpec = AggregationUtils.CreateInstanceFromConfigBlock<WorkerTexture>(config);

                textureSpec.Textures = LoadTexturesFromSpec(textureSpec.Paths);
                tempSources[config["Name"]] = textureSpec;
            }

            mySources = tempSources;
        }

        public WorkerTexture GetTexture(string aSourceName)
        {
            if (!mySources.ContainsKey(aSourceName))
            {
                throw new ArgumentException($"Provided invalid source name for the content worker. Ensure that source with name {aSourceName} is registered in the storage.");
            }

            return mySources[aSourceName];
        }

        /// <summary>
        /// Returns all textures from the config
        /// that are previously marked with the specified prefix.
        /// </summary>
        /// <param name="aPrefix">
        /// Targeted prefix.
        /// </param>
        /// <returns>
        /// Array of textures with
        /// the specified prefix.
        /// </returns>
        public WorkerTexture[] GetTexturesByPrefix(string aPrefix)
        {
            var tempFoundPrefixes = new List<WorkerTexture>();
            var prefixQuery = $"{aPrefix}/";

            foreach (KeyValuePair<string, WorkerTexture> pair in mySources)
            {
                if (!pair.Key.StartsWith(prefixQuery)) continue;
                tempFoundPrefixes.Add(pair.Value);
            }

            return tempFoundPrefixes.ToArray();
        }
    }
}
