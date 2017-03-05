using BattleConsole.screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        
        public static void SetActiveConsole(States type)
        {
            switch(type)
            {
                case States.TITLE:
                    SadConsole.Engine.ConsoleRenderStack.Clear();
                    SadConsole.Engine.ConsoleRenderStack.Add(titleScreen);
                    titleScreen.Begin();
                    break;
                case States.PLAYING:
                    SadConsole.Engine.ConsoleRenderStack.Clear();
                    SadConsole.Engine.ConsoleRenderStack.Add(playScreen);
                    playScreen.Begin();
                    break;
                case States.QUIT:
                    break;
                default:
                    break;
            }

        }
    }
}
