using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dict

{ // Завдання Перекладач 
    class Diction
    {
        string rus;
        string ukr;
        public string Rus
        {
            get
            {
                return rus;
            }
            set
            {
                rus = value;
            }
        }
        public string Ukr
        {
            get
            {
                return ukr;
            }
            set
            {
                ukr = value;
            }
        }
        public Diction(string Rus1, string ukr1)
        {
            Rus = Rus1;
            ukr = ukr1;
        }

        public override string ToString()
        {
            return string.Format("Переклад:" + ukr);
        }
    }
    class Program

    {
        static void Nahod(Diction d1, string s1)
        {
            if (s1 == d1.Rus)
                Console.WriteLine(d1.ToString()); // Зчитуємо строку
        }
        static void Main(string[] args)
        {
            List<Diction> Dict = new List<Diction>(); // Створюємо так називаємий метод куди зберігаємо слова.
            string s1;
            string s2;
            Console.WriteLine(" \t Увага ![Введiть у консоль 'q' для виходу з режиму додавання слiв]\n\n");
            // Сворюємо цикл запису слів, в якому призначаємо словам переклад
            do  
            {
                Console.WriteLine("\t\t\t Введiть росiйське слово");
                s1 = Console.ReadLine();
                if (s1 == "q") break;
                Console.WriteLine("\t\t\t Введiть українське значенння слова");
                s2 = Console.ReadLine();
                Dict.Add(new Diction(s1, s2));
            } while (s1 != "");

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("\t\t\t Введiть слово росiйською яке хочете перекласти");
            string s3 = Console.ReadLine();
            foreach (Diction dic in Dict)
            {
                Nahod(dic, s3);
            }
            Console.ReadKey();
        }


    }
}