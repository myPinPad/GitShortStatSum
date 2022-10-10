using System.Text.RegularExpressions;

namespace ShortStatSum;

/// <summary>
/// Represents a line from a 'git diff --shortstat' output that contains a repo name.
/// </summary>
public class RepoNameLine
{
    public const string RegExString = "(?<reponame>.*):$";
    private static Regex? RegEx;

    /// <summary>Contains the sting of text that was used to construct this line.</summary>
    public string Line { get; set; }
    public string RepoName { get; set; }

    public RepoNameLine(
        string line,
        string repoName)
    {
        Line = line;
        RepoName = repoName;
    }

    public static bool TryParse(string sourceLine, out RepoNameLine? repoNameLine)
    {
        repoNameLine = null;

        if (string.IsNullOrWhiteSpace(sourceLine))
            return false;

        if (RegEx == null)
            RegEx = new Regex(RegExString);

        var matchResult = RegEx.Match(sourceLine);
        if (!matchResult.Success)
            return false;

        repoNameLine = new RepoNameLine(
            sourceLine,
            matchResult.Groups["reponame"].Value);
        return true;
    }
}
