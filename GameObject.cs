﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System.Xml.Linq;

namespace QuadTreeNamespace
{
    public abstract class GameObject
    {
        protected Game _game;
        protected Texture2D _texture;
        protected Vector2 _position;
        protected Point _size;
        protected bool _isColliding, _isCollidingWithPlayer;
        protected float _speed;

        public bool IsColliding { get { return _isColliding; } set { _isColliding = value; } }
        public bool IsCollidingWithPlayer { get { return _isCollidingWithPlayer; } set { _isCollidingWithPlayer = value; } }
        public Vector2 Position { get { return _position; } }
        public Point Size { get { return _size; } }

        public virtual void Move(GameTime gameTime)
        { }

        public virtual void Collision(GameObject element)
        { }

        public virtual void Collision(List<GameObject> elements)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i] != this)
                {
                    if (Bounds.Intersects(elements[i].Bounds))
                    {
                        elements[i].IsColliding = true;
                        _isColliding = true;
                    }
                    else
                    {
                        elements[i].IsColliding = false;
                        _isColliding = false;
                    }
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, new Rectangle((int)_position.X, (int)_position.Y, _size.X, _size.Y), Color.White);
        }

        public Rectangle Bounds
        {
            get { return new Rectangle((int)_position.X, (int)_position.Y, _size.X, _size.Y); }
        }
    }
}

