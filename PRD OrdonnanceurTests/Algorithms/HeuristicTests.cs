using NUnit.Framework;
using PRD_Ordonnanceur.Data;
using System;
using System.Collections.Generic;

namespace PRD_Ordonnanceur.Algorithms.Tests
{
    [TestFixture()]
    public class HeuristicTests
    {
        [Test()]
        public void Smallest_index_DTITest()
        {
            List<Step> stepSequence = new();

            DateTime earliestDate1 = DateTime.MinValue;
            DateTime earliestDate2 = DateTime.Now;
            DateTime earliestDate3 = DateTime.MaxValue;

            DateTime latestDate = DateTime.Now;

            List<Tank> setTank = new();
            string numberProduct1 = "1";
            string numberProduct2 = "2";

            List<List<String>> consommableQuantity = new();

            DateTime starting_hour = DateTime.Now;
            int next_step = 2;

            OF OF1 = new(1, starting_hour, next_step, stepSequence, earliestDate1, latestDate, setTank, numberProduct1, consommableQuantity);
            OF OF2 = new(2, starting_hour, next_step, stepSequence, earliestDate2, latestDate, setTank, numberProduct2, consommableQuantity);
            OF OF3 = new(3, starting_hour, next_step, stepSequence, earliestDate3, latestDate, setTank, numberProduct1, consommableQuantity);

            List<OF> OFs1 = new();
            OFs1.Add(OF1); 
            OFs1.Add(OF2);
            OFs1.Add(OF3);

            List<OF> OFs2 = new();
            OFs2.Add(OF2);
            OFs2.Add(OF1);
            OFs2.Add(OF3);


            List<OF> OFs3 = new();
            OFs3.Add(OF2);
            OFs3.Add(OF3);
            OFs3.Add(OF1);

            Heuristic heuristic = new();

            int resultIndex = heuristic.Smallest_index_DTI(OFs1);
            int expectedIndex = 0;

            Assert.AreEqual(expectedIndex, resultIndex);

            resultIndex = heuristic.Smallest_index_DTI(OFs2);
            expectedIndex = 1;

            Assert.AreEqual(expectedIndex, resultIndex);

            resultIndex = heuristic.Smallest_index_DTI(OFs3);
            expectedIndex = 2;

            Assert.AreEqual(expectedIndex, resultIndex);
        }

        [Test()]
        public void SortCrescentDtiCrescentDliTest()
        {
            DateTime earliestDate1 = DateTime.MinValue;
            DateTime earliestDate2 = DateTime.Now;
            DateTime earliestDate3 = new(5000, 01, 01);

            DateTime latestDate = DateTime.Now;

            Tank tank1 = new();
            Tank tank2 = new();

            string numberProduct1 = "1";
            string numberProduct2 = "2";

            string[] tableau1 = { "1" };
            string[] tableau2 = { "2" };

            List<Tank> setTank = new();
            List<Step> stepSequence = new();
            List<List<String>> consommableQuantity = new();

            DateTime starting_hour = DateTime.Now;
            int next_step = 2;

            OF OF1 = new(1, starting_hour, next_step, stepSequence, earliestDate1, latestDate, setTank, numberProduct1, consommableQuantity);
            OF OF2 = new(2, starting_hour, next_step, stepSequence, earliestDate2, latestDate, setTank, numberProduct2, consommableQuantity);
            OF OF3 = new(3, starting_hour, next_step, stepSequence, earliestDate3, latestDate, setTank, numberProduct1, consommableQuantity);


            List<OF> OFs1 = new();
            OFs1.Add(OF1);
            OFs1.Add(OF2);
            OFs1.Add(OF3);

            List<OF> OFs2 = new();
            OFs2.Add(OF2);
            OFs2.Add(OF1);
            OFs2.Add(OF3);


            List<OF> OFs3 = new();
            OFs3.Add(OF2);
            OFs3.Add(OF3);
            OFs3.Add(OF1);

            Heuristic heuristic = new();

            OFs1 = heuristic.SortCrescentDtiCrescentDli(OFs1);

            Assert.AreEqual(OFs1[0].EarliestDate, OF1.EarliestDate);
            Assert.AreEqual(OFs1[1].EarliestDate, OF2.EarliestDate);
            Assert.AreEqual(OFs1[2].EarliestDate, OF3.EarliestDate);

            OFs2 = heuristic.SortCrescentDtiCrescentDli(OFs2);

            Assert.AreEqual(OFs2[0].EarliestDate, OF1.EarliestDate);
            Assert.AreEqual(OFs2[1].EarliestDate, OF2.EarliestDate);
            Assert.AreEqual(OFs2[2].EarliestDate, OF3.EarliestDate);

            OFs3 = heuristic.SortCrescentDtiCrescentDli(OFs3);

            Assert.AreEqual(OFs3[0].EarliestDate, OF1.EarliestDate);
            Assert.AreEqual(OFs3[1].EarliestDate, OF2.EarliestDate);
            Assert.AreEqual(OFs3[2].EarliestDate, OF3.EarliestDate);
        }
    }
}