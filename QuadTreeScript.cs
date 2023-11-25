using System;
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
        private const int MaxObjects = 10;
        private const int MaxLevels = 5;

        private int _level;
        private List<GameObject> _objects;
        private Rectangle _bounds;
        private QuadTreeScript[] _nodes;

        public QuadTreeScript(int level, Rectangle bounds)
        {
            _level = level;
            _objects = new List<GameObject>();
            _bounds = bounds;
            _nodes = new QuadTreeScript[4];
        }

        public void Clear()
        {
            _objects.Clear();

            for (int i = 0; i < _nodes.Length; i++)
            {
                if (_nodes[i] != null)
                {
                    _nodes[i].Clear();
                    _nodes[i] = null;
                }
            }
        }

        private void Split()
        {
            int subWidth = _bounds.Width / 2;
            int subHeight = _bounds.Height / 2;
            int x = _bounds.X;
            int y = _bounds.Y;

            _nodes[0] = new QuadTreeScript(_level + 1, new Rectangle(x + subWidth, y, subWidth, subHeight));
            _nodes[1] = new QuadTreeScript(_level + 1, new Rectangle(x, y, subWidth, subHeight));
            _nodes[2] = new QuadTreeScript(_level + 1, new Rectangle(x, y + subHeight, subWidth, subHeight));
            _nodes[3] = new QuadTreeScript(_level + 1, new Rectangle(x + subWidth, y + subHeight, subWidth, subHeight));
        }

        private int GetIndex(GameObject collidable)
        {
            int index = -1;
            double verticalMidpoint = _bounds.X + (_bounds.Width / 2);
            double horizontalMidpoint = _bounds.Y + (_bounds.Height / 2);

            // Object can completely fit within the top quadrants
            bool topQuadrant = (collidable.Bounds.Y < horizontalMidpoint && collidable.Bounds.Y + collidable.Bounds.Height < horizontalMidpoint);
            // Object can completely fit within the bottom quadrants
            bool bottomQuadrant = (collidable.Bounds.Y > horizontalMidpoint);

            // Object can completely fit within the left quadrants
            if (collidable.Bounds.X < verticalMidpoint && collidable.Bounds.X + collidable.Bounds.Width < verticalMidpoint)
            {
                if (topQuadrant)
                    index = 1;
                else if (bottomQuadrant)
                    index = 2;
            }
            // Object can completely fit within the right quadrants
            else if (collidable.Bounds.X > verticalMidpoint)
            {
                if (topQuadrant)
                    index = 0;
                else if (bottomQuadrant)
                    index = 3;
            }

            return index;
        }

        public void Insert(GameObject collidable)
        {
            if (_nodes[0] != null)
            {
                int index = GetIndex(collidable);

                if (index != -1)
                {
                    _nodes[index].Insert(collidable);
                    return;
                }
            }

            _objects.Add(collidable);

            if (_objects.Count > MaxObjects && _level < MaxLevels)
            {
                if (_nodes[0] == null)
                    Split();

                int i = 0;
                while (i < _objects.Count)
                {
                    int index = GetIndex(_objects[i]);
                    if (index != -1)
                        _nodes[index].Insert(_objects[i]);
                    else
                    {
                        i++;
                    }
                }

                _objects.Clear();
            }
        }

        public List<GameObject> Retrieve(GameObject collidable)
        {
            List<GameObject> returnObjects = new List<GameObject>();

            int index = GetIndex(collidable);
            if (index != -1 && _nodes[0] != null)
            {
                returnObjects.AddRange(_nodes[index].Retrieve(collidable));
            }

            returnObjects.AddRange(_objects);

            return returnObjects;
        }
    }
}
