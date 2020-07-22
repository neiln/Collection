using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.Text.RegularExpressions;

namespace RegexPatTest
{
    class Program
    {
        private const string Pattern = @"^\w+[_]\d{8}[_]+\w+(.bak)$";
        static void Main(string[] args)
        {

            string input = @"Elogic_AAdAMS_20200407_PRODUCTION.Bak";
            Console.WriteLine(IsMatch(input) ? $"{input} - Match" : $"{input} - Not a match");


            input = @"Elogic_AAdAMS_20200407_PRODUCTION.Bak";
            Console.WriteLine(IsMatch(input) ? $"{input} - Match" : $"{input} - Not a match");

            input = @"Elogic_AAdAMS_20200407_PRODUCTION.Baky";
            Console.WriteLine(IsMatch(input) ? $"{input} - Match" : $"{input} - Not a match");

            input = @"Elogic_AAdAMS_202200407_PRODUCTION.bak";
            Console.WriteLine(IsMatch(input) ? $"{input} - Match" : $"{input} - Not a match");
            
            input = @"20200407_PRODUCTION_OKAY.BAK";
            Console.WriteLine(IsMatch(input) ? $"{input} - Match" : $"{input} - Not a match");

            input = @"AAdAMS_20200417_PRODUCTION_OKAY.BAK";
            Console.WriteLine(IsMatch(input) ? $"{input} - Match" : $"{input} - Not a match");

            //get the date from the value
            string value = Regex.Match(input, @"[_]\d{8}[_]").Value;
            if (!string.IsNullOrEmpty(value))
            {
                value = value.Replace("_", "");
                if (DateTime.TryParseExact(value, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
                {
                    Console.WriteLine(result.ToLongDateString());
                }
                else
                {
                    Console.WriteLine($"Invalid Date format {value} (yyyyMMdd)");
                }
            }

        }

        static bool IsMatch(string input)
        {
            return Regex.IsMatch(input, Pattern, RegexOptions.IgnoreCase);
        }

        static bool ValidateFilenameFormat(string stringFormat)
        {
            string pattern = @"^\w+[_]\d{8}[_]+\w+(.bak)$";

            //test the pattern
            if (Regex.IsMatch(stringFormat, pattern, RegexOptions.IgnoreCase))
            {
                //verify the date format
                //get the date from the value
                string value = Regex.Match(stringFormat, @"[_]\d{8}[_]").Value;
                if (!string.IsNullOrEmpty(value))
                {
                    value = value.Replace("_", "");
                    if (DateTime.TryParseExact(value, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
