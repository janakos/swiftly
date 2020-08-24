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
             {"regularSinglularPrice",      new int[2] {69, 8}},
             {"promotionalSinglularPrice",  new int[2] {78, 8}},
             {"regularSplitPrice",          new int[2] {87, 8}},
             {"promotionalSplitPrice",      new int[2] {96, 8}},
             {"regularForX",                new int[2] {105, 8}},
             {"promotionalForX",            new int[2] {114, 8}},
             {"flags",                      new int[2] {123, 9}},
             {"productSize",                new int[2] {133, 9}},
        };

        /* 
         * Read data from a single file
         * Iterate over each row of text data and separate into disctinct fields
         * Logic used is defined by schema dictionary above
         */
        public Dictionary<string, List<string>> ParseFile(string path)
        {
            IEnumerable<string> inputData = File.ReadLines(path);
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