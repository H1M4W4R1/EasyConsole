using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using EasyConsole.Attributes;
using EasyConsole.Rendering;

namespace EasyConsole.Commands
{
    public class CommandProcessor
    {
        /// <summary>
        /// All commands in this processor
        /// </summary>
        protected List<ConsoleCommand> commands = new List<ConsoleCommand>();

        /// <summary>
        /// Get commands
        /// </summary>
        /// <returns></returns>
        internal List<ConsoleCommand> GetCommands() => commands;

        /// <summary>
        /// Register new console command
        /// </summary>
        /// <param name="cmd"></param>
        /// <exception cref="ArgumentException"></exception>
        public void RegisterCommand(ConsoleCommand cmd)
        {
            if (commands.Any(c => c.Name.Equals(cmd.Name)))
                throw new ArgumentException("Command already exists!");
            
            if (string.IsNullOrEmpty(cmd.Name))
                throw new ArgumentException("Command name cannot be null or empty!");
            
            commands.Add(cmd);
        }

        /// <summary>
        /// Register new console command using attribute override
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="onMethod">Method attribute is placed on</param>
        public void RegisterCommand(OnCommandAttribute cmd, MethodBase onMethod) =>
            RegisterCommand(cmd.ConstructCommand(onMethod));

        /// <summary>
        /// Reorder commands using alphabet ;)
        /// </summary>
        public void ReorderCommands()
        {
            commands = commands.OrderBy(c => c.Name).ToList();
        }

        /// <summary>
        /// Try to execute command
        /// </summary>
        /// <param name="source">Source console window</param>
        /// <param name="input">Input command</param>
        public bool TryToExecute(ConsoleWindow source, string input)
        {
            // Split
            var regex = new Regex(@"(?<="")\w[^""]*(?="")|\w+");
            var matches = regex.Matches(input).Select(q => q.Value).ToArray();

            // Check if found
            if (matches.Length < 1) return false;

            matches[0] = matches[0].ToLower().Trim(' ');
            
            // Find command
            var command = commands.FirstOrDefault(c => c.Name.Equals(matches[0]));
            
            // Check if found
            if (command == null) return false;

            try
            {
                // Try to execute command without error
                command.Execute(source, matches.Skip(1).ToArray());
                return true;
            }
            catch(Exception exception)
            {
                ConsoleWindow.Error(exception.Message);
                ConsoleWindow.Info("Usage: " + command.Usage);
                
                source.RequestCommand();
                return false;
            }
        }
    }
}