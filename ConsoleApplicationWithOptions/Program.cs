using System;
using System.Collections.Generic;
using ConsoleApplicationWithOptions.Controllers;

namespace ConsoleApplicationWithOptions
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            MenuController menuController = new MenuController(
                new List<Tuple<string, Action>>
                {
                    new Tuple<string, Action>("Zliczyć wystąpienie każdej z liczb?", null),
                    new Tuple<string, Action>("Która liczba została wylosowana najwięcej razy?", null),
                    new Tuple<string, Action>("Sześć liczb które zostały wylosowane najmniej razy?", null),
                    new Tuple<string, Action>("Czy kiedykolwiek nastąpiło powtórzenie?", null),
                }
            );
            menuController.Run();
        }
    }
}