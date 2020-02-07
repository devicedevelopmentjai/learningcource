using System;
using FactoryExcercise;
using SOLID;
using static System.Console;
namespace LetsMove
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Udemy!");
            /*
            BeforeSRP beforeSRP = new BeforeSRP();
            beforeSRP.AddEntry("Hi, am using Udemy!");
            beforeSRP.AddEntry("You may find good resources!");
            //WriteLine(beforeSRP);
            //beforeSRP.RemoveEntry(1);
            OpenCloseEntry.Start();
            beforeSRP.AddEntry("Happy code!");            
            //WriteLine(beforeSRP);*/
            
            // Factor pattern
            FactoryKickUp.Main();
        }
    }
}
