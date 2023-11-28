using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

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
        bool _useQuadTree = true;
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
            worldBounds = new Rectangle(0, 0, this.Window.ClientBounds.Width, this.Window.ClientBounds.Height);

            _quadTree = new QuadTreeScript(0, worldBounds);
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
            _player = new Player(Content.Load<Texture2D>("test"), this);

            foreach (var element in elements)
            {
                _quadTree.Insert(element);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            _player.Update(gameTime);

            if (_useQuadTree == true)
            {
                QuadTreeScript playerQuadrant = _quadTree.GetQuadrant(_player);
                List<GameObject> elementsInPlayerQuadrant = _quadTree.GetElementsInQuadrant(playerQuadrant, _player);
                Window.Title = "Quadtree: Ativada/ com " + elementsInPlayerQuadrant.Count + "elementos no quadrante do player";

                _player.Collision(elementsInPlayerQuadrant);

                foreach (GameObject item in elements)
                {
                    if (item is DynamicElement)
                        item.Move(gameTime);

                    QuadTreeScript itemQuadrant = _quadTree.GetQuadrant(item);
                    List<GameObject> elementsInItemQuadrant = _quadTree.GetElementsInQuadrant(itemQuadrant, item);
                    item.Collision(elementsInItemQuadrant);
                }
            }
            else
            {
                Window.Title = "Quadtree Desativada";
                _player.Collision(elements);

                foreach (GameObject element in elements)
                {
                    if (element is DynamicElement)
                        element.Move(gameTime);
                    element.Collision(elements);
                    element.Collision(_player);
                }
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

            //_quadTree.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}