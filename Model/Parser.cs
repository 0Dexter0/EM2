using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EM3.Model
{
    class Parser
    {
        public string[] ParseData(string data)
        {
            string[] lines = data.Split('#');

            for (int i = 1; i < lines.Length; i++)
            {
                lines[i] = lines[i].Substring(2);
            }

            return lines;
        }

        public string[] SplitAndFormat(string data)
        {
            data = data.Replace('\r', ' ');
            var arr = data.Split('\n');

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = arr[i].Trim();
            }

            return arr;
        }
    }
}
