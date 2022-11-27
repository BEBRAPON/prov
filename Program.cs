
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace prov
{
    internal class Program
    {

        static void Main(string[] args)
        {
            diski.Disks();
        }
    }
    internal static class diski
    {

        public static void Disks()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            var disky = drives.Length - 1;
            foreach (var item in drives)
            {

                Console.WriteLine($"  {item.Name} ----- доступно {item.AvailableFreeSpace / 1024 / 1024 / 1024}Гб ({item.AvailableFreeSpace / 1024 / 1024} Мб) из {item.TotalSize / 1024 / 1024 / 1024}Гб ({item.AvailableFreeSpace / 1024 / 1024} Мб)");
            }
            Console.WriteLine("========================================================================================================================");
            Console.SetCursorPosition(60, drives.Length + 1);
            Console.WriteLine("Диски и устройства");
            Console.WriteLine("========================================================================================================================");

            StrelkiAndMaxAndMin menu = new StrelkiAndMaxAndMin(0, disky);

            int pos = StrelkiAndMaxAndMin.Strelki();

            StrelkiAndMaxAndMin.key = Console.ReadKey();
            Console.Clear();
            DirsAndFiles(drives[pos].ToString());
        }
        static void DirsAndFiles(string path)
        {
            int pos1 = 0;
            int pos2 = 0;

            while (true)
            {

                DirectoryInfo dir = new DirectoryInfo(path);
                var directories = Directory.GetDirectories(path);
                var files = Directory.GetFiles(path);
                var DlinaDirsAndFiles = directories.Length + files.Length - 1;

                StrelkiAndMaxAndMin menu = new StrelkiAndMaxAndMin(0, DlinaDirsAndFiles);

                foreach (var item in dir.GetDirectories())
                {
                    Console.WriteLine("  " + item.Name);
                }

                foreach (var item in dir.GetFiles())
                {
                    Console.WriteLine("  " + item.Name);
                }

                foreach (var item in directories)
                {
                    DateTime date = File.GetCreationTime(item);
                    Console.SetCursorPosition(50, pos1++);
                    Console.WriteLine(date);
                    FileInfo failes = new FileInfo(item);
                    Console.SetCursorPosition(80, pos2++);
                    Console.WriteLine(failes.Extension);
                }

                foreach (var item in files)
                {
                    DateTime date = File.GetCreationTime(item);
                    Console.SetCursorPosition(50, pos1++);
                    Console.WriteLine(date);
                    FileInfo failes = new FileInfo(item);
                    Console.SetCursorPosition(80, pos2++);
                    Console.WriteLine(failes.Extension);
                }
                Console.WriteLine("========================================================================================================================");
                Console.SetCursorPosition(5, DlinaDirsAndFiles + 2);
                Console.WriteLine("Название");
                Console.SetCursorPosition(54, DlinaDirsAndFiles + 2);
                Console.WriteLine("Дата создания");
                Console.SetCursorPosition(81, DlinaDirsAndFiles + 2);
                Console.WriteLine("Тип");
                Console.WriteLine("========================================================================================================================");

                pos1 = 0;
                pos2 = 0;

                StrelkiAndMaxAndMin.key = new ConsoleKeyInfo();
                int pos = StrelkiAndMaxAndMin.Strelki();
                Console.Clear();
                if (pos == -1)
                {
                    return;
                }

                else if (pos < directories.Length)
                {
                    DirsAndFiles(directories[pos].ToString());
                }

                else
                {
                    Process.Start(new ProcessStartInfo { FileName = files[pos - directories.Length], UseShellExecute = true });
                }


            }

        }
    }
    internal class StrelkiAndMaxAndMin

    {
        public static ConsoleKeyInfo key = new ConsoleKeyInfo();
        public static int posStr = 0;
        public static int max;
        public static int min;
        public StrelkiAndMaxAndMin(int Min, int Max)
        {
            min = Min;
            max = Max;

        }

        public static int Strelki()
        {
            string p = "  ";
            while (true)
            {
                if (key.Key == ConsoleKey.UpArrow)
                {
                    Console.SetCursorPosition(0, posStr);
                    Console.WriteLine(p);
                    posStr--;

                    if (posStr < min)
                    {
                        posStr = min;
                    }
                }

                if (key.Key == ConsoleKey.DownArrow)
                {
                    Console.SetCursorPosition(0, posStr);
                    Console.WriteLine(p);
                    posStr++;
                    if (posStr > max)
                    {
                        posStr = max;
                    }
                }


                else if (key.Key == ConsoleKey.Enter)
                {
                    break;

                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    return -1;
                }

                Console.SetCursorPosition(0, posStr);
                Console.WriteLine("->");
                key = Console.ReadKey();

            }
            return posStr;
        }



    }

}
