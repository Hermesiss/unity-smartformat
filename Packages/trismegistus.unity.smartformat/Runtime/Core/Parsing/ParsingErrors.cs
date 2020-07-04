using System;
using System.Collections.Generic;
using System.Linq;

namespace Trismegistus.SmartFormat.Core.Parsing
{
    /// <summary>
    /// Represents parsing errors in a Format string.
    /// This exception only gets thrown when Parser.ErrorAction is set to ThrowError.
    /// </summary>
    public class ParsingErrors : Exception
    {
        private readonly Format result;

        public ParsingErrors(Format result)
        {
            this.result = result;
            Issues = new List<ParsingIssue>();
        }

        public List<ParsingIssue> Issues { get; }
        public bool HasIssues => Issues.Count > 0;

        public string MessageShort
        {
            get
            {
                return string.Format("The format string has {0} issue{1}: {2}",
                    Issues.Count,
                    Issues.Count == 1 ? "" : "s",
                    string.Join(", ", Issues.Select(i => i.Issue).ToArray())
                );
            }
        }

        public override string Message
        {
            get
            {
                var arrows = "";
                var lastArrow = 0;
                foreach (var issue in Issues)
                {
                    arrows += new string('-', issue.Index - lastArrow);
                    if (issue.Length > 0)
                    {
                        arrows += new string('^', Math.Max(issue.Length, 1));
                        lastArrow = issue.Index + issue.Length;
                    }
                    else
                    {
                        arrows += '^';
                        lastArrow = issue.Index + 1;
                    }
                }

                return string.Format("The format string has {0} issue{1}:\n{2}\nIn: \"{3}\"\nAt:  {4} ",
                    Issues.Count,
                    Issues.Count == 1 ? "" : "s",
                    string.Join(", ", Issues.Select(i => i.Issue).ToArray()),
                    result.baseString,
                    arrows
                );
            }
        }

        public void AddIssue(string issue, int startIndex, int endIndex)
        {
            Issues.Add(new ParsingIssue(issue, startIndex, endIndex - startIndex));
        }

        public class ParsingIssue
        {
            public ParsingIssue(string issue, int index, int length)
            {
                Issue = issue;
                Index = index;
                Length = length;
            }

            public int Index { get; }
            public int Length { get; }
            public string Issue { get; }
        }
    }
}
