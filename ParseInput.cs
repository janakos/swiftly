using System;
using System.Collections.Generic;
using System.IO;

namespace DataProcessor
{
    public class Parser
    {
        string path;

        Dictionary<string, int[]> field_cords = new Dictionary<string, int[]>
        {
             {"product_id",                     new int[2] {0, 8}},
             {"product_description",            new int[2] {9, 58}},
             {"regular_singlular_price",        new int[2] {69, 7}},
             {"promotional_singlular_price",    new int[2] {78, 7}},
             {"regular_split_price",            new int[2] {87, 7}},
             {"promotional_split_price",        new int[2] {96, 7}},
             {"regular_for_x",                  new int[2] {105, 7}},
             {"promotional_for_x",              new int[2] {114, 8}},
             {"flags",                          new int[2] {123, 8}},
             {"product_size",                   new int[2] {133, 8}},
        };

        public Parser(string path)
        {
            this.path = path;
        }

        public List<Dictionary<string, string>> ParseFile()
        {
            IEnumerable<string> input_data = File.ReadLines(this.path);
            List<Dictionary<string, string>> output = new List<Dictionary<string, string>>();

            foreach (var row in input_data)
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                foreach (KeyValuePair<string, int[]> field_cord in field_cords)
                {
                    dict.Add(
                        field_cord.Key,
                        row.Substring(field_cord.Value[0], field_cord.Value[1])
                    );
                }
                output.Add(dict);
            }
            return output;
        }
    }
}