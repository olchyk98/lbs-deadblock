using System.Collections.Generic;
using Deadblock.Tools;
using Microsoft.Xna.Framework.Audio;

namespace Deadblock.Generic
{
    /// <summary>
    // Wrapper for SoundEffectInstance
    // that keeps track of
    // when the last time the player
    // was fired.
    /// </summary>
    internal class SequentialPlayer
    {
        private long myLastPlayTime = default;
        private SoundEffect mySoundEffect;

        /// <summary>
        /// Boolean, that represents if the sound instance
        /// is played in this time.
        /// If false, the Play method cannot
        /// be called right, as the sample is currently playing.
        /// </summary>
        public bool IsPlayable
        {
            get => myLastPlayTime == default
                || NativeUtils.GetTime() - myLastPlayTime > mySoundEffect.Duration.Milliseconds;
        }

        /// <summary>
        /// Plays the sound.
        /// </summary>
        /// <returns>
        /// Boolean, that represents
        /// if the sound was played.
        /// The method call can be ignored,
        /// if the sample is already playing.
        /// </returns>
        public bool Play()
        {
            if (!IsPlayable) return false;

            mySoundEffect.Play();
            myLastPlayTime = NativeUtils.GetTime();

            return true;
        }

        public SequentialPlayer(SoundEffect aSound)
        {
            mySoundEffect = aSound;
        }
    }

    public class SoundOrchestrator : DeliveredGameSlot, ISoundOrchestrator
    {
        // { id: instance }
        private Dictionary<string, SoundEffectInstance> myActivePlayers;
        // { key: sequentialPlayer  }
        private Dictionary<string, SequentialPlayer> myActiveSequentialPlayers;
        private long lastTime = 0;

        public SoundOrchestrator(GameProcess aGame) : base(aGame)
        {
            myActivePlayers = new Dictionary<string, SoundEffectInstance>();
            myActiveSequentialPlayers = new Dictionary<string, SequentialPlayer>();
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
            return gameInstance.Content.Load<SoundEffect>($"Sounds/{aKey}");
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
            // Creates a new player if the
            // sound was never called before.
            if (!myActiveSequentialPlayers.ContainsKey(aSoundKey))
            {
                var tempSound = GetSoundEffect(aSoundKey);
                var tempPlayer = new SequentialPlayer(tempSound);

                myActiveSequentialPlayers[aSoundKey] = tempPlayer;
                tempPlayer.Play();
            }

            // Method, may be ignored, if the buffer
            // is already filled with the sample output (sound emission).
            myActiveSequentialPlayers[aSoundKey].Play();
        }
    }
}
