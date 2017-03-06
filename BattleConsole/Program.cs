using System;
using Console = SadConsole.Consoles.Console;
using BattleConsole.worlds;

namespace BattleConsole
{
    class Program
    {
        static void Main(string[] args)
        {
         
            // Setup the engine and creat the main window.
            SadConsole.Engine.Initialize("IBM.font", 80, 25);

            // Hook the start event so we can add consoles to the system.
            SadConsole.Engine.EngineStart += Engine_EngineStart;

            // Hook the update event that happens each frame so we can trap keys and respond.
            SadConsole.Engine.EngineUpdated += Engine_EngineUpdated;

            SadConsole.Engine.ProcessMouseWhenOffScreen = false;

            // Start the game.
            SadConsole.Engine.Run();
        }

        private static void Engine_EngineStart(object sender, EventArgs e)
        {

            // TODO set our title bar...somehow?
            ///SadConsole.Engine.??

            // Clear the default console
            SadConsole.Engine.ConsoleRenderStack.Clear();
            SadConsole.Engine.UseKeyboard = true;
            SadConsole.Engine.UseMouse = true;
            SadConsole.Engine.ActiveConsole = null;

            SadConsole.Engine.Keyboard.RepeatDelay = 0.07f;
            SadConsole.Engine.Keyboard.InitialRepeatDelay = 0.1f;

            GameWorld.Start();
        }

        private static void Engine_EngineUpdated(object sender, EventArgs e)
        {
            // TODO not sure we need anything here
        }
    }
}
