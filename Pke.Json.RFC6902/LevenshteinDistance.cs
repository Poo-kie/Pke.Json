using System;
using System.Collections.Generic;

namespace Pke.Json.RFC6902
{
    public static class LevenshteinDistance
    {
        /// <summary>
        /// Compute the distance between two strings.
        /// </summary>
        public static LevenshteinDistanceResult Execute<T>(T[] from, T[] to, Func<T, T, bool> equals) 
        {
            var x = to.Length;
            var y = from.Length;
            
            var result = new LevenshteinDistanceResult(new int[y + 1, x + 1],
                new LevenshteinDistanceMatrixCell[y + 1, x + 1]);

            // Step 1 : initialize 2D array
            for (int i = 0; i <= y; result.DistanceMatrix[i, 0] = i++) { }
            for (int j = 0; j <= x; result.DistanceMatrix[0, j] = j++) { }

            // Step 2: iterate rows
            for (int i = 1; i <= y; i++) {
                //Step 3: iterate columns
                for (int j = 1; j <= x; j++) {
                    // Step 4: set value of replacement cost to indicator function
                    // (0 if values are same, 1 otherwise)
                    int cost = equals(to[j - 1], from[i - 1]) ? 0 : 1;

                    // Step 5: get min of deletion, insertion and substitution
                    var del = result.DistanceMatrix[i - 1, j] + 1;
                    var ins = result.DistanceMatrix[i, j - 1] + 1;
                    var sub = result.DistanceMatrix[i - 1, j - 1] + cost;

                    result.DistanceMatrix[i, j] = Math.Min(Math.Min(del, ins), sub);

                    // Step 6: add backtrace pointers
                    if (result.DistanceMatrix[i,j] == del)
                    {
                        result.BacktraceMatrix[i,j] = new LevenshteinDistanceMatrixCell(1, LevenshteinDistanceMatrixDirections.Up);
                    } 
                    else if (result.DistanceMatrix[i,j] == ins)
                    {
                        result.BacktraceMatrix[i,j] = new LevenshteinDistanceMatrixCell(1, LevenshteinDistanceMatrixDirections.Left);
                    }
                    else 
                    {
                        result.BacktraceMatrix[i,j] = new LevenshteinDistanceMatrixCell(cost, LevenshteinDistanceMatrixDirections.UpLeft);
                    }
                }
            }

            return result;
        }

        public static IEnumerable<LevenshteinDistanceOp<T>> Backtrace<T>(LevenshteinDistanceResult result, T[] from, T[] to)
        {
            var x = result.BacktraceMatrix.GetLength(1) - 1;
            var y = result.BacktraceMatrix.GetLength(0) - 1;
            
            while(y != 0 && x != 0)
            {
                var pointer = result.BacktraceMatrix[y, x];

                if (pointer.Direction == LevenshteinDistanceMatrixDirections.Up)
                {
                    yield return new LevenshteinDistanceOp<T>(y - 1, from[y - 1], LevenshteinDistanceOps.Deletion);
                    --y;
                    continue;
                }
                
                if (pointer.Direction == LevenshteinDistanceMatrixDirections.Left)
                {
                    yield return new LevenshteinDistanceOp<T>(x - 1, to[x - 1], LevenshteinDistanceOps.Insertion);
                    --x;
                    continue;
                }

                if (pointer.Direction == LevenshteinDistanceMatrixDirections.UpLeft)
                {
                    // only a substituion if cost == 1
                    if (pointer.Cost == 1) yield return new LevenshteinDistanceOp<T>(x - 1, to[x - 1], LevenshteinDistanceOps.Substitution);
                    --y;
                    --x;
                    continue;
                }
            }
        }
    }
}