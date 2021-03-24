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

        // Информация для директора
        static string[] directorInformation =
            {
                "Учителя",
                "Предметы",
                "Классы",
                "Кабинеты",
                "Время звонков"
            };

        /// <summary>
        /// Заголовок - РАСПИСАНИЕ УРОКОВ
        /// </summary>
        static void TitleTimetable()
        {
            Console.SetCursorPosition(25, 5);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("РАСПИСАНИЕ УРОКОВ");
        }

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

            Console.WriteLine(menu[active].ToUpper());
        }

        /// <summary>
        /// Перемещение курсором по меню и выбор
        /// </summary>
        /// <param name="menu">Меню</param>
        /// <param name="x">Х</param>
        /// <param name="y">У</param>
        static int Select(string[] menu, int x, int y)
        {
            TitleTimetable();

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

                        if (menu == modules)
                        {
                            Authorization(active);
                        }
                        else if (menu == directorInformation)
                        {
                            ModuleDirectorInformation(active);
                        }

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

            TitleTimetable();

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
                ModuleDirector();
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
           
            //Console.Clear();
            //Select(modules, 28, 15);
        }

        static void ModuleDirector()
        {
            Console.Clear();
            TitleTimetable();

            Console.SetCursorPosition(20, 13);
            Console.WriteLine("ДОБАВИТЬ/ИЗМЕНИТЬ ИНФОРМАЦИЮ:");

            Select(directorInformation, 28, 16);
        }

        static void ModuleDirectorInformation(int information)
        {
            Console.Clear();
            TitleTimetable();

            Console.SetCursorPosition(27, 13);
            Console.WriteLine($"{directorInformation[information].ToUpper()}:");

            string informationSelect = "";
            switch (information)
            {
                case 0:
                    informationSelect = "teachers";
                    break;
                case 1:
                    informationSelect = "lessons";
                    break;
                case 2:
                    informationSelect = "classes";
                    break;
                case 3:
                    informationSelect = "cabinets";
                    break;
                case 4:
                    informationSelect = "times";
                    break;
            }

            int linesCount = System.IO.File.ReadAllLines($@"director/{informationSelect}.txt").Length;

            FileStream informationStream = new FileStream($@"director/{informationSelect}.txt", FileMode.Open);
            StreamReader informationReader = new StreamReader(informationStream, Encoding.Default);

            int countIndex = 0;

            string[] informationEdit = new string[linesCount];

            while (!informationReader.EndOfStream)
            {
                informationEdit[countIndex] = informationReader.ReadLine();
                countIndex++;
            }

            informationReader.Close();
            informationStream.Close();

            Select(informationEdit, 23, 16);

        }

        static void Main(string[] args)
        {
            Console.SetWindowSize(70, 40);
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.Clear();

            Select(modules, 29, 15);

            //Console.WriteLine($"ВЫ {modules[index]}");
            Console.ReadKey(true);
        }
    }
}

