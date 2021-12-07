using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ConsoleApplicationWithOptions.Helpers;
using ConsoleApplicationWithOptions.Views;

namespace ConsoleApplicationWithOptions.Controllers
{
    public class LottoController : Controller
    {
        private const int MaxLottoNumber = 49;
        private readonly int[] _numbersCount = new int[MaxLottoNumber];
        private readonly int[] _numbersCopy = new int[MaxLottoNumber];
        private bool _repeats;

        public LottoController(List<Tuple<string, Action>> textAndAction = null) : base(textAndAction)
        {
            if (textAndAction != null)
                View = new LottoView(new List<string>(TextAndAction.ConvertAll(p => p.Item1)));
            PrepareData();
        }

        private string[] ParseLottoFileToNumbersArray()
        {
            // indexes, dates and numbers divided by space
            // numbers divided by commas
            // file dl.txt located in release folder
            string[] fileAsStrArr = File.ReadAllLines(Path.GetFullPath("dl.txt"));
            string[] lottoResults = new string[fileAsStrArr.Length];

            for (int i = 0; i < fileAsStrArr.Length; i++)
            {
                // index & date is not needed - we are only interested in numbers
                lottoResults[i] = fileAsStrArr[i].Split(' ')[2];
            }
            return lottoResults;
        }

        private void PrepareData()
        {
            string[] lottoResults = ParseLottoFileToNumbersArray();
            _repeats = lottoResults.Length != lottoResults.Distinct().Count();

            foreach (var lottoResult in lottoResults)
            {
                int[] lottoNumbers = Array.ConvertAll(lottoResult.Split(','), int.Parse);
                foreach (var number in lottoNumbers)
                {
                    ++_numbersCount[number - 1];
                    ++_numbersCopy[number - 1];
                }
            }
        }

        public override void Run()
        {
            if (View != null)
            {
                do
                {
                    View.Display();
                } while (RunKeyAction(Console.ReadKey().Key));
            }
        }

        private bool RunKeyAction(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    return false;
                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    return true;
                default:
                    ConsoleHelper.WrongKeyMessage("Bad key");
                    return true;
            }
        }

        public LottoController CountNumbers()
        {
            List<string> toDisplay = new List<string>();
            int halfOfSize = (MaxLottoNumber - 1) / 2;
            for (int i = 0; i < halfOfSize; i++)
                toDisplay.Add($"{i + 1}: {_numbersCount[i]}  |  {i + halfOfSize + 1}: {_numbersCount[i + halfOfSize]}");
            toDisplay.Add($"{MaxLottoNumber}: {_numbersCount[MaxLottoNumber - 1]}");

            View = new LottoView(toDisplay);
            return this;
        }

        public LottoController MostFrequently()
        {
            Array.Sort(_numbersCopy);
            Array.Reverse(_numbersCopy);
            int mostFrequently = _numbersCopy[0];
            View = new LottoView(new List<string>
            {
                $"{Array.IndexOf(_numbersCount, mostFrequently) + 1}: {mostFrequently}"
            });
            return this;
        }

        public LottoController SixLeastFrequently()
        {
            Array.Sort(_numbersCopy);
            List<string> toDisplay = new List<string>();
            for (int i = 0; i < 6 && i < _numbersCopy.Length; i++)
                toDisplay.Add($"{Array.IndexOf(_numbersCount, _numbersCopy[i]) + 1}: {_numbersCopy[i]}");

            View = new LottoView(toDisplay);
            return this;
        }

        public LottoController Repeated()
        {
            View = new LottoView(new List<string>
            {
                (_repeats ? "N" : "Nie n") + "astąpiło powtórzenie"
            });
            return this;
        }
    }
}