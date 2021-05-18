using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM3.Model
{
    class Runner
    {
        private delegate double Sum(double var1, double var2);
        private delegate double Subtr(double var1, double var2);
        private delegate string Out(double value);
        private List<string[]> Lines { get; }

        public List<(string, Delegate)> Methods { get; private set; }

        public Runner(List<string[]> lines)
        {
            Lines = lines;
            Init();
        }

        private void Init()
        {
            Sum sum = Operations.Sum;
            Subtr subtr = Operations.Subtr;
            Out @out = Operations.Out;

            Methods.Add(("Sum", sum));
            Methods.Add(("Subtr", subtr));
            Methods.Add(("Out", @out));
        }

    }
}
