using System.Security.Cryptography;

namespace _3ai.solutions.Hasher
{
    public class RandomHashService
    {
        public char[] ValidCharacters { get; init; } = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
        public int HashSize { get; init; } = 5;

        public double Uniqueness()
        {
            return Math.Pow(ValidCharacters.Length, HashSize);
        }

        public string GetNewHash()
        {
            char[] chars = new char[HashSize];
            for (int i = 0; i < HashSize; i++)
                chars[i] = ValidCharacters[RandomNumberGenerator.GetInt32(ValidCharacters.Length)];
            return new string(chars);
        }
    }
}