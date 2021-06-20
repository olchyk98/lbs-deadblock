using System;
using System.Collections.Generic;
using Deadblock;
using Deadblock.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Engine
{
    public class ContentWorker : DeliveredGameSlot, IContentWorker
    {
        private Dictionary<string, string> mySources;

        public ContentWorker (GameProcess aGame, Dictionary<string, string> someSources) : base(aGame)
        {
            mySources = someSources;
        }

        public Texture2D GetTexture (string aSourceName)
        {
            if(!mySources.ContainsKey(aSourceName))
            {
                throw new ArgumentException($"Provided invalid source name for the content worker. Ensure that source with name {aSourceName} was provided to the storage.");
            }

            string tempPath = mySources[aSourceName];
            return Texture2D.FromFile(gameInstance.GraphicsDevice, tempPath);
        }

        public void WritePath (string aSourceName, string aPath)
        {
            mySources[aSourceName] = aPath;
        }
    }
}
