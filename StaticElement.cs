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
        float scale = 0.05f;
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
            Vector2 origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
            spriteBatch.Draw(Texture, Position, null, Color.White, 0f, origin, scale, SpriteEffects.None, 0f);
        }
    }
}
