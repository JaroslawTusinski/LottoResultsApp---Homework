using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleApplicationWithOptions.Helpers;

namespace ConsoleApplicationWithOptions.Views
{
    public abstract class View
    {
        private const string TopLeftCorner = "┌";
        private const string TopRightCorner = "┐";
        private const string BottomLeftCorner = "└";
        private const string BottomRightCorner = "┘";
        private const string HorizontalLine = "─";
        protected const string VerticalLine = "│";
        protected readonly int LeftCursorPosition = -1;
        private readonly int _topCursorPosition = -1;
        private readonly int _largestOptionLength;
        protected readonly List<string> ToDisplay;

        protected View(List<string> toDisplay)
        {
            ToDisplay = toDisplay;
            _largestOptionLength = ToDisplay.Max(s => s.Length);
            LeftCursorPosition += (Console.WindowWidth - _largestOptionLength) / 2;
            _topCursorPosition += (Console.WindowHeight - ToDisplay.Count) / 2;
            _topCursorPosition = _topCursorPosition < 0 ? 0 : _topCursorPosition;
        }

        public void Display(int markIndex = -1)
        {
            Console.Clear();
            Console.SetCursorPosition(LeftCursorPosition, _topCursorPosition);
            ConsoleHelper.SetConsoleColors();
            PrintHorizontalBorder();
            Print(markIndex);
            PrintHorizontalBorder(false);
            ConsoleHelper.SetConsoleColors(color: ConsoleColor.Black);
            Console.CursorVisible = false;
        }

        protected virtual void Print(int markIndex)
        {
            foreach (var lineToDisplay in ToDisplay)
            {
                ConsoleHelper.PrintWithLeftSpace(VerticalLine, LeftCursorPosition);
                Console.Write(CreateLine(lineToDisplay));
                ConsoleHelper.SetConsoleColors();
                Console.WriteLine(VerticalLine);
            }
        }

        protected StringBuilder CreateLine(string lineToDisplay)
        {
            StringBuilder stringBuilder = new StringBuilder();
            char spaceChar = ' ';
            var spaceLength = (_largestOptionLength - (double) lineToDisplay.Length) / 2;

            for (int i = 0; i <= Math.Ceiling(spaceLength); i++)
                stringBuilder.Append(spaceChar);
            stringBuilder.Append(lineToDisplay);
            for (int i = 0; i <= Math.Floor(spaceLength); i++)
                stringBuilder.Append(spaceChar);
            return stringBuilder;
        }

        private void PrintHorizontalBorder(bool top = true)
        {
            ConsoleHelper.PrintWithLeftSpace(top ? TopLeftCorner : BottomLeftCorner, LeftCursorPosition);
            for (int i = -1; i <= _largestOptionLength; i++)
                Console.Write(HorizontalLine);
            Console.WriteLine(top ? TopRightCorner : BottomRightCorner);
        }
    }
}