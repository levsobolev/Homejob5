using System;
namespace fordina.Student
{
    public struct student
    {
        public string Name;
        public string Surname;
        public int Date;
        public string Exam;
        public int Point;
        public student(string surname, string name, int date, string exam, int point)
        {
            Name = name;
            Surname = surname;
            Date = date;
            Exam = exam;
            Point = point;
        }
        public void PrintInfo()
        {
            Console.WriteLine($" {Date} {Exam} {Point}");
        }


    }
}
