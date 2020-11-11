using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Perceptron_App
{
    /// <summary>
    /// This class takes a given 2D plain filled with points and tries to find a straight line which divides the points by color perfectly.
    /// If there is a solution, it is returned, if not the programm throws an exception.
    /// </summary>
    class Solver
    {
        Perceptron p;

        public Straightline FindSolutionFor(List<ModelPoint> model, bool debug)
        {
            p = new Perceptron(debug);
            p.train(model, 0.95f);
            return p.Solution;
        }

        public Color TestPoint(int x, int y)
        {
            return p.testPoint(x, y); 
        }

        public void Reset()
        {
            p.ResetWeights();
        }
    }

    /// <summary>
    /// A representation of a line in y = mx+b.
    /// </summary>
    class Straightline
    {
        float M;
        float B;

        public Straightline(float m, float b )
        {
            M = m;
            B = b;
        }

        public float F(int x)
        {
            return (M * x) + B;
        }

        public override string ToString()
        {
            string result = "Line: y = " + M + "x ";
            if(B < 0)
            {
                result += B;
            }
            else
            {
                result += "+" + B;
            }
            return result;
        }
    }

    /// <summary>
    /// Signals the non-solvability of the model.
    /// </summary>
    class NoSolutionException : Exception
    {
        public NoSolutionException(string message) : base(message)
        {
            
        }
    }
}
