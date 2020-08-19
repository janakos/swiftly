using System.Collections.Generic;

namespace DataProcessor
{
    public class Parse
    {

        string[] input_data;

        int[] product_id =                  new int[] { 0, 8 };
        int[] product_description =         new int[] { 9, 58 };
        int[] regular_singlular_price =     new int[] { 69, 7 };
        int[] promotional_singlular_price = new int[] { 78, 7 };
        int[] regular_split_price =         new int[] { 87, 7 };
        int[] promotional_split_price =     new int[] { 96, 7 };
        int[] regular_for_x =               new int[] { 105, 7 };
        int[] promotional_for_x =           new int[] { 114, 7 };
        int[] flags =                       new int[] { 123, 8 };
        int[] product_size =                new int[] { 133, 8 };

        int[,] input_cords = new int[,] {
            product_id,
            product_description,
            regular_singlular_price,
            promotional_singlular_pric,
            regular_split_price,
            promotional_split_price,
            regular_for_x,
            promotional_for_x,
            flags,
            product_size
        };

        public Parse(string[] input_data)
        {
            this.input_data = input_data;
        }

        public Dictionary<string, string>[] ParseFile()
        {
            Dictionary<string, string>[] output = new Dictionary<string, string>[];
            for (int i = 0; i < this.input_data.Length; i++)
            {
                output[i] = new Dictionary<string, string>();
                for (int j = 0; j < input_cords.Length; j++)
                {
                    output[i].Add(
                        nameof(input_cords[j]),
                        this.input_data[i].Substring(input_cords[j][0], input_cords[j][1])
                    );
                }
            }
        }
    }
}