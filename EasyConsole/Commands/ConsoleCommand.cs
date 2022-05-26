using System;
using System.Reflection;
using EasyConsole.Rendering;

namespace EasyConsole.Commands
{
    public class ConsoleCommand
    {
        public ConsoleCommand(string name, string description, string usage, 
            MethodBase method)
        {
            Name = name;
            Description = description;
            Usage = usage;
            Method = method;
        }

        /// <summary>
        /// Name of the command
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Description of the command
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Command's usage
        /// </summary>
        public string Usage { get; set; }
        
        /// <summary>
        /// Amount of arguments required
        /// </summary>
        public int ArgumentsCount { get; set; }

        /// <summary>
        /// Command's method
        /// </summary>
        public MethodBase Method { get; set; }

        /// <summary>
        /// Execute this command
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="args"></param>
        public void Execute(ConsoleWindow hWnd, string[] args)
        {
            var mParams = Method.GetParameters();
            var argz = new object[args.Length];
            
            // Check if there are enough arguments
            if (mParams.Length > args.Length)
            {
                throw new ArgumentException("Too few arguments passed.");
            }
            
            // Check if there are too many arguments
            if (mParams.Length < args.Length)
            {
                throw new ArgumentException("Too many arguments passed.");
            }
            
            for (var index = 0; index < args.Length; index++)
            {
                // Convert types to proper ones
                if(mParams[index].ParameterType != typeof(string))
                    argz[index] = Convert.ChangeType(args[index], mParams[index].ParameterType);
                else
                    argz[index] = args[index];
            }

            // ReSharper disable once CoVariantArrayConversion
            Method.Invoke(hWnd, argz);
        }
    }
}