using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Perceptron_App
{
    /// <summary>
    /// Creates a 2D plane with positive Points
    /// </summary>
    class ModelGenerator
    {
        private int AmountOfPoints = 0;
        public List<ModelPoint> points = new List<ModelPoint>();
        private Random rand = new Random(); 
        
        /// <summary>
        /// Generates the given amount of Points in a 100x100 positive grid.
        /// The points will be evenly split in their color distribution.
        /// </summary>
        /// <param name="amountOfPoints"></param>
        /// <param name="isRandom"></param>
        /// <returns></returns>
        public List<ModelPoint> GenerateNewSet(int amountOfPoints, bool isRandom)
        { 
            AmountOfPoints = amountOfPoints;
            points.Clear();
            if (isRandom)
            {
                for (int i = 0; i < AmountOfPoints; ++i)
                {
                    Color Color = ((i + 1) % 2 == 0) ? Color.Red : Color.Blue;
                    ModelPoint NewPoint = new ModelPoint(rand.Next(0, 100), rand.Next(0, 100), Color);
                    points.Add(NewPoint);
                }
            }
            else
            {
                for (int i = 0; i < AmountOfPoints; ++i)
                {
                    if((i+1) % 2 == 0)
                    {
                        ModelPoint NewPoint = new ModelPoint(rand.Next(0, 49), rand.Next(0, 100), Color.Red);
                        points.Add(NewPoint);
                    }
                    else
                    {
                        ModelPoint NewPoint = new ModelPoint(rand.Next(51, 100), rand.Next(0, 100), Color.Blue);
                        points.Add(NewPoint);
                    }
                }
            }
            return points;
        }

        /// <summary>
        /// Returns the current list of points as a human-readable String.
        /// </summary>
        /// <returns></returns>
        public String toString()
        {
            string result = "";
            for(int i = 0; i < points.Count; ++i)
            {
                result = string.Concat(result, "\n" + points[i].toString());
            }
            return result;
        }
    }

    class ModelPoint
    {
        public int X;
        public int Y;
        public Color Color;

        public ModelPoint(int x, int y, Color color)
        {
            X = x;
            Y = y; 
            Color = color;
        }
        
        public string toString()
        {
            return "X: " + X + " Y: " + Y + " " + Color.ToString() + ";";
        }
    }

}
