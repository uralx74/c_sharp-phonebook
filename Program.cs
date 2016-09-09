using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook
{


    /*
     * Определение класса для хранения элемента справочника
     */
    class PhoneBookItem
    {

        public static bool operator < (PhoneBookItem leftItem, PhoneBookItem rightItem)
        {

            return String.Compare(leftItem.name, rightItem.name) == -1;
        }
        public static bool operator > (PhoneBookItem leftItem, PhoneBookItem rightItem)
        {
            return String.Compare(leftItem.name, rightItem.name) == 1;
        }

        public PhoneBookItem(string name, string phone)
        {
            this.name = name;
            this.phone = phone;
        }

        public string name;
        public string phone;
    }


    /*
     * Реализация Reciever из паттерна Command
     */
    class ProcessingUnit
    {
        private List<PhoneBookItem> phoneBookItems = new List<PhoneBookItem>();
        public void Add(string name, string phone)
        {
            phoneBookItems.Add(new PhoneBookItem(name, phone));
        }
        public void List()
        {
            List<PhoneBookItem> phoneBookItemsSorted = phoneBookItems;


            int n = phoneBookItems.Count;


            bool swaped = false;
            
            do
            {
                swaped = false;
                for (int i = 1; i < n; i++)
                {
                    PhoneBookItem tmpItem;
                    if (phoneBookItems[i - 1] > phoneBookItems[i])
                    {
                        //Swap<int>(ref a, ref b);
                        swaped = true;
                        tmpItem = phoneBookItems[i - 1];
                        phoneBookItems[i - 1] = phoneBookItems[i];
                        phoneBookItems[i] = tmpItem;
                    }
                }
            } while (swaped == true);
            
             
            int index = 0;
            foreach (PhoneBookItem item in phoneBookItems)
            {
                Console.WriteLine("Abonent #{0} ", ++index);
                Console.WriteLine("Name: {0} ", item.name);
                Console.WriteLine("Phone: {0} ", item.phone);
                Console.WriteLine("");
            }
        }
    }

    /*
    * Реализация базового класса Command паттерна Command
    */
    abstract class Command
    {
        public ProcessingUnit processingUnit;
        public abstract void Execute();
    }

    /*
     * Реализация класса ConcreteCommand паттерна Command
     */
    class AddCommand : Command
    {
        private string name;
        private string phone;
        public AddCommand(ProcessingUnit processingUnit, string name, string phone)
        {
            this.name = name;
            this.phone = phone;
            this.processingUnit = processingUnit;
        }
        public override void Execute()
        {
            this.processingUnit.Add(name, phone);
        }
    }

    /*
    * Реализация класса ConcreteCommand паттерна Command
    */
    class ListCommand : Command
    {
        public ListCommand(ProcessingUnit processingUnit)
        {
            this.processingUnit = processingUnit;
        }
        public override void Execute()
        {
            this.processingUnit.List();
        }
    }

    class Program
    {

        public static void ShowHelp()
        {
            Console.Clear();
            Console.WriteLine("Список доступных комманд:\n" +
                "  add <name> <phone>\t Добавляет нового абонента\n" +
                "  list\tВыводит список абонентов\n" +
                "  help\tВыводит список команд\n" +
                "  exit\tВыход из программы"
                );
        }

        static void Main(string[] args)
        {

            ProcessingUnit processingUnit = new ProcessingUnit();

            /* Тест паттерна Command
            Command command = new AddCommand(processingUnit, "snegana", "8-351-777777");
            command.Execute();

            Command command1 = new AddCommand(processingUnit, "angela", "8-909-777-8888");
            command1.Execute();

            Command command2 = new AddCommand(processingUnit, "boris", "1");
            command2.Execute();

            Command command3 = new AddCommand(processingUnit, "danny", "3");
            command3.Execute();

            Command commandList = new ListCommand(processingUnit);
            commandList.Execute();
            */

            ShowHelp();

            bool exitFlag = false;
            do
            {
                Console.Write("> ");

                string userCommand = Console.ReadLine();
                String[] commandParameters = userCommand.Split(' ');
                if (commandParameters.Count() < 0)
                {
                    continue;
                }

                switch (commandParameters[0])
                {
                    case "list":
                        {
                            Command command = new ListCommand(processingUnit);
                            command.Execute();
                            break;
                        }
                    case "add":
                        {
                            if (commandParameters.Count() == 3)
                            {
                                
                                Command command = new AddCommand(processingUnit, commandParameters[1], commandParameters[2]);
                                command.Execute();
                            } else
                            {
                                Console.WriteLine("Неверный формат команды add!");
                            }
                            break;
                        }
                    case "exit":
                        {
                            exitFlag = true;
                            break;
                        }
                    case "help":
                        {
                            ShowHelp();
                            break;
                        }

                }
            } while (!exitFlag);
            //} while (Console.ReadKey().Key != ConsoleKey.Escape);
        }
    }
}
