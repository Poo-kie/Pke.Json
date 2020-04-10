namespace Pke.Json.RFC6902
{
    public class LevenshteinDistanceResult
    {
        public LevenshteinDistanceResult(int[,] distanceMatrix, LevenshteinDistanceMatrixCell[,] backtraceMatrix)
        {
            DistanceMatrix = distanceMatrix;
            BacktraceMatrix = backtraceMatrix;
        }

        public int[,] DistanceMatrix { get; }

        public LevenshteinDistanceMatrixCell[,] BacktraceMatrix { get; } 
    }
}