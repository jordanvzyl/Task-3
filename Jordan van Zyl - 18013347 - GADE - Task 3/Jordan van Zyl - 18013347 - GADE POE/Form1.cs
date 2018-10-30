using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jordan_van_Zyl___18013347___GADE_POE
{
    public partial class Game : Form
    {
        // Declare objects
        Random rnd = new Random();
        GameEngine ge;

        public Game()
        {
            InitializeComponent();
        }

        private void Game_Load(object sender, EventArgs e)
        {
            int numUnits = rnd.Next(15, 31);
            int numBuildings = 4;

            ge = new GameEngine(numUnits, numBuildings);
            lblMap.Text = ge.Redraw();
        }

        int time = 0;
        private void GameTime_Tick(object sender, EventArgs e)
        {
            rchDisplay.Clear();
            cmbUnits.Items.Clear();
            cmbUnits.Items.Add(ge.getUnitInfo());
            int count = 0;
            // Add info to combobox
            for (int i = 0; i < ge.getArray().Length; i++)
            {
                if (ge.getArray()[i] != null)
                {
                    string[] unit = ge.getArray()[i].GetType().ToString().Split('.');
                    string type = unit[unit.Length - 1];
                    if (type == "RangedUnit")
                    {
                        RangedUnit r = (RangedUnit)ge.getArray()[i];
                        cmbUnits.Items.Add(r.toString());
                        count++;
                    }
                    if(type == "MeleeUnit")
                    {
                        MeleeUnit m = (MeleeUnit)ge.getArray()[i];
                        cmbUnits.Items.Add(m.toString());
                        count++;
                    }
                    if(type == "ArtilleryUnit")
                    {
                        ArtilleryUnit ar = (ArtilleryUnit)ge.getArray()[i];
                        cmbUnits.Items.Add(ar.toString());
                        count++;
                    }
                    if(type == "MedicUnit")
                    {
                        MedicUnit me = (MedicUnit)ge.getArray()[i];
                        cmbUnits.Items.Add(me.toString());
                        count++;
                    }
                    if(type == "ReconUnit")
                    {
                        ReconUnit re = (ReconUnit)ge.getArray()[i];
                        cmbUnits.Items.Add(re.toString());
                        count++;
                    }
                    if(type == "StormTrooperUnit")
                    {
                        StormTrooperUnit st = (StormTrooperUnit)ge.getArray()[i];
                        cmbUnits.Items.Add(st.toString());
                        count++;
                    }
                    if(type == "SupportUnit")
                    {
                        SupportUnit su = (SupportUnit)ge.getArray()[i];
                        cmbUnits.Items.Add(su.toString());
                        count++;
                    }

                }
            }
            rchDisplay.Text = ge.display(time) + ge.getNum() + "\n" + "Actual size: " + ge.getArrSize() + "\n" + "Combobox size: " + count;
            ge.playGame(time);
            lblMap.Text = ge.Redraw();
            time++;
        }

            public void cmbUnits_SelectedIndexChanged(object sender, EventArgs e)
            {

            }
        }
    }

