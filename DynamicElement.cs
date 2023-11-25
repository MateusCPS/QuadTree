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
    public class DynamicElement:Collidable
    {
        float scale = 1f;
        Vector2 dynamicObjectPos, Velocity;
        int areaWidth, areaHeight;
        public DynamicElement(Texture2D texture, Vector2 position, int areaWidth, int areaHeight)
        {
            Texture = texture;
            dynamicObjectPos = position;
            this.areaWidth = areaWidth;
            this.areaHeight = areaHeight;
            InitializeRandomVelocity();
        }

        private void InitializeRandomVelocity()
        {
            // Gere uma direção de movimento aleatória
            float randomDirection = MathHelper.ToRadians(Abp.RandomHelper.GetRandom(0,360));

            // Atribua uma velocidade inicial constante (você pode ajustar conforme necessário)
            float initialSpeed = 100f;
            Velocity = new Vector2((float)Math.Cos(randomDirection), (float)Math.Sin(randomDirection)) * initialSpeed;
        }

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            dynamicObjectPos += Velocity * deltaTime;

            // Adicione lógica específica para elementos dinâmicos aqui

            if (dynamicObjectPos.X < 0)
            {
                // Inverta a direção horizontal
                Velocity.X *= -1;
            }
            if (dynamicObjectPos.X > areaWidth)
            {
                Velocity.X *= -1;
            }

            if (dynamicObjectPos.Y < 0)
            {
                // Inverta a direção vertical
                Velocity.Y *= -1;
            }
            if (dynamicObjectPos.Y > areaHeight)
            {
                Velocity.Y *= -1;
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
            spriteBatch.Draw(Texture, dynamicObjectPos, null, Color.Black, 0f, origin, scale, SpriteEffects.None, 0f);
        }
    }
}
