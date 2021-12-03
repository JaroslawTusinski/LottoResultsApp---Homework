using System;
using System.Collections.Generic;
using ConsoleApplicationWithOptions.Views;

namespace ConsoleApplicationWithOptions.Controllers
{
    public class MenuController
    {
        private readonly MenuView _menuView;

        public MenuController(List<string> optionsList)
        {
            _menuView = new MenuView(optionsList);
        }

        public void Run()
        {
            Console.CursorVisible = false;
            ConsoleKey key = ConsoleKey.DownArrow;
            do
            {
                Console.Clear();
                _menuView.Display(key);
                key = Console.ReadKey().Key;
            } while (key != ConsoleKey.Escape);
        }
    }
}