using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ShortStatSum.Tests;

[TestClass()]
public class RepoNameLineTests
{
    [DataTestMethod()]
    [DataRow("myproject:")]
    [DataRow("mynamespace.myproject:")]
    [DataRow("github\\mynamespace.myproject:")]
    [DataRow("c:\\github\\mynamespace.myproject:")]
    public void TryParse_ShouldSucceedForValidInputs(string line)
    {
        bool success = RepoNameLine.TryParse(line, out RepoNameLine? result);

        success.Should().BeTrue();
        result.Should().NotBeNull();
    }

    [DataTestMethod()]
    [DataRow("")]
    [DataRow("myproject")]
    [DataRow("mynamespace.myproject")]
    [DataRow("github\\mynamespace.myproject")]
    [DataRow("c:\\github\\mynamespace.myproject")]
    [DataRow(" 2 files changed, 9 insertions(+), 1 deletion(-)")]
    [DataRow("warning: LF will be replaced by CRLF in blah/blah.")]
    [DataRow("The file will have its original line endings in your working directory")]
    public void TryParse_ShouldREportFalseForUnexpectedInputs(string line)
    {
        bool success = RepoNameLine.TryParse(line, out RepoNameLine? result);

        success.Should().BeFalse();
        result.Should().BeNull();
    }
}