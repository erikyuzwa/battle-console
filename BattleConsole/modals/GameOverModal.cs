﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleConsole.modals
{
    class GameOverModal : SadConsole.Consoles.Window
    {

        public SadConsole.Controls.Button okButton;

        public GameOverModal(int w, int h) : base(w, h)
        {
            var label1 = new SadConsole.Controls.DrawingSurface(20, 1);
            label1.Position = new Point(2, 2);
            label1.Print(0, 0, "The Fleet Has Been Sunk!");

            /*
            var input = new SadConsole.Controls.InputBox(14);
            input.Position = new Point(2, 3);
            */

            okButton = new SadConsole.Controls.Button(9);
            okButton.Position = new Point(2, 5);
            okButton.Text = "Ok";

            //this.Add(input);
            this.Add(label1);
            this.Add(okButton);
        }
    }
}
