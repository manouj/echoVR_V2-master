using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Background
{
    enum mapTypes : int {empty=0, filled};
    class WorldMap
    {
        const int size = 10;
        public int[,] map = new int[size,size];
        Random rand;

        WorldMap()
        {
            rand = new Random();
            fillMap();
        }

        void fillMap()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    map[i, j] = GetRandomSpace();
                }
            }
        }

        int GetRandomSpace()
        {
            int typeCount = Enum.GetNames(typeof(mapTypes)).Length;
            return rand.Next(typeCount);
        }
    }
}
