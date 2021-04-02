using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

        // Дни недели для модуля Завуч
        static string[] daysOfWeek =
        {
            "Понедельник",
            "Вторник",
            "Среда",
            "Четверг",
            "Пятница",
            "Суббота"
        };

        // Индекс информаци из модуля Директор
        static int directorInformationIndex = 0;

        // Информация изменения по каждому подразделу директора
        static string[] informationEdit;

        // Информация удаления по каждому подразделу директора
        static string[] informationDelete;

        // Имя файла, который нужно открыть для модуля Директор
        static string informationSelect = "";

        // Имя файла, который открывает файл с классами для модуля Завуч
        static string[] informationTeacherClasses;

        // Имя файла, который открывает файл с предметами для модуля Завуч
        static string[] informationTeacherLessons;

        // Имя файла, с именами учителей для модуля Завуч
        static string[] informationTeacherName;

        // Интрументы модуля Диреткор
        static string[] tools =
            {
                "ДОБАВИТЬ",
                "ИЗМЕНИТЬ",
                "УДАЛИТЬ"
            };

        // Выбор инструмента для выхода в меню
        static int selectTool = 0;

        // Для кнопки ESCAPE
        static int selectTool1 = 0;

        static int selectTool2 = 0;
        /// <summary>
        /// Надпись выхода
        /// </summary>
        static void EscButton()
        {
            string str = "Нажмите ESC, чтобы сделать шаг назад";
            int left = PositionLeft(str);
            Console.SetCursorPosition(left, 11);
            Console.WriteLine(str);
        }

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
                        // Информация из модуля Директор
                        else if (menu == directorInformation)
                        {
                            // Меню инструментов
                            ModuleDirectorTools(active);
                        }
                        // Интрументы модуля Диреткор
                        else if (menu == tools)
                        {
                            ModuleDirectorToolsInformation(active);
                        }
                        //меню по каждому подразделу директора
                        else if (menu == informationEdit)
                        {
                            // страница изменения объектов
                            ModuleDirectorInformationEdit(active);
                        }
                        //меню по каждому подразделу директора
                        else if (menu == informationDelete)
                        {
                            // страница изменения объектов
                            ModuleDirectorInformationDeleteObject(active);
                        }
                        // меню по классам завуча
                        else if (menu == informationTeacherClasses)
                        {
                            TeacherDaysOfWeek(active);
                        }
                        // меню по дням недели завуча
                        else if (menu == daysOfWeek)
                        {
                            TeacherLessons(active);
                        }
                        else if (menu == informationTeacherLessons)
                        {
                            TeacherNameLessons(active);
                        }
                        


                        break;
                    case ConsoleKey.Escape:
                        if (menu == informationDelete)
                        {
                            ModuleDirectorTools(selectTool);
                        }
                        else if (menu == informationEdit)
                        {
                            ModuleDirectorTools(selectTool);
                        }
                        else if (menu == tools)
                        {
                            ModuleDirector();
                        }
                        else if (menu == directorInformation)
                        {
                            Console.SetWindowSize(70, 40);
                            Console.BackgroundColor = ConsoleColor.DarkCyan;
                            Console.Clear();

                            string str = modules[0];
                            int left = PositionLeft(str);
                            Select(modules, left, 15);
                        }
                        else if (menu == informationTeacherClasses)
                        {
                            Console.SetWindowSize(70, 40);
                            Console.BackgroundColor = ConsoleColor.DarkCyan;
                            Console.Clear();

                            string str = modules[0];
                            int left = PositionLeft(str);
                            Select(modules, left, 15);
                        }
                        else if (menu == daysOfWeek)
                        {
                            TeacherInformation();
                        }
                        else if (menu == informationTeacherLessons)
                        {
                            TeacherDaysOfWeek(selectTool1);
                        }
                        else if (menu == informationTeacherName)
                        {
                            TeacherLessons(selectTool2);
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

                switch (module)
                {
                    case 0:
                        ModuleDirector();
                        break;
                    case 1:
                        ModuleTeacher();
                        break;
                    case 2:
                        Console.WriteLine("test");
                        break;
                }
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

            EscButton();

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
            selectTool = tool;

            EscButton();

            string str = $"{directorInformation[tool].ToUpper()}";
            int left = PositionLeft(str);
            Console.SetCursorPosition(left, 13);
            Console.WriteLine(str);

            // получаем имя файла
            switch (tool)
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

            str = tools[0];
            left = PositionLeft(str);
            Select(tools, left, 16);
        }

        /// <summary>
        /// Выбор инструмента
        /// </summary>
        /// <param name="tool"></param>
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
                    ModuleDirectorInformationAdd();
                    break;
                case 1:
                    ModuleDirectorInformation(directorInformationIndex);
                    break;
                case 2:
                    ModuleDirectorInformationDelete(directorInformationIndex);
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

            EscButton();

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

            EscButton();

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
            str = "ИЗМЕНИТЬ";
            left = PositionLeft(str);
            Console.SetCursorPosition(left, 13);
            Console.WriteLine(str);
            EscButton();

            str = informationEdit[0];
            left = PositionLeft(str);
            Select(informationEdit, left, 16);
        }

        /// <summary>
        /// Страница добавления объектов
        /// </summary>
        /// <param name="informationAddCurrent">Индекс объекта, который надо добавить</param>
        static void ModuleDirectorInformationAdd()
        {
            Console.Clear();
            TitleTimetable();

            EscButton();

            // чтение данных
            // кол-во строк в файле
            int linesCount = File.ReadAllLines($@"director/{informationSelect}.txt").Length;

            // читаем из файла
            FileStream informationStream = new FileStream($@"director/{informationSelect}.txt", FileMode.OpenOrCreate);
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

            // список объектов
            string str = informationEdit[0];
            int left = PositionLeft(str);
            int yIndex = 17;
            foreach (var information in informationEdit)
            {
                Console.SetCursorPosition(left, yIndex);
                Console.WriteLine(information);
                yIndex++;
            }

            Console.SetCursorPosition(25, 15);
            Console.Write("ДОБАВИТЬ: ");

            // выход назад
            ConsoleKeyInfo info = Console.ReadKey(true);
            if (info.Key == ConsoleKey.Escape)
            {
                ModuleDirectorTools(selectTool);
            }

            string informationAdd = Console.ReadLine();

            // записываем в файл измененные данные
            FileStream informationStream2 = new FileStream($@"director/{informationSelect}.txt", FileMode.OpenOrCreate);
            StreamWriter informationWriter = new StreamWriter(informationStream2, Encoding.Default);

            foreach (var information in informationEdit)
            {
                informationWriter.WriteLine($"{information}");
            }
            informationWriter.Write(informationAdd);

            informationWriter.Close();
            informationStream2.Close();

            ModuleDirectorInformationAdd();
        }

        /// <summary>
        /// Страница удаления объектов
        /// </summary>
        /// <param name="informationAddCurrent">Индекс объекта, который надо удалить</param>
        static void ModuleDirectorInformationDelete(int informationDeleteCurrent)
        {
            //Console.Clear();
            TitleTimetable();

            EscButton();

            // кол-во строк в файле
            int linesCount = File.ReadAllLines($@"director/{informationSelect}.txt").Length;

            // читаем из файла
            FileStream informationStream = new FileStream($@"director/{informationSelect}.txt", FileMode.OpenOrCreate);
            StreamReader informationReader = new StreamReader(informationStream, Encoding.Default);

            int countIndex = 0;

            informationDelete = new string[linesCount];

            while (!informationReader.EndOfStream)
            {
                informationDelete[countIndex] = informationReader.ReadLine();
                countIndex++;
            }

            informationReader.Close();
            informationStream.Close();

            string str = informationDelete[0];
            int left = PositionLeft(str);
            Select(informationDelete, left, 16);
        }

        static void ModuleDirectorInformationDeleteObject(int informationDeleteCurrent)
        {
            Console.Clear();
            TitleTimetable();

            EscButton();

            // записываем пустой символ
            informationDelete[informationDeleteCurrent] = "";

            // новый массив
            string[] informationDelete2 = new string[informationDelete.Length - 1];
            int index = 0;
            for (int i = 0; i < informationDelete.Length; i++)
            {
                if (informationDelete[i] != "")
                {
                    informationDelete2[index] = informationDelete[i];
                    index++;
                }
            }

            informationDelete = new string[informationDelete2.Length];
            informationDelete = informationDelete2;

            File.Delete($@"director/{informationSelect}.txt");

            // записываем в файл измененные данные
            FileStream informationStream = new FileStream($@"director/{informationSelect}.txt", FileMode.OpenOrCreate);
            StreamWriter informationWriter = new StreamWriter(informationStream, Encoding.Default);

            foreach (var information in informationDelete)
            {
                informationWriter.WriteLine($"{information}");
            }

            informationWriter.Close();
            informationStream.Close();

            //foreach (var inb in informationDelete)
            //{
            //    Console.WriteLine(inb);
            //}


            string str = "УДАЛИТЬ";
            int left = PositionLeft(str);
            Console.SetCursorPosition(left, 13);
            Console.WriteLine(str);

            str = informationDelete[0];
            left = PositionLeft(str);
            Select(informationDelete, left, 16);
        }

        //--------------------------МОДУЛЬ 2--------------------------------

        static void ModuleTeacher()
        {
            Console.Clear();
            TitleTimetable();

            EscButton();



            string str = directorInformation[1];
            int left = PositionLeft(str);

            //Select(TeacherInformation, left, 16); 
            TeacherInformation();
        }

        static void TeacherInformation(/*int teacherInformation*/)
        {
            Console.Clear();
            TitleTimetable();
            EscButton();

            string str = "КЛАССЫ";
            int left = PositionLeft(str);
            Console.SetCursorPosition(left, 13);
            Console.WriteLine(str);

            // кол-во строк в файле
            int linesCount = File.ReadAllLines($@"director/classes.txt").Length;

            // читаем из файла
            FileStream informationStream = new FileStream($@"director/classes.txt", FileMode.Open);
            StreamReader informationReader = new StreamReader(informationStream, Encoding.Default);

            int countIndex = 0;

            informationTeacherClasses = new string[linesCount];

            while (!informationReader.EndOfStream)
            {
                informationTeacherClasses[countIndex] = informationReader.ReadLine();
                countIndex++;
            }

            informationReader.Close();
            informationStream.Close();

            str = informationTeacherClasses[0];
            left = PositionLeft(str);

            Select(informationTeacherClasses, left, 16);
        }

        static void TeacherDaysOfWeek(int clas)
        {
            Console.Clear();
            TitleTimetable();

            EscButton();

            selectTool1 = clas;

            string str = $"КЛАСС: {informationTeacherClasses[clas]}";
            int left = PositionLeft(str);
            Console.SetCursorPosition(left, 13);
            Console.WriteLine(str);

            str = daysOfWeek[0];
            left = PositionLeft(str);

            Select(daysOfWeek, left, 16);
        }

        static void TeacherLessons(int day)
        {
            Console.Clear();
            TitleTimetable();
            EscButton();

            selectTool2 = day;

            string str = $"ДЕНЬ НЕДЕЛИ: {daysOfWeek[day]}";
            int left = PositionLeft(str);
            Console.SetCursorPosition(left, 13);
            Console.WriteLine(str);
            // кол-во строк в файле
            int linesCount = File.ReadAllLines($@"director/lessons.txt").Length;

            // читаем из файла
            FileStream informationStream = new FileStream($@"director/lessons.txt", FileMode.Open);
            StreamReader informationReader = new StreamReader(informationStream, Encoding.Default);

            int countIndex = 0;

            informationTeacherLessons = new string[linesCount];

            while (!informationReader.EndOfStream)
            {
                informationTeacherLessons[countIndex] = informationReader.ReadLine();
                countIndex++;
            }

            informationReader.Close();
            informationStream.Close();

            str = informationTeacherLessons[0];
            left = PositionLeft(str);

            Select(informationTeacherLessons, left, 16);
        }

        static void TeacherNameLessons(int lesson)
        {
            Console.Clear();
            TitleTimetable();
            EscButton();

            selectTool2 = lesson;

            string str = $"ПРЕДМЕТ: {informationTeacherLessons[lesson]}";
            int left = PositionLeft(str);
            Console.SetCursorPosition(left, 13);
            Console.WriteLine(str);
            // кол-во строк в файле
            int linesCount = File.ReadAllLines($@"director/teachers.txt").Length;

            // читаем из файла
            FileStream informationStream = new FileStream($@"director/teachers.txt", FileMode.Open);
            StreamReader informationReader = new StreamReader(informationStream, Encoding.Default);

            int countIndex = 0;

            informationTeacherName = new string[linesCount];

            while (!informationReader.EndOfStream)
            {
                informationTeacherName[countIndex] = informationReader.ReadLine();
                countIndex++;
            }

            informationReader.Close();
            informationStream.Close();

            str = informationTeacherName[0];
            left = PositionLeft(str);

            Select(informationTeacherName, left, 16);
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

