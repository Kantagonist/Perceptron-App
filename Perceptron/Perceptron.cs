using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron_App
{
    class Perceptron
    {
        public float[] weights;
        public Straightline Solution;
        internal bool Debug;

        internal Perceptron(bool debug)
        {
            Debug = debug;
        }

        /// <summary>
        /// Trains this instance of the Perceptron until the 
        /// </summary>
        /// <param name="points"></param>
        /// <param name="errorThreshold"></param>
        internal void train(List<ModelPoint> points, float errorThreshold)
        {
            //set configs
            weights = new float[] { 0, 0.5f, 0.5f };
            float learningRate = 0.1f;
            float iterationError = 0.95f;

            //actual learning, but capped with a hard stop if iteration error can not be reached
            float[] y;
            List<ModelPoint> misclassified = new List<ModelPoint>();
            int hardStop = 100000; //adjust at will
            for(int iteration = 0; iteration < hardStop; ++iteration)
            {
                //reset counters and arrays
                int k = 0;
                misclassified.Clear();
                y = new float[points.Count];
                
                //calculate the output of the weighted graph and flag misclassified points 
                foreach (ModelPoint entry in points)
                {    
                    y[k] = weights[0] + (entry.X * weights[1]) + (entry.Y * weights[2]);
                    if (y[k] > 0 && entry.Color == Color.Blue) //weights consider the entry to be red, but it is blue
                    {
                        misclassified.Add(entry);
                    }
                    else if (y[k] <= 0 && entry.Color == Color.Red) //weights consider the entry to be blue, but it is red
                    {
                        misclassified.Add(entry);
                    }
                    k++;
                }

                //update the weights
                foreach(ModelPoint entry in misclassified)
                {
                    float d =  entry.Color == Color.Red ? 1 : -1;
                    weights[0] = weights[0] + (learningRate * d * 1);
                    weights[1] = weights[1] + (learningRate * d * entry.X);
                    weights[2] = weights[2] + (learningRate * d * entry.Y);
                }

                //calculate the iteration error, if below the set threshhold, break the loop
                float missclassMargin = ((float)misclassified.Count) / ((float)points.Count);

                if (Debug)
                {
                    printLearnCycleOnConsole(iteration, misclassified.Count, missclassMargin);
                }
                
                if (missclassMargin <= (1f - iterationError))
                {
                    break;
                }
                if (iteration == hardStop - 1)
                {
                    throw new NoSolutionException("Sorry, the system was unable to find a solution because it ran out of cycles");
                }
            }

            //fills in the newly found decision boundary
            getFunction();
        }

        internal void ResetWeights()
        {
            weights = new float[] { 0, 0.5f, 0.5f };
        }

        /// <summary>
        /// Checks what colour the system classifies the given point.
        /// HINT: only use after the system is trained
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <returns>Returns the calculated colour of the point.</returns>
        internal Color testPoint(int x1, int x2)
        {
            float Y = weights[0] + (x1 * weights[1]) + (x2 * weights[2]);
            if(Y > 0) //colour is red
            {
                return Color.Red;
            }
            else //colour is blue
            {
                return Color.Blue;
            }
        }

        /// <summary>
        /// Takes the weights and the bias.
        /// Calculates the decision boundary for the perceptron.
        /// </summary>
        /// <returns></returns>
        private void getFunction()
        {
            float xIntercept = -weights[0] / weights[2];
            float yIntercept = -weights[0] / weights[1];
            float m = yIntercept / xIntercept;
            this.Solution = new Straightline(m, xIntercept);
        }

        private void printLearnCycleOnConsole(int cycle, int amountOfMisclassified, float correctMargin)
        {
            Console.WriteLine(
                "Cycle:\t" + cycle +
                "\tweights:\t{" + weights[0] + ", " + weights[1] + ", " + weights[2] + "´}" +
                "\twrong:\t" + amountOfMisclassified + 
                "\twrong:\t" + correctMargin);
        }
    }
}
