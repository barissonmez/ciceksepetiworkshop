using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CicekSepeti.Facility
{
    public class Facilities
    {
        public static string GeneratePassword(string password, string salt)
        {
            var sHashWithSalt = password + salt;

            var saltedHashBytes = Encoding.UTF8.GetBytes(sHashWithSalt);

            var algorithm = new System.Security.Cryptography.SHA256Managed();

            var hashedPassword = algorithm.ComputeHash(saltedHashBytes);

            return Convert.ToBase64String(hashedPassword);
        }

        public static string GenerateSalt(int length, bool isStrong)
        {
            var rnd = new Random();
            var seed = rnd.Next(1, int.MaxValue);
            const string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            const string specialCharacters = @"!#$%&'()*+,-./:;<=>?@[\]_";

            var chars = new char[length];
            var rd = new Random(seed);

            for (var i = 0; i < length; i++)
            {
                if (isStrong && i % rnd.Next(3, length) == 0)
                {
                    chars[i] = specialCharacters[rd.Next(0, specialCharacters.Length)];
                }
                else
                {
                    chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
                }
            }

            return new string(chars);
        }

        public static string GetDescription(Enum value)
        {
            var descriptionAttribute = (DescriptionAttribute)value.GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(false).FirstOrDefault(a => a is DescriptionAttribute);

            return descriptionAttribute != null ? descriptionAttribute.Description : value.ToString();
        }
        
    }
}
