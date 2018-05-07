using StringSorter.Interface;
using System.Collections.Generic;

namespace StringSorter.Model
{
    public class Sorter
    {
        public string Sort(ISorter sorter, string s)
        {
            return sorter.Sort(s);
        }

        public IEnumerable<ISorter> GetSorters()
        {
            List<ISorter> sorters = new List<ISorter>();
            sorters.Add(new BubbleSorter());
            sorters.Add(new MergeSorter());
            return sorters;
        }
    }
}
