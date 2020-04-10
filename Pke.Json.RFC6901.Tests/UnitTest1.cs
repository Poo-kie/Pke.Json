using System;
using Xunit;

namespace Pke.Json.RFC6901.Tests
{
    public class ReferenceTokenTests
    {
        [Fact]
        public void Ctor_ShouldCreateReferenceTokenWithEscapedSequences_WhenKeyWithPathDelimiterIsProvided()
        {
            var rt = new ReferenceToken("a/b/c");

            Assert.Equal("a~1b~1c", rt.ToString());
        }

        [Fact]
        public void Ctor_ShouldCreateReferenceTokenWithEscapedSequences_WhenKeyWithReservedEscapeSequencesIsProvided()
        {
            var rt = new ReferenceToken("a/b/c~01/d~0/e~1/f");

            Assert.Equal("a~1b~1c~001~1d~00~1e~01~1f", rt.ToString());
        }

        [Fact]
        public void Ctor_ShouldCreateReferenceToken_WhenKeyAsEmptyStringIsProvided()
        {
            var rt = new ReferenceToken("");

            Assert.Equal("", rt.ToString());
        }

        [Fact]
        public void Ctor_ShouldThrowArgumentNullException_WhenNullKeyIsProvided()
        {
            Assert.Throws<ArgumentNullException>(() => new ReferenceToken(null));
        }
    }
}
