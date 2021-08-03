using Deadblock.Logic;

namespace Deadblock.Engine
{
    public abstract class Monster : RototableEntity
    {
        public Monster(GameProcess aGame, string aTextureKey, float someHealth) : base(aGame, aTextureKey, someHealth)
        {
            InitializeStats();
        }

        /// <summary>
        /// Sets default stat values.
        /// </summary>
        protected void InitializeStats()
        {
            // Sets default values for monster.
            SetSpeed(2);
            SetStrength(10);
            SetAttackRange(1);
            SetAttackSpeed(1000);
        }

        /// <summary>
        /// Returns targeted player, represented
        /// as an entity.
        /// </summary>
        private Entity GetTargetPlayer()
        {
            return gameInstance.World.MainPlayer;
        }

        /// <summary>
        /// Uses the internal algorithm
        /// to decide what the monster should do next.
        /// </summary>
        private void DoThink()
        {
            var tempTarget = GetTargetPlayer();

            if (!AttackEntity(tempTarget))
                MoveTowards(tempTarget);
        }

        override public void Draw()
        {
            base.Draw();
        }

        override public void Update()
        {
            DoThink();
            base.Update();
        }

        override public bool AttackEntity(Entity aTarget)
        {
            SoundOrchestrator.PlaySoundInSequencer("monster/world/attack");
            return base.AttackEntity(aTarget);
        }
    }
}
