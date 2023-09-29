using System;
using System.Collections.Generic;

namespace Clinic.ViewModels
{
    public class EnumKeyValue
    {
        public int Index { get; set; }

        public String Name { get; set; }

        public EnumKeyValue()
        {
        }
        
        public EnumKeyValue(int index, string name)
        {
            Index = index;
            Name = name;
        }
    }
}
