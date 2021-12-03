using System.Collections.Generic;
using ConsoleApplicationWithOptions.Controllers;

namespace ConsoleApplicationWithOptions
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            MenuController menuController = new MenuController(
                new List<string>
                {
                    "Zliczyć wystąpienie każdej z liczb?",
                    "Która liczba została wylosowana najwięcej razy?",
                    "Sześć liczb które zostały wylosowane najmniej razy?",
                    "Czy kiedykolwiek nastąpiło powtórzenie?",
                }
            );
            menuController.Run();
        }
    }
}