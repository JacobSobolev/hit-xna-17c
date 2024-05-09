using System;

namespace C17Ex01
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            using (var game = new SpaceInvadersGame())
            {
                game.Run();
            }
        }
    }
#endif
}
