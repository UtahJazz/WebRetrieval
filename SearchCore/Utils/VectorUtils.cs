using System.Collections.Generic;
using System.Linq;

namespace SearchCore.Utils
{
    public static class VectorUtils
    {
        public static int MinimalDistance(int[][] vectors)
        {
            if (vectors.Length == 1)
            {
                return 0;
            }

            var pointers = new int[vectors.Length];
            var minimalDistance = CalculateDistance(pointers, vectors);

            while (!IsPointersAtEnd(pointers, vectors))
            {
                MoveMinimalPointer(pointers, vectors);
                var currentDistance = CalculateDistance(pointers, vectors);
                if (currentDistance < minimalDistance)
                {
                    minimalDistance = currentDistance;
                }
            }

            return minimalDistance;
        }

        private static void MoveMinimalPointer(int[] pointers, int[][] vectors)
        {
            pointers[FindMinialPointerId(pointers, vectors)]++;
        }

        private static int FindMinialPointerId(int[] pointers, int[][] vectors)
        {
            var minimalPointerValue = int.MaxValue;
            var minimalId = -1;

            for (var i = 0; i < pointers.Length; i++)
            {
                if (pointers[i] < (vectors[i].Length - 1) &&
                    vectors[i][pointers[i]] < minimalPointerValue)
                {
                    minimalPointerValue = vectors[i][pointers[i]];
                    minimalId = i;
                }
            }

            return minimalId;
        }

        private static int CalculateDistance(int[] pointers, int[][] vectors)
        {
            var distance = 0;
            var values = pointers
                .Select((pointer, i) => vectors[i][pointer])
                .OrderBy(x => x)
                .ToList();

            for (var i = 0; i < values.Count - 1; i++)
            {
                distance += values[i + 1] - values[i];
            }

            return distance;
        }

        private static bool IsPointersAtEnd(IList<int> pointers, IList<int[]> vectors)
        {
            var isEnd = true;

            for (var i = 0; i < pointers.Count; i++)
            {
                isEnd = isEnd && (vectors[i].Length - 1) == pointers[i];
            }

            return isEnd;
        }
    }
}
