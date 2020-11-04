using System;
using System.Collections.Generic;
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
            float[] initialWeights = new float[] { 0, 0.5f, 0.5f };
            float learningRate = 0.1f;

            //initialize the weight vectors with 0
            Dictionary<ModelPoint, float[]> weights = new Dictionary<ModelPoint, float[]>();
            for(int i = 0; i < points.Count; ++i)
            {
                weights.Add(points[i], initialWeights);
            }

            //initialize the threshold

            //actual learning, but capped with a hard stop if iteration error can not be reached
            float[] y = new float[points.Count];
            int hardStop = 1000; //adjust at will
            for(int iteration = 0; iteration < hardStop; ++iteration)
            {
                //calculate the output of the threshold function
                int k = 0;
                foreach (KeyValuePair<ModelPoint, float[]> entry in weights)
                {    
                    y[k++] = entry.Value[0] + (entry.Key.X * entry.Value[1]) + (entry.Key.Y * entry.Value[2]);
                }

                //calculate the iteration error, break if enough

                //update the weights
            }
        }

        private int threshold(Dictionary<ModelPoint, int> weights, float bias)
        {
            return 0; //TODO implement me
        }
    }
}
