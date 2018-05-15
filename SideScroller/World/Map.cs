using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MooleyMania.World.Generation;
using MooleyMania.World.Tiles;
using SideScroller.World;
using SideScroller.World.Tiles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace MooleyMania
{
    public class Map
    {
        public readonly int MaxWidth;

        public readonly int MaxHeight;

        public readonly int skyStart;

        public readonly int hillsStart;

        private Tile[,] tiles;

        public List<Drop> Drops { get; } = new List<Drop>(); 

        public Map(int width, int height)
        {
            this.MaxWidth = width;
            this.MaxHeight = height;
            skyStart = this.MaxHeight / 10; // At 10%
            hillsStart = this.MaxHeight / 5; // At 20%
        }

        public Tile this[int x, int y]
        {
            get
            {
                return tiles[x, y];
            }
        }

        public Tile[,] Tiles { get { return tiles; } }

        public void Generate()
        {
            Debug.WriteLine("Generating terrain...");
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            tiles = new Tile[MaxWidth, MaxHeight];

            Random random = new Random();

            float offset = (float)random.NextDouble() * Int16.MaxValue;

            for (int i = 0; i < MaxWidth; i++)
            {
                // Generate sky
                for (int j = 0; j < skyStart; j++)
                {
                    tiles[i, j] = new Air(i, j, this);
                }

                // Generate hills 
                float hillsPerlin = Perlin.Fbm((i * .003f) + offset, 16);
                var landHeight = ((hillsPerlin + 1.0f) * ((skyStart - hillsStart) / 2f)) + hillsStart;
                for (int j = skyStart; j < hillsStart; j++)
                {
                    if (j < landHeight)
                        tiles[i, j] = new Air(i, j, this);
                    else
                        tiles[i, j] = new Dirt(i, j, this);
                }

                // Generate underground
                for (int j = hillsStart; j < MaxHeight; j++)
                {
                    tiles[i, j] = new Dirt(i, j, this);
                }

                for (int j = skyStart; j < MaxHeight; j++)
                {
                    float cavesPerlin = Perlin.Fbm((i * .05f) + offset, (j * .05f) + offset, 8);

                    if (cavesPerlin > .2)
                    {
                        tiles[i, j] = new Air(i, j, this);
                    }
                }
            }

            for (int i = 0; i < MaxWidth; i++)
            {
                for (int j = 0; j < MaxHeight; j++)
                {
                    if (tiles[i, j].Type == TileType.Air)
                    {

                    }
                }
            }
        
            Bitmap bitmap = new Bitmap(MaxWidth, MaxHeight);

            for (int i = 0; i < MaxWidth; i++)
            {
                for (int j = 0; j < MaxHeight; j++)
                {
                    if (tiles[i, j].Type == TileType.Air)
                        bitmap.SetPixel(i, j, System.Drawing.Color.White);
                    else
                        bitmap.SetPixel(i, j, System.Drawing.Color.Brown);

                }
            }

            bitmap.Save(@"map.png");

            stopwatch.Stop();

            Debug.WriteLine("Generated map in " + stopwatch.Elapsed.ToString());

        }

        public int Draw(SpriteBatch batch, Camera camera)
        {
            // Render tiles around character
            int renderDistance = (int)(camera.Bounds.Width / Tile.Size) + 5;
            int calls = 0;

            for (int x = (int)Main.Player.TilePosition.X - renderDistance; x < (int)Main.Player.TilePosition.X + renderDistance; x++)
            {
                for (int y = (int)Main.Player.TilePosition.Y - renderDistance; y < (int)Main.Player.TilePosition.Y + renderDistance; y++)
                {
                    if (x >= 0 && x < this.MaxWidth && y >= 0 && y < this.MaxHeight)
                    {
                        tiles[x, y].Draw(batch);
                        calls++;
                    }
                }
            }

            // Render drops on top of tiles
            foreach(Drop drop in Drops)
            {
                drop.Draw(batch);
            }
            
            return calls;
        }

        public void Update(GameTime gameTime)
        {
            // Update drops
            for (int i = Drops.Count - 1; i >= 0; i--)
            {
                Drop drop = Drops[i];

                // Use map to detect collisions
                drop.DetectCollisions(this);

                // Calc dist from drop tp player
                float distToPlayer = Vector2.Distance(drop.Position, Main.Player.Bounds.Center.ToVector2());

                if (distToPlayer < 2 * Tile.Size)
                {
                    // Pick up
                    Drops.Remove(drop);
                    Main.inventory.AddItems(drop.Type, 1);
                }
                else if (distToPlayer < Player.PickupRange * Tile.Size)
                {
                    // Move drop towards player
                    drop.MoveToPlayer(Main.Player);
                }

                // Will move it
                drop.Update(gameTime);
            }
        }

        public void Load(ContentManager content)
        {
            foreach(Tile t in Tiles)
            {
                t.Load(content);
            }
        }
    }
}
