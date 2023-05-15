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
        public double[] _sales = new double[24];

        protected internal bool _test;
        protected internal DateTime _testDateTime;

        public Machine(List<Items> machineList)
        {
            itemsList = machineList;
            for (int i = 0; i < _sales.Length; i++)
            {
                _sales[i] = 0;
            }
        }

        /// <summary>
        /// Add the <c>amount</c> to the available machine credit.
        /// </summary>
        /// <param name="amount">The amount to be added to the credit balance.</param>
        public void Insert(double amount)
        {
            _credit += amount;
        }

        /// <summary>
        /// Selects an item from the items list based on the provided code.
        /// </summary>
        /// <param name="code">The code representing the selected item.</param>
        /// <returns>A message indicating the result of the selection.</returns>
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

        /// <summary>
        /// Retrieves the remaining credit balance and rounds it to two decimal places.
        /// </summary>
        /// <returns>The remaining credit balance rounded to two decimal places.</returns>
        public double GetChange()
        {
            return Math.Round(_credit, 2);
        }

        /// <summary>
        /// Retrieves the total balance and rounds it to two decimal places.
        /// </summary>
        /// <returns>The total balance rounded to two decimal places.</returns>
        public double GetBalance()
        {
            return Math.Round(_total, 2); ;
        }

        /// <summary>
        /// Updates the quantity of the selected item, adjusts the credit balance, and updates the total balance.
        /// </summary>
        /// <param name="item">The item to be updated.</param>
        private void UpdateList(Items item)
        {
            DateTime saleTime;

            // Check if the test mode is active
            if (_test)
            {
                // Use the testDateTime value if in test mode
                saleTime = _testDateTime;
                _test = false;
            }
            else
            {
                // Use the current system time if not in test mode
                saleTime = DateTime.Now;
            }

            int currentSplit = saleTime.Hour % 24;

            _sales[currentSplit] += item.Price;

            item.Quantity -= 1;
            _credit -= item.Price;
            _total += item.Price;
        }

        /// <summary>
        /// Sets the time to a specific value for testing purposes only.
        /// </summary>
        /// <param name="isoDateTime">The ISO-formatted date and time string to set.</param>
        /// <remarks>
        /// This method should only be used for testing purposes to override the current time with a specific value.
        /// It sets the internal flag to indicate that the test mode is active and assigns the provided time to be used.
        /// </remarks>
        public void SetTime(string isoDateTime)
        {
            _test = true;
            _testDateTime = DateTime.Parse(isoDateTime, null, System.Globalization.DateTimeStyles.RoundtripKind);
        }
    }
}
