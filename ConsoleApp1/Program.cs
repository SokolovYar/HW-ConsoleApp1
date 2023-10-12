using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Employee
    {
        public Employee() 
        {
            Name = null;
            Post = null;
        }
        public Employee(string name, string post)
        {
            Name = name;
            Post = post;
        }
        public string Name { get; set; }// = "TestName";
        public string Post { get; set; }//= "TestPost";
        public override string ToString()
        {
            return $"Name: {Name ?? "NoName"}, post: {Post ?? "NoData"}";
        }
    }
    internal class Journal
    {
        protected Employee[] EmployeeList;
        public Journal (int count = 0)
        {
            if (count < 1) throw new ArgumentOutOfRangeException("count can`t be less than zero");
            EmployeeList = new Employee[count];
            for (int i = 0; i < count; i++)
                EmployeeList[i] = new Employee();
        }

        public Employee this[int index]
        {
            get
            {
                if (index < 0 || index >= EmployeeList.Length) throw new IndexOutOfRangeException();
                return EmployeeList[index];
            }
            set
            {
                if (index < 0 || index >= EmployeeList.Length) throw new IndexOutOfRangeException();
                EmployeeList[index] = value;
            }
        }
        public void Print()
        {
            foreach (var item in EmployeeList)
                Console.WriteLine(item);
        }
        public static Journal operator + (Journal obj, int i)
        {
            //поскольку массив в С№ имеет фиксированную длину, то для его увеличения создаём новый увеличенной длины
            Employee[] temp = new Employee[obj.EmployeeList.Length + i];
            int k = 0;

            //сперва копируется старый массив
            for (; k < obj.EmployeeList.Length; k++)
                temp[k] = obj.EmployeeList[k];

            //потом добавляется новая частьи из пустых Employee
            for (; k < obj.EmployeeList.Length + i; k++)
                temp[k] = new Employee();
            obj.EmployeeList = temp;
            return obj;
        }
        public static Journal operator +(Journal obj, Employee employee)
        {
            Employee[] temp = new Employee[obj.EmployeeList.Length + 1];
            int k = 0;

            //сперва копируется старый массив
            for (; k < obj.EmployeeList.Length; k++)
                temp[k] = obj.EmployeeList[k];

            //потом добавляется новsый элемент Employee
                temp[k] = employee;
            obj.EmployeeList = temp;
            return obj;
        }
        public static Journal operator - (Journal obj, int i)
        {
            if (i > obj.EmployeeList.Length) throw new IndexOutOfRangeException();
            Employee[] temp = new Employee[obj.EmployeeList.Length - i];
            for (int j = 0; j < temp.Length; j++)
                temp[j] = obj.EmployeeList[j];
            obj.EmployeeList = temp;
            return obj;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj == this) return true;
            if (obj is Journal)
            {
               Journal T = (Journal)obj;
               if (T.EmployeeList.Length != EmployeeList.Length) return false;
               for (int i = 0; i < EmployeeList.Length; i++)
                    if (EmployeeList[i] != T.EmployeeList[i]) return false;
               return true;
                
            }
            return false;
        }
        public static bool operator == (Journal obj1, Journal obj2)
        {
            return obj1.EmployeeList.Length == obj2.EmployeeList.Length;
        }
        public static bool operator !=(Journal obj1, Journal obj2)
        {
            return obj1.EmployeeList.Length != obj2.EmployeeList.Length;
        }
        public static bool operator >(Journal obj1, Journal obj2)
        {
            return obj1.EmployeeList.Length > obj2.EmployeeList.Length;
        }
        public static bool operator <(Journal obj1, Journal obj2)
        {
            return obj1.EmployeeList.Length > obj2.EmployeeList.Length;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Journal MyJournal = new Journal(3);
            MyJournal[0].Name = "Joe";
            MyJournal[0].Post = "CEO";
            MyJournal[1].Name = "Boris";
            MyJournal[1].Post = "Manager";
            MyJournal[2].Name = "Ivan";
            MyJournal[2].Post = "Engeneer";
            MyJournal.Print();
            
            try  //попытка создать ошибочный объект
            {
                Console.WriteLine("\nTrying to call wrong constructor");
                Journal WrongJournal = new Journal(-5);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            Console.WriteLine();

            // проверка перегрузок операторов +
            MyJournal = MyJournal + 2;
            MyJournal = MyJournal + new Employee("Oleg", "worker");
            MyJournal.Print();
            Console.WriteLine();

            //проверка перегрузки оператора -
            MyJournal = MyJournal - 3;
            MyJournal.Print();
            Console.WriteLine();

            //проверка перегрузки логических операторов
            Journal AnotherJournal = new Journal(3);
            //MyJournal == AnotherJournal ? Console.WriteLine("Two objects consist of the same amount of occurrences") : Console.WriteLine("Two objects is NOT consist of the same amount of occurrences");
            if (MyJournal == AnotherJournal) Console.WriteLine("Two objects consist of the same amount of occurrences");
                else Console.WriteLine("Two objectsis NOT consist of the same amount of occurrences");

        }
    }
}
