using System.Text;

namespace CodeSnippetsLibrary.Strings
{
    internal class StringBuilderExample
    {
        //replace every "digit" < 5 with "0" and every digit >= 5 with "1"
        public static string FakeBin(string x)
        {
            StringBuilder builder = new();

            //x is a string, which in C# is an enumerable sequence of char values.
            //So when you loop through it with foreach, t is a character — not a string.
            //Characters (chars) in C# are stored internally as Unicode numeric values (compatible with ASCII for digits and letters).
            //So '0' = 48, '1' = 49, ..., '9' = 57.
            //That means you can compare chars numerically:
            foreach (char t in x)
            {
                builder.Append(t >= '5' ? '1' : '0');
            }

            return builder.ToString();
        }

        //moves first letter of word to end with 'ay' after eg "igPay atinlay siay oolcay"
        public static string PigIt(string str)
        {
            return string.Join(" ", str.Split(" ").Select(w => char.IsPunctuation(w[0]) ? w : $"{w[1..]}{w[0]}ay"));
        }

    }
}
