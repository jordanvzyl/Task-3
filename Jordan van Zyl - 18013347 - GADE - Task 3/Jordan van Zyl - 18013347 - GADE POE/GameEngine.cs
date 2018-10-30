using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jordan_van_Zyl___18013347___GADE_POE
{
    class GameEngine
    {
        Map map;
        Random rnd = new Random();

        public GameEngine(int numUnits, int numBuildings)
        {
            map = new Map(numUnits, numBuildings);
        }

        public string Redraw()
        {
            string s = "";
            s += map.fillMap();
            return s;
        }

        public void playGame(int time)
        {
            map.Generate(time);
            map.spawn(time);
            for (int i = 0; i < map.unitArray.Length; i++)
            {
                if (map.unitArray[i] != null)
                {
                    string[] unitType = map.unitArray[i].GetType().ToString().Split('.');
                    string type = unitType[unitType.Length - 1];
                    if (type == "RangedUnit" && map.unitArray[i] != null)
                    {
                        RangedUnit r = (RangedUnit)map.unitArray[i];
                        if (r.isDead() == false)
                        {
                            map.ArrMap[r.Pos_Y, r.Pos_X] = r.Symbol;
                            for (int j = 0; j < map.buildingArray.Length; j++)
                            {
                                string[] building = map.buildingArray[j].ToString().Split('.');
                                string buidlingType = building[building.Length - 1];
                                if (buidlingType == "FactoryBuilding")
                                {
                                    FactoryBuilding f_Building = (FactoryBuilding)map.buildingArray[j];
                                    map.ArrMap[f_Building.Pos_Y, f_Building.Pos_X] = f_Building.Symbol;
                                }
                                if (buidlingType == "ResourceBuilding")
                                {
                                    ResourceBuilding r_Building = (ResourceBuilding)map.buildingArray[j];
                                    map.ArrMap[r_Building.Pos_Y, r_Building.Pos_X] = r_Building.Symbol;
                                }
                            }
                            // Test if this works
                            Unit closestEnemy = r.closestUnit(map.unitArray);

                            if (closestEnemy != null)
                            {
                                Console.WriteLine("Current unit: " + "\n" + map.unitArray[i].toString());
                                Console.WriteLine("Closest unit: " + "\n" + closestEnemy.toString());
                                string[] enemy = closestEnemy.GetType().ToString().Split('.');
                                string enemyType = enemy[enemy.Length - 1];
                                int enemyHealth = 0;
                                if (enemyType == "RangedUnit")
                                {
                                    RangedUnit r_Enemy = (RangedUnit)closestEnemy;
                                    enemyHealth = r_Enemy.Health;
                                }
                                if (enemyType == "MeleeUnit")
                                {
                                    MeleeUnit m_Enemy = (MeleeUnit)closestEnemy;
                                    enemyHealth = m_Enemy.Health;
                                }
                                if (enemyType == "ArtilleryUnit")
                                {
                                    ArtilleryUnit ar_Enemy = (ArtilleryUnit)closestEnemy;
                                    enemyHealth = ar_Enemy.Health;
                                }
                                if (enemyType == "MedicUnit")
                                {
                                    MedicUnit me_Enemy = (MedicUnit)closestEnemy;
                                    enemyHealth = me_Enemy.Health;
                                }
                                if (enemyType == "ReconUnit")
                                {
                                    ReconUnit re_Enemy = (ReconUnit)closestEnemy;
                                    enemyHealth = re_Enemy.Health;
                                }
                                if (enemyType == "StormTrooperUnit")
                                {
                                    StormTrooperUnit st_Enemy = (StormTrooperUnit)closestEnemy;
                                    enemyHealth = st_Enemy.Health;
                                }
                                if (enemyType == "SupportUnit")
                                {
                                    SupportUnit su_Enemy = (SupportUnit)closestEnemy;
                                    enemyHealth = su_Enemy.Health;
                                }

                                if ((r.Health / r.MaxHealth * 100) <= 25)
                                {
                                    runAway(r);
                                    r.IsAttacking = false;
                                }
                                else
                                {
                                    if (enemyHealth > 0)
                                    {
                                        if (r.IsAttacking == true)
                                        {
                                            r.combat(closestEnemy);
                                        }

                                        if (r.IsAttacking == false)
                                        {
                                            if (r.withinAtkRange(closestEnemy) == true)
                                            {
                                                r.combat(closestEnemy);
                                            }
                                            else
                                            {
                                                if (time % r.Speed == 0 && enemyHealth > 0)
                                                {
                                                    moveUnits(r, closestEnemy);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        int enemyPos = 0;

                                        for (int j = 0; j < map.unitArray.Length; j++)
                                        {
                                            if (map.unitArray[j] == closestEnemy)
                                            {
                                                enemyPos = j;
                                            }
                                        }

                                        if (enemyType == "RangedUnit")
                                        {
                                            RangedUnit r_Enemy = (RangedUnit)closestEnemy;
                                            map.ArrMap[r_Enemy.Pos_Y, r_Enemy.Pos_X] = ".";
                                            //map.unitArray[enemyPos] = null;
                                        }
                                        if (enemyType == "MeleeUnit")
                                        {
                                            MeleeUnit m_Enemy = (MeleeUnit)closestEnemy;
                                            map.ArrMap[m_Enemy.Pos_Y, m_Enemy.Pos_X] = ".";
                                            //map.unitArray[enemyPos] = null;
                                        }
                                        if (enemyType == "ArtilleryUnit")
                                        {
                                            ArtilleryUnit ar_Enemy = (ArtilleryUnit)closestEnemy;
                                            map.ArrMap[ar_Enemy.Pos_Y, ar_Enemy.Pos_X] = ".";
                                        }
                                        if (enemyType == "MedicUnit")
                                        {
                                            MedicUnit me_Enemy = (MedicUnit)closestEnemy;
                                            map.ArrMap[me_Enemy.Pos_Y, me_Enemy.Pos_X] = ".";
                                        }
                                        if (enemyType == "ReconUnit")
                                        {
                                            ReconUnit re_Enemy = (ReconUnit)closestEnemy;
                                            map.ArrMap[re_Enemy.Pos_Y, re_Enemy.Pos_X] = ".";
                                        }
                                        if (enemyType == "StormTrooperUnit")
                                        {
                                            StormTrooperUnit st_Enemy = (StormTrooperUnit)closestEnemy;
                                            map.ArrMap[st_Enemy.Pos_Y, st_Enemy.Pos_X] = ".";
                                        }
                                        if (enemyType == "SupportUnit")
                                        {
                                            SupportUnit su_Enemy = (SupportUnit)closestEnemy;
                                            map.ArrMap[su_Enemy.Pos_Y, su_Enemy.Pos_X] = ".";
                                        }
                                    }
                                }
                            }
                            /*
                            else
                            {
                                Console.WriteLine("Closest is null");
                            }
                            
                            
                            else
                            {
                                if(time % r.Speed == 0 )
                                {
                                    runAway(r);
                                }
                            }
                            */

                        }
                        else
                        {
                            map.ArrMap[r.Pos_Y, r.Pos_X] = ".";
                            //map.unitArray[i] = null;
                        }
                    }
                    if (type == "MeleeUnit" && map.unitArray[i] != null)
                    {
                        MeleeUnit m = (MeleeUnit)map.unitArray[i];
                        if (m.isDead() == false)
                        {
                            map.ArrMap[m.Pos_Y, m.Pos_X] = m.Symbol;
                            for (int j = 0; j < map.buildingArray.Length; j++)
                            {
                                string[] building = map.buildingArray[j].ToString().Split('.');
                                string buidlingType = building[building.Length - 1];
                                if (buidlingType == "FactoryBuilding")
                                {
                                    FactoryBuilding f_Building = (FactoryBuilding)map.buildingArray[j];
                                    map.ArrMap[f_Building.Pos_Y, f_Building.Pos_X] = f_Building.Symbol;
                                }
                                if (buidlingType == "ResourceBuilding")
                                {
                                    ResourceBuilding r_Building = (ResourceBuilding)map.buildingArray[j];
                                    map.ArrMap[r_Building.Pos_Y, r_Building.Pos_X] = r_Building.Symbol;
                                }
                            }
                            Unit closestEnemy = m.closestUnit(map.unitArray);

                            if (closestEnemy != null)
                            {
                                string[] enemy = closestEnemy.GetType().ToString().Split('.');
                                string enemyType = enemy[enemy.Length - 1];
                                int enemyHealth = 0;
                                if (enemyType == "RangedUnit")
                                {
                                    RangedUnit r_Enemy = (RangedUnit)closestEnemy;
                                    enemyHealth = r_Enemy.Health;
                                }
                                if (enemyType == "MeleeUnit")
                                {
                                    MeleeUnit m_Enemy = (MeleeUnit)closestEnemy;
                                    enemyHealth = m_Enemy.Health;
                                }
                                if (enemyType == "ArtilleryUnit")
                                {
                                    ArtilleryUnit ar_Enemy = (ArtilleryUnit)closestEnemy;
                                    enemyHealth = ar_Enemy.Health;
                                }
                                if (enemyType == "MedicUnit")
                                {
                                    MedicUnit me_Enemy = (MedicUnit)closestEnemy;
                                    enemyHealth = me_Enemy.Health;
                                }
                                if (enemyType == "ReconUnit")
                                {
                                    ReconUnit re_Enemy = (ReconUnit)closestEnemy;
                                    enemyHealth = re_Enemy.Health;
                                }
                                if (enemyType == "StormTrooperUnit")
                                {
                                    StormTrooperUnit st_Enemy = (StormTrooperUnit)closestEnemy;
                                    enemyHealth = st_Enemy.Health;
                                }
                                if (enemyType == "SupportUnit")
                                {
                                    SupportUnit su_Enemy = (SupportUnit)closestEnemy;
                                    enemyHealth = su_Enemy.Health;
                                }

                                if ((m.Health / m.MaxHealth * 100) <= 25)
                                {
                                    runAway(m);
                                    m.IsAttacking = false;
                                }
                                else
                                {
                                    if (enemyHealth > 0)
                                    {
                                        if (m.IsAttacking == true)
                                        {
                                            m.combat(closestEnemy);
                                        }

                                        if (m.IsAttacking == false)
                                        {
                                            if (m.withinAtkRange(closestEnemy) == true)
                                            {
                                                m.combat(closestEnemy);
                                            }
                                            else
                                            {
                                                if (time % m.Speed == 0 && enemyHealth > 0)
                                                {
                                                    moveUnits(m, closestEnemy);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        int enemyPos = 0;

                                        for (int j = 0; j < map.unitArray.Length; j++)
                                        {
                                            if (map.unitArray[j] == closestEnemy)
                                            {
                                                enemyPos = j;
                                            }
                                        }

                                        if (enemyType == "RangedUnit")
                                        {
                                            RangedUnit r_Enemy = (RangedUnit)closestEnemy;
                                            map.ArrMap[r_Enemy.Pos_Y, r_Enemy.Pos_X] = ".";
                                            //map.unitArray[enemyPos] = null;
                                        }
                                        if (enemyType == "MeleeUnit")
                                        {
                                            MeleeUnit m_Enemy = (MeleeUnit)closestEnemy;
                                            map.ArrMap[m_Enemy.Pos_Y, m_Enemy.Pos_X] = ".";
                                            //map.unitArray[enemyPos] = null;
                                        }
                                        if (enemyType == "ArtilleryUnit")
                                        {
                                            ArtilleryUnit ar_Enemy = (ArtilleryUnit)closestEnemy;
                                            map.ArrMap[ar_Enemy.Pos_Y, ar_Enemy.Pos_X] = ".";
                                        }
                                        if (enemyType == "MedicUnit")
                                        {
                                            MedicUnit me_Enemy = (MedicUnit)closestEnemy;
                                            map.ArrMap[me_Enemy.Pos_Y, me_Enemy.Pos_X] = ".";
                                        }
                                        if (enemyType == "ReconUnit")
                                        {
                                            ReconUnit re_Enemy = (ReconUnit)closestEnemy;
                                            map.ArrMap[re_Enemy.Pos_Y, re_Enemy.Pos_X] = ".";
                                        }
                                        if (enemyType == "StormTrooperUnit")
                                        {
                                            StormTrooperUnit st_Enemy = (StormTrooperUnit)closestEnemy;
                                            map.ArrMap[st_Enemy.Pos_Y, st_Enemy.Pos_X] = ".";
                                        }
                                        if (enemyType == "SupportUnit")
                                        {
                                            SupportUnit su_Enemy = (SupportUnit)closestEnemy;
                                            map.ArrMap[su_Enemy.Pos_Y, su_Enemy.Pos_X] = ".";
                                        }
                                    }
                                }
                            }
                            /*
                            else
                            {
                                if(time % m.Speed == 0)
                                {
                                    runAway(m);
                                }
                            }
                            */

                        }
                        else
                        {
                            map.ArrMap[m.Pos_Y, m.Pos_X] = ".";
                            //map.unitArray[i] = null;
                        }
                    }
                    if (type == "ArtilleryUnit" && map.unitArray[i] != null)
                    {
                        ArtilleryUnit ar = (ArtilleryUnit)map.unitArray[i];
                        if (ar.isDead() == false)
                        {
                            map.ArrMap[ar.Pos_Y, ar.Pos_X] = ar.Symbol;
                            for (int j = 0; j < map.buildingArray.Length; j++)
                            {
                                string[] building = map.buildingArray[j].ToString().Split('.');
                                string buidlingType = building[building.Length - 1];
                                if (buidlingType == "FactoryBuilding")
                                {
                                    FactoryBuilding f_Building = (FactoryBuilding)map.buildingArray[j];
                                    map.ArrMap[f_Building.Pos_Y, f_Building.Pos_X] = f_Building.Symbol;
                                }
                                if (buidlingType == "ResourceBuilding")
                                {
                                    ResourceBuilding r_Building = (ResourceBuilding)map.buildingArray[j];
                                    map.ArrMap[r_Building.Pos_Y, r_Building.Pos_X] = r_Building.Symbol;
                                }
                            }
                            Unit closestEnemy = ar.closestUnit(map.unitArray);

                            if (closestEnemy != null)
                            {
                                string[] enemy = closestEnemy.GetType().ToString().Split('.');
                                string enemyType = enemy[enemy.Length - 1];
                                int enemyHealth = 0;
                                if (enemyType == "RangedUnit")
                                {
                                    RangedUnit r_Enemy = (RangedUnit)closestEnemy;
                                    enemyHealth = r_Enemy.Health;
                                }
                                if (enemyType == "MeleeUnit")
                                {
                                    MeleeUnit m_Enemy = (MeleeUnit)closestEnemy;
                                    enemyHealth = m_Enemy.Health;
                                }
                                if (enemyType == "ArtilleryUnit")
                                {
                                    ArtilleryUnit ar_Enemy = (ArtilleryUnit)closestEnemy;
                                    enemyHealth = ar_Enemy.Health;
                                }
                                if (enemyType == "MedicUnit")
                                {
                                    MedicUnit me_Enemy = (MedicUnit)closestEnemy;
                                    enemyHealth = me_Enemy.Health;
                                }
                                if (enemyType == "ReconUnit")
                                {
                                    ReconUnit re_Enemy = (ReconUnit)closestEnemy;
                                    enemyHealth = re_Enemy.Health;
                                }
                                if (enemyType == "StormTrooperUnit")
                                {
                                    StormTrooperUnit st_Enemy = (StormTrooperUnit)closestEnemy;
                                    enemyHealth = st_Enemy.Health;
                                }
                                if (enemyType == "SupportUnit")
                                {
                                    SupportUnit su_Enemy = (SupportUnit)closestEnemy;
                                    enemyHealth = su_Enemy.Health;
                                }

                                if ((ar.Health / ar.MaxHealth * 100) <= 25)
                                {
                                    runAway(ar);
                                    ar.IsAttacking = false;
                                }
                                else
                                {
                                    if (enemyHealth > 0)
                                    {
                                        if (ar.IsAttacking == true)
                                        {
                                            ar.combat(closestEnemy);
                                        }

                                        if (ar.IsAttacking == false)
                                        {
                                            if (ar.withinAtkRange(closestEnemy) == true)
                                            {
                                                ar.combat(closestEnemy);
                                            }
                                            else
                                            {
                                                if (time % ar.Speed == 0 && enemyHealth > 0)
                                                {
                                                    moveUnits(ar, closestEnemy);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        int enemyPos = 0;

                                        for (int j = 0; j < map.unitArray.Length; j++)
                                        {
                                            if (map.unitArray[j] == closestEnemy)
                                            {
                                                enemyPos = j;
                                            }
                                        }

                                        if (enemyType == "RangedUnit")
                                        {
                                            RangedUnit r_Enemy = (RangedUnit)closestEnemy;
                                            map.ArrMap[r_Enemy.Pos_Y, r_Enemy.Pos_X] = ".";
                                            //map.unitArray[enemyPos] = null;
                                        }
                                        if (enemyType == "MeleeUnit")
                                        {
                                            MeleeUnit m_Enemy = (MeleeUnit)closestEnemy;
                                            map.ArrMap[m_Enemy.Pos_Y, m_Enemy.Pos_X] = ".";
                                            //map.unitArray[enemyPos] = null;
                                        }
                                        if (enemyType == "ArtilleryUnit")
                                        {
                                            ArtilleryUnit ar_Enemy = (ArtilleryUnit)closestEnemy;
                                            map.ArrMap[ar_Enemy.Pos_Y, ar_Enemy.Pos_X] = ".";
                                        }
                                        if (enemyType == "MedicUnit")
                                        {
                                            MedicUnit me_Enemy = (MedicUnit)closestEnemy;
                                            map.ArrMap[me_Enemy.Pos_Y, me_Enemy.Pos_X] = ".";
                                        }
                                        if (enemyType == "ReconUnit")
                                        {
                                            ReconUnit re_Enemy = (ReconUnit)closestEnemy;
                                            map.ArrMap[re_Enemy.Pos_Y, re_Enemy.Pos_X] = ".";
                                        }
                                        if (enemyType == "StormTrooperUnit")
                                        {
                                            StormTrooperUnit st_Enemy = (StormTrooperUnit)closestEnemy;
                                            map.ArrMap[st_Enemy.Pos_Y, st_Enemy.Pos_X] = ".";
                                        }
                                        if (enemyType == "SupportUnit")
                                        {
                                            SupportUnit su_Enemy = (SupportUnit)closestEnemy;
                                            map.ArrMap[su_Enemy.Pos_Y, su_Enemy.Pos_X] = ".";
                                        }

                                        if ((ar.Health / ar.MaxHealth * 100) <= 25)
                                        {
                                            runAway(ar);
                                            ar.IsAttacking = false;
                                        }
                                    }
                                }
                            }
                            /*
                            else
                            {
                                if(time % m.Speed == 0)
                                {
                                    runAway(m);
                                }
                            }
                            */

                        }
                        else
                        {
                            map.ArrMap[ar.Pos_Y, ar.Pos_X] = ".";
                            //map.unitArray[i] = null;
                        }
                    }
                    if (type == "MedicUnit" && map.unitArray[i] != null)
                    {
                        MedicUnit me = (MedicUnit)map.unitArray[i];
                        if (me.isDead() == false)
                        {
                            map.ArrMap[me.Pos_Y, me.Pos_X] = me.Symbol;
                            for (int j = 0; j < map.buildingArray.Length; j++)
                            {
                                string[] building = map.buildingArray[j].ToString().Split('.');
                                string buidlingType = building[building.Length - 1];
                                if (buidlingType == "FactoryBuilding")
                                {
                                    FactoryBuilding f_Building = (FactoryBuilding)map.buildingArray[j];
                                    map.ArrMap[f_Building.Pos_Y, f_Building.Pos_X] = f_Building.Symbol;
                                }
                                if (buidlingType == "ResourceBuilding")
                                {
                                    ResourceBuilding r_Building = (ResourceBuilding)map.buildingArray[j];
                                    map.ArrMap[r_Building.Pos_Y, r_Building.Pos_X] = r_Building.Symbol;
                                }
                            }
                            Unit closestEnemy = me.closestUnit(map.unitArray);

                            if (closestEnemy != null)
                            {
                                string[] enemy = closestEnemy.GetType().ToString().Split('.');
                                string enemyType = enemy[enemy.Length - 1];
                                int enemyHealth = 0;
                                if (enemyType == "RangedUnit")
                                {
                                    RangedUnit r_Enemy = (RangedUnit)closestEnemy;
                                    enemyHealth = r_Enemy.Health;
                                }
                                if (enemyType == "MeleeUnit")
                                {
                                    MeleeUnit m_Enemy = (MeleeUnit)closestEnemy;
                                    enemyHealth = m_Enemy.Health;
                                }
                                if (enemyType == "ArtilleryUnit")
                                {
                                    ArtilleryUnit ar_Enemy = (ArtilleryUnit)closestEnemy;
                                    enemyHealth = ar_Enemy.Health;
                                }
                                if (enemyType == "MedicUnit")
                                {
                                    MedicUnit me_Enemy = (MedicUnit)closestEnemy;
                                    enemyHealth = me_Enemy.Health;
                                }
                                if (enemyType == "ReconUnit")
                                {
                                    ReconUnit re_Enemy = (ReconUnit)closestEnemy;
                                    enemyHealth = re_Enemy.Health;
                                }
                                if (enemyType == "StormTrooperUnit")
                                {
                                    StormTrooperUnit st_Enemy = (StormTrooperUnit)closestEnemy;
                                    enemyHealth = st_Enemy.Health;
                                }
                                if (enemyType == "SupportUnit")
                                {
                                    SupportUnit su_Enemy = (SupportUnit)closestEnemy;
                                    enemyHealth = su_Enemy.Health;
                                }

                                if ((me.Health / me.MaxHealth * 100) <= 25)
                                {
                                    runAway(me);
                                    me.IsAttacking = false;
                                }
                                else
                                {
                                    if (enemyHealth > 0)
                                    {
                                        if (me.IsAttacking == true)
                                        {
                                            me.combat(closestEnemy);
                                        }

                                        if (me.IsAttacking == false)
                                        {
                                            if (me.withinAtkRange(closestEnemy) == true)
                                            {
                                                me.combat(closestEnemy);
                                            }
                                            else
                                            {
                                                if (time % me.Speed == 0 && enemyHealth > 0)
                                                {
                                                    moveUnits(me, closestEnemy);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        int enemyPos = 0;

                                        for (int j = 0; j < map.unitArray.Length; j++)
                                        {
                                            if (map.unitArray[j] == closestEnemy)
                                            {
                                                enemyPos = j;
                                            }
                                        }

                                        if (enemyType == "RangedUnit")
                                        {
                                            RangedUnit r_Enemy = (RangedUnit)closestEnemy;
                                            map.ArrMap[r_Enemy.Pos_Y, r_Enemy.Pos_X] = ".";
                                            //map.unitArray[enemyPos] = null;
                                        }
                                        if (enemyType == "MeleeUnit")
                                        {
                                            MeleeUnit m_Enemy = (MeleeUnit)closestEnemy;
                                            map.ArrMap[m_Enemy.Pos_Y, m_Enemy.Pos_X] = ".";
                                            //map.unitArray[enemyPos] = null;
                                        }
                                        if (enemyType == "ArtilleryUnit")
                                        {
                                            ArtilleryUnit ar_Enemy = (ArtilleryUnit)closestEnemy;
                                            map.ArrMap[ar_Enemy.Pos_Y, ar_Enemy.Pos_X] = ".";
                                        }
                                        if (enemyType == "MedicUnit")
                                        {
                                            MedicUnit me_Enemy = (MedicUnit)closestEnemy;
                                            map.ArrMap[me_Enemy.Pos_Y, me_Enemy.Pos_X] = ".";
                                        }
                                        if (enemyType == "ReconUnit")
                                        {
                                            ReconUnit re_Enemy = (ReconUnit)closestEnemy;
                                            map.ArrMap[re_Enemy.Pos_Y, re_Enemy.Pos_X] = ".";
                                        }
                                        if (enemyType == "StormTrooperUnit")
                                        {
                                            StormTrooperUnit st_Enemy = (StormTrooperUnit)closestEnemy;
                                            map.ArrMap[st_Enemy.Pos_Y, st_Enemy.Pos_X] = ".";
                                        }
                                        if (enemyType == "SupportUnit")
                                        {
                                            SupportUnit su_Enemy = (SupportUnit)closestEnemy;
                                            map.ArrMap[su_Enemy.Pos_Y, su_Enemy.Pos_X] = ".";
                                        }

                                        if ((me.Health / me.MaxHealth * 100) <= 25)
                                        {
                                            runAway(me);
                                            me.IsAttacking = false;
                                        }
                                    }
                                }
                            }
                            /*
                            else
                            {
                                if(time % m.Speed == 0)
                                {
                                    runAway(m);
                                }
                            }
                            */

                        }
                        else
                        {
                            map.ArrMap[me.Pos_Y, me.Pos_X] = ".";
                            //map.unitArray[i] = null;
                        }
                    }
                    if (type == "ReconUnit" && map.unitArray[i] != null)
                    {
                        ReconUnit re = (ReconUnit)map.unitArray[i];
                        if (re.isDead() == false)
                        {
                            map.ArrMap[re.Pos_Y, re.Pos_X] = re.Symbol;
                            for (int j = 0; j < map.buildingArray.Length; j++)
                            {
                                string[] building = map.buildingArray[j].ToString().Split('.');
                                string buidlingType = building[building.Length - 1];
                                if (buidlingType == "FactoryBuilding")
                                {
                                    FactoryBuilding f_Building = (FactoryBuilding)map.buildingArray[j];
                                    map.ArrMap[f_Building.Pos_Y, f_Building.Pos_X] = f_Building.Symbol;
                                }
                                if (buidlingType == "ResourceBuilding")
                                {
                                    ResourceBuilding r_Building = (ResourceBuilding)map.buildingArray[j];
                                    map.ArrMap[r_Building.Pos_Y, r_Building.Pos_X] = r_Building.Symbol;
                                }
                            }
                            Unit closestEnemy = re.closestUnit(map.unitArray);

                            if (closestEnemy != null)
                            {
                                string[] enemy = closestEnemy.GetType().ToString().Split('.');
                                string enemyType = enemy[enemy.Length - 1];
                                int enemyHealth = 0;
                                if (enemyType == "RangedUnit")
                                {
                                    RangedUnit r_Enemy = (RangedUnit)closestEnemy;
                                    enemyHealth = r_Enemy.Health;
                                }
                                if (enemyType == "MeleeUnit")
                                {
                                    MeleeUnit m_Enemy = (MeleeUnit)closestEnemy;
                                    enemyHealth = m_Enemy.Health;
                                }
                                if (enemyType == "ArtilleryUnit")
                                {
                                    ArtilleryUnit ar_Enemy = (ArtilleryUnit)closestEnemy;
                                    enemyHealth = ar_Enemy.Health;
                                }
                                if (enemyType == "MedicUnit")
                                {
                                    MedicUnit me_Enemy = (MedicUnit)closestEnemy;
                                    enemyHealth = me_Enemy.Health;
                                }
                                if (enemyType == "ReconUnit")
                                {
                                    ReconUnit re_Enemy = (ReconUnit)closestEnemy;
                                    enemyHealth = re_Enemy.Health;
                                }
                                if (enemyType == "StormTrooperUnit")
                                {
                                    StormTrooperUnit st_Enemy = (StormTrooperUnit)closestEnemy;
                                    enemyHealth = st_Enemy.Health;
                                }
                                if (enemyType == "SupportUnit")
                                {
                                    SupportUnit su_Enemy = (SupportUnit)closestEnemy;
                                    enemyHealth = su_Enemy.Health;
                                }

                                if ((re.Health / re.MaxHealth * 100) <= 25)
                                {
                                    runAway(re);
                                    re.IsAttacking = false;
                                }
                                else
                                {
                                    if (enemyHealth > 0)
                                    {
                                        if (re.IsAttacking == true)
                                        {
                                            re.combat(closestEnemy);
                                        }

                                        if (re.IsAttacking == false)
                                        {
                                            if (re.withinAtkRange(closestEnemy) == true)
                                            {
                                                re.combat(closestEnemy);
                                            }
                                            else
                                            {
                                                if (time % re.Speed == 0 && enemyHealth > 0)
                                                {
                                                    moveUnits(re, closestEnemy);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        int enemyPos = 0;

                                        for (int j = 0; j < map.unitArray.Length; j++)
                                        {
                                            if (map.unitArray[j] == closestEnemy)
                                            {
                                                enemyPos = j;
                                            }
                                        }

                                        if (enemyType == "RangedUnit")
                                        {
                                            RangedUnit r_Enemy = (RangedUnit)closestEnemy;
                                            map.ArrMap[r_Enemy.Pos_Y, r_Enemy.Pos_X] = ".";
                                            //map.unitArray[enemyPos] = null;
                                        }
                                        if (enemyType == "MeleeUnit")
                                        {
                                            MeleeUnit m_Enemy = (MeleeUnit)closestEnemy;
                                            map.ArrMap[m_Enemy.Pos_Y, m_Enemy.Pos_X] = ".";
                                            //map.unitArray[enemyPos] = null;
                                        }
                                        if (enemyType == "ArtilleryUnit")
                                        {
                                            ArtilleryUnit ar_Enemy = (ArtilleryUnit)closestEnemy;
                                            map.ArrMap[ar_Enemy.Pos_Y, ar_Enemy.Pos_X] = ".";
                                        }
                                        if (enemyType == "MedicUnit")
                                        {
                                            MedicUnit me_Enemy = (MedicUnit)closestEnemy;
                                            map.ArrMap[me_Enemy.Pos_Y, me_Enemy.Pos_X] = ".";
                                        }
                                        if (enemyType == "ReconUnit")
                                        {
                                            ReconUnit re_Enemy = (ReconUnit)closestEnemy;
                                            map.ArrMap[re_Enemy.Pos_Y, re_Enemy.Pos_X] = ".";
                                        }
                                        if (enemyType == "StormTrooperUnit")
                                        {
                                            StormTrooperUnit st_Enemy = (StormTrooperUnit)closestEnemy;
                                            map.ArrMap[st_Enemy.Pos_Y, st_Enemy.Pos_X] = ".";
                                        }
                                        if (enemyType == "SupportUnit")
                                        {
                                            SupportUnit su_Enemy = (SupportUnit)closestEnemy;
                                            map.ArrMap[su_Enemy.Pos_Y, su_Enemy.Pos_X] = ".";
                                        }

                                        if ((re.Health / re.MaxHealth * 100) <= 25)
                                        {
                                            runAway(re);
                                            re.IsAttacking = false;
                                        }
                                    }
                                }
                            }
                            /*
                            else
                            {
                                if(time % m.Speed == 0)
                                {
                                    runAway(m);
                                }
                            }
                            */

                        }
                        else
                        {
                            map.ArrMap[re.Pos_Y, re.Pos_X] = ".";
                            //map.unitArray[i] = null;
                        }
                    }
                    if (type == "StormTrooperUnit" && map.unitArray[i] != null)
                    {
                        StormTrooperUnit st = (StormTrooperUnit)map.unitArray[i];
                        if (st.isDead() == false)
                        {
                            map.ArrMap[st.Pos_Y, st.Pos_X] = st.Symbol;
                            for (int j = 0; j < map.buildingArray.Length; j++)
                            {
                                string[] building = map.buildingArray[j].ToString().Split('.');
                                string buidlingType = building[building.Length - 1];
                                if (buidlingType == "FactoryBuilding")
                                {
                                    FactoryBuilding f_Building = (FactoryBuilding)map.buildingArray[j];
                                    map.ArrMap[f_Building.Pos_Y, f_Building.Pos_X] = f_Building.Symbol;
                                }
                                if (buidlingType == "ResourceBuilding")
                                {
                                    ResourceBuilding r_Building = (ResourceBuilding)map.buildingArray[j];
                                    map.ArrMap[r_Building.Pos_Y, r_Building.Pos_X] = r_Building.Symbol;
                                }
                            }
                            Unit closestEnemy = st.closestUnit(map.unitArray);

                            if (closestEnemy != null)
                            {
                                string[] enemy = closestEnemy.GetType().ToString().Split('.');
                                string enemyType = enemy[enemy.Length - 1];
                                int enemyHealth = 0;
                                if (enemyType == "RangedUnit")
                                {
                                    RangedUnit r_Enemy = (RangedUnit)closestEnemy;
                                    enemyHealth = r_Enemy.Health;
                                }
                                if (enemyType == "MeleeUnit")
                                {
                                    MeleeUnit m_Enemy = (MeleeUnit)closestEnemy;
                                    enemyHealth = m_Enemy.Health;
                                }
                                if (enemyType == "ArtilleryUnit")
                                {
                                    ArtilleryUnit ar_Enemy = (ArtilleryUnit)closestEnemy;
                                    enemyHealth = ar_Enemy.Health;
                                }
                                if (enemyType == "MedicUnit")
                                {
                                    MedicUnit me_Enemy = (MedicUnit)closestEnemy;
                                    enemyHealth = me_Enemy.Health;
                                }
                                if (enemyType == "ReconUnit")
                                {
                                    ReconUnit re_Enemy = (ReconUnit)closestEnemy;
                                    enemyHealth = re_Enemy.Health;
                                }
                                if (enemyType == "StormTrooperUnit")
                                {
                                    StormTrooperUnit st_Enemy = (StormTrooperUnit)closestEnemy;
                                    enemyHealth = st_Enemy.Health;
                                }
                                if (enemyType == "SupportUnit")
                                {
                                    SupportUnit su_Enemy = (SupportUnit)closestEnemy;
                                    enemyHealth = su_Enemy.Health;
                                }

                                if ((st.Health / st.MaxHealth * 100) <= 25)
                                {
                                    runAway(st);
                                    st.IsAttacking = false;
                                }
                                else
                                {
                                    if (enemyHealth > 0)
                                    {
                                        if (st.IsAttacking == true)
                                        {
                                            st.combat(closestEnemy);
                                        }

                                        if (st.IsAttacking == false)
                                        {
                                            if (st.withinAtkRange(closestEnemy) == true)
                                            {
                                                st.combat(closestEnemy);
                                            }
                                            else
                                            {
                                                if (time % st.Speed == 0 && enemyHealth > 0)
                                                {
                                                    moveUnits(st, closestEnemy);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        int enemyPos = 0;

                                        for (int j = 0; j < map.unitArray.Length; j++)
                                        {
                                            if (map.unitArray[j] == closestEnemy)
                                            {
                                                enemyPos = j;
                                            }
                                        }

                                        if (enemyType == "RangedUnit")
                                        {
                                            RangedUnit r_Enemy = (RangedUnit)closestEnemy;
                                            map.ArrMap[r_Enemy.Pos_Y, r_Enemy.Pos_X] = ".";
                                            //map.unitArray[enemyPos] = null;
                                        }
                                        if (enemyType == "MeleeUnit")
                                        {
                                            MeleeUnit m_Enemy = (MeleeUnit)closestEnemy;
                                            map.ArrMap[m_Enemy.Pos_Y, m_Enemy.Pos_X] = ".";
                                            //map.unitArray[enemyPos] = null;
                                        }
                                        if (enemyType == "ArtilleryUnit")
                                        {
                                            ArtilleryUnit ar_Enemy = (ArtilleryUnit)closestEnemy;
                                            map.ArrMap[ar_Enemy.Pos_Y, ar_Enemy.Pos_X] = ".";
                                        }
                                        if (enemyType == "MedicUnit")
                                        {
                                            MedicUnit me_Enemy = (MedicUnit)closestEnemy;
                                            map.ArrMap[me_Enemy.Pos_Y, me_Enemy.Pos_X] = ".";
                                        }
                                        if (enemyType == "ReconUnit")
                                        {
                                            ReconUnit re_Enemy = (ReconUnit)closestEnemy;
                                            map.ArrMap[re_Enemy.Pos_Y, re_Enemy.Pos_X] = ".";
                                        }
                                        if (enemyType == "StormTrooperUnit")
                                        {
                                            StormTrooperUnit st_Enemy = (StormTrooperUnit)closestEnemy;
                                            map.ArrMap[st_Enemy.Pos_Y, st_Enemy.Pos_X] = ".";
                                        }
                                        if (enemyType == "SupportUnit")
                                        {
                                            SupportUnit su_Enemy = (SupportUnit)closestEnemy;
                                            map.ArrMap[su_Enemy.Pos_Y, su_Enemy.Pos_X] = ".";
                                        }

                                        if ((st.Health / st.MaxHealth * 100) <= 25)
                                        {
                                            runAway(st);
                                            st.IsAttacking = false;
                                        }
                                    }
                                }
                            }
                            /*
                            else
                            {
                                if(time % m.Speed == 0)
                                {
                                    runAway(m);
                                }
                            }
                            */

                        }
                        else
                        {
                            map.ArrMap[st.Pos_Y, st.Pos_X] = ".";
                            //map.unitArray[i] = null;
                        }
                    }
                    if (type == "SupportUnit" && map.unitArray[i] != null)
                    {
                        SupportUnit su = (SupportUnit)map.unitArray[i];
                        if (su.isDead() == false)
                        {
                            map.ArrMap[su.Pos_Y, su.Pos_X] = su.Symbol;
                            for (int j = 0; j < map.buildingArray.Length; j++)
                            {
                                string[] building = map.buildingArray[j].ToString().Split('.');
                                string buidlingType = building[building.Length - 1];
                                if (buidlingType == "FactoryBuilding")
                                {
                                    FactoryBuilding f_Building = (FactoryBuilding)map.buildingArray[j];
                                    map.ArrMap[f_Building.Pos_Y, f_Building.Pos_X] = f_Building.Symbol;
                                }
                                if (buidlingType == "ResourceBuilding")
                                {
                                    ResourceBuilding r_Building = (ResourceBuilding)map.buildingArray[j];
                                    map.ArrMap[r_Building.Pos_Y, r_Building.Pos_X] = r_Building.Symbol;
                                }
                            }
                            Unit closestEnemy = su.closestUnit(map.unitArray);

                            if (closestEnemy != null)
                            {
                                string[] enemy = closestEnemy.GetType().ToString().Split('.');
                                string enemyType = enemy[enemy.Length - 1];
                                int enemyHealth = 0;
                                if (enemyType == "RangedUnit")
                                {
                                    RangedUnit r_Enemy = (RangedUnit)closestEnemy;
                                    enemyHealth = r_Enemy.Health;
                                }
                                if (enemyType == "MeleeUnit")
                                {
                                    MeleeUnit m_Enemy = (MeleeUnit)closestEnemy;
                                    enemyHealth = m_Enemy.Health;
                                }
                                if (enemyType == "ArtilleryUnit")
                                {
                                    ArtilleryUnit ar_Enemy = (ArtilleryUnit)closestEnemy;
                                    enemyHealth = ar_Enemy.Health;
                                }
                                if (enemyType == "MedicUnit")
                                {
                                    MedicUnit me_Enemy = (MedicUnit)closestEnemy;
                                    enemyHealth = me_Enemy.Health;
                                }
                                if (enemyType == "ReconUnit")
                                {
                                    ReconUnit re_Enemy = (ReconUnit)closestEnemy;
                                    enemyHealth = re_Enemy.Health;
                                }
                                if (enemyType == "StormTrooperUnit")
                                {
                                    StormTrooperUnit st_Enemy = (StormTrooperUnit)closestEnemy;
                                    enemyHealth = st_Enemy.Health;
                                }
                                if (enemyType == "SupportUnit")
                                {
                                    SupportUnit su_Enemy = (SupportUnit)closestEnemy;
                                    enemyHealth = su_Enemy.Health;
                                }

                                if ((su.Health / su.MaxHealth * 100) <= 25)
                                {
                                    runAway(su);
                                    su.IsAttacking = false;
                                }
                                else
                                {
                                    if (enemyHealth > 0)
                                    {
                                        if (su.IsAttacking == true)
                                        {
                                            su.combat(closestEnemy);
                                        }

                                        if (su.IsAttacking == false)
                                        {
                                            if (su.withinAtkRange(closestEnemy) == true)
                                            {
                                                su.combat(closestEnemy);
                                            }
                                            else
                                            {
                                                if (time % su.Speed == 0 && enemyHealth > 0)
                                                {
                                                    moveUnits(su, closestEnemy);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        int enemyPos = 0;

                                        for (int j = 0; j < map.unitArray.Length; j++)
                                        {
                                            if (map.unitArray[j] == closestEnemy)
                                            {
                                                enemyPos = j;
                                            }
                                        }

                                        if (enemyType == "RangedUnit")
                                        {
                                            RangedUnit r_Enemy = (RangedUnit)closestEnemy;
                                            map.ArrMap[r_Enemy.Pos_Y, r_Enemy.Pos_X] = ".";
                                            //map.unitArray[enemyPos] = null;
                                        }
                                        if (enemyType == "MeleeUnit")
                                        {
                                            MeleeUnit m_Enemy = (MeleeUnit)closestEnemy;
                                            map.ArrMap[m_Enemy.Pos_Y, m_Enemy.Pos_X] = ".";
                                            //map.unitArray[enemyPos] = null;
                                        }
                                        if (enemyType == "ArtilleryUnit")
                                        {
                                            ArtilleryUnit ar_Enemy = (ArtilleryUnit)closestEnemy;
                                            map.ArrMap[ar_Enemy.Pos_Y, ar_Enemy.Pos_X] = ".";
                                        }
                                        if (enemyType == "MedicUnit")
                                        {
                                            MedicUnit me_Enemy = (MedicUnit)closestEnemy;
                                            map.ArrMap[me_Enemy.Pos_Y, me_Enemy.Pos_X] = ".";
                                        }
                                        if (enemyType == "ReconUnit")
                                        {
                                            ReconUnit re_Enemy = (ReconUnit)closestEnemy;
                                            map.ArrMap[re_Enemy.Pos_Y, re_Enemy.Pos_X] = ".";
                                        }
                                        if (enemyType == "StormTrooperUnit")
                                        {
                                            StormTrooperUnit st_Enemy = (StormTrooperUnit)closestEnemy;
                                            map.ArrMap[st_Enemy.Pos_Y, st_Enemy.Pos_X] = ".";
                                        }
                                        if (enemyType == "SupportUnit")
                                        {
                                            SupportUnit su_Enemy = (SupportUnit)closestEnemy;
                                            map.ArrMap[su_Enemy.Pos_Y, su_Enemy.Pos_X] = ".";
                                        }

                                        if ((su.Health / su.MaxHealth * 100) <= 25)
                                        {
                                            runAway(su);
                                            su.IsAttacking = false;
                                        }
                                    }
                                }
                            }
                            /*
                            else
                            {
                                if(time % m.Speed == 0)
                                {
                                    runAway(m);
                                }
                            }
                            */

                        }
                        else
                        {
                            map.ArrMap[su.Pos_Y, su.Pos_X] = ".";
                            //map.unitArray[i] = null;
                        }
                    }
                }
            }
            Redraw();
        }

        // Method to move
        public void moveUnits(Unit unit, Unit enemy)
        {
            string[] unitType = unit.GetType().ToString().Split('.');
            string thisType = unitType[unitType.Length - 1];

            string[] enemyType = enemy.GetType().ToString().Split('.');
            string thisEnemy = enemyType[enemyType.Length - 1];

            if (thisType == "MeleeUnit")
            {
                MeleeUnit m = (MeleeUnit)unit;
                if (thisEnemy == "MeleeUnit")
                {
                    MeleeUnit enemyUnit = (MeleeUnit)enemy;

                    int distance_X = m.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = m.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && m.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = m.Pos_X;
                            int new_X = m.Pos_X + 1;
                            m.newPosition(new_X, m.Pos_Y);
                            map.ArrMap[m.Pos_Y, new_X] = m.Symbol;
                            map.ArrMap[m.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && m.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = m.Pos_X;
                            int new_X = m.Pos_X - 1;
                            m.newPosition(new_X, m.Pos_Y);
                            map.ArrMap[m.Pos_Y, new_X] = m.Symbol;
                            map.ArrMap[m.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && m.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = m.Pos_Y;
                            int new_Y = m.Pos_Y + 1;
                            m.newPosition(m.Pos_X, new_Y);
                            map.ArrMap[new_Y, m.Pos_X] = m.Symbol;
                            map.ArrMap[old_Y, m.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && m.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = m.Pos_Y;
                            int new_Y = m.Pos_Y - 1;
                            m.newPosition(m.Pos_X, new_Y);
                            map.ArrMap[new_Y, m.Pos_X] = m.Symbol;
                            map.ArrMap[old_Y, m.Pos_X] = ".";
                        }
                    }
                }
                if (thisEnemy == "RangedUnit")
                {
                    RangedUnit enemyUnit = (RangedUnit)enemy;

                    int distance_X = m.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = m.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && m.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = m.Pos_X;
                            int new_X = m.Pos_X + 1;
                            m.newPosition(new_X, m.Pos_Y);
                            map.ArrMap[m.Pos_Y, new_X] = m.Symbol;
                            map.ArrMap[m.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && m.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = m.Pos_X;
                            int new_X = m.Pos_X - 1;
                            m.newPosition(new_X, m.Pos_Y);
                            map.ArrMap[m.Pos_Y, new_X] = m.Symbol;
                            map.ArrMap[m.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && m.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = m.Pos_Y;
                            int new_Y = m.Pos_Y + 1;
                            m.newPosition(m.Pos_X, new_Y);
                            map.ArrMap[new_Y, m.Pos_X] = m.Symbol;
                            map.ArrMap[old_Y, m.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && m.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = m.Pos_Y;
                            int new_Y = m.Pos_Y - 1;
                            m.newPosition(m.Pos_X, new_Y);
                            map.ArrMap[new_Y, m.Pos_X] = m.Symbol;
                            map.ArrMap[old_Y, m.Pos_X] = ".";
                        }
                    }
                }
                if (thisEnemy == "ArtilleryUnit")
                {
                    ArtilleryUnit enemyUnit = (ArtilleryUnit)enemy;

                    int distance_X = m.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = m.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && m.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = m.Pos_X;
                            int new_X = m.Pos_X + 1;
                            m.newPosition(new_X, m.Pos_Y);
                            map.ArrMap[m.Pos_Y, new_X] = m.Symbol;
                            map.ArrMap[m.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && m.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = m.Pos_X;
                            int new_X = m.Pos_X - 1;
                            m.newPosition(new_X, m.Pos_Y);
                            map.ArrMap[m.Pos_Y, new_X] = m.Symbol;
                            map.ArrMap[m.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && m.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = m.Pos_Y;
                            int new_Y = m.Pos_Y + 1;
                            m.newPosition(m.Pos_X, new_Y);
                            map.ArrMap[new_Y, m.Pos_X] = m.Symbol;
                            map.ArrMap[old_Y, m.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && m.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = m.Pos_Y;
                            int new_Y = m.Pos_Y - 1;
                            m.newPosition(m.Pos_X, new_Y);
                            map.ArrMap[new_Y, m.Pos_X] = m.Symbol;
                            map.ArrMap[old_Y, m.Pos_X] = ".";
                        }
                    }
                }
                if (thisEnemy == "MedicUnit")
                {
                    MedicUnit enemyUnit = (MedicUnit)enemy;

                    int distance_X = m.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = m.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && m.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = m.Pos_X;
                            int new_X = m.Pos_X + 1;
                            m.newPosition(new_X, m.Pos_Y);
                            map.ArrMap[m.Pos_Y, new_X] = m.Symbol;
                            map.ArrMap[m.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && m.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = m.Pos_X;
                            int new_X = m.Pos_X - 1;
                            m.newPosition(new_X, m.Pos_Y);
                            map.ArrMap[m.Pos_Y, new_X] = m.Symbol;
                            map.ArrMap[m.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && m.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = m.Pos_Y;
                            int new_Y = m.Pos_Y + 1;
                            m.newPosition(m.Pos_X, new_Y);
                            map.ArrMap[new_Y, m.Pos_X] = m.Symbol;
                            map.ArrMap[old_Y, m.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && m.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = m.Pos_Y;
                            int new_Y = m.Pos_Y - 1;
                            m.newPosition(m.Pos_X, new_Y);
                            map.ArrMap[new_Y, m.Pos_X] = m.Symbol;
                            map.ArrMap[old_Y, m.Pos_X] = ".";
                        }
                    }
                }
                if (thisEnemy == "ReconUnit")
                {
                    ReconUnit enemyUnit = (ReconUnit)enemy;

                    int distance_X = m.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = m.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && m.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = m.Pos_X;
                            int new_X = m.Pos_X + 1;
                            m.newPosition(new_X, m.Pos_Y);
                            map.ArrMap[m.Pos_Y, new_X] = m.Symbol;
                            map.ArrMap[m.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && m.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = m.Pos_X;
                            int new_X = m.Pos_X - 1;
                            m.newPosition(new_X, m.Pos_Y);
                            map.ArrMap[m.Pos_Y, new_X] = m.Symbol;
                            map.ArrMap[m.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && m.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = m.Pos_Y;
                            int new_Y = m.Pos_Y + 1;
                            m.newPosition(m.Pos_X, new_Y);
                            map.ArrMap[new_Y, m.Pos_X] = m.Symbol;
                            map.ArrMap[old_Y, m.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && m.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = m.Pos_Y;
                            int new_Y = m.Pos_Y - 1;
                            m.newPosition(m.Pos_X, new_Y);
                            map.ArrMap[new_Y, m.Pos_X] = m.Symbol;
                            map.ArrMap[old_Y, m.Pos_X] = ".";
                        }
                    }
                }
                if (thisEnemy == "StormTrooperUnit")
                {
                    StormTrooperUnit enemyUnit = (StormTrooperUnit)enemy;

                    int distance_X = m.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = m.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && m.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = m.Pos_X;
                            int new_X = m.Pos_X + 1;
                            m.newPosition(new_X, m.Pos_Y);
                            map.ArrMap[m.Pos_Y, new_X] = m.Symbol;
                            map.ArrMap[m.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && m.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = m.Pos_X;
                            int new_X = m.Pos_X - 1;
                            m.newPosition(new_X, m.Pos_Y);
                            map.ArrMap[m.Pos_Y, new_X] = m.Symbol;
                            map.ArrMap[m.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && m.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = m.Pos_Y;
                            int new_Y = m.Pos_Y + 1;
                            m.newPosition(m.Pos_X, new_Y);
                            map.ArrMap[new_Y, m.Pos_X] = m.Symbol;
                            map.ArrMap[old_Y, m.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && m.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = m.Pos_Y;
                            int new_Y = m.Pos_Y - 1;
                            m.newPosition(m.Pos_X, new_Y);
                            map.ArrMap[new_Y, m.Pos_X] = m.Symbol;
                            map.ArrMap[old_Y, m.Pos_X] = ".";
                        }
                    }
                }
                if (thisEnemy == "SupportUnit")
                {
                    SupportUnit enemyUnit = (SupportUnit)enemy;

                    int distance_X = m.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = m.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && m.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = m.Pos_X;
                            int new_X = m.Pos_X + 1;
                            m.newPosition(new_X, m.Pos_Y);
                            map.ArrMap[m.Pos_Y, new_X] = m.Symbol;
                            map.ArrMap[m.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && m.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = m.Pos_X;
                            int new_X = m.Pos_X - 1;
                            m.newPosition(new_X, m.Pos_Y);
                            map.ArrMap[m.Pos_Y, new_X] = m.Symbol;
                            map.ArrMap[m.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && m.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = m.Pos_Y;
                            int new_Y = m.Pos_Y + 1;
                            m.newPosition(m.Pos_X, new_Y);
                            map.ArrMap[new_Y, m.Pos_X] = m.Symbol;
                            map.ArrMap[old_Y, m.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && m.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = m.Pos_Y;
                            int new_Y = m.Pos_Y - 1;
                            m.newPosition(m.Pos_X, new_Y);
                            map.ArrMap[new_Y, m.Pos_X] = m.Symbol;
                            map.ArrMap[old_Y, m.Pos_X] = ".";
                        }
                    }
                }

            }
            if (thisType == "RangedUnit")
            {
                RangedUnit r = (RangedUnit)unit;
                if (thisEnemy == "MeleeUnit")
                {
                    MeleeUnit enemyUnit = (MeleeUnit)enemy;

                    int distance_X = r.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = r.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && r.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = r.Pos_X;
                            int new_X = r.Pos_X + 1;
                            r.newPosition(new_X, r.Pos_Y);
                            map.ArrMap[r.Pos_Y, new_X] = r.Symbol;
                            map.ArrMap[r.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && r.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = r.Pos_X;
                            int new_X = r.Pos_X - 1;
                            r.newPosition(new_X, r.Pos_Y);
                            map.ArrMap[r.Pos_Y, new_X] = r.Symbol;
                            map.ArrMap[r.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && r.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = r.Pos_Y;
                            int new_Y = r.Pos_Y + 1;
                            r.newPosition(r.Pos_X, new_Y);
                            map.ArrMap[new_Y, r.Pos_X] = r.Symbol;
                            map.ArrMap[old_Y, r.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && r.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = r.Pos_Y;
                            int new_Y = r.Pos_Y - 1;
                            r.newPosition(r.Pos_X, new_Y);
                            map.ArrMap[new_Y, r.Pos_X] = r.Symbol;
                            map.ArrMap[old_Y, r.Pos_X] = ".";
                        }
                    }
                }
                if (thisEnemy == "RangedUnit")
                {
                    RangedUnit enemyUnit = (RangedUnit)enemy;

                    int distance_X = r.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = r.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && r.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = r.Pos_X;
                            int new_X = r.Pos_X + 1;
                            r.newPosition(new_X, r.Pos_Y);
                            map.ArrMap[r.Pos_Y, new_X] = r.Symbol;
                            map.ArrMap[r.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && r.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = r.Pos_X;
                            int new_X = r.Pos_X - 1;
                            r.newPosition(new_X, r.Pos_Y);
                            map.ArrMap[r.Pos_Y, new_X] = r.Symbol;
                            map.ArrMap[r.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && r.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = r.Pos_Y;
                            int new_Y = r.Pos_Y + 1;
                            r.newPosition(r.Pos_X, new_Y);
                            map.ArrMap[new_Y, r.Pos_X] = r.Symbol;
                            map.ArrMap[old_Y, r.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && r.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = r.Pos_Y;
                            int new_Y = r.Pos_Y - 1;
                            r.newPosition(r.Pos_X, new_Y);
                            map.ArrMap[new_Y, r.Pos_X] = r.Symbol;
                            map.ArrMap[old_Y, r.Pos_X] = ".";
                        }
                    }
                }
                if (thisEnemy == "ArtilleryUnit")
                {
                    ArtilleryUnit enemyUnit = (ArtilleryUnit)enemy;

                    int distance_X = r.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = r.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && r.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = r.Pos_X;
                            int new_X = r.Pos_X + 1;
                            r.newPosition(new_X, r.Pos_Y);
                            map.ArrMap[r.Pos_Y, new_X] = r.Symbol;
                            map.ArrMap[r.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && r.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = r.Pos_X;
                            int new_X = r.Pos_X - 1;
                            r.newPosition(new_X, r.Pos_Y);
                            map.ArrMap[r.Pos_Y, new_X] = r.Symbol;
                            map.ArrMap[r.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && r.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = r.Pos_Y;
                            int new_Y = r.Pos_Y + 1;
                            r.newPosition(r.Pos_X, new_Y);
                            map.ArrMap[new_Y, r.Pos_X] = r.Symbol;
                            map.ArrMap[old_Y, r.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && r.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = r.Pos_Y;
                            int new_Y = r.Pos_Y - 1;
                            r.newPosition(r.Pos_X, new_Y);
                            map.ArrMap[new_Y, r.Pos_X] = r.Symbol;
                            map.ArrMap[old_Y, r.Pos_X] = ".";
                        }
                    }
                }
                if (thisEnemy == "MedicUnit")
                {
                    MedicUnit enemyUnit = (MedicUnit)enemy;

                    int distance_X = r.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = r.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && r.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = r.Pos_X;
                            int new_X = r.Pos_X + 1;
                            r.newPosition(new_X, r.Pos_Y);
                            map.ArrMap[r.Pos_Y, new_X] = r.Symbol;
                            map.ArrMap[r.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && r.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = r.Pos_X;
                            int new_X = r.Pos_X - 1;
                            r.newPosition(new_X, r.Pos_Y);
                            map.ArrMap[r.Pos_Y, new_X] = r.Symbol;
                            map.ArrMap[r.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && r.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = r.Pos_Y;
                            int new_Y = r.Pos_Y + 1;
                            r.newPosition(r.Pos_X, new_Y);
                            map.ArrMap[new_Y, r.Pos_X] = r.Symbol;
                            map.ArrMap[old_Y, r.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && r.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = r.Pos_Y;
                            int new_Y = r.Pos_Y - 1;
                            r.newPosition(r.Pos_X, new_Y);
                            map.ArrMap[new_Y, r.Pos_X] = r.Symbol;
                            map.ArrMap[old_Y, r.Pos_X] = ".";
                        }
                    }
                }
                if (thisEnemy == "ReconUnit")
                {
                    ReconUnit enemyUnit = (ReconUnit)enemy;

                    int distance_X = r.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = r.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && r.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = r.Pos_X;
                            int new_X = r.Pos_X + 1;
                            r.newPosition(new_X, r.Pos_Y);
                            map.ArrMap[r.Pos_Y, new_X] = r.Symbol;
                            map.ArrMap[r.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && r.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = r.Pos_X;
                            int new_X = r.Pos_X - 1;
                            r.newPosition(new_X, r.Pos_Y);
                            map.ArrMap[r.Pos_Y, new_X] = r.Symbol;
                            map.ArrMap[r.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && r.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = r.Pos_Y;
                            int new_Y = r.Pos_Y + 1;
                            r.newPosition(r.Pos_X, new_Y);
                            map.ArrMap[new_Y, r.Pos_X] = r.Symbol;
                            map.ArrMap[old_Y, r.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && r.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = r.Pos_Y;
                            int new_Y = r.Pos_Y - 1;
                            r.newPosition(r.Pos_X, new_Y);
                            map.ArrMap[new_Y, r.Pos_X] = r.Symbol;
                            map.ArrMap[old_Y, r.Pos_X] = ".";
                        }
                    }
                }
                if (thisEnemy == "StormTrooperUnit")
                {
                    StormTrooperUnit enemyUnit = (StormTrooperUnit)enemy;

                    int distance_X = r.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = r.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && r.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = r.Pos_X;
                            int new_X = r.Pos_X + 1;
                            r.newPosition(new_X, r.Pos_Y);
                            map.ArrMap[r.Pos_Y, new_X] = r.Symbol;
                            map.ArrMap[r.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && r.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = r.Pos_X;
                            int new_X = r.Pos_X - 1;
                            r.newPosition(new_X, r.Pos_Y);
                            map.ArrMap[r.Pos_Y, new_X] = r.Symbol;
                            map.ArrMap[r.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && r.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = r.Pos_Y;
                            int new_Y = r.Pos_Y + 1;
                            r.newPosition(r.Pos_X, new_Y);
                            map.ArrMap[new_Y, r.Pos_X] = r.Symbol;
                            map.ArrMap[old_Y, r.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && r.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = r.Pos_Y;
                            int new_Y = r.Pos_Y - 1;
                            r.newPosition(r.Pos_X, new_Y);
                            map.ArrMap[new_Y, r.Pos_X] = r.Symbol;
                            map.ArrMap[old_Y, r.Pos_X] = ".";
                        }
                    }
                }
                if (thisEnemy == "SupportUnit")
                {
                    SupportUnit enemyUnit = (SupportUnit)enemy;

                    int distance_X = r.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = r.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && r.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = r.Pos_X;
                            int new_X = r.Pos_X + 1;
                            r.newPosition(new_X, r.Pos_Y);
                            map.ArrMap[r.Pos_Y, new_X] = r.Symbol;
                            map.ArrMap[r.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && r.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = r.Pos_X;
                            int new_X = r.Pos_X - 1;
                            r.newPosition(new_X, r.Pos_Y);
                            map.ArrMap[r.Pos_Y, new_X] = r.Symbol;
                            map.ArrMap[r.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && r.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = r.Pos_Y;
                            int new_Y = r.Pos_Y + 1;
                            r.newPosition(r.Pos_X, new_Y);
                            map.ArrMap[new_Y, r.Pos_X] = r.Symbol;
                            map.ArrMap[old_Y, r.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && r.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = r.Pos_Y;
                            int new_Y = r.Pos_Y - 1;
                            r.newPosition(r.Pos_X, new_Y);
                            map.ArrMap[new_Y, r.Pos_X] = r.Symbol;
                            map.ArrMap[old_Y, r.Pos_X] = ".";
                        }
                    }
                }
            }
            if (thisType == "ArtilleryUnit")
            {
                ArtilleryUnit ar = (ArtilleryUnit)unit;
                if (thisEnemy == "MeleeUnit")
                {
                    MeleeUnit enemyUnit = (MeleeUnit)enemy;

                    int distance_X = ar.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = ar.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && ar.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = ar.Pos_X;
                            int new_X = ar.Pos_X + 1;
                            ar.newPosition(new_X, ar.Pos_Y);
                            map.ArrMap[ar.Pos_Y, new_X] = ar.Symbol;
                            map.ArrMap[ar.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && ar.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = ar.Pos_X;
                            int new_X = ar.Pos_X - 1;
                            ar.newPosition(new_X, ar.Pos_Y);
                            map.ArrMap[ar.Pos_Y, new_X] = ar.Symbol;
                            map.ArrMap[ar.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && ar.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = ar.Pos_Y;
                            int new_Y = ar.Pos_Y + 1;
                            ar.newPosition(ar.Pos_X, new_Y);
                            map.ArrMap[new_Y, ar.Pos_X] = ar.Symbol;
                            map.ArrMap[old_Y, ar.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && ar.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = ar.Pos_Y;
                            int new_Y = ar.Pos_Y - 1;
                            ar.newPosition(ar.Pos_X, new_Y);
                            map.ArrMap[new_Y, ar.Pos_X] = ar.Symbol;
                            map.ArrMap[old_Y, ar.Pos_X] = ".";
                        }
                    }
                }
                if (thisEnemy == "RangedUnit")
                {
                    RangedUnit enemyUnit = (RangedUnit)enemy;

                    int distance_X = ar.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = ar.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && ar.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = ar.Pos_X;
                            int new_X = ar.Pos_X + 1;
                            ar.newPosition(new_X, ar.Pos_Y);
                            map.ArrMap[ar.Pos_Y, new_X] = ar.Symbol;
                            map.ArrMap[ar.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && ar.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = ar.Pos_X;
                            int new_X = ar.Pos_X - 1;
                            ar.newPosition(new_X, ar.Pos_Y);
                            map.ArrMap[ar.Pos_Y, new_X] = ar.Symbol;
                            map.ArrMap[ar.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && ar.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = ar.Pos_Y;
                            int new_Y = ar.Pos_Y + 1;
                            ar.newPosition(ar.Pos_X, new_Y);
                            map.ArrMap[new_Y, ar.Pos_X] = ar.Symbol;
                            map.ArrMap[old_Y, ar.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && ar.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = ar.Pos_Y;
                            int new_Y = ar.Pos_Y - 1;
                            ar.newPosition(ar.Pos_X, new_Y);
                            map.ArrMap[new_Y, ar.Pos_X] = ar.Symbol;
                            map.ArrMap[old_Y, ar.Pos_X] = ".";
                        }
                    }
                }
                if (thisEnemy == "ArtilleryUnit")
                {
                    ArtilleryUnit enemyUnit = (ArtilleryUnit)enemy;

                    int distance_X = ar.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = ar.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && ar.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = ar.Pos_X;
                            int new_X = ar.Pos_X + 1;
                            ar.newPosition(new_X, ar.Pos_Y);
                            map.ArrMap[ar.Pos_Y, new_X] = ar.Symbol;
                            map.ArrMap[ar.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && ar.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = ar.Pos_X;
                            int new_X = ar.Pos_X - 1;
                            ar.newPosition(new_X, ar.Pos_Y);
                            map.ArrMap[ar.Pos_Y, new_X] = ar.Symbol;
                            map.ArrMap[ar.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && ar.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = ar.Pos_Y;
                            int new_Y = ar.Pos_Y + 1;
                            ar.newPosition(ar.Pos_X, new_Y);
                            map.ArrMap[new_Y, ar.Pos_X] = ar.Symbol;
                            map.ArrMap[old_Y, ar.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && ar.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = ar.Pos_Y;
                            int new_Y = ar.Pos_Y - 1;
                            ar.newPosition(ar.Pos_X, new_Y);
                            map.ArrMap[new_Y, ar.Pos_X] = ar.Symbol;
                            map.ArrMap[old_Y, ar.Pos_X] = ".";
                        }
                    }
                }
                if (thisEnemy == "MedicUnit")
                {
                    MedicUnit enemyUnit = (MedicUnit)enemy;

                    int distance_X = ar.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = ar.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && ar.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = ar.Pos_X;
                            int new_X = ar.Pos_X + 1;
                            ar.newPosition(new_X, ar.Pos_Y);
                            map.ArrMap[ar.Pos_Y, new_X] = ar.Symbol;
                            map.ArrMap[ar.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && ar.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = ar.Pos_X;
                            int new_X = ar.Pos_X - 1;
                            ar.newPosition(new_X, ar.Pos_Y);
                            map.ArrMap[ar.Pos_Y, new_X] = ar.Symbol;
                            map.ArrMap[ar.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && ar.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = ar.Pos_Y;
                            int new_Y = ar.Pos_Y + 1;
                            ar.newPosition(ar.Pos_X, new_Y);
                            map.ArrMap[new_Y, ar.Pos_X] = ar.Symbol;
                            map.ArrMap[old_Y, ar.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && ar.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = ar.Pos_Y;
                            int new_Y = ar.Pos_Y - 1;
                            ar.newPosition(ar.Pos_X, new_Y);
                            map.ArrMap[new_Y, ar.Pos_X] = ar.Symbol;
                            map.ArrMap[old_Y, ar.Pos_X] = ".";
                        }
                    }
                }
                if (thisEnemy == "ReconUnit")
                {
                    ReconUnit enemyUnit = (ReconUnit)enemy;

                    int distance_X = ar.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = ar.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && ar.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = ar.Pos_X;
                            int new_X = ar.Pos_X + 1;
                            ar.newPosition(new_X, ar.Pos_Y);
                            map.ArrMap[ar.Pos_Y, new_X] = ar.Symbol;
                            map.ArrMap[ar.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && ar.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = ar.Pos_X;
                            int new_X = ar.Pos_X - 1;
                            ar.newPosition(new_X, ar.Pos_Y);
                            map.ArrMap[ar.Pos_Y, new_X] = ar.Symbol;
                            map.ArrMap[ar.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && ar.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = ar.Pos_Y;
                            int new_Y = ar.Pos_Y + 1;
                            ar.newPosition(ar.Pos_X, new_Y);
                            map.ArrMap[new_Y, ar.Pos_X] = ar.Symbol;
                            map.ArrMap[old_Y, ar.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && ar.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = ar.Pos_Y;
                            int new_Y = ar.Pos_Y - 1;
                            ar.newPosition(ar.Pos_X, new_Y);
                            map.ArrMap[new_Y, ar.Pos_X] = ar.Symbol;
                            map.ArrMap[old_Y, ar.Pos_X] = ".";
                        }
                    }
                }
                if (thisEnemy == "StormTrooperUnit")
                {
                    StormTrooperUnit enemyUnit = (StormTrooperUnit)enemy;

                    int distance_X = ar.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = ar.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && ar.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = ar.Pos_X;
                            int new_X = ar.Pos_X + 1;
                            ar.newPosition(new_X, ar.Pos_Y);
                            map.ArrMap[ar.Pos_Y, new_X] = ar.Symbol;
                            map.ArrMap[ar.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && ar.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = ar.Pos_X;
                            int new_X = ar.Pos_X - 1;
                            ar.newPosition(new_X, ar.Pos_Y);
                            map.ArrMap[ar.Pos_Y, new_X] = ar.Symbol;
                            map.ArrMap[ar.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && ar.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = ar.Pos_Y;
                            int new_Y = ar.Pos_Y + 1;
                            ar.newPosition(ar.Pos_X, new_Y);
                            map.ArrMap[new_Y, ar.Pos_X] = ar.Symbol;
                            map.ArrMap[old_Y, ar.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && ar.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = ar.Pos_Y;
                            int new_Y = ar.Pos_Y - 1;
                            ar.newPosition(ar.Pos_X, new_Y);
                            map.ArrMap[new_Y, ar.Pos_X] = ar.Symbol;
                            map.ArrMap[old_Y, ar.Pos_X] = ".";
                        }
                    }
                }
                if (thisEnemy == "SupportUnit")
                {
                    SupportUnit enemyUnit = (SupportUnit)enemy;

                    int distance_X = ar.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = ar.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && ar.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = ar.Pos_X;
                            int new_X = ar.Pos_X + 1;
                            ar.newPosition(new_X, ar.Pos_Y);
                            map.ArrMap[ar.Pos_Y, new_X] = ar.Symbol;
                            map.ArrMap[ar.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && ar.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = ar.Pos_X;
                            int new_X = ar.Pos_X - 1;
                            ar.newPosition(new_X, ar.Pos_Y);
                            map.ArrMap[ar.Pos_Y, new_X] = ar.Symbol;
                            map.ArrMap[ar.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && ar.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = ar.Pos_Y;
                            int new_Y = ar.Pos_Y + 1;
                            ar.newPosition(ar.Pos_X, new_Y);
                            map.ArrMap[new_Y, ar.Pos_X] = ar.Symbol;
                            map.ArrMap[old_Y, ar.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && ar.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = ar.Pos_Y;
                            int new_Y = ar.Pos_Y - 1;
                            ar.newPosition(ar.Pos_X, new_Y);
                            map.ArrMap[new_Y, ar.Pos_X] = ar.Symbol;
                            map.ArrMap[old_Y, ar.Pos_X] = ".";
                        }
                    }
                }
            }
            if (thisType == "MedicUnit")
            {
                MedicUnit me = (MedicUnit)unit;
                if (thisEnemy == "MeleeUnit")
                {
                    MeleeUnit enemyUnit = (MeleeUnit)enemy;

                    int distance_X = me.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = me.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && me.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = me.Pos_X;
                            int new_X = me.Pos_X + 1;
                            me.newPosition(new_X, me.Pos_Y);
                            map.ArrMap[me.Pos_Y, new_X] = me.Symbol;
                            map.ArrMap[me.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && me.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = me.Pos_X;
                            int new_X = me.Pos_X - 1;
                            me.newPosition(new_X, me.Pos_Y);
                            map.ArrMap[me.Pos_Y, new_X] = me.Symbol;
                            map.ArrMap[me.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && me.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = me.Pos_Y;
                            int new_Y = me.Pos_Y + 1;
                            me.newPosition(me.Pos_X, new_Y);
                            map.ArrMap[new_Y, me.Pos_X] = me.Symbol;
                            map.ArrMap[old_Y, me.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && me.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = me.Pos_Y;
                            int new_Y = me.Pos_Y - 1;
                            me.newPosition(me.Pos_X, new_Y);
                            map.ArrMap[new_Y, me.Pos_X] = me.Symbol;
                            map.ArrMap[old_Y, me.Pos_X] = ".";
                        }
                    }
                }
                if (thisEnemy == "RangedUnit")
                {
                    RangedUnit enemyUnit = (RangedUnit)enemy;

                    int distance_X = me.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = me.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && me.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = me.Pos_X;
                            int new_X = me.Pos_X + 1;
                            me.newPosition(new_X, me.Pos_Y);
                            map.ArrMap[me.Pos_Y, new_X] = me.Symbol;
                            map.ArrMap[me.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && me.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = me.Pos_X;
                            int new_X = me.Pos_X - 1;
                            me.newPosition(new_X, me.Pos_Y);
                            map.ArrMap[me.Pos_Y, new_X] = me.Symbol;
                            map.ArrMap[me.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && me.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = me.Pos_Y;
                            int new_Y = me.Pos_Y + 1;
                            me.newPosition(me.Pos_X, new_Y);
                            map.ArrMap[new_Y, me.Pos_X] = me.Symbol;
                            map.ArrMap[old_Y, me.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && me.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = me.Pos_Y;
                            int new_Y = me.Pos_Y - 1;
                            me.newPosition(me.Pos_X, new_Y);
                            map.ArrMap[new_Y, me.Pos_X] = me.Symbol;
                            map.ArrMap[old_Y, me.Pos_X] = ".";
                        }
                    }
                }
                if (thisEnemy == "ArtilleryUnit")
                {
                    ArtilleryUnit enemyUnit = (ArtilleryUnit)enemy;

                    int distance_X = me.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = me.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && me.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = me.Pos_X;
                            int new_X = me.Pos_X + 1;
                            me.newPosition(new_X, me.Pos_Y);
                            map.ArrMap[me.Pos_Y, new_X] = me.Symbol;
                            map.ArrMap[me.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && me.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = me.Pos_X;
                            int new_X = me.Pos_X - 1;
                            me.newPosition(new_X, me.Pos_Y);
                            map.ArrMap[me.Pos_Y, new_X] = me.Symbol;
                            map.ArrMap[me.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && me.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = me.Pos_Y;
                            int new_Y = me.Pos_Y + 1;
                            me.newPosition(me.Pos_X, new_Y);
                            map.ArrMap[new_Y, me.Pos_X] = me.Symbol;
                            map.ArrMap[old_Y, me.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && me.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = me.Pos_Y;
                            int new_Y = me.Pos_Y - 1;
                            me.newPosition(me.Pos_X, new_Y);
                            map.ArrMap[new_Y, me.Pos_X] = me.Symbol;
                            map.ArrMap[old_Y, me.Pos_X] = ".";
                        }
                    }
                }
                if (thisEnemy == "MedicUnit")
                {
                    MedicUnit enemyUnit = (MedicUnit)enemy;

                    int distance_X = me.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = me.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && me.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = me.Pos_X;
                            int new_X = me.Pos_X + 1;
                            me.newPosition(new_X, me.Pos_Y);
                            map.ArrMap[me.Pos_Y, new_X] = me.Symbol;
                            map.ArrMap[me.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && me.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = me.Pos_X;
                            int new_X = me.Pos_X - 1;
                            me.newPosition(new_X, me.Pos_Y);
                            map.ArrMap[me.Pos_Y, new_X] = me.Symbol;
                            map.ArrMap[me.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && me.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = me.Pos_Y;
                            int new_Y = me.Pos_Y + 1;
                            me.newPosition(me.Pos_X, new_Y);
                            map.ArrMap[new_Y, me.Pos_X] = me.Symbol;
                            map.ArrMap[old_Y, me.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && me.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = me.Pos_Y;
                            int new_Y = me.Pos_Y - 1;
                            me.newPosition(me.Pos_X, new_Y);
                            map.ArrMap[new_Y, me.Pos_X] = me.Symbol;
                            map.ArrMap[old_Y, me.Pos_X] = ".";
                        }
                    }
                }
                if (thisEnemy == "ReconUnit")
                {
                    ReconUnit enemyUnit = (ReconUnit)enemy;

                    int distance_X = me.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = me.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && me.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = me.Pos_X;
                            int new_X = me.Pos_X + 1;
                            me.newPosition(new_X, me.Pos_Y);
                            map.ArrMap[me.Pos_Y, new_X] = me.Symbol;
                            map.ArrMap[me.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && me.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = me.Pos_X;
                            int new_X = me.Pos_X - 1;
                            me.newPosition(new_X, me.Pos_Y);
                            map.ArrMap[me.Pos_Y, new_X] = me.Symbol;
                            map.ArrMap[me.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && me.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = me.Pos_Y;
                            int new_Y = me.Pos_Y + 1;
                            me.newPosition(me.Pos_X, new_Y);
                            map.ArrMap[new_Y, me.Pos_X] = me.Symbol;
                            map.ArrMap[old_Y, me.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && me.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = me.Pos_Y;
                            int new_Y = me.Pos_Y - 1;
                            me.newPosition(me.Pos_X, new_Y);
                            map.ArrMap[new_Y, me.Pos_X] = me.Symbol;
                            map.ArrMap[old_Y, me.Pos_X] = ".";
                        }
                    }
                }
                if (thisEnemy == "StormTrooperUnit")
                {
                    StormTrooperUnit enemyUnit = (StormTrooperUnit)enemy;

                    int distance_X = me.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = me.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && me.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = me.Pos_X;
                            int new_X = me.Pos_X + 1;
                            me.newPosition(new_X, me.Pos_Y);
                            map.ArrMap[me.Pos_Y, new_X] = me.Symbol;
                            map.ArrMap[me.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && me.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = me.Pos_X;
                            int new_X = me.Pos_X - 1;
                            me.newPosition(new_X, me.Pos_Y);
                            map.ArrMap[me.Pos_Y, new_X] = me.Symbol;
                            map.ArrMap[me.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && me.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = me.Pos_Y;
                            int new_Y = me.Pos_Y + 1;
                            me.newPosition(me.Pos_X, new_Y);
                            map.ArrMap[new_Y, me.Pos_X] = me.Symbol;
                            map.ArrMap[old_Y, me.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && me.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = me.Pos_Y;
                            int new_Y = me.Pos_Y - 1;
                            me.newPosition(me.Pos_X, new_Y);
                            map.ArrMap[new_Y, me.Pos_X] = me.Symbol;
                            map.ArrMap[old_Y, me.Pos_X] = ".";
                        }
                    }
                }
                if (thisEnemy == "SupportUnit")
                {
                    SupportUnit enemyUnit = (SupportUnit)enemy;

                    int distance_X = me.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = me.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && me.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = me.Pos_X;
                            int new_X = me.Pos_X + 1;
                            me.newPosition(new_X, me.Pos_Y);
                            map.ArrMap[me.Pos_Y, new_X] = me.Symbol;
                            map.ArrMap[me.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && me.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = me.Pos_X;
                            int new_X = me.Pos_X - 1;
                            me.newPosition(new_X, me.Pos_Y);
                            map.ArrMap[me.Pos_Y, new_X] = me.Symbol;
                            map.ArrMap[me.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && me.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = me.Pos_Y;
                            int new_Y = me.Pos_Y + 1;
                            me.newPosition(me.Pos_X, new_Y);
                            map.ArrMap[new_Y, me.Pos_X] = me.Symbol;
                            map.ArrMap[old_Y, me.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && me.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = me.Pos_Y;
                            int new_Y = me.Pos_Y - 1;
                            me.newPosition(me.Pos_X, new_Y);
                            map.ArrMap[new_Y, me.Pos_X] = me.Symbol;
                            map.ArrMap[old_Y, me.Pos_X] = ".";
                        }
                    }
                }
            }
            if (thisType == "ReconUnit")
            {
                ReconUnit re = (ReconUnit)unit;
                if (thisEnemy == "MeleeUnit")
                {
                    MeleeUnit enemyUnit = (MeleeUnit)enemy;

                    int distance_X = re.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = re.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && re.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = re.Pos_X;
                            int new_X = re.Pos_X + 1;
                            re.newPosition(new_X, re.Pos_Y);
                            map.ArrMap[re.Pos_Y, new_X] = re.Symbol;
                            map.ArrMap[re.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && re.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = re.Pos_X;
                            int new_X = re.Pos_X - 1;
                            re.newPosition(new_X, re.Pos_Y);
                            map.ArrMap[re.Pos_Y, new_X] = re.Symbol;
                            map.ArrMap[re.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && re.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = re.Pos_Y;
                            int new_Y = re.Pos_Y + 1;
                            re.newPosition(re.Pos_X, new_Y);
                            map.ArrMap[new_Y, re.Pos_X] = re.Symbol;
                            map.ArrMap[old_Y, re.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && re.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = re.Pos_Y;
                            int new_Y = re.Pos_Y - 1;
                            re.newPosition(re.Pos_X, new_Y);
                            map.ArrMap[new_Y, re.Pos_X] = re.Symbol;
                            map.ArrMap[old_Y, re.Pos_X] = ".";
                        }
                    }
                }
                if (thisEnemy == "RangedUnit")
                {
                    RangedUnit enemyUnit = (RangedUnit)enemy;

                    int distance_X = re.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = re.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && re.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = re.Pos_X;
                            int new_X = re.Pos_X + 1;
                            re.newPosition(new_X, re.Pos_Y);
                            map.ArrMap[re.Pos_Y, new_X] = re.Symbol;
                            map.ArrMap[re.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && re.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = re.Pos_X;
                            int new_X = re.Pos_X - 1;
                            re.newPosition(new_X, re.Pos_Y);
                            map.ArrMap[re.Pos_Y, new_X] = re.Symbol;
                            map.ArrMap[re.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && re.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = re.Pos_Y;
                            int new_Y = re.Pos_Y + 1;
                            re.newPosition(re.Pos_X, new_Y);
                            map.ArrMap[new_Y, re.Pos_X] = re.Symbol;
                            map.ArrMap[old_Y, re.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && re.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = re.Pos_Y;
                            int new_Y = re.Pos_Y - 1;
                            re.newPosition(re.Pos_X, new_Y);
                            map.ArrMap[new_Y, re.Pos_X] = re.Symbol;
                            map.ArrMap[old_Y, re.Pos_X] = ".";
                        }
                    }
                }
                if (thisEnemy == "ArtilleryUnit")
                {
                    ArtilleryUnit enemyUnit = (ArtilleryUnit)enemy;

                    int distance_X = re.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = re.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && re.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = re.Pos_X;
                            int new_X = re.Pos_X + 1;
                            re.newPosition(new_X, re.Pos_Y);
                            map.ArrMap[re.Pos_Y, new_X] = re.Symbol;
                            map.ArrMap[re.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && re.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = re.Pos_X;
                            int new_X = re.Pos_X - 1;
                            re.newPosition(new_X, re.Pos_Y);
                            map.ArrMap[re.Pos_Y, new_X] = re.Symbol;
                            map.ArrMap[re.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && re.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = re.Pos_Y;
                            int new_Y = re.Pos_Y + 1;
                            re.newPosition(re.Pos_X, new_Y);
                            map.ArrMap[new_Y, re.Pos_X] = re.Symbol;
                            map.ArrMap[old_Y, re.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && re.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = re.Pos_Y;
                            int new_Y = re.Pos_Y - 1;
                            re.newPosition(re.Pos_X, new_Y);
                            map.ArrMap[new_Y, re.Pos_X] = re.Symbol;
                            map.ArrMap[old_Y, re.Pos_X] = ".";
                        }
                    }
                }
                if (thisEnemy == "MedicUnit")
                {
                    MedicUnit enemyUnit = (MedicUnit)enemy;

                    int distance_X = re.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = re.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && re.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = re.Pos_X;
                            int new_X = re.Pos_X + 1;
                            re.newPosition(new_X, re.Pos_Y);
                            map.ArrMap[re.Pos_Y, new_X] = re.Symbol;
                            map.ArrMap[re.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && re.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = re.Pos_X;
                            int new_X = re.Pos_X - 1;
                            re.newPosition(new_X, re.Pos_Y);
                            map.ArrMap[re.Pos_Y, new_X] = re.Symbol;
                            map.ArrMap[re.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && re.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = re.Pos_Y;
                            int new_Y = re.Pos_Y + 1;
                            re.newPosition(re.Pos_X, new_Y);
                            map.ArrMap[new_Y, re.Pos_X] = re.Symbol;
                            map.ArrMap[old_Y, re.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && re.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = re.Pos_Y;
                            int new_Y = re.Pos_Y - 1;
                            re.newPosition(re.Pos_X, new_Y);
                            map.ArrMap[new_Y, re.Pos_X] = re.Symbol;
                            map.ArrMap[old_Y, re.Pos_X] = ".";
                        }
                    }
                }
                if (thisEnemy == "ReconUnit")
                {
                    ReconUnit enemyUnit = (ReconUnit)enemy;

                    int distance_X = re.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = re.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && re.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = re.Pos_X;
                            int new_X = re.Pos_X + 1;
                            re.newPosition(new_X, re.Pos_Y);
                            map.ArrMap[re.Pos_Y, new_X] = re.Symbol;
                            map.ArrMap[re.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && re.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = re.Pos_X;
                            int new_X = re.Pos_X - 1;
                            re.newPosition(new_X, re.Pos_Y);
                            map.ArrMap[re.Pos_Y, new_X] = re.Symbol;
                            map.ArrMap[re.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && re.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = re.Pos_Y;
                            int new_Y = re.Pos_Y + 1;
                            re.newPosition(re.Pos_X, new_Y);
                            map.ArrMap[new_Y, re.Pos_X] = re.Symbol;
                            map.ArrMap[old_Y, re.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && re.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = re.Pos_Y;
                            int new_Y = re.Pos_Y - 1;
                            re.newPosition(re.Pos_X, new_Y);
                            map.ArrMap[new_Y, re.Pos_X] = re.Symbol;
                            map.ArrMap[old_Y, re.Pos_X] = ".";
                        }
                    }
                }
                if (thisEnemy == "StormTrooperUnit")
                {
                    StormTrooperUnit enemyUnit = (StormTrooperUnit)enemy;

                    int distance_X = re.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = re.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && re.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = re.Pos_X;
                            int new_X = re.Pos_X + 1;
                            re.newPosition(new_X, re.Pos_Y);
                            map.ArrMap[re.Pos_Y, new_X] = re.Symbol;
                            map.ArrMap[re.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && re.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = re.Pos_X;
                            int new_X = re.Pos_X - 1;
                            re.newPosition(new_X, re.Pos_Y);
                            map.ArrMap[re.Pos_Y, new_X] = re.Symbol;
                            map.ArrMap[re.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && re.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = re.Pos_Y;
                            int new_Y = re.Pos_Y + 1;
                            re.newPosition(re.Pos_X, new_Y);
                            map.ArrMap[new_Y, re.Pos_X] = re.Symbol;
                            map.ArrMap[old_Y, re.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && re.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = re.Pos_Y;
                            int new_Y = re.Pos_Y - 1;
                            re.newPosition(re.Pos_X, new_Y);
                            map.ArrMap[new_Y, re.Pos_X] = re.Symbol;
                            map.ArrMap[old_Y, re.Pos_X] = ".";
                        }
                    }
                }
                if (thisEnemy == "SupportUnit")
                {
                    SupportUnit enemyUnit = (SupportUnit)enemy;

                    int distance_X = re.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = re.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && re.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = re.Pos_X;
                            int new_X = re.Pos_X + 1;
                            re.newPosition(new_X, re.Pos_Y);
                            map.ArrMap[re.Pos_Y, new_X] = re.Symbol;
                            map.ArrMap[re.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && re.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = re.Pos_X;
                            int new_X = re.Pos_X - 1;
                            re.newPosition(new_X, re.Pos_Y);
                            map.ArrMap[re.Pos_Y, new_X] = re.Symbol;
                            map.ArrMap[re.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && re.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = re.Pos_Y;
                            int new_Y = re.Pos_Y + 1;
                            re.newPosition(re.Pos_X, new_Y);
                            map.ArrMap[new_Y, re.Pos_X] = re.Symbol;
                            map.ArrMap[old_Y, re.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && re.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = re.Pos_Y;
                            int new_Y = re.Pos_Y - 1;
                            re.newPosition(re.Pos_X, new_Y);
                            map.ArrMap[new_Y, re.Pos_X] = re.Symbol;
                            map.ArrMap[old_Y, re.Pos_X] = ".";
                        }
                    }
                }
            }
            if (thisType == "StormTrooperUnit")
            {
                StormTrooperUnit st = (StormTrooperUnit)unit;
                if (thisEnemy == "MeleeUnit")
                {
                    MeleeUnit enemyUnit = (MeleeUnit)enemy;

                    int distance_X = st.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = st.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && st.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = st.Pos_X;
                            int new_X = st.Pos_X + 1;
                            st.newPosition(new_X, st.Pos_Y);
                            map.ArrMap[st.Pos_Y, new_X] = st.Symbol;
                            map.ArrMap[st.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && st.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = st.Pos_X;
                            int new_X = st.Pos_X - 1;
                            st.newPosition(new_X, st.Pos_Y);
                            map.ArrMap[st.Pos_Y, new_X] = st.Symbol;
                            map.ArrMap[st.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && st.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = st.Pos_Y;
                            int new_Y = st.Pos_Y + 1;
                            st.newPosition(st.Pos_X, new_Y);
                            map.ArrMap[new_Y, st.Pos_X] = st.Symbol;
                            map.ArrMap[old_Y, st.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && st.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = st.Pos_Y;
                            int new_Y = st.Pos_Y - 1;
                            st.newPosition(st.Pos_X, new_Y);
                            map.ArrMap[new_Y, st.Pos_X] = st.Symbol;
                            map.ArrMap[old_Y, st.Pos_X] = ".";
                        }
                    }
                }
                if (thisEnemy == "RangedUnit")
                {
                    RangedUnit enemyUnit = (RangedUnit)enemy;

                    int distance_X = st.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = st.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && st.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = st.Pos_X;
                            int new_X = st.Pos_X + 1;
                            st.newPosition(new_X, st.Pos_Y);
                            map.ArrMap[st.Pos_Y, new_X] = st.Symbol;
                            map.ArrMap[st.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && st.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = st.Pos_X;
                            int new_X = st.Pos_X - 1;
                            st.newPosition(new_X, st.Pos_Y);
                            map.ArrMap[st.Pos_Y, new_X] = st.Symbol;
                            map.ArrMap[st.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && st.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = st.Pos_Y;
                            int new_Y = st.Pos_Y + 1;
                            st.newPosition(st.Pos_X, new_Y);
                            map.ArrMap[new_Y, st.Pos_X] = st.Symbol;
                            map.ArrMap[old_Y, st.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && st.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = st.Pos_Y;
                            int new_Y = st.Pos_Y - 1;
                            st.newPosition(st.Pos_X, new_Y);
                            map.ArrMap[new_Y, st.Pos_X] = st.Symbol;
                            map.ArrMap[old_Y, st.Pos_X] = ".";
                        }
                    }
                }
                if (thisEnemy == "ArtilleryUnit")
                {
                    ArtilleryUnit enemyUnit = (ArtilleryUnit)enemy;

                    int distance_X = st.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = st.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && st.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = st.Pos_X;
                            int new_X = st.Pos_X + 1;
                            st.newPosition(new_X, st.Pos_Y);
                            map.ArrMap[st.Pos_Y, new_X] = st.Symbol;
                            map.ArrMap[st.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && st.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = st.Pos_X;
                            int new_X = st.Pos_X - 1;
                            st.newPosition(new_X, st.Pos_Y);
                            map.ArrMap[st.Pos_Y, new_X] = st.Symbol;
                            map.ArrMap[st.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && st.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = st.Pos_Y;
                            int new_Y = st.Pos_Y + 1;
                            st.newPosition(st.Pos_X, new_Y);
                            map.ArrMap[new_Y, st.Pos_X] = st.Symbol;
                            map.ArrMap[old_Y, st.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && st.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = st.Pos_Y;
                            int new_Y = st.Pos_Y - 1;
                            st.newPosition(st.Pos_X, new_Y);
                            map.ArrMap[new_Y, st.Pos_X] = st.Symbol;
                            map.ArrMap[old_Y, st.Pos_X] = ".";
                        }
                    }
                }
                if (thisEnemy == "MedicUnit")
                {
                    MedicUnit enemyUnit = (MedicUnit)enemy;

                    int distance_X = st.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = st.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && st.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = st.Pos_X;
                            int new_X = st.Pos_X + 1;
                            st.newPosition(new_X, st.Pos_Y);
                            map.ArrMap[st.Pos_Y, new_X] = st.Symbol;
                            map.ArrMap[st.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && st.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = st.Pos_X;
                            int new_X = st.Pos_X - 1;
                            st.newPosition(new_X, st.Pos_Y);
                            map.ArrMap[st.Pos_Y, new_X] = st.Symbol;
                            map.ArrMap[st.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && st.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = st.Pos_Y;
                            int new_Y = st.Pos_Y + 1;
                            st.newPosition(st.Pos_X, new_Y);
                            map.ArrMap[new_Y, st.Pos_X] = st.Symbol;
                            map.ArrMap[old_Y, st.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && st.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = st.Pos_Y;
                            int new_Y = st.Pos_Y - 1;
                            st.newPosition(st.Pos_X, new_Y);
                            map.ArrMap[new_Y, st.Pos_X] = st.Symbol;
                            map.ArrMap[old_Y, st.Pos_X] = ".";
                        }
                    }
                }
                if (thisEnemy == "ReconUnit")
                {
                    ReconUnit enemyUnit = (ReconUnit)enemy;

                    int distance_X = st.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = st.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && st.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = st.Pos_X;
                            int new_X = st.Pos_X + 1;
                            st.newPosition(new_X, st.Pos_Y);
                            map.ArrMap[st.Pos_Y, new_X] = st.Symbol;
                            map.ArrMap[st.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && st.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = st.Pos_X;
                            int new_X = st.Pos_X - 1;
                            st.newPosition(new_X, st.Pos_Y);
                            map.ArrMap[st.Pos_Y, new_X] = st.Symbol;
                            map.ArrMap[st.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && st.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = st.Pos_Y;
                            int new_Y = st.Pos_Y + 1;
                            st.newPosition(st.Pos_X, new_Y);
                            map.ArrMap[new_Y, st.Pos_X] = st.Symbol;
                            map.ArrMap[old_Y, st.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && st.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = st.Pos_Y;
                            int new_Y = st.Pos_Y - 1;
                            st.newPosition(st.Pos_X, new_Y);
                            map.ArrMap[new_Y, st.Pos_X] = st.Symbol;
                            map.ArrMap[old_Y, st.Pos_X] = ".";
                        }
                    }
                }
                if (thisEnemy == "StormTrooperUnit")
                {
                    StormTrooperUnit enemyUnit = (StormTrooperUnit)enemy;

                    int distance_X = st.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = st.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && st.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = st.Pos_X;
                            int new_X = st.Pos_X + 1;
                            st.newPosition(new_X, st.Pos_Y);
                            map.ArrMap[st.Pos_Y, new_X] = st.Symbol;
                            map.ArrMap[st.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && st.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = st.Pos_X;
                            int new_X = st.Pos_X - 1;
                            st.newPosition(new_X, st.Pos_Y);
                            map.ArrMap[st.Pos_Y, new_X] = st.Symbol;
                            map.ArrMap[st.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && st.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = st.Pos_Y;
                            int new_Y = st.Pos_Y + 1;
                            st.newPosition(st.Pos_X, new_Y);
                            map.ArrMap[new_Y, st.Pos_X] = st.Symbol;
                            map.ArrMap[old_Y, st.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && st.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = st.Pos_Y;
                            int new_Y = st.Pos_Y - 1;
                            st.newPosition(st.Pos_X, new_Y);
                            map.ArrMap[new_Y, st.Pos_X] = st.Symbol;
                            map.ArrMap[old_Y, st.Pos_X] = ".";
                        }
                    }
                }
                if (thisEnemy == "SupportUnit")
                {
                    SupportUnit enemyUnit = (SupportUnit)enemy;

                    int distance_X = st.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = st.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && st.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = st.Pos_X;
                            int new_X = st.Pos_X + 1;
                            st.newPosition(new_X, st.Pos_Y);
                            map.ArrMap[st.Pos_Y, new_X] = st.Symbol;
                            map.ArrMap[st.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && st.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = st.Pos_X;
                            int new_X = st.Pos_X - 1;
                            st.newPosition(new_X, st.Pos_Y);
                            map.ArrMap[st.Pos_Y, new_X] = st.Symbol;
                            map.ArrMap[st.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && st.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = st.Pos_Y;
                            int new_Y = st.Pos_Y + 1;
                            st.newPosition(st.Pos_X, new_Y);
                            map.ArrMap[new_Y, st.Pos_X] = st.Symbol;
                            map.ArrMap[old_Y, st.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && st.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = st.Pos_Y;
                            int new_Y = st.Pos_Y - 1;
                            st.newPosition(st.Pos_X, new_Y);
                            map.ArrMap[new_Y, st.Pos_X] = st.Symbol;
                            map.ArrMap[old_Y, st.Pos_X] = ".";
                        }
                    }
                }
            }
            if (thisType == "SupportUnit")
            {
                SupportUnit su = (SupportUnit)unit;
                if (thisEnemy == "MeleeUnit")
                {
                    MeleeUnit enemyUnit = (MeleeUnit)enemy;

                    int distance_X = su.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = su.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && su.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = su.Pos_X;
                            int new_X = su.Pos_X + 1;
                            su.newPosition(new_X, su.Pos_Y);
                            map.ArrMap[su.Pos_Y, new_X] = su.Symbol;
                            map.ArrMap[su.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && su.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = su.Pos_X;
                            int new_X = su.Pos_X - 1;
                            su.newPosition(new_X, su.Pos_Y);
                            map.ArrMap[su.Pos_Y, new_X] = su.Symbol;
                            map.ArrMap[su.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && su.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = su.Pos_Y;
                            int new_Y = su.Pos_Y + 1;
                            su.newPosition(su.Pos_X, new_Y);
                            map.ArrMap[new_Y, su.Pos_X] = su.Symbol;
                            map.ArrMap[old_Y, su.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && su.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = su.Pos_Y;
                            int new_Y = su.Pos_Y - 1;
                            su.newPosition(su.Pos_X, new_Y);
                            map.ArrMap[new_Y, su.Pos_X] = su.Symbol;
                            map.ArrMap[old_Y, su.Pos_X] = ".";
                        }
                    }
                }
                if (thisEnemy == "RangedUnit")
                {
                    RangedUnit enemyUnit = (RangedUnit)enemy;

                    int distance_X = su.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = su.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && su.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = su.Pos_X;
                            int new_X = su.Pos_X + 1;
                            su.newPosition(new_X, su.Pos_Y);
                            map.ArrMap[su.Pos_Y, new_X] = su.Symbol;
                            map.ArrMap[su.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && su.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = su.Pos_X;
                            int new_X = su.Pos_X - 1;
                            su.newPosition(new_X, su.Pos_Y);
                            map.ArrMap[su.Pos_Y, new_X] = su.Symbol;
                            map.ArrMap[su.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && su.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = su.Pos_Y;
                            int new_Y = su.Pos_Y + 1;
                            su.newPosition(su.Pos_X, new_Y);
                            map.ArrMap[new_Y, su.Pos_X] = su.Symbol;
                            map.ArrMap[old_Y, su.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && su.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = su.Pos_Y;
                            int new_Y = su.Pos_Y - 1;
                            su.newPosition(su.Pos_X, new_Y);
                            map.ArrMap[new_Y, su.Pos_X] = su.Symbol;
                            map.ArrMap[old_Y, su.Pos_X] = ".";
                        }
                    }
                }
                if (thisEnemy == "ArtilleryUnit")
                {
                    ArtilleryUnit enemyUnit = (ArtilleryUnit)enemy;

                    int distance_X = su.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = su.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && su.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = su.Pos_X;
                            int new_X = su.Pos_X + 1;
                            su.newPosition(new_X, su.Pos_Y);
                            map.ArrMap[su.Pos_Y, new_X] = su.Symbol;
                            map.ArrMap[su.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && su.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = su.Pos_X;
                            int new_X = su.Pos_X - 1;
                            su.newPosition(new_X, su.Pos_Y);
                            map.ArrMap[su.Pos_Y, new_X] = su.Symbol;
                            map.ArrMap[su.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && su.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = su.Pos_Y;
                            int new_Y = su.Pos_Y + 1;
                            su.newPosition(su.Pos_X, new_Y);
                            map.ArrMap[new_Y, su.Pos_X] = su.Symbol;
                            map.ArrMap[old_Y, su.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && su.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = su.Pos_Y;
                            int new_Y = su.Pos_Y - 1;
                            su.newPosition(su.Pos_X, new_Y);
                            map.ArrMap[new_Y, su.Pos_X] = su.Symbol;
                            map.ArrMap[old_Y, su.Pos_X] = ".";
                        }
                    }
                }
                if (thisEnemy == "MedicUnit")
                {
                    MedicUnit enemyUnit = (MedicUnit)enemy;

                    int distance_X = su.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = su.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && su.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = su.Pos_X;
                            int new_X = su.Pos_X + 1;
                            su.newPosition(new_X, su.Pos_Y);
                            map.ArrMap[su.Pos_Y, new_X] = su.Symbol;
                            map.ArrMap[su.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && su.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = su.Pos_X;
                            int new_X = su.Pos_X - 1;
                            su.newPosition(new_X, su.Pos_Y);
                            map.ArrMap[su.Pos_Y, new_X] = su.Symbol;
                            map.ArrMap[su.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && su.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = su.Pos_Y;
                            int new_Y = su.Pos_Y + 1;
                            su.newPosition(su.Pos_X, new_Y);
                            map.ArrMap[new_Y, su.Pos_X] = su.Symbol;
                            map.ArrMap[old_Y, su.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && su.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = su.Pos_Y;
                            int new_Y = su.Pos_Y - 1;
                            su.newPosition(su.Pos_X, new_Y);
                            map.ArrMap[new_Y, su.Pos_X] = su.Symbol;
                            map.ArrMap[old_Y, su.Pos_X] = ".";
                        }
                    }
                }
                if (thisEnemy == "ReconUnit")
                {
                    ReconUnit enemyUnit = (ReconUnit)enemy;

                    int distance_X = su.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = su.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && su.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = su.Pos_X;
                            int new_X = su.Pos_X + 1;
                            su.newPosition(new_X, su.Pos_Y);
                            map.ArrMap[su.Pos_Y, new_X] = su.Symbol;
                            map.ArrMap[su.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && su.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = su.Pos_X;
                            int new_X = su.Pos_X - 1;
                            su.newPosition(new_X, su.Pos_Y);
                            map.ArrMap[su.Pos_Y, new_X] = su.Symbol;
                            map.ArrMap[su.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && su.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = su.Pos_Y;
                            int new_Y = su.Pos_Y + 1;
                            su.newPosition(su.Pos_X, new_Y);
                            map.ArrMap[new_Y, su.Pos_X] = su.Symbol;
                            map.ArrMap[old_Y, su.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && su.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = su.Pos_Y;
                            int new_Y = su.Pos_Y - 1;
                            su.newPosition(su.Pos_X, new_Y);
                            map.ArrMap[new_Y, su.Pos_X] = su.Symbol;
                            map.ArrMap[old_Y, su.Pos_X] = ".";
                        }
                    }
                }
                if (thisEnemy == "StormTrooperUnit")
                {
                    StormTrooperUnit enemyUnit = (StormTrooperUnit)enemy;

                    int distance_X = su.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = su.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && su.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = su.Pos_X;
                            int new_X = su.Pos_X + 1;
                            su.newPosition(new_X, su.Pos_Y);
                            map.ArrMap[su.Pos_Y, new_X] = su.Symbol;
                            map.ArrMap[su.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && su.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = su.Pos_X;
                            int new_X = su.Pos_X - 1;
                            su.newPosition(new_X, su.Pos_Y);
                            map.ArrMap[su.Pos_Y, new_X] = su.Symbol;
                            map.ArrMap[su.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && su.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = su.Pos_Y;
                            int new_Y = su.Pos_Y + 1;
                            su.newPosition(su.Pos_X, new_Y);
                            map.ArrMap[new_Y, su.Pos_X] = su.Symbol;
                            map.ArrMap[old_Y, su.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && su.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = su.Pos_Y;
                            int new_Y = su.Pos_Y - 1;
                            su.newPosition(su.Pos_X, new_Y);
                            map.ArrMap[new_Y, su.Pos_X] = su.Symbol;
                            map.ArrMap[old_Y, su.Pos_X] = ".";
                        }
                    }
                }
                if (thisEnemy == "SupportUnit")
                {
                    SupportUnit enemyUnit = (SupportUnit)enemy;

                    int distance_X = su.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = su.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && su.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = su.Pos_X;
                            int new_X = su.Pos_X + 1;
                            su.newPosition(new_X, su.Pos_Y);
                            map.ArrMap[su.Pos_Y, new_X] = su.Symbol;
                            map.ArrMap[su.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && su.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = su.Pos_X;
                            int new_X = su.Pos_X - 1;
                            su.newPosition(new_X, su.Pos_Y);
                            map.ArrMap[su.Pos_Y, new_X] = su.Symbol;
                            map.ArrMap[su.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && su.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = su.Pos_Y;
                            int new_Y = su.Pos_Y + 1;
                            su.newPosition(su.Pos_X, new_Y);
                            map.ArrMap[new_Y, su.Pos_X] = su.Symbol;
                            map.ArrMap[old_Y, su.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && su.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = su.Pos_Y;
                            int new_Y = su.Pos_Y - 1;
                            su.newPosition(su.Pos_X, new_Y);
                            map.ArrMap[new_Y, su.Pos_X] = su.Symbol;
                            map.ArrMap[old_Y, su.Pos_X] = ".";
                        }
                    }
                }
            }
        }

        // Method to runaway
        public void runAway(Unit unit)
        {
            string[] unitType = unit.GetType().ToString().Split('.');
            string UnitType = unitType[unitType.Length - 1];
            int rndMove = rnd.Next(1, 5);
            switch (rndMove)
            {
                case 1:
                    {
                        // Randomly move up
                        if (UnitType == "RangedUnit")
                        {
                            RangedUnit r = (RangedUnit)unit;
                            if (r.Pos_Y != 0)
                            {
                                int old_Y = r.Pos_Y;
                                int new_Y = r.Pos_Y - 1;
                                r.newPosition(r.Pos_X, new_Y);
                                map.ArrMap[new_Y, r.Pos_X] = r.Symbol;
                                map.ArrMap[old_Y, r.Pos_X] = ".";
                            }
                        }
                        if (UnitType == "MeleeUnit")
                        {
                            MeleeUnit m = (MeleeUnit)unit;
                            if (m.Pos_Y != 0)
                            {
                                int old_Y = m.Pos_Y;
                                int new_Y = m.Pos_Y - 1;
                                m.newPosition(m.Pos_X, new_Y);
                                map.ArrMap[new_Y, m.Pos_X] = m.Symbol;
                                map.ArrMap[old_Y, m.Pos_X] = ".";
                            }
                        }
                        if (UnitType == "ArtilleryUnit")
                        {
                            ArtilleryUnit ar = (ArtilleryUnit)unit;
                            if (ar.Pos_Y != 0)
                            {
                                int old_Y = ar.Pos_Y;
                                int new_Y = ar.Pos_Y - 1;
                                ar.newPosition(ar.Pos_X, new_Y);
                                map.ArrMap[new_Y, ar.Pos_X] = ar.Symbol;
                                map.ArrMap[old_Y, ar.Pos_X] = ".";
                            }
                        }
                        if (UnitType == "MedicUnit")
                        {
                            MedicUnit me = (MedicUnit)unit;
                            if (me.Pos_Y != 0)
                            {
                                int old_Y = me.Pos_Y;
                                int new_Y = me.Pos_Y - 1;
                                me.newPosition(me.Pos_X, new_Y);
                                map.ArrMap[new_Y, me.Pos_X] = me.Symbol;
                                map.ArrMap[old_Y, me.Pos_X] = ".";
                            }
                        }
                        if (UnitType == "ReconUnit")
                        {
                            ReconUnit re = (ReconUnit)unit;
                            if (re.Pos_Y != 0)
                            {
                                int old_Y = re.Pos_Y;
                                int new_Y = re.Pos_Y - 1;
                                re.newPosition(re.Pos_X, new_Y);
                                map.ArrMap[new_Y, re.Pos_X] = re.Symbol;
                                map.ArrMap[old_Y, re.Pos_X] = ".";
                            }
                        }
                        if (UnitType == "StormTrooperUnit")
                        {
                            StormTrooperUnit st = (StormTrooperUnit)unit;
                            if (st.Pos_Y != 0)
                            {
                                int old_Y = st.Pos_Y;
                                int new_Y = st.Pos_Y - 1;
                                st.newPosition(st.Pos_X, new_Y);
                                map.ArrMap[new_Y, st.Pos_X] = st.Symbol;
                                map.ArrMap[old_Y, st.Pos_X] = ".";
                            }
                        }
                        if (UnitType == "SupportUnit")
                        {
                            SupportUnit su = (SupportUnit)unit;
                            if (su.Pos_Y != 0)
                            {
                                int old_Y = su.Pos_Y;
                                int new_Y = su.Pos_Y - 1;
                                su.newPosition(su.Pos_X, new_Y);
                                map.ArrMap[new_Y, su.Pos_X] = su.Symbol;
                                map.ArrMap[old_Y, su.Pos_X] = ".";
                            }
                        }
                    }
                    break;
                case 2:
                    {
                        // Randomly move left
                        if (UnitType == "RangedUnit")
                        {
                            RangedUnit r = (RangedUnit)unit;
                            if (r.Pos_X != 0)
                            {
                                int old_X = r.Pos_X;
                                int new_X = r.Pos_X - 1;
                                r.newPosition(new_X, r.Pos_Y);
                                map.ArrMap[r.Pos_Y, new_X] = r.Symbol;
                                map.ArrMap[r.Pos_Y, old_X] = ".";
                            }
                        }
                        if (UnitType == "MeleeUnit")
                        {
                            MeleeUnit m = (MeleeUnit)unit;
                            if (m.Pos_X != 0)
                            {
                                int old_X = m.Pos_X;
                                int new_X = m.Pos_X - 1;
                                m.newPosition(new_X, m.Pos_Y);
                                map.ArrMap[m.Pos_Y, new_X] = m.Symbol;
                                map.ArrMap[m.Pos_Y, old_X] = ".";
                            }
                        }
                        if (UnitType == "ArtilleryUnit")
                        {
                            ArtilleryUnit ar = (ArtilleryUnit)unit;
                            if (ar.Pos_X != 0)
                            {
                                int old_X = ar.Pos_X;
                                int new_X = ar.Pos_X - 1;
                                ar.newPosition(new_X, ar.Pos_Y);
                                map.ArrMap[ar.Pos_Y, new_X] = ar.Symbol;
                                map.ArrMap[ar.Pos_Y, old_X] = ".";
                            }
                        }
                        if (UnitType == "MedicUnit")
                        {
                            MedicUnit me = (MedicUnit)unit;
                            if (me.Pos_X != 0)
                            {
                                int old_X = me.Pos_X;
                                int new_X = me.Pos_X - 1;
                                me.newPosition(new_X, me.Pos_Y);
                                map.ArrMap[me.Pos_Y, new_X] = me.Symbol;
                                map.ArrMap[me.Pos_Y, old_X] = ".";
                            }
                        }
                        if (UnitType == "ReconUnit")
                        {
                            ReconUnit re = (ReconUnit)unit;
                            if (re.Pos_X != 0)
                            {
                                int old_X = re.Pos_X;
                                int new_X = re.Pos_X - 1;
                                re.newPosition(new_X, re.Pos_Y);
                                map.ArrMap[re.Pos_Y, new_X] = re.Symbol;
                                map.ArrMap[re.Pos_Y, old_X] = ".";
                            }
                        }
                        if (UnitType == "StormTrooperUnit")
                        {
                            StormTrooperUnit st = (StormTrooperUnit)unit;
                            if (st.Pos_X != 0)
                            {
                                int old_X = st.Pos_X;
                                int new_X = st.Pos_X - 1;
                                st.newPosition(new_X, st.Pos_Y);
                                map.ArrMap[st.Pos_Y, new_X] = st.Symbol;
                                map.ArrMap[st.Pos_Y, old_X] = ".";
                            }
                        }
                        if (UnitType == "SupportUnit")
                        {
                            SupportUnit su = (SupportUnit)unit;
                            if (su.Pos_X != 0)
                            {
                                int old_X = su.Pos_X;
                                int new_X = su.Pos_X - 1;
                                su.newPosition(new_X, su.Pos_Y);
                                map.ArrMap[su.Pos_Y, new_X] = su.Symbol;
                                map.ArrMap[su.Pos_Y, old_X] = ".";
                            }
                        }
                    }
                    break;
                case 3:
                    {
                        // Randomly move down
                        if (UnitType == "RangedUnit")
                        {
                            RangedUnit r = (RangedUnit)unit;
                            if (r.Pos_Y != 19)
                            {
                                int old_Y = r.Pos_Y;
                                int new_Y = r.Pos_Y + 1;
                                r.newPosition(r.Pos_X, new_Y);
                                map.ArrMap[new_Y, r.Pos_X] = r.Symbol;
                                map.ArrMap[old_Y, r.Pos_X] = ".";
                            }
                        }
                        if (UnitType == "MeleeUnit")
                        {
                            MeleeUnit m = (MeleeUnit)unit;
                            if (m.Pos_Y != 19)
                            {
                                int old_Y = m.Pos_Y;
                                int new_Y = m.Pos_Y + 1;
                                m.newPosition(m.Pos_X, new_Y);
                                map.ArrMap[new_Y, m.Pos_X] = m.Symbol;
                                map.ArrMap[old_Y, m.Pos_X] = ".";
                            }
                        }
                        if (UnitType == "ArtilleryUnit")
                        {
                            ArtilleryUnit ar = (ArtilleryUnit)unit;
                            if (ar.Pos_Y != 19)
                            {
                                int old_Y = ar.Pos_Y;
                                int new_Y = ar.Pos_Y + 1;
                                ar.newPosition(ar.Pos_X, new_Y);
                                map.ArrMap[new_Y, ar.Pos_X] = ar.Symbol;
                                map.ArrMap[old_Y, ar.Pos_X] = ".";
                            }
                        }
                        if (UnitType == "MedicUnit")
                        {
                            MedicUnit me = (MedicUnit)unit;
                            if (me.Pos_Y != 19)
                            {
                                int old_Y = me.Pos_Y;
                                int new_Y = me.Pos_Y + 1;
                                me.newPosition(me.Pos_X, new_Y);
                                map.ArrMap[new_Y, me.Pos_X] = me.Symbol;
                                map.ArrMap[old_Y, me.Pos_X] = ".";
                            }
                        }
                        if (UnitType == "ReconUnit")
                        {
                            ReconUnit re = (ReconUnit)unit;
                            if (re.Pos_Y != 19)
                            {
                                int old_Y = re.Pos_Y;
                                int new_Y = re.Pos_Y + 1;
                                re.newPosition(re.Pos_X, new_Y);
                                map.ArrMap[new_Y, re.Pos_X] = re.Symbol;
                                map.ArrMap[old_Y, re.Pos_X] = ".";
                            }
                        }
                        if (UnitType == "StormTrooperUnit")
                        {
                            StormTrooperUnit st = (StormTrooperUnit)unit;
                            if (st.Pos_Y != 19)
                            {
                                int old_Y = st.Pos_Y;
                                int new_Y = st.Pos_Y + 1;
                                st.newPosition(st.Pos_X, new_Y);
                                map.ArrMap[new_Y, st.Pos_X] = st.Symbol;
                                map.ArrMap[old_Y, st.Pos_X] = ".";
                            }
                        }
                        if (UnitType == "SupportUnit")
                        {
                            SupportUnit su = (SupportUnit)unit;
                            if (su.Pos_Y != 19)
                            {
                                int old_Y = su.Pos_Y;
                                int new_Y = su.Pos_Y + 1;
                                su.newPosition(su.Pos_X, new_Y);
                                map.ArrMap[new_Y, su.Pos_X] = su.Symbol;
                                map.ArrMap[old_Y, su.Pos_X] = ".";
                            }
                        }
                    }
                    break;
                case 4:
                    {
                        // Randomly move right
                        if (UnitType == "RangedUnit")
                        {
                            RangedUnit r = (RangedUnit)unit;
                            if (r.Pos_X != 19)
                            {
                                int old_X = r.Pos_X;
                                int new_X = r.Pos_X + 1;
                                r.newPosition(new_X, r.Pos_Y);
                                map.ArrMap[r.Pos_Y, new_X] = r.Symbol;
                                map.ArrMap[r.Pos_Y, old_X] = ".";
                            }
                        }
                        if (UnitType == "MeleeUnit")
                        {
                            MeleeUnit m = (MeleeUnit)unit;
                            if (m.Pos_X != 19)
                            {
                                int old_X = m.Pos_X;
                                int new_X = m.Pos_X + 1;
                                m.newPosition(new_X, m.Pos_Y);
                                map.ArrMap[m.Pos_Y, new_X] = m.Symbol;
                                map.ArrMap[m.Pos_Y, old_X] = ".";
                            }
                        }
                        if (UnitType == "ArtilleryUnit")
                        {
                            ArtilleryUnit ar = (ArtilleryUnit)unit;
                            if (ar.Pos_X != 19)
                            {
                                int old_X = ar.Pos_X;
                                int new_X = ar.Pos_X + 1;
                                ar.newPosition(new_X, ar.Pos_Y);
                                map.ArrMap[ar.Pos_Y, new_X] = ar.Symbol;
                                map.ArrMap[ar.Pos_Y, old_X] = ".";
                            }
                        }
                        if (UnitType == "MedicUnit")
                        {
                            MedicUnit me = (MedicUnit)unit;
                            if (me.Pos_X != 19)
                            {
                                int old_X = me.Pos_X;
                                int new_X = me.Pos_X + 1;
                                me.newPosition(new_X, me.Pos_Y);
                                map.ArrMap[me.Pos_Y, new_X] = me.Symbol;
                                map.ArrMap[me.Pos_Y, old_X] = ".";
                            }
                        }
                        if (UnitType == "ReconUnit")
                        {
                            ReconUnit re = (ReconUnit)unit;
                            if (re.Pos_X != 19)
                            {
                                int old_X = re.Pos_X;
                                int new_X = re.Pos_X + 1;
                                re.newPosition(new_X, re.Pos_Y);
                                map.ArrMap[re.Pos_Y, new_X] = re.Symbol;
                                map.ArrMap[re.Pos_Y, old_X] = ".";
                            }
                        }
                        if (UnitType == "StormTrooperUnit")
                        {
                            StormTrooperUnit st = (StormTrooperUnit)unit;
                            if (st.Pos_X != 19)
                            {
                                int old_X = st.Pos_X;
                                int new_X = st.Pos_X + 1;
                                st.newPosition(new_X, st.Pos_Y);
                                map.ArrMap[st.Pos_Y, new_X] = st.Symbol;
                                map.ArrMap[st.Pos_Y, old_X] = ".";
                            }
                        }
                        if (UnitType == "SupportUnit")
                        {
                            SupportUnit su = (SupportUnit)unit;
                            if (su.Pos_X != 19)
                            {
                                int old_X = su.Pos_X;
                                int new_X = su.Pos_X + 1;
                                su.newPosition(new_X, su.Pos_Y);
                                map.ArrMap[su.Pos_Y, new_X] = su.Symbol;
                                map.ArrMap[su.Pos_Y, old_X] = ".";
                            }
                        }
                    }
                    break;
            }
        }

        public int getArrSize()
        {
            return map.unitArray.Length;
        }

        public Unit[] getArray()
        {
            return map.unitArray;
        }

        public string getNum()
        {
            int count = 0;
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (map.ArrMap[i, j] == "m" || map.ArrMap[i, j] == "M" || map.ArrMap[i, j] == "r" || map.ArrMap[i, j] == "R")
                    {
                        count++;
                    }
                }
            }

            return "number of units is: " + count;
        }

        public string display(int time)
        {
            string s = "";
            for (int i = 0; i < map.buildingArray.Length; i++)
            {
                string[] buildingType = map.buildingArray[i].GetType().ToString().Split('.');
                string type = buildingType[buildingType.Length - 1];

                if (type == "ResourceBuilding")
                {
                    ResourceBuilding r_building = (ResourceBuilding)map.buildingArray[i];
                    // r_building.Generate(time);
                    s += r_building.toString();
                }
                else
                {
                    s += "No resource buildings were found in the array";
                }
            }
            return s;
        }

        public string getUnitInfo()
        {
            return map.unitInfo();
        }
    }
}
