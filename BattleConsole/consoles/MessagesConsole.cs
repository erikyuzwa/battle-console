using System;
using System.Collections.Generic;
using System.Linq;
using SadConsole;

namespace BattleConsole.consoles
{
    class MessagesConsole : SadConsole.Consoles.Console
    {

        public MessagesConsole(int width, int height) : base(width, height)
        {

            this.CanFocus = false;
            this.CanUseKeyboard = false;
            this.CanUseMouse = false;
            this.VirtualCursor.IsVisible = true;
        }

        public void PrintMessage(string text)
        {
            ShiftDown(1);
            VirtualCursor.Print(text).CarriageReturn();
        }

        public void PrintMessage(ColoredString text)
        {
            ShiftDown(1);
            VirtualCursor.Print(text).CarriageReturn();
        }

    }
}
