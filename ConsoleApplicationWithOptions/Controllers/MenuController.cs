using System;
using System.Collections.Generic;
using ConsoleApplicationWithOptions.Helpers;
using ConsoleApplicationWithOptions.Views;

namespace ConsoleApplicationWithOptions.Controllers
{
    public class MenuController : Controller
    {
        public MenuController(List<Tuple<string, Action>> textAndAction) : base(textAndAction)
        {
            View = new MenuView(new List<string>(TextAndAction.ConvertAll(p => p.Item1)));
        }

        public override void Run()
        {
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
                    MarkIndex = MarkIndex > 0 ? --MarkIndex : TextAndAction.Count - 1;
                    break;
                case ConsoleKey.DownArrow:
                    MarkIndex = MarkIndex < TextAndAction.Count - 1 ? ++MarkIndex : 0;
                    break;
                case ConsoleKey.Enter:
                    if (TextAndAction[MarkIndex] != null)
                        TextAndAction[MarkIndex].Item2();
                    break; 
                default:
                    ConsoleHelper.WrongKeyMessage("Bad key");
                    break;
            }
        }
    }
}