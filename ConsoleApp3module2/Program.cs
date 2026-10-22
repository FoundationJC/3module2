using System;
using System.Collections;      // для ArrayList
using System.Collections.Generic; // для List<T>
using ClassLibrary3module2;

namespace ConsoleApp3module2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8; // для українських символів

            // ========== ЗАВДАННЯ 1-2: МАСИВ, КОЛЕКЦІЇ ==========

            // 1. Створення 6 об'єктів CryptoString (одні й ті самі для всіх структур!)
            Strings[] stringsArray = new Strings[6]
            {
                new Strings("Hello", 3, true),
                new Strings("World", 1, false),
                new Strings("CSharp", 2, true),
                new Strings("Programming", 4, true),
                new Strings("Lab", 5, false),
                new Strings("Work", 2, false)
            };

            // Демонстрація роботи з масивом
            Console.WriteLine("=== РОБОТА З МАСИВОМ ===");
            foreach (var s in stringsArray) s.Print();

            // Оновлення елемента масиву (індексатор)
            stringsArray[0].Encrypt(); // шифруємо перший рядок
            Console.WriteLine("\nПісля шифрування першого елемента:");
            stringsArray[0].Print();

            Console.WriteLine("\nДешифруємо назад:");
            stringsArray[0].Decrypt();
            stringsArray[0].Print(); // знову "Hello"


            // Пошук у масиві (лінійний пошук)
            Console.WriteLine("\nПошук рядка з довжиною 3:");
            foreach (var s in stringsArray)
                if (s.Length == 3) { Console.WriteLine("Знайдено:"); s.Print(); break; }

            // 2. Узагальнена колекція List<T>
            Console.WriteLine("\n=== РОБОТА З List<T> ===");
            List<Strings> list = new List<Strings>(stringsArray); // копіюємо з масиву

            // Додавання
            list.Add(new Strings("NewItem", 1, true));
            Console.WriteLine($"Кількість елементів у List: {list.Count}");

            // Видалення (видаляємо останній)
            list.RemoveAt(list.Count - 1);

            // Оновлення (змінюємо перший елемент)
            list[0] = new Strings("Updated", 2, true);
            list[0].Print();

            // Пошук через предикат (лямбда-вираз)
            var found = list.Find(s => s.Value.Contains("Sharp"));
            if (found != null) { Console.WriteLine("Знайдено через Find:"); found.Print(); }

            // Прохід foreach
            Console.WriteLine("\nВсі елементи List:");
            foreach (var s in list) s.Print();

            // 3. Неузагальнена колекція ArrayList
            Console.WriteLine("\n=== РОБОТА З ArrayList ===");
            ArrayList arrayList = new ArrayList();

            // Boxing для value types, але CryptoString — клас, тобто reference type
            foreach (var s in stringsArray) arrayList.Add(s);

            // Додавання
            arrayList.Add(new Strings("ArrayListItem", 3, true));

            // Видалення
            arrayList.RemoveAt(0); // видаляємо перший

            // Оновлення з безпечним приведенням
            if (arrayList[0] is Strings first)   // is — перевіряє тип і приводить одночасно
            {
                first.Encrypt();
            }

            // Пошук (потрібен цикл з приведенням)
            Console.WriteLine("\nПошук у ArrayList:");
            foreach (Strings s in arrayList) // явне приведення у foreach
            {
                if (s.Length > 5) { s.Print(); break; }
            }

            // Оновлення з безпечним приведенням
            if (arrayList[0] is Strings second)   // is — перевіряє тип і приводить одночасно
            {
                second.Decrypt();
            }

            // Пошук (потрібен цикл з приведенням)
            Console.WriteLine("\nПошук у ArrayList(Дешифрування):");
            foreach (Strings s in arrayList) // явне приведення у foreach
            {
                if (s.Length > 5) { s.Print(); break; }
            }

            // Порівняння: спробуємо додати "неправильний" тип у ArrayList
            arrayList.Add("Це просто рядок, не Strings"); // ArrayList дозволяє!
            Console.WriteLine($"\nArrayList містить {arrayList.Count} елементів (включно зі строкою)");
            // А List<CryptoString> НЕ дозволив би це зробити — це головна відмінність!

            // Видаляємо строку, щоб не ламати дерево
            arrayList.RemoveAt(arrayList.Count - 1);

            // ========== ЗАВДАННЯ 3-4: БІНАРНЕ ДЕРЕВО + ІТЕРАТОР ==========

            Console.WriteLine("\n=== БІНАРНЕ ДЕРЕВО (preorder) ===");

            // Створюємо чисті об'єкти спеціально для дерева (6 штук)
            var treeItems = new List<Strings>
            {
                new Strings("Hello", 3, true),
                new Strings("World", 3, true),
                new Strings("CSharp", 3, true),
                new Strings("Programming", 3, true),
                new Strings("Lab", 3, true),
                new Strings("Work", 3, true)
            };
      

            
            
            // Створюємо дерево з компаратором
            var tree = new BinaryTree<Strings>(new StringsComparer());

            // Додаємо 6 об'єктів (ті самі, що й у масиві)
            foreach (var s in treeItems) tree.Add(s);

            // Демонстрація обходу дерева через foreach (використовує наш ітератор!)
            Console.WriteLine("Обхід дерева (прямий порядок):");
            foreach (var s in tree)
            {
                s.Print();
            }

            // Додатково: зашифруємо перший елемент дерева, щоб показати, що методи працюють
            Console.WriteLine("\nШифруємо 'Hello' у дереві:");
            treeItems[0].Encrypt();  // тепер Hello -> Khoor
            treeItems[0].Print();    // виведе "Khoor"

            Console.WriteLine("\nДешифруємо назад:");
            treeItems[0].Decrypt();
            treeItems[0].Print(); // знову "Hello"


            // Додатково: сортування List через IComparable
            Console.WriteLine("\nСортування List за довжиною (IComparable):");
            list.Sort(); // використовує CompareTo з Strings
            foreach (var s in list)
            {
                s.Print();
            }

            Console.WriteLine("\nСортування List через IComparer:");
            list.Sort(new StringsComparer()); // той самий порядок за довжиною


            Console.WriteLine("\nГотово! Натисніть будь-яку клавішу...");
            Console.ReadKey();
        }
    }
}
