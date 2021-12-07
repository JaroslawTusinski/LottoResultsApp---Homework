using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

        public LottoController(Dictionary<string, Action> actionsDictionary = null) : base(actionsDictionary)
        {
            if (actionsDictionary != null)
                View = new LottoView(ActionsDictionary.Keys.ToList());
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

        public LottoController CountNumbers(int columns = 2)
        {
            List<string> toDisplay = new List<string>();
            for (int i = 0; i < MaxLottoNumber; i++)
            {
                var line = new StringBuilder(CreateLine(i));
                for (int j = 1; j < columns && i < MaxLottoNumber - 1; j++)
                {
                    ++i;
                    line.Append($" │ {CreateLine(i)}");
                }
                toDisplay.Add(line.ToString());
            }

            View = new LottoView(toDisplay);
            return this;
        }

        private string CreateLine(int i)
        {
            StringBuilder line = new StringBuilder();
            if (i + 1 <= 9)
                line = new StringBuilder(" ");
            return line.Append($"{(i + 1)}: {_numbersCount[i]}").ToString();
        }

        public LottoController GetNumbersSortedByFrequently(int quantity = 1, bool desc = false)
        {
            Array.Sort(_numbersCopy);
            if (desc)
                Array.Reverse(_numbersCopy);
            if (quantity < 1)
                quantity = 1;
            else if (quantity > _numbersCopy.Length)
                quantity = _numbersCopy.Length;
            
            List<string> toDisplay = new List<string>();
            for (int i = 0; i < quantity; i++)
                toDisplay.Add($"{Array.IndexOf(_numbersCount, _numbersCopy[i]) + 1}: {_numbersCopy[i]}");

            View = new LottoView(toDisplay);
            return this;
        }

        public LottoController RepeatedInfo()
        {
            View = new LottoView(new List<string>
            {
                (_repeats ? "N" : "Nie n") + "astąpiło powtórzenie"
            });
            return this;
        }
    }
}