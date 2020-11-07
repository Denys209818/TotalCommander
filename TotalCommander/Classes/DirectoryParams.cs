using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TotalCommander.Classes
{
    static class DirectoryParams
    {
        /// <summary>
        /// Відображає файли по лівій частині екрану
        /// </summary>
        /// <returns>Повертає шлях</returns>
        public static string ShowAllFiles(DirectoryInfo dir, int counter = 0)
        {
            int num;
            //  Запис у масив усіх данних про файли та папки
            IEnumerable<string> nameTake;
            Console.BackgroundColor = ConsoleColor.Blue;
            DirectoryInfo[] directories = dir.GetDirectories();
            FileInfo[] files = dir.GetFiles();

            if (counter < 0)
            {
                counter = 0;
            }

            int skipped = 0;
            int CounterX = 6;
            int CounterY = 2;

            ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
            //  Відображення меню для вибору папки чи файлу
            do
            {
                int c = skipped;
                bool flag = false;
                int a = skipped;
                CounterX = 6;
                CounterY = 2;
                string[] names = new string[directories.Length + files.Length + 1];
                for (int i = 0; i < directories.Length; i++)
                {
                    names[i] = directories[i].Name;
                }
                for (int i = 0; i < files.Length; i++)
                {
                    names[i + directories.Length] = files[i].Name;
                }
                names[directories.Length + files.Length] = "..";
                nameTake = names;
                if (nameTake.Count() > 20)
                {
                    foreach (var str in nameTake.Skip(skipped).Take(20))
                    {
                        if (counter == a++)
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                        Console.SetCursorPosition(CounterX, CounterY++);
                        Console.WriteLine(str);
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                }
                else
                {
                    foreach (var str in nameTake)
                    {
                        if (counter == a++)
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                        Console.SetCursorPosition(CounterX, CounterY++);
                        if (str.Length < 48)
                        {
                            Console.WriteLine(str);
                        }
                        else
                        {
                            for (int i = 0; i < 48; i++)
                            {
                                Console.Write(str[i]);
                            }
                            Console.Write("..");
                        }
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                }

                string pattern = @".*\.txt";
                Params.ClearRight();
                if (!names[counter].Contains(".."))
                {
                    if (Regex.IsMatch(names[counter], pattern))
                    {
                        if (File.Exists(dir.FullName + @"\" + names[counter]))
                        {
                            using (FileStream fs = new FileStream(dir.FullName + @"\" + names[counter], FileMode.Open, FileAccess.Read))
                            {
                                using (StreamReader sr = new StreamReader(fs))
                                {
                                    try
                                    {
                                        Params.ReadAndShowFileRight(sr);
                                    }
                                    catch { }
                                }
                            }
                        }
                    } 
                    else if (!names[counter].Contains("."))
                    { 
                    if (Directory.Exists(dir.FullName + @"\" + names[counter]))
                    {
                        DirectoryInfo dInfo = new DirectoryInfo(dir.FullName + @"\" + names[counter]);
                        try
                        {
                            num = ShowAllFilesRight(dInfo);
                                if (num > 0)
                                {
                                    counter++;
                                    flag = true;
                                }
                                else if (num < 0 && num != -5)
                                {
                                    counter--;
                                    if (counter < 0)
                                    {
                                        counter = directories.Length + files.Length;
                                        if (counter > 20) 
                                        {
                                            skipped = counter - 20 + 1;
                                            Params.Clear();
                                        }
                                    }
                                    flag = true;
                                    
                                }
                                else if (num == 0)
                                {
                                    break;
                                }
                            }
                        catch { }
                
                    }
                    }
                }
                    Console.BackgroundColor = ConsoleColor.Blue;

                if (!flag) 
                {
                    keyInfo = Console.ReadKey();


                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.UpArrow:
                            {
                                if (counter > 0)
                                {
                                counter--;
                                    if (skipped > 0)
                                    {
                                        Params.Clear();
                                        Console.BackgroundColor = ConsoleColor.Blue;
                                        skipped--;
                                    }
                                }
                                else
                                {
                                    Params.Clear();
                                    Console.BackgroundColor = ConsoleColor.Blue;
                                    counter = directories.Length + files.Length;
                                    if (counter > 20)
                                    {
                                        skipped = counter + 1 - 20;
                                    }
                                }

                               

                                break;
                            }
                        case ConsoleKey.DownArrow:
                            {
                                if (counter < directories.Length + files.Length)
                                {
                                    counter++;
                                    if (counter >= 20 + skipped)
                                    {
                                        Params.Clear();
                                        Console.BackgroundColor = ConsoleColor.Blue;
                                        skipped++;
                                    }
                                }
                                else
                                {
                                    Params.Clear();
                                    Console.BackgroundColor = ConsoleColor.Blue;
                                    skipped = 0;
                                    counter = 0;
                                }
                                break;
                            }
                    }

                    if (keyInfo.Key != ConsoleKey.UpArrow && keyInfo.Key != ConsoleKey.DownArrow)
                    {
                        break;
                    }
                }
                if (c == skipped && skipped > 0)
                {
                    skipped--;
                }

                }
                while (keyInfo.Key != ConsoleKey.Enter) ;
            //  Кінець меню
                Console.BackgroundColor = ConsoleColor.Black;

                if (counter < directories.Length)
                {
                    return directories[counter].FullName;
                }
                else
                {
                    if (counter == directories.Length + files.Length)
                    {

                        if (dir.FullName == Drive.driveName)
                        {
                            return dir.FullName;
                        }
                        else
                        {
                            return dir.Parent.FullName;
                        }
                    }
                    counter -= directories.Length;
                    return files[counter].FullName;
                }

            
        }

        /// <summary>
        /// Відображає файли по правій частині екрану
        /// </summary>
        /// <returns>Повертає шлях</returns>
        public static int ShowAllFilesRight(DirectoryInfo dir, int skipp = 0)
        { 
            DirectoryInfo[] directories = dir.GetDirectories();
            FileInfo[] files = dir.GetFiles();

            int skipped = skipp;
           
          

                string[] names = new string[directories.Length + files.Length + 1];
                for (int i = 0; i < directories.Length; i++)
                {
                    names[i] = directories[i].Name;
                }
                for (int i = 0; i < files.Length; i++)
                {
                    names[i + directories.Length] = files[i].Name;
                }

                IEnumerable<string> nameTake = names;
            //  Меню
            //  Якщо в масиві більше 20 елементів 
            if (nameTake.Count() > 20)
            {
                while (true) 
                {
                    Params.ClearRight();
                    Console.BackgroundColor = ConsoleColor.Blue;
                    int CounterX = (98 / 2) + 9;
                    int CounterY = 2;
                    skipped = skipp;
                foreach (var str in nameTake.Skip(skipped).Take(20))
                {
                    Console.SetCursorPosition(CounterX, CounterY++);

                    if (str != null)
                    {
                        if (str.Length < 48)
                        {
                            Console.WriteLine(str);
                        }
                        else
                        {
                            for (int i = 0; i < 48; i++)
                            {
                                Console.Write(str[i]);
                            }
                            Console.Write("..");
                        }
                    }
                    else
                    {
                        CounterY--;
                    }

                    Console.ForegroundColor = ConsoleColor.Gray;
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.S:
                        {
                            if (20 + skipp < nameTake.Count() - 1)
                            {
                                skipp++;
                            }
                            break;
                        }
                    case ConsoleKey.W:
                        {
                            if (skipp > 0)
                            {
                                skipp--;
                            }
                            break;
                        }
                    case ConsoleKey.UpArrow:
                        {
                            return -1;
                        }
                    case ConsoleKey.DownArrow:
                        {
                            return 1;
                        }
                    case ConsoleKey.Enter:
                        {
                            return 0;
                        }
                }
                    Console.BackgroundColor = ConsoleColor.Black;

                }
            }
            //  Якщо в масиві менше 20 елементів 
            else
            {
                Params.ClearRight();
                Console.BackgroundColor = ConsoleColor.Blue;
                int CounterX = (98 / 2) + 9;
                int CounterY = 2;
                foreach (var str in nameTake)
                {
                    Console.SetCursorPosition(CounterX, CounterY++);

                    if (str != null)
                    {
                        if (str.Length < 48)
                        {
                            Console.WriteLine(str);
                        }
                        else
                        {
                            for (int i = 0; i < 48; i++)
                            {
                                Console.Write(str[i]);
                            }
                            Console.Write("..");
                        }
                    }
                    else
                    {
                        CounterY--;
                    }

                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                Console.BackgroundColor = ConsoleColor.Black;
            }


            Console.BackgroundColor = ConsoleColor.Black;
            return -5;
        }
    }
}
