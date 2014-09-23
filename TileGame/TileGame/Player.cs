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
using System.IO;
using WindowsGame2;

namespace WindowsGame2
{
    public class Player
    {
        public Vector2 position;
        public Vector2 velocity;
        public bool controlLeft;
        public bool controlRight;
        public bool controlUp;
        public bool controlDown;
        public bool controlJump;
        public int Width = 32;
        public int Height = 48;
        public float MaxSpeed = 3f;
        public int AirTime = 1000;


        public void UpdatePlayer(int i = -1)
        {
            if (i == -1)
            {
                controlLeft = false;
                controlRight = false;
                controlUp = false;
                controlDown = false;
                controlJump = false;


                Keys[] pressedKeys = Main.KeyState.GetPressedKeys();
                for (int j = 0; j < pressedKeys.Length; j++)
                {
                    string a = string.Concat(pressedKeys[j]);
                    Console.WriteLine(a);
                    if (a == Main.cUp)
                        this.controlUp = true;
                    if (a == Main.cLeft)
                        this.controlLeft = true;
                    if (a == Main.cDown)
                        this.controlDown = true;
                    if (a == Main.cRight)
                        this.controlRight = true;
                    if (a == Main.cJump)
                        this.controlJump = true;
                }

                if (controlLeft)
                {
                    if (velocity.X > -MaxSpeed)
                        velocity.X -= 0.1f;
                }

                if (controlRight)
                {
                    if (velocity.X < MaxSpeed)
                        velocity.X += 0.1f;
                }

                if (controlJump && AirTime > 0)
                {
                    AirTime--;
                    velocity.Y = -3f;
                }
                else
                {
                    if (velocity.Y < 5)
                        velocity.Y += 0.1f;
                }

                if (velocity.X > 0.1f && !controlRight)
                {
                    velocity.X -= Math.Min(0.1f, velocity.X);
                }
                if (velocity.X < -0.1f && !controlLeft)
                {
                    velocity.X += Math.Min(0.1f, (0 - velocity.X));
                }


                velocity = Collision.TileCollision(position, velocity, Width, Height);

                position += velocity;
                Main.Cam.GoTo(this);
            }
        }
    }
}
