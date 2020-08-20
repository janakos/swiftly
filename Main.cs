using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System;
using DataProcessor;

class Program
{
    static void Main(string[] args)
    {
        // Test Input Parser
        Parser p = new Parser("C:\\Users\\alexj\\source\\repos\\swiftly\\sample.txt");
        Dictionary<string, List<string>> data = p.ParseFile();

        foreach (KeyValuePair<string, List<string>> item in data)
        {
            Console.WriteLine(item.Key);
            foreach (string s in item.Value)
            {
                Console.WriteLine(s);
            }
        }
    }
}
