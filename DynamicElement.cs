using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.DirectoryServices.ActiveDirectory;

namespace QuadTreeNamespace
{
    public class DynamicElement:GameObject
    {
        Vector2 Velocity;
        int areaWidth, areaHeight;
        Color color = Color.Black;
        Random random = new Random();
        public DynamicElement(Texture2D texture, Game game)
        {
            _texture = texture;
            _game = game;
            _size = new Point(10, 10);
            float x = random.Next(0, _game.Window.ClientBounds.Width - _size.X);
            float y = random.Next(0, _game.Window.ClientBounds.Height - _size.Y);
            
            _position = new Vector2(x, y);

            InitializeRandomVelocity();
        }

        private void InitializeRandomVelocity()
        {
            float randomDirection = MathHelper.ToRadians(Abp.RandomHelper.GetRandom(0,360));

            float initialSpeed = 100f;
            Velocity = new Vector2((float)Math.Cos(randomDirection), (float)Math.Sin(randomDirection)) * initialSpeed;
        }

        public override void Collision(GameObject playerElement)
        {
            if (Bounds.Intersects(playerElement.Bounds))
            {
                Rectangle thisRef = Bounds;
                Rectangle playerRef = playerElement.Bounds;
                Rectangle difference;

                Rectangle.Intersect(ref playerRef, ref thisRef, out difference);
                playerElement.IsColliding = true;
                _isCollidingWithPlayer = true;
                Velocity = new Vector2(thisRef.Center.X - difference.Center.X,
                                              thisRef.Center.Y - difference.Center.Y);
                Velocity.Normalize();
            }
            else
            {
                _isColliding = false;
            }
        }

        public override void Collision(List<GameObject> elements)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i] != this)
                {
                    if (Bounds.Intersects(elements[i].Bounds))
                    {
                        Rectangle thisRef = Bounds;
                        Rectangle playerRef = elements[i].Bounds;
                        Rectangle difference;

                        Rectangle.Intersect(ref playerRef, ref thisRef, out difference);

                        elements[i].IsColliding = true;
                        _isColliding = true;

                        Velocity = new Vector2(thisRef.Center.X - difference.Center.X,
                                              thisRef.Center.Y - difference.Center.Y);
                        if (Velocity != Vector2.Zero)
                            Velocity.Normalize();
                    }
                    else
                    {
                        _isColliding = false;
                    }
                }
            }
        }


        public override void Move(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _position += Velocity * deltaTime;


            if (_position.X < 0)
            {
                Velocity.X *= -1;
            }
            if (_position.X > _game.Window.ClientBounds.Width)
            {
                Velocity.X *= -1;
            }

            if (_position.Y < 0)
            {
                Velocity.Y *= -1;
            }
            if (_position.Y > _game.Window.ClientBounds.Height)
            {
                Velocity.Y *= -1;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(_isCollidingWithPlayer || _isColliding)
            {
                color = Color.White;
            }

            spriteBatch.Draw(_texture, new Rectangle((int)_position.X, (int)_position.Y, _size.X, _size.Y), color); ;
        }
    }
}
