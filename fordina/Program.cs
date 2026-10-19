using System;
using System.Collections.Generic;
using System.IO;
using fordina.Student;

namespace fordina
{
    public class Program
    {

        public static Dictionary<string, student> PrintDictionary(string path)
        {
            Dictionary<string, student > dict = new Dictionary<string, student>();
            using(StreamReader reader =  new StreamReader(path))
            {
                string line;
                while((line = reader.ReadLine()) != null)
                {
                    string[] infoStudent = line.Split(',');
                    string key = infoStudent[0] + " " + infoStudent[1];
                    student student = new student(infoStudent[0], infoStudent[1], int.Parse(infoStudent[2]), infoStudent[3], int.Parse(infoStudent[4]));
                    dict[key] = student;
                }
            }
            return dict;
        }

        public static int Sorted(KeyValuePair<string, student> first, KeyValuePair<string, student> second)
        {
            if ( first.Value.Point > second.Value.Point)
            {
                return 1;
            }
            if (first.Value.Point < second.Value.Point)
            {
                return -1;
            }
            return 0;
        }
        public static void Opeation(Dictionary<string, student> journal)
        {
            bool isCommand = true;
            while (isCommand)
            {
                Console.WriteLine("Введите команду: Новый студент/Удалить/Сортировать/Выход");
                string command = Console.ReadLine().ToLower();

                if (command == "новый студент")
                {
                    Console.WriteLine("Введите информацию о новом студенте:");
                    Console.WriteLine("Имя:");
                    string name = Console.ReadLine();
                    Console.WriteLine("Фамилия:");
                    string surname = Console.ReadLine();
                    Console.WriteLine("Год рождения:");
                    bool isYear = int.TryParse(Console.ReadLine(), out int year);
                    Console.WriteLine("Экзамен:");
                    string exam = Console.ReadLine();
                    Console.WriteLine("Баллы:");
                    bool isPoint = int.TryParse(Console.ReadLine(), out int point);

                    string key = surname + " " + name;
                    student student = new student(surname, name, year, exam, point);

                    journal[key] = student;
                }
                else if (command == "удалить")
                {
                    Console.WriteLine("Введите имя и фамилию студента через пробел");
                    string key = Console.ReadLine();
                    if (!journal.ContainsKey(key))
                    {
                        Console.WriteLine("Введены неверные данные");
                        continue;
                    }
                    else
                    {
                        journal.Remove(key);
                    }
                }
                else if (command == "сортировать")
                {
                    List<KeyValuePair<string, student>> sortedJournal = new List<KeyValuePair<string, student>>(journal);
                    sortedJournal.Sort(Sorted);
                    journal.Clear();
                    foreach (KeyValuePair<string, student> i in sortedJournal)
                    {
                        journal[i.Key] = i.Value;
                    }
                }
                else if (command == "выход")
                {
                    isCommand = false;
                }
                else
                {
                    Console.WriteLine("Команда не распознана, попробуйте еще раз");
                }
            }
        }
        public static void Main(string[] args)
        {
            //Номер 1
            Console.WriteLine("1. Создать List на 64 элемента, скачать из интернета 32 пары картинок (любых). В List\r\n" +
                "должно содержаться по 2 одинаковых картинки. Необходимо перемешать List с\r\n" +
                "картинками. Вывести в консоль перемешанные номера (изначальный List и\r\n" +
                "полученный List). Перемешать любым способом.");
            string papkaPath = "..\\..\\картинки для дз";

            List<string> files;
            
            string[] filePaths = Directory.GetFiles(papkaPath);

            files = new List<string>(filePaths);
            
            Console.WriteLine("Пути к картинкам до перемешки:");
            foreach(string path in files)
            {
                Console.WriteLine(path);
            }

            //Перемешивание
            Random rand = new Random();

            for(int i = files.Count - 1; i > 0; i--)
            {
                int j = rand.Next(i + 1);
                (files[i], files[j]) = (files[j], files[i]);
            }

            Console.WriteLine("Пути к картинкам после перемешки:");
            foreach (string path in files)
            {
                Console.WriteLine(path);
            }

            //Номер 2
            Console.WriteLine("2. Создать студента из вашей группы (фамилия, имя, год рождения, с каким\r\n" +
                "экзаменом поступил, баллы). Создать словарь для студентов из вашей группы (10\r\n" +
                "человек).\r\n" +
                "Необходимо прочитать информацию о студентах с файла. В консоли необходимо создать\r\n" +
                "меню:\r\n" +
                "a. Если пользователь вводит: Новый студент, то необходимо\r\n" +
                "ввести информацию о новом студенте и добавить его в List\r\n" +
                "b. Если пользователь вводит: Удалить, то по фамилии и имени удаляется\r\n" +
                "студент\r\nc. Если пользователь вводит: Сортировать, то происходит сортировка по\r\n" +
                "баллам (по возрастанию)");

            Dictionary<string, student> journal = PrintDictionary("..\\..\\Students.txt");

            Opeation(journal);
            foreach(KeyValuePair<string, student> i in journal)
            {
                Console.Write(i.Key);
                i.Value.PrintInfo();
            }
            //Номер 3
            Console.WriteLine("3. Создать бабулю. У бабули есть Имя, возраст, болезнь и лекарство от этой болезни,\r\n" +
                "которое она принимает (болезней может быть у бабули несколько). Реализуйте список\r\n" +
                "бабуль. Также есть больница (у больницы есть название, список болезней, которые\r\n" +
                "они лечат и вместимость). Больниц также, как и бабуль несколько. Бабули по очереди\r\n" +
                "поступают (реализовать ввод с клавиатуры) и дальше идут в больницу, в зависимости от\r\n" +
                "заполненности больницы и списка болезней, которые лечатся в данной больнице,\r\n" +
                "реализовать функционал, который будет распределять бабулю в нужную больницу. Если\r\n" +
                "бабуля не имеет болезней, то она хочет только спросить - она может попасть в первую\r\n" +
                "свободную клинику. Если бабулю ни одна из больниц не лечит, то бабуля остаётся на\r\n" +
                "улице плакать. На экран выводить список всех бабуль, список всех больниц,\r\n" +
                "болезни, которые там лечат и сколько бабуль в данный момент находится в\r\n" +
                "больнице, также\r\n" +
                "вывести процент заполненности больницы. P.S. Бабуля попадает в больницу, если\r\n" +
                "там лечат более 50% ее болезней. Больницы реализовать в виде стека, бабуль в виде\r\n" +
                "очереди.");


            //Номер 4
            Console.WriteLine("4. Написать метод для обхода графа в глубину или ширину - вывести на экран\r\nкратчайший путь.");


        }
    }
}
