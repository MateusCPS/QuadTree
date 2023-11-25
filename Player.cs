using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using SharpDX.Direct2D1.Effects;

namespace QuadTreeNamespace
{
    public class Player : GameObject
    {
        Vector2 playerPosition;

        public Player(Texture2D texture)
        {
            _texture = texture;
            _position = new Vector2(100, 100); // Posição inicial do jogador
            playerPosition = Position;
            _size = new Point(15, 15);
        }

        public override void Collision(List<GameObject> elements)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (Bounds.Intersects(elements[i].Bounds))
                {
                    elements[i].IsCollidingWithPlayer = true;
                    _isColliding = true;
                }
                else
                {
                    elements[i].IsCollidingWithPlayer = false;
                    _isColliding = false;
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Left))
                _position.X -= 5;
            if (keyboardState.IsKeyDown(Keys.Right))
                _position.X += 5;
            if (keyboardState.IsKeyDown(Keys.Up))
                _position.Y -= 5;
            if (keyboardState.IsKeyDown(Keys.Down))
                _position.Y += 5;
        }
    }
}
