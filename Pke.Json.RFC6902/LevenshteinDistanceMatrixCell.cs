namespace Pke.Json.RFC6902
{

    public class LevenshteinDistanceMatrixCell
    {
        public LevenshteinDistanceMatrixCell(int cost, LevenshteinDistanceMatrixDirections direction)
        {
            Cost = cost;
            Direction = direction;
        }

        public int Cost { get; }

        public LevenshteinDistanceMatrixDirections Direction { get; }
    }
}