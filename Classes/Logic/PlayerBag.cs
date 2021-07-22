using Deadblock.Generic;

namespace Deadblock.Logic
{
    public class PlayerBag : DeliveredGameSlot
    {
        public int Trees { get; private set; }

        public PlayerBag(GameProcess aGame) : base(aGame)
        {  }

        /// <summary>
        /// Resets all bag values to zero.
        /// </summary>
        public void ClearValues ()
        {
            ResetTreesCount();
        }

        /// <summary>
        /// Sets trees count to zero.
        /// </summary>
        public int ResetTreesCount () => Trees = 0;

        /// <summary>
        /// Increments a tree to the bag.
        /// </summary>
        public int AddTree () => Trees++;
    }
}
