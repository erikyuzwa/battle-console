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

        public bool isGameOver = false;
        public bool isPlayer1Winner = false;
        public bool isPlayer1Active = false;

        private Console messageHeaderConsole;
        private Console player1HeaderConsole;
        private Console player2HeaderConsole;

        public GameObject mouseCursor;

        public PlayScreen()
        {

            player1Console = new PlayerFleetConsole(24, 12);
            player1Console.Position = new Point(5, 5);

            player2Console = new PlayerFleetConsole(24, 12);
            player2Console.Position = new Point(40, 5);

            // Draw the line for the header
            player1HeaderConsole = new Console(24, 1);
            player1HeaderConsole.Position = new Point(5, 4);
            player1HeaderConsole.DoUpdate = false;
            player1HeaderConsole.CanUseKeyboard = false;
            player1HeaderConsole.CanUseMouse = false;
            player1HeaderConsole.Fill(Color.White, ColorAnsi.Blue, 196, null);
            player1HeaderConsole.Print(2, 0, " PLAYER 1 ");

            // Draw the line for the header
            player2HeaderConsole = new Console(24, 1);
            player2HeaderConsole.Position = new Point(40, 4);
            player2HeaderConsole.DoUpdate = false;
            player2HeaderConsole.CanUseKeyboard = false;
            player2HeaderConsole.CanUseMouse = false;
            player2HeaderConsole.Fill(Color.White, ColorAnsi.Blue, 196, null);
            player2HeaderConsole.Print(2, 0, " PLAYER 2 ");


            messageConsole = new MessagesConsole(60, 6);
            messageConsole.Position = new Point(5, 19);

            // Setup the message header to be as wide as the screen but only 1 character high
            
            messageHeaderConsole = new Console(60, 1);
            messageHeaderConsole.Position = new Point(5, 18);
            messageHeaderConsole.DoUpdate = false;
            messageHeaderConsole.CanUseKeyboard = false;
            messageHeaderConsole.CanUseMouse = false;

            // Draw the line for the header
            messageHeaderConsole.Fill(Color.White, ColorAnsi.Blue, 196, null);
            messageHeaderConsole.Print(2, 0, " Messages ");

            // add our child console(s) to this container
            Add(player1Console);
            Add(player2Console);
            Add(player1HeaderConsole);
            Add(player2HeaderConsole);
            Add(messageConsole);
            Add(messageHeaderConsole);

            // creating a mouse cursor that we use as a guide for plotting the
            // shots is done by enabling the mouse for this parent container,
            // and creating a GameObject to update as we process the mouse.
            // Note that we're NOT adding this GameObject to our container
            // entity list.
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
            // enable this console to receive input events
            SadConsole.Engine.ActiveConsole = this;

            // spawn the entities in our respective boards
            player1Console.SpawnEntities();
            player2Console.SpawnEntities();

            // clear our messages -- player 1 always goes first
            this.isPlayer1Active = true;
            messageConsole.ClearMessages();
            messageConsole.PrintMessage("[Player 1]");

            this.isGameOver = false;
            this.isPlayer1Winner = false;

            this.HighlightFleetConsole();
            
        }

        // TODO perform any cleanup tasks for this state
        public void End()
        {         
        }

        public void HighlightFleetConsole()
        {
            if (this.isPlayer1Active)
            {
                player1HeaderConsole.Fill(Color.Black, Color.Yellow, 196, null);
                player1HeaderConsole.Print(2, 0, " PLAYER 1 ");

                player2HeaderConsole.Fill(Color.White, ColorAnsi.Blue, 196, null);
                player2HeaderConsole.Print(2, 0, " PLAYER 2 ");


            } else
            {
                player2HeaderConsole.Fill(Color.Black, Color.Yellow, 196, null);
                player2HeaderConsole.Print(2, 0, " PLAYER 2 ");

                player1HeaderConsole.Fill(Color.White, ColorAnsi.Blue, 196, null);
                player1HeaderConsole.Print(2, 0, " PLAYER 1 ");
            }

        }

        public override bool ProcessMouse(MouseInfo info)
        {

            bool keyHit = false;
            bool turnTaken = false;

            this.mouseCursor.Position = info.WorldLocation;



            if (info.LeftClicked)
            {
                // if we have a valid click then run our collision detection,
                // update the board and change players
                var testPos = info.WorldLocation;

                // bounds check of mouse in player1 console window
                // only allow clicks from player2 in this container
                if ((5 <= testPos.X && testPos.X <= 28) && (5 <= testPos.Y && testPos.Y <= 16) && !this.isPlayer1Active)
                {
                    if (player1Console.CollisionCheckEntities(testPos))
                    {
                        //messageConsole.PrintMessage("Hit!");
                        messageConsole.AppendMessage(" Direct Hit!");
                    } else
                    {
                        //messageConsole.PrintMessage("Miss!");
                        messageConsole.AppendMessage(" Miss!");
                    }

                    // now check if any entities are left standing. We only care
                    // about
                    if (player1Console.AreAllEntitiesDebris())
                    {
                        // game is over!
                        messageConsole.PrintMessage("Player1's fleet is sunk!");
                        this.isGameOver = true;
                    }

                    turnTaken = true;
                }

                // bounds check of mouse in player2 console window
                // only allow clicks from player1 in this container
                if ((40 <= testPos.X && testPos.X <= 63) && (5 <= testPos.Y && testPos.Y <= 16) && this.isPlayer1Active)
                {
                    if (player2Console.CollisionCheckEntities(testPos))
                    {
                        messageConsole.AppendMessage(" Direct Hit!");
                    }
                    else
                    {
                        messageConsole.AppendMessage(" Miss!");
                    }

                    // now check if any entities are left standing. We only care about
                    if (player2Console.AreAllEntitiesDebris())
                    {
                        // game is over!
                        messageConsole.PrintMessage("Player2's fleet is sunk!");
                        this.isGameOver = true;
                        this.isPlayer1Winner = true;
                    }

                    turnTaken = true;
                }

                // if our game-over condition is set then display our modal dialog
                if (this.isGameOver)
                {
                    SadConsole.Engine.MonoGameInstance.IsMouseVisible = true;
                    var popup = new GameOverModal(35, 7, this.isPlayer1Winner);
                    popup.Center();

                    popup.okButton.Click += (btn, args) =>
                    {
                        popup.Hide();
                        GameWorld.SetActiveConsole(States.TITLE);
                    };

                    popup.Show(true);

                }


                // if we made it this far without a win/lose condition then switch players
                if (turnTaken)
                {
                    this.isPlayer1Active = !this.isPlayer1Active;
                    this.HighlightFleetConsole();
                    if (this.isPlayer1Active)
                    {
                        messageConsole.PrintMessage("[Player 1]");
                    } else
                    {
                        messageConsole.PrintMessage("[Player 2]");
                    }
                    
                }
                
            }

            return keyHit || base.ProcessMouse(info);
        }

        public override bool ProcessKeyboard(KeyboardInfo info)
        {
            bool keyHit = false;

            if (info.KeysPressed.Contains(AsciiKey.Get(Keys.Escape)))
            {
                SadConsole.Engine.MonoGameInstance.IsMouseVisible = true;

                var popup = new GameQuitModal(30, 7);
                popup.Center();
                popup.okButton.Click += (btn, args) =>
                {
                    popup.Hide();
                    GameWorld.SetActiveConsole(States.TITLE);
                };

                popup.cancelButton.Click += (btn, args) =>
                {
                    SadConsole.Engine.MonoGameInstance.IsMouseVisible = false;
                    popup.Hide();
                };

                popup.Show(true);
            }

            return keyHit || base.ProcessKeyboard(info);
        }

        // our mouse cursor isn't added to our console the same way our other
        // objects are, so we need to override Render() to make sure we
        // call it
        public override void Render()
        {
            base.Render();

            mouseCursor.Render();
        }

    }
}
