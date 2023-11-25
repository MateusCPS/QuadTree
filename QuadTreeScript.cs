﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace QuadTreeNamespace
{
    public class QuadTreeScript
    {
        private const int MAX_ELEMENTS_NODE = 4;
        private const int MAX_LEVELS = 10;

        private int level; //serve para controlar a altura da arvore
        private List<GameObject> elements;
        private Rectangle bounds; //serve para definir os limites do box da quadtree
        private QuadTreeScript[] nodes; //representa os nos internos da arvore
        private Texture2D pixel;

        public QuadTreeScript(int level, Rectangle bounds)
        {
            this.level = level;
            elements = new List<GameObject>();
            this.bounds = bounds;
            nodes = new QuadTreeScript[4];
        }

        //Serve para reiniciar/limpar a quadtree
        public void Clear()
        {
            elements.Clear(); //limpa a lista de elementos

            for (int i = 0; i < nodes.Length; i++) //passa por todos os nos da arvore
            {
                if (nodes[i] != null) //Se existir algo nesse node
                {
                    nodes[i].Clear(); // A funcao e chamada recursivamente limpando cada no
                    nodes[i] = null; // Passa o valor null para o no
                }
            }
        }

        //Retorna em qual quadrante está um elemento
        public QuadTreeScript GetQuadrant(GameObject element)
        {
            if (nodes[0] != null)
            {
                int index = GetIndex(element);

                if (index != -1)
                {
                    return nodes[index].GetQuadrant(element);
                }
            }

            return this;
        }

        //Retorna os elementos de um quadrante
        public List<GameObject> GetElementsInQuadrant(QuadTreeScript quadrant, GameObject element)
        {
            List<GameObject> quadrantElements = new List<GameObject>();

            if (!bounds.Intersects(quadrant.bounds))
            {
                return quadrantElements;
            }

            foreach (var e in elements)
            {
                if (e != element && quadrant.bounds.Intersects(e.Bounds))
                {
                    quadrantElements.Add(e);
                }
            }

            if (nodes[0] != null)
            {
                foreach (var node in nodes)
                {
                    quadrantElements.AddRange(node.GetElementsInQuadrant(quadrant, element));
                }
            }

            return quadrantElements;
        }



        //Divide a quadtree em quatro subquadrantes
        private void Split()
        {
            int subWidth = bounds.Width / 2;
            int subHeight = bounds.Height / 2;
            int x = bounds.X;
            int y = bounds.Y;

            nodes[0] = new QuadTreeScript(level + 1, new Rectangle(x + subWidth, y, subWidth, subHeight));                // Quadrante Superior Direito
            nodes[1] = new QuadTreeScript(level + 1, new Rectangle(x, y, subWidth, subHeight));                           // Quadrante Superior Esquerdo
            nodes[2] = new QuadTreeScript(level + 1, new Rectangle(x, y + subHeight, subWidth, subHeight));               // Quadrante Inferior Esquerdo
            nodes[3] = new QuadTreeScript(level + 1, new Rectangle(x + subWidth, y + subHeight, subWidth, subHeight));    // Quadrante Inferior Direito
        }

        //Essa funcao determina o indice do no filho adequado para colocar um objeto na quadtree
        private int GetIndex(GameObject element)
        {
            int index = -1;

            float verticalMidpoint = bounds.X + (bounds.Width / 2);
            float horizontalMidpoint = bounds.Y + (bounds.Height / 2);

            bool topQuadrant = (element.Position.Y < horizontalMidpoint && element.Position.Y + element.Size.Y < horizontalMidpoint);
            bool bottomQuadrant = (element.Position.Y > horizontalMidpoint);

            if (element.Position.X < verticalMidpoint && element.Position.X + element.Size.X < verticalMidpoint)
            {
                if (topQuadrant)
                    index = 1;
                else if (bottomQuadrant)
                    index = 2;
            }
            else if (element.Position.X > verticalMidpoint)
            {
                if (topQuadrant)
                    index = 0;
                else if (bottomQuadrant)
                    index = 3;
            }

            return index;
        }

        //Insere um elemento na Quadtree, passando o no correto para o objeto
        public void Insert(GameObject element)
        {
            if (nodes[0] != null)
            {
                int index = GetIndex(element);

                if (index != -1)
                {
                    nodes[index].Insert(element);
                    return;
                }
            }

            elements.Add(element);

            if (elements.Count > MAX_ELEMENTS_NODE && level < MAX_LEVELS)
            {
                if (nodes[0] == null)
                    Split();

                int i = 0;
                while (i < elements.Count)
                {
                    int index = GetIndex(elements[i]);
                    if (index != -1)
                    {
                        nodes[index].Insert(elements[i]);
                        elements.RemoveAt(i);
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }



        //Essa funcao serve para desenhar retangulos na tela
        private void DrawRectangle(SpriteBatch spriteBatch, Rectangle rectangle, Color color, int thickness)
        {
            // Desenhar linhas horizontais
            spriteBatch.Draw(pixel, new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, thickness), color); // Linha superior
            spriteBatch.Draw(pixel, new Rectangle(rectangle.Left, rectangle.Bottom - thickness, rectangle.Width, thickness), color); // Linha inferior

            // Desenhar linhas verticais
            spriteBatch.Draw(pixel, new Rectangle(rectangle.Left, rectangle.Top, thickness, rectangle.Height), color); // Linha esquerda
            spriteBatch.Draw(pixel, new Rectangle(rectangle.Right - thickness, rectangle.Top, thickness, rectangle.Height), color); // Linha direita
        }


        // Renderiza a Quadtree na tela
        public void Draw(SpriteBatch spriteBatch)
        {
            pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });

            DrawRectangle(spriteBatch, bounds, Color.Red, 1);

            foreach (var node in nodes)
            {
                if (node != null)
                    node.Draw(spriteBatch);
            }
        }
    }
}
