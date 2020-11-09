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
                int[] line = new int[4];
                for (int i = 0; i <= 100; ++i)
                {
                    if (decisionBoundary.F(i) >= 0)
                    {
                        line[0] = i;
                        line[1] = (int)decisionBoundary.F(i);
                        for (int j = i; j <= 100; ++j)
                        {
                            if (decisionBoundary.F(j) >= 100)
                            {
                                line[2] = j;
                                line[3] = (int)decisionBoundary.F(j);
                                break;
                            }
                        }
                        break;
                    }
                }
                Pen pen = new Pen(Color.Blue);
                Point a = new Point((line[0] * 7) + 25, (line[1] * 7) + 25);
                Point b = new Point((line[2] * 7) + 25, (line[3] * 7) + 25);
                //TODO fix the out of bound error, probably because the m in straightline doesn't translate
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
                
                //prints the given model on the console (enable for debugging)
                //Console.WriteLine(modelGenerator.toString());
                
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
            //solves the point list to a 95% accuracy
            Straightline Solution = S.FindSolutionFor(modelGenerator.points);
            RedrawModel(modelGenerator.points, Solution);
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

        
        private void button4_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            float m = rand.Next(1, 4);
            float b = 20f;
            Straightline y = new Straightline(m, b);
            RedrawModel(modelGenerator.points, y);
        }
    }
}