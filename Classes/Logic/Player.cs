using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Deadblock.Sessions;
using Deadblock.Engine;
using Deadblock.Tools;
using Deadblock.Generic;

namespace Deadblock.Logic
{
    public sealed class Player : RototableEntity
    {
        public PlayerBag Bag { get; private set; }

        public UniversalEvent<GameEndScenario> OnFinish { get; private set; }

        public Player(GameProcess aGame) : base(aGame, "ent/player", 100)
        {
            OnFinish = new UniversalEvent<GameEndScenario>();

            OnDie.Subscribe(() => OnFinish.Invoke(GameEndScenario.PLAYER_LOST_NOHEALTH));

            ConnectInput();
            InitializeBag();
            SetupHeadBars();
            PlaceEntity();

            SetSpeed(3);
            SetStrength(10);
            SetAttackRange(50);
            SetAttackSpeed(500);
        }

        private void InitializeBag()
        {
            Bag = new PlayerBag(gameInstance);

            Bag.OnWaterExpired.Subscribe(() => OnFinish.Invoke(GameEndScenario.PLAYER_LOST_NODRINK));
            Bag.OnAllTreesCollected.Subscribe(() => OnFinish.Invoke(GameEndScenario.PLAYER_WON));
        }

        /// <summary>
        /// Places player on
        /// the default position in the world.
        /// </summary>
        private void PlaceEntity()
        {
            var tempScreenSize = NativeUtils.GetScreenCenterPosition(gameInstance);
            var tempDimensions = Texture.GetDimensions();

            var tempX = tempScreenSize.X - tempDimensions.X / 2;
            var tempY = tempScreenSize.Y - tempDimensions.Y / 2;

            var tempNextPosition = new Vector2(tempX, tempY);
            SetPosition(tempNextPosition);
        }

        /// <summary>
        /// Setups setup active
        /// bars for the player.
        /// </summary>
        private void SetupHeadBars()
        {
            var tempThirstBar = new ThirstBar(gameInstance, this);
            RegisterHeadBar(tempThirstBar);
        }

        /// <summary>
        /// Interacts with all blocks
        /// that are in front of the player.
        /// </summary>
        private void InteractWithEnvironment()
        {
            var currentPosition = Position;
            var forwardPosition = Position + Direction * GameGlobals.SCREEN_BLOCK_SIZE;

            var currentBlocks = gameInstance.World.GetBlocksOnPosition(currentPosition);
            var forwardBlocks = gameInstance.World.GetBlocksOnPosition(forwardPosition);

            var blocks = currentBlocks.Union(forwardBlocks).ToArray();

            foreach (var block in blocks)
            {
                if (!(block is InteractableDynamicBlock)) continue;
                InteractableDynamicBlock interactableBlock = (InteractableDynamicBlock)block;
                interactableBlock.InteractWith(this);
            }
        }

        /// <summary>
        /// Interacts with nearby entities.
        /// </summary>
        /// <returns>
        /// Returns false if there are no
        /// other entities in the interaction range.
        /// </returns>
        private bool InteractWithEntities()
        {
            var nearestMonster = gameInstance.World.GetNearestMonster(Position, AttackRange);
            if (nearestMonster == null) return false;

            AttackEntity(nearestMonster);
            return true;
        }

        /// <summary>
        /// Searches for the nearby entities,
        /// to interact with them.
        /// If there are no other entities,
        /// tries to interact with nearby environment,
        /// such as block and block-like entities.
        /// </summary>
        private void InteractWithWorld()
        {
            if (!InteractWithEntities())
                InteractWithEnvironment();
        }

        /// <summary>
        /// Assigns listeners
        /// to the input handler to
        /// give user the ability
        /// to control the player.
        /// </summary>
        private void ConnectInput()
        {
            Action<Vector2> Move = (direction) => MoveEntity(direction);

            //////////////////////

            gameInstance.InputSystem.OnMoveUp.Subscribe(() => Move(new Vector2(0, -1)));

            gameInstance.InputSystem.OnMoveRight.Subscribe(() => Move(new Vector2(1, 0)));

            gameInstance.InputSystem.OnMoveDown.Subscribe(() => Move(new Vector2(0, 1)));

            gameInstance.InputSystem.OnMoveLeft.Subscribe(() => Move(new Vector2(-1, 0)));

            gameInstance.InputSystem.OnEscape.Subscribe(() => OnFinish.Invoke(GameEndScenario.IMMEDIATE_EXIT));

            //////////////////////

            gameInstance.InputSystem.OnRegularUse.Subscribe(() => InteractWithWorld());
        }

        override public void Update()
        {
            Bag.Update();
            base.Update();
        }

        override public bool AttackEntity(Entity aTarget)
        {
            var couldAttack = base.AttackEntity(aTarget);

            if (couldAttack)
                SoundOrchestrator.PlaySound("player-world-attack");

            return couldAttack;
        }
    }
}
