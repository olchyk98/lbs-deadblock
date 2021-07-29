using Deadblock.Generic;

namespace Deadblock.Logic
{
    public class PlayerBag : DeliveredGameSlot
    {
        public int Trees { get; private set; }

        /// <summary>
        /// Values that rerepsents
        /// how much water is left in the bag.
        /// Permitted value is between 0 and 1;
        /// </summary>
        public float WaterFillment { get; private set; } = 1f;

        public UniversalEvent OnWaterExpired;
        public UniversalEvent OnAllTreesCollected;

        private readonly float WaterUsagePerTick = .0002f;
        public readonly int MaxNumberOfTrees = 20;

        public PlayerBag(GameProcess aGame) : base(aGame)
        {
            OnWaterExpired = new UniversalEvent();
            OnAllTreesCollected = new UniversalEvent();

            ResetState();
        }

        /// <summary>
        /// Resets all bag values to zero.
        /// </summary>
        public void ResetState()
        {
            Trees = 0;
            WaterFillment = 1;
        }

        /// <summary>
        /// Increments a tree to the bag.
        /// </summary>
        /// <returns>
        /// Number of trees in the bag.
        /// </returns>
        public int CollectTree()
        {
            ++Trees;

            if (Trees >= MaxNumberOfTrees)
                OnAllTreesCollected.Invoke();

            return Trees;
        }

        /// <summary>
        /// Drinks water, resets timer.
        /// </summary>
        public void DrinkWater() => WaterFillment = 1f;

        /// <summary>
        /// Should be called on each tick.
        /// Refreshes and controls internal bag values.
        /// </summary>
        public void Update()
        {
            WaterFillment -= WaterUsagePerTick;

            if (WaterFillment <= 0f)
                OnWaterExpired.Invoke();
        }
    }
}
