using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using TotalCommander.Classes;

namespace TotalCommander
{
    class Program
    {
        //
       //  ІМПОРТ ДАННИХ З ДЛЛ
      //
        private const int MF_BYCOMMAND = 0x00000000;
        public const int SC_CLOSE = 0xF060;
        public const int SC_MINIMIZE = 0xF020;
        public const int SC_MAXIMIZE = 0xF030;
        public const int SC_SIZE = 0xF000;

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern bool EnableScrollBar(IntPtr hWnd, int wSBflags, int wArrows);
        //
       //  КІНЕЦЬ ІМПОРТУ ДАННИХ З ДЛЛ
      //

        static void Main()
        {
            //  Буква і в консолі
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            //  Налаштування для Window
            SettingsWindow();
            //  Задання картинки програми
            Console.BackgroundColor = ConsoleColor.Blue;
            ShowBackground();
            ShowBottomMenu();
            Console.BackgroundColor = ConsoleColor.Black;
            ShowButtonInBottomMenu();

            string path = null;
            while (path == null) 
            {
                //  Диск
               path = Drive.ChangeDrive();
            }

            string toDrive = Drive.driveName;
            while (true) 
            {
                string pattern = @".*\.w*";
                Regex regex = new Regex(pattern);
                    string exPath = path;
                if (!regex.IsMatch(path))
                {
                    DirectoryInfo dir = new DirectoryInfo(path);
                    try
                    {
                        //  Виведення усіх файлів з папки по вказаному шляху
                        path = DirectoryParams.ShowAllFiles(dir);
                    }
                    catch 
                    {
                        Console.SetCursorPosition(28, 3);
                        Console.WriteLine("Error!");
                        Console.ReadKey();
                        path = toDrive;
                    }
                    
                    
                    Params.Clear();
                }
                else
                {
                //  Вміст txt файлу
                    FileInfo file = new FileInfo(path);
                    if (file.Extension == ".txt")
                    {
                        using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                        {
                            using (StreamReader sr = new StreamReader(fs))
                            {
                                Params.ReadAndShowFile(sr);
                            }
                        }
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.SetCursorPosition(20, 4);
                        Console.WriteLine("Файл не зчитується");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Params.Clear();
                    }

                    //  Очищує ліву частину програми
                    Params.Clear();
                    path = file.DirectoryName;
                }

            }

        }

        /// <summary>
            ///  відображення фону
            /// </summary>
        static void ShowBackground() 
        {
            for (int y = 0; y < 23; y++)
            {
                for (int x = 0; x < 118; x++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.WriteLine(" ");
                }
            }
            Console.CursorVisible = false;
            for (int i = 0; i < 108; i++) 
            {
                    Console.SetCursorPosition(i+5, 1);
                Console.WriteLine("_");
            }

            for (int i = 2; i < 22; i++)
            {
                Console.SetCursorPosition(4, i);
                Console.WriteLine("|");

                Console.SetCursorPosition(113, i);
                Console.WriteLine("|");
                Console.SetCursorPosition(57, i);
                Console.WriteLine("|");
            }

            for (int i = 0; i < 110; i++) 
            {
                Console.SetCursorPosition(i+4, 22);
                Console.WriteLine("@");
            }
        }

        /// <summary>
        /// Відображення нижнього фону для меню
        /// </summary>
        static void ShowBottomMenu() 
        {
            for (int i = 0; i < 118; i++) 
            {
                Console.SetCursorPosition(i, 26);
                Console.WriteLine(" ");
            }
        }

        /// <summary>
        /// Відображення кнопок для нижнього меню
        /// </summary>
        static void ShowButtonInBottomMenu() 
        {
            Console.SetCursorPosition(2, 26);
            Console.WriteLine("1. Button");

            Console.SetCursorPosition(17, 26);
            Console.WriteLine("2. Button");

            Console.SetCursorPosition(32, 26);
            Console.WriteLine("3. Button");

            Console.SetCursorPosition(47, 26);
            Console.WriteLine("4. Button");

            Console.SetCursorPosition(62, 26);
            Console.WriteLine("5. Button");

            Console.SetCursorPosition(77, 26);
            Console.WriteLine("6. Button");

            Console.SetCursorPosition(92, 26);
            Console.WriteLine("7. Button");

            Console.SetCursorPosition(107, 26);
            Console.WriteLine("8. Button");

            Console.SetCursorPosition(0, 0);

        }

        /// <summary>
        /// Налаштування для Window
        /// </summary>
        static void SettingsWindow() 
        {
            IntPtr handle = GetConsoleWindow();
            IntPtr sysMenu = GetSystemMenu(handle, false);

            if (handle != IntPtr.Zero)
            {
                DeleteMenu(sysMenu, SC_MINIMIZE, MF_BYCOMMAND);
                DeleteMenu(sysMenu, SC_MAXIMIZE, MF_BYCOMMAND);
                DeleteMenu(sysMenu, SC_SIZE, MF_BYCOMMAND);
            }

           int saveBufferWidth = Console.BufferWidth;
            int saveBufferHeight = 28;
            Console.SetWindowSize(118, 27);
            Console.SetBufferSize(saveBufferWidth , saveBufferHeight);
        }
    }
}
