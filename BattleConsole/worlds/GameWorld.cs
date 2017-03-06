using BattleConsole.screens;

namespace BattleConsole.worlds
{
    enum States
    {
        TITLE = 0,
        PLAYING,
        QUIT
    };

    static class GameWorld
    {
        public static TitleScreen titleScreen;
        public static PlayScreen playScreen;

        /// <summary>
        /// Called one time to initiate everything. Assumes SadConsole has been setup and is ready to go.
        /// </summary>
        public static void Start()
        {
            titleScreen = new TitleScreen();
            playScreen = new PlayScreen();

            SetActiveConsole(States.TITLE);

        }
        
        // A high level state switcher to run the appropriate Console depending upon state
        public static void SetActiveConsole(States type)
        {
            switch(type)
            {
                case States.TITLE:
                case States.QUIT:
                    SadConsole.Engine.MonoGameInstance.IsMouseVisible = false;
                    SadConsole.Engine.ConsoleRenderStack.Clear();
                    SadConsole.Engine.ConsoleRenderStack.Add(titleScreen);
                    titleScreen.Begin();
                    break;
                case States.PLAYING:
                    SadConsole.Engine.ConsoleRenderStack.Clear();
                    SadConsole.Engine.ConsoleRenderStack.Add(playScreen);
                    playScreen.Begin();
                    break;
                default:
                    break;
            }

        }
    }
}
