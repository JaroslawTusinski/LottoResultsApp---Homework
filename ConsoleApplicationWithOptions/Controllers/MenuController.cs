using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApplicationWithOptions.Helpers;
using ConsoleApplicationWithOptions.Views;

namespace ConsoleApplicationWithOptions.Controllers
{
    public class MenuController : Controller
    {
        public MenuController(Dictionary<string, Action> actionsDictionary = null) : base(actionsDictionary)
        {
            if (actionsDictionary != null)
                View = new MenuView(ActionsDictionary.Keys.ToList());
        }

        public override void Run()
        {
            if (View == null)
                return;
            
            ConsoleKey key = ConsoleKey.DownArrow;
            do
            {
                RunKeyAction(key);
                View.Display(MarkIndex);
                key = Console.ReadKey().Key;
            } while (key != ConsoleKey.Escape);
        }

        private void RunKeyAction(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    MarkIndex = MarkIndex > 0 ? --MarkIndex : ActionsDictionary.Count - 1;
                    break;
                case ConsoleKey.DownArrow:
                    MarkIndex = MarkIndex < ActionsDictionary.Count - 1 ? ++MarkIndex : 0;
                    break;
                case ConsoleKey.Enter:
                    if (ActionsDictionary.Values.ElementAt(MarkIndex) != null)
                        ActionsDictionary.Values.ElementAt(MarkIndex)();
                    break;
                default:
                    ConsoleHelper.WrongKeyMessage("Bad key");
                    break;
            }
        }
    }
}