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
            //initialize the weight vectors with 0
            Dictionary<ModelPoint, List<float>> weights = new Dictionary<ModelPoint, List<float>>();
            for(int i = 0; i < points.Count; ++i)
            {
                List<float> temp = new List<float>
                {
                    0,
                    0,
                    0
                };
                weights.Add(points[i], temp);
            }

            //initialize the threshold

            //actual learning, but capped with a hard stop if iteration error can not be reached
            int hardStop = 1000; //adjust at will
            for(int iteration = 0; iteration < hardStop; ++iteration)
            {
                foreach (KeyValuePair<ModelPoint, List<float>> entry in weights)
                {
                    //calculate the output of the threshold function

                    //update the weights

                }

                //calculate the iteration error

            }
        }

        private int threshold(Dictionary<ModelPoint, int> weights, float bias)
        {
            return 0; //TODO implement me
        }
    }
}
