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
        public static Straightline FindSolutionFor(List<ModelPoint> model)
        {
            Perceptron p = new Perceptron();
            p.train(model, 0.95f);
            return p.Solution;
        }
    }

    /// <summary>
    /// A representation of a line in y = mx+b.
    /// </summary>
    class Straightline
    {
        float M
        {
            get { return M; }
            set { M = value; }
        }
        float B
        {
            get { return B; }
            set { B = value; }
        }

        public Straightline(float m, float b )
        {
            M = m;
            B = b;
        }

        public float F(int x)
        {
            return (M * x) + B;
        }
    }

    /// <summary>
    /// Signals the non-solvability of the model.
    /// </summary>
    class NoSolutionException : System.Exception
    {

    }
}
