using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TileGame
{
    public class Collision
    {
        public static Vector2 TileCollision(Vector2 Position, Vector2 Velocity, int Width, int Height, bool fallThrough = false, bool fall2 = false)
        {
            if (Velocity.Y < 0.0f)
            {
                for (int i = 0; i <= (Width / 16); i++)
                {
                    int j = (Width / 16) == i ? 1 : 0;
                    Tile t = Main.tile[(int)((Position.X - j) / 16 + i), ((int)Position.Y) / 16];
                    if (Main.tileSolid[t.type])
                    {
                        Velocity.Y = 0;
                        break;
                    }
                }
            }

            if (Velocity.Y > 0.0f)
            {
                for (int i = 0; i <= (Width / 16); i++)
                {
                    int j = (Width / 16) == i ? 1 : 0;
                    Tile t = Main.tile[(int)((Position.X - j) / 16 + i), ((int)Position.Y) / 16 + (Height / 16)];
                    if (Main.tileSolid[t.type])
                    {
                        Velocity.Y = -(Position.Y % 16 + 1) / 10; ;
                        Main.plr.AirTime = 10;
                        break;
                    }
                }
            }

            if (Velocity.X < 0.0f)
            {
                for (int i = 1; i <= (Height / 16); i++)
                {
                    Tile t = Main.tile[(int)(Position.X - 1) / 16, (int)(Position.Y - 1) / 16 + i];
                    if (Main.tileSolid[t.type])
                    {
                        Velocity.X = 0;
                        break;
                    }
                }
            }

            if (Velocity.X > 0.0f)
            {
                for (int i = 1; i <= (Height / 16); i++)
                {
                    Tile t = Main.tile[(int)(Position.X + 1) / 16 + (Width / 16), (int)(Position.Y - 1) / 16 + i];
                    if (Main.tileSolid[t.type])
                    {
                        Velocity.X = 0;
                        break;
                    }
                }
            }
            return Velocity;
        }
    }
}