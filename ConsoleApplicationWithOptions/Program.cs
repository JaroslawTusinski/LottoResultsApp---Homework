using System;
using System.Collections.Generic;
using ConsoleApplicationWithOptions.Controllers;

namespace ConsoleApplicationWithOptions
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            LottoController lottoController = new LottoController();
            MenuController menuController = new MenuController(
                new Dictionary<string, Action>
                {
                    {"Zliczyć wystąpienie każdej z liczb?", () => lottoController.CountNumbers(7).Run()},
                    {"Która liczba została wylosowana najwięcej razy?", () => lottoController.GetNumbersSortedByFrequently(desc: true).Run()},
                    {"Sześć liczb które zostały wylosowane najmniej razy?", () => lottoController.GetNumbersSortedByFrequently(6).Run()},
                    {"Czy kiedykolwiek nastąpiło powtórzenie?", () => lottoController.RepeatedInfo().Run()},
                }
            );
            menuController.Run();
        }
    }
}