using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TotalCommander
{
    static class Drive
    {
        public static string driveName = null;
        /// <summary>
        ///  Метод для вибору диску
        /// </summary>
        /// <returns>Повертає шлях до диску</returns>
        public static string ChangeDrive() 
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            DriveInfo[] drives = DriveInfo.GetDrives();
            int counterPos = 2;
            int counterMenu = 0;
            ConsoleKeyInfo keyInfo;
            //  Меню для вибору диску
            do
            {
                counterPos = 2;
                for (int i = 0; i < drives.Length; i++)
                {
                    Console.SetCursorPosition(6, counterPos++);
                    if (counterMenu == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    Console.WriteLine(drives[i].Name);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        {
                            if (counterMenu > 0)
                            {
                                counterMenu--;
                            }
                            else
                            {
                                counterMenu = drives.Length - 1;
                            }
                            break;
                        }
                    case ConsoleKey.DownArrow:
                        {
                            if (counterMenu < drives.Length - 1)
                            {
                                counterMenu++;
                            }
                            else
                            {
                                counterMenu = 0;
                            }
                            break;
                        }
                }
            } while (keyInfo.Key != ConsoleKey.Enter);
            Console.BackgroundColor = ConsoleColor.Black;

            Drive.driveName = drives[counterMenu].Name;

            DriveInfo dIn = new DriveInfo(Drive.driveName);

            //  Перевірка диску на корректність
            if (dIn.IsReady) 
            {
            return drives[counterMenu].Name;
            }
            return null;
        }
    }
}
