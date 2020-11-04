using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron_App
{
    class Perceptron
    {
        public Perceptron()
        {

        }

        /// <summary>
        /// Trains this instance of the Perceptron until the 
        /// </summary>
        /// <param name="points"></param>
        /// <param name="errorThreshold"></param>
        public void train(List<ModelPoint> points, float errorThreshold)
        {
            //set configs
            float[] weights = new float[] { 0, 0.5f, 0.5f };
            float learningRate = 0.1f;
            float iterationError = 0.95f;

            //actual learning, but capped with a hard stop if iteration error can not be reached
            float[] y = new float[points.Count];
            List<ModelPoint> misclassified = new List<ModelPoint>();
            int hardStop = 1000; //adjust at will
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
                float correctMargin = ((float)points.Count) / ((float)misclassified.Count);
                if (correctMargin >= iterationError) break;
            }

            //TODO find a way to make a graph out of the weights
        }
    }
}
