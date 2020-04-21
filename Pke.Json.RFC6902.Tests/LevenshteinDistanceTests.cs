using System.Linq;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Pke.Json.RFC6902.Tests
{
    public class LevenshteinDistanceTests
    {
        [Fact]
        public void Execute_ShouldReturnExpectedEditDistance_ForTwoStrings()
        {
            var from = "cat".ToCharArray();
            var to = "hat".ToCharArray();

            var result = LevenshteinDistance.Execute(from, to, CharEquals);

            Assert.Equal(1, result.DistanceMatrix[3, 3]);
        }

        [Fact]
        public void Execute_ShouldReturnExpectedEditDistance_ForTwoStringsWhenFromStringIsEmpty()
        {
            var from = "".ToCharArray();
            var to = "hat".ToCharArray();

            var result = LevenshteinDistance.Execute(from, to, CharEquals);

            Assert.Equal(3, result.DistanceMatrix[0, 3]);
        }

        [Fact]
        public void Execute_ShouldReturnExpectedEditDistance_ForTwoStringsWhenToStringIsEmpty()
        {
            var from = "cat".ToCharArray();
            var to = "".ToCharArray();

            var result = LevenshteinDistance.Execute(from, to, CharEquals);

            Assert.Equal(3, result.DistanceMatrix[3, 0]);
        }

        [Fact]
        public void Execute_ShouldReturnExpectedEditDistance_ForTwoStringsWhenBothStringsAreEmpty()
        {
            var from = "".ToCharArray();
            var to = "".ToCharArray();

            var result = LevenshteinDistance.Execute(from, to, CharEquals);

            Assert.Equal(0, result.DistanceMatrix[0, 0]);
        }

        [Fact]
        public void Backtrace_ShouldBeEmpty_ForTwoStringsWhenBothStringsAreEmpty()
        {
            var from = "".ToCharArray();
            var to = "".ToCharArray();

            var result = LevenshteinDistance.Execute(from, to, CharEquals);
            var backtrace = LevenshteinDistance.Backtrace(result, from, to).ToList();

            Assert.NotNull(backtrace);
            Assert.Empty(backtrace);
        }

        [Fact]
        public void Backtrace_ShouldReturnEmptyArray_ForTwoJsonArraysWhenBothArraysAreEmpty()
        {
            var from = (JToken.FromObject(new int[0]) as JArray).ToArray();
            var to = (JToken.FromObject(new int[0]) as JArray).ToArray();

            var result = LevenshteinDistance.Execute(from, to, JToken.DeepEquals);

            var backtrace = LevenshteinDistance.Backtrace(result, from, to).ToList();

            Assert.NotNull(backtrace);
            Assert.Empty(backtrace);
        }

        [Fact]
        public void Backtrace_ShouldReturnInsertion_ForTwoJsonArraysWhenToArrayHasExtraElement()
        {
            var from = (JToken.FromObject(new[] { 1, 2, 3, 4 }) as JArray).ToArray();
            var to = (JToken.FromObject(new[] { 1, 2, 3, 4, 5 }) as JArray).ToArray();

            var result = LevenshteinDistance.Execute(from, to, JToken.DeepEquals);
            var backtrace = LevenshteinDistance.Backtrace(result, from, to).ToList();

            Assert.NotNull(backtrace);
            Assert.Single(backtrace);
            Assert.Contains(backtrace, x => x.Index == 4 && x.Operation == LevenshteinDistanceOps.Insertion);
        }

        [Fact]
        public void Backtrace_ShouldReturnDeletion_ForTwoJsonArraysWhenFromArrayHasExtraElement()
        {
            var from = (JToken.FromObject(new[] { 1, 2, 3, 4, 5 }) as JArray).ToArray();
            var to = (JToken.FromObject(new[] { 1, 2, 3, 4 }) as JArray).ToArray();

            var result = LevenshteinDistance.Execute(from, to, JToken.DeepEquals);
            var backtrace = LevenshteinDistance.Backtrace(result, from, to).ToList();

            Assert.NotNull(backtrace);
            Assert.Single(backtrace);
            Assert.Contains(backtrace, x => x.Index == 4 && x.Operation == LevenshteinDistanceOps.Deletion);
        }

        [Fact]
        public void Backtrace_ShouldReturnExpectedResult_ForTwoJsonArraysWhenToArrayHasExtraElements()
        {                                       // 0  1    2   3   4   5   6   7
            var from = (JToken.FromObject(new[] { 15, 0, 101, 29, 41, 40, 10, 19 }) as JArray).ToArray();
            var to = (JToken.FromObject(new[] {    3, 4, 101, 29,  0,  2,  3,  4, 10, 18 }) as JArray).ToArray();

            var result = LevenshteinDistance.Execute(from, to, JToken.DeepEquals);
            var backtrace = LevenshteinDistance.Backtrace(result, from, to).ToList();

            Assert.NotNull(backtrace);
            Assert.Equal(7, backtrace.Count);
            Assert.Contains(backtrace, x => x.Index == 9 && x.Operation == LevenshteinDistanceOps.Substitution);
            Assert.Contains(backtrace, x => x.Index == 7 && x.Operation == LevenshteinDistanceOps.Insertion);
            Assert.Contains(backtrace, x => x.Index == 6 && x.Operation == LevenshteinDistanceOps.Insertion);
            Assert.Contains(backtrace, x => x.Index == 5 && x.Operation == LevenshteinDistanceOps.Substitution);
            Assert.Contains(backtrace, x => x.Index == 4 && x.Operation == LevenshteinDistanceOps.Substitution);
            Assert.Contains(backtrace, x => x.Index == 1 && x.Operation == LevenshteinDistanceOps.Substitution);
            Assert.Contains(backtrace, x => x.Index == 0 && x.Operation == LevenshteinDistanceOps.Substitution);
        }

        [Fact]
        public void Backtrace_ShouldReturnExpectedResult_ForTwoJsonArraysWhenToArrayHasFewerElements()
        {                                       // 0  1    2   3   4   5   6   7
            var from = (JToken.FromObject(new[] { 15, 0, 101, 29, 41, 40, 10, 19 }) as JArray).ToArray();
            var to = (JToken.FromObject(new[]    { 3, 4, 101, 29,  0,  2 }) as JArray).ToArray();

            var result = LevenshteinDistance.Execute(from, to, JToken.DeepEquals);
            var backtrace = LevenshteinDistance.Backtrace(result, from, to).ToList();

            Assert.NotNull(backtrace);
            Assert.Equal(6, backtrace.Count);
            Assert.Contains(backtrace, x => x.Index == 7 && x.Operation == LevenshteinDistanceOps.Deletion);
            Assert.Contains(backtrace, x => x.Index == 6 && x.Operation == LevenshteinDistanceOps.Deletion);
            Assert.Contains(backtrace, x => x.Index == 5 && x.Operation == LevenshteinDistanceOps.Substitution);
            Assert.Contains(backtrace, x => x.Index == 4 && x.Operation == LevenshteinDistanceOps.Substitution);
            Assert.Contains(backtrace, x => x.Index == 1 && x.Operation == LevenshteinDistanceOps.Substitution);
            Assert.Contains(backtrace, x => x.Index == 0 && x.Operation == LevenshteinDistanceOps.Substitution);
        }

        public static bool CharEquals(char c1, char c2) => Equals(c1, c2);
    }
}