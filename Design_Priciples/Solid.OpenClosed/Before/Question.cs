using System.Collections.Generic;

namespace Design_Principles.Solid.OpenClosed.Before;

class Question
{
    public string Title { get; set; }
    public int Mark { get; set; }
    public QuestionType QuestionType { get; set; }
    public List<string> Choices { get; set; } = new List<string>();  // note that not every question has choices
}
