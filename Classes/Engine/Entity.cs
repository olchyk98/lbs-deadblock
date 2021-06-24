using System;
using Microsoft.Xna.Framework;
using Deadblock.Generic;
using Deadblock.Tools;

namespace Deadblock.Engine
{
    public abstract class Entity : DeliveredGameSlot
    {
        public Vector2 Position { get; private set; }
        public float Health { get; private set; }
        public float MaxHealth { get; }
        public bool isActive { get; private set; }

        public Entity(GameProcess aGame, float someMaxHealth, float someHealth = default) : base(aGame)
        {
            Health = (someHealth != default) ? someHealth : someMaxHealth;
            MaxHealth = someMaxHealth;
            Position = new Vector2(0, 0);
            isActive = true;
        }

        /// <summary>
        /// Sets health to fullhealth.
        /// </summary>
        private void SetFullHealth ()
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
        private void ApplyDamage (float someDamage)
        {
            if(someDamage < 0)
            {
                throw new AggregateException("Applied damage cannot be less than 0. Contact DEV.");
            }

            Health -= someDamage;
        }

        /// <summary>
        /// Restores health with the
        /// specified amount of health points.
        /// </summary>
        /// <param name="someHealth">
        /// Number of health points that are
        /// going to be added.
        /// </param>
        private void RestoreHealth (float someHealth)
        {
            if(someHealth < 0)
            {
                throw new AggregateException("Cannot add number of health points if the value is less than 0. Contact DEV.");
            }

            Health += someHealth;
        }

        /// <summary>
        /// Moves entity with the specified force.
        /// </summary>
        /// <param name="aForce">
        /// Targeted force.
        /// </param>
        /// <returns>
        /// New Position of the entity.
        /// </returns>
        public Vector2 MoveEntity (Vector2 aForce)
        {
            var tempNextPosition = Position += aForce;

            NativeUtils.ValidateIfOutOfScreen(gameInstance, tempNextPosition);

            return tempNextPosition;
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
        public Vector2 SetPosition (Vector2 aPosition)
        {
            NativeUtils.ValidateIfOutOfScreen(gameInstance, aPosition);

            Position = aPosition;
            return aPosition;
        }
    }
}
