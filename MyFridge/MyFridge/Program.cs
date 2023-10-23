namespace MyFridge
{
    internal class Program
    {
        private static void PrintByAvailabilityFrige(List<Refrigerator>refrigerators)
        {
            SortByAvailability(refrigerators);
            foreach (var item in refrigerators)
            {
                Console.WriteLine(item.ToString());
            }
        }
        private static void PrintByAvailabilityShelfe(Refrigerator refrigerator)
        {
            foreach (var item in refrigerator.SortByAvailability())
            {
                Console.WriteLine(item.ToString());
            }
        }
        private static void PrintEExpiryDate(Refrigerator refrigerator)
        {
            foreach (var item in refrigerator.SortOfExpiryDate())
            {
                Console.WriteLine(item.ToString());
            }
        }
        public static void WantToEat(Refrigerator refrigerator)
        {
            try
            {
            Console.WriteLine("what do you want to eat? enter: kind(Food Drink) & kosher(Meaty Dairy Fur )");
            KindItem kind = (KindItem)Enum.Parse(typeof(KindItem), Console.ReadLine()??"ther is not type of kind");
            Kosher kosher = (Kosher)Enum.Parse(typeof(Kosher), Console.ReadLine() ?? "it is not kosher ");
                foreach (var item in refrigerator.WantToEat(kosher, kind))
                {
                    Console.WriteLine(item.ToString());
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message+ "Try again - ");
            }
           

        }
        public static void Cleaning (Refrigerator refrigerator)
        {
            try
            {
                foreach (var item in refrigerator.SortOfExpiryDate())
                {
                    if (item.ExpiryDate < DateTime.Now) Console.WriteLine("thrown out:");
                    Console.WriteLine(item.Name);
                }
                refrigerator.Cleaning();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "Try again - ");

            }
        }
        public static void InputItem(Refrigerator refrigerator)
        {
            Console.WriteLine("get item: get name,kind(Food Drink),kosher(Meaty Dairy Fur ) expiryDate,space");
            try
            {
                string name = Console.ReadLine() ?? "it is not null ", s = (Console.ReadLine() ?? "not null"), koshrs = Console.ReadLine() ?? "not null", expiryDates = Console.ReadLine() ?? "not null", spaces = Console.ReadLine() ?? "not null";
                KindItem kind = (KindItem)Enum.Parse(typeof(KindItem), s);
                Kosher kosher = (Kosher)Enum.Parse(typeof(Kosher), koshrs);
                DateTime expiryDate = DateTime.Parse(expiryDates);
                int space = int.Parse(spaces);
                var inputItem = new Item(name, kind, kosher, expiryDate, space);
                refrigerator.InsertingItem(inputItem);
                Console.WriteLine("The food entered successfully");
            }
             catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "Try again - ");

            }
        }
        public static void HoutItem(Refrigerator refrigerator)
        {
            try
            {
                Console.WriteLine("Give an ID number of an item to take out of the fridge");
                int id = int.Parse(Console.ReadLine() ?? "it is not null ");
                Console.WriteLine(refrigerator.TookItem(id));
                Console.WriteLine("The food came out successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "Try again - ");

            }

        }
            public static void SortByAvailability(List<Refrigerator> refrigerators)
        {
            refrigerators.Sort((p1, p2) => p1.GetFreeSpace().CompareTo(p2.GetFreeSpace()));
            
        }
        static void Main(string[] args)
        {
                List<Item> items = new List<Item>();
            items.Add(new Item("milk", KindItem.Drink, Kosher.Dairy, DateTime.Now, 11));
            items.Add(new Item("pizza", KindItem.Food, Kosher.Dairy, DateTime.Now.AddDays(11), 11));
            items.Add(new Item("woter", KindItem.Drink, Kosher.Fur, DateTime.Now.AddDays(80), 4));
            List<Shelf> shelf = new List<Shelf>();
            shelf.Add(new Shelf(1, items));
            shelf.Add(new Shelf(2, items));
            shelf.Add(new Shelf(3, items));
            shelf.Add(new Shelf(4, items));
            Refrigerator refrigerator = new("amkor", "blake", shelf);
            List<Item> items2 = new List<Item>();
            items2.Add(new Item("milk", KindItem.Drink, Kosher.Dairy, DateTime.Now, 78));
            items2.Add(new Item("pizza", KindItem.Food, Kosher.Dairy, DateTime.Now.AddDays(11), 11));
            items2.Add(new Item("woter", KindItem.Drink, Kosher.Fur, DateTime.Now.AddDays(80), 4));
            List<Shelf> shelf2 = new List<Shelf>();
            shelf2.Add(new Shelf(1, items2));
            shelf2.Add(new Shelf(2, items));
            shelf2.Add(new Shelf(3, items));
            shelf2.Add(new Shelf(4, items2));
            Refrigerator refrigerator2 = new("sumsong", "silver", shelf2);
            List<Refrigerator> refrigerators = new();
            refrigerators.Add(refrigerator2);
            refrigerators.Add(refrigerator);

            Console.WriteLine("Press 1: the program will print all the items on the refrigerator and all its contents.\r\nClick 2: the program will print how much space is left in the fridge\r\nPress 3: The program will allow the user to put an item in the fridge.\r\nPress 4: The program will allow the user to remove an item from the refrigerator.\r\nPress 5: the program will clean the refrigerator and print all the checked items to the user.\r\nPress 6: the program will ask the user &quot;What do I want to eat?&quot; and bring the function to bring a product.\r\nClick 7: the program will print all the products sorted by their expiration date.\r\nPress 8: the program will print all the shelves arranged according to the free space left on them.\r\nPress 9: the program will print all the refrigerators arranged according to the free space left in them.\r\nClick 10: The program will prepare the refrigerator for shopping\r\nPress 100: system shutdown.");
            string input = "0";
            while (input != "100")
            {
                Console.WriteLine("press a number from 1-10");
                input = Console.ReadLine()??"not null";
                switch (input)
                {
                    case "1":
                        Console.WriteLine(refrigerator.ToString()); break;
                    case "2":
                        Console.WriteLine(refrigerator.GetFreeSpace()); break;
                    case "3":
                        InputItem(refrigerator); break;
                    case "4":
                        HoutItem(refrigerator); break;
                    case "5":
                        Cleaning(refrigerator); break;
                        case "6":
                        WantToEat(refrigerator); break;
                    case "7":
                        PrintEExpiryDate(refrigerator);break;
                    case "8":
                        PrintByAvailabilityShelfe(refrigerator);break;
                        case "9":
                        PrintByAvailabilityFrige(refrigerators);break;
                        case "10":
                        refrigerator.GoingShopping();break;
                    case "100":
                        Console.WriteLine("good by...");break;
                    default:
                        Console.WriteLine("Invalid input");break;







                }
            }
        }

       
    }
}
