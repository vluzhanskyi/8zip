using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8zip
{
    public class ChangeMaxProgressValueEventArgs
    {
         public int Value { get; set; }
         public ChangeMaxProgressValueEventArgs(int newValue)
        {
            Value = newValue;
        }
    }
}
