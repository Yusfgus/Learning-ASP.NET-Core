using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Principles.Solid.OpenClosed.After;

internal static class QuestionBank
{
    public static List<Question> Generate()
    {
        return new List<Question>
        {
            new WHQuestion
            {
                Title = "What are the four pillars of OOP?",
                Mark = 8
            },
            new MultipleChoiceQuestion
            {
                Title = "Which of the following are value types?",
                Mark = 6,
                Choices = new List<string>
                {
                    "A: Integer",
                    "B: Array",
                    "C: Single",
                    "D: String",
                    "E: Long",
                }
            },
            new TrueFalseQuestion
            {
                Title = "Earth is Bigger than sun?",
                Mark = 4
            },
            new MultipleChoiceQuestion
            {
                Title = "Which of the following is an 8-byte Integer?",
                Mark = 6,
                Choices = new List<string>
                {
                    "A.  Char",
                    "B.  Long",
                    "C.  Short",
                    "D.  Byte",
                    "E.  Integer"
                }
            },
            new MatchQuestion
            {
                Title = "Match Column One values with single choice from 2nd Column?",
                Mark = 10,
                Rows = new Dictionary<string, string>
                {
                    {"1", "A"},
                    {"2", "B"},
                    {"3", "C"},
                    {"4", "D"},
                    {"5", "E"}
                }
            }

        };
    }
}

