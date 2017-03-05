using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleConsole.modals
{
    class GameQuitModal : SadConsole.Consoles.Window
    {

        public SadConsole.Controls.Button okButton;
        public SadConsole.Controls.Button cancelButton;

        public GameQuitModal(int w, int h) : base(w, h)
        {
            var label1 = new SadConsole.Controls.DrawingSurface(20, 1);
            label1.Position = new Point(2, 2);
            label1.Print(0, 0, "Really Quit?");

            okButton = new SadConsole.Controls.Button(9);
            okButton.Position = new Point(2, 5);
            okButton.Text = "Ok";

            cancelButton = new SadConsole.Controls.Button(9);
            cancelButton.Position = new Point(19, 5);
            cancelButton.Text = "Cancel";

            this.Add(label1);
            this.Add(okButton);
            this.Add(cancelButton);

            this.Title = "Quit Already?";
        }
    }
}
