using System;
using System.Reflection;
using EasyConsole.Commands;

namespace EasyConsole.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class OnCommandAttribute : Attribute
    {
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
        /// Construct command from this attribute
        /// </summary>
        /// <returns></returns>
        public ConsoleCommand ConstructCommand(MethodBase method) => new (Name.ToLower(), Description, 
            string.IsNullOrEmpty(Usage) ? Name.ToLower() : Usage, ArgumentsCount, method);
        
        /// <summary>
        /// Console command constructor
        /// </summary>
        /// <param name="name">Command name</param>
        /// <param name="description">Description shown in help</param>
        /// <param name="usage">Usage shown if failed</param>
        /// <param name="args">Amount of arguments</param>
        public OnCommandAttribute(string name, string description, string usage = "", int args = 0)
        {
            Name = name;
            Description = description;
            Usage = usage;
            ArgumentsCount = args;
        }
    }
}