# EasyConsole
Simple Console for C#.

# Current list of functions at ConsoleWindow level
* `EndLine()` - end current line
* `Color()` - set console color
  * color - new console color
* `ResetColor()` - revert to white
* `Header()` - create stylized app header
  * text - header text
  * <verticalSpacing> - vertical empty lines count before text
  * <horizontalSpacing> - horizontal spaces count before text
  * <separatorCharacter> - character to be used to draw frame
  * <horizontalPadding> - horizontal frame size
  * <verticalPadding> - vertical frame size
  * <textColor> - text color
  * <separatorColor> - frame color 
* `Separator()` - draw separator
  * length - amount of characters in separator
  * <character> - character to be used in separator
  * <color> - separator color
* `Text()` - write text, beware: default color is always white
  * text - text to write
  * color - text color
* `FixedWidthText()` - create fixed-width text
  * text - text to write
  * length - text length
  * <trim> - trim text if too long
  * <padCharacter> - padding character (eg. space)
  * <color> - text color
* `SetInputSymbol()` - change input symbol from '>' to anything else, shared between windows
  * symbol - new symbol
* `SetInputSymbolColor()` - change input symbol color, shared between windows
  * color - new color
* [string] `RequestInput()` - requests user input
* `RequestCommand()` - request command from user
* `Clear()` - clears console window
* `Cursor()` - set cursor position
  * x - left position
  * y - top position
* `Draw()` - draw window
* `Refresh()` - refresh window (alias to **Draw()**)
* `Open()` - open window
* `Error()` - print error
  * text - text to print
* `Warning()` - print warning
  * text - text to print
* `Info()` - print info
  * text - text to print
* `Success()` - print success
  * text - text to print

# How to use?
1. Create your program and include this library in your references
2. Copy Program.cs from Example to your application
3. Create new class named `StartupWindow` that extends from `ConsoleWindow`
4. Implement `_Draw()` method
5. Profit
  
# How to create command
In your `: ConsoleWindow` class create new method with text / number parameters.
Add `[OnCommand(name, description, <usage>)] attribute to it. Everyting else is automatic ;)
  
Note: it's recommended to place `RequestCommand()` at end of your command. 
  Otherwise window will automatically refresh after executing your command and all drawn text will be removed.

# How to change window?
If you want to change Console Window use `ConsoleManager.Open<T>` where `T` is type of class that extends from `ConsoleWindow`. 
Instance is constructed automatically if does not exist.
  
