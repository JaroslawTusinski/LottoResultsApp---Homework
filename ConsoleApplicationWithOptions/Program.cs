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
                new List<Tuple<string, Action>>
                {
                    new Tuple<string, Action>("Zliczyć wystąpienie każdej z liczb?", () => lottoController.CountNumbers().Run()),
                    new Tuple<string, Action>("Która liczba została wylosowana najwięcej razy?", () => lottoController.MostFrequently().Run()),
                    new Tuple<string, Action>("Sześć liczb które zostały wylosowane najmniej razy?", () => lottoController.SixLeastFrequently().Run()),
                    new Tuple<string, Action>("Czy kiedykolwiek nastąpiło powtórzenie?", () => lottoController.Repeated().Run()),
                }
            );
            menuController.Run();
        }
    }
}