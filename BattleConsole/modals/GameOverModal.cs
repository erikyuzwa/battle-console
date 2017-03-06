using Microsoft.Xna.Framework;

namespace BattleConsole.modals
{
    class GameOverModal : SadConsole.Consoles.Window
    {

        public SadConsole.Controls.Button okButton;

        public GameOverModal(int w, int h, bool isPlayer1Winner) : base(w, h)
        {
            if (isPlayer1Winner)
            {
                this.Title = "YOU WIN!";
            } else {
                this.Title = "YOU LOSE!";
            }

            var label1 = new SadConsole.Controls.DrawingSurface(30, 1);
            label1.Position = new Point(2, 2);
            label1.Print(0, 0, "The Fleet Has Been Sunk!");

            okButton = new SadConsole.Controls.Button(9);
            okButton.Position = new Point(2, 5);
            okButton.Text = "Ok";

            this.Add(label1);
            this.Add(okButton);
        }
    }
}
