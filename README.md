# GitShortStatSum

This tool calculates the total number of changes in the output of multiple

    git diff --shortstat

commands.
It is designed to work with Mateo del Norte's meta tool.

Examples:

    meta git diff MyTag --shortstat | ShortStatSum.exe
    will count the changes since the MyTag git tag.

    meta git diff MyTag1..MyTag2 --shortstat | ShortStatSum.exe
    will count the changes between MyTag1 and MyTag2.

For details on git diff syntax, see

    git help diff