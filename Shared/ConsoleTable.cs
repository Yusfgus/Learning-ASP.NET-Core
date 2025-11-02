using System.Text;

namespace Shared;

public class ConsoleTable
{
    public enum LineState
    {
        Top = 1,
        Middle = 2,
        Bottom = 3
    }


    private int TableWidth; // Adjust as needed
    private List<int> Columns_Widths = null!;

    public ConsoleTable(int tableWidth, int colNum)
    {
        TableWidth = tableWidth;
        int width = tableWidth / colNum;
        TableWidth = width * colNum;
        Columns_Widths = Enumerable.Repeat(width, colNum).ToList();
    }

    public ConsoleTable(List<int> colWidth)
    {
        Columns_Widths = colWidth;
        TableWidth = Columns_Widths.Sum() + 2 + Columns_Widths.Count();
    }

    public void PrintLine(LineState lineState = LineState.Middle)
    {
        char start, middles, end;

        if (lineState.Equals(LineState.Top))
        { start = '┌'; middles = '┬'; end = '┐'; }
        else if(lineState.Equals(LineState.Middle))
        { start = '├'; middles = '┼'; end = '┤'; }
        else
        { start = '└'; middles = '┴'; end = '┘'; }

        StringBuilder line = new StringBuilder(TableWidth);
        line.Append(start);
        foreach(int w in Columns_Widths)
        {
            // line.Append('─', w);
            line.Append('─', w);
            line.Append(middles);
        }
        line[line.Length - 1] = end;

        Console.WriteLine(line);
    }

    public void PrintRow(params string[] columns)
    {
        if (columns.Length > Columns_Widths.Count())
        {
            throw new ArgumentException("Columns size must be the same as widths size");
        }
        
        // │ ─ ┌ ┐ └ ┘ 
        // int columnWidth = (TableWidth - columns.Length - 1) / columns.Length; // -1 for the first '|'
        StringBuilder row = new StringBuilder("│");
        int i = 0;
        foreach (string column in columns)
        {
            row.Append(AlignCentre(column, Columns_Widths[i++]));
            row.Append('│');
            
        }
        Console.WriteLine(row);
    }

    public string AlignCentre(string text, int width)
    {
        text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;
        if (string.IsNullOrEmpty(text))
        {
            return new string(' ', width);
        }
        else
        {
            return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
        }
    }

    public static void ExampleUsage01()
    {
        ConsoleTable ct = new ConsoleTable(60, 3);
        ct.PrintLine(LineState.Top);
        ct.PrintRow("ID", "Name", "Age");
        ct.PrintLine();
        ct.PrintRow("1", "John Doe", "30");
        ct.PrintRow("2", "Jane Smith", "25");
        ct.PrintRow("3", "Peter Jones", "42");
        ct.PrintLine(LineState.Bottom);
    }
    public static void ExampleUsage02()
    {
        ConsoleTable ct = new ConsoleTable(new List<int>{4, 5, 5});
        ct.PrintLine(LineState.Top);
        ct.PrintRow("ID", "Name", "Age");
        ct.PrintLine();
        ct.PrintRow("1", "John Doe", "30");
        ct.PrintRow("2", "Jane Smith", "25");
        ct.PrintRow("3", "Peter Jones", "42");
        ct.PrintLine(LineState.Bottom);
    }
}