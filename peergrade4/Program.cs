using System;
using System.Collections.Generic;
using System.IO;

namespace peergrade4
{
    class Program
    {
        static Storage storage;
        /// <summary>
        /// Создание склада через консоль.
        /// </summary>
        static void MakeStorageWihtConsole()
        {
            Console.WriteLine("Введите стоимость аренды вашего склада.");
            double rent;
            while (!double.TryParse(Console.ReadLine(), out rent) || rent < 0) Console.WriteLine("Некорректная цена.");
            Console.WriteLine("Введите вместимость вашего склада.");
            int capacity;
            while (!int.TryParse(Console.ReadLine(), out capacity) || capacity < 0) Console.WriteLine("Некорректное число ящиков.");
            storage = new Storage(capacity, rent);
        }
        /// <summary>
        /// Запуск программы.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Приветствую вы находитесь в приложении \"Мой склад\"");
            while (true)
            {
                Console.WriteLine("Выбирете способ ввода данных");
                Console.WriteLine("1)Ввод данных через консоль");
                Console.WriteLine("2)Ввод данных через файлы");
                Console.WriteLine("Для выхода введите exit");

                    var a = Console.ReadLine();
                switch (a.Trim().ToLower())
                {
                    case "1":
                        StartWithConsole();
                        break;
                    case "2":
                        StartWithFiles();
                        break;
                    case "exit":
                        System.Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Пожалуйста выберете пункт из списка.");
                        break;
                }
            }
        }
        /// <summary>
        /// Работа через консоль.
        /// </summary>
        static void StartWithConsole()
        {
            Console.Clear();
            MakeStorageWihtConsole();
            do
            {
                Console.Clear();
                Console.WriteLine("Пожалуйста выберете команду:");
                Console.WriteLine("1)Добавить контейнер на склад.");
                Console.WriteLine("2)Удалить контейнер из склада.");
                Console.WriteLine("3)Узнать информацию о складе.");
                Console.WriteLine("exit для выхода из приложения.");
                var a = Console.ReadLine();
                switch (a.Trim().ToLower())
                {
                    case "1":
                        AddContainer();
                        break;
                    case "2":
                        PopContainer();
                        break;
                    case "3":
                        StorageInfo();
                        break;
                    case "exit":
                        System.Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Пожалуйста выберете пункт из списка.");
                        break;
                }
            } while (true);
        }
        /// <summary>
        /// Создание нового контейнера, и добавление его на склад.
        /// </summary>
        static void AddContainer()
        {
            Console.WriteLine("Введите количество ящиков в контейнере");
            uint n;
            while (!uint.TryParse(Console.ReadLine(), out n)) Console.WriteLine("Некорректное число ящиков.");
            storage.Add((int)n);

        }
        /// <summary>
        /// Вывод списка контейнеров.
        /// </summary>
        static void StorageInfo()
        {
            if(storage.сontainers.Count == 0)
            {
                Console.WriteLine("Склад пуст.");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("Список контейнеров.");
            for(int i = 0; i < storage.сontainers.Count; i++)
            {
                Console.WriteLine(storage.сontainers[i]);
            }
            Console.ReadKey();
        }
        /// <summary>
        /// Удаление контейнера со склада.
        /// </summary>
        static void PopContainer()
        {
            Console.WriteLine("Выберите id контейнера который хотите продать. Для возврата введите back.");
            for (int i = 0; i < storage.сontainers.Count; i++)
            {
                Console.WriteLine(storage.сontainers[i]);
            }
            string id;
            while (true)
            {
                id = Console.ReadLine();
                if (storage.GetId(id)) break;
                if (id == "back") return;
                Console.WriteLine("Введенного id нет в системе. Для возврата введите back.");
            }
            storage.Pop(id);
        }
        /// <summary>
        /// Проверка на существование файла.
        /// </summary>
        /// <param name="n">Номер файла</param>
        /// <param name="path">Полный путь до файла.</param>
        /// <returns></returns>
        static bool FileExists(int n, out string path)
        {
            
            Console.WriteLine($"Введите полный путь до {n}го файла");
            path = Console.ReadLine();
            if (path == "") return false;
            FileInfo file = new FileInfo(path);
            if (!file.Exists)
            {
                Console.WriteLine("Файла с таким именем не существует!");
                return false;
            }
            return true;


        }
        /// <summary>
        /// Работа через файлы.
        /// </summary>
        static void StartWithFiles()
        {
            Console.WriteLine("Создайте 3 файла.");
            Console.WriteLine("В первом файле в одной строке напишите стоимость аренды и количество ящиков в складе через пробел.");
            Console.WriteLine("Во втором файле в каждой строке введите команду: Add или Pop.");
            Console.WriteLine("В 3тьем файле через пробел введите количество ящиков, массу и стоимость каждого из них, для команды Add.");
            Console.WriteLine("Или id для команды Pop.(id - номер добавления контейнера)");
            string path, path1, path2;
            while (!FileExists(1, out path)) ;
            while (!FileExists(2, out path1)) ;
            while (!FileExists(3, out path2)) ;
            if (!MakeStorageWihtFile(path))
            {
                Console.WriteLine("Ошибка входных данных в первом файле.");
            }
            List<string> commands = new List<string>();
            List<string> data = new List<string>();
            using (StreamReader sr = new StreamReader(path1, System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    commands.Add(line);
                }
            }
            using (StreamReader sr = new StreamReader(path2, System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    data.Add(line);
                }
            }
            ProcessFiles(ref commands, ref data);
            return;
        }
        /// <summary>
        /// Создание склада через файл.
        /// </summary>
        /// <param name="path">путь до файла.</param>
        /// <returns></returns>
        static bool MakeStorageWihtFile(string path)
        {
            try
            {
                string input;
                using (StreamReader sr = new StreamReader(path))
                {
                    input = sr.ReadToEnd();
                }
                if (input.Split(' ').Length == 2)
                {
                    double rent;
                    int capacity;
                    if (!double.TryParse(input.Split(' ')[0], out rent) || rent < 0)
                    {
                        Console.WriteLine("Некорректная цена.");
                        return false;
                    }
                    if (!int.TryParse(input.Split(' ')[1], out capacity) || capacity < 0)
                    {
                        Console.WriteLine("Некорректное число ящиков.");
                        return false;
                    }
                    storage = new Storage(capacity, rent);
                }
            }
            catch
            {
                Console.WriteLine("Ошибка при чтении файла.");
                return false;
            }
            return true;
        }
        /// <summary>
        /// Обработка комманд через файлы.
        /// </summary>
        /// <param name="commands">Команды.</param>
        /// <param name="data">Входные данные.</param>
        /// <returns></returns>
        static bool ProcessFiles(ref List<string> commands, ref List<string> data)
        {
            if (commands.Count != data.Count)
            {
                Console.WriteLine("количество команд не соответствует вхожным данным");
                return false;
            }
            for (int i = 0; i < commands.Count; i++)
            {
                if (commands[i].ToLower().Trim() == "pop")
                {
                    if (!storage.GetId(data[i]))
                    {
                        Console.WriteLine("Неверные входные данные для i строки.");
                        return false;
                    }
                    storage.Pop(data[i]);
                    Console.WriteLine("Контейнер успешно удален.");
                    StorageInfo();
                }
                else if (commands[i].ToLower().Trim() == "add")
                {
                    string input = data[i];
                    List<double> doubleInput = new List<double>();
                    for (int j = 0; j < input.Split(' ').Length; j++)
                    {
                        if (!double.TryParse(input.Split(' ')[j], out double inp))
                        {
                            Console.WriteLine($"Неверные входные данные для {i} строки.");
                            return false;
                        }
                        doubleInput.Add(inp);
                    }
                    if (doubleInput[0] * 2 + 1 != doubleInput.Count)
                    {
                        Console.WriteLine($"Неверные входные данные для {i} строки.");
                        return false;
                    }
                    storage.Add(ref doubleInput);
                }
            }
            Console.WriteLine("Для продолжения нажмите любую кнопку.");
            Console.ReadKey();
            Console.Clear();
            return true;
        }

    }
    
}
