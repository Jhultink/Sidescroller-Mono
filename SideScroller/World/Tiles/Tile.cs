﻿using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SideScroller.ScrollerWorld.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SideScroller
{
    public abstract class Tile : ITile
    {
        public const int Size = 12;
        public TileType Type;
        public TileOrientation Orientation;
        protected ContentManager content;
        protected Map map;
        protected Texture2D texture;
        protected int xTile;
        protected int yTile;
        protected Body body;
        protected World world;
        protected Vector2 Bounds;
        
        protected Tile(int xCord, int yCord, TileType type, Map map, World world)
        {
            this.xTile = xCord;
            this.yTile = yCord;
            this.map = map;
            this.Type = type;
            this.world = world;
            this.Bounds = new Vector2(1, 1);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle(ConvertUnits.ToDisplayUnits(body.Position - (Bounds / 2)).ToPoint(), new Point(Tile.Size)), Color.White);
        }

        public virtual void Load(ContentManager content)
        {
            this.content = content;

            texture = content.Load<Texture2D>(Type.ToString());

            body = BodyFactory.CreateRectangle(world, ConvertUnits.ToSimUnits(Tile.Size), ConvertUnits.ToSimUnits(Tile.Size), 1f);
            body.Position = new Vector2(xTile, yTile);
            body.BodyType = BodyType.Static;
        }

        public virtual void Clicked(GameTime gameTime)
        {
            this.Type = TileType.Air;
            this.texture = this.content.Load<Texture2D>(TileType.Air.ToString());
        }
    }
}
