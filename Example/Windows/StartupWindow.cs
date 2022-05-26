using System;
using EasyConsole.Attributes;
using EasyConsole.Rendering;

namespace Example.Windows
{
    public class StartupWindow : ConsoleWindow
    {
        protected override void _Draw()
        {
            Header("Example application 0.0.1", spacingVertical: 1,
                textColor: ConsoleColor.Red, separatorCharacter: '#', separatorColor: ConsoleColor.Yellow);

            RequestCommand();
        }

        [OnCommand("quit", "exit the application")]
        [OnCommand("qqq", "exit the application")]
        protected void Exit()
        {
            Environment.Exit(1);
        }

        [OnCommand("echo", "echo a message", "echo <message>")]
        protected void Echo(string message)
        {
            Text(message, ConsoleColor.Magenta);
            EndLine();
            
            RequestCommand();
        }
    }
}