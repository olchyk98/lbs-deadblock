using System;
using Microsoft.Xna.Framework;
using Deadblock.Generic;
using Deadblock.Tools;

namespace Deadblock.Engine
{
    public abstract class Entity : DeliveredGameSlot
    {
        public Vector2 Position { get; private set; }
        public Vector2 Direction { get; private set; }

        public int Speed { get; private set; }
        public int Strength { get; private set; }
        public int AttackRange { get; private set; }
        public int AttackSpeed { get; private set; } // ms

        private long myLastAttackTime = 0;

        public float Health { get; private set; }
        public float MaxHealth { get; }

        public UniversalEvent OnDie { get; private set; }
        internal SoundOrchestrator SoundOrchestrator;

        public Entity(GameProcess aGame, float someMaxHealth, float someHealth = default) : base(aGame)
        {
            Health = (someHealth != default) ? someHealth : someMaxHealth;
            MaxHealth = someMaxHealth;
            Position = new Vector2(0, 0);
            Direction = new Vector2(0, -1);

            Speed = 0;
            Strength = 0;
            AttackRange = 0;
            AttackSpeed = 0;

            OnDie = new UniversalEvent();
            SoundOrchestrator = new SoundOrchestrator(aGame);
        }

        /// <summary>
        /// Sets health to fullhealth.
        /// </summary>
        private void ResetHealth()
        {
            Health = MaxHealth;
        }

        /// <summary>
        /// Applies damage to the entity,
        /// by reducing its health.
        /// </summary>
        /// <param name="someDamage">
        /// Number of health points
        /// that health will be reduced with.
        /// </param>
        virtual public void ApplyDamage(float someDamage)
        {
            if (someDamage < 0)
            {
                throw new AggregateException("Applied damage cannot be less than 0. Contact DEV.");
            }

            Health -= someDamage;

            if (Health < 0)
            {
                OnDie.Invoke();
            }
        }

        /// <summary>
        /// Restores health with the
        /// specified amount of health points.
        /// </summary>
        /// <param name="someHealth">
        /// Number of health points that are
        /// going to be added.
        /// </param>
        private void RestoreHealth(float someHealth)
        {
            if (someHealth < 0)
            {
                throw new AggregateException("Cannot add number of health points if the value is less than 0. Contact DEV.");
            }

            Health += someHealth;
        }

        /// <summary>
        /// Moves entity with the specified force.
        /// Also rotates entity to the specified direction,
        /// inherited from the force value.
        /// </summary>
        /// <param name="aDirection">
        /// Targeted direction.
        /// </param>
        /// <returns>
        /// New Position of the entity.
        /// </returns>
        public Vector2 MoveEntity(Vector2 aDirection)
        {
            Direction = aDirection;

            //////////////////

            var tempNextPosition = Position + aDirection * Speed;

            //////////////////

            var tempIsOnScreen = NativeUtils.IsPointOnCanvas(gameInstance, tempNextPosition);
            var tempIsBlocked = EngineUtils.IsTouchingCollider(gameInstance, tempNextPosition);

            //////////////////

            if (!tempIsOnScreen || tempIsBlocked)
                return Position;

            //////////////////

            SoundOrchestrator.PlaySoundInSequencer("entity/ground/walk");
            return SetPosition(tempNextPosition);
        }

        /// <summary>
        /// Sets position of the entity
        /// to the specified one.
        /// Validates if specified position is
        /// not out-of-boundaries and throws
        /// an error if it is.
        /// </summary>
        /// <param name="aPosition">
        /// Targeted Position.
        /// </param>
        /// <returns>
        /// Specified Position.
        /// </returns>
        /// <throws>
        /// AggregateException, if the position
        /// is off screen.
        /// </throws>
        public Vector2 SetPosition(Vector2 aPosition)
        {
            var isOfScreen = NativeUtils.IsPointOnCanvas(gameInstance, aPosition);

            if (!isOfScreen)
            {
                throw new AggregateException("The specified position is out of the screen. Contact DEV.");
            }

            Position = aPosition;
            return aPosition;
        }

        /// <summary>
        /// Sets speed for
        /// the entity
        /// </summary>
        /// <param name="someSpeed">
        /// Speed value.
        /// </param>
        /// <returns>
        /// New speed for the entity.
        /// </returns>
        public int SetSpeed(int someSpeed)
        {
            Speed = someSpeed;
            return Speed;
        }

        /// <summary>
        /// Sets strength
        /// for the tntiy
        /// </summary>
        /// <param name="someStrength">
        /// Strength value.
        /// </param>
        /// <returns>
        /// New strength for the entity.
        /// </returns>
        public int SetStrength(int someStrength)
        {
            Strength = someStrength;
            return Strength;
        }

        /// <summary>
        /// Sets attack range
        /// for the entity.
        /// </summary>
        /// <param name="someAttackRange">
        /// Attack range value.
        /// </param>
        /// <returns>
        /// New attack range value
        /// for the entity.
        /// </returns>
        public int SetAttackRange(int someAttackRange)
        {
            AttackRange = someAttackRange;
            return someAttackRange;
        }

        /// <summary>
        /// Sets attack speed
        /// for the entity.
        /// </summary>
        /// <param name="someAttackRange">
        /// Attack speed value.
        /// </param>
        /// <returns>
        /// New attack speed value
        /// for the entity.
        /// </returns>
        public int SetAttackSpeed(int someAttackSpeed)
        {
            AttackSpeed = someAttackSpeed;
            return someAttackSpeed;
        }

        /// <summary>
        /// Sets direction to the target
        /// entity, and moves to it.
        /// </summary>
        /// <param name="aTarget">
        /// Entity that needs to be targeted.
        /// </param>
        public void MoveTowards(Entity aTarget)
        {
            var tempDirection = new Vector2(0, 0);
            var tempTargetPosition = aTarget.Position;

            if (tempTargetPosition.X != Position.X)
                tempDirection.X = NativeUtils.ParseBooleanToDirection(tempTargetPosition.X > Position.X);

            if (tempTargetPosition.Y != Position.Y)
                tempDirection.Y = NativeUtils.ParseBooleanToDirection(tempTargetPosition.Y > Position.Y);

            MoveEntity(tempDirection);
        }

        /// <summary>
        /// Attacks the specified target entity.
        /// </summary>
        /// <returns>
        /// True, if monster
        /// is in the attack range.
        /// It will return true,
        /// even if the entity didn't actually attack,
        /// for example, if there's still attack cooldown active.
        /// </returns>
        virtual public bool AttackEntity(Entity aTarget)
        {
            var tempDistanceToTarget = (int)Vector2.Distance(Position, aTarget.Position);
            var tempCurrentTime = NativeUtils.GetTime();

            // Guards
            if (tempDistanceToTarget > AttackRange)
                return false;

            if (tempCurrentTime <= myLastAttackTime + AttackSpeed)
                return true;

            // Execution
            aTarget.ApplyDamage(Strength);
            myLastAttackTime = tempCurrentTime;

            return true;
        }
    }
}
