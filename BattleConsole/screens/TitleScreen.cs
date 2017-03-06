﻿using Microsoft.Xna.Framework;
using SadConsole.Consoles;
using Console = SadConsole.Consoles.Console;
using BattleConsole.consoles;
using SadConsole.Input;
using BattleConsole.worlds;
using BattleConsole.modals;

namespace BattleConsole.screens
{
    class TitleScreen : ConsoleList
    {
        public TitleConsole titleConsole;

        public TitleScreen()
        {

            titleConsole = new consoles.TitleConsole(80, 25);

            Add(titleConsole);
           
        }

        public void Begin()
        {
            // allow this console to receive input events
            SadConsole.Engine.ActiveConsole = this;

            // clear the console
            titleConsole.Clear();

            // display a primitive title screen and main menu
            titleConsole.Print(26, 6, "  BattleConsole  v" + Constants.APP_VERSION);
            titleConsole.Print(30, 7, " -=-=-=-=-=-=-= ");
            titleConsole.Print(30, 8, " [1] 1 player game ");
            titleConsole.Print(30, 9, " [2] 2 player game ");
            titleConsole.Print(30, 10, " [Q] Quit ");
            titleConsole.Print(15, 20, " -- copyright (c) 2017 by Wazoo Enterprises Inc. --");

            this.Render();

        }

        // TODO perform any cleanup tasks for this state
        public void End()
        {
        }

        public override bool ProcessKeyboard(KeyboardInfo info)
        {
            if (info.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Q) || 
                (info.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape)))
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
