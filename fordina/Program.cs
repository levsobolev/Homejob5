using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using fordina.Grandmother;
using fordina.Hospital;
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

        public static void ShortestPathSearch(Dictionary<string, List<string>> graph, string start)
        {
            if (!graph.ContainsKey(start))
            {
                return;
            }
            Queue<string> queue = new Queue<string>();
            HashSet<string> visited = new HashSet<string>();

            queue.Enqueue(start);
            visited.Add(start);

            while (queue.Count > 0)
            {
                string currentPoint = queue.Dequeue();
                Console.WriteLine(currentPoint + " ");

                if(graph.TryGetValue(currentPoint, out List<string> neighbors))
                {
                    foreach(string neighbor in neighbors)
                    {
                        if (!visited.Contains(neighbor))
                        {
                            visited.Add(neighbor);
                            queue.Enqueue(neighbor);
                        }
                    }
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
            foreach (string path in files)
            {
                Console.WriteLine(path);
            }

            //Перемешивание
            Random rand = new Random();

            for (int i = files.Count - 1; i > 0; i--)
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
            foreach (KeyValuePair<string, student> i in journal)
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
            Queue<grandmother> listGrandmother = new Queue<grandmother>
            (new[] {
                new grandmother("Alica", 67, new Dictionary<string, string>(){ { "Артроз", "Мази" }, {"Гипертония", "Диуретики" } }),
                new grandmother("Taya", 64, new Dictionary<string, string>(){ { "Деменция", "Донепезил" }, {"Гипертония", "Диуретики" } }),
                new grandmother("Olga", 71, new Dictionary<string, string>(){ { "Артроз", "Мази" }, {"Насморк", "Нафтизин" } }),
                new grandmother("Masha", 75,new Dictionary<string, string>(){ { "Остеоартрит", "Мелоксикам" }, {"Остеопороз", "Мази" } }),
                new grandmother("Tanya", 60, new Dictionary<string, string>(){ { "Деменция", "Донепезил" }, {"Гипертония", "Диуретики" } }),
                new grandmother("Dasha", 83, new Dictionary<string, string>(){ { "Насморк", "Нафтизин" }, {"Остеоартрит", "Мелоксикам" } }),
                new grandmother("Vera", 90, new Dictionary<string, string>(){ { "Артроз", "Мази" }, {"Остеопороз", "Мази" } })
            });

            Stack<hospital> listHospitals = new Stack<hospital>
            (new[]{
                new hospital("Республиканская клиническая больница", new string[]{"Артроз", "Гипертония", "Деменция", "Диабет"}, 12, 10),
                new hospital("Городская больница №4", new string[]{"Артроз", "Диабет", "Остеоартрит", "Деменция"}, 20, 12),
                new hospital("Городская больница №2", new string[]{"Остеопороз", "Насморк", "Гипертония", "Деменция"}, 9, 8),
                new hospital("Республиканская клиническая больница №2", new string[]{"Остеоартрит", "Гипертония", "Деменция", "Насморк"}, 13, 13),
                new hospital("Республиканская клиническая инфекционная больница имени профессора А.Ф.Агафонова", new string[]{"Остеопороз", "Гипертония", "Артроз", "Насморк"}, 30, 15),
                new hospital("КОРЛ", new string[]{"Артроз", "Остеопороз", "Остеоартрит", "Насморк"}, 10, 2)
                
            });

            Console.WriteLine("Ввод данных бабушек(введите пустую строку чтобы выйти):");

            bool isInput = true;
            while (isInput)
            {
                Console.Write("Введите имя:");
                string name = Console.ReadLine();
                if (string.IsNullOrEmpty(name))
                {
                    break;
                }
                Console.Write("Введите возраст:");
                bool isAge = byte.TryParse(Console.ReadLine(), out byte age);
                if (isAge)
                {
                    age = 75;
                }

                grandmother currentGrand = new grandmother(name, age);

                Console.WriteLine("Введите ваши болезни и лекарства, которые от них принимаете(гнажмите enter два раза чтобы закончить ввод болезней):");
                bool isDiseases = true;

                while (isDiseases)
                {
                    Console.Write("Болезнь:");
                    string diseases = Console.ReadLine();
                    if(string.IsNullOrEmpty(diseases))
                    {
                        break;
                    }
                    Console.Write($"Лекарство от {diseases}:");
                    string medicine = Console.ReadLine();
                    if (string.IsNullOrEmpty(medicine))
                    {
                        break;
                    }
                    currentGrand.Diseases.Add(diseases, medicine);
                    
                }

                listGrandmother.Enqueue(currentGrand);
            }

            List<grandmother> list = new List<grandmother>(listGrandmother);

            List<grandmother> onStreet = new List<grandmother>();

            while(listGrandmother.Count > 0)
            {
                grandmother currentGra = listGrandmother.Dequeue();
                bool isStreet = false;

                if (currentGra.Diseases.Count == 0)
                {
                    foreach(hospital i in listHospitals)
                    {
                        if (!i.isFull())
                        {
                            i.AddFullness();
                            isStreet = true;
                            break;
                        }
                    }
                }
                else
                {
                    foreach(hospital i in listHospitals)
                    {
                        if(!i.isFull())
                        {
                            int length = currentGra.Diseases.Count;
                            int coincidences = 0;
                            foreach(KeyValuePair<string, string> dis in  currentGra.Diseases)
                            {
                                foreach(string dise in i.Diseases)
                                {
                                    if(dis.Key.ToLower() == dise.ToLower())
                                    {
                                        coincidences += 1;
                                        break;
                                    }
                                }
                            }
                            if((double)coincidences / length >= 0.5)
                            {
                                i.AddFullness();
                                isStreet = true;
                                break;
                            }
                        }
                    }
                }
                if (!isStreet)
                {
                    onStreet.Add(currentGra);
                }
            }

            List<hospital> listhos = new List<hospital>(listHospitals);

            Console.WriteLine("Отчет:\nСписок всех бабушек:");
            foreach(grandmother i in list)
            {
                Console.WriteLine($"{i.Name} {i.Age}");
            }
            Console.WriteLine($"Из них остались плакать на улице: {onStreet.Count}");
            Console.WriteLine("Список больниц, количества бабушек в них и процент их заполненности:");
            foreach(hospital i in listhos)
            {
                Console.WriteLine($"{i.Title}: {i.Fullness} посетителей; заполнена на {(double)i.Fullness / i.Capacity * 100:F1}%");
            }


            //Номер 4
            Console.WriteLine("4. Написать метод для обхода графа в глубину или ширину - вывести на экран\r\nкратчайший путь.");

            Dictionary<string, List<string>> graph = new Dictionary<string, List<string>>()
            {
                { "A", new List<string> { "B", "C" , "F"} },
                {"B", new List<string> {"C", "D", "A"}  },
                {"C", new List<string>{"D"} },
                {"D", new List<string>{} },
                {"F", new List<string>{"C", "H", "G", "Y"}  },
                {"H", new List<string>{"D", "U", "Y"}  },
                {"G", new List<string>{"Y"} },
                {"U", new List<string>{"Y", "I", "O", "P"}  },
                {"Y", new List<string>{"I", "B"}  },
                {"O", new List<string>{"P", "A"} },
                {"P", new List<string>{"A", "P"}  }
            };
            ShortestPathSearch(graph, "A");
        }

    }
}
