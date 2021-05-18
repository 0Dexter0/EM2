using System.Collections.Generic;

namespace EM3.Model
{
    class Parser
    {
        public List<string[]> ParseData(string data)
        {
            List<string[]> result = new();
            string[] lines = data.Split('\n');

            for (int i = 0; i < lines.Length; i++)
            {
                result.Add(lines[i].Split(' '));
            }

            return result;
        }
    }
}
