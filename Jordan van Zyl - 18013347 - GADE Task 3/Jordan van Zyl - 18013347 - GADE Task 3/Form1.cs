using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jordan_van_Zyl___18013347___GADE_Task_3
{
    public partial class Form1 : Form
    {
        GameEngine ge;
        Map map;
        Random rnd = new Random();
        bool flag;
        int size_X;
        int size_Y;

        public bool Flag { get => flag; set => flag = value; }
        public int Size_X { get => size_X; set => size_X = value; }
        public int Size_Y { get => size_Y; set => size_Y = value; }

        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Flag = true;
            
        }

        // Boolean method to return status of the game (paused/unpaused)
        public bool gameStatus()
        {
            bool status = false;
            if (Flag == true)
            {
                status = true;
            }
            else
            {
                status = false;
            }
            return status;
        }

        int time = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (gameStatus() == true)
            {
                time++;
                map.updatePosition();
                lblTime.Text = "" + time;
                lblMap.Text = map.redraw(Size_X, Size_Y);
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            Flag = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int size_X = rnd.Next(10, 21);
            int size_Y = rnd.Next(10, 21);

            Size_X = size_X;
            Size_Y = size_Y;

            int lowest = 0;
            int highest = 0;

            if (size_X > size_Y)
            {
                highest = size_X;
                lowest = size_Y;
            }
            else
            {
                highest = size_Y;
                lowest = size_X;
            }

            int numUnits = rnd.Next(lowest, highest);
            int final_X = rnd.Next(lowest, highest + 1);
            int final_Y = rnd.Next(lowest, highest + 1);
            ge = new GameEngine(size_X, size_Y);
            map = new Map(size_X, size_Y);
            lblMap.Text += map.redraw(size_X, size_Y);
        }
    }
}
