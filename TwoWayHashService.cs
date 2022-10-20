using System.Net;
using System.Text;

namespace _3ai.solutions.Hasher
{
    public sealed class TwoWayHashService
    {
        public int Config1 { get; init; } = 10;
        public int Config2 { get; init; } = 12;
        public int Config3 { get; init; } = 14;

        public string CreateSafeString(string str)
        {
            byte tmp;
            var chars = Encoding.UTF8.GetBytes(str);
            for (int i = 1; i <= (int)Math.Round(chars.Length / 2d); i += 2)
            {
                tmp = chars[i - 1];
                tmp += Convert.ToByte(i % Config1);
                chars[i - 1] = chars[^i];
                chars[i - 1] -= Convert.ToByte(i % Config2);
                chars[^i] = tmp;
            }
            return WebUtility.UrlEncode(Encoding.UTF8.GetString(chars));
        }

        public string ReadSafeString(string str)
        {
            byte tmp;
            var chars = Encoding.UTF8.GetBytes(WebUtility.UrlDecode(str));
            for (int i = 1; i <= (int)Math.Round(chars.Length / 2d); i += 2)
            {
                tmp = chars[i - 1];
                tmp += Convert.ToByte(i % Config2);
                chars[^i] -= Convert.ToByte(i % Config1);
                chars[i - 1] = chars[^i];
                chars[^i] = tmp;
            }
            return Encoding.UTF8.GetString(chars);
        }

        public string CreateSafeStringUnicode(string str)
        {
            var chars = Encoding.Unicode.GetBytes(str);
            for (int i = 0; i < chars.Length; i += 2)
                chars[i] -= Convert.ToByte(i % Config3);
            return WebUtility.UrlEncode(Encoding.Unicode.GetString(chars));
        }

        public string ReadSafeStringUnicode(string str)
        {
            var chars = Encoding.Unicode.GetBytes(WebUtility.UrlDecode(str));
            for (int i = 0; i < chars.Length; i += 2)
                chars[i] += Convert.ToByte(i % Config3);
            return Encoding.Unicode.GetString(chars);
        }
    }
}