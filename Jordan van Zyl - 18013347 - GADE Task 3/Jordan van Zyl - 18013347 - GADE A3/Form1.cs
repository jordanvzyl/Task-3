using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jordan_van_Zyl___18013347___GADE_A3
{
    public partial class Form1 : Form
    {
        // Declared objects
        GameEngine ge;
        Map map;
        Random rnd = new Random();

        // Global Variables
        bool flag;
        int size_X;
        int size_Y;

        // Accessor and set methods for the global variables
        public bool Flag { get => flag; set => flag = value; }
        public int Size_X { get => size_X; set => size_X = value; }
        public int Size_Y { get => size_Y; set => size_Y = value; }

        public Form1()
        {
            InitializeComponent();
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

        private void btnStart_Click(object sender, EventArgs e)
        {
            Flag = true;
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            Flag = false;
        }

        int time = 0;
        private void GameTime_Tick(object sender, EventArgs e)
        {
            if (gameStatus() == true)
            {
                time++;
                map.updatePosition();

                lblTime.Text = "" + time;
                lblMap.Text = map.redraw(Size_X, Size_Y);
                cmbUnits.Items.Clear();
                for (int i = 0; i < map.Unit.Length; i++)
                {
                    cmbUnits.Items.Add(map.UnitInfo(i).toString());

                }
                for (int j = 0; j < map.ArrRBuilding.Length; j++)
                {
                    // cmbUnits.Items.Add(map.BuildingInfo(j).toString());
                }
            }
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

        private void cmbUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
            rchUnitInfo.Text = cmbUnits.SelectedItem.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            map.Save();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cmbWidth_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
