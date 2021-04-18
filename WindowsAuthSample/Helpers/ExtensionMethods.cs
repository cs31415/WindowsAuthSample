using System.Linq;

namespace WindowsAuthSample.Helpers
{
    public static class ExtensionMethods
    {
        public static string TakeFirst(this string text, int number)
        {
            if (text == null)
                return null;

            return new string(text.Take(number).ToArray());
        }
    }
}