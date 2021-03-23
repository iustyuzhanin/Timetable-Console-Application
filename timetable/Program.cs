using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timetable
{
    class Program
    {
        // Модули
        static string[] modules =
            {
                "ДИРЕКТОР",
                "ЗАВУЧ",
                "ИНФОРМЕР"
            };

        /// <summary>
        /// Вертикальное меню
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="active"></param>
        static void DrawMenu(string[] menu, int x, int y, int active)
        {
            //Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;

            for (int i = 0; i < menu.Length; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.WriteLine(menu[i]);
            }

            //Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(x, y + active);

            Console.WriteLine(menu[active]);
        }

        /// <summary>
        /// Перемещение курсором по меню и выбор
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        static int Select(string[] menu, int x, int y)
        {
            Console.SetCursorPosition(25, 5);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("РАСПИСАНИЕ УРОКОВ");

            Console.CursorVisible = false;
            int active = 0;
            bool isWorking = true;
            //int menuBorder = 0;
            while (isWorking)
            {
                // 0) отрисуем менюшку
                DrawMenu(menu, x, y, active);
                // 1) считываем клавишу
                ConsoleKeyInfo info = Console.ReadKey(true);
                // 2) анализ клавиши
                switch (info.Key)
                {
                    case ConsoleKey.Enter:
                        isWorking = false;
                        Authorization();
                        break;
                    case ConsoleKey.UpArrow:
                        if (active > 0)
                        {
                            active--;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (active < menu.Length - 1)
                        {
                            active++;
                        }
                        break;
                }
            }
            Console.CursorVisible = true;
            return active;
        }

        static void Authorization()
        {
            Console.Clear();

            Console.SetCursorPosition(25, 5);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("РАСПИСАНИЕ УРОКОВ");

            Console.SetCursorPosition(25, 14);
            Console.WriteLine("Введите логин:");
            Console.SetCursorPosition(25, 15);
            string login = Console.ReadLine();

            Console.SetCursorPosition(25, 17);
            Console.WriteLine("Введите пароль:");
            Console.SetCursorPosition(25, 18);
            ConsoleKeyInfo info;
            var password = "";
            while ((info = Console.ReadKey(true)).Key != ConsoleKey.Enter)
            {
                if (info.Key == ConsoleKey.Backspace)
                {
                    if (password.Length > 0)
                    {
                        password = password.Remove(password.Length - 1);
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                    }
                }
                else if (info.KeyChar != 0)
                {
                    password += info.KeyChar;
                    Console.Write("*");
                }
            }

            Console.WriteLine(password);
            Console.ReadKey();
            Console.Clear();
            Select(modules, 30, 15);

        }

        static void Main(string[] args)
        {
            Console.SetWindowSize(70, 40);
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.Clear();

            int index = Select(modules, 30, 15);

            Console.WriteLine($"ВЫ {modules[index]}");
            Console.ReadKey(true);
        }
    }
}

