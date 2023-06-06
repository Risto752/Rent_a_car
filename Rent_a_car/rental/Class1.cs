using System;
using System.Collections.Generic;
using System.Text;

namespace rental
{
    class ComboItem
    {

        public int Key { get; set; }
        public string Value { get; set; }
        public ComboItem(int key, string value)
        {
            Key = key; Value = value;
        }
        public override string ToString()
        {
            return Value;
        }


    }
}
