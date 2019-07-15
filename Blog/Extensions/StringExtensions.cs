namespace Blog.Extensions
{
    public static class StringExtensions
    {

        public static string PrepareUrlTitle(this string str)
        {
            return str.Replace(" ", "-").Replace("?", "");
        }

    }
}