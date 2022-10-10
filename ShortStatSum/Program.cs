using System;

namespace ShortStatSum
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string? line;
            int numChangedRepos = 0;
            int totalNumFilesChanged = 0;
            int totalNumInsertions = 0;
            int totalNumDeletions = 0;

            if (!Console.IsInputRedirected)
            {
                Write("This tool calculates the total number of changes in the output of multiple");
                Write("    git diff --shortstat");
                Write("commands.");
                Write("It is designed to work with Mateo del Norte's meta tool.");
                Write("");
                Write("Examples:");
                Write("    meta git diff MyTag --shortstat | ShortStatSum.exe");
                Write("    will count the changes since the MyTag git tag.");
                Write("");
                Write("    meta git diff MyTag1..MyTag2 --shortstat | ShortStatSum.exe");
                Write("    will count the changes between MyTag1 and MyTag2.");
                Write("");
                Write("For details on git diff syntax, see ");
                Write("    git help diff");
                Environment.Exit(0);
            }

            while (true)
            {
                line = Console.ReadLine();
                if (line == null)
                    break;

                if (ShortStatLine.TryParse(line, out ShortStatLine? shortStatLine))
                {
                    numChangedRepos++;
                    totalNumFilesChanged += shortStatLine!.NumFilesChanged;
                    totalNumInsertions += shortStatLine!.NumInsertions;
                    totalNumDeletions += shortStatLine!.NumDeletions;
                }
            }

            Console.WriteLine();
            Write($" ({numChangedRepos} repos changed, {totalNumFilesChanged} files changed,{totalNumInsertions} insertions?(+), {totalNumDeletions} deletions?(-)");
        }

        private static void Write(string line) => Console.WriteLine(line);
    }
}