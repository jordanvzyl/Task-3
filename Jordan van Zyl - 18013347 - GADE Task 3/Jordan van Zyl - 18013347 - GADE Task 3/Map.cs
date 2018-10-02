using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jordan_van_Zyl___18013347___GADE_Task_3
{
    public class Map
    {
        string[,] arrMap;
        Unit[] unit;
        MeleeUnit m_Unit;
        RangedUnit r_Unit;

        public Map(int size_X, int size_Y)
        {
            arrMap = new string[size_Y, size_X];
            for(int i = 0; i < size_Y; i++)
            {
                for(int j = 0; j < size_X; j++)
                {
                    arrMap[i, j] = ".";
                }
            }

            Random rnd = new Random();

            // Randomly generate a number of units to placed on the map and instantiate the arrunit array
            int numUnits = rnd.Next(1, 51);
            unit = new Unit[numUnits];

            // Add the units to the map
            int unitCount = 0;
            while (unitCount < numUnits)
            {
                // Generate random properties for each unit
                int pos_X = rnd.Next(0, size_X);
                int pos_Y = rnd.Next(0, size_Y);
                int maxHealth = 100;
                int health = 100;
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
                        m_Unit = new MeleeUnit(name1, pos_X, pos_Y, health, maxHealth, speed, attack, 1, team, symbol, isAttacking);

                        if (arrMap[pos_Y, pos_X] == ".")
                        {

                            unit[unitCount] = m_Unit;
                            arrMap[pos_Y, pos_X] = symbol;
                            unitCount++;
                        }
                    }
                    else
                    {
                        team = "Villain";
                        symbol = "m";
                        m_Unit = new MeleeUnit(name2, pos_X, pos_Y, health, maxHealth, speed, attack, 1, team, symbol, isAttacking);

                        if (arrMap[pos_Y, pos_X] == ".")
                        {

                            unit[unitCount] = m_Unit;
                            arrMap[pos_Y, pos_X] = symbol;
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
                        r_Unit = new RangedUnit(name1, pos_X, pos_Y, health, maxHealth, speed, attack, atkRange, team, symbol, isAttacking);

                        if (arrMap[pos_Y, pos_X] == ".")
                        {
                            unit[unitCount] = r_Unit;
                            arrMap[pos_Y, pos_X] = symbol;
                            unitCount++;
                        }
                    }
                    else
                    {
                        team = "Villain";
                        symbol = "r";
                        r_Unit = new RangedUnit(name2, pos_X, pos_Y, health, maxHealth, speed, attack, atkRange, team, symbol, isAttacking);

                        if (arrMap[pos_Y, pos_X] == ".")
                        {
                            unit[unitCount] = r_Unit;
                            arrMap[pos_Y, pos_X] = symbol;
                            unitCount++;
                        }
                    }
                }
            }
        }

        // Method to move unit in the map
        public void updatePosition()
        {
            for(int i = 0; i < unit.Length; i++)
            {
                Unit individualUnit = unit[i].closestUnit(unit);
                if(unit[i].withinAtkRange(individualUnit) == false)
                {
                    if (unit[i].Pos_X < individualUnit.Pos_X)
                    {
                        unit[i].newPosition(unit[i].Pos_X + 1, unit[i].Pos_Y);
                        arrMap[unit[i].Pos_Y, unit[i].Pos_X] = unit[i].Symbol;
                        arrMap[unit[i].Pos_Y, unit[i].Pos_X - 1] = ".";
                    }
                    if(unit[i].Pos_X > individualUnit.Pos_X)
                    {
                        unit[i].newPosition(unit[i].Pos_X - 1, unit[i].Pos_Y);
                        arrMap[unit[i].Pos_Y, unit[i].Pos_X] = unit[i].Symbol;
                        arrMap[unit[i].Pos_Y, unit[i].Pos_X + 1] = ".";
                    }
                    if(unit[i].Pos_X == individualUnit.Pos_X)
                    {
                        if(unit[i].Pos_Y < individualUnit.Pos_Y)
                        {
                            unit[i].newPosition(unit[i].Pos_X, unit[i].Pos_Y + 1);
                            arrMap[unit[i].Pos_Y, unit[i].Pos_X] = unit[i].Symbol;
                            arrMap[unit[i].Pos_Y - 1, unit[i].Pos_X] = ".";
                        }
                        if(unit[i].Pos_Y > individualUnit.Pos_Y)
                        {
                            unit[i].newPosition(unit[i].Pos_X, unit[i].Pos_Y - 1);
                            arrMap[unit[i].Pos_Y, unit[i].Pos_X] = unit[i].Symbol;
                            arrMap[unit[i].Pos_Y + 1, unit[i].Pos_X] = ".";

                        }
                    }
                }
            }
        }

        public string redraw(int size_X, int size_Y)
        {
            string s = "";
            for (int i = 0; i < size_Y; i++)
            {
                for (int j = 0; j < size_X; j++)
                {
                    s += arrMap[i, j];
                }
                s += "\n";
            }
            return s;
        }
    }

    
}
