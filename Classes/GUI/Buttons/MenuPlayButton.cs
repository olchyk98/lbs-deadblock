using Microsoft.Xna.Framework;

namespace Deadblock.GUI
{
    public class MenuPlayButton : GUIButton
    {
        public MenuPlayButton (GameProcess aGame, Vector2 aPosition, string aTextureKey) : base(aGame, aPosition, aTextureKey)
        {
            OnClick.Subscribe(HandleStartGame);
        }

        private void HandleStartGame()
        {

        }
    }
}
