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
using Microsoft.Xna.Framework.Input;
using SadConsole.Game;
using SadConsole;
using BattleConsole.modals;
using BattleConsole.worlds;

namespace BattleConsole.screens
{
    class PlayScreen : ConsoleList
    {
        public MessagesConsole messageConsole;
        public PlayerFleetConsole player1Console;
        public PlayerFleetConsole player2Console;
        public int numberOfActivePlayers = 1; // either 1 or 2
        public int currentlyActivePlayer = 1; // either 1 or 3

        public bool isGameOver = false;
        public bool isPlayer1Winner = false;

        private Console messageHeaderConsole;
        private Console player1HeaderConsole;
        private Console player2HeaderConsole;

        public GameObject mouseCursor;

        public PlayScreen()
        {

            
            player1Console = new PlayerFleetConsole(24, 12);
            player1Console.Position = new Point(5, 5);

            player2Console = new PlayerFleetConsole(24, 12);
            player2Console.Position = new Point(50, 5);

            // Draw the line for the header
            player1HeaderConsole = new Console(24, 1);
            player1HeaderConsole.Position = new Point(5, 4);
            player1HeaderConsole.DoUpdate = false;
            player1HeaderConsole.CanUseKeyboard = false;
            player1HeaderConsole.CanUseMouse = false;
            player1HeaderConsole.Fill(Color.White, ColorAnsi.Blue, 196, null);
            //player1HeaderConsole.SetGlyph(56, 0, 193); // This makes the border match the character console's left-edge border
            player1HeaderConsole.Print(0, 0, " PLAYER 1 ");

            // Draw the line for the header
            player2HeaderConsole = new Console(24, 1);
            player2HeaderConsole.Position = new Point(50, 4);
            player2HeaderConsole.DoUpdate = false;
            player2HeaderConsole.CanUseKeyboard = false;
            player2HeaderConsole.CanUseMouse = false;
            player2HeaderConsole.Fill(Color.White, ColorAnsi.Blue, 196, null);
            //player2HeaderConsole.SetGlyph(56, 0, 193); // This makes the border match the character console's left-edge border
            player2HeaderConsole.Print(0, 0, " PLAYER 2 ");



            messageConsole = new MessagesConsole(60, 6);
            messageConsole.Position = new Point(5, 20);

            // Setup the message header to be as wide as the screen but only 1 character high
            messageHeaderConsole = new Console(60, 1);
            messageHeaderConsole.Position = new Point(5, 19);
            messageHeaderConsole.DoUpdate = false;
            messageHeaderConsole.CanUseKeyboard = false;
            messageHeaderConsole.CanUseMouse = false;

            // Draw the line for the header
            messageHeaderConsole.Fill(Color.White, Color.Black, 196, null);
            messageHeaderConsole.SetGlyph(56, 0, 193); // This makes the border match the character console's left-edge border
            messageHeaderConsole.Print(2, 0, " Messages ");


            Add(player1Console);
            Add(player2Console);
            Add(player1HeaderConsole);
            Add(player2HeaderConsole);

            Add(messageConsole);
            Add(messageHeaderConsole);

            this.CanUseMouse = true;

            AnimatedTextSurface mouseAnimation = new AnimatedTextSurface("default", 1, 1, Engine.DefaultFont);
            mouseAnimation.CreateFrame();
            mouseAnimation.CurrentFrame[0].Foreground = ColorAnsi.BlueBright;
            mouseAnimation.CurrentFrame[0].GlyphIndex = 178;


            this.mouseCursor = new GameObject();
            this.mouseCursor.Animation = mouseAnimation;


        }

        public void Begin()
        {
            SadConsole.Engine.ActiveConsole = this;

            player1Console.SpawnEntities();
            player2Console.SpawnEntities();

            messageConsole.Clear();
            messageConsole.PrintMessage("player 1's turn");

            if (this.numberOfActivePlayers == 1)
            {
                player2HeaderConsole.Clear();
                player2HeaderConsole.Fill(Color.White, ColorAnsi.Blue, 196, null);
                player2HeaderConsole.Print(0, 0, " CPU ");
            }

            this.isGameOver = false;
            this.isPlayer1Winner = false;

           
        }

        public void End()
        {
            
        }

        public override bool ProcessMouse(MouseInfo info)
        {

            bool keyHit = false;

            this.mouseCursor.Position = info.WorldLocation;

            if (info.LeftClicked)
            {
                // if we have a valid click then run our collision detection,
                // update the board and change players
                var testPos = info.WorldLocation;
                //System.Console.WriteLine("world location {0}:", testPos);

                // bounds check of mouse in player1 console window
                if ((5 <= testPos.X && testPos.X <= 28) && (5 <= testPos.Y && testPos.Y <= 16))
                {
                    //System.Console.WriteLine("player1 window");
                    if (player1Console.CollisionCheckEntities(testPos))
                    {
                        messageConsole.PrintMessage("Hit!");
                    } else
                    {
                        messageConsole.PrintMessage("Miss!");
                    }

                    // now check if any entities are left standing. We only care
                    // about
                    if (player1Console.AreAllEntitiesDebris())
                    {
                        // game is over!
                        messageConsole.PrintMessage("Player1's fleet is sunk!");
                        this.isGameOver = true;
                    }


                }

                // bounds check of mouse in player2 console window
                if ((50 <= testPos.X && testPos.X <= 73) && (5 <= testPos.Y && testPos.Y <= 16))
                {
                    //System.Console.WriteLine("player2 window");
                    if (player2Console.CollisionCheckEntities(testPos))
                    {
                        messageConsole.PrintMessage("Hit!");
                    }
                    else
                    {
                        messageConsole.PrintMessage("Miss!");
                    }

                    // now check if any entities are left standing. We only care
                    // about
                    if (player2Console.AreAllEntitiesDebris())
                    {
                        // game is over!
                        messageConsole.PrintMessage("Player2's fleet is sunk!");
                        this.isGameOver = true;
                        this.isPlayer1Winner = true;
                    }
                }

                if (this.isGameOver)
                {
                    var popup = new GameOverModal(35, 7);
                    popup.Center();
                    if (this.isPlayer1Winner)
                    {
                        popup.Title = "YOU WIN!";

                    } else
                    {
                        popup.Title = "YOU LOSE!";
                    }

                    popup.okButton.Click += (btn, args) =>
                    {
                        popup.Hide();
                        GameWorld.SetActiveConsole(States.TITLE);
                    };

                    popup.Show(true);

                }


            }

            return keyHit || base.ProcessMouse(info);
        }

        public override bool ProcessKeyboard(KeyboardInfo info)
        {
            bool keyHit = false;

            if (info.KeysPressed.Contains(AsciiKey.Get(Keys.Escape)))
            {
                var popup = new GameQuitModal(30, 7);
                popup.Center();
                popup.okButton.Click += (btn, args) =>
                {
                    popup.Hide();
                    GameWorld.SetActiveConsole(States.TITLE);
                };

                popup.cancelButton.Click += (btn, args) =>
                {
                    popup.Hide();
                };

                popup.Show(true);
            }

            return keyHit || base.ProcessKeyboard(info);
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Render()
        {
            base.Render();

            mouseCursor.Render();
        }

    }
}
