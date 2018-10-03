using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jordan_van_Zyl___18013347___GADE_Task_3
{
    public class Map
    {
        // Declared Arrays
        string[,] arrMap;
        Unit[] unit;
        ResourceBuilding[] arrRBuilding = new ResourceBuilding[4];

        // Declared Object types
        MeleeUnit m_Unit;
        RangedUnit r_Unit;
        ResourceBuilding r_Building;

        // Global variables
        int size_X;
        int size_Y;

        public int Size_X { get => size_X; set => size_X = value; }
        public int Size_Y { get => size_Y; set => size_Y = value; }

        public Unit[] Unit { get => unit; set => unit = value; }
        public string[,] ArrMap { get => arrMap; set => arrMap = value; }
        public ResourceBuilding[] ArrRBuilding { get => arrRBuilding; set => arrRBuilding = value; }

        public Map(int size_X, int size_Y)
        {
            ArrMap = new string[size_Y, size_X];
            for(int i = 0; i < size_Y; i++)
            {
                for(int j = 0; j < size_X; j++)
                {
                    ArrMap[i, j] = ".";
                }
            }

            Random rnd = new Random();

            // Randomly generate a number of units to placed on the map and instantiate the arrunit array
            int numUnits = rnd.Next(1, 51);
            Unit = new Unit[numUnits];

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

                        if (ArrMap[pos_Y, pos_X] == ".")
                        {

                            Unit[unitCount] = m_Unit;
                            ArrMap[pos_Y, pos_X] = symbol;
                            unitCount++;
                        }
                    }
                    else
                    {
                        team = "Villain";
                        symbol = "m";
                        m_Unit = new MeleeUnit(name2, pos_X, pos_Y, health, maxHealth, speed, attack, 1, team, symbol, isAttacking);

                        if (ArrMap[pos_Y, pos_X] == ".")
                        {

                            Unit[unitCount] = m_Unit;
                            ArrMap[pos_Y, pos_X] = symbol;
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

                        if (ArrMap[pos_Y, pos_X] == ".")
                        {
                            Unit[unitCount] = r_Unit;
                            ArrMap[pos_Y, pos_X] = symbol;
                            unitCount++;
                        }
                    }
                    else
                    {
                        team = "Villain";
                        symbol = "r";
                        r_Unit = new RangedUnit(name2, pos_X, pos_Y, health, maxHealth, speed, attack, atkRange, team, symbol, isAttacking);

                        if (ArrMap[pos_Y, pos_X] == ".")
                        {
                            Unit[unitCount] = r_Unit;
                            ArrMap[pos_Y, pos_X] = symbol;
                            unitCount++;
                        }
                    }
                }
            }
            CreateBuildings();
        }

        // Method to create buildings
        public void CreateBuildings()
        {
            Random rnd = new Random();
            bool flag = false;
            int buildings = 0;
            while (flag == false && buildings < 4)
            {
                int pos_X = rnd.Next(0, size_X);
                int pos_Y = rnd.Next(0, size_Y);
                int health = 100;
                int teamRoll = rnd.Next(1, 3);
                string team = "";
                string symbol = "";

                switch (teamRoll)
                {
                    case 1:
                        {
                            team = "Hero";
                            symbol = "B";
                        }
                        break;
                    case 2:
                        {
                            team = "Villain";
                            symbol = "b";
                        }
                        break;
                }

                string resourceType = "";
                int type = rnd.Next(1, 3);
                switch (type)
                {
                    case 1:
                        {
                            resourceType = "Health";
                        }
                        break;
                    case 2:
                        {
                            resourceType = "Extra damage";
                        }
                        break;
                }

                if (ArrMap[pos_Y, pos_X] == ".")
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }

                if (flag == true)
                {
                    ResourceBuilding r_Building = new ResourceBuilding(pos_X, pos_Y, health, team, symbol, resourceType, 30);
                    ArrRBuilding[buildings] = r_Building;
                    arrMap[pos_Y, pos_X] = symbol;
                    buildings++;
                }
            }
        }

        // Method to move units in the map array
        public void updatePosition()
        {
            Random rnd = new Random();
            for(int i = 0; i < Unit.Length; i++)
            {
                Unit individualUnit = Unit[i].closestUnit(Unit);
                if(Unit[i].withinAtkRange(individualUnit) == false)
                {
                    if (Unit[i].Pos_X < individualUnit.Pos_X)
                    {
                        Unit[i].newPosition(Unit[i].Pos_X + 1, Unit[i].Pos_Y);
                        ArrMap[Unit[i].Pos_Y, Unit[i].Pos_X] = Unit[i].Symbol;
                        ArrMap[Unit[i].Pos_Y, Unit[i].Pos_X - 1] = ".";
                    }
                    if(Unit[i].Pos_X > individualUnit.Pos_X)
                    {
                        Unit[i].newPosition(Unit[i].Pos_X - 1, Unit[i].Pos_Y);
                        ArrMap[Unit[i].Pos_Y, Unit[i].Pos_X] = Unit[i].Symbol;
                        ArrMap[Unit[i].Pos_Y, Unit[i].Pos_X + 1] = ".";
                    }
                    if(Unit[i].Pos_X == individualUnit.Pos_X)
                    {
                        if(Unit[i].Pos_Y < individualUnit.Pos_Y)
                        {
                            Unit[i].newPosition(Unit[i].Pos_X, Unit[i].Pos_Y + 1);
                            ArrMap[Unit[i].Pos_Y, Unit[i].Pos_X] = Unit[i].Symbol;
                            ArrMap[Unit[i].Pos_Y - 1, Unit[i].Pos_X] = ".";
                        }
                        if(Unit[i].Pos_Y > individualUnit.Pos_Y)
                        {
                            Unit[i].newPosition(Unit[i].Pos_X, Unit[i].Pos_Y - 1);
                            ArrMap[Unit[i].Pos_Y, Unit[i].Pos_X] = Unit[i].Symbol;
                            ArrMap[Unit[i].Pos_Y + 1, Unit[i].Pos_X] = ".";

                        }
                    }
                }
                if(Unit[i].withinAtkRange(individualUnit) == true)
                {
                    Unit[i].IsAttacking = true;
                    Unit[i].combat(individualUnit);
                    if(Unit[i].Health <= 0)
                    {
                        ArrMap[Unit[i].Pos_Y, Unit[i].Pos_X] = ".";
                        Unit[i] = null;
                    }
                    if(Unit[i].Health < 25)
                    {
                        int rndMovement = rnd.Next(1, 5);
                        switch(rndMovement)
                        {
                            case 1:
                                {
                                    // Move up
                                    if(Unit[i].Pos_Y - Unit[i].Speed <= 0)
                                    {
                                        Unit[i].newPosition(Unit[i].Pos_X, 0);
                                        ArrMap[Unit[i].Pos_Y, Unit[i].Pos_X] = Unit[i].Symbol;
                                        ArrMap[Unit[i].Pos_Y + 1, Unit[i].Pos_X] = ".";
                                    }
                                    else
                                    {
                                        Unit[i].newPosition(Unit[i].Pos_X, Unit[i].Pos_Y - Unit[i].Speed);
                                        ArrMap[Unit[i].Pos_Y, Unit[i].Pos_X] = Unit[i].Symbol;
                                        ArrMap[Unit[i].Pos_Y + Unit[i].Speed, Unit[i].Pos_X] = ".";
                                    }
                                    break;
                                }
                            case 2:
                                {
                                    // Move down
                                    if (Unit[i].Pos_Y + Unit[i].Speed >= Size_Y)
                                    {
                                        Unit[i].newPosition(Unit[i].Pos_X, Size_Y);
                                        ArrMap[Unit[i].Pos_Y, Unit[i].Pos_X] = Unit[i].Symbol;
                                        ArrMap[Unit[i].Pos_Y - 1, Unit[i].Pos_X] = ".";
                                    }
                                    else
                                    {
                                        Unit[i].newPosition(Unit[i].Pos_X, Unit[i].Pos_Y + Unit[i].Speed);
                                        ArrMap[Unit[i].Pos_Y, Unit[i].Pos_X] = Unit[i].Symbol;
                                        ArrMap[Unit[i].Pos_Y - Unit[i].Speed, Unit[i].Pos_X] = ".";
                                    }
                                    break;
                                }
                            case 3:
                                {
                                    // Move left
                                    if(Unit[i].Pos_X - Unit[i].Speed <= 0)
                                    {
                                        Unit[i].newPosition(0, Unit[i].Pos_Y);
                                        ArrMap[Unit[i].Pos_Y, Unit[i].Pos_X] = Unit[i].Symbol;
                                        ArrMap[Unit[i].Pos_Y + 1, Unit[i].Pos_X + 1] = ".";
                                    }
                                    else
                                    {
                                        Unit[i].newPosition(Unit[i].Pos_X + Unit[i].Speed, Unit[i].Pos_Y);
                                        ArrMap[Unit[i].Pos_Y, Unit[i].Pos_X] = Unit[i].Symbol;
                                        ArrMap[Unit[i].Pos_Y - Unit[i].Speed, Unit[i].Pos_X] = ".";
                                    }
                                    break;
                                }
                            case 4:
                                {
                                    // Move right
                                    if (Unit[i].Pos_X + Unit[i].Speed >= Size_X)
                                    {
                                        Unit[i].newPosition(Size_X, Unit[i].Pos_Y);
                                        ArrMap[Unit[i].Pos_Y, Unit[i].Pos_X] = Unit[i].Symbol;
                                        ArrMap[Unit[i].Pos_Y - 1, Unit[i].Pos_X + 1] = ".";
                                    }
                                    else
                                    {
                                        Unit[i].newPosition(Unit[i].Pos_X + Unit[i].Speed, Unit[i].Pos_Y);
                                        ArrMap[Unit[i].Pos_Y, Unit[i].Pos_X] = Unit[i].Symbol;
                                        ArrMap[Unit[i].Pos_Y - Unit[i].Speed, Unit[i].Pos_X] = ".";
                                    }
                                    break;
                                }
                        }
                    }
                }
            }
        }

        // Method to redraw the map
        public string redraw(int size_X, int size_Y)
        {
            string s = "";
            for (int i = 0; i < size_Y; i++)
            {
                for (int j = 0; j < size_X; j++)
                {
                    s += ArrMap[i, j];
                }
                s += "\n";
            }
            return s;
        }

        // Method to get the names of each unit for the combobox
        public Unit UnitInfo(int i)
        {
            return Unit[i];
        }

        public Building BuildingInfo(int i)
        {
            return ArrRBuilding[i];
        }
    }

    
}
