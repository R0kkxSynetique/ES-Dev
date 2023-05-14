using Microsoft.VisualStudio.TestTools.UnitTesting;
using ES_Dev;
using System.Collections.Generic;

namespace ES_Dev_Tests
{
    [TestClass]
    public class UnitTest1
    {
        private Items _smarlies = new("Smarlies", "A01", 10, 1.60);
        private Items _carampar = new("Carampar", "A02", 5, 0.60);
        private Items _avril = new("Avril", "A03", 2, 2.10);
        private Items _kokokola = new("KokoKola", "A04", 1, 2.95);

        public List<Items> ItemList;

        [TestMethod]
        public void Case1()
        {
            ItemList = new(new List<Items> { _smarlies, _carampar, _avril, _kokokola });
            Machine machine = new(ItemList);

            machine.Insert(3.40);
            Assert.AreEqual("Vending Smarlies", machine.Choose("A01"));
            Assert.AreEqual(1.80, machine.GetChange());
        }
        [TestMethod]
        public void Case2()
        {
            ItemList = new(new List<Items> { _smarlies, _carampar, _avril, _kokokola });
            Machine machine = new(ItemList);

            machine.Insert(2.10);
            Assert.AreEqual("Vending Avril", machine.Choose("A03"));
            Assert.AreEqual(0.00, machine.GetChange());
            Assert.AreEqual(2.10, machine.GetBalance());
        }
        [TestMethod]
        public void Case3()
        {
            ItemList = new(new List<Items> { _smarlies, _carampar, _avril, _kokokola });
            Machine machine = new(ItemList);

            Assert.AreEqual("Not enough money!", machine.Choose("A01"));
        }
        [TestMethod]
        public void Case4()
        {
            ItemList = new(new List<Items> { _smarlies, _carampar, _avril, _kokokola });
            Machine machine = new(ItemList);

            machine.Insert(1.00);
            Assert.AreEqual("Not enough money!", machine.Choose("A01"));
            Assert.AreEqual(1.00, machine.GetChange());
            Assert.AreEqual("Vending Carampar", machine.Choose("A02"));
            Assert.AreEqual(0.40, machine.GetChange());
        }
        [TestMethod]
        public void Case5()
        {
            ItemList = new(new List<Items> { _smarlies, _carampar, _avril, _kokokola });
            Machine machine = new(ItemList);

            machine.Insert(1.00);
            Assert.AreEqual("Invalid selection!", machine.Choose("A05"));
        }
        [TestMethod]
        public void Case6()
        {
            ItemList = new(new List<Items> { _smarlies, _carampar, _avril, _kokokola });
            Machine machine = new(ItemList);

            machine.Insert(6.00);
            Assert.AreEqual("Vending KokoKola", machine.Choose("A04"));
            Assert.AreEqual("Item KokoKola: Out of stock!", machine.Choose("A04"));
            Assert.AreEqual(3.05, machine.GetChange());
        }
        [TestMethod]
        public void Case7()
        {
            ItemList = new(new List<Items> { _smarlies, _carampar, _avril, _kokokola });
            Machine machine = new(ItemList);

            machine.Insert(6.00);
            Assert.AreEqual("Vending KokoKola", machine.Choose("A04"));
            machine.Insert(6.00);
            Assert.AreEqual("Item KokoKola: Out of stock!", machine.Choose("A04"));
            Assert.AreEqual("Vending Smarlies", machine.Choose("A01"));
            Assert.AreEqual("Vending Carampar", machine.Choose("A02"));
            Assert.AreEqual("Vending Carampar", machine.Choose("A02"));
            Assert.AreEqual(6.25, machine.GetChange());
            Assert.AreEqual(5.75, machine.GetBalance());
        }
    }
}
