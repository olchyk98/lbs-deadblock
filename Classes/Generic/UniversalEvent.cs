using System;
using System.Collections.Generic;

namespace Deadblock.Generic
{
    public interface IUniversalEvent
    {
        /// <summary>
        /// Calls all listeners
        /// with the passed payload.
        /// </summary>
        public void Invoke();

        /// <summary>
        /// Assigns a new listener
        /// to the event.
        /// </summary>
        /// <param name="listener">
        /// New listener that
        /// will be assigned.
        /// </param>
        public void Subscribe(Action listener);
    }

    public interface IUniversalEvent<T>
    {
        /// <summary>
        /// Calls all listeners
        /// with the passed payload.
        /// </summary>
        /// <param name="payload">
        /// Payload that
        /// will be sent.
        /// </param>
        public void Invoke(T payload);

        /// <summary>
        /// Assigns a new listener
        /// to the event.
        /// </summary>
        /// <param name="listener">
        /// New listener that
        /// will be assigned.
        /// </param>
        public void Subscribe(Action<T> listener);
    }

    public class UniversalEvent : IUniversalEvent
    {
        private List<Action> myListeners;

        public UniversalEvent()
        {
            myListeners = new List<Action>();
        }

        public void Invoke()
        {
            foreach (var listener in myListeners)
                listener();
        }

        public void Subscribe(Action listener) => myListeners.Add(listener);
    }

    public class UniversalEvent<T> : IUniversalEvent<T>
    {
        private List<Action<T>> myListeners;

        public UniversalEvent()
        {
            myListeners = new List<Action<T>>();
        }

        public void Invoke(T payload)
        {
            foreach (var listener in myListeners)
                listener(payload);
        }

        public void Subscribe(Action<T> listener) => myListeners.Add(listener);
    }
}
