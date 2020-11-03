using System;
using System.Collections.Generic;
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
        public Straightline findSolutionFor(List<ModelPoint> model)
        {
            return null; //test return
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
        float X
        {
            get { return X; }
            set { X = value; }
        }
        float B
        {
            get { return B; }
            set { B = value; }
        }

        public Straightline(float m, float x, float b )
        {
            M = m;
            X = x;
            B = b;
        }

        public float getY()
        {
            return (M * X) + B;
        }
    }

    /// <summary>
    /// Signals the non-solvability of the model.
    /// </summary>
    class NoSolutionException : System.Exception
    {

    }
}
