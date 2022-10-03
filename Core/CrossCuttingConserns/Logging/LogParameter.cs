using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConserns.Logging
{
    public class LogParameter
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public object Type { get; set; }
    }
}
