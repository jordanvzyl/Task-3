using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jordan_van_Zyl___18013347___GADE_POE
{
    class FactoryBuilding : Building
    {
        // Private fields
        private int unitsToProduce;
        private int ticksPerProduction;
        private int spawn_X;
        private int spawn_Y;

        // Map object

        // FactoryBuilding constructor that inherits from Building class
        public FactoryBuilding(int pos_X, int pos_Y, int health, string team, string symbol) : base(pos_X, pos_Y, health, team, symbol)
        {
            this.ticksPerProduction = 4;
            this.unitsToProduce = 10;
        }

        // Accessor methods
        public int Pos_X
        {
            get
            {
                return pos_X;
            }

            set
            {
                pos_X = value;
            }
        }

        public int Pos_Y
        {
            get
            {
                return pos_Y;
            }

            set
            {
                pos_Y = value;
            }
        }

        public int Health
        {
            get
            {
                return health;
            }

            set
            {
                health = value;
            }
        }

        public string Team
        {
            get
            {
                return team;
            }
            set
            {
                team = value;
            }
        }

        public string Symbol
        {
            get
            {
                return symbol;
            }
            set
            {
                symbol = value;
            }
        }

        public int TicksPerProduction
        {
            get
            {
                return ticksPerProduction;
            }
        }

        public int UnitsToProduce
        {
            get
            {
                return unitsToProduce;
            }
            set
            {
                unitsToProduce = value;
            }
        }

        // Override death method
        public override bool Death()
        {
            if (this.Health <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Method to generate and remove resources
        public Unit SpawnUnits(string[,] arrMap, int currentResources)
        {
            Random rnd = new Random();
            Unit newUnit = null;
            // Add the units to the map
            if (this.UnitsToProduce > 0 && currentResources % 4 == 0)
            {
                // Generate random properties for each unit
                int health = 100;
                int maxHealth = 100;
                int speed = rnd.Next(1, 3);
                int attack = rnd.Next(1, 3);
                int atkRange = rnd.Next(1, 5);
                string team = "";
                string symbol = "";
                bool isAttacking = false;
                int nameOption = rnd.Next(1, 11);
                string name = "";
                switch (nameOption)
                {
                    case 1:
                        {
                            name = "Josh";
                        }
                        break;
                    case 2:
                        {
                            name = "Cameron";
                        }
                        break;
                    case 3:
                        {
                            name = "Luke";
                        }
                        break;
                    case 4:
                        {
                            name = "Andrew";
                        }
                        break;
                    case 5:
                        {
                            name = "Matthew";
                        }
                        break;
                    case 6:
                        {
                            name = "Liall";
                        }
                        break;
                    case 7:
                        {
                            name = "Paul";
                        }
                        break;
                    case 8:
                        {
                            name = "Jesse";
                        }
                        break;
                    case 9:
                        {
                            name = "Peter";
                        }
                        break;
                    case 10:
                        {
                            name = "Will";
                        }
                        break;
                }

                // Randomise the unit type that will be added to the array
                int unitType = rnd.Next(1, 8);
                int typeteam = rnd.Next(1, 3);

                // Determine unit spawn points
                if (this.Pos_Y > 0)
                {
                    spawn_Y = this.Pos_Y - 1;
                    spawn_X = this.Pos_Y;
                }

                // Add a Melee unit to the map array
                if (unitType == 1)
                {
                    if (typeteam == 1)
                    {
                        team = "Hero";
                        symbol = "M";

                        if (arrMap[spawn_Y, spawn_X] == "." && spawn_Y != 0)
                        {
                            MeleeUnit M_Unit = new MeleeUnit(name, spawn_X, spawn_Y, health, maxHealth, speed, attack, 1, team, symbol, isAttacking);
                            this.UnitsToProduce--;
                            newUnit = M_Unit;
                        }
                    }
                    else
                    {
                        team = "Villain";
                        symbol = "m";

                        if (arrMap[spawn_Y, spawn_X] == "." && spawn_Y != 0)
                        {
                            MeleeUnit M_Unit = new MeleeUnit(name, spawn_X, spawn_Y, health, maxHealth, speed, attack, 1, team, symbol, isAttacking);
                            this.UnitsToProduce--;
                            newUnit = M_Unit;
                        }
                    }
                }
                // Add a Ranged unit to the map array
                if (unitType == 2)
                {
                    if (typeteam == 1)
                    {
                        team = "Hero";
                        symbol = "R";

                        if (arrMap[spawn_Y, spawn_X] == "." && spawn_Y != 0)
                        {
                            RangedUnit R_Unit = new RangedUnit(name, spawn_X, spawn_Y, health, maxHealth, speed, attack, atkRange, team, symbol, isAttacking);
                            this.UnitsToProduce--;
                            newUnit = R_Unit;
                        }
                    }
                    else
                    {
                        team = "Villain";
                        symbol = "r";

                        if (arrMap[spawn_Y, spawn_X] == "." && spawn_Y != 0)
                        {
                            RangedUnit R_Unit = new RangedUnit(name, spawn_X, spawn_Y, maxHealth, health, speed, attack, atkRange, team, symbol, isAttacking);
                            this.UnitsToProduce--;
                            newUnit = R_Unit;
                        }
                    }
                }
                // Add an Artillery unit to the map array

                if (unitType == 3)
                {
                    if (typeteam == 1)
                    {
                        team = "Hero";
                        symbol = "A";

                        if (arrMap[pos_Y, pos_X] == ".")
                        {
                            ArtilleryUnit ar_Unit = new ArtilleryUnit(name, pos_X, pos_Y, health, maxHealth, speed, 5, 3, team, symbol, isAttacking);
                            this.UnitsToProduce--;
                            newUnit = ar_Unit;
                        }
                    }
                    else
                    {
                        team = "Villain";
                        symbol = "a";

                        if (arrMap[pos_Y, pos_X] == ".")
                        {
                            ArtilleryUnit ar_Unit = new ArtilleryUnit(name, pos_X, pos_Y, health, maxHealth, speed, 5, 3, team, symbol, isAttacking);
                            this.UnitsToProduce--;
                            newUnit = ar_Unit;
                        }
                    }
                    // Add a Medic unit to the map array
                }
                if (unitType == 4)
                {
                    if (typeteam == 1)
                    {
                        team = "Hero";
                        symbol = "E";

                        if (arrMap[pos_Y, pos_X] == ".")
                        {
                            MedicUnit me_Unit = new MedicUnit(name, pos_X, pos_Y, health, maxHealth, 2, attack, 1, team, symbol, isAttacking);
                            this.UnitsToProduce--;
                            newUnit = me_Unit;
                        }
                    }
                    else
                    {
                        team = "Villain";
                        symbol = "e";

                        if (arrMap[pos_Y, pos_X] == ".")
                        {
                            MedicUnit me_Unit = new MedicUnit(name, pos_X, pos_Y, health, maxHealth, 2, attack, 1, team, symbol, isAttacking);
                            this.UnitsToProduce--;
                            newUnit = me_Unit;
                        }
                    }
                }
                // Add a Recon unit to the map array
                if (unitType == 5)
                {
                    if (typeteam == 1)
                    {
                        team = "Hero";
                        symbol = "S";

                        if (arrMap[pos_Y, pos_X] == ".")
                        {
                            ReconUnit re_Unit = new ReconUnit(name, pos_X, pos_Y, health, maxHealth, speed, 4, 3, team, symbol, isAttacking);
                            this.UnitsToProduce--;
                            newUnit = re_Unit;
                        }
                    }
                    else
                    {
                        team = "Villain";
                        symbol = "s";

                        if (arrMap[pos_Y, pos_X] == ".")
                        {
                            ReconUnit re_Unit = new ReconUnit(name, pos_X, pos_Y, health, maxHealth, speed, 4, 3, team, symbol, isAttacking);
                            this.UnitsToProduce--;
                            newUnit = re_Unit;
                        }
                    }
                }
                // Add a Storm Trooper unit to the map array
                if (unitType == 6)
                {
                    if (typeteam == 1)
                    {
                        team = "Hero";
                        symbol = "T";

                        if (arrMap[pos_Y, pos_X] == ".")
                        {
                            StormTrooperUnit st_Unit = new StormTrooperUnit(name, pos_X, pos_Y, health, maxHealth, speed, 3, 2, team, symbol, isAttacking);
                            this.UnitsToProduce--;
                            newUnit = st_Unit;
                        }
                    }
                    else
                    {
                        team = "Villain";
                        symbol = "t";

                        if (arrMap[pos_Y, pos_X] == ".")
                        {
                            StormTrooperUnit st_Unit = new StormTrooperUnit(name, pos_X, pos_Y, health, maxHealth, speed, 3, 2, team, symbol, isAttacking);
                            this.UnitsToProduce--;
                            newUnit = st_Unit;
                        }
                    }
                }
                // Add a Support unit to the map array
                if (unitType == 7)
                {
                    if (typeteam == 1)
                    {
                        team = "Hero";
                        symbol = "U";

                        if (arrMap[pos_Y, pos_X] == ".")
                        {
                            SupportUnit su_Unit = new SupportUnit(name, pos_X, pos_Y, health, maxHealth, 1, 3, 2, team, symbol, isAttacking);
                            this.UnitsToProduce--;
                            newUnit = su_Unit;
                        }
                    }
                    else
                    {
                        team = "Villain";
                        symbol = "u";

                        if (arrMap[pos_Y, pos_X] == ".")
                        {
                            SupportUnit su_Unit = new SupportUnit(name, pos_X, pos_Y, health, maxHealth, 1, 3, 2, team, symbol, isAttacking);
                            this.UnitsToProduce--;
                            newUnit = su_Unit;
                        }
                    }
                }
            }
            return newUnit;
        }

        // Override toString method
        public override string toString()
        {
            string s = "";
            s += unitsToProduce + "," + spawn_X + "," + spawn_Y + "," + Health + "," + Team + "," + Symbol + "\n";
            return s;
        }
    }
}
