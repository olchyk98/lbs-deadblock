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
        /// <param name="aListener">
        /// New listener that
        /// will be assigned.
        /// </param>
        public void Subscribe(Action aListener);

        /// <summary>
        /// Unassigns the passed listener
        /// from the event.
        /// </summary>
        /// <param name="aListener">
        /// Targeted listener,
        /// that needs to be unassigned.
        /// </param>
        public void Unsubscribe(Action aListener);
    }

    public interface IUniversalEvent<T>
    {
        /// <summary>
        /// Calls all listeners
        /// with the passed payload.
        /// </summary>
        /// <param name="aPayload">
        /// Payload that
        /// will be sent.
        /// </param>
        public void Invoke(T aPayload);

        /// <summary>
        /// Assigns a new listener
        /// to the event.
        /// </summary>
        /// <param name="aListener">
        /// New listener that
        /// will be assigned.
        /// </param>
        public void Subscribe(Action<T> aListener);

        /// <summary>
        /// Unassigns the passed listener
        /// from the event.
        /// </summary>
        /// <param name="aListener">
        /// Targeted listener,
        /// that needs to be unassigned.
        /// </param>
        public void Unsubscribe(Action<T> aListener);
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

        public void Subscribe(Action aListener) => myListeners.Add(aListener);

        public void Unsubscribe(Action aListener) => myListeners.Remove(aListener);
    }

    public class UniversalEvent<T> : IUniversalEvent<T>
    {
        private List<Action<T>> myListeners;

        public UniversalEvent()
        {
            myListeners = new List<Action<T>>();
        }

        public void Invoke(T aPayload)
        {
            foreach (var listener in myListeners)
                listener(aPayload);
        }

        public void Subscribe(Action<T> aListener) => myListeners.Add(aListener);

        public void Unsubscribe(Action<T> aListener) => myListeners.Remove(aListener);
    }
}
