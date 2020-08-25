using System.Collections.Generic;
using System.IO;
using System;


namespace DataProcessor
{
    public class Parser
    {
        // Schema for input data, each field is specified by a name along with location and length
        Dictionary<string, int[]> FieldCords = new Dictionary<string, int[]>
        {
             {"productId",                  new int[2] {0, 8}},
             {"productDescription",         new int[2] {9, 58}},
             {"regularSingularPrice",       new int[2] {69, 8}},
             {"promotionalSingularPrice",   new int[2] {78, 8}},
             {"regularSplitPrice",          new int[2] {87, 8}},
             {"promotionalSplitPrice",      new int[2] {96, 8}},
             {"regularForX",                new int[2] {105, 8}},
             {"promotionalForX",            new int[2] {114, 8}},
             {"flags",                      new int[2] {123, 9}},
             {"productSize",                new int[2] {133, 9}},
        };

        /* 
         * Iterate through each line of text from file
         * Use FieldCords object to create substring representing each field
         */
        public Dictionary<string, List<string>> ParseFile(string path)
        {
            List<string> inputData = ReadLines(path);
            Dictionary<string, List<string>> output = InitializeOutputDict();

            foreach (string row in inputData)
            {
                foreach (KeyValuePair<string, int[]> fieldCord in FieldCords)
                {
                    output[fieldCord.Key].Add(
                        row.Substring(fieldCord.Value[0], fieldCord.Value[1])
                    );
                }
            }
            return output;
        }

        /*
         * Iterate through each char in file using a StreamReader object
         * Add to List<char> until newline is reach then append to List<string> lines
         * then clear List<char> line so we can take in a new set of chars
         */
        private List<string> ReadLines(string path)
        {
            List<string> lines = new List<string>();
            using (StreamReader streamReader = new StreamReader(path))
            {
                List<char> line = new List<char>();
                while (streamReader.Peek() >= 0)
                {
                    char c = (char)streamReader.Read();
                    if (c.Equals('\n')) {
                        lines.Add(new string(line.ToArray()));
                        line.Clear();
                    } 
                    else 
                    {
                        line.Add(c);
                    }
                }
            }
            return lines;
        }

        // Initialize Dictionary with appropriate keys and empty List<String>
        private Dictionary<string, List<string>> InitializeOutputDict()
        {
            Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();
            foreach (KeyValuePair<string, int[]> fieldCord in FieldCords)
            {
                dict.Add(fieldCord.Key, new List<string>());
            }
            return dict;
        }
    }
}
