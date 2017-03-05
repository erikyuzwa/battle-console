using SadConsole.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleConsole.consoles
{
    class TitleConsole : SadConsole.Consoles.Console
    {
        public TitleConsole(int width, int height) : base(width, height)
        {
            
            SadConsole.Shapes.Box box = SadConsole.Shapes.Box.GetDefaultBox();
            box.Width = width;
            box.Height = height;
            box.Draw(this);

        }

        public TitleConsole(string text, int width, int height) : this(width, height)
        {
            Print(2, 0, text);
        }


    }
}
