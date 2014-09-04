using System.Collections.Generic;
using System.Linq;

namespace SearchCore.UserQueryProcesser
{
    public static class BinaryQueryOperators
    {
        public static int[] And(int[] firstOperand, int[] secondOperand)
        {
            int leftIterator = 0, rightIterator = 0;
            var resultFiles = new List<int>();
            while (leftIterator < firstOperand.Length && rightIterator < secondOperand.Length)
            {
                var leftId = firstOperand[leftIterator];
                var rightId = secondOperand[rightIterator];
                if (leftId == rightId)
                {
                    resultFiles.Add(leftId);

                    leftIterator++;
                    rightIterator++;
                }
                else if (leftId < rightId)
                {
                    leftIterator++;
                }
                else if (rightId < leftId)
                {
                    rightIterator++;
                }
            }

            return resultFiles.ToArray();
        }

        public static int[] Or(int[] firstOperand, int[] secondOperand)
        {
            var result = firstOperand.ToList();
            result.AddRange(secondOperand);
            return result.Distinct().OrderBy(x => x).ToArray();
        }
    }
}
