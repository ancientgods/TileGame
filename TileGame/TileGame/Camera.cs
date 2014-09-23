using Microsoft.Xna.Framework;
using System.Reflection;
using System;

namespace WindowsGame2
{
    public class Camera
    {
        private float _x;
        private float _y;
        public float X { get { return _x; } }
        public float Y { get { return _y; } }

        public void GoTo(float x, float y)
        {
            _x = x;
            _y = y;
            OutOfBoundsCheck();
        }

        public void GoTo(Player plr)
        {
            GoTo(plr.position.X - (Main.ScreenWidth / 2), plr.position.Y - (Main.ScreenHeight / 2));
        }

        private void OutOfBoundsCheck()
        {
            if (_x < 0)
                _x = 0;

            if (_y < 0)
                _y = 0;

            if (_x > (Main.MaxTilesX - 32) * 16 - Main.ScreenWidth)
                _x = (Main.MaxTilesX - 32) * 16 - Main.ScreenWidth;

            if (_y > (Main.MaxTilesY - 32) * 16 - Main.ScreenHeight)
                _y = (Main.MaxTilesY - 32) * 16 - Main.ScreenHeight;
        }


        public static Camera operator +(Camera c1, Vector2 c2)
        {
            Camera c = new Camera() { _x = (c1.X + c2.X), _y = (c1.Y + c2.Y) };
            c.OutOfBoundsCheck();
            return c;
        }
    }
}
