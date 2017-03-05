using Microsoft.Xna.Framework;
using SadConsole.Consoles;
using System;
using Console = SadConsole.Consoles.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleConsole.consoles;
using SadConsole.Input;
using BattleConsole.worlds;
using BattleConsole.modals;

namespace BattleConsole.screens
{
    class TitleScreen : ConsoleList
    {
        //public Console ViewConsole;
        // public CharacterStatusPanel StatsConsole;
        // public MessagesConsole MessageConsole;

        //private Console messageHeaderConsole;
        public TitleConsole titleConsole;

        public TitleScreen()
        {

            titleConsole = new consoles.TitleConsole(80, 25);

            // Keyboard setup
            

            SadConsole.Engine.ActiveConsole = this;

            Add(titleConsole);
           
        }

        public void Begin()
        {
            titleConsole.Clear();

            titleConsole.Print(30, 6, "  BattleConsole  ");
            titleConsole.Print(30, 7, " -=-=-=-=-=-=-= ");
            titleConsole.Print(30, 8, " [1] 1 player game ");
            titleConsole.Print(30, 9, " [2] 2 player game ");
            titleConsole.Print(30, 10, " [Q] Quit ");
            titleConsole.Print(15, 20, " -- copyright (c) 2017 by Wazoo Enterprises Inc. --");

            this.Render();

        }

        public override bool ProcessKeyboard(KeyboardInfo info)
        {
            if (info.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Q))
            {

                var popup = new GameQuitModal(30, 7);
                popup.Center();
                popup.okButton.Click += (btn, args) =>
                {
                    System.Console.WriteLine("quit game");
                    popup.Hide();
                    System.Environment.Exit(0);
                };

                popup.cancelButton.Click += (btn, args) =>
                {
                    System.Console.WriteLine("cancel quit");
                    popup.Hide();
                };

                popup.Show(true);
            }

            if (info.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.NumPad1))
            {
                // kick off player vs. cpu
                System.Console.WriteLine("player vs. cpu");
                GameWorld.playScreen.numberOfActivePlayers = 1;
                GameWorld.SetActiveConsole(States.PLAYING);
            }

            if (info.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.NumPad2))
            {
                // kick off player vs. player
                System.Console.WriteLine("player vs. player");
                GameWorld.playScreen.numberOfActivePlayers = 2;
                GameWorld.SetActiveConsole(States.PLAYING);
            }

            return true;
        }


    }
}
