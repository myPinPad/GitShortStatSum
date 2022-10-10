using System.Text.RegularExpressions;

namespace ShortStatSum;

/// <summary>
/// Represents a line from a 'git diff --shortstat' output that contains a repo name.
/// </summary>
public class ShortStatLine
{
    // Example: " 5 files changed, 147 insertions(+), 106 deletions(-)"
    public const string RegExString =
        @" (?<numfileschanged>\d+) files changed," +
        @" (?<numinsertions>\d+) insertions?\(\+\)," +
        @" (?<numdeletions>\d+) deletions?\(-\)";
    private static Regex? RegEx;

    /// <summary>Contains the sting of text that was used to construct this line.</summary>
    public string Line { get; set; }
    public int NumFilesChanged { get; set; }
    public int NumInsertions { get; set; }
    public int NumDeletions { get; set; }

    public ShortStatLine(
        string line,
        int numFilesChanged,
        int numInsertions,
        int numDeletions)
    {
        Line = line;
        NumFilesChanged = numFilesChanged;
        NumInsertions = numInsertions;
        NumDeletions = numDeletions;
    }

    public static bool TryParse(string sourceLine, out ShortStatLine? repoNameLine)
    {
        repoNameLine = null;

        if (string.IsNullOrWhiteSpace(sourceLine))
            return false;

        if (RegEx == null)
            RegEx = new Regex(RegExString);

        var matchResult = RegEx.Match(sourceLine);
        if (!matchResult.Success)
            return false;

        repoNameLine = new ShortStatLine(
            line: sourceLine,
            numFilesChanged: SafeParseInt(matchResult.Groups["numfileschanged"].Value),
            numInsertions: SafeParseInt(matchResult.Groups["numinsertions"].Value),
            numDeletions: SafeParseInt(matchResult.Groups["numdeletions"].Value));
        return true;
    }

    private static int SafeParseInt(string intString)
    {
        var success = int.TryParse(intString, out int result);
        return success ? result : 0;
    }
}
