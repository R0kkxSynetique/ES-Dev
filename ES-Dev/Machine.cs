using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_Dev
{
    public class Machine
    {
        public List<Items> itemsList;

        protected double _credit;
        protected double _total;

        public Machine(List<Items> machineList)
        {
            itemsList = machineList;
        }

        public void Insert(double amount)
        {
            _credit += amount;
        }

        public string Choose(string code)
        {
            string message = "";
            Items selectedItem = itemsList.SingleOrDefault(item => item.Code == code);
            if (selectedItem != null)
            {
                if (selectedItem.Price > _credit)
                {
                    return message = "Not enough money!";
                }
                if (selectedItem.Quantity <= 0)
                {
                    return message = "Item " + selectedItem.Name + ": Out of stock!";
                }
                UpdateList(selectedItem);
                return "Vending " + selectedItem.Name;
            }
            else
            {
                return message = "Invalid selection!";
            }
        }

        public double GetChange()
        {
            return Math.Round(_credit, 2);
        }

        public double GetBalance()
        {
            return Math.Round(_total, 2); ;
        }

        private void UpdateList(Items item)
        {
            item.Quantity -= 1;
            _credit -= item.Price;
            _total += item.Price;
        }
    }
}
