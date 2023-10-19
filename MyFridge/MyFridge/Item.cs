using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFridge
{
    internal class Item
    {
        public static int UniqueId { get; set; } = 1;
        public int Id { get;}
        public string Name { get; set; }
        public KindItem Kind { get; set; }
        public Kosher Kosher { get; set; }
        public DateTime ExpiryDate { get; set; }
        private int _space;
        public int Space
        {
            get { return _space; }
            set
            {
                if (value <= 0) { throw new Exception("invalid space"); }
                _space = value;
            }
        }
        public Item(string name,KindItem kind,Kosher kosher,DateTime expiryDate, int space) {
            Id = UniqueId++;
            Name = name; Kind=kind; Kosher = kosher; Space = space;
            ExpiryDate = expiryDate;
        }
        public override string ToString() => ("id: "+Id+" name: "+Name+" kind+ "+Kind+" expiryDate: "+ExpiryDate+" space: "+Space);

         

    }
}
