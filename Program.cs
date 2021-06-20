using System;

namespace Deadblock
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new GameProcess())
                game.Run();
        }
    }
}
