using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Perceptron_App
{
    public partial class Form1 : Form
    {
        private ModelGenerator modelGenerator = new ModelGenerator();
        List<Point> Line = new List<Point>();
        private Graphics g;
        Solver S = new Solver();
        
        //enables printing crucial local variables for debugging
        bool Debug = true;

        public Form1()
        {
            InitializeComponent();
        }

        private void RedrawModel(List<ModelPoint> model, Straightline decisionBoundary)
        {
            //draws the dataset on the canvas
            g = this.CreateGraphics();
            g.Clear(Color.White);
            Pen p = new Pen(Color.Black);
            Pen blueP = new Pen(Color.Blue);
            Pen redP = new Pen(Color.Red);

            //draws the coordinate system
            g.DrawLine(p, 25, 700, 25, 0);
            g.DrawLine(p, 25, 700, 725, 700);

            //draws the points 
            List<ModelPoint> pointList = modelGenerator.points;
            if (pointList.Count > 0)
            {
                for (int i = 0; i < pointList.Count; ++i)
                {
                    if (pointList[i].Color == Color.Blue)
                    {
                        g.DrawEllipse(blueP, 25 + (pointList[i].X * 7), 700 - (pointList[i].Y * 7), 7, 7);
                    }
                    else
                    {
                        g.DrawEllipse(redP, 25 + (pointList[i].X * 7), 700 - (pointList[i].Y * 7), 7, 7);
                    }
                }
            }

            //paints the straight line on the canvas
            if(decisionBoundary != null)
            {
                float[] line = new float[4];
                for (int i = 0; i <= 100; ++i)
                {
                    float yZero = decisionBoundary.F(i);
                    if (yZero >= 0 && yZero <= 1)
                    {
                        line[0] = i;
                        line[1] = (int)yZero;
                        for (int j = 0; j <= 100; ++j)
                        {
                            float yHundred = decisionBoundary.F(j);
                            if (yHundred >= 100 && yHundred <= 101)
                            {
                                line[2] = j;
                                line[3] = yHundred;
                                break;
                            }
                        }
                        break;
                    }
                }
                Pen pen = new Pen(Color.Blue);
                Point a = new Point((int)((line[0] * 7) + 25), (int)((line[1] * 7) + 25)); //point for 0 <= f(x) <= 1
                Point b = new Point((int)((line[2] * 7) + 25), (int)((line[3] * 7) + 25)); //point for 100 <= f(x) <= 101

                if (Debug) 
                {
                    Console.WriteLine("Translated Points:\t" + a.ToString() + "\t" + b.ToString());
                }

                g.DrawLine(pen, a, b);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //get amount of points generated
            string amount = textBox1.Text;
            try
            {
                //generating the model
                List<ModelPoint> model = modelGenerator.GenerateNewSet(int.Parse(amount), radioButton2.Checked);

                if (Debug) 
                { 
                    Console.WriteLine(modelGenerator.toString());
                }

                //drawing the newly generated model on the canvas
                RedrawModel(model, null);
            }
            catch (System.FormatException exception)
            {
                //do nothing
            }
        }

        /// <summary>
        /// Creates the line which divides the points.
        /// TODO call the training method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //solves the point list to a 95% accuracy
                Straightline Solution = S.FindSolutionFor(modelGenerator.points, Debug);

                if (Debug)
                {
                    Console.WriteLine(Solution.ToString());
                }

                RedrawModel(modelGenerator.points, Solution);
            }
            catch(NoSolutionException noSolutionException)
            {
                MessageBox.Show(noSolutionException.Message);
            }
        }

        /// <summary>
        /// Ignores all user input which isn't a digit key.
        /// Pasting in words is still possible, but the content of the textfield is parsed through int.
        /// This way, wrong input is prevented.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Tests the given Point on the canvas.
        /// Prints it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if(g == null)
            {
                return;
            }

            int x, y;
            if(textBox2.Text == "" || textBox3.Text == "")
            {
                try
                {
                    x = int.Parse(textBox2.Text);
                    y = int.Parse(textBox3.Text);
                    Color solution = S.TestPoint(x, y);
                    SolidBrush brush = new SolidBrush(solution);
                    //TODO find out why he doesn't paint
                    g.FillEllipse(brush, 25 + (x*7), 700 - (y*7), 7, 7 );
                    brush.Dispose();
                }catch(FormatException exception)
                {
                    MessageBox.Show("Please input a point between 0 and 99 into the fields for X and Y, otherwise the system recognizes a wrong input");
                }
            }
        }
    }
}