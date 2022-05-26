using System;
using System.Linq;
using System.Reflection;
using EasyConsole.Attributes;
using EasyConsole.Commands;

namespace EasyConsole.Rendering
{
    public abstract class ConsoleWindow
    {
        /// <summary>
        /// Input request symbol
        /// </summary>
        protected static string _inputSymbol = "> ";

        /// <summary>
        /// Commands processor
        /// </summary>
        protected CommandProcessor _commandProcessor = new ();
        
        /// <summary>
        /// Input symbol color
        /// </summary>
        protected static ConsoleColor _inputSymbolColor = ConsoleColor.White;
        
        /// <summary>
        /// End current console line
        /// </summary>
        public static void EndLine()
        {
            Console.WriteLine();
        }

        /// <summary>
        /// Change console foreground color
        /// </summary>
        public static void Color(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }

        /// <summary>
        /// Reset color to white
        /// </summary>
        public static void ResetColor() => Color(ConsoleColor.White);
        
        /// <summary>
        /// Draw text header eg.
        /// *******
        /// * APP *
        /// *******
        /// </summary>
        /// <param name="text">Text to write</param>
        /// <param name="spacingVertical">Vertical empty lines count before text</param>
        /// <param name="spacingHorizontal">Horizontal spaces count before text</param>
        /// <param name="separatorCharacter">Character used to draw frame</param>
        /// <param name="padHorizontal">Horizontal amount of frame characters</param>
        /// <param name="padVertical">Vertical amount of frame characters</param>
        /// <param name="textColor">Text color</param>
        /// <param name="separatorColor">Frame color</param>
        public static void Header(string text, int spacingVertical = 4, 
            int spacingHorizontal = 4, char separatorCharacter = '*', int padHorizontal = 1, int padVertical = 1,
            ConsoleColor textColor = ConsoleColor.White, ConsoleColor separatorColor = ConsoleColor.White)
        {
            var sizeHorizontal = text.Length + spacingHorizontal * 2 + padHorizontal * 2;
            
            // Draw padding
            for(var q = 0; q < padVertical; q++)
                Separator(sizeHorizontal, separatorCharacter, separatorColor);
            EndLine();

            for (var q = 0; q < spacingVertical; q++)
            {
                Separator(padHorizontal, separatorCharacter, separatorColor);
                Separator(spacingHorizontal*2 + text.Length, ' ');
                Separator(padHorizontal, separatorCharacter, separatorColor);
                EndLine();
            }

            // Draw text with padding
            Separator(padHorizontal, separatorCharacter, separatorColor);
            Separator(spacingHorizontal, ' ');
            Text(text, textColor);
            Separator(spacingHorizontal, ' ');
            Separator(padHorizontal, separatorCharacter, separatorColor);
            EndLine();
            
            // Draw padding
            for (var q = 0; q < spacingVertical; q++)
            {
                Separator(padHorizontal, separatorCharacter, separatorColor);
                Separator(spacingHorizontal*2 + text.Length, ' ');
                Separator(padHorizontal, separatorCharacter, separatorColor);
                EndLine();
            }
            
            for(var q = 0; q < padVertical; q++)
                Separator(sizeHorizontal, separatorCharacter, separatorColor);
            EndLine();
        }

        /// <summary>
        /// Draw separator
        /// </summary>
        /// <param name="length">Length of separator</param>
        /// <param name="separatorCharacter">Character to use</param>
        /// <param name="separatorColor">Custom color of characters</param>
        public static void Separator(int length, char separatorCharacter = '*', ConsoleColor separatorColor = ConsoleColor.White)
        {
            Color(separatorColor);
            
            for(var i = 0; i < length; i++)
                Console.Write(separatorCharacter);

            ResetColor();
        }

        /// <summary>
        /// Write regular text
        /// </summary>
        /// <param name="text">Text to write</param>
        /// <param name="color">Text color</param>
        public static void Text(string text, ConsoleColor color = ConsoleColor.White)
        {
            Color(color);
            
            Console.Write(text);
            
            ResetColor();
        }

        /// <summary>
        /// Draw fixed width text
        /// </summary>
        /// <param name="text">Text to write</param>
        /// <param name="length">Length of text</param>
        /// <param name="trim">Trim if text is too long</param>
        /// <param name="padCharacter">Character to pad at end</param>
        /// <param name="color">Text color</param>
        public static void FixedWidthText(string text, int length, bool trim = true, char padCharacter = ' ',
            ConsoleColor color = ConsoleColor.White)
        {
            // Check text length
            if (text.Length > length)
            {
                if (trim)
                {
                    text = text[..length];
                }
                else
                {
                    throw new ArgumentException("Text is too long");
                }
            }
            
            // Write text
            Text(text, color);
            
            // Pad with spaces
            for (var i = text.Length; i < length; i++) Console.Write(padCharacter);
        }

        /// <summary>
        /// Set input symbol to new one.
        /// </summary>
        /// <param name="symbol"></param>
        public static void SetInputSymbol(string symbol) => _inputSymbol = symbol;
        
        /// <summary>
        /// Set input symbol color to new one.
        /// </summary>
        /// <param name="color"></param>
        public static void SetInputSymbolColor(ConsoleColor color) => _inputSymbolColor = color;
        
        /// <summary>
        /// Request user's input
        /// </summary>
        public static string RequestInput()
        {
            Color(_inputSymbolColor);
            Text(_inputSymbol);
            ResetColor();
            
            return Console.ReadLine();
        }

        /// <summary>
        /// Clear console
        /// </summary>
        public static void Clear()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            
            ResetColor();
        }

        /// <summary>
        /// Move cursor to new position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void Cursor(int x, int y) => Console.SetCursorPosition(x, y);
        
        /// <summary>
        /// Draw this window
        /// </summary>
        public void Draw()
        {
            Clear();
            _Draw();
        }
        
        /// <summary>
        /// Internal method for drawing this window
        /// </summary>
        protected abstract void _Draw();

        [OnCommand("help", "show this help")]
        public void HelpCommand()
        {
            Text("Available commands:", ConsoleColor.Cyan);
            ResetColor();
            EndLine();

            var cWidth = _commandProcessor.GetCommands().Max(cmd => cmd.Name.Length);

            foreach (var v in _commandProcessor.GetCommands())
            {
                FixedWidthText(v.Name, cWidth + 4);
                Text(v.Description);
                EndLine();
            }
            
            RequestCommand();
        }

        public static void Error(string text)
        {
            Text($"[Error] {text}", ConsoleColor.Red);
            EndLine();
        }
        
        public static void Warning(string text)
        {
            Text($"[Warning] {text}", ConsoleColor.Yellow);
            EndLine();
        }
        
        public static void Success(string text)
        {
            Text($"[Success] {text}", ConsoleColor.Green);
            EndLine();
        }
        
        public static void Info(string text)
        {
            Text($"[Info] {text}");
            EndLine();
        }

        /// <summary>
        /// Request command from user
        /// </summary>
        public void RequestCommand()
        {
            var input = RequestInput();
            if (!_commandProcessor.TryToExecute(this, input))
            {
                Error("Unknown command. Type 'help' for help.");
                RequestCommand();
            }
        }

        /// <summary>
        /// Refresh window
        /// </summary>
        public void Refresh() => Draw();
        
        /// <summary>
        /// Open console window
        /// </summary>
        public virtual void Open()
        {
            // Do nothing by default, just draw ;)
            Draw();
        }
        
        public ConsoleWindow()
        {

            // Scan attributes for command processor and register commands
            foreach (var method in GetType().GetMethods(BindingFlags.NonPublic | 
                                                        BindingFlags.Instance | BindingFlags.Public))
            {
                foreach (var attribute in method.GetCustomAttributes(typeof(OnCommandAttribute), false))
                {
                    var attr = (OnCommandAttribute) attribute;
                    _commandProcessor.RegisterCommand(attr, method);
                }

                _commandProcessor.ReorderCommands();
            }
        }
    }
}