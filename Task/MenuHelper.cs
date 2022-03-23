using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Task
{
    public class MenuHelper
    {
        public static bool FileChoice(FileInfo files)
        {
            Console.WriteLine("\n    1. Word occurrence.\n    2. Character occurrence\n    3. Lines Count\n    4. Word Count\n    5. Char Count\n    6. Most Frequently & longest word\n    7. comparison files\n\n    8: Exit\n            ");

            var fileOption = FunHelper.Read("Select Options:").ToLower();

            string[] isExit = new string[] { "8", "exit" };

            if (isExit.Contains(fileOption))
                return  true;

            if (!int.TryParse(fileOption, out int input))
            {
                Console.WriteLine("Only Integer Value \n");
                return false;
            }

            var txtfile = File.ReadAllText(files.FullName);
            string[] specialChar = new string[] { @"\", "|", "!", "#", "$", "%", "&", "/", "(", ")", "=", "?", "»", "«", "@", "£", "§", "€", "{", "}", ".", "-", "–", ";", "'", "<", ">", "_", "," };
            var escapeSequence = new char[] { '\r', '\n', '\t' };

            if (input == 1)
            {
                string word = FunHelper.Read("word to find.");
                Console.WriteLine($"Count : {txtfile.Split(' ').Where(w => w.ToLower() == word.ToLower()).Count()}\n");
            }
            else if (input == 2)
            {
                string @char = FunHelper.Read("char to find.");

                if (char.TryParse(@char, out char chartoFind))
                    Console.WriteLine($"Count : {txtfile.ToCharArray().Count(c => c == chartoFind)}\n");
                else
                    Console.WriteLine("Invalid char.\n");
            }
            else if (input == 3) { Console.WriteLine($"Count : {txtfile.Split('\r').Length}\n"); }
            else if (input == 4) { Console.WriteLine($"Count : {txtfile.Split(new char[] { ' ', '\r', '\n', '\t' }).Where(w => !string.IsNullOrEmpty(w) && !specialChar.Contains(w)).Count()}\n"); }
            else if (input == 5) { Console.WriteLine($"Count : {txtfile.ToArray().Where(c => !char.IsWhiteSpace(c)).Select(s => s).Count()}\nChar Count with Space: {txtfile.ToCharArray().Where(w => !escapeSequence.Contains(w)).Count()}\n"); }
            else if (input == 6)
            {
                var counts = Regex.Replace(txtfile, @"[^0-9a-zA-Z\n\r\t/ ]+", "").Split(new char[] { ' ', '\r', '\n', '\t', '/' })
                             .GroupBy(g => g)
                             .OrderByDescending(g => g.Count())
                             .Select(w => new { w.Key, Count = w.Count(), w.Key.Length })
                             .Where(w => !string.IsNullOrEmpty(w.Key)).ToList();

                var count = 1;

                Console.WriteLine($"Word and its frequency:\n");
                counts.Where(w => w.Count > 1).Take(5).ToList().ForEach(w => Console.WriteLine($"{count++}: {w.Key} => {w.Count} Times"));

                var longestWord = counts?.OrderByDescending(w => w.Length)?.FirstOrDefault();
                Console.WriteLine($"\nlongest word is [{longestWord?.Key}] with Length {longestWord?.Length}\n");
            }
            else if (input == 7)
            {
                var allFiles = MenuHelper.ListAllFile();

                string fileTwoID = FunHelper.Read("Select 2nd File to Compare");

                if (!int.TryParse(fileTwoID, out int fileIndex))
                {
                    Console.WriteLine("Invalid Selection\n");
                    return false;
                }

                if (fileIndex < 1 || fileIndex > 4)
                {
                    Console.WriteLine("Invalid Selection\n");
                    return false;
                }

                var fileInfoOne = allFiles.Where(w => w.FullName == files.FullName).FirstOrDefault();
                var fileInfoTwo = allFiles[fileIndex - 1];
                Console.WriteLine($"Selected File: {fileInfoTwo.Name}\n");

                Console.WriteLine(FunHelper.Compare(fileInfoOne.FullName, fileInfoTwo.FullName) ? "Files are Same" : "Files are Not Same\n");
            }

            return false;
        }

        public static (FileInfo, bool) AllFIles()
        {
            var files = ListAllFile();

            string fileId = FunHelper.Read("Select File").ToLower();
            string[] isExit = new string[] { "5", "exit" };

            if (isExit.Contains(fileId))
                return (null, true);


            if (int.TryParse(fileId, out int fileIndex))
                if (files.Count() >= fileIndex)
                    return (files[fileIndex - 1], false);

            Console.WriteLine("Invalid Selection\n");
            return (null, false);


        }

        public static List<FileInfo> ListAllFile()
        {
            var fileInfo = Directory.GetFiles($"{AppDomain.CurrentDomain.BaseDirectory}text", "*.txt", SearchOption.TopDirectoryOnly).Select((file, index) => (new FileInfo(file), index)).OrderBy(w => w.Item1.Name).Take(4).Select(s => { Console.WriteLine($"{s.index + 1} : {s.Item1.Name}"); return s.Item1; }).ToList();

            Console.WriteLine($"5 : Exit");

            return fileInfo;
        }
    }
}