using System;
using System.Collections.Generic;
using System.IO;
using ConsoleApplicationWithOptions.Views;

namespace ConsoleApplicationWithOptions.Controllers
{
    public class LottoController
    {
        private readonly LottoView _lottoView;
        private Dictionary<string, Array> _lottoResults;

        public LottoController()
        {
            _lottoView = new LottoView();
            _lottoResults = ParseLottoFileToMap();
        }

        private Dictionary<string, Array> ParseLottoFileToMap()
        {
            // indexes, dates and numbers divided by space
            // numbers divided by commas
            // file dl.txt located in release folder
            string[] fileAsStrArr = File.ReadAllLines(Path.GetFullPath("dl.txt"));
            Dictionary<string, Array> lottoResults = new Dictionary<string, Array>();

            foreach (var lineOfFile in fileAsStrArr)
            {
                string[] dataFromLine = lineOfFile.Split(' ');
                lottoResults.Add(
                    dataFromLine[1],
                    Array.ConvertAll(dataFromLine[1].Split(','), int.Parse)
                );
            }
            return lottoResults;
        }

        public void Run(int optionIndex)
        {
            _lottoView.Display();
        }
    }
}