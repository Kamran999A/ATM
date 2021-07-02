using System;
namespace Academy
{
    class Group
    {
        public string GroupName { get; set; } = string.Empty;
        public string Tutor { get; set; } = string.Empty;
        public Student[] students { get; set; }
        public int CountStudent { get; set; } = 0;
        public void AddStudent(ref Student student)
        {
            var temp = new Student[++CountStudent];
            if (students != null)
            {
                students.CopyTo(temp, 0);
            }
            temp[temp.Length - 1] = student;
            students = temp;
        }
        public void ShowGroup()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("+++++Group+++++");
            Console.WriteLine($"Group Name : {GroupName}");
            Console.WriteLine($"Group Tutor : {Tutor}");
            foreach (var s in students)
            {
                s.ShowStudent();
            }
            Console.WriteLine();
            Console.ResetColor();
        }
    }
}
