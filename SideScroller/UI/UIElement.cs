using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SideScroller.UI
{
    public abstract class UIElement
    {
        public Vector2 AbsolutePosition { get; private set; }

        public bool IsVisible;
        public Vector2 RelativePosition;
        public string TextureName;
        public Texture2D Texture;
        public int Width, Height;


        public UIElement(Vector2 relativePosition, string textureName, int width, int height, bool isVisible = true)
        {
            this.RelativePosition = relativePosition;
            this.TextureName = textureName;
            this.Width = width;
            this.Height = height;
            this.IsVisible = isVisible;
        }

        public virtual void Load(ContentManager content)
        {
            Texture = content.Load<Texture2D>(TextureName);
        }

        public virtual void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            if(IsVisible)
            {
               this.AbsolutePosition = camera.Position + RelativePosition;
                spriteBatch.Draw(Texture, new Rectangle((int)Math.Round(this.AbsolutePosition.X, 0),
                    (int)Math.Round(this.AbsolutePosition.Y, 0), this.Width, this.Height), Color.White);
            }
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void UpdateRelativePosition(Vector2 relPos)
        {
            this.RelativePosition = relPos;
        }
    }

    public enum UIAnchor
    {
        TopLeft,
        TopRight,
        BottomRight,
        BottomLeft
    }

}
