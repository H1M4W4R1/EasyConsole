using System;
using EasyConsole.Rendering;
using Example.Windows;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleManager.Open<StartupWindow>();
            
            // Update refreshes the screen ;)
            while(true) ConsoleManager.Update();
        }
    }
}