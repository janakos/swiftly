using System;
using DataProcessor;

class Program
{
    static void Main(string[] args)
    {
        string[] lines = System.IO.File.ReadAllLines(@"C:\Users\alexj\source\repos\swiftly\input-sample.txt");
        Parse p = new Parse(lines);
        //string[] lines = System.IO.File.ReadAllLines(@Path.Combine(Directory.GetCurrentDirectory(), "\\fileName.txt"));
    }
}
