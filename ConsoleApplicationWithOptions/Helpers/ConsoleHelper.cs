using System;
using System.Threading;

namespace ConsoleApplicationWithOptions.Helpers
{
    public static class ConsoleHelper
    {
        public static void SetConsoleColors(
            ConsoleColor background = ConsoleColor.Black,
            ConsoleColor color = ConsoleColor.White
        )
        {
            Console.BackgroundColor = background;
            Console.ForegroundColor = color;
        }

        public static void PrintWithLeftSpace(string valueToPrint, int leftCursorPosition)
        {
            Console.CursorLeft = leftCursorPosition;
            Console.Write(valueToPrint);
        }

        public static void WrongKeyMessage(string message, bool verticalCenter = true, bool horizontalCenter = true)
        {
            ConsoleColor[,] errorColors =
            {
                {ConsoleColor.White, ConsoleColor.Black},
                {ConsoleColor.Black, ConsoleColor.White},
                {ConsoleColor.DarkRed, ConsoleColor.Green},
            };

            Console.Beep();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < errorColors.GetLength(0); j++)
                {
                    SetConsoleColors(errorColors[j, 0], errorColors[j, 1]);
                    Console.Clear();
                    Console.SetCursorPosition(
                        horizontalCenter ? (Console.WindowWidth - message.Length) / 2 : 0,
                        verticalCenter ? (Console.WindowHeight - 1) / 2 : 0
                    );
                    Console.Write(message);
                    Thread.Sleep(50);
                }
            }

            SetConsoleColors();
            Console.Clear();
        }
    }
}