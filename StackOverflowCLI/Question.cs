using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowCLI
{
    class Question
    {
        public string Title;
        public string Body;
        public string AcceptedAnswer;

        public string FormatAnswer()
        {
            return "Title: " + Title + Environment.NewLine + "Body: " + Body + Environment.NewLine + "AcceptedAnswer: " + AcceptedAnswer + Environment.NewLine;
        }
    }
}
