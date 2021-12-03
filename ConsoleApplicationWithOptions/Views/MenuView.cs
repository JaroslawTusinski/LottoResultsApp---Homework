using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleApplicationWithOptions.Controllers;
using ConsoleApplicationWithOptions.Helpers;

namespace ConsoleApplicationWithOptions.Views
{
    public class MenuView
    {
        private const string TopLeftCorner = "┌";
        private const string TopRightCorner = "┐";
        private const string BottomLeftCorner = "└";
        private const string BottomRightCorner = "┘";
        private const string VerticalLine = "│";
        private const string HorizontalLine = "─";
        private readonly List<string> _options;
        private int _selectedOptionIndex = -1;
        private int _leftCursorPosition = -1;
        private int _topCursorPosition = -1;
        private int _largestOptionsLenght;
        private readonly LottoController _lottoController;

        public MenuView(List<string> optionsList)
        {
            _options = optionsList;
            _largestOptionsLenght = _options.Max(s => s.Length);
            _leftCursorPosition = (Console.WindowWidth - _largestOptionsLenght) / 2;
            _topCursorPosition = (Console.WindowHeight - _options.Count) / 2;
            _lottoController = new LottoController();
        }

        public void Display(ConsoleKey key)
        {
            VerifyKey(key);
            Console.SetCursorPosition(_leftCursorPosition, _topCursorPosition);
            Print();
        }

        private void VerifyKey(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    _selectedOptionIndex = _selectedOptionIndex > 0 ? --_selectedOptionIndex : (_options.Count - 1);
                    break;
                case ConsoleKey.DownArrow:
                    _selectedOptionIndex = _selectedOptionIndex < _options.Count - 1 ? ++_selectedOptionIndex : 0;
                    break;
                case ConsoleKey.Enter:
                    _lottoController.Run(_selectedOptionIndex);
                    break;
                default:
                    ConsoleHelper.WrongKeyMessage("Bad key");
                    break;
            }
        }

        private void Print()
        {
            ConsoleHelper.SetConsoleColors();
            ConsoleHelper.PrintWithLeftSpace(TopLeftCorner, _leftCursorPosition);
            Console.Write(AddHorizontalBorder());
            Console.WriteLine(TopRightCorner);
            PrintOptions();
            ConsoleHelper.PrintWithLeftSpace(BottomLeftCorner, _leftCursorPosition);
            Console.Write(AddHorizontalBorder());
            Console.Write(BottomRightCorner);
            ConsoleHelper.SetConsoleColors(color: ConsoleColor.Black);
        }

        private StringBuilder AddHorizontalBorder()
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = -1; i <= _largestOptionsLenght; i++)
            {
                stringBuilder.Append(HorizontalLine);
            }

            return stringBuilder;
        }

        private void PrintOptions()
        {
            ConsoleHelper.SetConsoleColors();
            for (int i = 0; i < _options.Count; i++)
            {
                ConsoleHelper.PrintWithLeftSpace(VerticalLine, _leftCursorPosition);
                if (_selectedOptionIndex == i)
                    ConsoleHelper.SetConsoleColors(ConsoleColor.Green, ConsoleColor.Black);
                
                Console.Write(CreateLeftVertical(_options[i].Length));
                Console.Write(_options[i]);
                Console.Write(CreateLeftVertical(_options[i].Length, true));
                ConsoleHelper.SetConsoleColors();
                Console.WriteLine(VerticalLine);
            }
        }

        private StringBuilder CreateLeftVertical(int optionLength, bool isRightBorder = false)
        {
            StringBuilder stringBuilder = new StringBuilder();
            var max = (_largestOptionsLenght - (double) optionLength) / 2;

            if (!isRightBorder) max = Math.Ceiling(max);
            else max = Math.Floor(max);

            for (int i = 0; i <= max; i++)
                stringBuilder.Append(" ");

            return stringBuilder;
        }
    }
}