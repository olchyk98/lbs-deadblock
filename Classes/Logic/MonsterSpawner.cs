using System;
using System.Linq;
using Deadblock.Generic;
using Deadblock.Tools;

namespace Deadblock.Engine
{
    public class MonsterSpawner : DeliveredGameSlot
    {
        private int myTicksToSpawn;

        public MonsterSpawner(GameProcess aGame) : base(aGame)
        {
            ResetSpawnCooldown();
        }

        /// <summary>
        /// Resets ticks to spawn value.
        /// Sets to a random number of ticks.
        /// </summary>
        private void ResetSpawnCooldown()
        {
            myTicksToSpawn = NativeUtils.RandomizeValue(100, 400);
        }

        /// <summary>
        /// Randomizes a monster.
        /// </summary>
        private Monster RandomizeMonster()
        {
            var tempParentType = typeof(Monster);
            var tempAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            /////////////////////////

            var tempImplementedTypes = tempAssemblies
              .SelectMany((f) => f.GetTypes())
              .Where((f) => tempParentType.IsAssignableFrom(f) && !f.Equals(tempParentType))
              .ToList();

            /////////////////////////

            var tempMonsterType = NativeUtils.Choice<Type>(tempImplementedTypes);
            var instance = (Monster)Activator.CreateInstance(tempMonsterType, gameInstance);

            //////////////////////

            var tempSpawnPosition = NativeUtils.RandomizeScreenPosition(gameInstance);
            instance.SetPosition(tempSpawnPosition);

            //////////////////////

            return instance;
        }

        /// <summary>
        /// Shall be called on each tick.
        /// Updates internal values and
        /// spawns new monsters when needed.
        /// </summary>
        /// <returns>
        /// Instance of a newly created monster.
        /// May be null if no new monster
        /// needs to be spawned.
        /// </returns>
        public Monster ProcessSpawnTick()
        {
            if (--myTicksToSpawn > 0) return null;

            ResetSpawnCooldown();
            return RandomizeMonster();
        }
    }
}
