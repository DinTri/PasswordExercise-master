using System;
using System.Collections.Generic;

namespace PasswordExercise.Contracts
{
    public class PasswordGenerator : IPasswordGenerator
    {
        public string GeneratePassword(PasswordRequirements requirements)
        {
            string[] randomCharacters = new[]
            {
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ", // uppercase 
                "abcdefghijkmnopqrstuvwxyz", // lowercase
                "0123456789", // digits
                "!@#$£%^&*()<>{}-=" // non-alphanumeric
            };
            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            try
            {
                if (requirements.MaxLength == 0)
                {
                    requirements.MaxLength = 8;
                }

                if (requirements.MinLength == 0)
                {
                    requirements.MinLength = 5;
                }

                if (requirements.MinUpperAlphaChars > 0)
                {
                    PwRequirements(requirements.MinUpperAlphaChars, chars, rand, randomCharacters, 0);
                }

                if (requirements.MinLowerAlphaChars > 0)
                {
                    PwRequirements(requirements.MinLowerAlphaChars, chars, rand, randomCharacters, 1);
                }

                if (requirements.MinNumericChars > 0)
                {
                    PwRequirements(requirements.MinNumericChars, chars, rand, randomCharacters, 2);
                }

                if (requirements.MinSpecialChars > 0)
                {
                    PwRequirements(requirements.MinSpecialChars, chars, rand, randomCharacters, 3);
                }

                for (int i = chars.Count; i < requirements.MaxLength; i++)
                {
                    string generatedChars = randomCharacters[rand.Next(0, randomCharacters.Length)];
                    chars.Insert(rand.Next(0, chars.Count),
                        generatedChars[rand.Next(0, generatedChars.Length)]);
                }

                return new string(chars.ToArray());

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static void PwRequirements(int requirements, List<char> chars, Random rand, string[] randomCharacters, int k)
        {
            for (var i = 0; i < requirements; i++)
            {
                chars.Insert(rand.Next(0, i),
                    randomCharacters[k][rand.Next(i, randomCharacters[k].Length)]);
            }
        }
    }
}

