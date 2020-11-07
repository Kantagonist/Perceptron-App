﻿using System;
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

        public Straightline FindSolutionFor(List<ModelPoint> model)
        {
            p = new Perceptron();
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