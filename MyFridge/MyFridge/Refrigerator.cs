using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MyFridge
{
    internal class Refrigerator
    {

        public static int UniqueId { get; set; } = 1;
        public int Id { get; } = 134;
        public string Model { get; set; }
        public string Color { get; set; }
        public List<Shelf> Shelves { get; set; }
        public int AmountOfShelves { get; set; }



        public Refrigerator(string model, string color, List<Shelf> shelves)
        {
            Id = UniqueId++;
            Model = model;
            Color = color;
            if (shelves == null) throw new Exception("shelves null");
            foreach (var shelf in shelves)
            {
               if (shelf==null) throw new Exception("shelf null");
            }
            Shelves = shelves;
            AmountOfShelves = Shelves.Count;
        }



        public override string ToString()
        {
            string stringSelf="";
            foreach (var shelf in Shelves)
            {
                stringSelf = stringSelf.Insert(stringSelf.Length, shelf.ToString() + "\n");
            }
            return(
            "id:" + Id + " model: " + Model + " color: " + Color + " amount of shelves: " + AmountOfShelves +
             " shelves: " + stringSelf);
        }

        public int GetFreeSpace() { 
            int freeSpace = 0;
            foreach (var shelf in Shelves)
            {
                freeSpace += shelf.SpaceAvailable;
            }
            return freeSpace;
        }

        public void InsertingItem(Item item)
        {
            if (item == null) throw new Exception("invalid item");
            if (GetFreeSpace()<item.Space) throw new Exception("There is not enough space");
            foreach (var shelf in Shelves)
            {
                if (shelf.SpaceAvailable >= item.Space)
                {
                    shelf.InsertingItem(item);
                    return;
                }
            }
                throw new Exception("There is not enough space");

            
  
        }


        public Item TookItem(int itemId)
        {
            Item? empty;
            foreach (var shelf in Shelves)
            {
                
                
                    empty = shelf.TookItem(itemId);
                    if (empty != null) return empty;
                
                                   

            }
             throw new Exception("No item if such ID number");
           
        }

        public void Cleaning()
        {
            foreach (Shelf shelf in Shelves)
            {
                shelf.CleanExpiredItems();
            }
        }

        public List<Item> WantToEat(Kosher kosher, KindItem kind)
        {
            List<Item> items = new();
            foreach (Shelf shelf in Shelves)
            {
                if (shelf.WantToEat(kosher,kind)!=null)
                {
                    items.AddRange(shelf.WantToEat(kosher, kind));
                }
            }
            if (items.Count == 0) throw new Exception("Unfortunately there is currently no suitable food. ");
            return items;
        }
        public List<Item> SortOfExpiryDate()
        {
            List<Item> items = new List<Item>();
            foreach (var shelf in Shelves)
            {
                items.AddRange(shelf.Items);
            }
            items.Sort((p1, p2) => p1.ExpiryDate.CompareTo(p2.ExpiryDate));
            
            return items;

        }
        public List<Shelf> SortByAvailability()
        {
            List<Shelf> shelves= new();
            shelves.AddRange(Shelves);
            shelves.Sort((p1, p2) => p1.SpaceAvailable.CompareTo(p2.SpaceAvailable));
            return shelves;

        }
        public List<Item> DairyProducts3(ref int freeSpace)
        {
        List <Item> items = new();
            foreach (var shelf in Shelves)
            {
                foreach (var item in shelf.Items)
                {
                    if (item.Kosher == Kosher.Dairy && item.ExpiryDate.AddDays(-3) <= DateTime.Today)
                    {
                        freeSpace+=item.Space;
                        items.Add(item);
                    }
                }
            }
            return items;
        }
        public void GoingShopping()
           
        {
            const int FreeSpace = 29;
            if (this.GetFreeSpace() >= FreeSpace)
            {
                Console.WriteLine("go to shopping");
                return;
            }
            this.Cleaning();
            if (this.GetFreeSpace() >= FreeSpace)
            {
                Console.WriteLine("go to shopping");
                return;
            }
            var freeSpace=this.GetFreeSpace();
            List<Item> items = new();
            items.AddRange(this.DairyProducts3(ref freeSpace));
            if (freeSpace>=29)
            {
                foreach(var item in items)
                {
                    this.TookItem(item.Id);
                    Console.WriteLine("Throw in the trash "+item.Name);
                }
                Console.WriteLine("go to shopping");
                return;
            }
            items.AddRange(this.MeatProducts(ref freeSpace));
            if (freeSpace >= 29)
            {
                foreach (var item in items)
                {
                    this.TookItem(item.Id);
                    Console.WriteLine("Throw in the trash " + item.Name);
                }
                Console.WriteLine("go to shopping");
                return;
            }
            items.AddRange(this.FurProducts(ref freeSpace));
            if (freeSpace >= 29)
            {
                foreach (var item in items)
                {
                    this.TookItem(item.Id);
                    Console.WriteLine("Throw in the trash " + item.Name);
                }
                Console.WriteLine("go to shopping");
                return;
            }
            Console.WriteLine("it is not the time to go shoping");
        }

        private IEnumerable<Item> MeatProducts(ref int freeSpace)
        {
            List<Item> items = new();
            foreach (var shelf in Shelves)
            {
                foreach (var item in shelf.Items)
                {
                    if (item.Kosher == Kosher.Meaty && item.ExpiryDate.AddDays(-7) <= DateTime.Today)
                    {
                        freeSpace += item.Space;
                        items.Add(item);
                    }
                }
            }
            return items;

        }

        private IEnumerable<Item> FurProducts(ref int freeSpace)
        {
            List<Item> items = new();
            foreach (var shelf in Shelves)
            {
                foreach (var item in shelf.Items)
                {
                    if (item.Kosher == Kosher.Parve && item.ExpiryDate.AddDays(-1) <= DateTime.Today)
                    {
                        freeSpace += item.Space;
                        items.Add(item);
                    }
                }
            }
            return items;
        }
    }

}
