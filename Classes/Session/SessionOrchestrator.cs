using System;
using Deadblock.Generic;
using System.Collections.Generic;

namespace Deadblock.Sessions
{
    public class SessionOrchestrator : DeliveredGameSlot
    {
        private ISession myActiveSession;

        // TODO: Rewrite with State Pattern.
        public MainMenuSession MenuSession { get; private set; }
        public PlaySession PlaySession { get; private set; }

        public SessionOrchestrator(GameProcess aGame) : base(aGame)
        {  }

        /// <summary>
        /// Creates session instances,
        /// and subscribes to their main events.
        /// </summary>
        private void InitializeSessions ()
        {
            MenuSession = new MainMenuSession(gameInstance);
            PlaySession = new PlaySession(gameInstance);

            PlaySession.OnEnd.Subscribe(() => SelectSession(MenuSession));
            MenuSession.OnPlay.Subscribe(() => SelectSession(PlaySession));
            MenuSession.OnQuit.Subscribe(() => gameInstance.Exit());

            foreach(var session in GetAllSessions())
                session.Initialize();
        }

        /// <summary>
        /// Makes instance to switch
        /// to the specified session,
        /// if exists.
        /// </summary>
        /// <param name="aSession">
        /// Targeted session.
        /// </param>
        private void SelectSession (ISession aSession)
        {
            if(aSession == null)
            {
                throw new AggregateException("Tried to setup an invalid session.");
            }

            myActiveSession = aSession;
        }

        /// <summary>
        /// Returns all available sessions
        /// as a list.
        /// </summary>
        private IList<ISession> GetAllSessions ()
        {
            var tempSessions = new List<ISession>();

            tempSessions.Add(MenuSession);
            tempSessions.Add(PlaySession);

            return tempSessions;
        }

        /// <summary>
        /// Draws current session objects,
        /// using exposed session API.
        /// </summary>
        public void Draw()
        {
            myActiveSession.Draw();
        }

        /// <summary>
        /// Updates internalm mechanisms
        /// for current session
        /// </summary>
        public void Update ()
        {
            myActiveSession.Update();
        }

        /// <summary>
        /// Setups the initial
        /// state for the orchestrator.
        /// </summary>
        public void Initialize()
        {
            InitializeSessions();
            SelectSession(MenuSession);
        }
    }
}
