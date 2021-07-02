using System;
namespace Academy
{
    class Academy
    {
        public string AcademyName { get; set; }
        public Teacher[] teachers { get; set; }
        public int CountTeacher { get; set; }
        public void AddTeacher(ref Teacher teacher)
        {
            var temp = new Teacher[++CountTeacher];
            if (teachers != null)
            {
                teachers.CopyTo(temp, 0);
            }
            temp[temp.Length - 1] = teacher;
            teachers = temp;
        }
        public void ShowAcademy()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("********Academy********");
            Console.WriteLine($"Academy Name :{AcademyName}");
            foreach (var t in teachers)
            {
                t.ShowTeacher();
            }
            Console.WriteLine();
            Console.ResetColor();
        }
    }
}
