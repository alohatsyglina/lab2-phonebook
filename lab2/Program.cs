using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace lab2
{
    /// <summary>
    /// абстрактный класс, обозначающий запись в телефонном справочнике
    /// </summary>
    public abstract class PhoneBook
    {
        /// <summary>
        /// Поле имени
        /// </summary>
        public string name;
        /// <summary>
        /// Поле адреса
        /// </summary>
        public string address;
        /// <summary>
        /// Телефонный номер
        /// </summary>
        public string phoneNumber;

        /// <summary>
        /// Метод, выводящий на экран информацию о записи
        /// </summary>
        public abstract void printInfo();

        /// <summary>
        /// Проверка записи на соответствие введенному значению
        /// </summary>
        /// <param name="searchingName">Значение фамилии или названия организации, с которым происходит сравнение
        /// </param>
        public void Search(string searchingName)
        {
            Trace.WriteLine("Проверка на соответствие");
            if (this.name.Contains(searchingName)) this.printInfo();            
        }
    }

    /// <summary>
    /// Персона, характеризуется именем, адресом и номером телефона
    /// </summary>
    internal class Person : PhoneBook
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="address">Адрес</param>
        /// <param name="phoneNumber">Телефон</param>
        public Person(string name, string address, string phoneNumber)
        {
            this.name = name;
            this.address = address;
            this.phoneNumber = phoneNumber;
            Trace.WriteLine("Создан экземпляр класса Person");
        }

        /// <summary>
        /// Вывод записи
        /// </summary>
        public override void printInfo()
        {
            Console.WriteLine($"Имя: {name}\nАдрес: {address}\nТелефон: {phoneNumber}\n");
        }

    }

    /// <summary>
    /// Организация
    /// </summary>
    internal class Organization : PhoneBook
    {
        /// <summary>
        /// Факс
        /// </summary>
        public string fax;
        /// <summary>
        /// Контактное лицо
        /// </summary>
        public string contact;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="address">Адрес</param>
        /// <param name="phoneNumber">Номер телефона</param>
        /// <param name="fax">Факс</param>
        /// <param name="contact">Контактное лицо</param>
        public Organization(string name, string address, string phoneNumber, string fax, string contact)
        {
            this.name = name;
            this.address = address;
            this.phoneNumber = phoneNumber;
            this.fax = fax;
            this.contact = contact;
            Trace.WriteLine("Создан экземпляр класса Organization");
        }

        public override void printInfo()
        {
            Console.WriteLine($"Название: {name}\nАдрес: {address}\nТелефон: {phoneNumber}\nФакс: {fax}\nКонтактное лицо: {contact}\n");
        }

        /// <summary>
        /// Друг
        /// </summary>
        internal class Friend : PhoneBook
        {
            /// <summary>
            /// Дата рождения
            /// </summary>
            public string birthDate;

            /// <summary>
            /// Конструктор класса
            /// </summary>
            /// <param name="name">Имя</param>
            /// <param name="address">Адрес</param>
            /// <param name="phoneNumber">Номер телефона</param>
            /// <param name="birthDate">Дата рождения</param>
            public Friend(string name,string address, string phoneNumber,string birthDate)
            {
                this.name = name;
                this.address = address;
                this.phoneNumber = phoneNumber;
                this.birthDate = birthDate;
                Trace.WriteLine("Создан экземпляр класса Friend");
            }

            public override void printInfo()
            {
                Console.WriteLine($"Имя: {name}\nАдрес: {address}\nТелефон: {phoneNumber}\nДата рождения: {birthDate}\n");
            }
        }
        class Program
        {
            /// <summary>
            /// Преобразование строки из файла в экземпляр класса
            /// </summary>
            /// <param name="line">Преобразуемая строка</param>
            /// <returns></returns>
            static PhoneBook parseNote(string line)
            {
                string[] element = line.Split(';');
                switch (element[0])
                {
                    case "person":
                        return new Person(element[1], element[2], element[3]);
                    case "organization":
                        return new Organization(element[1], element[2], element[3], element[4], element[5]);
                    case "friend":
                        return new Friend(element[1], element[2], element[3], element[4]);                    
                    default: return new Person(" "," "," ");
                }
            }

            /// <summary>
            /// Построковое чтение из файла
            /// </summary>
            /// <param name="fileName">Название файла для чтения</param>
            /// <returns></returns>
            static PhoneBook[] readFromFile(string fileName)
            {             
                System.IO.StreamReader file = new System.IO.StreamReader("input.txt");
                int n = Convert.ToInt32(file.ReadLine());
                PhoneBook[] phoneBookDataBase = new PhoneBook[n];
                for(int i = 0; i < n; i++)
                {
                    phoneBookDataBase[i] = parseNote(file.ReadLine());
                }
                return phoneBookDataBase;
            }

            static void Main(string[] args)
            {
                PhoneBook[] phoneBase = readFromFile("input.txt");
                Console.WriteLine("Телефонный справочник: ");
                foreach (PhoneBook note in phoneBase)
                {
                    note.printInfo();
                }

                Console.Write("Для поиска записи введите фамилию человека или название организации: ");
                string nameForSearch = Console.ReadLine();
                foreach(PhoneBook note in phoneBase)
                {
                    note.Search(nameForSearch);
                }
                Console.ReadKey();
            }
        }
    }
}
