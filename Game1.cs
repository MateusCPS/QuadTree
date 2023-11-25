using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace QuadTreeNamespace
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player _player;
        private List<GameObject> elements;
        //private List<DynamicElement> _dynamicElements;
        private QuadTreeScript _quadTree;
        Rectangle worldBounds;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = 1820;
            _graphics.PreferredBackBufferHeight = 970;
        }

        protected override void Initialize()
        {
            elements = new List<GameObject>();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            for (int i = 0; i < 100; i++)
            {
                elements.Add(new StaticElement(Content.Load<Texture2D>("Enemy_Tank"), this));
            }
            for (int i = 0; i < 200; i++)
            {
                elements.Add(new DynamicElement(Content.Load<Texture2D>("Heart"), this));
            }
            _player = new Player(Content.Load<Texture2D>("test"));
        }

        protected override void Update(GameTime gameTime)
        {

            _player.Update(gameTime);
            _player.Collision(elements);

            foreach(GameObject element in elements)
            {
                if (element is DynamicElement)
                    element.Move(gameTime);
            }

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            // Desenhar elementos aqui
            _player.Draw(_spriteBatch);
            foreach (var element in elements)
                element.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}