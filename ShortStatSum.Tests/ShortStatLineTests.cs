using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShortStatSum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortStatSum.Tests
{
    [TestClass()]
    public class ShortStatLineTests
    {
        [TestMethod()]
        public void TryParse_HappyCases()
        {
            string line = " 2 files changed, 9 insertions(+), 1 deletion(-)";

            bool success = ShortStatLine.TryParse(line, out ShortStatLine? result);

            success.Should().BeTrue();
            result.Should().NotBeNull();
            result!.NumFilesChanged.Should().Be(2);
            result!.NumInsertions.Should().Be(9);
            result!.NumDeletions.Should().Be(1);
        }

        [DataTestMethod()]
        [DataRow("")]
        [DataRow("myproject:")]
        [DataRow("mynamespace.myproject:")]
        [DataRow("github\\mynamespace.myproject:")]
        [DataRow("c:\\github\\mynamespace.myproject:")]
        [DataRow("warning: LF will be replaced by CRLF in blah/blah.")]
        [DataRow("The file will have its original line endings in your working directory")]
        public void TryParse_ShouldREportFalseForUnexpectedInputs(string line)
        {
            bool success = ShortStatLine.TryParse(line, out ShortStatLine? result);

            success.Should().BeFalse();
            result.Should().BeNull();
        }
    }
}