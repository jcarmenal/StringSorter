using StringSorter.Interface;
using System;
using System.Linq;
using System.Collections.Generic;

namespace StringSorter.Model
{
    // Bubble Sort algorithm
    public class BubbleSorter : ISorter
    {
        public string Sort(string s)
        {
            if (s.Length < 1)
            {
                throw new Exception("String length is less than minimum allowed(1).");
            }
            char[] buff = s.ToCharArray();
            for(int i = 0; i < buff.Length; i++)
                for (int j = 0; j < buff.Length; j++)
            {
                if(i != j && buff[i] < buff[j])
                {
                    char temp = buff[i];
                    buff[i] = buff[j];
                    buff[j] = temp;
                }
            }
            return new string(buff);
        }

        public string Name
        {
            get { return "Bubble Sort"; }
        }
    }
}
