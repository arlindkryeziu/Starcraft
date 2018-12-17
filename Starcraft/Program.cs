using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starcraft
{
    class Program
    {
        static int[] attacks = { 9, 3, 1 };

        static List<int[]> closedList = new List<int[]>();
        static void Main(string[] args)
        {
            int[] hp = { 54, 18, 6, 0 };
            Console.WriteLine(DestroySCV(hp));
            Console.ReadKey();
        }

        static int DestroySCV(int[] hp)
        {
            if (IsSCVDestroyted(hp))
                return hp[hp.Length - 1];

            List<int[]> combinations = GenerateCombinations(hp);

            int minValue = int.MaxValue;
            foreach (int[] item in combinations)
            {
                int[] temp = ClosedListContains(item);
                if (temp != null)
                    minValue = Math.Min(temp[item.Length - 1] + 1, minValue);
                else
                {
                    temp = item;
                    temp[temp.Length - 1] = DestroySCV(item) + 1;
                    closedList.Add(temp);
                    minValue = Math.Min(temp[temp.Length - 1], minValue);
                }
            }

            return minValue;
        }

        static List<int[]> GenerateCombinations(int[] array)
        {
            List<int[]> combinations = new List<int[]>();
            combinations.Add(new int[] { array[0] - attacks[0], array[1] - attacks[1], array[2] - attacks[2], 0 });
            combinations.Add(new int[] { array[0] - attacks[0], array[1] - attacks[2], array[2] - attacks[1], 0 });
            combinations.Add(new int[] { array[0] - attacks[1], array[1] - attacks[0], array[2] - attacks[2], 0 });
            combinations.Add(new int[] { array[0] - attacks[1], array[1] - attacks[2], array[2] - attacks[0], 0 });
            combinations.Add(new int[] { array[0] - attacks[2], array[1] - attacks[0], array[2] - attacks[1], 0 });
            combinations.Add(new int[] { array[0] - attacks[2], array[1] - attacks[1], array[2] - attacks[0], 0 });

            //for (int i = 0; i < array.Length - 1; i++)
            //{
            //    int[] temp = new int[array.Length];
            //    for (int j = 0; j < array.Length - 1; j++)
            //    {
            //        temp[j] = array[i] - attacks[j];
            //    }
            //    combinations.Add(temp);
            //}
            return combinations;
        }

        static bool IsSCVDestroyted(int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
                if (array[i] > 0)
                    return false;
            return true;
        }

        static int[] ClosedListContains(int[] array)
        {
            foreach (int[] item in closedList)
            {
                bool isFound = true;
                for (int i = 0; i < array.Length - 1; i++)
                    if (item[i] != array[i])
                    {
                        isFound = false;
                        continue;
                    }

                if (isFound)
                    return item;
            }
            return null;
        }

    }
}
