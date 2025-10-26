using Shared;

namespace Linq;

public class Projection
{
    public static void Select()
    {
        Utils.printTitle(title: "Projection ( Select )", color: ConsoleColor.Blue, width: 70);

        List<string> words = new() { "i", "love", "asp.net", "core" };

        // IEnumerable<string> result01 = words.Select(x => {return x;});
        IEnumerable<string> result01 = words.Select(x => x.ToUpper());
        // IEnumerable<string> result01 = from w in words
        //                                select w.ToUpper();
        result01.Print("Return Same Type");

        // ==============================================================================================

        IEnumerable<int> result02 = words.Select(x => x.Length);
        // IEnumerable<int> result02 = from w in words
        //                                select w.Length;
        result02.Print("Return Different Type");
    }

    public static void SelectMany()
    {
        Utils.printTitle(title: "Projection ( SelectMany )", color: ConsoleColor.Blue, width: 70);

        string[] sentences = { "I love Asp.Net", "I like sql server", "I love C#" };

        IEnumerable<char> result01 = sentences.SelectMany(x => x);
        result01.Print("Split sentences to characters");

        IEnumerable<string> result02 = sentences.SelectMany(x => x.Split(' ') /*return array*/);
        result02.Print("Split sentences to words");

        // ==============================================================================================

        List<List<string>> skills = new()
        {
            new() {"skill 1", "skill 3"},
            new() {"skill 1", "skill 2"},
            new() {"skill 2", "skill 3", "skill 4"},
            new() {"skill 1", "skill 4", "skill 2"},
        };

        IEnumerable<string> result03 = skills.SelectMany(x => x);
        result03.Print("Split skills lists to skills");

        IEnumerable<string> result04 = skills.SelectMany(x => x).Distinct();
        // var result04 = (from skill_list in skills
        //                 from  skill in skill_list
        //                 select skill
        //                 ).Distinct();
        result04.Print("Split skills lists to distinct skills");

    }

    public static void Zip()
    {
        Utils.printTitle(title: "Projection ( Zip )", color: ConsoleColor.Blue, width: 70);

        string[] colorName = { "Red", "Green", "Blue", "extra" };
        string[] colorHex = { "FF0000", "00FF00", "0000FF" };

        IEnumerable<string> name_hex = colorName.Zip(colorHex, (name, hex) => $"{name} ({hex})");
        // IEnumerable<string> name_hex = from pair in colorName.Zip(colorHex)
        //                                select $"{pair.First} ({pair.Second})";
        name_hex.Print("Colors: Name (Hex)");

        IEnumerable<string> hex_name = colorHex.Zip(colorName, (hex, name) => $"{hex} ({name})");
        hex_name.Print("Colors: Hex (Name)");
    }
}