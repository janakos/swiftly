using System.IO;
using System.Collections.Generic;
using System.Reflection;
using DataProcessor;
using System;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Parser p = new Parser("C:\\Users\\alexj\\source\\repos\\swiftly\\sample.txt");
        List<Dictionary<string, string>> data = p.ParseFile();
        foreach (var row in data)
        {
            foreach (KeyValuePair<string, string> item in row)
            {
                Console.WriteLine(item.Key);
                Console.WriteLine(item.Value);
            }
        }
    }
}
