namespace Deadblock.Generic
{
    public interface ISoundOrchestrator
    {
        /// <summary>
        /// Plays a sound by instantiating
        /// a new player. The id of the newly created
        /// player is returned. That can be
        /// used to control player.
        /// </summary>
        /// <param name="aSoundKey">
        /// Key of the targeted sound,
        /// that references an EXISTING
        /// item in the Mono Content Pipeline.
        ///
        /// Recommended convention for keys is: { source/environment/action }
        /// For example: { player/grass/walk }
        /// </param>
        /// <returns>
        /// ID of the player that instantiated.
        /// </returns>
        /// <implementation>
        /// - Requires a separate buffer!
        /// - Shall be implemented with Ux00 fillers.
        /// </implementation>
        public string PlaySoundInPlayer(string aSoundKey);

        /// <summary>
        /// Players a single sound.
        /// When the sound is done playing,
        /// the player instance is destroyed.
        /// The method does not expose a player.
        /// </summary>
        /// <param name="aSoundKey">
        /// Key of the targeted sound,
        /// that references an EXISTING
        /// item in the Mono Content Pipeline.
        ///
        /// Recommended convention for keys is: { source/environment/action }
        /// For example: { player/grass/walk }
        /// </param>
        public void PlaySound(string aSoundKey);

        /// <summary>
        /// Stops player with the specified
        /// id.
        /// </summary>
        /// <param name="aPlayerId">
        /// ID of the targeted player.
        /// </param>
        public void StopSoundFromPlayer(string aPlayerId);

        /// <summary>
        /// Kills all active
        /// instances of player,
        /// which immediately stops any sound play.
        ///
        /// This method will also stop
        /// all sounds playing in sequence.
        ///
        /// This method won't stop sounds
        /// that are fired using .PlaySound() method.
        /// </summary>
        public void KillPlayers();

        /// <summary>
        /// Plays the specified sound,
        /// if the audio buffer is not
        /// already filled with the exact same sound.
        ///
        /// This specified sound will play,
        /// if buffer is filled with the sound
        /// from another type of player (for example, if
        /// player was created using .PlaySoundInPlayer() method).
        /// </summary>
        /// <param name="aSoundKey">
        /// Key of the targeted sound,
        /// that references an EXISTING
        /// item in the Mono Content Pipeline.
        ///
        /// Recommended convention for keys is: { source/environment/action }
        /// For example: { player/grass/walk }
        /// </param>
        /// <implementation>
        /// - Requires a separate buffer!
        /// - Shall be implemented with Ux00 fillers.
        /// </implementation>
        public void PlaySoundInSequencer(string aSoundKey);
    }
}
