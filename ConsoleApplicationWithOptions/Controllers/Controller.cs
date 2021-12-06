using System;
using System.Collections.Generic;
using ConsoleApplicationWithOptions.Views;

namespace ConsoleApplicationWithOptions.Controllers
{
    public abstract class Controller
    {
        protected readonly List<Tuple<string, Action>> TextAndAction;
        protected View View;
        protected int MarkIndex = -1;

        protected Controller(List<Tuple<string, Action>> textAndAction)
        {
            TextAndAction = textAndAction;
        }

        public virtual void Run()
        {
            View.Display(MarkIndex);
        }
    }
}