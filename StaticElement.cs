using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace QuadTreeNamespace
{
    public class StaticElement:Collidable
    {
        public StaticElement(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
        }

        public override void Update(GameTime gameTime)
        {
            // Lógica específica para elementos estáticos (se necessário)
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
