using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shpoon
{
    public static class MyExtentions
    {
        public static List<List<T>> Split<T>(this List<T> list, Predicate<T> predicate, bool keepSeparetor)
        {
            var lists = new List<List<T>>();
            var current = new List<T>();

            lists.Add(current);

            for (int i = 0; i < list.Count; i++)
            {
                if (predicate(list[i]))
                {
                    if (keepSeparetor)
                        current.Add(list[i]);

                    current = new List<T>();
                    lists.Add(current);
                }
                else
                    current.Add(list[i]);
            }

            return lists;
        }

        public static int FindNextIndex<T>(this List<T> list, int index, Predicate<T> predicate)
        {
            for (int i = index; i < list.Count; i++)
                if (predicate(list[i]))
                    return i;

            return -1;
        }

        public static List<T> GetRange<T>(this List<T> list, int index)
        {
            var range = new List<T>();

            for (int i = index; i < list.Count; i++)
                range.Add(list[i]);

            return range;
        }

        public static List<T> GetRangeBetween<T>(this List<T> list, int beginIndex, int endIndex)
        {
            return list.GetRange(beginIndex, endIndex - beginIndex);
        }
        public static List<T> GetRangeWhile<T>(this List<T> list, int index, Predicate<T> predicate)
        {
            var range = new List<T>();

            while (predicate(list[index]))
            {
                range.Add(list[index]);
                index++;
            }

            return range;
        }
        
        public static List<T> GetRangeContained<T>(this List<T> list, int index, Predicate<T> open, Predicate<T> close)
        {
            var range = new List<T>();
            int openedNum = 0;

            if (open(list[index]))
                openedNum++;
            else
                return range;

            for (int i = index + 1; i < list.Count; i++)
            {
                if (open(list[i]))
                    openedNum++;
                else if (close(list[i]))
                    openedNum--;

                if (openedNum == 0)
                    break;
                else
                    range.Add(list[i]);
            }

            return range;
        }


    }
}
