using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSnippetsLibrary.Array
{
    internal class ArrayIteration
    {
        public static class ArraysInversion
        {
            public static int[] InvertValues(int[] input)
            {
                //Select projects each element in the array to a new value.
                return [.. input.Select(n => -n)];
            }

            public static int[] InvertValuesUsingExpressionBodiedSyntax(int[] input) =>
                 input == null ? [] : [.. input.Select(x => -x)];
        }
    }
}
