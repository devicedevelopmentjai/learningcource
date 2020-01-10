using System;
using System.Collections.Generic;
namespace SOLID
{
    public class BeforeSRP
    {
        private readonly IList<string> _entries;
        private static uint _count = 1;
        public BeforeSRP()
        {
            _entries = new List<string>();
        }
        public uint AddEntry(string value)
        {
            _entries.Add($"{_count++} : {value}");
            return _count;
        }
        public void RemoveEntry(uint index) => _entries.RemoveAt((int)index);
        
        public override string ToString() => string.Join(Environment.NewLine, _entries);

    }
}
