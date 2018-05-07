using StringSorter.Interface;
using System;
using System.Collections.Generic;

namespace StringSorter.Model
{
    // Merge Sort algorithm
    public class MergeSorter : ISorter
    {
        public string Sort(string s)
        {
            if (s.Length < 1)
            {
                throw new Exception("String length is less than minimum allowed(1).");
            }
            else if(s.Length == 1)
            {
                return s;
            }
            int level = 0;
            List<Tuple<int, int[]>> arr = GetGroups(s.Length, out level);

            char[] buff = s.ToCharArray();

            // Sort last level (smallest unit)
            SortLastLevel(ref buff, level, arr);

            // Do the merging
            Merge(ref buff, level, arr);

            return new string(buff);
        }

        // Merges the array groups by level
        private void Merge(
            ref char[] buff,
            int level,
            List<Tuple<int, int[]>> arr)
        {
            int len_i = level - 1;
            while (len_i >= 0)
            {
                Tuple<int, int[]> mem = arr[len_i];
                int[] grp = mem.Item2;
                int grp_i = 0;
                int last_i = -1;
                int left_i = 0;
                int right_i = 0;
                while (grp_i < mem.Item1)
                {
                    left_i = last_i + 1;
                    right_i = left_i + grp[grp_i];
                    // Swap left and right as necessary
                    Swap(ref buff, grp, grp_i, left_i, right_i);
                    last_i = last_i + grp[grp_i] + grp[grp_i + 1];
                    grp_i += 2;
                }
                len_i--;
            }
        }

        // Sorts the last level
        private void SortLastLevel
            (
            ref char[] buff,
            int level,
            List<Tuple<int, int[]>> arr
            )
        {
            int buff_i = 0;

            for (int i = 0; i < arr[level - 1].Item1; i++)
            {
                if (arr[level - 1].Item2[i] == 2)
                {
                    if (buff[buff_i + 1] < buff[buff_i])
                    {
                        char temp = buff[buff_i];
                        buff[buff_i] = buff[buff_i + 1];
                        buff[buff_i + 1] = temp;
                    }
                    buff_i += 2;
                }
                else
                {
                    buff_i++;
                }
            }
        }

        // Swap characters by groups of 2
        private void Swap(
            ref char[] buff,
            int[] grp,
            int grp_i,
            int left_i,
            int right_i)
        {
            char[] right_s = new char[grp[grp_i + 1]];
            char[] left_s = new char[grp[grp_i]];
            Array.Copy(buff, right_i, right_s, 0, grp[grp_i + 1]);
            Array.Copy(buff, left_i, left_s, 0, grp[grp_i]);
            int start = left_i;
            System.Collections.IEnumerator left_e = left_s.GetEnumerator();
            left_e.MoveNext();
            System.Collections.IEnumerator right_e = right_s.GetEnumerator();
            right_e.MoveNext();
            bool left_end = false;
            bool right_end = false;
            while (left_i < start + grp[grp_i] + grp[grp_i + 1])
            {
                if (right_end
                    || (!right_end && !left_end
                    && Convert.ToChar(left_e.Current) <= Convert.ToChar(right_e.Current)))
                {
                    buff[left_i] = Convert.ToChar(left_e.Current);
                    if (!left_end)
                        left_end = !left_e.MoveNext();
                    left_i++;
                }
                else
                {
                    buff[left_i] = Convert.ToChar(right_e.Current);
                    if (!right_end)
                        right_end = !right_e.MoveNext();
                    left_i++;
                }
                if (left_end && right_end)
                {
                    break;
                }
            }
        }

        // Generate the groupings
        private List<Tuple<int, int[]>> GetGroups(int len, out int level)
        {
            List<Tuple<int, int[]>> arr = new List<Tuple<int, int[]>>();
            bool end = false;
            level = 1;
            int count = 0;
            int groupIdx = 0;

            var a = Tuple.Create(0, new int[1]);
            a.Item2[0] = len;
            arr.Add(a);

            while (!end)
            {
                groupIdx = 0;
                len = (int)Math.Pow(2, level);
                int[] groups = new int[len];
                // Larger group at the left
                while (count < len)
                {
                    int j = arr[level - 1].Item2[groupIdx];
                    int left = j % 2 == 1 ? j / 2 + 1 : j / 2;
                    groups[count] = left;
                    count++;
                    int right = j / 2;
                    groups[count] = right;
                    if (left == 1 || right == 1)
                    {
                        end = true;
                    }
                    count++;
                    groupIdx++;
                }
                a = Tuple.Create(len, groups);
                arr.Add(a);
                count = 0;
                level++;
            }
            return arr;
        }

        public string Name
        {
            get { return "Merge Sort"; }
        }
    }
}
