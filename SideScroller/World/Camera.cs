using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MooleyMania
{
    public class Camera
    {

        private Matrix transform;

        public Matrix Transform { get { return transform; } }
        private Vector2 center;
        private Viewport viewport;
        public float Zoom = 1f;
        public Rectangle Bounds { get { return viewport.Bounds; } }
        public Vector2 Center { get { return center; } }

        public Vector2 Position { get { return center - new Vector2(Bounds.Width / 2, Bounds.Height / 2); } }

        public Camera(Viewport newViewport)
        {
            viewport = newViewport;
        }


        public void Update(Vector2 position, int xOffset, int yOffset)
        {
            if (position.X < viewport.Width / 2)
                center.X = viewport.Width / 2;
            else if (position.X > (xOffset * Tile.Size) - (viewport.Width / 2))
                center.X = (xOffset * Tile.Size) - (viewport.Width / 2);
            else
                center.X = position.X;

            if (position.Y < viewport.Height / 2)
                center.Y = viewport.Height / 2;
            else if (position.Y > (yOffset * Tile.Size) - (viewport.Height / 2))
                center.Y = (yOffset * Tile.Size) - (viewport.Height / 2);
            else
                center.Y = position.Y;


            transform = Matrix.CreateTranslation(new Vector3(-center.X + (viewport.Width / 2),
                                                             -center.Y + (viewport.Height / 2), 1f)) * Matrix.CreateScale(Zoom);
        }

        public void UpdateScreenSize(int width, int height)
        {
            viewport.Width = width;
            viewport.Height = height;
        }
    }
}
