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
using SideScroller.World;
using SideScroller.Helpers;
using SideScroller.UI;

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
        Inventory inventory;

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
            inventory = new Inventory(camera);

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

            Drop.Content = Content;

            Map.Load(Content);
            Player.Load(Content);
            inventoryBar.Load(Content);
            inventory.Load(Content);
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

            HandleKeyInput();
            
            // Update player
            Player.Update(gameTime);

            // Update Map
            Map.Update(gameTime);

            // Update inventory
            inventoryBar.Update(gameTime);
            inventory.Update(gameTime);

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
                    float xClick = currentMouseState.ToAbsolute(camera).X;
                    float yClick = currentMouseState.ToAbsolute(camera).Y;

                    Vector2 tileClick = currentMouseState.ToAbsolute(camera).ToTile();

                    Tile tile = Map.Tiles[(int)xClick / Tile.Size, (int)yClick / Tile.Size];

                    if (tile.Type != TileType.Air)
                    {
                        Map.Drops.Add(new Drop(
                            (int)tileClick.X,
                            (int)tileClick.Y,
                            Map.Tiles[(int) xClick / Tile.Size, (int) yClick/ Tile.Size].Type));

                        tile.Clicked(gameTime);
                    }
                }
            }

            Debug.WriteLineIf(gameTime.IsRunningSlowly, "Update is running slowly: " + calls + " tiles updated");
            base.Update(gameTime);
        }

        private void HandleKeyInput()
        {
            // Allows the game to exit
            if (pastKeyboardState.IsKeyDown(Keys.Escape) && currentKeyboardState.IsKeyUp(Keys.Escape))
                this.Exit();

            // Show inventory
            if (pastKeyboardState.IsKeyDown(Keys.E) && currentKeyboardState.IsKeyUp(Keys.E))
                inventory.IsVisible = !inventory.IsVisible;
        }

        protected override void Draw(GameTime gameTime)
        {
            // Set background to blue
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);

            // Draw map
            int calls = Map.Draw(spriteBatch, camera);
            Debug.WriteLineIf(gameTime.IsRunningSlowly, "Draw is running slowly: " + calls + " tiles updated");

            Player.Draw(spriteBatch, camera);

            // Inventory
            inventoryBar.Draw(spriteBatch, camera);
            inventory.Draw(spriteBatch, camera);

            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
