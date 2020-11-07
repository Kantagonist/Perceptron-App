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

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void solvableModel_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //generates the new dataset
            string amount = textBox1.Text;
            try
            {
                List<ModelPoint> model = modelGenerator.GenerateNewSet(int.Parse(amount), radioButton2.Checked);
                
                //prints the given model on the console (enable for debugging)
                //Console.WriteLine(modelGenerator.toString());

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
            Straightline Solution = Solver.FindSolutionFor(modelGenerator.points);
            int[] line = new int[4];
            for(int i = 0; i <= 100; ++i)
            {
                if(Solution.F(i) >= 0)
                {
                    line[0] = i;
                    line[1] = (int) Solution.F(i);
                    for(int j = i; j <= 100; ++j)
                    {
                        if(Solution.F(j) >= 100)
                        {
                            line[2] = j;
                            line[3] = (int)Solution.F(j);
                        }
                        break;
                    }
                    break;
                }
            }

            //remove the existing line, if there is one
            if(Line.Count == 2)
            {
                Line.RemoveAt(0);
                Line.RemoveAt(1);
            }

            //create a new line
            Pen pen = new Pen(Color.Blue);
            Point a = new Point((line[0] * 7) + 25, (line[1] * 7) + 25);
            Point b = new Point((line[2] * 7) + 25, (line[3] * 7) + 25);
            g.DrawLine(pen, a, b);
            Refresh();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }
    }
}