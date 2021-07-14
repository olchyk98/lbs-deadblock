using System;

namespace Deadblock
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var tempGame = new GameProcess())
                tempGame.Run();
        }
    }
}
