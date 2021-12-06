using System;
using System.Collections.Generic;
using ConsoleApplicationWithOptions.Helpers;

namespace ConsoleApplicationWithOptions.Views
{
    public class MenuView: View
    {
        public MenuView(List<string> toDisplay) : base(toDisplay){}

        protected override void Print(int markIndex)
        {
            for (int i = 0; i < ToDisplay.Count; i++)
            {
                ConsoleHelper.PrintWithLeftSpace(VerticalLine, LeftCursorPosition);
                if (markIndex == i)
                    ConsoleHelper.SetConsoleColors(ConsoleColor.Green, ConsoleColor.Black);
                
                Console.Write(CreateLine(ToDisplay[i]));
                ConsoleHelper.SetConsoleColors();
                Console.WriteLine(VerticalLine);
            }
        }
    }
}