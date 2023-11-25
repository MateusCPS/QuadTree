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
        private List<StaticElement> _staticElements;
        private List<DynamicElement> _dynamicElements;
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
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // Carregar texturas para o player, elementos estáticos e dinâmicos
            worldBounds = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            
            _player = new Player(Content.Load<Texture2D>("test"));
            _staticElements = GenerateStaticElements(100, worldBounds.Width, worldBounds.Height);
            _dynamicElements = GenerateDynamicElements(200, worldBounds.Width, worldBounds.Height);

            //// Configurar a QuadTree
            //_quadTree = new QuadTreeScript(0, worldBounds);
            //foreach (var element in _staticElements)
            //    _quadTree.Insert(element);
            //foreach (var element in _dynamicElements)
            //    _quadTree.Insert(element);
            //_quadTree.Insert(_player);
        }

        protected override void Update(GameTime gameTime)
        {
            // Atualizar a lógica do jogo aqui

            // Movimentar o jogador
            _player.Update(gameTime);
            foreach(var element in _dynamicElements) {
                element.Update(gameTime);
            }

            // Atualizar a QuadTree
            //_quadTree.Clear();
            //foreach (var element in _staticElements)
            //    _quadTree.Insert(element);
            //foreach (var element in _dynamicElements)
            //    _quadTree.Insert(element);
            //_quadTree.Insert(_player);

            // Checar colisões apenas para elementos no mesmo setor que o jogador
            //List<Collidable> potentialColliders = _quadTree.Retrieve(_player);
            //foreach (var collider in potentialColliders)
            //{
            //    if (collider != _player && _player.CheckCollision(collider))
            //    {
            //        // Lógica de colisão aqui
            //    }
            //}

            base.Update(gameTime);
        }

        private List<StaticElement> GenerateStaticElements(int count, int areaWidth, int areaHeight)
        {
            List<StaticElement> staticElements = new List<StaticElement>();
            Random random = new Random();

            for (int i = 0; i < count; i++)
            {
                // Gere posições x e y normalizadas (entre 0 e 1)
                float normalizedX = (float)random.NextDouble();
                float normalizedY = (float)random.NextDouble();

                // Calcule as posições reais dentro da área especificada
                int x = (int)(normalizedX * areaWidth);
                int y = (int)(normalizedY * areaHeight);

                Texture2D staticTexture = Content.Load<Texture2D>("Enemy_Tank");

                // Crie o elemento estático e adicione à lista
                StaticElement staticElement = new StaticElement(staticTexture, new Vector2(x, y));
                staticElements.Add(staticElement);
            }

            return staticElements;
        }
        private List<DynamicElement> GenerateDynamicElements(int count, int areaWidth, int areaHeight)
        {
            List<DynamicElement> dynamicElements = new List<DynamicElement>();
            Random random = new Random();

            for (int i = 0; i < count; i++)
            {
                // Gere posições x e y normalizadas (entre 0 e 1)
                float normalizedX = (float)random.NextDouble();
                float normalizedY = (float)random.NextDouble();

                // Calcule as posições reais dentro da área especificada
                int x = (int)(normalizedX * areaWidth);
                int y = (int)(normalizedY * areaHeight);

                DynamicElement dynamicElement = new DynamicElement(Content.Load<Texture2D>("Heart"), new Vector2(x, y), areaWidth, areaHeight);
                dynamicElements.Add(dynamicElement);
            }

            return dynamicElements;
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            // Desenhar elementos aqui
            _player.Draw(_spriteBatch);
            foreach (var element in _staticElements)
                element.Draw(_spriteBatch);
            foreach (var element in _dynamicElements)
                element.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}