using System;
using System.Collections.Generic;
using System.Linq;
using Deadblock.Generic;
using Deadblock.Tools;

namespace Deadblock.Engine
{
    public class MonstersSpawner : DeliveredGameSlot
    {
        private int myTicksToSpawn;

        public MonstersSpawner(GameProcess aGame) : base(aGame)
        {
            ResetSpawnCooldown();
        }

        /// <summary>
        /// Resets ticks to spawn value.
        /// Sets to a random number of ticks.
        /// </summary>
        private void ResetSpawnCooldown()
        {
            myTicksToSpawn = NativeUtils.RandomizeValue(50, 400);
        }

        /// <summary>
        /// Randomizes a monster.
        /// </summary>
        private Monster RandomizeMonster()
        {
            var tempParentType = typeof(Monster);
            var tempAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            var tempImplementedTypes = tempAssemblies.SelectMany((f) => f.GetTypes()).Where((f) => tempParentType.IsAssignableFrom(f) && !f.Equals(tempParentType)).ToList();

            var tempMonsterType = NativeUtils.Choice<Type>(tempImplementedTypes);
            return (Monster)Activator.CreateInstance(tempMonsterType, gameInstance);
        }

        /// <summary>
        /// Randomizes an array of monsters
        /// and returns it.
        /// </summary>
        private Monster[] RandomizeMonsters()
        {
            var tempMonsters = new List<Monster>();
            var tempNOfMonsters = NativeUtils.RandomizeValue(3, 10);

            for (var ma = 0; ma < tempNOfMonsters; ++ma)
            {
                var tempMonster = RandomizeMonster();
                tempMonsters.Add(tempMonster);
            }

            return tempMonsters.ToArray();
        }

        /// <summary>
        /// Shall be called on each tick.
        /// Updates internal values and
        /// spawns new monsters when needed.
        /// </summary>
        /// <returns>
        /// Array of newly created monsters.
        /// May be empty if no new monsters
        /// were created on a tick.
        /// </returns>
        public Monster[] ProcessSpawnTick()
        {
            if (--myTicksToSpawn <= 0)
            {
                ResetSpawnCooldown();
                return RandomizeMonsters();
            }

            return new Monster[] { };
        }
    }
}
