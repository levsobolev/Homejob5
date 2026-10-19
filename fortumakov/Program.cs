using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using fortumakov.Month;
using System.Linq;

namespace fortumakov
{
    
    public class Program
    {
        /// <summary>
        /// выводит количество согласных и гласных букв в файле
        /// </summary>
        /// <param name="filePath">путь к файлу</param>
        public static void Task61(string filePath)
        {
            int counter = 0;
            using (StreamReader reader = new StreamReader(filePath))
            {
                int charCode;
                while ((charCode = reader.Read()) != -1)
                {
                    counter++;
                }
            }
            char[] chars = new char[counter];
            using (StreamReader reader = new StreamReader(filePath))
            {
                int index = 0;
                int charCode;
                while ((charCode = reader.Read()) != -1)
                {
                    char symbol = (char)charCode;
                    chars[index++] = symbol;
                }
            }
            var result = HelpTask61(chars);
            Console.WriteLine($"{result.CountConsonants} {result.CountVowels}");
        }

        /// <summary>
        /// печатает количество согласных и гласных в переданном файле
        /// </summary>
        /// <param name="filePath">путь к файлу</param>
        public static void TaskHome61(string filePath)
        {
            List<char> chars = new List<char>();
            using (StreamReader reader = new StreamReader(filePath))
            {
                int charCode;
                while ((charCode = reader.Read()) != -1)
                {
                    char symbol = (char)charCode;
                    chars.Add(symbol);
                }
            }
            char[] charsArray = chars.ToArray();
            var result = HelpTask61(charsArray);
            Console.WriteLine($"{result.CountConsonants} {result.CountVowels}");

        }

        /// <summary>
        /// считает количество согласных и гласных букв
        /// </summary>
        /// <param name="chars">массив символов из файла</param>
        /// <returns>кортеж с значениями</returns>
        public static (int CountConsonants, int CountVowels) HelpTask61(char[] chars)
        {
            int resConsonants = 0;
            int resVowels = 0;

            string consonants = "йцкнгшщзхфвпрълджчсмтьб";
            string vowels = "уеыаоэяиюё";

            foreach (char i in chars)
            {
                foreach (char j in consonants)
                {
                    if (char.ToLower(i) == j)
                    {
                        resConsonants++;
                        break;
                    }

                }
            }
            foreach (char i in chars)
            {
                foreach (char j in vowels)
                {
                    if (i == j)
                    {
                        resVowels++;
                        break;
                    }
                }
            }

            return (resConsonants, resVowels);
        }

        /// <summary>
        /// считает произведение двух матриц
        /// </summary>
        /// <returns>итоговую матрицу, если введены матрицы, которые нельзя перемножить, то выводится первая матрица</returns>
        public static int[,] Task62()
        {
            int[,] matrix1 = HelpTask62();
            int[,] matrix2 = HelpTask62();

            try
            {
                int rows1 = matrix1.GetLength(0);
                int rows2 = matrix2.GetLength(0);
                int cols1 = matrix1.GetLength(1);
                int cols2 = matrix2.GetLength(1);

                if (cols1 != rows2)
                {
                    throw new Exception("Количество столбцов 1 матрицы и количество строк 2 матрицы не совпадают");
                }

                int[,] result = new int[rows1, cols2];

                for (int i = 0; i < rows1; i++)
                {
                    for (int j = 0; j < cols2; j++)
                    {
                        for (int k = 0; k < cols1; k++)
                        {
                            result[i,j] = matrix1[i,k] * matrix2[k,j];
                        }
                    }
                }
                return result;

            }
            catch(Exception ex) 
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
                return matrix1;
            }
        }

        /// <summary>
        /// создает матрицу заданных размеров 
        /// </summary>
        /// <returns>итоговая матрица</returns>
        public static int[,] HelpTask62()
        {
            Console.WriteLine("Введите количество строк матрицы:");
            bool isStringLength = int.TryParse(Console.ReadLine(), out int stringLength);

            Console.WriteLine("Введите количество столбцов матрицы:");
            bool isColumnLength = int.TryParse(Console.ReadLine(), out int columnLength);

            Random rand = new Random();
            int[,] matrix = new int[stringLength, columnLength];
            for (int i = 0; i < stringLength; i++ )
            {
                for (int j = 0; j < columnLength; j++ )
                {
                    matrix[i, j] = rand.Next();
                }
            }

            return matrix;
            
        }

        /// <summary>
        /// печатает матрицу
        /// </summary>
        /// <param name="matrix">матрица, которую нужно напечатать </param>
        public static void MatrixPrint(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// считает средние температуры в каждом месяце и записывает результаты в массив, сортирует массив по возрастанию
        /// </summary>
        /// <returns>отсортированный массив средних температур</returns>
        public static float[] Task63()
        {
            Random rand = new Random();
            int[,] temperature = new int[12, 30]; 

            for (int i = 0;i < temperature.GetLength(0);i++)
            {
                for (int j = 0;j < temperature.GetLength(1);j++)
                {
                    temperature[i,j] = rand.Next(-72, 66);
                }
            }

            float[] result = new float[12];

            for (int i = 0; i < temperature.GetLength(0); i++)
            {
                int currentValue = 0;
                for (int j = 0; j < temperature.GetLength(1); j++)
                {
                    currentValue += temperature[i, j];
                }

                float averageTemperature = currentValue / temperature.GetLength(0);
                result[i] = averageTemperature;
                
            }
            Array.Sort(result);
            return result;
        }

        /// <summary>
        /// считает средние температуры за месяц 
        /// </summary>
        /// <returns>словарь средних температур за каждый месяц</returns>
        public static Dictionary<month, float> TaskHome63()
        {
            Random rand = new Random();
            Dictionary<month, int[]> months = new Dictionary<month, int[]>();
            
            for(int i = 0; i < 12; i++)
            {
                int[] month = new int[30];
                for (int j = 0; j < 30; j++)
                {
                    month[j] = rand.Next(-72, 66);
                }
                months.Add((month)i, month);
            }

            Dictionary<month, float> result = new Dictionary<month, float>();
            int index = 0;
            foreach(var i  in months.Values)
            {

                int sum = 0;
                for (int j = 0;j < 30; j++)
                {
                    sum += i[j];
                }
                float averageTemperature = sum / 30;
                result[(month)index] = averageTemperature;
                index ++;
            }

            return result;
        }

        /// <summary>
        /// перемножает 2 произвольные матрицы
        /// </summary>
        /// <returns>матрица, полученная произведением, если матрицы нельзя перемножить, то возвращает первую матрицу</returns>
        public static LinkedList<LinkedList<int>> TaskHome62()
        {
            LinkedList<LinkedList<int>> matrix1 = HelpTaskHome62();
            LinkedList<LinkedList<int>> matrix2 = HelpTaskHome62();

            int row1 = matrix1.Count;
            int row2 = matrix2.Count;
            int column1 = matrix1.ElementAt(0).Count;
            int column2 = matrix2.ElementAt(0).Count;

            try
            {
                if (column1 != row2)
                {
                    throw new Exception("Количество столбцов первой матрицы не совпадает с количеством строк второй");
                }

                LinkedList<LinkedList<int>> result = new LinkedList<LinkedList<int>>();

                for (int i = 0; i < row1; i++)
                {
                    LinkedList<int> currentList = new LinkedList<int>();
                    
                    for (int j = 0; j < column2; j++)
                    {
                        int sum = 0;
                        for (int k = 0; k < column1; k++)
                        {
                            int element1 = matrix1.ElementAt(i).ElementAt(k);
                            int element2 = matrix2.ElementAt(k).ElementAt(j);
                            sum += element1 * element2;
                        }
                        currentList.AddLast(sum);
                    }
                    result.AddLast(currentList);
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
                return matrix1;
            }

        }

        /// <summary>
        /// печатает матрицу, переданную в метод
        /// </summary>
        /// <param name="matrix">матрица, которую нужно напечатать</param>
        public static void PrintMatrixLinked(LinkedList<LinkedList<int>> matrix)
        {
            foreach(var i  in matrix)
            {
                foreach( var j in i)
                {
                    Console.Write(j + "\t");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Создает матрицу указанных размеров из рандомных элементов
        /// </summary>
        /// <returns>матрица из произвольных элементов</returns>
        public static LinkedList<LinkedList<int>> HelpTaskHome62()
        {
            Random rand = new Random();
            Console.WriteLine("Введите количество строк матрицы:");
            bool isRow = int.TryParse(Console.ReadLine(), out int row);

            Console.WriteLine("Введите количество столбцов матрицы:");
            bool isColumn = int.TryParse(Console.ReadLine(), out int column);

            LinkedList < LinkedList<int> > result = new LinkedList<LinkedList<int>>();

            for (int i = 0; i < row; i++)
            {
                LinkedList<int> currentList = new LinkedList<int>();
                
                for (int j = 0; j < column; j++)
                {
                    currentList.AddLast(rand.Next());
                }

                result.AddLast(currentList);
                
            }
            return result;
        }
        public static void Main(string[] args)
        {
            //Номер 6.1 из лабораторной работы
            Console.WriteLine("Упражнение 6.1 Написать программу, которая вычисляет число гласных и согласных букв в\r\n" +
                "файле. Имя файла передавать как аргумент в функцию Main. Содержимое текстового файла\r\n" +
                "заносится в массив символов. Количество гласных и согласных букв определяется проходом\r\n" +
                "по массиву. Предусмотреть метод, входным параметром которого является массив\r\n" +
                "символов. Метод вычисляет количество гласных и согласных букв.");

            Task61("..\\..\\for.txt");

            //Номер 6.2 из лабораторной работы
            Console.WriteLine("Упражнение 6.2 Написать программу, реализующую умножению двух матриц, заданных\r\n" +
                "в виде двумерного массива. В программе предусмотреть два метода: метод печати\r\n" +
                "матрицы, метод умножения матриц (на вход две матрицы, возвращаемое значение –\r\nматрица).");

            int[,] matrix = Task62();
            
            MatrixPrint(matrix);

            //Номер 6.3 из лабораторной работы
            Console.WriteLine("Упражнение 6.3 Написать программу, вычисляющую среднюю температуру за год. Создать\r\n" +
                "двумерный рандомный массив temperature[12,30], в котором будет храниться температура\r\n" +
                "для каждого дня месяца (предполагается, что в каждом месяце 30 дней). Сгенерировать\r\n" +
                "значения температур случайным образом. Для каждого месяца распечатать среднюю\r\n" +
                "температуру. Для этого написать метод, который по массиву temperature [12,30] для каждого\r\n" +
                "месяца вычисляет среднюю температуру в нем, и в качестве результата возвращает массив\r\n" +
                "средних температур. Полученный массив средних температур отсортировать по\r\nвозрастанию.");

            float[] temperature1 = Task63();
            for (int i = 0; i < temperature1.Length; i++)
            {
                Console.Write(temperature1[i] + "\t");
            }
            Console.WriteLine();

            //Номер 6.1 из дз 
            Console.WriteLine("Домашнее задание 6.1 Упражнение 6.1 выполнить с помощью коллекции List<T>.");

            TaskHome61("..\\..\\for.txt");

            //Номер 6.2 из дз
            Console.WriteLine("Домашнее задание 6.2 Упражнение 6.2 выполнить с помощью коллекций\r\nLinkedList<LinkedList<T>>.");

            LinkedList<LinkedList<int>> matrix2 = TaskHome62();
            PrintMatrixLinked(matrix2);

            //Номер 6.3 из дз
            Console.WriteLine("Домашнее задание 6.3 Написать программу для упражнения 6.3, использовав класс\r\n" +
                "Dictionary<TKey, TValue>. В качестве ключей выбрать строки – названия месяцев, а в\r\n" +
                "качестве значений – массив значений температур по дням.");

            Dictionary<month, float> temperature2 = TaskHome63();
            foreach(var i in temperature2)
            {
                Console.WriteLine($"{i.Key}: {i.Value}");
            }

        }
    }
}
