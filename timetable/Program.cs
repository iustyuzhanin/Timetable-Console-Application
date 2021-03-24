using System;
using System.Collections.Generic;
using System.IO;
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
        /// <param name="menu">Меню</param>
        /// <param name="x">Х</param>
        /// <param name="y">У</param>
        /// <param name="active">Выбор меню</param>
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
        /// <param name="menu">Меню</param>
        /// <param name="x">Х</param>
        /// <param name="y">У</param>
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
                        Authorization(active);
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

        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="module">Индекс модуля менюшки</param>
        static void Authorization(int module)
        {
            Console.Clear();

            int xPos = 26;

            Console.SetCursorPosition(25, 5);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("РАСПИСАНИЕ УРОКОВ");

            Console.SetCursorPosition(27, 13);
            Console.WriteLine("АВТОРИЗАЦИЯ");

            Console.SetCursorPosition(xPos, 16);
            Console.WriteLine("Введите логин:");
            Console.SetCursorPosition(xPos, 17);
            string login = Console.ReadLine();

            Console.SetCursorPosition(xPos, 19);
            Console.WriteLine("Введите пароль:");
            Console.SetCursorPosition(xPos, 20);

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

            //Console.WriteLine(password);
            //Console.ReadKey(true);

            // номер строки, где нах-ся нужный логин
            int lineNumber = 0;

            switch (module)
            {
                case 0:
                    lineNumber = 1;
                    break;
                case 1:
                    lineNumber = 5;
                    break;
                case 2:
                    lineNumber = 9;
                    break;
            }

            string loginFile = File.ReadLines("users.txt").Skip(lineNumber).Take(1).First();
            string passwordFile = File.ReadLines("users.txt").Skip(++lineNumber).Take(1).First();

            Console.SetCursorPosition(23, 22);
            if (login==loginFile && password==passwordFile)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Вход выполнен успешно.");
                Console.ReadKey(true);
            }
            else
            {
                Console.SetCursorPosition(20, 22);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine("Неправильный логин или пароль.");
                Console.SetCursorPosition(25, 23);
                Console.WriteLine("Введите еще раз.");
                Console.BackgroundColor = ConsoleColor.DarkCyan;
                Console.ReadKey(true);
                Authorization(module);
            }

            //Console.WriteLine(loginFile);
            //Console.WriteLine(passwordFile);
           
            Console.Clear();
            Select(modules, 28, 15);

        }

        static void Main(string[] args)
        {
            Console.SetWindowSize(70, 40);
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.Clear();

            int index = Select(modules, 29, 15);

            //Console.WriteLine($"ВЫ {modules[index]}");
            Console.ReadKey(true);
        }
    }
}

