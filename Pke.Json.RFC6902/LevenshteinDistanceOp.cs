namespace Pke.Json.RFC6902
{
    public class LevenshteinDistanceOp<T>
    {
        public LevenshteinDistanceOp(int idx, T value, LevenshteinDistanceOps op)
        {
            Index = idx;
            Operation = op;
            Value = value;
        }

        public int Index { get; }

        public T Value { get; }

        public LevenshteinDistanceOps Operation { get; } 
    }
}