using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jordan_van_Zyl___18013347___GADE_Task_3
{
    public class GameEngine
    {
        Map map;
        Unit[] unit;

        int size_X;
        int size_Y;
        public int Size_X { get => size_X; set => size_X = value; }
        public int Size_Y { get => size_Y; set => size_Y = value; }

        public GameEngine(int size_X, int size_Y)
        {
            map = new Map(size_X, size_Y);
            map.updatePosition();
            this.size_X = size_X;
            this.size_Y = size_Y;
        }

        public string Map()
        {
            return map.redraw(Size_X, Size_Y);
        }
        
    }
}
