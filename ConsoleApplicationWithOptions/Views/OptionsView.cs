using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleApplicationWithOptions.Helpers;

namespace ConsoleApplicationWithOptions.Views
{
    public class OptionsView
    {
        private readonly string TOP_LEFT_CORNER = "┌";
        private readonly string TOP_RIGHT_CORNER = "┐";
        private readonly string BOTTOM_LEFT_CORNER = "└";
        private readonly string BOTTOM_RIGHT_CORNER = "┘";
        private readonly string VERTICAL_LINE = "│";
        private readonly string HORIZONTAL_LINE = "─";
        private List<string> options;
        private int selectedOptionIndex = -1;
        private int leftCursorPosition = -1;
        private int topCursorPosition = -1;
        private int largestOptionsLenght;

        public OptionsView(List<string> optionsList)
        {
            options = optionsList;
            largestOptionsLenght = options.Max(s => s.Length);
            leftCursorPosition = (Console.WindowWidth - largestOptionsLenght) / 2;
            topCursorPosition = (Console.WindowHeight - options.Count) / 2;
        }

        public void Display(ConsoleKey key)
        {
            verifyKey(key);
            Console.SetCursorPosition(leftCursorPosition, topCursorPosition);
            print();
        }

        private void verifyKey(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    selectedOptionIndex = selectedOptionIndex > 0 ? --selectedOptionIndex : (options.Count - 1);
                    break;
                case ConsoleKey.DownArrow:
                    selectedOptionIndex = selectedOptionIndex < options.Count - 1 ? ++selectedOptionIndex : 0;
                    break;
                case ConsoleKey.Enter:
                    // do nothing
                    break;
                default:
                    ConsoleHelper.WrongKeyMessage("Bad key");
                    break;
            }
        }

        private void print()
        {
            ConsoleHelper.SetConsoleColors();
            ConsoleHelper.PrintWithLeftSpace(TOP_LEFT_CORNER, leftCursorPosition);
            Console.Write(addHorizontalBorder());
            Console.WriteLine(TOP_RIGHT_CORNER);
            printOptions();
            ConsoleHelper.PrintWithLeftSpace(BOTTOM_LEFT_CORNER, leftCursorPosition);
            Console.Write(addHorizontalBorder());
            Console.Write(BOTTOM_RIGHT_CORNER);
            ConsoleHelper.SetConsoleColors(color: ConsoleColor.Black);
        }

        private StringBuilder addHorizontalBorder()
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = -1; i <= largestOptionsLenght; i++)
            {
                stringBuilder.Append(HORIZONTAL_LINE);
            }

            return stringBuilder;
        }

        private void printOptions()
        {
            ConsoleHelper.SetConsoleColors();
            for (int i = 0; i < options.Count; i++)
            {
                ConsoleHelper.PrintWithLeftSpace(VERTICAL_LINE, leftCursorPosition);
                if (selectedOptionIndex == i)
                    ConsoleHelper.SetConsoleColors(ConsoleColor.Green, ConsoleColor.Black);
                
                Console.Write(createLeftVartical(options[i].Length));
                Console.Write(options[i]);
                Console.Write(createLeftVartical(options[i].Length, true));
                ConsoleHelper.SetConsoleColors();
                Console.WriteLine(VERTICAL_LINE);
            }
        }

        private StringBuilder createLeftVartical(int optionLength, bool isRightBorder = false)
        {
            StringBuilder stringBuilder = new StringBuilder();
            var max = (largestOptionsLenght - (double) optionLength) / 2;

            if (!isRightBorder) max = Math.Ceiling(max);
            else max = Math.Floor(max);

            for (int i = 0; i <= max; i++)
                stringBuilder.Append(" ");

            return stringBuilder;
        }
    }
}