using System;
using System.Collections.Generic;
using ConsoleApplicationWithOptions.Views;

namespace ConsoleApplicationWithOptions.Controllers
{
    public abstract class Controller
    {
        protected readonly Dictionary<string, Action> ActionsDictionary;
        protected View View = null;
        protected int MarkIndex = -1;

        protected Controller(Dictionary<string, Action> actionsDictionary = null)
        {
            ActionsDictionary = actionsDictionary;
        }

        public virtual void Run()
        {
            if (View == null)
                return;
            View.Display(MarkIndex);
        }
    }
}