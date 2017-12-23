using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;
using MooleyMania.Helpers;
using MooleyMania.UI;
using MooleyMania.World.Tiles;

namespace MooleyMania
{
    public class Main : Game
    {
        public static Map Map;
        public static Player Player;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Camera camera;
        InventoryBar inventoryBar;

        KeyboardState pastKeyboardState;
        KeyboardState currentKeyboardState;
        MouseState pastMouseState;
        MouseState currentMouseState;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            this.Window.AllowUserResizing = true;
            this.Window.ClientSizeChanged += Window_ClientSizeChanged;
        }

        private void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            camera.UpdateScreenSize(this.Window.ClientBounds.Width, this.Window.ClientBounds.Height);
            inventoryBar.UpdateRelativePosition(new Vector2(camera.Bounds.Right - 50, camera.Bounds.Top + 50));
        }

        protected override void Initialize()
        {

            Map = new Map(200, 1000);
            Player = new Player(50, 0);
            camera = new Camera(GraphicsDevice.Viewport);
            inventoryBar = new InventoryBar(camera);


            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Keyboard and mouse setup
            pastKeyboardState = Keyboard.GetState();
            currentKeyboardState = Keyboard.GetState();
            pastMouseState = Mouse.GetState();
            currentMouseState = Mouse.GetState();

            Map.Generate();

            Map.Load(Content);
            Player.Load(Content);
            inventoryBar.Load(Content);

        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            pastKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            pastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            // Allows the game to exit
            if (pastKeyboardState.IsKeyDown(Keys.Escape) && currentKeyboardState.IsKeyUp(Keys.Escape))
                this.Exit();

            // Update player
            Player.Update(gameTime);

            inventoryBar.Update(gameTime);

            // Player collisions
            int collisionUpdateDistance = 50;
            int calls = 0;
            for (int x = (int)Player.TilePosition.X - collisionUpdateDistance; x < (int)Player.TilePosition.X + collisionUpdateDistance; x++)
            {
                for (int y = (int)Player.TilePosition.Y - collisionUpdateDistance; y < (int)Player.TilePosition.Y + collisionUpdateDistance; y++)
                {
                    if (x >= 0 && x < Map.MaxWidth && y >= 0 && y < Map.MaxHeight)
                    {
                        if (Map.Tiles[x, y].Type != TileType.Air)
                            Player.Collision(Map.Tiles[x, y].Rectangle, Map.MaxWidth, Map.MaxHeight);
                        calls++;

                        camera.Update(Player.Position, Map.MaxWidth, Map.MaxHeight);
                    }
                }
            }

            // Handle clicks
            if (currentMouseState.LeftButton == ButtonState.Pressed)
            {
                // Checks to make sure clicks are inside screen
                if (currentMouseState.X >= 0 && currentMouseState.X < camera.Bounds.Width && currentMouseState.Y > 0 && currentMouseState.Y < camera.Bounds.Height)
                {
                    Map.Tiles[(int)currentMouseState.ToAbsolute(camera).X / Tile.Size, (int)currentMouseState.ToAbsolute(camera).Y / Tile.Size]
                       = new Air((int)currentMouseState.ToAbsolute(camera).X / Tile.Size, (int)currentMouseState.ToAbsolute(camera).Y / Tile.Size);
                }
            }


            Debug.WriteLineIf(gameTime.IsRunningSlowly, "Update is running slowly: " + calls + " tiles updated");
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);

            int calls = Map.Draw(spriteBatch, camera);
            Player.Draw(spriteBatch);

            inventoryBar.Draw(spriteBatch, camera);

            Debug.WriteLineIf(gameTime.IsRunningSlowly, "Draw is running slowly: " + calls + " tiles updated");


            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
