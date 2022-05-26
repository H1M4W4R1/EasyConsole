using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyConsole.Rendering
{
    public static class ConsoleManager
    {
        private static List<ConsoleWindow> _windows = new List<ConsoleWindow>();

        private static ConsoleWindow _lastWindow;
        private static ConsoleWindow _currentWindow;
        
        private static ConsoleWindow GetWindow<T>() where T : ConsoleWindow =>
            _windows.FirstOrDefault(w => w is T);
        
        
        public static void Open<T>() where T : ConsoleWindow
        {
            var hWnd = GetWindow<T>();
            
            if (hWnd == null)
            {
                hWnd = Activator.CreateInstance<T>();
                _windows.Add(hWnd);
            }

            _currentWindow = hWnd;
        }

        /// <summary>
        /// Invoked every tick to prevent RAM shortage
        /// </summary>
        public static void Update()
        {
            if(_lastWindow != _currentWindow)
                _currentWindow?.Open();
            else
                _currentWindow?.Refresh();
        }
    }
}