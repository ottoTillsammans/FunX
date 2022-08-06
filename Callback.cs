using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunX
{
    public class Callback
    {
        public delegate void Alarm();

        public delegate void Alarm<T>(T value);
    }
}
