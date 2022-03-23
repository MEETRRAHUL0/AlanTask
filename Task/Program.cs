using System;
using System.IO;

namespace Task
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            bool exitMainMenu;
            do
            {
                var res = MenuHelper.AllFIles();
                exitMainMenu = res.Item2;

                if (res.Item2 || res.Item1 == null) continue;

                bool exitSubMenu;
                do
                {
                    exitSubMenu = MenuHelper.FileChoice(res.Item1);
                } while (!exitSubMenu);

            } while (!exitMainMenu);
        }
    }
}