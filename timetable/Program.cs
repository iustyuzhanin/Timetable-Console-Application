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

        // Информация из модуля Директор
        static string[] directorInformation =
            {
                "Учителя",
                "Предметы",
                "Классы",
                "Кабинеты",
                "Время звонков"
            };

        // Индекс информаци из модуля Директор
        static int directorInformationIndex = 0;

        // Информация по каждому подразделу директора
        static string[] informationEdit;

        // Имя файла, который нужно открыть для модуля Директор
        static string informationSelect = "";

        // интрументы модуля Диреткор
        static string[] tools =
            {
                "ДОБАВИТЬ",
                "ИЗМЕНИТЬ",
                "УДАЛИТЬ"
            };


        /// <summary>
        /// Для определения центра по ширине консоли для заданной строки
        /// </summary>
        /// <param name="str">Строка</param>
        /// <returns></returns>
        static int PositionLeft(string str)
        {
            int left = Console.WindowWidth / 2 - str.Length / 2;
            return left;
        }

        /// <summary>
        /// Заголовок - РАСПИСАНИЕ УРОКОВ
        /// </summary>
        static void TitleTimetable()
        {
            string str = "РАСПИСАНИЕ УРОКОВ";
            int left = PositionLeft(str);
            Console.SetCursorPosition(left, 5);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(str);
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

                        // выбираем в какой раздел заходить, зависит от текущего меню
                        // глвное меню
                        if (menu == modules)
                        {
                            // авторизация
                            Authorization(active);
                        }
                        //// меню в модуле Директор
                        //else if (menu == directorInformation)
                        //{
                        //    // список информации в модуле Директор
                        //    ModuleDirectorInformation(active);
                        //}
                        //меню по каждому подразделу директора
                        else if (menu == informationEdit)
                        {
                            // страница изменения объектов
                            ModuleDirectorInformationEdit(active);
                        }
                        else if (menu == directorInformation)
                        {
                            ModuleDirectorTools(active);
                        }
                        else if (menu == tools)
                        {
                            ModuleDirectorToolsInformation(active);
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
            TitleTimetable();

            string str = "АВТОРИЗАЦИЯ";
            int left = PositionLeft(str);
            Console.SetCursorPosition(left, 13);
            Console.WriteLine(str);

            str = "Введите логин:";
            left = PositionLeft(str);
            Console.SetCursorPosition(left, 16);
            Console.WriteLine(str);

            Console.SetCursorPosition(left, 17);
            string login = Console.ReadLine();

            Console.SetCursorPosition(left, 19);
            Console.WriteLine("Введите пароль:");
            Console.SetCursorPosition(left, 20);

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

            //Console.SetCursorPosition(23, 22);
            if (login==loginFile && password==passwordFile)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                str = "Вход выполнен успешно";
                left = PositionLeft(str);
                Console.SetCursorPosition(left, 22);
                Console.WriteLine(str);
                Console.ReadKey(true);
                ModuleDirector();
            }
            else
            {
                str = "Неправильный логин или пароль";
                left = PositionLeft(str);
                Console.SetCursorPosition(left, 22);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine(str);

                str = "Введите еще раз";
                left = PositionLeft(str);
                Console.SetCursorPosition(left, 23);
                Console.WriteLine(str);
                Console.BackgroundColor = ConsoleColor.DarkCyan;
                Console.ReadKey(true);
                Authorization(module);
            }

            //Console.WriteLine(loginFile);
            //Console.WriteLine(passwordFile);         
        }

        /// <summary>
        /// Меню модуля Директор
        /// </summary>
        static void ModuleDirector()
        {
            Console.Clear();
            TitleTimetable();

            string str = "ИНФОРМАЦИЯ";
            int left = PositionLeft(str);
            Console.SetCursorPosition(left, 13);
            Console.WriteLine(str);

            str = directorInformation[1];
            left = PositionLeft(str);

            Select(directorInformation, left, 16);
        }

        /// <summary>
        /// Меню инструментов
        /// </summary>
        /// <param name="tool">Индекс инструмента</param>
        static void ModuleDirectorTools(int tool)
        {
            Console.Clear();
            TitleTimetable();
            directorInformationIndex = tool;

            string str = $"{directorInformation[tool].ToUpper()}";
            int left = PositionLeft(str);
            Console.SetCursorPosition(left, 13);
            Console.WriteLine(str);

            str = tools[0];
            left = PositionLeft(str);
            Select(tools, left, 16);
        }

        static void ModuleDirectorToolsInformation(int tool)
        {
            Console.Clear();
            TitleTimetable();

            string str = $"{tools[tool].ToUpper()}";
            int left = PositionLeft(str);
            Console.SetCursorPosition(left, 13);
            Console.WriteLine(str);

            switch (tool)
            {
                case 0:
                    ModuleDirectorInformationAdd(directorInformationIndex);
                    break;
                case 1:
                    ModuleDirectorInformation(directorInformationIndex);
                    break;
                case 2:

                    break;
            }
        }

        /// <summary>
        /// Список объектов модуля Директор
        /// </summary>
        /// <param name="information">Индекс выбранной информации из модуля Директор</param>
        static void ModuleDirectorInformation(int information)
        {
            //Console.Clear();
            TitleTimetable();

            //string str = $"{tools[information].ToUpper()}";
            //int left = PositionLeft(str);
            //Console.SetCursorPosition(left, 13);
            //Console.WriteLine(str);

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

            // кол-во строк в файле
            int linesCount = File.ReadAllLines($@"director/{informationSelect}.txt").Length;

            // читаем из файла
            FileStream informationStream = new FileStream($@"director/{informationSelect}.txt", FileMode.Open);
            StreamReader informationReader = new StreamReader(informationStream, Encoding.Default);

            int countIndex = 0;

            informationEdit = new string[linesCount];

            while (!informationReader.EndOfStream)
            {
                informationEdit[countIndex] = informationReader.ReadLine();
                countIndex++;
            }

            informationReader.Close();
            informationStream.Close();

            string str = informationEdit[0];
            int left = PositionLeft(str);
            Select(informationEdit, left, 16);

        }

        /// <summary>
        /// Страница изменения объектов
        /// </summary>
        /// <param name="informationEditCurrent">Индекс объекта, который надо изменить</param>
        static void ModuleDirectorInformationEdit(int informationEditCurrent)
        {
            Console.Clear();
            TitleTimetable();

            string str = $"ИЗМЕНИТЬ: {informationEdit[informationEditCurrent]}";
            int left = PositionLeft(str);
            Console.SetCursorPosition(left, 15);
            Console.WriteLine(str);

            str = $"НА: ";
            Console.SetCursorPosition(left, 17);
            Console.Write(str);
            string informationEditNew = Console.ReadLine();

            foreach (var information in informationEdit)
            {
                if (information == informationEdit[informationEditCurrent])
                {
                    informationEdit[informationEditCurrent] = informationEditNew;
                }
            }

            // записываем в файл измененные данные
            FileStream informationStream = new FileStream($@"director/{informationSelect}.txt", FileMode.Open);
            StreamWriter informationWriter = new StreamWriter(informationStream, Encoding.Default);

            foreach (var information in informationEdit)
            {
                informationWriter.WriteLine($"{information}");
            }

            informationWriter.Close();
            informationStream.Close();

            Console.Clear();
            Select(informationEdit, 23, 16);

        }

        /// <summary>
        /// Страница добавления объектов
        /// </summary>
        /// <param name="informationAddCurrent">Индекс объекта, который надо добавить</param>
        static void ModuleDirectorInformationAdd(int informationAddCurrent)
        {
            Console.Clear();
            TitleTimetable();

            Console.SetCursorPosition(25, 15);
            Console.Write("ДОБАВИТЬ: ");
            string informationAdd = Console.ReadLine();

            // записываем в файл новые данные
            //FileStream informationStream = new FileStream($@"director/{informationSelect}.txt", FileMode.OpenOrCreate);
            //StreamWriter informationWriter = new StreamWriter(informationStream, Encoding.Default);

            //informationWriter.WriteLine(informationAdd);

            //informationWriter.Close();
            //informationStream.Close();
        }

        static void Main(string[] args)
        {
            Console.SetWindowSize(70, 40);
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.Clear();

            string str = modules[0];
            int left = PositionLeft(str);
            Select(modules, left, 15);

            //Console.WriteLine($"ВЫ {modules[index]}");
            Console.ReadKey(true);
        }
    }
}

