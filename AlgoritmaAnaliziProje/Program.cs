using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmaAnaliziProje
{
    public class Program
    {
        public static string password = "AJSDHJASKDdenemeQKJWEKaksdjaksldq";
        public static string result;
        public static bool isMatched = false;
        public static int charactersToTestLength = 0;
        public static long computedKeys = 0;
        public static char[] charactersToTest =
        {
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
        'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
        'u', 'v', 'w', 'x', 'y', 'z','A','B','C','D','E',
        'F','G','H','I','J','K','L','M','N','O','P','Q','R',
        'S','T','U','V','W','X','Y','Z','1','2','3','4','5',
        '6','7','8','9','0','!','$','#','@','-'
    };
        public static void startBruteForce(char[] txt)
        {
            var keyChars = txt;
            var indexOfLastChar = txt.Length - 1;
            createNewKey(0, keyChars, txt.Length, indexOfLastChar);
        }

        public static void createNewKey(int currentCharPosition, char[] keyChars, int keyLength, int indexOfLastChar)
        {
            var nextCharPosition = currentCharPosition + 1;
            for (int i = 0; i < charactersToTestLength; i++)
            {
                keyChars[currentCharPosition] = charactersToTest[i];

                if (currentCharPosition < indexOfLastChar)
                {
                    createNewKey(nextCharPosition, keyChars, keyLength, indexOfLastChar);
                }
                else
                    computedKeys++;

                if ((new String(keyChars)) == password)
                {
                    if (!isMatched)
                    {
                        isMatched = true;
                        result = new String(keyChars);
                    }
                    return;
                }
            }
        }

        private static int[] BuildBadCharTable(char[] needle)
        {
            int[] badShift = new int[256];
            for (int i = 0; i < 256; i++)
            {
                badShift[i] = needle.Length;
            }
            int last = needle.Length - 1;
            for (int i = 0; i < last; i++)
            {
                badShift[(int)needle[i]] = last - i;
            }
            return badShift;
        }

        public static int boyerMooreHorsepool(String pattern, String text)
        {
            char[] needle = pattern.ToCharArray();
            char[] haystack = text.ToCharArray();

            if (needle.Length > haystack.Length)
            {
                return -1;
            }
            int[] badShift = BuildBadCharTable(needle);
            int offset = 0;
            int scan = 0;
            int last = needle.Length - 1;
            int maxoffset = haystack.Length - needle.Length;
            while (offset <= maxoffset)
            {
                for (scan = last; (needle[scan] == haystack[scan + offset]); scan--)
                {
                    if (scan == 0)
                    { //Match found
                        return offset;
                    }
                }
                offset += badShift[(int)haystack[offset + last]];
            }
            return -1;
        }

        public static void Main(string[] args)
        {
            string txt = "deneme".ToLower();
            Console.WriteLine("Sifrenizi Giriniz:");
            string sifre = Console.ReadLine().ToLower();
            Console.WriteLine("Hangi algoritmayi secmek istersiniz:(1-Brute Force , 2-Boyer Moore)");
            string secim = Console.ReadLine();
            if (secim == "1")
            {
                var timeStarted = DateTime.Now;
                charactersToTestLength = charactersToTest.Length;
                Console.WriteLine("Brute Force Başladı - {0}", timeStarted.ToString());
                while (!isMatched)
                {
                    startBruteForce(txt.ToCharArray());
                }
                if (isMatched == true)
                {
                    Console.WriteLine("Sifre Eslesti. - {0}", DateTime.Now.ToString());
                    Console.WriteLine("Gecen Zaman: {0}s", DateTime.Now.Subtract(timeStarted).TotalSeconds);
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Sifre Eslesmedi. - {0}", DateTime.Now.ToString());
                    Console.WriteLine("Gecen Zaman: {0}s", DateTime.Now.Subtract(timeStarted).TotalSeconds);
                    Console.ReadLine();
                }
            }
            else if (secim == "2")
            {
                var timeStarted = DateTime.Now;
                Console.WriteLine("Boyer Moore Başladı - {0}", timeStarted.ToString());
                var result = boyerMooreHorsepool(sifre, txt);
                if(result == 0)
                {
                    Console.WriteLine("Sifre Eslesti. - {0}", DateTime.Now.ToString());
                    Console.WriteLine("Gecen Zaman: {0}s", DateTime.Now.Subtract(timeStarted).TotalSeconds);
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Sifre Eslesmedi. - {0}", DateTime.Now.ToString());
                    Console.WriteLine("Gecen Zaman: {0}s", DateTime.Now.Subtract(timeStarted).TotalSeconds);
                    Console.ReadLine();
                }
                
            }
            else
            {
                Console.WriteLine("Yanlıs secim yaptınız!!!");
                Console.ReadLine();
            }
        }
    }
}
