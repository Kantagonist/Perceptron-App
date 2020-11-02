﻿using System;
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
        ModelGenerator modelGenerator = new ModelGenerator();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeComponent();
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
            string amount = textBox1.Text; //FIXME Text is empty even though textbox is filled
            int amt = 22;
            List<ModelPoint> model = modelGenerator.GenerateNewSet(amt, radioButton2.Checked);
            Console.WriteLine(modelGenerator.toString());

            //TODO draw the dataset on the canvas
            Graphics g = this.CreateGraphics();
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
                        g.DrawEllipse(blueP, 25 + (pointList[i].X * 7), 700 - (pointList[i].Y *7), 7, 7);
                    }
                    else
                    {
                        g.DrawEllipse(redP, 25 + (pointList[i].X * 7), 700 - (pointList[i].Y * 7), 7, 7);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}