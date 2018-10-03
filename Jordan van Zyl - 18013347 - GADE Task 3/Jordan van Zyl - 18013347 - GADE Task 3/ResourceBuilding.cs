using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jordan_van_Zyl___18013347___GADE_Task_3
{
    public class ResourceBuilding : Building
    {
        // Private fields
        private string resourceType;
        private int resourcePerTick = 2;
        private int resourceRemaining;

        // ResourceBuilding array
        ResourceBuilding[] arrRBuilding = new ResourceBuilding[4];

        // Map object
        Map map;

        public ResourceBuilding[] ArrRBuilding { get => arrRBuilding; set => arrRBuilding = value; }

        // ResourceBuilding constructor that inherits from Building class
        public ResourceBuilding(int pos_X, int pos_Y, int health, string team, string symbol, string resourceType, int resourceRemaning) : base(pos_X, pos_Y, health, team, symbol)
        {

        }

        // Override death method
        public override bool Death(Building rBuilding)
        {
            if (rBuilding.Health <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Method to generate and remove resources
        public void ManageResources(int GameTick)
        {
            if(GameTick % 3 == 0)
            {
                for(int i = 0; i < arrRBuilding.Length; i++)
                {
                    arrRBuilding[i].resourceRemaining -= 3;
                }
            }
        }

        // Override toString method
        public override string toString()
        {
            string s = "";
            s += "Building Type: Resource" + "\n" + "Resource Type: " + resourceType + "\n" + "X-Coordinate: " + Pos_X + "\n";
            s += "Y-Coordinate: " + Pos_Y + "\n" + "Heath: "+ Health + "\n" + "Team: " + Team + "\n" + "Symbol: " + Symbol + "\n";
            return s;
        }

        // Override Save method
        public override void Save()
        {
            if (Directory.Exists("saves") != true)
            {
                Directory.CreateDirectory("saves");
                Console.WriteLine("Directory created!");
            }

            FileStream file = new FileStream("saves/ResourceBuilding.file", FileMode.Append, FileAccess.Write);
            StreamWriter writer = new StreamWriter(file);
            writer.WriteLine(toString());
            writer.Close();
            file.Close();
        }
    }
}
