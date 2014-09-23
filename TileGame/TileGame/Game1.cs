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

namespace WindowsGame2
{

    public class Main : Game
    {
        public static string cLeft = "Q";
        public static string cUp = "Z";
        public static string cDown = "S";
        public static string cRight = "D";
        public static string cJump = "Space";

        public static Camera Cam = new Camera();
        public static KeyboardState KeyState = Keyboard.GetState();
        public static MouseState MouseState = Mouse.GetState();
        public static bool MouseLeft;
        public static bool MouseLeftRelease;

        public static bool MouseRight;
        public static bool MouseRightRelease;

        public static Texture2D Textures_Cursor;
        public static Player plr = new Player();
        public static int ScreenWidth { get { return graphics.GraphicsDevice.Viewport.Width; } }
        public static int ScreenHeight { get { return graphics.GraphicsDevice.Viewport.Height; } }

        public static int MaxTilesX = 1000;
        public static int MaxTilesY = 1000;
        public static Tile[,] tile = new Tile[MaxTilesX, MaxTilesY];
        public static Texture2D[] Textures_Tiles = new Texture2D[4];
        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;

        public static bool[] tileSolidTop = new bool[4];
        public static bool[] tileSolid = new bool[4];

        [ThreadStatic]
        public static Random rnd = new Random();

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            tileSolid[1] = true;
            tileSolidTop[1] = true;
            tileSolid[3] = true;

            Window.AllowUserResizing = true;
            for (int x = 0; x < MaxTilesX; x++)
            {
                for (int y = 0; y < MaxTilesY; y++)
                {
                    tile[x, y] = new Tile() { type = 1,active=true };
                    if (y < 16)
                    {
                        tile[x, y].type = 0;
                        tile[x, y].active = false;
                    }
                    if (y == 16)
                    {
                        tile[x, y].frameY = 16;
                        tile[x, y].frameX = (byte)(rnd.Next(0, 1) == 0 ? 0 : 16);
                    }
                    else
                        tile[x, y].frameX = (byte)(rnd.Next(0, 1) == 0 ? 0 : 16);
                }
            }
            plr.position.X = 80;
            plr.position.Y = 0;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Textures_Cursor = Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Cursor");
            for (int i = 0; i < Textures_Tiles.Length; i++)
                Textures_Tiles[i] = Content.Load<Texture2D>(string.Format("Images" + Path.DirectorySeparatorChar + "Tile_{0}", i));

            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            KeyState = Keyboard.GetState();
            MouseState = Mouse.GetState();

            if (MouseState.LeftButton == ButtonState.Pressed)
                MouseLeft = true;

            if (MouseState.RightButton == ButtonState.Pressed)
                MouseRight = true;

            plr.UpdatePlayer();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            DrawTiles();
            DrawPlayer();
            DrawMouse();

            spriteBatch.End();

            if (MouseLeft)
                MouseLeftRelease = false;
            else
                MouseLeftRelease = true;

            if (MouseRight)
                MouseRightRelease = false;
            else
                MouseRightRelease = true;

            MouseLeft = false;
            MouseRight = false;

            base.Draw(gameTime);
        }

        public static void DrawTiles()
        {
            for (int x = 0; x < (ScreenWidth / 16) + 2; x++)
            {
                for (int y = 0; y < (ScreenHeight / 16) + 2; y++)
                {
                    Tile t = tile[(int)Cam.X / 16 + x, (int)Cam.Y / 16 + y];
                    if (t.active)
                        spriteBatch.Draw(Textures_Tiles[t.type], new Vector2(x * 16 - Cam.X % 16, y * 16 - Cam.Y % 16), new Rectangle(t.frameX, t.frameY, 16, 16), Color.White, 0f, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0f);
                }
            }
        }

        public static void DrawPlayer()
        {
            float x = plr.position.X - Cam.X;
            float y = plr.position.Y - Cam.Y;
            spriteBatch.Draw(Textures_Tiles[2], new Vector2(x, y), new Rectangle(0, 0, plr.Width, plr.Height), Color.White, 0f, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0f);
        }

        public static void DrawMouse()
        {
            float Size = 1.0f;
            if (MouseLeft)
            {
                if (MouseState.X > 0 && MouseState.X < ScreenWidth && MouseState.Y > 0 && MouseState.Y < ScreenHeight)
                {
                    Tile t = tile[(int)(Cam.X + MouseState.X) / 16, (int)(Cam.Y + MouseState.Y) / 16];
                    t.type = 0;
                    t.active = false;
                    Size = 1.1f;
                }
            }
            spriteBatch.Draw(Textures_Cursor, new Vector2(MouseState.X, MouseState.Y), new Rectangle(0, 0, Textures_Cursor.Width, Textures_Cursor.Height), Color.White, 0f, new Vector2(0, 0), Size, SpriteEffects.None, 0f);
        }
    }
}
