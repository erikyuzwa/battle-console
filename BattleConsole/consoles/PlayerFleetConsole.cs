
using Microsoft.Xna.Framework;
using SadConsole.Input;
using SadConsole.Controls;
using SadConsole.Consoles;
using SadConsole.Game;
using SadConsole;

namespace BattleConsole.consoles
{
    class PlayerFleetConsole : SadConsole.Consoles.Console
    {
        /*
        public GameObject entity1;
        public GameObject entity2;
        public GameObject entity3;
        */

        public GameObject[] entities;




        public PlayerFleetConsole(int width, int height) : base(width, height)
        {
            // draw a border
            SadConsole.Shapes.Box box = SadConsole.Shapes.Box.GetDefaultBox();
            box.Width = width;
            box.Height = height;
            box.Draw(this);

            this.Fill(Color.Purple, Color.DarkGray, 250, null);

            /*
            this.CanUseMouse = true;
            this.MouseCanFocus = true;
            this.CanUseKeyboard = false;
            */

            
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

            /*
            this.entity1 = new GameObject(Engine.DefaultFont);
            this.entity1.Animation = playerAnimation;

            this.entity2 = new GameObject(Engine.DefaultFont);
            this.entity2.Animation = playerAnimation;
            
            this.entity3 = new GameObject(Engine.DefaultFont);
            this.entity3.Animation = playerAnimation;
            */

        }

        public void SpawnEntities()
        {
            // add our ship
           
            this.entities[0].Position = new Point(13, 3);
            this.entities[1].Position = new Point(13, 4);
            this.entities[2].Position = new Point(13, 5);

            
            this.entities[0].RenderOffset = this.Position - this.TextSurface.RenderArea.Location;
            this.entities[1].RenderOffset = this.Position - this.TextSurface.RenderArea.Location;
            this.entities[2].RenderOffset = this.Position - this.TextSurface.RenderArea.Location;
            
           
        }

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
                    if (item.Name == "ship")
                    {
                        item.Name = "debris";

                        AnimatedTextSurface debrisAnimation = new AnimatedTextSurface("default", 1, 1, Engine.DefaultFont);
                        debrisAnimation.CreateFrame();
                        debrisAnimation.CurrentFrame[0].Foreground = Color.Yellow;
                        debrisAnimation.CurrentFrame[0].GlyphIndex = '#';

                        item.Animation = debrisAnimation;
                
                        System.Console.WriteLine("hit!");
                        result = true;
                        break;
                    }
                   
                }

            }

            return result;
        }

        public void DrawEntity(Point pos)
        {


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
