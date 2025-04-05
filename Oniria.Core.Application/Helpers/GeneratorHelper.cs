using Oniria.Core.Domain.Constants;

namespace Oniria.Core.Application.Helpers
{
    public static class GeneratorHelper
    {
        private static Random random = new Random();

        public static string RandomPassword(int length)
        {
            string password = (
                GetRandomCharacter(CharactersConstants.UPPERCASE) +
                GetRandomCharacter(CharactersConstants.LOWERCASE) +
                GetRandomCharacter(CharactersConstants.NUMBERS) +
                GetRandomCharacter(CharactersConstants.SPECIAL)
            )
            .ToString() +
            RandomLetters(length, CharactersConstants.ALL);

            return password;
        }

        public static string RandomLetters(int length, string characters)
        {
            string stringGenerated = string.Empty;

            for (int i = 0; i < length; i++) {
                stringGenerated += GetRandomCharacter(characters);
            }

            return stringGenerated;
        }

        public static string GuidString()
        {
            return Guid.NewGuid().ToString();
        }

        private static char GetRandomCharacter(string characters)
        {
            int index = random.Next(characters.Length);
            return characters[index];
        }
    }
}
