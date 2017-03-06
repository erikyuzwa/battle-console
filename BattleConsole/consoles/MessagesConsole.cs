using System;
using System.Collections.Generic;
using System.Linq;
using SadConsole;

namespace BattleConsole.consoles
{
    class MessagesConsole : SadConsole.Consoles.Console
    {
        private int rowPosition;

        public MessagesConsole(int width, int height) : base(width, height)
        {

            this.CanFocus = false;
            this.CanUseKeyboard = false;
            this.CanUseMouse = false;
            this.VirtualCursor.IsVisible = false;

            // draw a border
            SadConsole.Shapes.Box box = SadConsole.Shapes.Box.GetDefaultBox();
            box.Width = width;
            box.Height = height;
            box.Draw(this);

            this.rowPosition = 0;

        }

        public void PrintMessage(string text)
        {
            this.UpdateRowPosition();

            this.Print(2, this.rowPosition, ">" + text);

            
        
        }

        public void PrintMessage(ColoredString text)
        {
            this.UpdateRowPosition();

            this.Print(2, this.rowPosition, ">" + text);
        }

        public void UpdateRowPosition()
        {
            this.rowPosition++;
            if (this.rowPosition > 4)
            {
                this.ClearMessages();
                this.rowPosition = 1;
            }
        }

        public void ClearMessages()
        {
            for (int i = 1; i < 5; i++)
            {
                this.Print(1, i, "                       ");
            }
        }

    }
}
