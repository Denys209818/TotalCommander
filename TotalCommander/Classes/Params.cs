using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TotalCommander.Classes
{
    static class Params
    {
        /// <summary>
        /// Очистка лівої частини екрану
        /// </summary>
        public static void Clear() 
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            for (int y = 0; y < 20; y++)
            {  
                for (int i = 0; i < (98/2)+3; i++) 
                {
                    Console.SetCursorPosition(i+5,y+2);
                    Console.WriteLine(" ");
                }

                for (int i = (98 / 2)+4; i < 108; i++)
                {
                    Console.SetCursorPosition(i + 5, y + 2);
                    Console.WriteLine(" ");
                }
            }
            Console.BackgroundColor = ConsoleColor.Black;

        }

        /// <summary>
        /// Очистка правої частини екрану
        /// </summary>
        public static void ClearRight()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            for (int y = 0; y < 20; y++)
            {
                for (int i = (98 / 2) + 4; i < 108; i++)
                {
                    Console.SetCursorPosition(i + 5, y + 2);
                    Console.WriteLine(" ");
                }

                for (int i = (98 / 2) + 4; i < 108; i++)
                {
                    Console.SetCursorPosition(i + 5, y + 2);
                    Console.WriteLine(" ");
                }
            }
            Console.BackgroundColor = ConsoleColor.Black;

        }

        /// <summary>
        /// Видалення межі між екранами
        /// </summary>
        public static void ClearMiddleLine()
        {   Console.BackgroundColor = ConsoleColor.Blue;
        
            for (int y = 0; y < 20; y++)
            {
                    Console.SetCursorPosition((98 / 2) + 8, y + 2);
                    Console.WriteLine(" ");
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }

        /// <summary>
        /// Показати межу між екранами
        /// </summary>
        public static void ShowMiddleLine()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            for (int y = 0; y < 20; y++)
            {
                Console.SetCursorPosition((98 / 2) + 8, y + 2);
                Console.WriteLine("|");
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }

        /// <summary>
        /// Зчитує файл і показує його по лівій частині екрану
        /// </summary>
        public static void ReadAndShowFile(StreamReader sr) 
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            int counter = 2;
            Params.Clear();
            for (int y = 0; y < 10; y++) 
            {
                if (!sr.EndOfStream) 
                {
                string str = sr.ReadLine();
                    Console.BackgroundColor = ConsoleColor.Blue;
                    if (str.Length < 100) 
                    {
           
                        for (int i = 0; i < str.Length; i++)
                        {
                            if (i % 50 == 0)
                            {
                                Console.SetCursorPosition(6, counter++);
                            }
                            Console.Write(str[i]);
                        }
                    }
                }
                else
                {
                    Console.ReadKey();
                    return;
                }
            }
                
            

            ConsoleKeyInfo keyInfo = Console.ReadKey();
            switch (keyInfo.Key) 
            {
                case ConsoleKey.DownArrow: 
                    {
                        ReadAndShowFile(sr);
                        break; 
                    }

            case ConsoleKey.Enter: { return; }
            }

            Console.BackgroundColor = ConsoleColor.Black;

        }

        /// <summary>
        /// Зчитує файл та показує його по правій частині екрану
        /// </summary>
        public static void ReadAndShowFileRight(StreamReader sr)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            int counter = 2;
            for (int y = 0; y < 10; y++)
            {
                if (!sr.EndOfStream)
                {
                    string str = sr.ReadLine();
                    Console.BackgroundColor = ConsoleColor.Blue;
                    if (str.Length < 100)
                    {
                        for (int i = 0; i < str.Length; i++)
                        {
                            if (i % 50 == 0)
                            {
                                Console.SetCursorPosition((98 / 2) + 9, counter++);
                            }
                            Console.Write(str[i]);
                        }
                    }
                }
            }

            Console.BackgroundColor = ConsoleColor.Black;

        }
    }
}
