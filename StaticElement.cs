using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;

namespace QuadTreeNamespace
{
    public class StaticElement : GameObject
    {
        Random random = new Random();
        Color color = Color.Yellow;
        public StaticElement(Texture2D texture, Game game)
        {
            _texture = texture;
            int width = random.Next(10, 20);
            int height = random.Next(10, 20);
            float x = random.Next(0, game.Window.ClientBounds.Width - width);
            float y = random.Next(0, game.Window.ClientBounds.Height - height);
            _position = new Vector2(x, y);

            _size = new Point(width, height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_isCollidingWithPlayer)
            {
                color = Color.Black;
            }
            else
            {
                color = Color.Yellow;
            }
            
            spriteBatch.Draw(_texture, new Rectangle((int)_position.X, (int)_position.Y, _size.X, _size.Y), color);
        }
    }
}
