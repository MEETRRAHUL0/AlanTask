using System;
using System.IO;


namespace Task
{
    public class FunHelper
    {
        public static string Read(string value)
        {
            Console.WriteLine($"{value}");
            Console.WriteLine("");
            return Console.ReadLine();
        }


        public static bool Compare(string fileOneName, string fileTwoName)
        {
            byte[] f1 = File.ReadAllBytes(fileOneName);
            byte[] f2 = File.ReadAllBytes(fileTwoName);

            if (f1.Length == f2.Length)
            {
                for (int i = 0; i < f1.Length; i++)
                    if (f1[i] != f2[i])
                        return false;
            }
            else { return false; }

            return true;

        }
    }
}
