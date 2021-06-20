using System.Collections.Generic;
using Deadblock;
using Engine;

namespace Logic
{
    public class GameContents : ContentWorker
    {
        private static Dictionary<string, string> myContents = new Dictionary<string, string> {
            {
                "env/main-grass",
                "./Content/Sprites/Environment/Ground/tile006.png"
            },
            {
                "env/green-tree",
                "./Content/Sprites/Environment/Ground/tile006.png"
            },
            {
                "env/dark-tree",
                "./Content/Sprites/Environment/Ground/tile006.png"
            },
            {
                "env/regular-flower",
                "./Content/Sprites/Environment/Ground/tile006.png"
            },
            {
                "env/vertical-gate",
                "./Content/Sprites/Environment/Ground/tile006.png"
            },
            {
                "env/horizontal-gate",
                "./Content/Sprites/Environment/Ground/tile006.png"
            },
            {
                "env/stack-sticks",
                "./Content/Sprites/Environment/Ground/tile006.png"
            }
        };

        public GameContents (GameProcess aGame) : base(aGame, myContents)
        {  }
    }
}
