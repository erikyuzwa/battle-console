
using Microsoft.Xna.Framework;
using SadConsole.Input;
using SadConsole.Controls;
using SadConsole.Consoles;
using SadConsole.Game;
using SadConsole;
using System;

namespace BattleConsole.consoles
{
    class PlayerFleetConsole : SadConsole.Consoles.Console
    {
        public GameObject[] entities;

        public PlayerFleetConsole(int width, int height) : base(width, height)
        {
            // disable keyboard events to this console
            this.CanUseKeyboard = false;

            // draw a border
            SadConsole.Shapes.Box box = SadConsole.Shapes.Box.GetDefaultBox();
            box.Width = width;
            box.Height = height;
            box.Draw(this);

            this.Fill(Color.Purple, Color.DarkGray, 250, null);

            AnimatedTextSurface playerAnimation = new AnimatedTextSurface("default", 1, 1, Engine.DefaultFont);
            playerAnimation.CreateFrame();
            playerAnimation.CurrentFrame[0].Foreground = Color.Orange;
            playerAnimation.CurrentFrame[0].GlyphIndex = '@';

   
            this.entities = new GameObject[3];

            for (int i = 0; i < 3; i++)
            {
                this.entities[i] = new GameObject(Engine.DefaultFont);
                this.entities[i].Animation = playerAnimation;
                this.entities[i].Name = "ship";
            }

        }

        public void SpawnEntities()
        {
            // Maybe there's a better way to spawn a ship entity. This might certainly not be
            // "ideal" if we want to create a fleet of several ships. 

            Random rnd = new Random();
            int randX = rnd.Next(3, 20);
            int randY = rnd.Next(3, 9);

            // right now the "algorithm" pretty much forces ships to spawn vertically - ¯\_(ツ)_/¯
            this.entities[0].Position = new Point(randX, randY);
            this.entities[1].Position = new Point(randX, randY+1);
            this.entities[2].Position = new Point(randX, randY+2);

            // we need to account for the offset of our entity within the world
            this.entities[0].RenderOffset = this.Position - this.TextSurface.RenderArea.Location;
            this.entities[1].RenderOffset = this.Position - this.TextSurface.RenderArea.Location;
            this.entities[2].RenderOffset = this.Position - this.TextSurface.RenderArea.Location;

        }

        // just cycle through our GameObject entities and look for any occurrence
        // of the "ship" Name property. If there are none, then it means the fleet
        // has been sunk.
        public bool AreAllEntitiesDebris()
        {
            bool result = true;
            foreach (var item in this.entities)
            {
                if (item.Name == "ship")
                {
                    result = false;
                    break;
                }

            }

            return result;
        }

        // false means "miss" - true means "hit"
        public bool CollisionCheckEntities(Point pos)
        {
            bool result = false;
            foreach (var item in this.entities)
            {
                var worldPos =  item.RenderOffset + item.Position;
                if ((pos.X == worldPos.X) && (pos.Y == worldPos.Y))
                {
                    // if we've detected a collision with a section of ship, then
                    // replace the cell with "debris". Using the Name property will
                    // help us determine a win condition later.
                    if (item.Name == "ship")
                    {
                        // set this GameObject's name to debris
                        item.Name = "debris";

                        // create a new AnimatedTextSurface with a debris glyph
                        AnimatedTextSurface debrisAnimation = new AnimatedTextSurface("default", 1, 1, Engine.DefaultFont);
                        debrisAnimation.CreateFrame();
                        debrisAnimation.CurrentFrame[0].Foreground = Color.Yellow;
                        debrisAnimation.CurrentFrame[0].GlyphIndex = '#';
                        item.Animation = debrisAnimation;
                
                        result = true;
                        break;
                    }
                   
                }

            }

            return result;
        }

        public override void Render()
        {
            base.Render();
            foreach (var item in this.entities)
            {
                item.Render();
            }

        }

        public override void Update()
        {
            base.Update();

            foreach (var item in this.entities)
            {
                item.Update();
            }

        }

    }
}
