using Deadblock.Generic;

namespace Deadblock.Logic
{
    public class PlayerBag : DeliveredGameSlot
    {
        public int Trees { get; private set; }

        public PlayerBag(GameProcess aGame) : base(aGame)
        {
            ResetState();
        }

        /// <summary>
        /// Resets all bag values to zero.
        /// </summary>
        public void ResetState()
        {
            Trees = 0;
        }

        /// <summary>
        /// Increments a tree to the bag.
        /// </summary>
        public int CollectTree() => Trees++;
    }
}
