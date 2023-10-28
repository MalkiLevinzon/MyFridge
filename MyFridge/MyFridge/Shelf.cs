using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
public sealed class StringBuilder : System.Runtime.Serialization.ISerializable

{
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        throw new NotImplementedException();
    }
}

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
            Id = UniqueId++;

            FloorNumber = floorNumber;
            int sum = 0;

            foreach (var item in items)
                {
                sum += item.Space;
               
                }
            if (sum < _spaceAvailable)
            {
                _spaceAvailable -= sum;
                Items = items;
            }
            else
                throw new Exception("Shelf could not be initialized. Shelf size: " + _spaceAvailable + " total items size " + sum);

        }



        public override string ToString()
        {
            string stringItems="";

            foreach (var item in Items)
            {
              stringItems=  stringItems.Insert(stringItems.Length,  item.ToString()+"\n");
            }
           

         
            return "Id: " + Id + " floor Number: " + FloorNumber + " items " + stringItems ;


        } 


        public void InsertingItem(Item item)
        {
            if (item!=null&&SpaceAvailable>=item.Space)
            {
                Items.Add(item);
                SpaceAvailable-=item.Space;

            }
        }
        public Item? TookItem(int itemId)
        {
            int id = -1;
            Item? returnItem = null ;

            foreach (Item item in Items)
            {
                if (item.Id == itemId)
                {
                    SpaceAvailable += item.Space;
                    returnItem = item;
                    id=itemId; 
                }
            }
            if(id!=-1)
            {
                Items.RemoveAll(item => item.Id == id);
                return returnItem;
            }
            return returnItem;
        }

        public void CleanExpiredItems()
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
