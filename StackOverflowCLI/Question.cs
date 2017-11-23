using Colorful;
using System;
using System.Drawing;

namespace StackOverflowCLI
{
    class Question
    {
        public string Title;
        public string Body;
        public string AcceptedAnswer;

        public void Print()
        {
            string output = "{0} {1}" + Environment.NewLine + Environment.NewLine + "{2}" + Environment.NewLine + "{3}" + Environment.NewLine + Environment.NewLine + "{4}" + Environment.NewLine + "{5}" + Environment.NewLine;
            Formatter[] variables = new Formatter[]
            {
                new Formatter("Title:", Color.Green),
                new Formatter(Title, Color.White),
                new Formatter("Question:", Color.Green),
                new Formatter(Body, Color.White),
                new Formatter("Accepted Answer:", Color.Green),
                new Formatter(AcceptedAnswer, Color.White)
            };

            Colorful.Console.WriteLineFormatted(output, Color.White, variables);
        }
    }
}
