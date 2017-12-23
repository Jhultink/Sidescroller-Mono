using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MooleyMania.World.Generation;
using MooleyMania.World.Tiles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MooleyMania
{
    public class Map
    {

        public List<Tile> dirtTiles { get; } = new List<Tile>();

        public readonly int MaxWidth;
        public readonly int MaxHeight;
        private Tile[,] tiles;


        //int width, height;

        public Map(int width, int height)
        {
            this.MaxWidth = width;
            this.MaxHeight = height;
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

            int totalWidth = this.MaxWidth;
            int totalHeight = this.MaxHeight;
            int skyStart = this.MaxHeight / 10;
            int hillsStart = this.MaxHeight / 5;

            //int totalWidth = this.MaxWidth;
            //int totalHeight = this.MaxWidth;
            //int skyStart = 10;
            //int hillsStart = 99;

            tiles = new Tile[totalWidth, totalHeight];

            Random random = new Random();
            //Bitmap bitmap = new Bitmap(totalWidth, totalHeight);

            float offset = (float)random.NextDouble() * Int16.MaxValue;

            for (int i = 0; i < totalWidth; i++)
            {
                // Generate sky
                for (int j = 0; j < skyStart; j++)
                {
                    //bitmap.SetPixel(i, j, Color.SkyBlue);
                    tiles[i, j] = new Air(i, j);
                }

                // Generate hills 
                float hillsPerlin = Perlin.Fbm((i * .003f) + offset, 16);
                var landHeight = ((hillsPerlin + 1.0f) * ((skyStart - hillsStart) / 2f)) + hillsStart;
                for (int j = skyStart; j < hillsStart; j++)
                {
                    if (j < landHeight)
                        //bitmap.SetPixel(i, j, Color.SkyBlue);
                        tiles[i, j] = new Air(i, j);

                    else
                        //bitmap.SetPixel(i, j, Color.Brown);
                        tiles[i, j] = new Dirt(i, j);

                }

                // Generate underground
                for (int j = hillsStart; j < totalHeight; j++)
                {
                    //bitmap.SetPixel(i, j, Color.Brown);
                    tiles[i, j] = new Dirt(i, j);
                }

                for (int j = skyStart; j < totalHeight; j++)
                {
                    float cavesPerlin = Perlin.Fbm((i * .05f) + offset, (j * .05f) + offset, 8);

                    if (cavesPerlin > .2)
                    {
                        //if(bitmap.GetPixel(i, j).ToArgb() != Color.SkyBlue.ToArgb())
                        //    bitmap.SetPixel(i, j, Color.White);
                        tiles[i, j] = new Air(i, j);
                    }
                }
            }


            Bitmap bitmap = new Bitmap(totalWidth, totalHeight);

            for (int i = 0; i < totalWidth; i++)
            {
                for (int j = 0; j < totalHeight; j++)
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

            return calls;

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
