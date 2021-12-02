using System;
using System.Collections.Generic;
using ConsoleApplicationWithOptions.Views;

namespace ConsoleApplicationWithOptions.Controllers
{
    public class OptionsController
    {
        private OptionsView optionsView;

        public OptionsController(List<string> optionsList)
        {
            optionsView = new OptionsView(optionsList);
        }

        public void Run()
        {
            Console.CursorVisible = false;
            ConsoleKey key = ConsoleKey.DownArrow;
            do
            {
                Console.Clear();
                optionsView.Display(key);
                key = Console.ReadKey().Key;
            } while (key != ConsoleKey.Escape);
        }
    }
}