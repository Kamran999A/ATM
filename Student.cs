using System;
namespace Academy
{
    class Student
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public Exam exam { get; set; }
        public void ShowStudent()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("+++++Student+++++");
            Console.WriteLine($"ID : {ID} ");
            Console.WriteLine($"Name : {Name} ");
            Console.WriteLine($"Surname : {Surname} ");
            Console.WriteLine($"Age : {Age} ");
            exam.ShowExam();
            Console.WriteLine();
            Console.ResetColor();
        }
    }
}
