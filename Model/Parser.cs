using System.Collections.Generic;
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
    }
}
