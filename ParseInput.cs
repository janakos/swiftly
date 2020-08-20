using System.Collections.Generic;
using System.IO;
using System;


namespace DataProcessor
{
    public class Parser
    {
        string path;

        // Schema for input data, each field is specified by a name along with location and length
        Dictionary<string, int[]> field_cords = new Dictionary<string, int[]>
        {
             {"product_id",                     new int[2] {0, 8}},
             {"product_description",            new int[2] {9, 58}},
             {"regular_singlular_price",        new int[2] {69, 8}},
             {"promotional_singlular_price",    new int[2] {78, 8}},
             {"regular_split_price",            new int[2] {87, 8}},
             {"promotional_split_price",        new int[2] {96, 8}},
             {"regular_for_x",                  new int[2] {105, 8}},
             {"promotional_for_x",              new int[2] {114, 8}},
             {"flags",                          new int[2] {123, 9}},
             {"product_size",                   new int[2] {133, 9}},
        };

        public Parser(string path)
        {
            this.path = path;
        }

        /* 
         * Read data from a single file
         * Iterate over each row of text data and separate into disctinct fields
         * Logic used is defined by schema dictionary above
         */
        public Dictionary<string, List<string>> ParseFile()
        {
            IEnumerable<string> input_data = File.ReadLines(this.path);
            Dictionary<string, List<string>> output = InitializeOutputDict();

            foreach (string row in input_data)
            {
                foreach (KeyValuePair<string, int[]> field_cord in field_cords)
                {
                    output[field_cord.Key].Add(
                        row.Substring(field_cord.Value[0], field_cord.Value[1])
                    );
                }
            }
            return output;
        }

        private Dictionary<string, List<string>> InitializeOutputDict()
        {
            Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();
            foreach (KeyValuePair<string, int[]> field_cord in field_cords)
            {
                dict.Add(field_cord.Key, new List<string>());
            }
            return dict;
        }
    }
}