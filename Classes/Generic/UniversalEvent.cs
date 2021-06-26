using System;
using System.Collections.Generic;

namespace Deadblock.Generic
{
    public class UniversalEvent<T>
    {
        private List<Action<T>> myListeners;

        public UniversalEvent ()
        {
            myListeners = new List<Action<T>>();
        }

        /// <summary>
        /// Calls all listeners
        /// with the passed payload.
        /// </summary>
        /// <param name="payload">
        /// Payload that
        /// will be sent.
        /// </param>
        public void Invoke(T payload)
        {
            foreach (var listener in myListeners)
            {
                listener(payload);
            }
        }

        /// <summary>
        /// Assigns a new listener
        /// to the event.
        /// </summary>
        /// <param name="listener">
        /// New listener that
        /// will be assigned.
        /// </param>
        public void Subscribe(Action<T> listener)
        {
            myListeners.Add(listener);
        }
    }
}
