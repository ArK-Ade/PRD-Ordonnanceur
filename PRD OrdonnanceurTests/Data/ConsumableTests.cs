using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace PRD_Ordonnanceur.Data.Tests
{
    [TestFixture()]
    public class ConsumableTests
    {
        [Test()]
        public void ConsumableTest()
        {
            int expectedId = 1;
            int expectedQuantity = 5;
            DateTime expectedDelaySupply = DateTime.Now.Date;

            List<Object> calendar = new();
            Consumable _consumable = new Consumable(1, 5, calendar, DateTime.Now.Date);

            Assert.AreEqual(expectedId, _consumable.Id);
            Assert.AreEqual(expectedQuantity, _consumable.QuantityAvailable);
            Assert.AreEqual(expectedDelaySupply, _consumable.DelaySupply);
        }
    }
}