using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SideScroller.Components;
using SideScroller.Entities;
using SideScroller.UI;

namespace SideScroller
{
    public class Player : Entity
    {
        public const int PickupRange = 10;

        public Vector2 Velocity;
        public Vector2 Position;
        public Inventory Inventory;
        public Vector2 Bounds = new Vector2(2, 3);

        #region Components
        private InputComponent _input;
        private PhysicsComponent _physics;
        #endregion

        public Player(int x, int y, InputComponent input, PhysicsComponent physics, World world)
        {
            _input = input;
            _physics = physics;
            this.world = world;

            Position = new Vector2(x, y);
        }

        public override void Load(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("player");

            body = BodyFactory.CreateRectangle(world, Bounds.X, Bounds.Y, 1f);
            body.Position = Position;
            body.BodyType = BodyType.Dynamic;
            body.CollisionCategories = Category.Cat1;
            body.CollidesWith = Category.All & ~Category.Cat3; // not air
            body.Restitution = 0;
            body.IgnoreGravity = false;
        }

        public override void Update(GameTime gameTime)
        {
            _input.Update(this.body, gameTime);
            //_physics.Update(this, gameTime);
        }   

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            spriteBatch.Draw(texture, new Rectangle(ConvertUnits.ToDisplayUnits(body.Position - (Bounds / 2)).ToPoint(), ConvertUnits.ToDisplayUnits(Bounds).ToPoint()), Color.White);
        }
    }
}
