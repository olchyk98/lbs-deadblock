using System.Collections.Generic;
using Deadblock.Tools;
using Microsoft.Xna.Framework.Audio;

namespace Deadblock.Generic
{
    public class SoundOrchestrator : DeliveredGameSlot, ISoundOrchestrator
    {
        // { id: instance }
        private Dictionary<string, SoundEffectInstance> myActivePlayers;
        // { key: instance  }
        private Dictionary<string, SoundEffectInstance> myActiveSequentialPlayers;

        public SoundOrchestrator(GameProcess aGame) : base(aGame)
        {
            myActivePlayers = new Dictionary<string, SoundEffectInstance>();
            myActiveSequentialPlayers = new Dictionary<string, SoundEffectInstance>();
        }

        /// <summary>
        /// Pulls specified sound content
        /// from the content pipeline.
        /// </summary>
        /// <param name="aKey">
        /// Key of the targeted sound,
        /// specified in the pipeline.
        /// </param>
        private SoundEffect GetSoundEffect(string aKey)
        {
            // In this case, it's okay to load the same sound multiple times,
            // since MonoGame has an internal caching system implemented.
            // Reference: https://github.com/MonoGame/MonoGame/blob/f2ee0def3690e1c95273623f60fe47ddc8c12c68/MonoGame.Framework/Content/ContentManager.cs#L246
            return gameInstance.Content.Load<SoundEffect>(aKey);
        }

        public void KillPlayers()
        {
            foreach (var player in myActivePlayers.Values)
                player.Stop();

            myActivePlayers.Clear();
        }

        public void PlaySound(string aSoundKey)
        {
            var tempSound = GetSoundEffect(aSoundKey);
            tempSound.Play();
        }

        public string PlaySoundInPlayer(string aSoundKey)
        {
            var tempSound = GetSoundEffect(aSoundKey);

            SoundEffectInstance tempPlayer = tempSound.CreateInstance();
            var tempPlayerId = NativeUtils.GenerateID();

            myActivePlayers.Add(tempPlayerId, tempPlayer);
            tempPlayer.Play();

            return tempPlayerId;
        }

        public void StopSoundFromPlayer(string aPlayerId)
        {
            if (!myActivePlayers.TryGetValue(aPlayerId, out SoundEffectInstance tempPlayer))
                return;

            tempPlayer.Stop();
            myActivePlayers.Remove(aPlayerId);
        }

        public void PlaySoundInSequencer(string aSoundKey)
        {
            myActiveSequentialPlayers.TryGetValue(aSoundKey, out SoundEffectInstance tempExistingPlayer);

            // Checks if the player already exists.
            if (tempExistingPlayer == null)
            {
                var tempSound = GetSoundEffect(aSoundKey);
                SoundEffectInstance tempPlayer = tempSound.CreateInstance();

                myActiveSequentialPlayers[aSoundKey] = tempPlayer;
                tempPlayer.Play();
            }

            // Checks if the buffer is filled with the output
            // of this player.
            if (tempExistingPlayer.State != SoundState.Stopped && tempExistingPlayer.State != SoundState.Paused)
            {
                // Moves cursor to the beginning.
                tempExistingPlayer.Stop();

                // Plays the sound.
                tempExistingPlayer.Play();
            }
        }
    }
}
