using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Models
{
    public class GenerateKey
    {
        public int RandNum(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        public string RandStr(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
            {
                return builder.ToString().ToLower();
            }
            return builder.ToString();
        }

        public string RandKey()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandStr(4, true));
            builder.Append(RandNum(1000, 9999));
            builder.Append(RandStr(2, false));
            return builder.ToString();
        }
    }
}
