using System;
using System.Collections.Generic;
using System.Linq;
using Deadblock;
using Deadblock.Generic;
using Deadmind.Engine;
using Microsoft.Xna.Framework.Graphics;
using Tools;

namespace Engine
{
    public class ContentWorker : DeliveredGameSlot, IContentWorker
    {
        private Dictionary<string, WorkerTexture> mySources;

        public ContentWorker (GameProcess aGame, string aConfigsPath) : base(aGame)
        {
            LoadSources(aConfigsPath);
        }

        /// <summary>
        /// Loads source configs
        /// from the specified file.
        /// </summary>
        /// <param name="aConfigsPath">
        /// Targeted file.
        /// </param>
        private void LoadSources (string aConfigsPath)
        {
            List<Dictionary<string, string>> tempConfigs = FileUtils.ReadAsConfigBlocks(aConfigsPath).ToList();
            var tempSources = new Dictionary<string, WorkerTexture>();

            foreach (var config in tempConfigs)
            {
                var textureSpec = AggregationUtils.CreateInstanceFromDictionary<WorkerTexture>(config);

                textureSpec.Texture = Texture2D.FromFile(gameInstance.GraphicsDevice, textureSpec.Path);
                tempSources[config["Name"]] = textureSpec;
            }

            mySources = tempSources;
        }

        public WorkerTexture GetTexture (string aSourceName)
        {
            if(!mySources.ContainsKey(aSourceName))
            {
                throw new ArgumentException($"Provided invalid source name for the content worker. Ensure that source with name {aSourceName} was provided to the storage.");
            }

            return mySources[aSourceName];
        }

        // NOTE: Optimized since 20 Jun 2021.
        // Hours spent to optimize: 0.1
        public WorkerTexture[] GetTexturesByPrefix(string aPrefix)
        {
            var tempFoundPrefixes = new List<WorkerTexture>();
            var prefixQuery = $"{aPrefix}/";

            foreach (KeyValuePair<string, WorkerTexture> pair in mySources)
            {
                if(!pair.Key.StartsWith(prefixQuery)) continue;
                tempFoundPrefixes.Add(pair.Value);
            }

            return tempFoundPrefixes.ToArray();
        }
    }
}
