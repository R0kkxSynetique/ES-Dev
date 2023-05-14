using System;

namespace ES_Dev
{
    public class Items
    {
        protected string _name;
        protected string _code;
        protected int _quantity;
        protected double _price;

        public string Name
        {
            get => _name;
        }
        public string Code
        {
            get => _code;
        }
        public int Quantity
        {
            get => _quantity;
            set => _quantity = value;
        }
        public double Price
        {
            get => _price;
        }

        public Items(string Name, string Code, int Quantity, double Price)
        {
            _name = Name;
            _code = Code;
            _quantity = Quantity;
            _price = Price;
        }
    }
}
