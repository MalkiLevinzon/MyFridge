using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MyFridge
{
    internal class Shelf
    {
        public static int UniqueId { get; set; } = 1;
        public int Id { get; }
        public int FloorNumber { get; }
        private int _spaceAvailable = 400;

        public int SpaceAvailable
        {
            get => _spaceAvailable; set
            {
                if (value <= 0)
                {
                    throw new Exception("There is no space");
                }

                _spaceAvailable = value;
            }
        }
        public List<Item> Items { get; set;}


        public Shelf(int floorNumber, List<Item> items)
        {
            
            Id += UniqueId++;

            FloorNumber = floorNumber;
                //if (items != null)
                //{
                //    foreach (var item in items)
                //    {
                //        if (item == null) items.Remove(item);
                //    }
                foreach (var item in items)
                {
                    if (_spaceAvailable - item.Space >= 0)
                        _spaceAvailable -= item.Space;
                    else
                        items.Remove(item);
                }
            if (items.Count > 0)
                Items = items;
            else
                throw new Exception("The items are not initialized");

        }



        public override string ToString()
        {
            string s = "";
            foreach (var item in Items)
            {
                s += item.ToString();
            }
            return"Id: "+Id+" floor Number: "+FloorNumber+" items "+s;
           

        } 


        public void InsertingItem(Item item)
        {
            if (item!=null&&SpaceAvailable>=item.Space)
            {
                Items.Add(item);
                SpaceAvailable-=item.Space;

            }
        }
        public Item TookItem(int itemId)
        {
            Item item ;
            foreach (Item item2 in Items)
            {
                if (item2.Id == itemId)
                {
                    item = item2;
                    SpaceAvailable += item2.Space;
                    return item;
                }
            }
            throw new NotImplementedException("No item if such ID number");
        }

        public void Cleaning()
        {
            foreach (Item item in Items)
            {
                if (item.ExpiryDate<DateTime.Now) {
                    this.TookItem(item.Id);
                }
            }


        }

        public List<Item> WantToEat(Kosher kosher,KindItem kind)
        {
            List<Item> items = new List<Item>();    
            foreach (Item item in Items)
            {
                if(item.ExpiryDate>DateTime.Now&&item.Kind==kind&&item.Kosher==kosher)
                    items.Add(item);
            }
            return items;   
        }
        public List<Item> SortOfExpiryDate()
        {
            List<Item> items= new List<Item>();
            items.AddRange(Items);
            items.Sort((p1, p2) => p1.ExpiryDate.CompareTo(p2.ExpiryDate));
            return items;

        }



    }

}
