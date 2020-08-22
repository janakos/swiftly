using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System;
using DataProcessor;
using System.Security.Cryptography.X509Certificates;

class Program
{

    static void Main(string[] args)
    {
        // Test Input Parser
        Parser p = new Parser();
        Dictionary<string, List<string>> parsed_data = p.ParseFile("C:\\Users\\alexj\\source\\repos\\swiftly\\sample.txt");

        Transformer t = new Transformer();
        List<ProductRecord> final_data = t.TransToProductRecord(parsed_data);
        foreach (var r in final_data)
        {
            Console.WriteLine(r);
        }



        //foreach (KeyValuePair<string, List<string>> item in data)
        //{
        //    Console.WriteLine(item.Key);
        //    foreach (string s in item.Value)
        //    {
        //        Console.WriteLine(s);
        //    }
        //}



        //Dictionary<string, List<object>> dict = new Dictionary<string, List<object>>()
        //{
        //    {"product_id", new List<object>() {1,4,8}},
        //    {"product_description", new List<object>() {"hello","ohno","lol"}}
        //};

        //List<ProductRecord> records = new List<ProductRecord>();

        //for (int i = 0; i < 3; i++)
        //{
        //    ProductRecord pr = new ProductRecord();
        //    foreach (KeyValuePair<string, List<object>> item in dict)
        //    {
        //        PropertyInfo propertyInfo = pr.GetType().GetProperty(item.Key);
        //        propertyInfo.SetValue(pr, Convert.ChangeType(item.Value[i], propertyInfo.PropertyType), null);
        //    }
        //    records.Add(pr);
        //}
    }
}
