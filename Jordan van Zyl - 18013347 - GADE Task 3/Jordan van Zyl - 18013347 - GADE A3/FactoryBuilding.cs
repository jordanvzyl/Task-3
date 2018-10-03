﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jordan_van_Zyl___18013347___GADE_A3
{
    class FactoryBuilding : Building
    {
        // Private fields
        private int unitsToProduce;
        private int ticksPerProduction;
        private int spawn_X;
        private int spawn_Y;

        // Map object
        Map map;

        // FactoryBuilding constructor that inherits from Building class
        public FactoryBuilding(int pos_X, int pos_Y, int health, string team, string symbol, int unitsToProduce, int ticksProduction) : base(pos_X, pos_Y, health, team, symbol)
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

        // Method to spawn a factory
        public void SpawnFactory()
        {

        }

        // Method to generate and remove resources
        public void SpawnUnits(int GameTick, int unitsProduce, int ticksProduction)
        {
            Random rnd = new Random();
            unitsToProduce = unitsProduce;
            // Add the units to the map
            int unitCount = 0;
            if (GameTick % ticksProduction == 0)
            {
                while (unitCount < unitsToProduce)
                {
                    // Generate random properties for each unit
                    int pos_X = rnd.Next(0, map.Size_X);
                    int pos_Y = rnd.Next(0, map.Size_Y);
                    int health = 100;
                    int maxHealth = 100;
                    int speed = rnd.Next(1, 3);
                    int attack = rnd.Next(1, 5);
                    int atkRange = rnd.Next(1, 11);
                    string team = "";
                    string symbol = "";
                    bool isAttacking = false;
                    string name1 = "Steve";
                    string name2 = "Bob";

                    // Randomise the unit type that will be added to the array
                    int unitType = rnd.Next(1, 3);
                    int typeteam = rnd.Next(1, 3);

                    // Add a Melee unit to the map array
                    if (unitType == 1)
                    {
                        if (typeteam == 1)
                        {
                            team = "Hero";
                            symbol = "M";
                            MeleeUnit M_Unit = new MeleeUnit(name1, pos_X, pos_Y, health, maxHealth, speed, attack, 1, team, symbol, isAttacking);

                            if (map.ArrMap[pos_Y, pos_X] == ".")
                            {
                                map.NumUnits += 1;
                                int newLength = map.Unit.Length;
                                map.Unit[newLength] = M_Unit;
                                map.ArrMap[pos_Y, pos_X] = symbol;
                                unitCount++;
                            }
                        }
                        else
                        {
                            team = "Villain";
                            symbol = "m";
                            MeleeUnit M_Unit = new MeleeUnit(name2, pos_X, pos_Y, health, maxHealth, speed, attack, 1, team, symbol, isAttacking);

                            if (map.ArrMap[pos_Y, pos_X] == ".")
                            {
                                map.NumUnits += 1;
                                int newLength = map.Unit.Length;
                                map.Unit[newLength] = M_Unit;
                                map.ArrMap[pos_Y, pos_X] = symbol;
                                unitCount++;
                            }
                        }
                    }
                    // Add a Ranged unit to the map array
                    else
                    {
                        if (typeteam == 1)
                        {
                            team = "Hero";
                            symbol = "R";
                            RangedUnit R_Unit = new RangedUnit(name1, pos_X, pos_Y, health, maxHealth, speed, attack, atkRange, team, symbol, isAttacking);

                            if (map.ArrMap[pos_Y, pos_X] == ".")
                            {
                                map.NumUnits += 1;
                                int newLength = map.Unit.Length;
                                map.Unit[unitCount] = R_Unit;
                                map.ArrMap[pos_Y, pos_X] = symbol;
                                unitCount++;
                            }
                        }
                        else
                        {
                            team = "Villain";
                            symbol = "r";
                            RangedUnit R_Unit = new RangedUnit(name2, pos_X, pos_Y, maxHealth, health, speed, attack, atkRange, team, symbol, isAttacking);

                            if (map.ArrMap[pos_Y, pos_X] == ".")
                            {
                                map.NumUnits += 1;
                                int newLength = map.Unit.Length;
                                map.Unit[unitCount] = R_Unit;
                                map.ArrMap[pos_Y, pos_X] = symbol;
                                unitCount++;
                            }
                        }
                    }
                }
            }
        }

        // Override toString method
        public override string toString()
        {
            string s = "";
            s += unitsToProduce + "," + spawn_X + "," + spawn_Y + "," + Health + "," + Team + "," + Symbol + "\n";
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

            FileStream file = new FileStream("saves/FactoryBuilding.file", FileMode.Append, FileAccess.Write);
            StreamWriter writer = new StreamWriter(file);
            writer.WriteLine(toString());
            writer.Close();
            file.Close();
        }
    }
}
