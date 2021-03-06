﻿using System;
using System.Collections.Generic;
using DataProcessor;

class Swiftly
{
    static void Main(string[] args)
    {
        Parser parser = new Parser();
        Dictionary<string, List<string>> parsedData = parser.ParseFile("C:\\Users\\alexj\\source\\repos\\swiftly\\sample.txt");

        Transformer transformer = new Transformer();
        List<ProductRecord> finalData = transformer.TransformToProductRecord(parsedData);

        foreach (ProductRecord r in finalData)
        {
            Console.WriteLine(r);
        }
    }
}
