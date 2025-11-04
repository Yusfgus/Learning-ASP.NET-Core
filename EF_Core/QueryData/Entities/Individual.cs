namespace EF_Core.QueryData.Entities;

public class Individual : Participant
{
    public string University { get; set; }
    public int YearOfGraduation { get; set; }
    public bool IsIntern { get; set; }

    public override string ToString()
    {
        return $"{Id}  | {LName}, {FName} | Graduated on ({YearOfGraduation}) From {University}" +
            $"({(IsIntern ? "Internship" : "")})";
    }
}
