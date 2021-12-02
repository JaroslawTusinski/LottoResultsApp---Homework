using System.Collections.Generic;
using ConsoleApplicationWithOptions.Controllers;

namespace ConsoleApplicationWithOptions
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            OptionsController optionsController = new OptionsController(
                new List<string>
                {
                    "Option",
                    "Option 2",
                    "Option three",
                    "Option 4",
                    "Last",
                }
            );
            optionsController.Run();
        }
    }
}