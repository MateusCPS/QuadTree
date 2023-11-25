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
    public class Player : Collidable
    {
        Vector2 playerPosition;
        float scale = 0.1f;
        public Player(Texture2D texture)
        {
            Texture = texture;
            Position = new Vector2(100, 100); // Posição inicial do jogador
            playerPosition = Position;
        }

        public override void Update(GameTime gameTime)
        {
            // Lógica de movimento do jogador
            // ...

            // Exemplo: Movimento usando as teclas de seta
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Left))
                playerPosition.X -= 5;
            if (keyboardState.IsKeyDown(Keys.Right))
                playerPosition.X += 5;
            if (keyboardState.IsKeyDown(Keys.Up))
                playerPosition.Y -= 5;
            if (keyboardState.IsKeyDown(Keys.Down))
                playerPosition.Y += 5;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
            spriteBatch.Draw(Texture, playerPosition, null, Color.White, 0f, origin, scale, SpriteEffects.None, 0f);
        }

        public bool CheckCollision(Collidable other)
        {
            Rectangle scaledBounds = new Rectangle(
            (int)(playerPosition.X - (Texture.Width * scale) / 2),
            (int)(playerPosition.Y - (Texture.Height * scale) / 2),
            (int)(Texture.Width * scale),
            (int)(Texture.Height * scale)
            );
            return scaledBounds.Intersects(other.Bounds);
        }
    }
}
