using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jordan_van_Zyl___18013347___GADE_A3
{
    class Map
    {
        // Declared Arrays
        string[,] arrMap;
        Unit[] unit;
        ResourceBuilding[] arrRBuilding = new ResourceBuilding[4];

        // Declared Object types
        MeleeUnit m_Unit;
        RangedUnit r_Unit;
        ResourceBuilding r_Building;
        FactoryBuilding f_Building;

        Random rnd = new Random();

        // Global variables
        int size_X;
        int size_Y;
        int numUnits;

        public int Size_X { get => size_X; set => size_X = value; }
        public int Size_Y { get => size_Y; set => size_Y = value; }

        public Unit[] Unit { get => unit; set => unit = value; }
        public string[,] ArrMap { get => arrMap; set => arrMap = value; }
        public ResourceBuilding[] ArrRBuilding { get => arrRBuilding; set => arrRBuilding = value; }
        public int NumUnits { get => numUnits; set => numUnits = value; }

        public Map(int size_X, int size_Y)
        {
            ArrMap = new string[size_Y, size_X];
            for (int i = 0; i < size_Y; i++)
            {
                for (int j = 0; j < size_X; j++)
                {
                    ArrMap[i, j] = ".";
                }
            }

            //CreateRBuildings();
            //SpawnFactory();

            // Random rnd = new Random();

            // Randomly generate a number of units to placed on the map and instantiate the arrunit array
            NumUnits = rnd.Next(1, 51);
            Unit = new Unit[NumUnits];

            // Add the units to the map
            int unitCount = 0;
            while (unitCount < NumUnits)
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
            
        }

        // Method to create buildings
        public void CreateRBuildings()
        {
            // Random rnd = new Random();
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

        public void SpawnFactory()
        {
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
                            symbol = "F";
                        }
                        break;
                    case 2:
                        {
                            team = "Villain";
                            symbol = "f";
                        }
                        break;
                }

                int unitsToSpawn = rnd.Next(5, 11);
                int ticksProduction = rnd.Next(2, 4);

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
                    FactoryBuilding f_Building = new FactoryBuilding(pos_X, pos_Y, health, team, symbol, unitsToSpawn, ticksProduction);
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
            for (int i = 0; i < Unit.Length; i++)
            {
                Unit individualUnit = Unit[i].closestUnit(Unit);
                if (Unit[i].withinAtkRange(individualUnit) == false)
                {
                    if (Unit[i].Pos_X < individualUnit.Pos_X)
                    {
                        Unit[i].newPosition(Unit[i].Pos_X + 1, Unit[i].Pos_Y);
                        ArrMap[Unit[i].Pos_Y, Unit[i].Pos_X] = Unit[i].Symbol;
                        ArrMap[Unit[i].Pos_Y, Unit[i].Pos_X - 1] = ".";
                    }
                    if (Unit[i].Pos_X > individualUnit.Pos_X)
                    {
                        Unit[i].newPosition(Unit[i].Pos_X - 1, Unit[i].Pos_Y);
                        ArrMap[Unit[i].Pos_Y, Unit[i].Pos_X] = Unit[i].Symbol;
                        ArrMap[Unit[i].Pos_Y, Unit[i].Pos_X + 1] = ".";
                    }
                    if (Unit[i].Pos_X == individualUnit.Pos_X)
                    {
                        if (Unit[i].Pos_Y < individualUnit.Pos_Y)
                        {
                            Unit[i].newPosition(Unit[i].Pos_X, Unit[i].Pos_Y + 1);
                            ArrMap[Unit[i].Pos_Y, Unit[i].Pos_X] = Unit[i].Symbol;
                            ArrMap[Unit[i].Pos_Y - 1, Unit[i].Pos_X] = ".";
                        }
                        if (Unit[i].Pos_Y > individualUnit.Pos_Y)
                        {
                            Unit[i].newPosition(Unit[i].Pos_X, Unit[i].Pos_Y - 1);
                            ArrMap[Unit[i].Pos_Y, Unit[i].Pos_X] = Unit[i].Symbol;
                            ArrMap[Unit[i].Pos_Y + 1, Unit[i].Pos_X] = ".";

                        }
                    }
                }
                if (Unit[i].withinAtkRange(individualUnit) == true)
                {
                    Unit[i].IsAttacking = true;
                    Unit[i].combat(individualUnit);
                    if (Unit[i].Health <= 0)
                    {
                        ArrMap[Unit[i].Pos_Y, Unit[i].Pos_X] = ".";
                        Unit[i].Health = 0;
                    }
                    if (Unit[i].Health < 25)
                    {
                        int rndMovement = rnd.Next(1, 5);
                        switch (rndMovement)
                        {
                            case 1:
                                {
                                    // Move up
                                    if (Unit[i].Pos_Y - Unit[i].Speed <= 0)
                                    {
                                        Unit[i].newPosition(Unit[i].Pos_X, 0);
                                        ArrMap[Unit[i].Pos_Y, Unit[i].Pos_X] = Unit[i].Symbol;
                                        if(Unit[i].Pos_Y + 1 <= Size_Y)
                                        {
                                            ArrMap[Unit[i].Pos_Y + 1, Unit[i].Pos_X] = ".";
                                        } 
                                    }
                                    else
                                    {
                                        Unit[i].newPosition(Unit[i].Pos_X, Unit[i].Pos_Y - Unit[i].Speed);
                                        ArrMap[Unit[i].Pos_Y, Unit[i].Pos_X] = Unit[i].Symbol;
                                        if(Unit[i].Pos_Y + Unit[i].Speed <= Size_Y)
                                        {
                                            ArrMap[Unit[i].Pos_Y + Unit[i].Speed, Unit[i].Pos_X] = ".";
                                        }
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
                                        if(Unit[i].Pos_Y - 1 >= 0)
                                        {
                                            ArrMap[Unit[i].Pos_Y - Unit[i].Speed, Unit[i].Pos_X] = ".";
                                        }
                                    }
                                    else
                                    {
                                        Unit[i].newPosition(Unit[i].Pos_X, Unit[i].Pos_Y + Unit[i].Speed);
                                        ArrMap[Unit[i].Pos_Y, Unit[i].Pos_X] = Unit[i].Symbol;
                                        if(Unit[i].Pos_Y - Unit[i].Speed >= 0)
                                        {
                                            ArrMap[Unit[i].Pos_Y - Unit[i].Speed, Unit[i].Pos_X] = ".";
                                        } 
                                    }
                                    break;
                                }
                            case 3:
                                {
                                    // Move left
                                    if (Unit[i].Pos_X - Unit[i].Speed <= 0)
                                    {
                                        Unit[i].newPosition(0, Unit[i].Pos_Y);
                                        ArrMap[Unit[i].Pos_Y, Unit[i].Pos_X] = Unit[i].Symbol;
                                        if(Unit[i].Pos_Y + 1 <= Size_X)
                                        {
                                            ArrMap[Unit[i].Pos_Y, Unit[i].Pos_X + 1] = ".";
                                        }
                                    }
                                    else
                                    {
                                        Unit[i].newPosition(Unit[i].Pos_X + Unit[i].Speed, Unit[i].Pos_Y);
                                        ArrMap[Unit[i].Pos_Y, Unit[i].Pos_X] = Unit[i].Symbol;
                                        if (Unit[i].Pos_Y + 1 <= Size_X)
                                        {
                                            ArrMap[Unit[i].Pos_Y, Unit[i].Pos_X + 1] = ".";
                                        }
                                        ArrMap[Unit[i].Pos_Y, Unit[i].Pos_X - Unit[i].Speed] = ".";
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
                                        ArrMap[Unit[i].Pos_Y, Unit[i].Pos_X - 1] = ".";
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

        // Method to get the details for each building for the combobox
        public Building BuildingInfo(int i)
        {
            return ArrRBuilding[i];
        }

        // Save method
        public void Save()
        {
            for (int i = 0; i < Unit.Length; i++)
            {
                string[] type = Unit[i].GetType().ToString().Split(',');
                string finalType = type[type.Length - 1];
                if (finalType == "RangedUnit")
                {
                    r_Unit.Save();
                    Console.WriteLine(Unit[i].ToString());
                }
                else
                {
                    m_Unit.Save();
                    Console.WriteLine(Unit[i].ToString());
                }
            }

            //f_Building.Save();
            //r_Building.Save();
        }

        // Read method to read in values from files
        public void Read()
        {
            // Read in MeleeUnits
            FileStream fileMeleeUnit = new FileStream("saves/MeleeUnit.file", FileMode.Open, FileAccess.Read);
            StreamReader readerMelee = new StreamReader(fileMeleeUnit);
            string line = readerMelee.ReadLine();
            while (line != null)
            {
                string[] unit = line.Split(',');
                string name = unit[0];
                int pos_X = Convert.ToInt32(unit[1]);
                int pos_Y = Convert.ToInt32(unit[2]);
                int health = Convert.ToInt32(unit[3]);
                int maxHealth = Convert.ToInt32(unit[4]);
                int speed = Convert.ToInt32(unit[5]);
                int attack = Convert.ToInt32(unit[6]);
                int atkRange = Convert.ToInt32(unit[7]);
                string team = unit[8];
                string symbol = unit[9];
                bool isAttacking = Convert.ToBoolean(unit[10]);

                MeleeUnit newMelee = new MeleeUnit(name, pos_X, pos_Y, health, maxHealth, speed, attack, atkRange, team, symbol, isAttacking);
                Console.WriteLine(newMelee.toString());

                line = readerMelee.ReadLine();
            }
            readerMelee.Close();
            fileMeleeUnit.Close();

            // Read in RangedUnits
            FileStream fileRangedUnit = new FileStream("saves/RangedUnit.file", FileMode.Open, FileAccess.Read);
            StreamReader readerRanged = new StreamReader(fileRangedUnit);
            string line2 = readerRanged.ReadLine();
            while (line2 != null)
            {
                string[] unit = line2.Split(',');
                string name = unit[0];
                int pos_X = Convert.ToInt32(unit[1]);
                int pos_Y = Convert.ToInt32(unit[2]);
                int health = Convert.ToInt32(unit[3]);
                int maxHealth = Convert.ToInt32(unit[4]);
                int speed = Convert.ToInt32(unit[5]);
                int attack = Convert.ToInt32(unit[6]);
                int atkRange = Convert.ToInt32(unit[7]);
                string team = unit[8];
                string symbol = unit[9];
                bool isAttacking = Convert.ToBoolean(unit[10]);

                RangedUnit newRanged = new RangedUnit(name, pos_X, pos_Y, health, maxHealth, speed, attack, atkRange, team, symbol, isAttacking);
                Console.WriteLine(newRanged.toString());

                line2 = readerRanged.ReadLine();
            }
            readerRanged.Close();
            fileRangedUnit.Close();

            // Read in resource building
            FileStream fileResource = new FileStream("saves/ResourceBuilding.file", FileMode.Open, FileAccess.Read);
            StreamReader readerResource = new StreamReader(fileResource);
            string line3 = readerResource.ReadLine();
            while (line3 != null)
            {
                string[] unit = line3.Split(',');
                string resourceType = unit[0];
                int pos_X = Convert.ToInt32(unit[1]);
                int pos_Y = Convert.ToInt32(unit[2]);
                int health = Convert.ToInt32(unit[3]);
                string team = unit[4];
                string symbol = unit[5];
                int resourceRemain = Convert.ToInt32(unit[6]);


                ResourceBuilding newResource = new ResourceBuilding(pos_X, pos_Y, health, team, symbol, resourceType, resourceRemain);
                Console.WriteLine(newResource.toString());

                line3 = readerResource.ReadLine();
            }
            readerResource.Close();
            fileResource.Close();

            // Read in factory building
            FileStream fileFactory = new FileStream("saves/FactoryBuilding.file", FileMode.Open, FileAccess.Read);
            StreamReader readerFactory = new StreamReader(fileFactory);
            string line4 = readerFactory.ReadLine();
            while (line4 != null)
            {
                string[] unit = line4.Split(',');
                int unitsToProduce = Convert.ToInt32(unit[0]);
                int pos_X = Convert.ToInt32(unit[1]);
                int pos_Y = Convert.ToInt32(unit[2]);
                int health = Convert.ToInt32(unit[3]);
                string team = unit[4];
                string symbol = unit[5];
                int ticksPerProduce = Convert.ToInt32(unit[6]);

                FactoryBuilding newFactory = new FactoryBuilding(pos_X, pos_Y, health, team, symbol, unitsToProduce, ticksPerProduce);
                Console.WriteLine(newFactory.toString());

                line4 = readerFactory.ReadLine();
            }
            readerFactory.Close();
            fileFactory.Close();
        }
    }
}
