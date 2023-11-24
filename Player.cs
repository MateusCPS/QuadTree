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

namespace QuadTreeNamespace
{
    public class Player : Collidable
    {
        Vector2 playerPosition;
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
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        public bool CheckCollision(Collidable other)
        {
            return Bounds.Intersects(other.Bounds);
        }
    }
}
