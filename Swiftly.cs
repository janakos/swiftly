using System;
using System.Collections.Generic;
using DataProcessor;

class Swiflty
{
    static void Main(string[] args)
    {
        Parser parser = new Parser();
        Dictionary<string, List<string>> parsedData = parser.ParseFile("C:\\Users\\alexj\\source\\repos\\swiftly\\sample.txt");

        //foreach (KeyValuePair<string, List<string>> item in parsedData) {
        //    Console.WriteLine(item.Key);
        //    foreach (string str in item.Value)
        //    {
        //        Console.WriteLine(str);
        //    }
        //}

        Transformer transformer = new Transformer();
        List<ProductRecord> finalData = transformer.TransToProductRecord(parsedData);

        foreach (var r in finalData)
        {
            Console.WriteLine(r);
        }
    }
}
