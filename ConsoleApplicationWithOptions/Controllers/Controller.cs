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

        protected Controller(List<Tuple<string, Action>> textAndAction = null)
        {
            if (textAndAction != null)
                TextAndAction = textAndAction;
        }

        public virtual void Run()
        {
            if (View != null)
                View.Display(MarkIndex);
        }
    }
}